Imports FN4Common

Public Class clientesDao

    Public Shared Function obterClientesPacotesaExpirar() As List(Of clientesVO)
        Try
            Return IBatisNETHelper.Instance.QueryForList(Of clientesVO)("obterpacotesaexpirar", Nothing)
        Catch ex As Exception
            Log.registrarErro("Erro: " & ex.Message, "MessageriaService")
            Return Nothing
        End Try
    End Function

End Class
