Imports Solution
Imports System
Imports System.Diagnostics
Imports System.IO
Imports System.Data
Imports System.Xml
Imports Solution.Log
Imports log4net
Imports Solution.Funcs
Imports Solution.Pedidos_ItensDAO
Public Class WebPedidos

    Public Shared Function NovosPedidos(ByVal cnpj As String, ByVal id_plataforma As Integer, ByVal token As String, ByVal login As String, ByVal senha As String) As ObjReturn
        'Response.ContentType = "application/json; charset=utf-8"
        Dim objRetorno As ObjReturn = New ObjReturn
        Try
            Log.CriaLog("Requisição de pedidos")

            Dim doc As XmlDocument = New XmlDocument()
            Dim contadorPed As Integer

            Dim proxy As Solution.wsciashop.wsIntegracaoSoapClient = New Solution.wsciashop.wsIntegracaoSoapClient()
            Dim itens As Solution.Pedidos.PedidoItem = New Pedidos.PedidoItem()
            Dim xmldoc As String

            Dim retorno As Long
            retorno = proxy.Pedidos(login, senha, xmldoc)

            Dim xmlReturn = New XmlDocument()
            xmlReturn.LoadXml(xmldoc)

            'Loop Pedidos
            For contadorPed = 0 To xmlReturn.GetElementsByTagName("receipt").Count - 1
                Dim ped As New PedidosVO
                ped.NFe_emit_CNPJ = cnpj
                ped.fk_id_plataforma = id_plataforma
                ped.num_pedido = xmlReturn.GetElementsByTagName("receipt")(contadorPed).Attributes.ItemOf("order_id").InnerText()
                ped.xName = xmlReturn.GetElementsByTagName("receipt")(contadorPed).SelectSingleNode("receipt_shopper").Attributes.ItemOf("nome").InnerText
                ped.email = xmlReturn.GetElementsByTagName("receipt")(contadorPed).SelectSingleNode("receipt_shopper").Attributes.ItemOf("email").InnerText
                ped.xLgr = xmlReturn.GetElementsByTagName("receipt")(contadorPed).SelectSingleNode("receipt_shopper").Attributes.ItemOf("ship_to_address1").InnerText
                ped.xMun = xmlReturn.GetElementsByTagName("receipt")(contadorPed).SelectSingleNode("receipt_shopper").Attributes.ItemOf("ship_to_address2").InnerText
                ped.UF = xmlReturn.GetElementsByTagName("receipt")(contadorPed).SelectSingleNode("receipt_shopper").Attributes.ItemOf("ship_to_address3").InnerText
                ped.CEP = Replace(xmlReturn.GetElementsByTagName("receipt")(contadorPed).SelectSingleNode("receipt_shopper").Attributes.ItemOf("ship_to_address4").InnerText, "-", "")
                ped.fone = xmlReturn.GetElementsByTagName("receipt")(contadorPed).SelectSingleNode("receipt_shopper").Attributes.ItemOf("ship_to_ddd_phone").InnerText.ToString +
                xmlReturn.GetElementsByTagName("receipt")(contadorPed).SelectSingleNode("receipt_shopper").Attributes.ItemOf("ship_to_phone").InnerText.ToString
                ped.CPF = xmlReturn.GetElementsByTagName("receipt")(contadorPed).SelectSingleNode("receipt_shopper").Attributes.ItemOf("shopper_cpf").InnerText
                ped.nro = xmlReturn.GetElementsByTagName("receipt")(contadorPed).SelectSingleNode("receipt_shopper").Attributes.ItemOf("ship_to_street_number").InnerText
                ped.xCpl = xmlReturn.GetElementsByTagName("receipt")(contadorPed).SelectSingleNode("receipt_shopper").Attributes.ItemOf("ship_to_street_compl").InnerText
                ped.xBairro = xmlReturn.GetElementsByTagName("receipt")(contadorPed).SelectSingleNode("receipt_shopper").Attributes.ItemOf("ship_to_district").InnerText
                ped.email = xmlReturn.GetElementsByTagName("receipt")(contadorPed).SelectSingleNode("receipt_shopper").Attributes.ItemOf("email").InnerText
                ped.xProcDate = xmlReturn.GetElementsByTagName("receipt")(contadorPed).SelectSingleNode("receipt_details").Attributes.ItemOf("date_entered").InnerText

                If ped.num_pedido = 1209 Then
                    Dim aqui = "Parou!"
                End If

                Try
                    PedidosDAO.inserirPedido(ped)
                    objRetorno.novo += 1

                    'Loop Itens
                    Dim pedItem As New Pedidos_ItensVO
                    Dim contadorItem As Integer
                    For contadorItem = 0 To xmlReturn.GetElementsByTagName("receipt")(contadorPed).SelectNodes("receipt_item").Count - 1

                        pedItem.NFe_emit_CNPJ = cnpj
                        pedItem.fk_id_plataforma = id_plataforma
                        pedItem.num_pedido = xmlReturn.GetElementsByTagName("receipt")(contadorPed).SelectNodes("receipt_item")(contadorItem).Attributes.ItemOf("order_id").InnerText
                        pedItem.cProd = xmlReturn.GetElementsByTagName("receipt")(contadorPed).SelectNodes("receipt_item")(contadorItem).Attributes.ItemOf("pf_id").InnerText
                        pedItem.qCom = xmlReturn.GetElementsByTagName("receipt")(contadorPed).SelectNodes("receipt_item")(contadorItem).Attributes.ItemOf("quantity").InnerText

                        Dim vAjust, vPadrao, vDesc As Double
                        vPadrao = CDbl(xmlReturn.GetElementsByTagName("receipt")(contadorPed).SelectNodes("receipt_item")(contadorItem).Attributes.ItemOf("list_price").InnerText) / 100
                        vAjust = (CDbl(xmlReturn.GetElementsByTagName("receipt")(contadorPed).SelectNodes("receipt_item")(contadorItem).Attributes.ItemOf("adjusted_price").InnerText) / 100) / pedItem.qCom

                        If (vPadrao = vAjust) Then
                            pedItem.vUnCom = vPadrao
                        Else
                            pedItem.vUnCom = vAjust
                        End If

                        pedItem.vDesc = (Funcs.calcDesc("disc_value", xmlReturn, contadorPed, contadorItem, pedItem.vUnCom))
                        pedItem.xProd = Funcs.testaCampoItemStr("product_name", xmlReturn, contadorPed, contadorItem)
                        pedItem.NCM = Funcs.testaCampoItemStr("codigo_ncm", xmlReturn, contadorPed, contadorItem)
                        pedItem.uCom = Funcs.testaCampoItemStr("unidade", xmlReturn, contadorPed, contadorItem)
                        pedItem.Orig = Funcs.testaCampoItemInt("origem_mercadoria", xmlReturn, contadorPed, contadorItem)
                        pedItem.subst = Funcs.testaCampoItemInt("st", xmlReturn, contadorPed, contadorItem)
                        Dim totValor As Double
                        totValor = Funcs.vTotItens(xmlReturn, contadorPed)

                        Dim frete As Double
                        Try
                            frete = CDbl(Math.Round(((xmlReturn.GetElementsByTagName("receipt")(contadorPed).SelectSingleNode("receipt_details").Attributes.ItemOf("shipping_cost").InnerText / 100) * pedItem.vUnCom) / totValor, 3))
                        Catch ex As Exception
                            frete = 0.0
                        End Try

                        pedItem.vFrete = frete * pedItem.qCom

                        PedidosDAO.atualizaItem(ped.num_pedido, ped.NFe_emit_CNPJ, ped.fk_id_plataforma, contadorItem + 1)
                        Try
                            Pedidos_ItensDAO.inserirPedido_Item(pedItem)
                        Catch ex As Exception
                            Pedidos_ItensDAO.alterarPedido_Item(pedItem)
                        End Try

                    Next contadorItem
                Catch ex As Exception

                    objRetorno.atualizados += 1
                End Try
            Next contadorPed
            Return objRetorno

        Catch ex As Exception
            Log.CriaLog("Erro:" + ex.Message)
            objRetorno.erro = "(Erro ao baixar os pedidos, favor abrir chamado técnico com a NFE4WEB, informando o problema.);"
            Return objRetorno
        Finally
            ' Verifica se o proxy não está fechado ou com erro e chama o close para fechar a conexão
        End Try
    End Function
End Class
