Imports FN4Common

Public Class empresaDAO

    Public Shared Function obterEmpresa(ByVal cnpj, ByVal cpf) As FN4Common.empresaVO
        Try
            Dim ht As New Hashtable
            ht.Add("cnpj", cnpj)
            ht.Add("cpf", cpf)

            Return IBatisNETHelper.Instance.QueryForObject("obterEmpresa", ht)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
