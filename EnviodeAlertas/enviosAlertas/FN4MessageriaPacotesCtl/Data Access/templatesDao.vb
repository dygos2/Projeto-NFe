Imports FN4Common

Public Class templatesDao

    Public Shared Function obterTemplate(ByVal id As Integer) As String
        Dim ht As New Hashtable
        ht.Add("id", id)
        Return IBatisNETHelper.Instance.QueryForList(Of templateVO)("obtertemplates", ht)

    End Function

End Class
