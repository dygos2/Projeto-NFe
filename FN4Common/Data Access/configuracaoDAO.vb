Public Class configuracaoDAO
    Public Shared Function obterConfiguracao(ByVal chave As String, ByVal idEmpresa As Integer) As configuracaoVO
        Try
            Dim ht As New Hashtable
            ht.Add("chave", chave)
            ht.Add("idEmpresa", idEmpresa)

            Return IBatisNETHelper.Instance.QueryForObject("obterConfiguracao", ht)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function obterFTPConfig(ByVal idEmpresa As Integer) As IList(Of configuracaoVO)
        Try
            Dim ht As New Hashtable
            ht.Add("idEmpresa", idEmpresa)
            Return IBatisNETHelper.Instance.QueryForList(Of configuracaoVO)("obterFTPConfig", idEmpresa)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
