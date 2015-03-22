Imports System.Xml.Schema
Imports FN4Common.DataAccess
Imports FN4Common
Imports System.Xml
Imports FN4EntradaTxtCtl
Imports FN4Common.Helpers
Imports System.IO
Imports System.Xml.Xsl


Public Class EnvioMonitor

    Private WithEvents tm As New System.Timers.Timer
    Private Shared resultadoValidacao As System.Text.StringBuilder

    Public Sub New()
        tm.Interval = Geral.Parametro("intervaloDeVarreduraDeSaida")
    End Sub

    Public Sub run()
        Log.registrarInfo("Monitor de Envio iniciado", "EnvioService")
        tm.Start()

    End Sub
    Public Sub pause()
        tm.Stop()
    End Sub

    Public Sub executarMonitorDeEnvio() Handles tm.Elapsed
        'parar o timer
        tm.Stop()
        Try
            'enviar todas as nfes
            'Log.registrarInfo("Enviando NFes", "EnvioService")


            enviarTodasNFe()
            'Log.registrarInfo("Enviando NFes em contingencia", "EnvioService")
            'enviarNFesEmContingencia()
            'enviaNotasDPEC()

            'obterProtocolos()
        Catch ex As Exception
            Log.registrarErro("Erro ao processar- " & Geral.ObterExceptionMessagesEmCascata(ex) & vbCrLf & ex.StackTrace, "EnvioService")
        Finally
            tm.Start()
        End Try

    End Sub

    Private Sub rejeitarNota(ByVal nota As notaVO)
        nota.statusDaNota = 3
        notas.alterarNota(nota)
    End Sub

    Public Sub gerarProcNfe(ByVal nota As notaVO, ByVal protnfe As XmlDocument)
        Dim nfe As New XmlDocument

        'carrega os arquivos

        Log.registrarErro("Entrou no gerar proc", "EnvioService")

        nfe.Load(nota.pastaDeTrabalho & nota.NFe_ide_nNF & "_assinado.xml")
        nfe.PreserveWhitespace = True

        protnfe.PreserveWhitespace = True

        Dim proc As New XmlDocument
        proc.Load(System.AppDomain.CurrentDomain.BaseDirectory & "XML\procNFe.xml")
        proc.PreserveWhitespace = True

        Try
            proc.ChildNodes(1).AppendChild(proc.ImportNode(nfe.ChildNodes(0), True))
        Catch ex As Exception
            Log.registrarErro("Erro ao montar o ProcNFe", "EnvioService")
        End Try

        Try
            proc.ChildNodes(1).AppendChild(proc.ImportNode(protnfe.ChildNodes(0).ChildNodes(6), True))
        Catch ex As Exception
            Log.registrarErro("Erro ao importar o ProtNFe - " & ex.Message, "EnvioService")
        End Try

        proc.Save(nota.pastaDeTrabalho & nota.NFe_ide_nNF & "_procNFe.xml")
    End Sub

    Private Sub enviarTodasNFe()

        Dim naoProcessadas As List(Of notaVO) = notaDAO.obterNotasNaoEnviadas
        Dim nota As notaVO
        Dim empresa As empresaVO

        'para cada nota não processada
        For Each nota In naoProcessadas

            'carregar o xml assinado dela
            Dim pathXmlAssinado As String = nota.pastaDeTrabalho & nota.NFe_ide_nNF & "_assinado.xml"

            Dim nfeXML As New XmlDocument

            Try
                nfeXML.Load(pathXmlAssinado)
            Catch ex As Exception
                Log.registrarErro("Erro ao carregar XML: " & pathXmlAssinado, "EnvioService")
                Continue For
            End Try

            'carrega empresa emitente
            empresa = FN4Common.empresaDAO.obterEmpresa(nota.NFe_emit_CNPJ, String.Empty)

            If empresa Is Nothing Then
                inserirHistorico("14", "empresa não encontrada", nota)
                nota.statusDaNota = 3
                notas.alterarNota(nota)
            End If

            Dim ufWs As String

            If UfsSemWebServices.SVAN.Contains(empresa.uf) Then
                ufWs = "SVAN"
            ElseIf UfsSemWebServices.SVRS.Contains(empresa.uf) Then
                ufWs = "SVRS"
            Else
                ufWs = empresa.uf
            End If

            Dim webservice
            If empresa.versao_nfe = "2.00" Then
                '2.00
                webservice = webserviceDAO.obterURLWebservice(ufWs, "NfeRecepcao", empresa.versao_nfe, empresa.homologacao)
            Else
                '3.10
                webservice = webserviceDAO.obterURLWebservice(ufWs, "NFeAutorizacao", empresa.versao_nfe, empresa.homologacao)
            End If

            If webservice Is Nothing Then
                nota.statusDaNota = 3
                nota.retEnviNFe_xMotivo = "Erro no ambiente informado"
                notaDAO.alterarNota(nota)
                inserirHistorico("26", "O ambiente configurado no painel de controle diverge do tipo de ambiente enviado (tp_amb). Se estiver utilizando a produção junto com a homologação, solicitar ao helpdesk o acionamento multi ambiente.", nota)
                Throw New Exception("Webservice não encontrado para nota " & nota.NFe_ide_nNF & " com os parametros: ufWs='" & ufWs & "', VersaoProduto='" & Geral.Parametro("VersaoProduto") & "', homologacao='" & empresa.homologacao & "'")
            End If

            If webservice.contingencia = 1 Then 'webservice não está funcionando, mandar para contingencia
                inserirHistorico("16", "", nota)
                nota.statusDaNota = 5
                notaDAO.alterarNota(nota)

                Continue For
            End If

            'criar lote
            inserirHistorico("10", "", nota)

            Dim enviNFe As New XmlDocument
            Dim pathEnvNFe As String

            If empresa.versao_nfe = "2.00" Then
                '2.00
                pathEnvNFe = System.AppDomain.CurrentDomain.BaseDirectory() & "XML\enviNFe200.xml"
            Else
                '3.10
                pathEnvNFe = System.AppDomain.CurrentDomain.BaseDirectory() & "XML\enviNFe.xml"
            End If

            Try
                enviNFe.Load(pathEnvNFe)
            Catch ex As Exception
                Log.registrarErro("Erro ao carregar XML: " & pathEnvNFe, "EnvioService")
                Continue For
            End Try

            'numerar o lote
            enviNFe.SelectSingleNode("/*[local-name()='enviNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='idLote' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText = nota.NFe_ide_nNF

            If empresa.versao_nfe <> "2.00" Then
                'sincrono/assincrono
                enviNFe.SelectSingleNode("/*[local-name()='enviNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='indSinc' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText = webservice.sincrono
            End If

            'adicionar 1 nota ao lote
            enviNFe.DocumentElement.AppendChild(enviNFe.ImportNode(nfeXML.ChildNodes(0), True))

            'carrega o cabecalho da mensagem
            Dim cabecMsg As XmlDocument = XmlHelper.obterUmCabecalho(enviNFe.SelectSingleNode("/*[local-name()='enviNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/@versao").InnerXml)

            'cria o webservice
            Dim ws
            If empresa.versao_nfe = "2.00" Then
                '2.0
                ws = New NFe.NFeRecepcao.NfeRecepcao2
                ws.nfeCabecMsgValue = New NFe.NFeRecepcao.nfeCabecMsg
            Else
                '3.10
                'verificar o estado e chamar o webservice de acordo (Paraná do KCT! Mudaram o schema do WS)
                Select Case empresa.uf
                    Case "PR"
                        ws = New NFe.NFeAutorizacaoPR.NfeAutorizacao3
                        ws.nfeCabecMsgValue = New NFe.NFeAutorizacaoPR.nfeCabecMsg
                    Case Else
                        ws = New NFe.NFeAutorizacao.NfeAutorizacao
                        ws.nfeCabecMsgValue = New NFe.NFeAutorizacao.nfeCabecMsg
                End Select
            End If

            Dim certificado = Geral.ObterCertificadoPorEmpresa(empresa.idEmpresa)
            'acesa ws
            ws.Url = webservice.url

            Log.registrarInfo("Enviando para " & ws.Url, "EnvioService")
            'adiciona o certificado
            ws.ClientCertificates.Add(certificado)

            ws.nfeCabecMsgValue.cUF = UFs.ListaDeCodigos(empresa.uf)
            ws.nfeCabecMsgValue.versaoDados = cabecMsg.InnerText

            Log.registrarInfo("Enviando nota - " & nota.NFe_ide_nNF, "EnvioService")
            inserirHistorico("11", "", nota)

            Dim xmlElementRetorno As XmlElement

            'faz o envio
            Try
                If empresa.versao_nfe = "2.00" Then
                    '2.0
                    xmlElementRetorno = ws.nfeRecepcaoLote2(enviNFe)
                Else
                    '3.1
                    xmlElementRetorno = ws.nfeAutorizacaoLote(enviNFe)
                End If

                ws.Dispose()
                ws = Nothing
            Catch ex As Exception
                'coloca o webservice em modo contingência
                If webservice.contingencia = 0 Then
                    Log.registrarErro("Erro ao enviar a Nota: Acionando o sistema de contingência para UF - '" & empresa.uf & "'", "EnvioService")
                    webservice.contingencia = 1
                    webserviceDAO.alterarWebservice(webservice)
                End If

                'apenas em caso de erro no webservice
                Log.registrarErro("Erro ao enviar nota: " & ex.Message & vbCrLf & ex.StackTrace, "EnvioService")
                inserirHistorico("12", "", nota)
                inserirHistorico("16", "", nota)

                'consultar a nota e caso retorno negativo, mandar para contingencia
                nota.statusDaNota = 199
                notaDAO.alterarNota(nota)
                'End If
                Continue For
            End Try

            'If webservice.contingencia = 1 Then
            'Log.registrarErro("Sistema normalizado: Saindo da contingência para a UF - '" & empresa.uf & "'", "EnvioService")
            'webservice.contingencia = 0
            'webserviceDAO.alterarWebservice(webservice)
            'End If

            Log.registrarInfo("Recebido o retorno do envio: " & xmlElementRetorno.InnerXml, "EnvioService")

            'verificar o retorno
            Dim xmlRetorno As New XmlDocument
            Dim stringWriter As New StringWriter()
            Dim xmlTextWriter As New XmlTextWriter(stringWriter)

            xmlElementRetorno.WriteTo(xmlTextWriter)

            Dim strRetorno = stringWriter.ToString()

            xmlRetorno.LoadXml(strRetorno.Replace("xmlns=""http://www.portalfiscal.inf.br/nfe""", ""))

            'salva o recibo na pasta de trabalho
            xmlRetorno.Save(nota.pastaDeTrabalho & nota.NFe_ide_nNF & "_recibo.xml")

            'verificar se retornou como lote recebido com sucesso (103/Assincrono) ou (104/sincrono)
            If xmlRetorno.SelectSingleNode("/retEnviNFe/cStat").InnerText = 103 Then 'assincrono
                'se for lote recebido com sucesso
                inserirHistorico("13", "", nota)

                'If nota.statusDaNota = 51 Then
                'nota.statusDaNota = 52
                'Else
                nota.statusDaNota = 1
                'End If

                'pega o recibo
                nota.retEnviNFe_infRec_nRec = xmlRetorno.SelectSingleNode("/retEnviNFe/infRec[1]/nRec[1]").InnerText
                notaDAO.alterarNota(nota)
            ElseIf xmlRetorno.SelectSingleNode("/retEnviNFe/cStat").InnerText = 204 Then 'Duplicidade da NFe

                If InStr(xmlRetorno.SelectSingleNode("/retEnviNFe/xMotivo").InnerText, "nRec") > 0 Then 'se retornar o recibo
                    Dim recibo_arr = Replace(Split(xmlRetorno.SelectSingleNode("/retEnviNFe/xMotivo").InnerText, "nRec:")(1), "]", "") 'pegar recibo
                    nota.retEnviNFe_infRec_nRec = recibo_arr
                    'gravar recibo
                End If

                nota.statusDaNota = 22 'nota autorizada, mandando cancelar
                notaDAO.alterarNota(nota)

                'se tiver o protocolo, mandar cancelar, se não, consultar o retorno do protocolo para depois cancelar (novo status 191 - concultar protocolo e cancelar)
                If Not String.IsNullOrEmpty(nota.protNfe_nProt) Then
                    'manda cancelar, pois não sei se a nota processada é esta última enviada e possui o protocolo
                    FN4Common.Geral.cancelarNFe(nota, "Cancelamento solicitado devido a erro de duplicidade na Sefaz.", empresa)
                Else
                    nota.statusDaNota = 18 'não possui o protocolo de retorno, buscar o protocolo da nota e mandar cancelar logo apos (18)
                    notaDAO.alterarNota(nota)
                End If

            ElseIf xmlRetorno.SelectSingleNode("/retEnviNFe/cStat").InnerText = 104 Then 'sincrono / Processado
                'tratar o retorno quando for sincrono
                FN4Common.Geral.tratar_retorno_xml(nota, xmlElementRetorno, empresa, 1)
            Else
                'caso contrario
                'criar historico do retorno 
                inserirHistorico("14", xmlRetorno.SelectSingleNode("/retEnviNFe/xMotivo").InnerText, nota)
                nota.retEnviNFe_xMotivo = xmlRetorno.SelectSingleNode("/retEnviNFe/xMotivo").InnerText
                nota.retEnviNFe_cStat = xmlRetorno.SelectSingleNode("/retEnviNFe/cStat").InnerText
                nota.statusDaNota = 3
                notaDAO.alterarNota(nota)
            End If
            xmlRetorno = Nothing
            stringWriter = Nothing
            xmlTextWriter = Nothing
        Next
    End Sub

    Public Shared Function validarXmlDPEC(ByVal PathXmlEnvio As String)
        resultadoValidacao = New System.Text.StringBuilder
        Dim myevent As ValidationEventHandler = New ValidationEventHandler(AddressOf ValidationEvent)
        'carrega o XSD

        Dim xschema As XmlSchema = XmlSchema.Read(New XmlTextReader(System.AppDomain.CurrentDomain.BaseDirectory() & "XSD\envDPEC_v1.01.xsd"), myevent)

        'configura a validação
        Dim xsettings As New XmlReaderSettings

        xsettings.ValidationType = ValidationType.Schema

        xsettings.Schemas.Add(xschema)

        'atribui o evento de validação
        AddHandler xsettings.ValidationEventHandler, myevent
        'faz a validação
        Using xreader As XmlReader = XmlReader.Create(PathXmlEnvio, xsettings)
            While xreader.Read
            End While
        End Using
        '"http://www.portalfiscal.inf.br/nfe", System.AppDomain.CurrentDomain.BaseDirectory() & "XSD\leiauteNFe_v1.10.xsd")

        Return resultadoValidacao.ToString
    End Function

    Private Shared Sub ValidationEvent(ByVal sender As Object, ByVal e As ValidationEventArgs)
        'erros.Add(New erro(vargs.Severity, vargs.Message, vargs.Exception.SourceUri, vargs.Exception.LineNumber, vargs.Exception.LinePosition))
        'If vargs.Message.IndexOf("The element 'NFe' in namespace 'http://www.portalfiscal.inf.br/nfe' has incomplete content. List of possible elements expected: 'Signature' in namespace") = -1 Then
        '    Throw New Exception("Erro de validação de schema. Arquivo " & vargs.Exception.SourceUri & _
        '          " (" & vargs.Exception.LineNumber & "," & vargs.Exception.LinePosition & _
        '          "): severidade " & vargs.Severity & " - " & vargs.Message)
        'End If

        Try
            If Not e.Message.Contains("The element 'envDPEC' in namespace 'http://www.portalfiscal.inf.br/nfe' has incomplete content. List of possible elements expected: 'Signature' in namespace") _
            And Not e.Message.Contains("O elemento 'envDPEC' no espaço para nome 'http://www.portalfiscal.inf.br/nfe' apresenta conteúdo incompleto. Lista de possíveis elementos esperados: 'Signature' no espaço para nome") Then
                Select Case e.Severity
                    Case XmlSeverityType.Error
                        resultadoValidacao.AppendLine("Erro(linha " & e.Exception.LineNumber & " posição " & e.Exception.LinePosition & "): " & e.Message)
                        resultadoValidacao.AppendLine("---")
                    Case XmlSeverityType.Warning
                        resultadoValidacao.AppendLine("Alerta: " & e.Message)
                        resultadoValidacao.AppendLine("---")
                End Select
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#Region "Acessorios"

    Private Sub inserirHistorico(ByVal tipo As String, ByVal descricao As String, ByVal nota As notaVO)
        Dim hist As New historicoVO(tipo, descricao, nota.NFe_ide_nNF, nota.NFe_emit_CNPJ, nota.serie)
        notaDAO.inserirHistorico(hist)

        Retorno.escreverRetorno(nota, DateTime.Now, tipo, descricao)
    End Sub

    Private Sub inserirHistorico(ByVal tipo As String, ByVal descricao As String, ByVal nNF As String, ByVal emitCNPJ As String, ByVal serie As Integer)
        Dim hist As New historicoVO(tipo, descricao, nNF, emitCNPJ, serie)
        notaDAO.inserirHistorico(hist)

        'TODO: Escrever retorno
    End Sub

    Private Sub RemoverNamespaceDoXml(ByVal caminhoDoArquivo As String)
        Dim leitor As System.IO.StreamReader

        leitor = File.OpenText(caminhoDoArquivo)

        Dim texto As String = leitor.ReadToEnd()

        leitor.Close()
        Dim strNamespace As String = " xmlns=" & Chr(34) & "http://www.portalfiscal.inf.br/nfe" & Chr(34)

        texto = texto.Replace(strNamespace, String.Empty)

        Dim escritor As New System.IO.StreamWriter(caminhoDoArquivo)
        escritor.Write(texto)
        escritor.Flush()
        escritor.Close()
    End Sub
#End Region


End Class
