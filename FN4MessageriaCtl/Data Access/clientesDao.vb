Imports FN4Common

Public Class clientesDao

    Public Shared Function obterClientesFaltadePagto() As List(Of clientesVO)
        Return IBatisNETHelper.Instance.QueryForList(Of clientesVO)("obterfaltadepagamento", Nothing)
    End Function
    Public Shared Function obterExpiracaoCertificados() As List(Of clientesVO)
        Return IBatisNETHelper.Instance.QueryForList(Of clientesVO)("obtercertsaexpirar", Nothing)
    End Function
    Public Shared Function obterEmpresasTrial() As List(Of clientesVO)
        Return IBatisNETHelper.Instance.QueryForList(Of clientesVO)("obtertrial", Nothing)
    End Function
    Public Shared Function alterarStatusCliente(ByVal id_fk_produtos_status_upd As Integer, ByVal id_fk_empresa As Integer, ByVal id_fk_produtos As Integer, ByVal id_fk_produtos_status As Integer)
        Try
            Dim ht As New Hashtable
            ht.Add("id_fk_empresa", id_fk_empresa)
            ht.Add("id_fk_produtos", id_fk_produtos)
            ht.Add("id_fk_produtos_status", id_fk_produtos_status)
            ht.Add("id_fk_produtos_status_upd", id_fk_produtos_status_upd)
            Return IBatisNETHelper.Instance.Update("alterarstatusCliente", ht)
        Catch ex As Exception
            Throw ex
        End Try

    End Function
End Class
