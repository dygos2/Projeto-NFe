Imports FN4Common.DataAccess
Imports FN4Common
Imports System.Xml
Imports System.IO
Imports FN4Common.Helpers

Public Class EntradaTxtMonitor
    Private WithEvents tm As New System.Timers.Timer
    Private historico As historicoVO
    Private numeroDaNota As Integer
    Private txtLimpo As String
    Private pastaDeTrabalho As String
    Private xmlDeTrabalho As XmlDocument
    Private pathXmlDeTrabalho As String
    Private pathXMLEnvio As String
    Private pathXMLAssinado As String
    Private dtProcessamento As String
    Private nota As notaVO
    Private empresa As empresaVO

    Public Sub New()
        Log.registrarInfo("Serviço Iniciado", "EntradaTxtService")
        tm.Interval = Geral.Parametro("intervaloDeVarreduraDeSaida")
    End Sub
    Public Sub run() Handles tm.Elapsed
        Try
            'parar o timer
            tm.Stop()

            Dim arquivo As String
            Dim retornoValidacao As String

            'para cada TXT
            For Each arquivo In Directory.GetFiles(FN4Common.Geral.Parametro("pastaEntrada"), "*.txt")
                'SEMAFORO
                Try
                    File.Move(arquivo, arquivo.Replace(".txt", ".TXT"))
                    arquivo = arquivo.Replace(".txt", ".TXT")
                Catch ex As Exception
                    Log.registrarErro("Erro: Arquivo em uso", "EntradaTxtService")
                    Continue For
                End Try
                Log.registrarInfo("Entrada do arquivo " & arquivo & " no sistema, processamento", "EntradaTxtService")

                Dim serie As Integer = CInt(arquivo.Substring(arquivo.LastIndexOf("\") + 1).Replace(".TXT", "").Split("-")(2))
                numeroDaNota = CInt(arquivo.Substring(arquivo.LastIndexOf("\") + 1).Replace(".TXT", "").Split("-")(1)) 'captura apenas o trecho do nome do arquivo após o traço
                Dim CNPJ As String = arquivo.Substring(arquivo.LastIndexOf("\") + 1).Replace(".TXT", "").Split("-")(0)

                If CNPJ.Trim(" ").Length < 14 Then
                    rejeitarArquivo(arquivo, Nothing, "Erro: CNPJ do arquivo de nota inválido")
                    Continue For
                End If

                ' Se o nome do arquivo contém a literal "canc"
                If arquivo.ToLower().Contains("canc") Then
                    processarCancelamentoDeNota(arquivo, CNPJ, serie)
                    Exit Sub
                End If
                Try
                    processarEntradaDeNota(arquivo, CNPJ, serie)
                Catch ex As Exception
                    Log.registrarErro("Erro inesperado " & vbCrLf & Geral.ObterExceptionMessagesEmCascata(ex) & vbCrLf & Geral.ObterStackTraceEmCascata(ex), "EntradaTxtService")
                    Continue For
                End Try

                '===================================================
                'testando o cliente para saber se pode processar a nota
                '===================================================
                Dim tipoSistema As Integer = Integer.Parse(FN4Common.Geral.Parametro("tp_sys"))

                ' Se o tipo do sistema for NFECommerce (1), verifica se tem créditos, se não, não deixa processar
                If tipoSistema = 1 Then
                    ' Se a empresa não tiver mais créditos de franquia nem de pacote
                    If empresa.frest = 0 And empresa.prest = 0 Then
                        nota.retEnviNFe_xMotivo = "Sem creditos para processamento da nota. Verifique seus creditos e franquia mensal."
                        inserirHistorico(1, "Nota rejeitada, cliente sem créditos para processamentos. Acesse o site da NFe4web e compre mais créditos, ou aguarde a liberação após o ciclo mensal.", nota, CNPJ)

                        rejeitarNota(arquivo, nota, Nothing, "Nota não pode ser processada pois cliente não possui créditos")
                        Throw New Exception("")
                    End If
                End If

                ' Se a empresa não estiver ativa, não pode processar nota
                If empresa.habilitado_stat = "0" Then
                    nota.retEnviNFe_xMotivo = "Empresa bloqueada para processamentos."
                    inserirHistorico(1, "Nota rejeitada, cliente bloqueado para processamentos. Entrar em contato com a área comercial da NFe4Web.", nota, CNPJ)

                    rejeitarNota(arquivo, nota, Nothing, "Nota não pode ser processada pois cliente bloqueado")
                    Throw New Exception("")
                End If
                '===================================================
                'fim do teste do cliente
                '===================================================

                Try
                    pathXMLAssinado = pathXMLEnvio.Replace("_transformado.xml", "_assinado.xml")
                    'Assinar o XML
                    Dim NFe As New XmlDocument
                    NFe.Load(pathXMLEnvio)
                    NFe.PreserveWhitespace = True
                    NFe = XmlHelper.assinarNFeXML(NFe, NFe.GetElementsByTagName("infNFe")(0).Attributes.ItemOf("Id").InnerText, empresa.idEmpresa)
                    'Gravar o XML assinado
                    NFe.Save(pathXMLAssinado)

                    inserirHistorico(6, "", nota, CNPJ)

                    nota.statusDaNota = 0
                    notaDAO.alterarNota(nota)
                Catch ex As Exception
                    Log.registrarErro("Erro inesperado " & vbCrLf & Geral.ObterExceptionMessagesEmCascata(ex) & vbCrLf & Geral.ObterStackTraceEmCascata(ex), "EntradaTxtService")
                    inserirHistorico(7, "", nota, CNPJ)

                    nota.retEnviNFe_xMotivo = "Erro no certificado digital. Se já instalou o certificado e ainda apresenta este erro, acionar o helpdesk passando o CNPJ da sua empresa."
                    notaDAO.alterarNota(nota)

                    rejeitarNota(arquivo, nota, ex, "Erro na assinatura do XML: ")
                    Continue For
                End Try

                Try
                    retornoValidacao = TxtXmlHelper.validarXmlDeEnvio(pathXMLAssinado)
                    If retornoValidacao <> "" Then
                        inserirHistorico(9, retornoValidacao, nota, CNPJ)

                        nota.retEnviNFe_xMotivo = "Erro de validação: " & retornoValidacao
                        notaDAO.alterarNota(nota)

                        rejeitarNota(arquivo, nota, New Exception("Nota não está de acordo com o XSD"), "Erro de validação: " & retornoValidacao)
                        Continue For
                    End If
                Catch ex As Exception
                    rejeitarNota(arquivo, nota, ex, "Erro inesperado na validacao: ")
                    Continue For
                End Try

                Try
                    Dim dtProcessamento As String = Format(DateTime.Now, "yyyyMMddhhmmss")
                    'salvar o arquivo original na pasta de aprovadas
                    'comentado em 20/02/2013, não precisa mais gravar em aceitas, pois o sistema já está gravando na pasta destino
                    'File.Copy(arquivo, FN4Common.Geral.Parametro("pastaDeAprovadas") & numeroDaNota & "_" & dtProcessamento & ".txt")
                    File.Delete(arquivo)
                Catch ex As Exception
                    Log.registrarErro("Erro inesperado: " & Geral.ObterExceptionMessagesEmCascata(ex) & vbCrLf & Geral.ObterStackTraceEmCascata(ex), "EntradaTxtService")
                End Try
            Next
        Catch ex As Exception
            'Em caso de erros não tratados
            Log.registrarErro("Erro inesperado " & vbCrLf & Geral.ObterExceptionMessagesEmCascata(ex) & vbCrLf & Geral.ObterStackTraceEmCascata(ex), "EntradaTxtService")
        Finally
            'sempre ligar o timer novamente
            tm.Start()
        End Try
    End Sub

    Private Sub processarCancelamentoDeNota(ByVal arquivo As String, ByVal cnpj As String, ByVal serie As Integer)
        ' Obtem nota da base de dados
        nota = notaDAO.obterNota(numeroDaNota, cnpj, serie)
        Dim informacoesDoArquivo As New FileInfo(arquivo)

        If nota Is Nothing Then ' Se não encontrar a nota
            rejeitarArquivo(arquivo, Nothing, "Erro: Tentativa de cancelamento de nota inexistente")
            Exit Sub
        End If

        If nota.statusDaNota <> 22 Then ' Se encontrar, mas seu status for diferente de 22
            rejeitarArquivo(arquivo, Nothing, String.Format("Tentativa de cancelamento da nota {0} e série {1} negada, status de nota não autorizada.", nota.NFe_ide_nNF, nota.serie))
            Exit Sub
        End If

        ' Lê o arquivo de texto para obter justificativa
        Dim reader As New StreamReader(arquivo, System.Text.Encoding.GetEncoding("ISO-8859-1"))

        Dim txtJustificativa As String = reader.ReadToEnd

        reader.Close()

        ' Se a justificativa tiver mais que 255 caractéres, pega somente os primeiros 255 dela
        If txtJustificativa.Length > 255 Then
            txtJustificativa = txtJustificativa.Substring(0, 255)
        End If

        ' Obtém empresa do banco para poder enviar o seu token para o WS
        Dim empresa = empresaDAO.obterEmpresa(cnpj, Nothing)

        ' Instancia o WS
        Dim ws As New NotasWs.FN4NotasService

        ' Obtém URL do webservice de cancelamento do ambiente corrente
        Dim urlWs = Geral.Parametro("UrlWebServiceNotas")
        ws.Url = urlWs

        Log.registrarErro("Enviando dados para cancelamento no servidor " & urlWs, "EntradaTxtService")

        Try
            ' Chama o webservice de cancelamento e cancela a nota
            Dim resultws = ws.cancelarNota(numeroDaNota, serie, txtJustificativa, cnpj, empresa.token)
            If Not resultws.Contains("Nota Enviada") Then
                Log.registrarErro("Erro no envio do arquivo para cancelamento: " & resultws, "EntradaTxtService")
            End If
        Catch ex As Exception
            Log.registrarErro("Erro ao conectar ao servidor de cancelamento - " & ex.Message, "EntradaTxtService")
        End Try

        ' Faz uma cópia do arquivo trabalhado para dentro da pasta da NFe
        File.Copy(arquivo, Path.Combine(nota.pastaDeTrabalho, informacoesDoArquivo.Name), True)

        ' Deleta o arquivo da pasta in
        File.Delete(arquivo)

        informacoesDoArquivo = Nothing
    End Sub

    Public Sub pause()
        tm.Stop()
        Log.registrarInfo("Monitor de Recepção TXT parado", "EntradaTxtService")
    End Sub
    Private Sub processarEntradaDeNota(ByVal arquivo As String, ByVal cnpj As String, ByVal serie As Integer)
        '---criação de nova nota---
        nota = New notaVO(numeroDaNota)
        nota.NFe_emit_CNPJ = cnpj
        nota.serie = serie
        nota.retEnviNFe_cStat = 0 'inicia cstat com 0

        '--- obtencao da empresa ---
        empresa = empresaDAO.obterEmpresa(cnpj, String.Empty)

        'TODO Descomentar
        If Not Seguranca.CompararMD5(empresa.validador, empresa.token) Then
            ' Nota inválida
            nota.statusDaNota = 3
            notaDAO.alterarNota(nota)
            Throw New Exception("[Erro] empresa cadastrada, porém inválida.")
        End If

        'verificar duplicidade de notasXpedidos


        Try
            'inserir o registro da nota no banco
            pastaDeTrabalho = FN4Common.Geral.Parametro("pastaDeProcessadas") & _
             cnpj & "\" & DateTime.Now.Year & "\" & DateTime.Now.Month & "\" & DateTime.Now.Day & "\" & _
                 numeroDaNota & "-" & nota.serie & "\"
            nota.pastaDeTrabalho = pastaDeTrabalho
            notaDAO.inserirNota(nota)

            Log.registrarInfo("Nota inserida na base", "EntradaTxtService")
        Catch ex As Exception When ex.Message.Contains("Duplicate entry")
            'se der exception, a nota já existe
            nota = notaDAO.obterNota(numeroDaNota, cnpj, serie)
            pastaDeTrabalho = nota.pastaDeTrabalho
            Log.registrarInfo("Nota já existente e obtida na base", "EntradaTxtService")
        Catch exc As Exception
            Log.registrarErro("Erro inesperado: " & exc.Message & vbCrLf & exc.StackTrace, "EntradaTxtService")
            Exit Sub
        End Try

        Try
            'Log.registrarInfo(nota.statusDaNota, "EntradaTxtService")

            '----se a nota já existir e não estiver com erro----
            If nota.statusDaNota <> 3 And nota.statusDaNota <> 0 Then
                'insere o historico de rejeição e finaliza o fluxo
                ' Log.registrarInfo("entrou aqui", "EntradaTxtService")

                If nota.statusDaNota = 21 Or nota.statusDaNota = 22 Then
                    Log.registrarErro("Nota já autorizada", "EntradaTxtService")
                    inserirHistorico(2, "", nota, cnpj)
                    nota.tentativasDeInclusao += 1
                    notaDAO.alterarNota(nota)
                    File.Delete(arquivo)
                Else
                    inserirHistorico(2, "", nota, cnpj)
                    Log.registrarErro("Nota já existente e em processamento", "EntradaTxtService")
                    File.Delete(arquivo)
                End If

                Throw New Exception("Nota já existente e em processamento")
            Else
                Try
                    'incrementa as tentativas de inclusão
                    nota.tentativasDeInclusao += 1

                    'inserir registro no historico da nota
                    inserirHistorico(1, "Tentativa n. " & nota.tentativasDeInclusao, nota, cnpj)

                    Log.registrarInfo("Checando pasta de trabalho: " & pastaDeTrabalho, "EntradaTxtService")
                Catch ex As Exception
                    Throw ex
                End Try

                Try
                    '--criar a pasta de trabalho (ano\mes\dia\numNota\--
                    If Not Directory.Exists(pastaDeTrabalho) Then
                        Log.registrarInfo("Criando pasta de trabalho: " & pastaDeTrabalho, "EntradaTxtService")
                        Directory.CreateDirectory(pastaDeTrabalho)
                        Log.registrarInfo("Pasta de trabalho criada", "EntradaTxtService")
                    End If
                Catch ex As Exception
                    rejeitarNota(arquivo, nota, ex, "Erro na criação da pasta")
                    Throw ex
                End Try

                '---leitura do arquivo---
                Try
                    'abrir, ler
                    Dim reader As New StreamReader(arquivo, System.Text.Encoding.GetEncoding("ISO-8859-1"))

                    Dim txtDeEntrada As String = reader.ReadToEnd

                    reader.Close()

                    'checar a ultima linha
                    If txtDeEntrada.Split(vbCrLf)(txtDeEntrada.Split(vbCrLf).Count - 1).Split(empresa.delimitador)(0).Trim().Equals("99") Then
                        'se for campo de controle(campo 99)

                        'obter campos de controle

                        obterCamposDeControle(nota, txtDeEntrada.Split(vbCrLf)(txtDeEntrada.Split(vbCrLf).Count - 1), empresa.delimitador)

                        'comentado pois fazia acesso indevido ao banco. poderia sergravado depois
                        'notaDAO.alterarNota(nota)
                        'retirar a ultima linha
                        'txtDeEntrada = txtDeEntrada.Replace(txtDeEntrada.Split(vbCrLf)(txtDeEntrada.Split(vbCrLf).Count - 1), "")
                    End If


                    'gerar o TXT sem acentuacao e caracteres especiais
                    txtLimpo = TxtXmlHelper.processarTxt(txtDeEntrada)
                Catch ex As Exception
                    'se der erro, sinalizar no historico e encerrar o fluxo
                    inserirHistorico(4, "", nota, cnpj)

                    rejeitarNota(arquivo, nota, ex, "Erro ao processar o arquivo" & arquivo & ": ")

                    Throw ex
                End Try

                '---geracao do XML de trabalho---
                Try
                    Log.registrarInfo("Gerando o XML de trabalho para o arquivo" & arquivo & " - nota nr: " & nota.NFe_ide_nNF & " serie: " & nota.serie, "EntradaTxtService")

                    xmlDeTrabalho = TxtXmlHelper.gerarXML(txtLimpo, empresa, nota.NFe_ide_nNF)
                    Log.registrarInfo("Arquivo XML gerado", "EntradaTxtService")

                    'insere o historico de transformação do TXT em XML
                    inserirHistorico(3, "", nota, cnpj)

                Catch ex As Exception
                    'se der exceção na geracao
                    'insere um registro no banco informando o erro
                    inserirHistorico(4, "Erro ao processar o arquivo" & arquivo & ": ", nota, cnpj)

                    nota.retEnviNFe_xMotivo = "Erro na entrada do arquivo, favor solicitar suporte ao helpdesk passando o CNPJ, número e série da nota com erro."
                    nota.statusDaNota = 3
                    notaDAO.alterarNota(nota)
                    rejeitarNota(arquivo, nota, ex, "Erro ao processar o arquivo" & arquivo & ": ")
                    Throw ex
                End Try

                '---atualizar os dados da nota e salvar os arquivos na pasta de trabalho---
                Try

                    nota.pastaDeTrabalho = pastaDeTrabalho
                    carregarCampos(nota, xmlDeTrabalho)
                    Log.registrarDebug("Atualizando dados da nota")

                    'atualizar os dados da nota, seja ela nova ou não
                    nota.statusDaNota = -1 'status inicial = -1
                    notas.alterarNota(nota)
                    Log.registrarDebug("Dados da nota atualizados")

                    'salvar o txt transformado na pasta de trabalho
                    Dim escrita As StreamWriter

                    Dim fileInfo As New FileInfo(arquivo)

                    escrita = File.CreateText(pastaDeTrabalho & fileInfo.Name)

                    fileInfo = Nothing

                    escrita.Write(txtLimpo)
                    escrita.Close()

                    'Salvar na pasta de entrada de XMLs
                    pathXmlDeTrabalho = pastaDeTrabalho & numeroDaNota & ".xml"
                    xmlDeTrabalho.Save(pathXmlDeTrabalho)
                Catch ex As Exception

                    nota.NFe_dest_CNPJ = ""
                    nota.NFe_dest_CPF() = ""
                    nota.NFe_dest_UF() = ""
                    nota.NFe_dest_xNome() = ""
                    nota.emailDestinatario = ""
                    nota.NFe_emit_xNome() = ""
                    nota.NFe_ide_dEmi() = System.DateTime.Now
                    nota.NFe_infNFe_id() = ""
                    nota.NFe_total_ICMSTot_vNF() = 0
                    nota.retEnviNFe_xMotivo = "Erro na entrada do arquivo (" & ex.Message & ")"
                    notaDAO.alterarNota(nota)

                    Log.registrarErro("Erro na alteracao dos dados da nota ou na criação dos arquivos" & arquivo & vbCrLf & ex.Message & vbCrLf & ex.StackTrace, "EntradaTxtService")
                    rejeitarNota(arquivo, nota, ex, "Erro ao processar o arquivo" & arquivo & ": ")
                    Throw ex
                End Try

                '---geracao do XML de saída---
                Try
                    pathXMLEnvio = pastaDeTrabalho & numeroDaNota & "_transformado.xml"
                    TxtXmlHelper.gerarXmlDeSaida(pathXmlDeTrabalho, pastaDeTrabalho, numeroDaNota, System.AppDomain.CurrentDomain.BaseDirectory & "XSLT\" & FN4Common.Geral.Parametro("stylesheetNFe"))
                Catch ex As Exception
                    inserirHistorico(5, "", nota, cnpj)

                    rejeitarNota(arquivo, nota, ex, "Erro ao processar o arquivo" & arquivo & ": ")
                    Throw ex
                End Try
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub carregarCampos(ByRef nota As notaVO, ByVal XML As XmlDocument)
        nota.NFe_dest_CNPJ = XML.SelectSingleNode("/NFe/infNFe/dest/CNPJ").InnerText
        nota.NFe_dest_CPF() = XML.SelectSingleNode("/NFe/infNFe/dest/CPF").InnerText
        nota.NFe_dest_UF() = XML.SelectSingleNode("/NFe/infNFe/dest/enderDest/UF").InnerText
        nota.NFe_dest_xNome() = XML.SelectSingleNode("NFe/infNFe/dest/xNome").InnerText
        nota.emailDestinatario = XML.SelectSingleNode("NFe/infNFe/dest/email").InnerText
        nota.NFe_emit_xNome() = XML.SelectSingleNode("/NFe/infNFe/emit/xNome").InnerText
        nota.NFe_ide_dEmi() = CDate(XML.SelectSingleNode("/NFe/infNFe/ide/dEmi").InnerText)
        nota.NFe_infNFe_id() = XML.SelectSingleNode("/NFe/infNFe").Attributes("Id").Value.Replace("NFe", "")
        nota.NFe_total_ICMSTot_vNF() = CDbl(XML.SelectSingleNode("/NFe/infNFe/total/ICMSTot/vNF").InnerText)
        nota.cfop = XML.SelectSingleNode("NFe/infNFe/det[1]/prod/CFOP").InnerText
    End Sub

    Private Sub obterCamposDeControle(ByRef n As notaVO, ByVal linha As String, ByVal delimitador As String)
        Dim campos As String() = linha.Split(delimitador)
        nota.impressora = campos(1)
        nota.num_pedido = campos(2)
    End Sub

    Public Sub rejeitarNota(ByVal arquivo As String, ByVal nota As notaVO, ByVal ex As Exception, ByVal mensagem As String)
        Me.dtProcessamento = Format(DateTime.Now, "yyyyMMddhhmmss")
        If Not ex Is Nothing Then
            Log.registrarErro(mensagem & ex.Message & vbCrLf & ex.StackTrace, "EntradaTxtService")
        Else
            Log.registrarErro(mensagem, "EntradaTxtService")
        End If
        File.Copy(arquivo, FN4Common.Geral.Parametro("pastaDeRejeitadas") & numeroDaNota & "_" & dtProcessamento & ".txt", True)
        'marca a nota como status 3(rejeitada)
        nota.statusDaNota = 3
        notaDAO.alterarNota(nota)

        File.Delete(arquivo)
    End Sub

    Public Sub rejeitarArquivo(ByVal arquivo As String, ByVal ex As Exception, ByVal mensagem As String)
        Me.dtProcessamento = Format(DateTime.Now, "yyyyMMddhhmmss")
        If Not ex Is Nothing Then
            Log.registrarErro(mensagem & ex.Message & vbCrLf & ex.StackTrace, "EntradaTxtService")
        Else
            Log.registrarErro(mensagem, "EntradaTxtService")
        End If
        File.Copy(arquivo, FN4Common.Geral.Parametro("pastaDeRejeitadas") & numeroDaNota & "_" & dtProcessamento & ".txt", True)
        File.Delete(arquivo)
    End Sub

#Region "Acessorios"

    Private Sub inserirHistorico(ByVal tipo As String, ByVal descricao As String, ByVal nota As notaVO, ByVal cnpj As String)
        Try

            Dim hist As New historicoVO(tipo, descricao, nota.NFe_ide_nNF, cnpj, nota.serie)
            notaDAO.inserirHistorico(hist)

            Retorno.escreverRetorno(nota, DateTime.Now, tipo, descricao)
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

End Class
