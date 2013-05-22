Imports FN4Common

Public Class notaDAO


    Public Shared Function obterNotasParaEnvioDeEmail() As List(Of notaVO)

        Return IBatisNETHelper.Instance.QueryForList(Of notaVO)("obterNotasEmail", Nothing)
    End Function

    Public Shared Sub inserirNota(ByVal nota As FN4Common.notaVO)
        IBatisNETHelper.Instance.Insert("inserirNota", nota)
    End Sub
    Public Shared Sub alterarNota(ByVal nota As FN4Common.notaVO)
        IBatisNETHelper.Instance.Update("alterarNota", nota)
    End Sub
    Public Shared Sub inserirHistorico(ByVal historico As FN4Common.historicoVO)
        IBatisNETHelper.Instance.Insert("inserirHistorico", historico)
    End Sub
End Class
