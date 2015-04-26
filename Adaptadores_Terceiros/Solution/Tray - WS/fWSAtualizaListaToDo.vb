Imports Solution
Public Class fWSAtualizaListaToDo
    Public Shared Function SincronizaPedido(ByVal cnpj As String, ByVal id_plataforma As Integer, ByVal token As String, ByVal idLoja As Integer, ByVal login As String, ByVal senha As String) As ObjReturn
        Dim proxy As wsTrayVirtJo.TrayWebServicewsdl = New wsTrayVirtJo.TrayWebServicewsdl()
        Dim objRetorno As ObjReturn = New ObjReturn
        Dim pedidosEmpresa = PedidosDAO.pegaPedidos(cnpj, id_plataforma)

        Dim n As Integer
        For n = 0 To pedidosEmpresa.Count - 1
            proxy.fWSAtualizaListaToDo(idLoja, login, senha, "pedidos", pedidosEmpresa(n).num_pedido)
        Next n

        Try
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
