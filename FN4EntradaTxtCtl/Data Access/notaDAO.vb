Imports FN4Common

Public Class notaDAO


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

    Public Shared Sub inserirNota(ByVal nota As FN4Common.notaVO)
        Try
            IBatisNETHelper.Instance.Insert("inserirNota", nota)
        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Public Shared Sub alterarNota(ByVal nota As FN4Common.notaVO)
        Try
            IBatisNETHelper.Instance.Update("alterarNota", nota)
        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Public Shared Sub inserirHistorico(ByVal historico As FN4Common.historicoVO)
        Try
            IBatisNETHelper.Instance.Insert("inserirHistorico", historico)
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub New()

    End Sub
End Class
