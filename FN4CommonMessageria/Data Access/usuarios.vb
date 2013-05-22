Namespace DataAccess
    Public Class usuarios
        Public Shared Function obterIdDeUsuario(ByVal usuario As usuarioVO) As Integer
            Return IBatisNETHelper.Instance.QueryForObject("obterIdDeUsuario", usuario)
        End Function
    End Class
End Namespace

