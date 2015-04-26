

Public Class notas

    Public Shared Function obterNotasdoCnpj(ByVal cnpj As String) As List(Of notaVO)
        Try
            Dim ht As New Hashtable
            ht.Add("nfe_emit_cnpj", cnpj)
            Dim listaNotas As List(Of notaVO)
            listaNotas = IBatis.Instance.QueryForList(Of notaVO)("obterNotasPorNumeroCNPJ", ht)
            Return listaNotas
        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class