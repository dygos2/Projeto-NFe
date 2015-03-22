

Public Class notasProcDAO

    Public Shared Function obterEmpresas() As List(Of notasProcVO)
        Try
            Dim nota As New List(Of notasProcVO)
            Return IBatisNETHelper.Instance.QueryForList(Of notasProcVO)("obterEmpresasProc", nota)
        Catch ex As Exception
            Throw ex
        End Try
    End Function



End Class
Public Class notaDAO

    Public Shared Sub inserirHistorico(ByVal historico As FN4Common.historicoVO)
        IBatisNETHelper.Instance.Insert("inserirHistorico", historico)
    End Sub

End Class