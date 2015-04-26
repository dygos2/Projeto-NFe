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

Public Class RetrieveNewOrdersRakuten
    Public Shared Function NovosPedidos(ByVal cnpj As String, ByVal id_plataforma As Integer, ByVal token As String, ByVal chavePedido As String, ByVal codLoja As String) As ObjReturn
        Dim doc As XmlDocument = New XmlDocument()
        Dim proxy As pedidos_rakuten_prod.OrderServiceSoapClient = New pedidos_rakuten_prod.OrderServiceSoapClient("OrderServiceSoap")

        Dim objRetorno As ObjReturn = New ObjReturn

        'Dieta Dukan
        'cnpj = 15773074000219
        'chavePedido = "901cea3a-28a8-4cba-bf58-de6f2d973a38"
        'codLoja = "444F9356-AE66-4AC5-BCF1-BF30384E2ED7"
        Dim statPed = "7"
        Try
            Dim ped = proxy.RetrieveNewOrders(cnpj, chavePedido, codLoja, statPed)

            Dim p As Integer
            For p = 0 To ped.Count - 1
                Dim pedido As New PedidosVO
                pedido.NFe_emit_CNPJ = ped(p).MerchantCNPJ
                pedido.fk_id_plataforma = id_plataforma
                pedido.num_pedido = ped(p).Code
                pedido.xName = ped(p).Customer.DisplayName
                pedido.xLgr = ped(p).ShipAddress.Street
                pedido.xMun = ped(p).ShipAddress.City
                pedido.UF = ped(p).ShipAddress.State
                pedido.CEP = Replace(ped(p).ShipAddress.ZipCode, "-", "")
                pedido.fone = Replace(ped(p).ShipAddress.Phones(0).Number, "-", "")
                pedido.CPF = ped(p).Customer.DisplayDocument
                pedido.nro = ped(p).ShipAddress.Number
                pedido.xCpl = ped(p).ShipAddress.Complement
                pedido.xBairro = ped(p).ShipAddress.District
                pedido.xProcDate = ped(p).PaymentCaptureDate
                pedido.email = ped(p).Customer.Email
                pedido.xQtyItems = ped(p).OrderItems.Products.Count
                pedido.fk_xOrderstatusID = 1

                Try
                    PedidosDAO.inserirPedido(pedido)
                    objRetorno.novo += 1

                    '//itens
                    Dim totValor = 0.0
                    Dim i As Integer
                    Dim s As Integer
                    For i = 0 To ped(p).OrderItems.Products.Count - 1
                        For s = 0 To ped(p).OrderItems.Products(i).SKUs.Count - 1
                            totValor = totValor + (ped(p).OrderItems.Products(i).SKUs(s).PromotionalPrice)
                        Next s
                    Next i

                    For i = 0 To ped(p).OrderItems.Products.Count - 1
                        For s = 0 To ped(p).OrderItems.Products(i).SKUs.Count - 1
                            Dim pedItem As New Pedidos_ItensVO
                            Dim sku_checkout = New Solution.pedidos_rakuten_prod.SKUCheckout()
                            sku_checkout = ped(p).OrderItems.Products(i).SKUs(s)
                            pedItem.NFe_emit_CNPJ = ped(p).MerchantCNPJ
                            pedItem.fk_id_plataforma = id_plataforma
                            pedItem.num_pedido = ped(p).Code
                            pedItem.cProd = ped(p).OrderItems.Products(i).InternalCode.ToString
                            'If (sku_checkout.PartNumber.ToString.Length < 6) Then
                            ' pedItem.cProd = ped(p).OrderItems.Products(i).Category.InternalCode.ToString + sku_checkout.PartNumber.ToString
                            'Else
                            'pedItem.cProd = sku_checkout.PartNumber.ToString
                            'End If
                            pedItem.xProd = ped(p).OrderItems.Products(i).Name
                            pedItem.qCom = sku_checkout.Quantity

                            If (pedido.NFe_emit_CNPJ = "15773074000219") Then
                                If (sku_checkout.PromotionalPrice = 0.0) Then
                                    pedItem.vUnCom = sku_checkout.RealPrice
                                Else
                                    pedItem.vUnCom = sku_checkout.PromotionalPrice

                                End If
                                pedItem.vDesc = 0.0
                            Else
                                pedItem.vUnCom = sku_checkout.RealPrice
                                pedItem.vDesc = sku_checkout.RealPrice - sku_checkout.PromotionalPrice
                            End If

                            pedItem.vDesc = pedItem.vDesc * pedItem.qCom
                            Dim frete As Double

                            frete = (ped(p).Shipping.ShippingValueBilled * (pedItem.vUnCom - pedItem.vDesc)) / totValor
                            pedItem.vFrete = frete

                            Try
                                Pedidos_ItensDAO.inserirPedido_Item(pedItem)
                            Catch ex As Exception
                                Pedidos_ItensDAO.alterarPedido_Item(pedItem)
                            End Try
                        Next s
                    Next i


                Catch ex As Exception
                    'PedidosDAO.alterarPedido(pedido)
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
