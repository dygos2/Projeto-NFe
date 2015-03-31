Imports FN4Common

Public Class notaDAO


    Public Shared Sub inserirNota(ByVal nota As FN4Common.notaVO)
        IBatisNETHelper.Instance.Insert("inserirNota", nota)
    End Sub

    Public Shared Sub alterar_notas_contingencia(ByVal uf As String, ByVal statusnovo As String, ByVal statusantigo As String)
        Dim ht As New Hashtable
        ht.Add("uf", uf)
        ht.Add("statusnovo", statusnovo)
        ht.Add("statusantigo", statusantigo)

        IBatisNETHelper.Instance.Update("alterarNotasContingencia", ht)
    End Sub

    Public Shared Function obterNotasPendentesDeRetorno() As List(Of notaVO)
        Dim ht As New Hashtable
        ht.Add("enviadasNormal", 1)
        ht.Add("enviadasContingencia", 51)

        Return IBatisNETHelper.Instance.QueryForList(Of FN4Common.notaVO)("obternotasPendentesDeRetorno", ht)
    End Function
    Public Shared Function obterNotasEnviarContingencia() As List(Of FN4Common.notaVO)
        Dim ht As New Hashtable
        ht.Add("naoProcessada", 50)
        Return IBatisNETHelper.Instance.QueryForList(Of FN4Common.notaVO)("obterNotasParaEnviar", ht)
    End Function
    Public Shared Function obterNotasNaoEnviadas() As List(Of FN4Common.notaVO)
        Dim ht As New Hashtable
        ht.Add("naoProcessada", 0)
        Return IBatisNETHelper.Instance.QueryForList(Of FN4Common.notaVO)("obterNotasParaEnviar", ht)
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
    Public Shared Function obterNota(ByVal nNf As Integer, ByVal cnpj As String) As FN4Common.notaVO
        Try
            Dim ht As New Hashtable
            ht.Add("numeroDaNota", nNf)
            ht.Add("CNPJEmitente", cnpj)

            Return IBatisNETHelper.Instance.QueryForObject("obterNotaPorNumeroCNPJ", ht)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function obterNota(ByVal nNf As Integer, ByVal cnpj As String, ByVal serie As Integer) As FN4Common.notaVO
        Try
            Dim ht As New Hashtable
            ht.Add("numeroDaNota", nNf)
            ht.Add("CNPJEmitente", cnpj)
            ht.Add("serie", serie)

            Return IBatisNETHelper.Instance.QueryForObject("obterNotaPorNumeroCNPJ", ht)
        Catch ex As Exception
            Throw ex
        End Try

    End Function


End Class