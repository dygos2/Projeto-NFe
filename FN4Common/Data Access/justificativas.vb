Namespace DataAccess
    Public Class justificativas
        Public Shared Function obterJustificativa(ByVal idNota As Integer, ByVal serie As Integer, ByVal cnpj As String) As FN4Common.justificativaVO
            Dim ht As New Hashtable

            ht.Add("idNota", idNota)
            ht.Add("serie", serie)
            ht.Add("cnpj", cnpj)

            Return IBatisNETHelper.Instance.QueryForObject("obterJustificativa", ht)
        End Function

        Public Shared Function inserirJustificativa(ByVal justificativa As justificativaVO) As Integer
            Return IBatisNETHelper.Instance.Insert("inserirJustificativa", justificativa)
        End Function

        Public Shared Sub removerJustificativas(ByVal idNota As Integer, ByVal serie As Integer, ByVal cnpj As String)
            Dim ht As New Hashtable

            ht.Add("idNota", idNota)
            ht.Add("serie", serie)
            ht.Add("cnpj", cnpj)

            IBatisNETHelper.Instance.Delete("cancelarJustificativas", ht)
        End Sub
    End Class
End Namespace

