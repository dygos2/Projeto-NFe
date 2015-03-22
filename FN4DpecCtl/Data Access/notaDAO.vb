Imports FN4Common
Public Class notaDAO
    Public Shared Function obterCNPJsComNotasEmContingencia()
        Return IBatisNETHelper.Instance.QueryForList(Of String)("obterCNPJComNotaNaoEnviada", 5)
    End Function
    Public Shared Function obterNotasEmContingenciaPorCNPJ(ByVal cnpj As String) As List(Of FN4Common.notaVO)
        Dim ht As New Hashtable
        ht.Add("value", 5)
        ht.Add("cnpj", cnpj)
        Return IBatisNETHelper.Instance.QueryForList(Of FN4Common.notaVO)("obterNotasProcessadasPorCNPJ", ht)
    End Function
    Public Shared Sub alterarNota(ByVal nota As FN4Common.notaVO)
        IBatisNETHelper.Instance.Update("alterarNota", nota)
    End Sub

    Public Shared Sub inserirHistorico(ByVal historico As FN4Common.historicoVO)
        IBatisNETHelper.Instance.Insert("inserirHistorico", historico)
    End Sub
    Public Shared Function obterNotasSemProtocolo()
        Return IBatisNETHelper.Instance.QueryForList(Of FN4Common.notaVO)("obterNotasSemProtocolo", 19)
    End Function
End Class
