Imports System.Net.Mail
Imports FN4Common

Public Class Email

    Public Shared Sub Enviar(ByVal emailDestino As List(Of String), ByVal assunto As String, ByVal mensagem As String)

        Dim oMsg As MailMessage = New MailMessage
        Dim remetente As String
        Dim senha As String
        Dim email_bcc As String

        remetente = Geral.Parametro("emailPadrao")
        senha = Geral.Parametro("senhaEmailPadrao")
        email_bcc = Geral.Parametro("emailbcc")

        Dim auth As System.Net.NetworkCredential
        oMsg.From = New MailAddress(remetente)

        For Each mail In emailDestino
            oMsg.To.Add(New MailAddress(mail))
        Next

        If email_bcc <> "" Then
            oMsg.Bcc.Add(New MailAddress(email_bcc))
        End If

        oMsg.SubjectEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1")
        oMsg.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1")

        oMsg.Subject = assunto
        oMsg.Bcc.Add(remetente)

        oMsg.IsBodyHtml = True

        oMsg.Body = mensagem

        Dim smtpClient As SmtpClient

        smtpClient = New SmtpClient(Geral.Parametro("servidorSMTPPadrao"))
        smtpClient.Port = CInt(IIf(Geral.Parametro("portaSMTPPadrao").ToLower().Equals("default"), 25, Geral.Parametro("portaSMTPPadrao")))

        If Geral.Parametro("ServidorSMTPRequerAutenticacaoPadrao").Equals("1") Then
            auth = New System.Net.NetworkCredential(remetente, senha)
            smtpClient.UseDefaultCredentials = False
            smtpClient.Credentials = auth
        End If

        smtpClient.EnableSsl = CBool(Geral.Parametro("enableSSLPadrao"))
        smtpClient.Send(oMsg)
    End Sub

    'Public Shared Sub Enviar(ByVal emailDestino As String, ByVal emailRemetente As String, ByVal senha As String, ByVal assunto As String, ByVal mensagem As String)
    '    Try
    '        Dim oMsg As MailMessage = New MailMessage
    '        oMsg.From = New MailAddress(emailRemetente)
    '        oMsg.To.Add(New MailAddress(emailDestino))

    '        oMsg.SubjectEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1")
    '        oMsg.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1")

    '        oMsg.Subject = assunto
    '        oMsg.IsBodyHtml = True
    '        oMsg.Body = mensagem

    '        Dim auth As New System.Net.NetworkCredential(emailRemetente, senha)

    '        Dim cliente As New SmtpClient("smtp.megaideas.net")
    '        cliente.EnableSsl = True

    '        cliente.UseDefaultCredentials = False
    '        cliente.Credentials = auth

    '        cliente.Send(oMsg)
    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Sub
    'Public Shared Sub EnviarCDO(ByVal emailDestino As String, ByVal emailRemetente As String, ByVal assunto As String, ByVal mensagem As String)

    '    'abaixo estão as configurações do CDOSYS o que deve ser alterado está com comentario o restante não precisa modificar
    '    Dim sch = "http://schemas.microsoft.com/cdo/configuration/"
    '    Dim cdoConfig = CreateObject("CDO.Configuration")
    '    cdoConfig.Fields.Item(sch & "sendusing") = 2
    '    cdoConfig.Fields.Item(sch & "smtpauthenticate") = 1
    '    cdoConfig.Fields.Item(sch & "smtpserver") = "smtp.megaideas.net" 'digite seu servidor SMTP
    '    cdoConfig.Fields.Item(sch & "smtpserverport") = 587 'Digite a porta segura para envio 25 é padrão
    '    cdoConfig.Fields.Item(sch & "smtpconnectiontimeout") = 30
    '    'os e-mails são enviados apenas com autenticação por isso você terá que informar um email e senha válido
    '    cdoConfig.Fields.Item(sch & "sendusername") = "suporte@megaideas.net" 'digite um email válido para autenticar
    '    cdoConfig.Fields.Item(sch & "sendpassword") = "123mudar" 'digite sua senha
    '    cdoConfig.fields.update()
    '    Dim cdoMessage = CreateObject("CDO.Message")
    '    cdoMessage.Configuration = cdoConfig
    '    cdoMessage.From = emailRemetente 'ENDEREÇO DE E-MAIL QUE SERÁ EXIBIDO NO FROM DA MENSAGEM
    '    cdoMessage.To = emailDestino 'digite o email para qual a mensagem será entregue.
    '    cdoMessage.Subject = assunto ' Digite o assunto da mensagem
    '    Dim strBody = mensagem
    '    cdoMessage.HTMLBody = strBody
    '    Try
    '        cdoMessage.Send()
    '        cdoMessage = Nothing
    '        cdoConfig = Nothing
    '        'se não houver erros a mensagem é enviada e a mensagem abaixo é exibida

    '    Catch ex As Exception
    '        'se houver algum erro, captura a mensagem de erro do servidor e exibe na tela
    '        Dim erro_mail As String = "Erro ao enviar email : " & ex.Message
    '        cdoMessage = Nothing
    '        cdoConfig = Nothing
    '    End Try

    'End Sub

End Class
