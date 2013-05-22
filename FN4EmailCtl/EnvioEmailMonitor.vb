Imports FN4Common
Imports System.IO
Imports FN4Common.DataAccess

Public Class EnvioEmailMonitor

    Private WithEvents tm As New System.Timers.Timer
    Public Sub New()
        tm.Interval = Geral.Parametro("intervaloDeVarreduraDeSaida")
    End Sub

    Public Sub run()
        Log.registrarInfo("Monitor de envio de emails iniciado", "EmailService")
        tm.Start()
    End Sub
    Public Sub pause()
        tm.Stop()
    End Sub


    Private Sub executarEnvioDeEmail() Handles tm.Elapsed
        enviarEmails()
    End Sub

    Private Sub enviarEmails()
        Dim ex As Exception

        Try
            tm.Stop()

            Dim notas As List(Of notaVO) = notaDAO.obterNotasParaEnvioDeEmail
            Dim nota As notaVO
            For Each nota In notas
                Try

                    Log.registrarInfo("Enviando emails: '" & nota.emailDestinatario & "' da nota " & nota.NFe_ide_nNF & " Contingencia - " & nota.impressoEmContingencia, "EmailService")
                    Dim empresa As empresaVO = empresaDAO.obterEmpresa(nota.NFe_emit_CNPJ, String.Empty)

                    If Not nota.emailDestinatario.Trim(" ") = "" Or Not empresa.email.Trim(" ") = "" Then
                        enviarEmail(nota)
                    Else
                        Log.registrarInfo("Nota " & nota.NFe_ide_nNF & " - Email do cliente e administrativo não informado", "EmailService")
                    End If

                Catch e As Exception
                    ex = e
                    Log.registrarErro("Erro inesperado: " & Geral.ObterExceptionMessagesEmCascata(ex) & vbCrLf & ex.StackTrace, "EmailService")
                End Try

                If nota.statusDaNota = 21 Then
                    nota.statusDaNota = 22
                Else
                    nota.impressaoSolicitada = 0
                End If
                notaDAO.alterarNota(nota)
            Next
        Catch exception As Exception
            Log.registrarErro("Erro inesperado: " & Geral.ObterExceptionMessagesEmCascata(exception), "EmailService")
        Finally
            tm.Start()
        End Try
    End Sub

    Private Sub enviarEmail(ByVal nota As notaVO)

        Dim corpo As String
        Dim uri As New Uri(System.Reflection.Assembly.GetExecutingAssembly.GetName.CodeBase)
        Dim caminho As String = Path.GetDirectoryName(uri.LocalPath)
        Dim servico As String = "EmailService"

        Dim empresa As empresaVO = empresaDAO.obterEmpresa(nota.NFe_emit_CNPJ, String.Empty)
        Dim tp_sys = configuracaoDAO.obterConfiguracao("tp_sys", empresa.idEmpresa)
        Dim str As StreamReader
        Dim link_danfe, pathDanfe As String
        Dim pasta_trab_arr As Array
        link_danfe = ""

        'Se tiver tp_sys na tabela de configurações do cliente, enviar body 1 (Cliente contratou envio do PDF)
        If Not tp_sys Is Nothing Then
            str = New StreamReader(caminho & "\body.html")
            Log.registrarInfo("Carregando arquivo de corpo da mensagem - " & caminho & "\body.html", "EmailService")
        Else
            str = New StreamReader(caminho & "\body2.html")
            Log.registrarInfo("Carregando arquivo de corpo da mensagem - " & caminho & "\body2.html", "EmailService")

            'montar a chamada do servidor Danfe para colocar no Body 2
            pasta_trab_arr = nota.pastaDeTrabalho.Split("\")
            If nota.statusDaNota = 21 Then
                link_danfe = Geral.Parametro("servidorDanfe") & "cnpj=" & empresa.cnpj & "&ano=" & pasta_trab_arr(5) & "&mes=" & pasta_trab_arr(6) & "&dia=" & pasta_trab_arr(7) & "&nnfe=" & pasta_trab_arr(8) & "&arq=" & nota.NFe_ide_nNF & "_procNFe&ch=" & nota.NFe_infNFe_id & "&srvid=" & Geral.Parametro("srvid") & "&tp_amb=" & empresa.homologacao + 1 & "&dest_saida=D"
            Else
                link_danfe = Geral.Parametro("servidorDanfe") & "cnpj=" & empresa.cnpj & "&ano=" & pasta_trab_arr(5) & "&mes=" & pasta_trab_arr(6) & "&dia=" & pasta_trab_arr(7) & "&nnfe=" & pasta_trab_arr(8) & "&arq=" & nota.NFe_ide_nNF & "_assinado&ch=" & nota.NFe_infNFe_id & "&srvid=" & Geral.Parametro("srvid") & "&tp_amb=" & empresa.homologacao + 1 & "&dest_saida=D"
            End If
        End If

        corpo = str.ReadToEnd

        corpo = corpo.Replace("@numeroDaNota", nota.NFe_ide_nNF)
        corpo = corpo.Replace("@emitente", nota.NFe_emit_xNome)
        corpo = corpo.Replace("@destinatario", nota.NFe_dest_xNome)
        corpo = corpo.Replace("@chaveDeAcesso", nota.NFe_infNFe_id)

        If tp_sys Is Nothing Then
            corpo = corpo.Replace("@link_danfe", link_danfe)
        End If

        str.Close()
        str.Dispose()

        Log.registrarInfo("Corpo do e-mail montado.", servico)

        Dim remetente As String
        Dim senha As String
        Dim displayremetente As String
        Dim login_email As String
        Dim destinatarios As New List(Of String)
        Dim destinatariosbcc As New List(Of String)
        Dim anexos As New List(Of String)
        Dim email_sep As String

        If Not tp_sys Is Nothing Then
            pathDanfe = Path.GetDirectoryName(Geral.Parametro("pathDanfe"))
            pathDanfe = Path.Combine(pathDanfe, "PDF")
            pathDanfe = Path.Combine(pathDanfe, nota.NFe_emit_CNPJ)
            pathDanfe = Path.Combine(pathDanfe, nota.NFe_ide_dEmi.ToString("yyyy") & nota.NFe_ide_dEmi.ToString("MM"))
            pathDanfe = Path.Combine(pathDanfe, nota.NFe_infNFe_id & ".pdf")
        End If

        If empresa.receberEmailNota And Not String.IsNullOrEmpty(empresa.email) Then
            For Each email_sep In empresa.email.Split(";")
                email_sep = email_sep.Trim
                If Geral.ValidarFormatoDeEmail(email_sep) Then
                    destinatariosbcc.Add(email_sep)
                End If
            Next
        End If

        If Not String.IsNullOrEmpty(nota.emailDestinatario) Then
            For Each email_sep In nota.emailDestinatario.Split(";")
                email_sep = email_sep.Trim
                If Geral.ValidarFormatoDeEmail(email_sep) Then
                    destinatarios.Add(email_sep)
                End If
            Next
        End If

        Dim pathXml As String = nota.pastaDeTrabalho & nota.NFe_ide_nNF & "_procNFe.xml"

        If File.Exists(pathXml) Then
            anexos.Add(pathXml)
        End If

        If Not tp_sys Is Nothing Then
            If File.Exists(pathDanfe) Then
                anexos.Add(pathDanfe)
            End If
        End If

        'colocando o nome do remetente
        If empresa.nomeFantasia <> "" Then
            displayremetente = empresa.nomeFantasia
        Else
            displayremetente = Geral.Parametro("displayemailPadrao")
        End If

        'se for nfecommerce("1")
        'se for fisconet("2")
        Dim eh_nfecommerce As String = Geral.Parametro("tp_sys")

        'se for nfecommerce, o cliente vai enviar por autenticação NFe4web, que está no geral
        If eh_nfecommerce = "1" Then
            If empresa.email <> "" Then
                Dim remetente_arr As Array = Split(empresa.email, ";")
                remetente = remetente_arr(0)
            Else
                remetente = Geral.Parametro("emailPadrao")
            End If
            senha = Geral.Parametro("senhaEmailPadrao")
            login_email = Geral.Parametro("loginEmailPadrao")
        Else
            Dim emailRemetente = configuracaoDAO.obterConfiguracao("emailRemetente", empresa.idEmpresa)
            Dim senhaEmail = configuracaoDAO.obterConfiguracao("senhaEmail", empresa.idEmpresa)
            login_email = ""

            If Not emailRemetente Is Nothing And Not senhaEmail Is Nothing Then
                If emailRemetente.valor.Equals("default") Or senhaEmail.valor.Equals("default") Then
                    remetente = Geral.Parametro("emailPadrao")
                    senha = Geral.Parametro("senhaEmailPadrao")
                Else
                    remetente = emailRemetente.valor
                    senha = senhaEmail.valor
                End If
            Else
                remetente = Geral.Parametro("emailPadrao")
                senha = Geral.Parametro("senhaEmailPadrao")
            End If
        End If

        Email.Enviar(destinatarios, _
            destinatariosbcc, remetente, senha, _
            nota.NFe_dest_xNome & " - NFe n°" & nota.NFe_ide_nNF & " emitida em " & nota.dEmiString, _
            corpo, anexos, empresa.idEmpresa, displayremetente, login_email)

        Dim strDestinatarios As String = ""

        For Each s As String In destinatarios
            If strDestinatarios.Equals(String.Empty) Then
                strDestinatarios = s
            Else
                strDestinatarios = strDestinatarios & ", " & s
            End If
        Next

        inserirHistorico(18, strDestinatarios, nota, empresa.cnpj)
        Log.registrarInfo("Nota " & nota.NFe_ide_nNF & " de série " & nota.serie & " - Email enviado para: " & strDestinatarios, "EmailService")

    End Sub

#Region "Acessorios"

    Private Sub inserirHistorico(ByVal tipo As String, ByVal descricao As String, ByVal nota As notaVO, ByVal cnpj As String)
        Try

            Dim hist As New historicoVO(tipo, descricao, nota.NFe_ide_nNF, cnpj, nota.serie)
            notaDAO.inserirHistorico(hist)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

End Class
