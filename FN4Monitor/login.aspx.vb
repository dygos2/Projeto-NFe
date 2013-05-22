Imports FN4Common

Partial Public Class index
    Inherits System.Web.UI.Page

    Private Sub efetuarLogin(ByVal userName As String, ByVal password As String)
        Dim usuario As usuarioVO
        usuario = loginDAO.obterUsuario(userName, password)

        If Not usuario Is Nothing Then
            Session.Add("idUsuario", usuario.idUsuario)
            Session.Add("login", usuario.email)
            Session.Add("tipoUsuario", usuario.tipo)
            Response.Redirect("index.aspx")
        Else
            lblValidacao.Text = "Login/Senha inválidos. Digite novamente"
            loginName.Value = ""
            userPassword.Value = ""
            loginName.Focus()

        End If
    End Sub
    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        efetuarLogin(loginName.Value, userPassword.Value)
    End Sub

End Class