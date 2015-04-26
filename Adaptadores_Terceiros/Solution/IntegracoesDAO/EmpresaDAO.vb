Public Class EmpresaDAO
    Public Shared Function validaEmpresa(ByVal cnpj As String, ByVal token As String)
        Try
            Dim ht As New Hashtable
            ht.Add("cnpj", cnpj)
            ht.Add("token", token)
            ' empresa As EmpresaVO
            Dim empresa = IBatis.Instance.QueryForObject("validaEmpresa", ht)
            Return empresa.contador
        Catch ex As Exception
            Throw (ex)
        End Try

    End Function
End Class
