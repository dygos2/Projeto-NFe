Imports FN4Common

Public Class notaDAO


    Public Shared Function obterNotasNaoEnviadas() As List(Of FN4Common.notaVO)
        Dim ht As New Hashtable
        ht.Add("naoProcessada", 0)
        'ht.Add("DPECEnviada", 51)
        Return IBatisNETHelper.Instance.QueryForList(Of FN4Common.notaVO)("obterNotasProcessadas", ht)
    End Function

    Public Shared Function obterNotasEmContingenciaPorCNPJ(ByVal cnpj As String) As List(Of FN4Common.notaVO)
        Dim ht As New Hashtable
        ht.Add("value", 5)
        ht.Add("cnpj", cnpj)
        Return IBatisNETHelper.Instance.QueryForList(Of FN4Common.notaVO)("obterNotasProcessadasPorCNPJ", ht)
    End Function

    Public Shared Function obterCNPJsComNotasEmContingencia()
        Return IBatisNETHelper.Instance.QueryForList(Of String)("obterCNPJComNotaNaoEnviada", 5)
    End Function

    Public Shared Function obterNotasEmContingencia() As List(Of FN4Common.notaVO)
        Return IBatisNETHelper.Instance.QueryForList(Of FN4Common.notaVO)("obterNotasProcessadas", 4)
    End Function

    Public Shared Sub alterarNota(ByVal nota As FN4Common.notaVO)
        IBatisNETHelper.Instance.Update("alterarNota", nota)
    End Sub

    Public Shared Sub inserirHistorico(ByVal historico As FN4Common.historicoVO)
        IBatisNETHelper.Instance.Insert("inserirHistorico", historico)
    End Sub

    Public Shared Function obterNotaPorId(ByVal id As Integer) As notaVO
        Try
            Return IBatisNETHelper.Instance.QueryForObject("obterNotaPorId", id)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function obterNotasSemProtocolo()
        Dim ht As New Hashtable
        ht.Add("semprotacancelar", 18)
        ht.Add("semprot", 19)
        ht.Add("semprot_conting", 17)
        Return IBatisNETHelper.Instance.QueryForList(Of FN4Common.notaVO)("obterNotasSemProtocolo", ht)
    End Function
End Class
