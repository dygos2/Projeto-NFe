Public Class PedidosDAO
    Public Shared Sub inserirPedido(ByVal ped As PedidosVO)
        IBatis.Instance.Insert("inserirPedido", ped)
    End Sub

    Public Shared Sub alterarPedido(ByVal ped As PedidosVO)
        IBatis.Instance.Update("alterarPedido", ped)
    End Sub

    Public Shared Sub baixarPedido(ByVal num_pedido As String, ByVal NFe_emit_CNPJ As String, ByVal fk_id_plataforma As Integer)
        Try
            Dim ht As New Hashtable
            ht.Add("num_pedido", num_pedido)
            ht.Add("NFe_emit_CNPJ", NFe_emit_CNPJ)
            ht.Add("fk_id_plataforma", fk_id_plataforma)
            IBatis.Instance.Update("baixarPedido", ht)
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Function pegaPedidos(ByVal NFe_emit_CNPJ As String, ByVal fk_id_plataforma As Integer) As List(Of PedidosVO)
        Try
            Dim ht As New Hashtable
            ht.Add("NFe_emit_CNPJ", NFe_emit_CNPJ)
            ht.Add("fk_id_plataforma", fk_id_plataforma)
            Return IBatis.Instance.QueryForList(Of PedidosVO)("pegaPedidos", ht)

        Catch ex As Exception
            Throw (ex)
        End Try

    End Function

    Public Shared Function atualizaItem(ByVal num_pedido As String, ByVal NFe_emit_CNPJ As String, ByVal fk_id_plataforma As Integer, ByRef xQtyItems As Integer)
        Try
            Dim ht As New Hashtable
            ht.Add("num_pedido", num_pedido)
            ht.Add("NFe_emit_CNPJ", NFe_emit_CNPJ)
            ht.Add("fk_id_plataforma", fk_id_plataforma)
            ht.Add("xQtyItems", xQtyItems)
            Return IBatis.Instance.Update("atualizaItens", ht)
        Catch ex As Exception
            Throw (ex)
        End Try

    End Function


    Public Shared Function pegaTotal(ByVal num_pedido As String) As PedidosVO
        Try
            Dim ht As New Hashtable
            ht.Add("num_pedido", num_pedido)
            Return IBatis.Instance.QueryForObject(Of PedidosVO)("pegaTot_ped", ht)

        Catch ex As Exception
            Throw (ex)
        End Try

    End Function

End Class
