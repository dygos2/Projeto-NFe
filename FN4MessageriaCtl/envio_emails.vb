Imports FN4Common
Imports System.IO
Imports FN4Common.DataAccess
Imports System.Threading

Public Class envio_emails

    Private WithEvents tm As New System.Timers.Timer
    Public Sub New()
        tm.Interval = Geral.Parametro("intervaloDeVarreduraDeEmails")
        tm.AutoReset = True
    End Sub

    Public Sub run()
        Log.registrarInfo("Monitor de messageria iniciado", "MessageriaService")
        'rodando primeiro e depois iniciando o timer para 1 dia
        enviarEmails(1)
        enviarEmails(2)
        tm.Start()
    End Sub
    Public Sub pause()
        Log.registrarInfo("Monitor de messageria parado", "MessageriaService")
        tm.Stop()
    End Sub

    Private Sub executarEnvioDeEmail() Handles tm.Elapsed
        enviarEmails(1)
        enviarEmails(2)
        enviarEmails(3) 'trial
    End Sub

    Private Sub enviarEmails(ByVal destino As Integer)
        'destino = 1/obterfaltadepagamento | 2/obterexpiraçoes de certificados
        Dim ex As Exception
        Try
            Dim clientes As List(Of clientesVO)
            Dim cliente As clientesVO

            Select Case destino
                Case 1
                    Log.registrarInfo("Rodando envio de falta de pagamentos", "MessageriaService")
                    clientes = clientesDao.obterClientesFaltadePagto
                Case 2
                    Log.registrarInfo("Rodando expirações de certificados", "MessageriaService")
                    clientes = clientesDao.obterExpiracaoCertificados
                Case 3 'trial
                    Log.registrarInfo("Rodando Trial", "MessageriaService")
                    'clientes = clientesDao.obterExpiracaoCertificados
                    clientes = clientesDao.obterEmpresasTrial
            End Select

            For Each cliente In clientes
                Try
                    'aguardando 2 segundos para mandar o próximo email
                    Dim stopwatch As Stopwatch = stopwatch.StartNew
                    Thread.Sleep(2000)
                    stopwatch.Stop()

                    Select Case destino
                        Case 1 'pagtos vencidos / não enviar emails
                            'If cliente.id_fk_produtos_status.Equals("2") Then 'o cliente já está ativo
                            '    If cliente.dias_vencidos < 30 Then
                            '        Log.registrarInfo("(Pagto vencido em '" & cliente.data_vencimento & "' enviando emails: '" & cliente.email & "' ('" & cliente.nome & "') - " & cliente.nomeEmpresa, "MessageriaService")
                            '        enviarEmail(cliente, 36) 'o id do template do pagto pendente para menos de 30 dias
                            '    Else
                            '        Log.registrarInfo("((Cancelado) Pagto vencido em '" & cliente.data_vencimento & "' enviando emails: '" & cliente.email & "' ('" & cliente.nome & "') - " & cliente.nomeEmpresa, "MessageriaService")
                            '        enviarEmail(cliente, 37) 'o id do template do pagto pendente para mais de 30 dias (cancelamento)
                            '        clientesDao.alterarStatusCliente(3, cliente.idEmpresa, 2, 5)
                            '        'TODO: Cancelar cliente no Godaddy
                            '    End If
                            'Else 'é cliente novo e está com o status de pagto em aberto
                            '    Select Case cliente.dias_vencidos
                            '        Case "4"
                            '            Log.registrarInfo("((Cliente NOVO) Pagto vencido em '" & cliente.data_vencimento & "' enviando emails: '" & cliente.email & "' ('" & cliente.nome & "') - " & cliente.nomeEmpresa, "MessageriaService")
                            '            enviarEmail(cliente, 41) 'o id do template do pagto do setup pendente em 4 dias
                            '        Case "7"
                            '            Log.registrarInfo("((Cliente NOVO) Pagto vencido em '" & cliente.data_vencimento & "' enviando emails: '" & cliente.email & "' ('" & cliente.nome & "') - " & cliente.nomeEmpresa, "MessageriaService")
                            '            enviarEmail(cliente, 41) 'o id do template de pagto do setup pendente em 7 dias
                            '        Case "10"
                            '            Log.registrarInfo("((Cliente NOVO) Pagto vencido em '" & cliente.data_vencimento & "' enviando emails: '" & cliente.email & "' ('" & cliente.nome & "') - " & cliente.nomeEmpresa, "MessageriaService")
                            '            enviarEmail(cliente, 41) 'o id do template do pagto do setup pendente em 10 dias
                            '        Case "20"
                            '            Log.registrarInfo("((Cliente NOVO)(Cancelamento) Pagto vencido em '" & cliente.data_vencimento & "' enviando emails: '" & cliente.email & "' ('" & cliente.nome & "') - " & cliente.nomeEmpresa, "MessageriaService")
                            '            clientesDao.alterarStatusCliente(3, cliente.idEmpresa, 2, 5)
                            '            enviarEmail(cliente, 42) 'o id do template do pagto do setup pendente em 20 dias
                            '            'TODO: Cancelar cliente no Godaddy
                            '    End Select
                            'End If
                        Case 2
                            If cliente.dias_exp > 0 And cliente.dias_exp <= 10 Then
                                Log.registrarInfo("(Expiração do certificado ('" & cliente.dias_exp & "' dias) enviando emails: '" & cliente.email & "' ('" & cliente.nome & "') - " & cliente.nomeEmpresa, "MessageriaService")
                                enviarEmail(cliente, 38) 'o id do template de expiração de certificado
                                'ElseIf cliente.dias_exp > -1 And cliente.dias_exp <= 7 Then
                                '   Log.registrarInfo("([Urgente] Expiração do certificado ('" & cliente.dias_exp & "' dias) enviando emails: '" & cliente.email & "' ('" & cliente.nome & "') - " & cliente.nomeEmpresa, "MessageriaService")
                                '  enviarEmail(cliente, 39) 'o id do template de expiração de certificado URGENTE
                            ElseIf cliente.dias_exp > -5 And cliente.dias_exp < 0 Then
                                Log.registrarInfo("([Urgente] Certificado expirado ('" & cliente.dias_exp & "' dias) enviando emails: '" & cliente.email & "' ('" & cliente.nome & "') - " & cliente.nomeEmpresa, "MessageriaService")
                                cliente.dias_exp = cliente.dias_exp * -1
                                enviarEmail(cliente, 40) 'o id do template de expiração de certificado URGENTE
                            End If
                        Case 3 'trial
                            'Se empresa não tiver token / falta o primeiro passo de cadastro
                            '-----Select token from tb_empresa
                            'Se cliente ainda não instalou o certificado digital
                            '-----Select cert_fim from tb_empresa
                            'Se não tiver cadastros fiscais cadastrados, ou informacao das integracoes da plataforma nao estiverem cadastradas
                            '----- select ipi_padrao from tb_empresa
                            '----- select count(*) from emissor_fiscal
                            '----- select count(*) from rel_integracoes

                    End Select

                Catch e As Exception
                    ex = e
                    Log.registrarErro("Erro inesperado: " & Geral.ObterExceptionMessagesEmCascata(ex) & vbCrLf & ex.StackTrace, "MessageriaService")
                End Try
            Next
        Catch exception As Exception
            Log.registrarErro("Erro inesperado: " & Geral.ObterExceptionMessagesEmCascata(exception), "MessageriaService")
        Finally

        End Try
    End Sub

    Private Sub enviarEmail(ByVal cliente As clientesVO, ByVal idtemplate As Integer)

        Dim templates As List(Of templatesVO) = templatesDao.obterTemplates(idtemplate)
        Dim assunto As String
        Dim corpo As String
        Dim destinatarios As New List(Of String)

        assunto = templates(0).assunto
        corpo = templates(0).template

        corpo = corpo.Replace("@url_pagamento", "http://www.nfe4web.com.br/manager/boleto_logar.asp?email=@email&id=@idlogin")
        corpo = corpo.Replace("@cert_fim", cliente.cert_fim)
        corpo = corpo.Replace("@dias_exp", cliente.dias_exp)
        corpo = corpo.Replace("@data_vencimento", cliente.data_vencimento)
        corpo = corpo.Replace("@email", cliente.email)
        corpo = corpo.Replace("@idEmpresa", cliente.idEmpresa)
        corpo = corpo.Replace("@idlogin", cliente.idlogin)
        corpo = corpo.Replace("@nomeCliente", cliente.nome)
        corpo = corpo.Replace("@nomeEmpresa", cliente.nomeEmpresa)
        corpo = corpo.Replace("@cnpj", cliente.cnpj)

        'incluindo os emails
        destinatarios.Add("comercial@megaideas.net")
        'destinatarios.Add(cliente.email)

        Email.Enviar(destinatarios, assunto, corpo)

    End Sub

#Region "Acessorios"

    Private Sub inserirHistorico(ByVal tipo As String, ByVal descricao As String, ByVal nota As notaVO, ByVal cnpj As String)
        Try

            'Dim hist As New historicoVO(tipo, descricao, nota.NFe_ide_nNF, cnpj, nota.serie)
            'clientesDao.inserirHistorico(hist)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

End Class
