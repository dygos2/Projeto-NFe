Namespace DataAccess
    Public Class notas
        Public Shared Function obterNotasParaDanfe() As List(Of notaVO)
            Dim listaNotas As List(Of notaVO)
            listaNotas = IBatisNETHelper.Instance.QueryForList(Of notaVO)("obterNotasParaDanfe", Nothing)
            Return listaNotas
        End Function

        Public Shared Function obterNumerosESeriesDasNotas() As List(Of notaAInutilizar)
            Dim listaNotas As List(Of notaAInutilizar)
            listaNotas = IBatisNETHelper.Instance.QueryForList("obterNumerosESeriesDasNotas", Nothing)
            Return listaNotas
        End Function

        Public Shared Function obterNota(ByVal nNf As Integer, ByVal cnpj As String) As FN4Common.notaVO
            Try
                Dim ht As New Hashtable
                ht.Add("numeroDaNota", nNf)
                ht.Add("CNPJEmitente", cnpj)

                Return IBatisNETHelper.Instance.QueryForObject("obterNotaPorNumeroCNPJ", ht)
            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Shared Function obterNotasdoCnpj(ByVal cnpj As String) As List(Of notaVO)
            Try
                Dim ht As New Hashtable
                ht.Add("CNPJEmitente", cnpj)
                Dim listaNotas As List(Of notaVO)
                listaNotas = IBatisNETHelper.Instance.QueryForList(Of notaVO)("obterNotasPorNumeroCNPJ", ht)
                Return listaNotas
            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Shared Sub inserirNota(ByVal nota As FN4Common.notaVO)
            Try
                IBatisNETHelper.Instance.Insert("inserirNota", nota)
            Catch ex As Exception
                Throw ex
            End Try

        End Sub
        Public Shared Sub alterarNota(ByVal nota As FN4Common.notaVO)
            Try
                IBatisNETHelper.Instance.Update("alterarNota", nota)
            Catch ex As Exception
                Throw ex
            End Try

        End Sub

        Public Shared Sub alterarNotaAposPrimeiroInsert(ByVal nota As FN4Common.notaVO)
            Try
                IBatisNETHelper.Instance.Update("alterarNotaAposPrimeiroInsert", nota)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Shared Sub inserirHistorico(ByVal historico As FN4Common.historicoVO)
            Try
                IBatisNETHelper.Instance.Insert("inserirHistorico", historico)
            Catch ex As Exception
                Throw ex
            End Try

        End Sub

        Public Shared Function obterNota(ByVal id As Integer, ByVal cnpj As String, ByVal serie As Integer) As notaVO
            Try
                Dim ht As New Hashtable
                ht.Add("numeroDaNota", id)
                ht.Add("CNPJEmitente", cnpj)
                ht.Add("serie", serie)

                Return IBatisNETHelper.Instance.QueryForObject("obterNotaPorNumeroCNPJ", ht)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Shared Sub inutilizarNotas(ByVal cnpj As String, ByVal serie As Integer, ByVal inicio As Integer, ByVal fim As Integer)
            Dim ht As New Hashtable

            ht.Add("cnpj", cnpj)
            ht.Add("serie", serie)
            ht.Add("inicio", inicio)
            ht.Add("fim", fim)

            IBatisNETHelper.Instance.Update("inutilizarNotas", ht)
        End Sub

        Public Shared Function obterNotasASeremCanceladas() As List(Of notaVO)
            Return IBatisNETHelper.Instance.QueryForList(Of notaVO)("obterNotasASeremCanceladas", Nothing)
        End Function

        Public Shared Sub cancelarNota(ByVal numeroDaNota As Integer, ByVal serie As Integer, ByVal cnpj As String)
            Dim ht As New Hashtable
            ht.Add("nNF", numeroDaNota)
            ht.Add("serie", serie)
            ht.Add("cnpj", cnpj)

            IBatisNETHelper.Instance.Update("cancelarNota", ht)
        End Sub

        Public Shared Function obterNotasASeremInutilizadas() As List(Of notaVO)
            Return IBatisNETHelper.Instance.QueryForList(Of notaVO)("obterNotasASeremInutilizadas", Nothing)
        End Function

        Public Shared Sub inutilizarNota(ByVal numeroDaNota As Integer, ByVal serie As Integer, ByVal cnpj As String)
            Dim ht As New Hashtable
            ht.Add("nNF", numeroDaNota)
            ht.Add("serie", serie)
            ht.Add("cnpj", cnpj)

            IBatisNETHelper.Instance.Update("inutilizarNota", ht)
        End Sub

        Public Shared Sub excluirNota(ByVal numero As Long, ByVal serie As Integer, ByVal cnpj As String)
            Dim ht As New Hashtable
            ht.Add("nNF", numero)
            ht.Add("serie", serie)
            ht.Add("cnpj", cnpj)

            IBatisNETHelper.Instance.Delete("excluirHistorico", ht)
            IBatisNETHelper.Instance.Delete("excluirNota", ht)
        End Sub

        Public Shared Function obterNotasPorIntervalo(ByVal inicio As Integer, ByVal fim As Integer, ByVal cnpj As String, ByVal serie As String) As List(Of notaVO)
            Dim ht As New Hashtable
            ht.Add("inicio", inicio)
            ht.Add("fim", fim)
            ht.Add("cnpj", cnpj)
            ht.Add("serie", serie)

            Return IBatisNETHelper.Instance.QueryForList(Of notaVO)("obterNotasPorIntervalo", ht)
        End Function

        Public Shared Function obterNotasParaPostBack() As List(Of notaVO)

            Return IBatisNETHelper.Instance.QueryForList(Of notaVO)("obterNotasParaPostBack", Nothing)
        End Function
    End Class
End Namespace