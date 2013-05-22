Public Partial Class index1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Write("Bem vindo, " & Session("login") & vbCrLf & "id: " & Session("id") & vbCrLf & Session("tipoUsuario"))
    End Sub

End Class