Imports Solution
Imports System
Imports System.IO
Imports System.Xml


Public Class OrderGet
    Public Shared Function NovosPedidos(ByVal cnpj As String, ByVal id_plataforma As Integer, ByVal token As String, ByVal login As String, ByVal senha As String, ByVal url As String) As ObjReturn
        Dim objRetorno As ObjReturn = New ObjReturn
        Try
            Log.CriaLog("Requisição de pedidos")


            Dim loja = ("https://webservice-" + url + ".vtexcommerce.com.br/Service.svc")

            Dim proxy As wsVtex.ServiceClient = AcessaWebService.GetVtexService(loja, login, senha)
            Dim i As Integer

            Dim listPedidos As IList(Of wsVtex.OrderDTO) = proxy.OrderGetByStatus("CAP")
            'Dim listPedidos As wsVtex.OrderDTO = proxy.OrderGet(id)
            Dim contPed As Integer

            For contPed = 0 To listPedidos.Count - 1
                Dim ped As New PedidosVO
                ped.NFe_emit_CNPJ = cnpj
                ped.fk_id_plataforma = id_plataforma



                ped.num_pedido = listPedidos(contPed).Id
                If listPedidos(contPed).Client.CompanyName <> "" Then
                    ped.xName = listPedidos(contPed).Client.CompanyName
                Else
                    ped.xName = listPedidos(contPed).Client.NickName
                End If

                ped.email = listPedidos(contPed).Client.Email
                If (listPedidos(contPed).Client.CpfCnpj.Length = 14) Then
                    ped.CNPJ = Replace(Replace(listPedidos(contPed).Client.CpfCnpj, "-", ""), ".", "")
                Else
                    ped.CPF = Replace(Replace(listPedidos(contPed).Client.CpfCnpj, "-", ""), ".", "")
                End If
                ped.xLgr = listPedidos(contPed).Address.Street.ToString
                ped.xMun = listPedidos(contPed).Address.City.ToString
                ped.UF = listPedidos(contPed).Address.State.ToString
                ped.CEP = Replace(listPedidos(contPed).Address.ZipCode, "-", "").ToString
                ped.fone = Replace(listPedidos(contPed).Address.Phone, "-", "").ToString
                ped.nro = listPedidos(contPed).Address.Number.ToString
                ped.xCpl = listPedidos(contPed).Address.More.ToString
                ped.xBairro = listPedidos(contPed).Address.Neighborhood.ToString
                ped.xProcDate = (listPedidos(contPed).PurchaseDate)
                ped.fk_xOrderstatusID = 1
                ped.tot_ped = listPedidos(contPed)._Cost



                Dim listDev As IList(Of wsVtex.OrderDeliveryDTO) = listPedidos(contPed).OrderDeliveries
                Dim contDev As Integer
                For contDev = 0 To listDev.Count - 1


                    Dim listItem = Funcs.agrupaItens(listPedidos(contPed).OrderDeliveries(contDev).OrderItems, cnpj, id_plataforma, proxy, listPedidos(contPed).Id)
                    ped.xQtyItems = listItem.Count

                    Dim contItem As Integer
                    For contItem = 0 To listItem.Count - 1
                        Try
                            Pedidos_ItensDAO.inserirPedido_Item(listItem(contItem))

                        Catch ex As Exception
                            Pedidos_ItensDAO.alterarPedido_Item(listItem(contItem))
                        End Try
                    Next contItem
                Next contDev

                Try
                    PedidosDAO.inserirPedido(ped)
                    objRetorno.novo += 1

                Catch ex As Exception

                    objRetorno.atualizados += 1
                End Try

            Next contPed

            Return objRetorno


        Catch ex As Exception
            objRetorno.erro = "(Erro ao baixar os pedidos, favor abrir chamado técnico com a NFE4WEB, informando o problema.);"
            Return objRetorno
        End Try

    End Function

End Class
