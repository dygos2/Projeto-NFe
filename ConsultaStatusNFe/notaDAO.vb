Imports FN4Common
Public Class notaDAO


    Public Shared Function obterNotas() As List(Of FN4Common.notaVO)
        Return IBatisNETHelper.Instance.QueryForList(Of FN4Common.notaVO)("obterTodasAsNotas", Nothing)
    End Function


    Public Shared Sub alterarNota(ByVal nota As FN4Common.notaVO)
        IBatisNETHelper.Instance.Update("alterarNota", nota)
    End Sub
End Class
