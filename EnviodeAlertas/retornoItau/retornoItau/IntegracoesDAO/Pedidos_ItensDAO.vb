Public Class Pedidos_ItensDAO
    Public Shared Sub inserirPedido_Item(ByVal pedItem As Pedidos_ItensVO)
        IBatis.Instance.Insert("inserirPedido_Item", pedItem)
    End Sub

    Public Shared Sub alterarPedido_Item(ByVal pedItem As Pedidos_ItensVO)
        IBatis.Instance.Update("alterarPedido_Item", pedItem)
    End Sub

    Public Shared Function pegaItens(ByVal cnpj As String, ByVal num_pedido As String) As List(Of Pedidos_ItensVO)
        Try
            Dim ht As New Hashtable
            ht.Add("NFe_emit_CNPJ", cnpj)
            ht.Add("num_pedido", num_pedido)
            Return IBatis.Instance.QueryForList(Of Pedidos_ItensVO)("pegaItens", ht)

        Catch ex As Exception
            Throw (ex)
        End Try
    End Function


End Class
