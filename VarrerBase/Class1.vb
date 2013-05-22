Imports FN4Common

Public Class notaDAO
    Public Shared Function obterNotasNaoEnviadas() As List(Of notaVO)
        Return IBatisNETHelper.Instance.QueryForList(Of FN4Common.notaVO)("obterNotasProcessadas", 1)
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

    Public Shared Function obterNotasPendentesDeRetorno() As List(Of notaVO)
        Dim ht As New Hashtable
        ht.Add("enviadasNormal", 1)
        ht.Add("enviadasDPEC", 52)

        Return IBatisNETHelper.Instance.QueryForList(Of FN4Common.notaVO)("obternotasPendentesDeRetorno", ht)
    End Function
End Class