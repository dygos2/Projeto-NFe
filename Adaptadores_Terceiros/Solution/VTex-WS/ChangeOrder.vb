Imports Solution
Imports System
Imports System.IO
Imports System.Xml
Public Class ChangeOrder
    Public Shared Function SincronizaPedido(ByVal cnpj As String, ByVal id_plataforma As Integer, ByVal token As String, ByVal login As String, ByVal senha As String, ByVal url As String) As ObjReturn
        Dim objRetorno As ObjReturn = New ObjReturn
        Try
            Log.CriaLog("Sincronização de pedidos")

            Dim loja = ("https://webservice-" + url + ".vtexcommerce.com.br/Service.svc")

            Dim proxy As wsVtex.ServiceClient = AcessaWebService.GetVtexService(loja, login, senha)

            Dim listPedidos = PedidosDAO.pegaPedidos(cnpj, id_plataforma)
            Dim i As Integer

            For i = 0 To listPedidos.Count - 1
                Try
                    '  proxy.OrderChangeStatus(listPedidos(i).num_pedido, "AES")
                    proxy.OrderChangeStatus(listPedidos(i).num_pedido, "ETR")
                    proxy.OrderChangeStatus(listPedidos(i).num_pedido, "ENT")
                    PedidosDAO.baixarPedido(listPedidos(i).num_pedido, cnpj, id_plataforma)
                Catch ex As Exception
                    'MsgBox("falha na sinzonização do pedido nº " + listPedidos(i).num_pedido)
                End Try

            Next i
            objRetorno.sincronizados = i

            Return objRetorno
        Catch ex As Exception
            objRetorno.erro = "Erro ao baixar os pedidos, favor abrir chamado técnico com a NFE4WEB, informando o problema." + CStr(ex.Message)
            Return objRetorno
        End Try
    End Function

End Class
