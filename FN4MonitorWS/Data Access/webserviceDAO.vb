﻿Imports FN4Common

Public Class webserviceDAO
    Public Shared Function obterURLWebservice(ByVal UF As String, ByVal NomeServico As String, ByVal Versao As String, ByVal Homologacao As Integer) As webserviceVO
        Try
            Dim ht As New Hashtable
            ht.Add("uf", UF)
            ht.Add("nome", NomeServico)
            ht.Add("versao", Versao)
            ht.Add("homologacao", Integer.Parse(Homologacao))

            Return IBatisNETHelper.Instance.QueryForObject("obterWebservice", ht)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
