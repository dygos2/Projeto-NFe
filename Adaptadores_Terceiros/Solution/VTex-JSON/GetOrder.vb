Imports System.Net
Imports System.IO
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Web
Imports System




Public Class GetOrder


    Shared Function pegaPedidos(ByVal nomeLoja As String, ByVal token As String, ByVal key As String, ByVal cnpj As String, ByVal id_plataforma As String) As ObjReturn
        'Shared Function pegaPedidos(ByVal nomeLoja As String) As IList(Of PedidosVO)
        Dim objRetorno As ObjReturn = New ObjReturn
        Try
            Dim myWebClient = New WebClient()
            myWebClient.Headers.Add("Accept", "application/json")
            myWebClient.Headers.Add("Content-Type", "application/json")
            myWebClient.Headers.Add("X-VTEX-API-AppToken", token)
            myWebClient.Headers.Add("X-VTEX-API-AppKey", key)


            '//A seguir é demonstrado como trazer os pedidos por Status
            Dim status As String

            status = "ready-for-handling"

            Dim UrlStatus = "http://" + nomeLoja + ".vtexcommercebeta.com.br/api/oms/pvt/orders/?per_page=200&f_status=" + status

            Dim jsonGetOrderByStatus = myWebClient.DownloadString(UrlStatus)
            Dim listPedidos As JObject = JObject.Parse(jsonGetOrderByStatus)


            Dim contPed As Integer
            For contPed = 0 To listPedidos("list").Count - 1

                Dim ped As New PedidosVO
                ped.NFe_emit_CNPJ = cnpj
                ped.fk_id_plataforma = id_plataforma

                ped.num_pedido = listPedidos("list")(contPed).Item("orderId").ToString


                ped.fk_id_plataforma = 11


                Dim UrlGetOrderById = String.Concat("http://" + nomeLoja + ".vtexcommercebeta.com.br/api/oms/pvt/orders/" + ped.num_pedido.ToString)
                Dim jsonGetOrderById = myWebClient.DownloadString(UrlGetOrderById)
                Dim pedido As JObject = JObject.Parse(jsonGetOrderById)

                Dim contItem As Integer
                For contItem = 0 To pedido("items").Count - 1

                    If (contItem = 0) Then
                        ped.xName = Funcs.encUTF8(pedido("clientProfileData").Item("firstName").ToString + " " + pedido("clientProfileData").Item("lastName").ToString)
                        Try
                            ped.email = Left(pedido("clientProfileData").Item("email").ToString, InStr(pedido("clientProfileData").Item("email").ToString, "-") - 1)
                        Catch ex As Exception
                            ped.email = pedido("clientProfileData").Item("email").ToString
                        End Try

                        If ((pedido("clientProfileData").Item("document").ToString.Length = 14)) Then
                            ped.CNPJ = Replace(Replace(pedido("clientProfileData").Item("document").ToString, "-", ""), ".", "")
                        Else
                            ped.CPF = Replace(Replace(pedido("clientProfileData").Item("document").ToString, "-", ""), ".", "")
                        End If
                        ped.xQtyItems = CInt(pedido("items").Count)
                        ped.xLgr = Funcs.encUTF8(pedido("shippingData").Item("address").Item("street").ToString)
                        ped.xMun = Funcs.encUTF8(pedido("shippingData").Item("address").Item("city").ToString)
                        ped.UF = pedido("shippingData").Item("address").Item("state").ToString
                        ped.CEP = Replace(pedido("shippingData").Item("address").Item("postalCode").ToString, "-", "")
                        ped.fone = Replace(pedido("clientProfileData").Item("phone").ToString, "+", "")
                        ped.nro = pedido("shippingData").Item("address").Item("number").ToString
                        ped.xCpl = Funcs.encUTF8(pedido("shippingData").Item("address").Item("complement").ToString)
                        ped.xBairro = Funcs.encUTF8(pedido("shippingData").Item("address").Item("neighborhood").ToString)
                        ped.xProcDate = (pedido("creationDate").ToString)
                        ped.fk_xOrderstatusID = 1
                        ped.tot_ped = pedido("value").ToString


                        Try
                            PedidosDAO.inserirPedido(ped)
                            objRetorno.novo += 1
                        Catch ex As Exception
                            ' PedidosDAO.alterarPedido(ped)
                            objRetorno.atualizados += 1
                        End Try
                    End If

                    Dim pedItem As New Pedidos_ItensVO()

                    pedItem.NFe_emit_CNPJ = cnpj
                    pedItem.fk_id_plataforma = id_plataforma
                    pedItem.num_pedido = pedido("orderId").ToString
                    pedItem.xProd = Funcs.encUTF8(pedido("items")(contItem).Item("name").ToString)
                    Dim texto = Funcs.isNulo(pedido("items")(contItem).Item("refId").ToString)
                    If (texto <> "") Then
                        If cnpj = "15837832000134" Then
                            pedItem.cProd = Funcs.isNulo(pedido("items")(contItem).Item("id").ToString)
                        Else
                            pedItem.cProd = Funcs.isNulo(Replace(Left(pedido("items")(contItem).Item("refId").ToString, InStr(pedido("items")(contItem).Item("refId").ToString, "-") - 1), " ", ""))
                        End If

                    Else
                        pedItem.cProd = texto
                    End If
                    pedItem.qCom = pedido("items")(contItem).Item("quantity").ToString
                    pedItem.vUnCom = CInt(pedido("items")(contItem).Item("price").ToString) / 100
                    pedItem.vFrete = CInt(pedido("shippingData").Item("logisticsInfo")(contItem).Item("price").ToString) / 100
                    Dim dis = 0
                    Dim desconto = 0.0
                    For dis = 0 To pedido("items")(contItem).Item("priceTags").Count - 1

                        Dim ship = Split(pedido("items")(contItem).Item("priceTags")(dis).Item("name").ToString, "-")


                        If (pedido("items")(contItem).Item("priceTags")(dis).Item("name").ToString <> "DISCOUNT@SHIPPINGMARKETPLACE") And
                            (ship(0).ToString <> "discount@shipping") Then

                            If (pedido("items")(contItem).Item("priceTags")(dis).Item("isPercentual").ToString) <> "True" Then
                                desconto = desconto + (CDbl(pedido("items")(contItem).Item("priceTags")(dis).Item("value").ToString) / 100)
                            End If
                        End If

                    Next dis

                    pedItem.vDesc = desconto * -1

                    Try
                        Pedidos_ItensDAO.inserirPedido_Item(pedItem)
                    Catch ex As Exception
                        Pedidos_ItensDAO.alterarPedido_Item(pedItem)
                    End Try

                Next contItem

                Dim request = WebRequest.Create("https://" + nomeLoja + ".vtexcommercebeta.com.br/api/oms/pvt/orders/" + listPedidos("list")(contPed).Item("orderId").ToString + "/start-handling")
                ' Dim request = WebRequest.Create("http://oms.vtexcommerce.com.br/api/oms/" + listPedidos("list")(contPed).Item("orderId").ToString + "/start-handling/?an=" + nomeLoja)
                Try
                    request.Method = "POST"
                    request.Headers.Add("X-VTEX-API-AppToken", token)
                    request.Headers.Add("X-VTEX-API-AppKey", key)

                    Dim SR = New StreamReader(request.GetResponse().GetResponseStream())
                    Dim Result = SR.ReadToEnd()

                Catch ex As Exception

                End Try

            Next contPed

            Return objRetorno

        Catch ex As Exception
            objRetorno.erro = "(Erro ao baixar os pedidos, favor abrir chamado técnico com a NFE4WEB, informando o problema.);" + ex.Message.ToString

            Return objRetorno
        End Try


    End Function


    Shared Function pegaitens(ByVal numPed As String) As IList(Of Pedidos_ItensVO)



        Dim peditens As New Pedidos_ItensVO
        Return peditens
    End Function


End Class
