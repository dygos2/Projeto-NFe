Imports retornoItau


Public Class clientesDao

    Public Shared Function obterclientesTrial() As List(Of clientesVO)
        Try
            Return IBatisNETHelper.Instance.QueryForList(Of clientesVO)("obterclientesTrial", Nothing)
        Catch ex As Exception
            Log.registrarErro("Erro: " & ex.Message, "MessageriaService")
            Return Nothing
        End Try
    End Function

End Class
