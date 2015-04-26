Imports Solution
Imports System
Imports System.Diagnostics
Imports System.IO
Imports System.Data
Imports System.Xml
Imports log4net
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Public Class Funcs

    Public Shared Function validaEmpresa(ByVal cnpj As String, ByVal token As String) As Integer
        Try
            Return EmpresaDAO.validaEmpresa(cnpj, token)
        Catch ex As Exception
            Throw (ex)
        End Try

    End Function

    Public Shared Function testaCampoItemStr(ByVal campo As String, ByRef xml As XmlDocument, ByRef contPed As Integer, ByVal contItem As Integer) As String
        Try
            Return xml.GetElementsByTagName("receipt")(contPed).SelectNodes("receipt_item")(contItem).Attributes.ItemOf(campo).InnerText
        Catch ex As Exception
            Return ""
        End Try
    End Function
        
    Public Shared Function testaCampoItemInt(ByVal campo As String, ByRef xml As XmlDocument, ByRef contPed As Integer, ByVal contItem As Integer) As Integer
        Try
            Return xml.GetElementsByTagName("receipt")(contPed).SelectNodes("receipt_item")(contItem).Attributes.ItemOf(campo).InnerText
        Catch ex As Exception
            Return 0
        End Try
    End Function


    Public Shared Function calcDesc(ByVal campo As String, ByRef xml As XmlDocument, ByRef contPed As Integer, ByVal contItem As Integer, ByVal vItem As Double) As Double

        Dim descI, descP, percDesc As Double

        Try
            percDesc = CDbl(xml.GetElementsByTagName("receipt")(contPed).SelectSingleNode("receipt_details").Attributes.ItemOf("discount_total").InnerText)
            descP = Math.Round((vItem / 100) * percDesc, 3)
        Catch eex As Exception

        End Try

        Try
            descI = CDbl(xml.GetElementsByTagName("receipt")(contPed).SelectNodes("receipt_item")(contItem).Attributes.ItemOf(campo).InnerText / 100)
        Catch ex As Exception
           
        End Try
        Dim desc = descI + descP
        Return desc
    End Function

    Public Shared Function vTotItens(ByRef xml As XmlDocument, ByRef contPed As Integer) As Double
        Dim vTot As Double

        Dim contadorItem As Integer
        For contadorItem = 0 To xml.GetElementsByTagName("receipt")(contPed).SelectNodes("receipt_item").Count - 1

            'Dim vAjust, vPadrao, qCom, vUnCom As Double
            'qCom = xml.GetElementsByTagName("receipt")(contPed).SelectNodes("receipt_item")(contadorItem).Attributes.ItemOf("quantity").InnerText
            'vPadrao = CDbl(xml.GetElementsByTagName("receipt")(contPed).SelectNodes("receipt_item")(contadorItem).Attributes.ItemOf("list_price").InnerText) / 100
            'vAjust = (CDbl(xml.GetElementsByTagName("receipt")(contPed).SelectNodes("receipt_item")(contadorItem).Attributes.ItemOf("adjusted_price").InnerText) / 100) / qCom

            'If (vPadrao = vAjust) Then
            'vUnCom = vPadrao
            'Else
            'vUnCom = vAjust
            'End If

            vTot = vTot + (CDbl(xml.GetElementsByTagName("receipt")(contPed).SelectNodes("receipt_item")(contadorItem).Attributes.ItemOf("adjusted_price").InnerText) / 100)
        Next contadorItem

        Return vTot
    End Function

    Public Shared Function agrupaItens(ByVal listItens As IList(Of wsVtex.OrderItemDTO), ByVal cnpj As String, ByVal id_plataforma As Integer, ByVal proxy As wsVtex.ServiceClient, ByVal num_pedido As String) As IList(Of Pedidos_ItensVO)
        Dim contDTO As Integer

        Dim ItensVO As IList(Of Pedidos_ItensVO) = New List(Of Pedidos_ItensVO)
        Dim descProd As wsVtex.ProductDTO = proxy.ProductGet(listItens(0).ProductId)

        Dim Item As New Pedidos_ItensVO 'adiciona o item um a lista
        Item.NFe_emit_CNPJ = cnpj
        Item.fk_id_plataforma = id_plataforma
        Item.num_pedido = num_pedido
        Item.cProd = listItens(0).ProductId
        Item.xProd = descProd.Name
        Item.qCom = 1
        Item.vUnCom = listItens(0).Cost


        Item.vFrete = listItens(0).ShippingCostOff
        Item.vDesc = listItens(0).Cost - listItens(0).CostOff

        ItensVO.Add(Item)
        Dim lastNr = 0
        For contDTO = 1 To listItens.Count - 1

            If ItensVO(lastNr).cProd = listItens(contDTO).ProductId Then   '//verifica se o item atual é igual ao anterior
                ItensVO(lastNr).qCom += 1 '//se fora adiciona um a quantidade de acordo com item anterior
                ItensVO(lastNr).vDesc += listItens(lastNr).Cost - listItens(lastNr).CostOff '// soma o desconto do item para a nova qtd
            Else
                Dim pedItem As New Pedidos_ItensVO                              '// senão cria um novo item e adiciona a lista
                Dim descPro As wsVtex.ProductDTO = proxy.ProductGet(listItens(contDTO).ProductId)
                lastNr += 1

                pedItem.NFe_emit_CNPJ = cnpj
                pedItem.fk_id_plataforma = id_plataforma
                pedItem.num_pedido = num_pedido
                pedItem.cProd = listItens(contDTO).ProductId
                pedItem.xProd = descPro.Name
                pedItem.qCom = 1
                pedItem.vUnCom = listItens(contDTO).Cost
                pedItem.vFrete = listItens(contDTO).ShippingCost
                pedItem.vDesc = listItens(contDTO).Cost - listItens(contDTO).CostOff

                ItensVO.Add(pedItem)

            End If

            'For contItem = 0 To ItensVO.Count - 1
            ' If (ItensVO(contItem).cProd = CStr(listItens(contDTO).ProductId)) Then
            ' ItensVO(contItem).qCom = ItensVO(contItem).qCom + 1
            ' ElseIf contItem + 1 = ItensVO.Count Then
            ' Dim pedItem As New Pedidos_ItensVO
            ' Dim descPro As wsVtex.ProductDTO = proxy.ProductGet(listItens(contDTO).ProductId)
            ' pedItem.NFe_emit_CNPJ = cnpj
            ' pedItem.fk_id_plataforma = id_plataforma
            ' pedItem.num_pedido = listItens(contDTO).Id
            ' pedItem.cProd = listItens(contDTO).ProductId
            ' pedItem.xProd = descPro.DescriptionShort
            ' pedItem.qCom = 1
            ' pedItem.vUnCom = listItens(contDTO).Cost
            ' pedItem.vFrete = listItens(contDTO).ShippingCost
            ' pedItem.vDesc = listItens(contDTO).Cost - listItens(contDTO).CostOff
            ' ItensVO.Add(pedItem)
            ' End If

            'Next contItem
        Next contDTO
        Return ItensVO
    End Function

    Public Shared Function isNulo(ByVal texto As String) As String

        If (IsDBNull(texto)) Then
            Return ""
        Else
            Return texto
        End If

    End Function

    Public Shared Function encUTF8(ByVal texto As String) As String

        Dim bytesUTF8 As Byte() = System.Text.Encoding.Default.GetBytes(texto)

        Dim fromUTF8 As String = System.Text.Encoding.UTF8.GetString(bytesUTF8)

        Return fromUTF8

    End Function

    Public Shared Function pegaMetas(ByVal metas As JArray, ByVal nfe_emit_cnpj As String, ByVal id_plataforma As Integer) As List(Of produto_fiscalVO)
        Dim listProds = New List(Of produto_fiscalVO)

        Dim prod = New produto_fiscalVO
        Dim prev = metas(0).Item("resourceId").ToString
        prod.cProd = prev
        prod.NFe_emit_CNPJ = nfe_emit_cnpj
        prod.fk_id_plataforma = id_plataforma
        Select Case metas(0).Item("key").ToString
            Case "NCM"
                prod.NCM = CInt(metas(0).Item("value").ToString)
            Case "orig"
                prod.orig = CInt(metas(0).Item("value").ToString)
            Case "subst"
                prod.subst = CInt(metas(0).Item("value").ToString)
            Case "uCom"
                prod.uCom = (metas(0).Item("value").ToString)
        End Select




        For i = 1 To metas.Count - 1
            'verifica se o id do item é igual ao da iteração anterior
            If (prev = metas(i).Item("resourceId").ToString) Then
                'se for, é o mesmo item porem um novo campo
                Select Case metas(i).Item("key").ToString
                    Case "NCM"
                        prod.NCM = CInt(metas(i).Item("value").ToString)
                    Case "orig"
                        prod.orig = CInt(metas(i).Item("value").ToString)
                    Case "subst"
                        prod.subst = CInt(metas(i).Item("value").ToString)
                    Case "uCom"
                        prod.uCom = (metas(i).Item("value").ToString)
                End Select

            Else
                'se não for, é um novo item. Cria um novo produto e inseri o primeiro campo
                listProds.Add(prod)

                prod = New produto_fiscalVO
                prev = metas(i).Item("resourceId").ToString
                prod.cProd = prev
                prod.NFe_emit_CNPJ = nfe_emit_cnpj
                prod.fk_id_plataforma = id_plataforma
                Select Case metas(i).Item("key").ToString
                    Case "NCM"
                        prod.NCM = CInt(metas(i).Item("value").ToString)
                    Case "orig"
                        prod.orig = CInt(metas(i).Item("value").ToString)
                    Case "subst"
                        prod.subst = CInt(metas(i).Item("value").ToString)
                    Case "uCom"
                        prod.uCom = (metas(i).Item("value").ToString)
                End Select


            End If


        Next i
        'add o ultimo produto a ser criado
        listProds.Add(prod)

        Return listProds

    End Function

End Class
