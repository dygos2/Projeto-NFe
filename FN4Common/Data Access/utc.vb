Namespace DataAccess
    Public Class utc
        Public Shared Function obterUTC(ByVal uf As String) As FN4Common.utcVO
            Dim ht As New Hashtable

            ht.Add("uf", uf)
            Return IBatisNETHelper.Instance.QueryForObject("obterUtc", ht)
        End Function
    End Class
End Namespace

