Imports FN4Common

Public Class notaDAO_cont
    Public Shared Function obterRetornosNotas() As List(Of FN4Common.notaVO)
        Dim ht As New Hashtable
        ht.Add("naoProcessada", 52)
        Return IBatisNETHelper.Instance.QueryForList(Of FN4Common.notaVO)("obterNotasParaEnviar", ht)
    End Function
End Class