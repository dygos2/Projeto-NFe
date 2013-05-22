

Namespace DataAccess
    Public Class series
        Public Shared Function obterSeries() As List(Of serieVO)
            Dim listaSeries As List(Of serieVO)
            listaSeries = IBatisNETHelper.Instance.QueryForList(Of serieVO)("obterSeries", Nothing)

            Return listaSeries
        End Function

        Public Shared Function obterSeriesPorCnpj(ByVal cnpj As String) As List(Of serieVO)
            Dim listaSeries As List(Of serieVO)

            listaSeries = IBatisNETHelper.Instance.QueryForList(Of serieVO)("obterSeriesPorCNPJ", cnpj)

            Return listaSeries
        End Function

        Public Shared Function obterNotasPorSerie(ByVal numero As Integer) As List(Of Integer)
            Dim listaNotas As List(Of Integer)
            listaNotas = IBatisNETHelper.Instance.QueryForList(Of Integer)("obterNotasPorSerie", numero)

            Return listaNotas
        End Function

        Public Shared Function obterNotasPorSerieECnpj(ByVal serie As Integer, ByVal cnpj As String) As List(Of Integer)
            Dim listaNotas As List(Of Integer)
            Dim ht As New Hashtable

            ht.Add("serie", serie)
            ht.Add("cnpj", cnpj)

            listaNotas = IBatisNETHelper.Instance.QueryForList(Of Integer)("obterNotasPorSerieECnpj", ht)

            Return listaNotas
        End Function

        Public Shared Function obterNotasExistentesASeremInutilizadas(ByVal numero As Integer, ByVal cnpj As String) As List(Of Integer)
            Dim listaDeNumeros As List(Of Integer)
            Dim ht As New Hashtable

            ht.Add("serie", numero)
            ht.Add("cnpj", cnpj)

            listaDeNumeros = IBatisNETHelper.Instance.QueryForList(Of Integer)("obterNotasExistentesASeremInutilizadas", ht)

            Return listaDeNumeros
        End Function
    End Class
End Namespace