Imports System.Net.Mail

Public Class EmailService

    Public Shared Sub Enviar(ByVal emailDestino As String, ByVal emailRemetente As String, ByVal assunto As String, ByVal mensagem As String)
        Dim oMsg As MailMessage = New MailMessage
        oMsg.From = New MailAddress(emailRemetente)
        oMsg.To.Add(New MailAddress(emailDestino))
        oMsg.Subject = assunto
        oMsg.Body = mensagem

        Dim cliente As New SmtpClient()
        cliente.Send(oMsg)
    End Sub

End Class
