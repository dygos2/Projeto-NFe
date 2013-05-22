Imports FN4Common

Public Class notaDAO


    Public Shared Function obterNotasImpressao() As List(Of FN4Common.notaVO)
        Return IBatisNETHelper.Instance.QueryForList(Of FN4Common.notaVO)("obterNotasImpressao", Nothing)
    End Function

    Public Shared Function obterNotasContingencia() As List(Of FN4Common.notaVO)
        Return IBatisNETHelper.Instance.QueryForList(Of FN4Common.notaVO)("obterNotasContingencia", Nothing)
    End Function

    Public Shared Sub alterarNota(ByVal nota As FN4Common.notaVO)
        IBatisNETHelper.Instance.Update("alterarNota", nota)
    End Sub

    Public Shared Sub inserirHistorico(ByVal historico As FN4Common.historicoVO)
        IBatisNETHelper.Instance.Insert("inserirHistorico", historico)
    End Sub
End Class
