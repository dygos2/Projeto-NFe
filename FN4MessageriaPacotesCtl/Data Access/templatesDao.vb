Imports FN4Common

Public Class templatesDao

    Public Shared Function obterTemplates(ByVal id As Integer) As List(Of templatesVO)
        Dim ht As New Hashtable
        ht.Add("id", id)
        Return IBatisNETHelper.Instance.QueryForList(Of templatesVO)("obtertemplates", ht)

    End Function

End Class
