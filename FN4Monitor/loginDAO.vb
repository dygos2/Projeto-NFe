Public Class loginDAO

    Public Shared Function obterUsuario(ByVal email As String, ByVal senha As String) As FN4Common.usuarioVO
        Dim ht As New Hashtable
        ht.Add("email", email)
        ht.Add("senha", senha)

        Return FN4Common.IBatisNETHelper.Instance.QueryForObject("obterUsuario", ht)
    End Function

End Class
