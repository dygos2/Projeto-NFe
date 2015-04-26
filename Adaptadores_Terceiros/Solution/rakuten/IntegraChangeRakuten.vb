Imports Solution.pedidos_rakuten_prod

Public Class IntegraChangeRakuten
    Public Shared Function SincronizaPedido(ByVal cnpj As String, ByVal id_plataforma As Integer, ByVal token As String, ByVal chavePedido As String, ByVal codLoja As String) As ObjReturn
        Dim proxy As OrderServiceSoapClient = New pedidos_rakuten_prod.OrderServiceSoapClient("OrderServiceSoap")
        Dim objRetorno As ObjReturn = New ObjReturn
        Dim pedidosEmpresa = PedidosDAO.pegaPedidos(cnpj, id_plataforma)

        'Dieta Dukan
        'chavePedido = "901cea3a-28a8-4cba-bf58-de6f2d973a38"
        'codLoja = "444F9356-AE66-4AC5-BCF1-BF30384E2ED7"
        Dim n As Integer
        Dim orderList(pedidosEmpresa.Count) As Decimal

        For n = 0 To pedidosEmpresa.Count - 1
            orderList(n) = pedidosEmpresa(n).num_pedido
        Next n

        Try
            Dim retorno = proxy.IsIntegratedNewOrder(orderList, cnpj, chavePedido, codLoja)

            For n = 0 To pedidosEmpresa.Count - 1
                PedidosDAO.baixarPedido(pedidosEmpresa(n).num_pedido, pedidosEmpresa(n).NFe_emit_CNPJ, pedidosEmpresa(n).fk_id_plataforma)
            Next n
            objRetorno.sincronizados = n
            Return (objRetorno)
        Catch ex As Exception
            objRetorno.erro = "Erro ao baixar os pedidos, favor abrir chamado técnico com a NFE4WEB, informando o problema." + CStr(ex.Message)
            Return objRetorno
        End Try
    End Function
End Class
