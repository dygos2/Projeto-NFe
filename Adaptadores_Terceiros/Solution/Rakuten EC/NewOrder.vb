Imports Solution
Imports System
Imports System.Diagnostics
Imports System.Xml
Imports System.Data
Imports System.ServiceModel
Imports Solution.Log
Imports log4net
Imports Solution.Funcs
Imports Solution.Pedidos_ItensDAO

Public Class NewOrder


    Public Shared Function NovosPedidos(ByVal cnpj As String, ByVal id_plataforma As Integer, ByVal token As String, ByVal chavePedido As String, ByVal codLoja As String) As ObjReturn
        ' Dim doc As XmlDocument = New XmlDocument()

        Dim url = "http://loja.bluebeach.com.br/ikcwebservice/pedido.asmx?op=ListarNovos"
        Dim objBinding As BasicHttpBinding = New BasicHttpBinding
        Dim nDefaultLength As Integer = 2000000

        objBinding.ReaderQuotas.MaxDepth = nDefaultLength
        objBinding.ReaderQuotas.MaxArrayLength = nDefaultLength
        objBinding.ReaderQuotas.MaxBytesPerRead = nDefaultLength
        objBinding.ReaderQuotas.MaxNameTableCharCount = nDefaultLength
        objBinding.ReaderQuotas.MaxStringContentLength = nDefaultLength
        objBinding.MaxReceivedMessageSize = nDefaultLength
        objBinding.MaxBufferPoolSize = nDefaultLength
        objBinding.MaxBufferSize = nDefaultLength

        objBinding.CloseTimeout = New TimeSpan(0, 10, 0)
        objBinding.OpenTimeout = objBinding.CloseTimeout
        objBinding.ReceiveTimeout = objBinding.CloseTimeout
        objBinding.SendTimeout = objBinding.CloseTimeout


        Dim proxy As wsRakutenEC.PedidoSoapClient = New wsRakutenEC.PedidoSoapClient(objBinding, New EndpointAddress(url))
        'pedidos_rakuten_prod.OrderServiceSoapClient = New pedidos_rakuten_prod.OrderServiceSoapClient("OrderServiceSoap")
        Dim docXml As XmlAttribute()
        Dim objRetorno As ObjReturn = New ObjReturn
        Dim cls As wsRakutenEC.clsSoapHeader = New wsRakutenEC.clsSoapHeader()
        cls.AnyAttr = docXml

        Try
            Dim ped = proxy.ListarNovos(cls, 0, 7, "7")
          
            Dim p As Integer
            For p = 0 To ped.Lista.Count - 1
                Dim pedido As New PedidosVO
                pedido.NFe_emit_CNPJ = cnpj
                pedido.fk_id_plataforma = id_plataforma
                pedido.num_pedido = ped.Lista(p).PedidoCodigo.ToString

                pedido.xLgr = ped.Lista(p).Logradouro.ToString
                pedido.xMun = ped.Lista(p).Cidade.ToString
                pedido.UF = ped.Lista(p).Estado.ToString
                pedido.CEP = Replace(ped.Lista(p).CEP, "-", "").ToString
                pedido.fone = Replace(ped.Lista(p).Telefone1, "-", "").ToString

                If ped.Lista(p).CPF <> "" Then
                    pedido.CPF = ped.Lista(p).CPF.ToString
                    pedido.xName = ped.Lista(p).NomeDestinatario.ToString()
                Else
                    pedido.CNPJ = ped.Lista(p).CNPJ.ToString
                    pedido.xName = ped.Lista(p).RazaoSocial.ToString
                End If

                pedido.nro = ped.Lista(p).Numero.ToString
                pedido.xCpl = ped.Lista(p).Complemento.ToString
                pedido.xBairro = ped.Lista(p).Bairro.ToString
                pedido.xProcDate = ped.Lista(p).Data
                pedido.email = ped.Lista(p).Email.ToString
                pedido.xQtyItems = ped.Lista(p).Itens.Count
                pedido.fk_xOrderstatusID = 1

                Try
                    PedidosDAO.inserirPedido(pedido)
                    objRetorno.novo += 1

                    '//ITENS
                    Dim totValor = 0.0
                    totValor = ped.Lista(p).ValorFreteCobrado
                    Dim i = 0
                    For i = 0 To ped.Lista(p).Itens.Count - 1

                        Dim pedItem As New Pedidos_ItensVO
                        pedItem.NFe_emit_CNPJ = cnpj
                        pedItem.fk_id_plataforma = id_plataforma
                        pedItem.num_pedido = ped.Lista(p).PedidoCodigo.ToString
                        pedItem.cProd = ped.Lista(p).Itens(i).CodigoInterno.ToString()
                        pedItem.xProd = ped.Lista(p).Itens(i).ItemNome.ToString
                        pedItem.qCom = ped.Lista(p).Itens(i).ItemQtde
                        pedItem.vUnCom = ped.Lista(p).Itens(i).ItemValor
                        pedItem.vDesc = ped.Lista(p).Itens(i).ItemValor - ped.Lista(p).Itens(i).ItemValorFinal
                        pedItem.vFrete = ped.Lista(p).Itens(i).ItemValorFreteCobrado

                        Try
                            Pedidos_ItensDAO.inserirPedido_Item(pedItem)
                        Catch ex As Exception
                            Pedidos_ItensDAO.alterarPedido_Item(pedItem)
                        End Try
                    Next i


                Catch ex As Exception
                    '  PedidosDAO.alterarPedido(pedido)
                    objRetorno.atualizados += 1
                End Try


            Next p
            Return objRetorno

        Catch ex As Exception
            objRetorno.erro = "(Erro ao baixar os pedidos, favor abrir chamado técnico com a NFE4WEB, informando o problema.);"
            Return objRetorno
        End Try

    End Function

End Class
