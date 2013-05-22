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
            enviaNotasDPEC()

            obterProtocolos()
        Catch ex As Exception
            Log.registrarErro("Erro ao processar- " & Geral.ObterExceptionMessagesEmCascata(ex) & vbCrLf & ex.StackTrace, "EnvioService")
        Finally
            tm.Start()
        End Try

    End Sub

    Private Sub obterProtocolos()
        Dim notasSemProtocolo As List(Of notaVO) = notaDAO.obterNotasSemProtocolo
        Dim nota As notaVO
        Dim empresa As empresaVO
        Dim webservice As webserviceVO

        For Each nota In notasSemProtocolo
            empresa = FN4Common.empresaDAO.obterEmpresa(nota.NFe_emit_CNPJ, String.Empty)

            Dim ufWs As String

            If UfsSemWebServices.SVAN.Contains(empresa.uf) Then
                ufWs = "SVAN"
            ElseIf UfsSemWebServices.SVRS.Contains(empresa.uf) Then
                ufWs = "SVRS"
            Else
                ufWs = empresa.uf
            End If

            webservice = webserviceDAO.obterURLWebservice(ufWs,
                                                          "NfeConsultaProtocolo",
                                                          Geral.Parametro("Versao_ConsultaProtocolo"),
                                                          empresa.homologacao)

            If webservice Is Nothing Then
                inserirHistorico("12", "Não foi possível localizar o webservice de consulta de protocolo. Versao -" & Geral.Parametro("Versao_ConsultaProtocolo") & " / Homologacao -" & empresa.homologacao & " / UF - " & ufWs & " / WS - NfeConsultaProtocolo", nota)
                nota.statusDaNota = 3
                notas.alterarNota(nota)

                Continue For
            End If

            Dim ws As New NFe.NfeConsultaProtocolo.NfeConsulta2
            ws.Url = webservice.url
            ws.nfeCabecMsgValue = New NFe.NfeConsultaProtocolo.nfeCabecMsg
            ws.nfeCabecMsgValue.cUF = UFs.ListaDeCodigos(empresa.uf)
            ws.nfeCabecMsgValue.versaoDados = Geral.Parametro("Versao_ConsultaProtocolo")

            Log.registrarErro("Iniciando consulta de situação no WS: " & ws.Url & " para UF " & ws.nfeCabecMsgValue.cUF, "EnvioService")

            Dim envConsulta As New XmlDocument
            Dim pathXmlConsultaCanonico = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "XML\ConsultaNFeCanonico.xml")
            Dim pathXmlConsulta = nota.pastaDeTrabalho
            Dim xmlRetorno As XmlNode

            ' Carrega o XML canonico de envio para esse webservice de consulta de situação
            Try
                envConsulta.Load(pathXmlConsultaCanonico)
                envConsulta.PreserveWhitespace = True
            Catch ex As Exception
                Log.registrarErro("Erro ao carregar XML para consulta de situação: " & pathXmlConsultaCanonico, "EnvioService")
                rejeitarNota(nota)
                Continue For
            End Try

            ' preenche os dados necessários no XML
            envConsulta.SelectSingleNode("/*[local-name()='consSitNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='tpAmb' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").InnerText = Convert.ToString(empresa.homologacao + 1)
            envConsulta.SelectSingleNode("/*[local-name()='consSitNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='chNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").InnerText = nota.NFe_infNFe_id

            'Salva o XML de consulta
            Try
                pathXmlConsulta = Path.Combine(pathXmlConsulta, nota.NFe_ide_nNF & "_consultaProt.xml")
                envConsulta.Save(pathXmlConsulta)
            Catch ex As Exception
                Log.registrarErro("Não foi possível salvar o XML de consulta: " & pathXmlConsulta, "EnvioService")
                rejeitarNota(nota)
            End Try

            'Log.registrarErro("Aguardando 7 segundos o processamento do Sefaz...", "EnvioService")

            'aguarda 7 segundos para caso o sefaz ainda esteja em processamento
            'Threading.Thread.Sleep(7000)

            Log.registrarErro("Retorno do processamento", "EnvioService")

            ' Faz a consulta no webservice
            Try
                Dim certificado = Geral.ObterCertificadoPorEmpresa(empresa.idEmpresa)
                ws.ClientCertificates.Add(certificado)

                'TxtXmlHelper.validarXmlGeral(Path.Combine(pathXmlConsulta, nota.NFe_ide_nNF & "_consultaProt.xml"), "consSitNFe_v2.01")

                xmlRetorno = ws.nfeConsultaNF2(envConsulta)
            Catch ex As Exception
                Log.registrarErro("Não foi possível consultar o status da nota " & nota.NFe_ide_nNF & " série " & nota.serie & " Erro - " & ex.Message, "EnvioService")
                rejeitarNota(nota)

                Continue For
            End Try

            Dim pathXmlRetorno As String = Path.Combine(nota.pastaDeTrabalho, nota.NFe_ide_nNF & "_retornoConsultaProt.xml")

            Dim xmlDocumentoRetorno As New XmlDocument
            Dim XmlDocumentoProtocolo As New XmlDocument
            Dim stringWriter As New StringWriter()
            Dim xmlTextWriter As New XmlTextWriter(stringWriter)

            xmlRetorno.WriteTo(xmlTextWriter)

            Dim strRetorno = stringWriter.ToString()

            xmlDocumentoRetorno.LoadXml(strRetorno)

            ' Salva o retorno da consulta do webservice
            xmlDocumentoRetorno.Save(pathXmlRetorno)

            Log.registrarErro("Retorno do Status - " & xmlRetorno.ChildNodes(3).InnerText & " (" & xmlRetorno.ChildNodes(2).InnerText & ")", "EnvioService")

            ' carrega alguns campos necessários do retorno
            'Dim resultado As String = xmlRetorno.SelectSingleNode("/*[local-name()='retConsSitNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='cStat' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][0]").InnerText
            Dim resultado As String = xmlRetorno.ChildNodes(2).InnerText

            Try
                'gerando o proc_nfe nos casos que se aplicam
                If resultado = "100" Or resultado = "150" Or resultado = "101" Or resultado = "151" Or resultado = "110" Then
                    ' gera o procNFe com base na nota assinada e no protNFe que veio no retorno do webservice.
                    gerarProcNfe(nota, xmlDocumentoRetorno)
                End If
            Catch ex As Exception
                Log.registrarErro("Não foi possível gerar o procNFe. Motivo: " & ex.Message, "EnvioService")
                rejeitarNota(nota)
            End Try

            If resultado = "101" Or resultado = "151" Or resultado = "155" Then
                Try
                    'Dim motivo As String = xmlRetorno.SelectSingleNode("/*[local-name()='retConsSitNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='protNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='xMotivo' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").InnerText
                    Dim motivo As String = xmlRetorno.ChildNodes(3).InnerText

                    'Dim protocolo As String = xmlRetorno.SelectSingleNode("/*[local-name()='retConsSitNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='protNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='nProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").InnerText
                    Dim protocolo As String = xmlRetorno.ChildNodes(6).ChildNodes(0).ChildNodes(4).InnerText

                    'altera para nota cancelada
                    nota.statusDaNota = 4

                    'grava as informações na Base
                    nota.retEnviNFe_xMotivo = motivo
                    nota.protNfe_nProt = protocolo
                    nota.retEnviNFe_cStat = resultado
                    notas.alterarNota(nota)
                    inserirHistorico(15, motivo, nota)

                Catch ex As Exception

                    Log.registrarErro("Não foi possível salvar na base o retorno da nota cancelada. Motivo: " & ex.Message, "EnvioService")
                    rejeitarNota(nota)
                    'grava histórico de consulta da NFe
                    inserirHistorico(30, ex.Message, nota)
                End Try


            ElseIf resultado = "100" Or resultado = "150" Then

                Try
                    'Dim motivo As String = xmlRetorno.SelectSingleNode("/*[local-name()='retConsSitNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='protNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='xMotivo' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").InnerText
                    Dim motivo As String = xmlRetorno.ChildNodes(3).InnerText

                    'Dim protocolo As String = xmlRetorno.SelectSingleNode("/*[local-name()='retConsSitNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='protNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='nProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").InnerText
                    Dim protocolo As String = xmlRetorno.ChildNodes(6).ChildNodes(0).ChildNodes(4).InnerText

                    'altera as flags para continuarmos o fluxo
                    nota.statusDaNota = 21
                    'nota.impressaoSolicitada = 1'comentei, nao vi utilidade

                    'grava as informações na Base
                    nota.retEnviNFe_xMotivo = motivo
                    nota.protNfe_nProt = protocolo
                    nota.retEnviNFe_cStat = 100
                    notas.alterarNota(nota)

                    'grava histórico de nota autorizada
                    inserirHistorico(15, motivo, nota)

                Catch ex As Exception
                    Log.registrarErro("Não foi possível salvar na base o retorno da nota autorizada. Motivo: " & ex.Message, "EnvioService")
                    rejeitarNota(nota)
                    'grava histórico de consulta da NFe
                    inserirHistorico(30, ex.Message, nota)
                End Try

            ElseIf resultado = "110" Then 'nota denegada
                Try
                    'Dim motivo As String = xmlRetorno.SelectSingleNode("/*[local-name()='retConsSitNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='protNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='xMotivo' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").InnerText
                    Dim motivo As String = xmlRetorno.ChildNodes(3).InnerText

                    'Dim protocolo As String = xmlRetorno.SelectSingleNode("/*[local-name()='retConsSitNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='protNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='nProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").InnerText
                    Dim protocolo As String = xmlRetorno.ChildNodes(6).ChildNodes(0).ChildNodes(4).InnerText

                    'altera para status de nota denegada
                    nota.statusDaNota = 7

                    'grava as informações na Base
                    nota.retEnviNFe_xMotivo = motivo
                    nota.protNfe_nProt = protocolo
                    nota.retEnviNFe_cStat = 110
                    notas.alterarNota(nota)
                    inserirHistorico(15, motivo, nota)

                Catch ex As Exception
                    Log.registrarErro("Não foi possível salvar na base o retorno da nota denegada. Motivo: " & ex.Message, "EnvioService")
                    rejeitarNota(nota)
                    'grava histórico de consulta da NFe
                    inserirHistorico(30, ex.Message, nota)
                End Try
            ElseIf resultado = "102" Then 'inutilizada
                Try
                    'Dim motivo As String = xmlRetorno.SelectSingleNode("/*[local-name()='retConsSitNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='protNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='xMotivo' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").InnerText
                    Dim motivo As String = xmlRetorno.ChildNodes(3).InnerText

                    'Dim protocolo As String = xmlRetorno.SelectSingleNode("/*[local-name()='retConsSitNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='protNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='nProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").InnerText
                    'Dim protocolo As String = xmlRetorno.ChildNodes(6).ChildNodes(0).ChildNodes(4).InnerText

                    'altera para status de nota denegada
                    nota.statusDaNota = 6

                    'grava as informações na Base
                    nota.retEnviNFe_xMotivo = motivo
                    'nota.protNfe_nProt = protocolo
                    nota.retEnviNFe_cStat = 102
                    notas.alterarNota(nota)
                    inserirHistorico(15, motivo, nota)

                Catch ex As Exception
                    Log.registrarErro("Não foi possível salvar na base o retorno da nota inutilizada. Motivo: " & ex.Message, "EnvioService")
                    rejeitarNota(nota)
                    'grava histórico de consulta da NFe
                    inserirHistorico(30, ex.Message, nota)
                End Try

            ElseIf resultado = "217" Then 'nao encontrada
                Try
                    'Dim motivo As String = xmlRetorno.SelectSingleNode("/*[local-name()='retConsSitNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='protNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='xMotivo' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").InnerText
                    Dim motivo As String = xmlRetorno.ChildNodes(3).InnerText

                    'Dim protocolo As String = xmlRetorno.SelectSingleNode("/*[local-name()='retConsSitNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='protNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='nProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").InnerText
                    'Dim protocolo As String = xmlRetorno.ChildNodes(6).ChildNodes(0).ChildNodes(4).InnerText

                    'altera para status de nota rejeitada, pois nao foi encontrada
                    nota.statusDaNota = 3

                    'grava as informações na Base
                    nota.retEnviNFe_xMotivo = "Rejeição: NF-e não consta na base de dados da SEFAZ. Envie a NF-e novamente para o sistema."
                    'nota.protNfe_nProt = protocolo
                    nota.retEnviNFe_cStat = 217
                    notas.alterarNota(nota)
                    inserirHistorico(15, motivo, nota)

                Catch ex As Exception
                    Log.registrarErro("Não foi possível salvar na base o retorno da nota nao localizada. Motivo: " & ex.Message, "EnvioService")
                    rejeitarNota(nota)
                    'grava histórico de consulta da NFe
                    inserirHistorico(30, ex.Message, nota)
                End Try
            Else
                nota.statusDaNota = 5
                notaDAO.alterarNota(nota)

                Dim motivo As String = xmlRetorno.ChildNodes(3).InnerText
                inserirHistorico(15, motivo & " - Contingencia ativada", nota)
                'passando a Nota Para Contingência pois a mesma não foi autorizada
                inserirHistorico("16", "Passando para Contingência depois da tentativa de consulta de Status. - Cstat - " & resultado & " / Motivo - " & motivo, nota)
            End If


        Next
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

            Dim webservice = webserviceDAO.obterURLWebservice(
                ufWs,
                "NfeRecepcao",
                Geral.Parametro("VersaoProduto"),
                empresa.homologacao)

            If webservice Is Nothing Then
                nota.statusDaNota = 3
                nota.retEnviNFe_xMotivo = "Erro no ambiente informado"
                notaDAO.alterarNota(nota)
                inserirHistorico("26", "O ambiente configurado no painel de controle diverge do tipo de ambiente enviado (tp_amb). Se estiver utilizando a produção junto com a homologação, solicitar ao helpdesk o acionamento multi ambiente.", nota)
                Throw New Exception("Webservice não encontrado para nota " & nota.NFe_ide_nNF & " com os parametros: ufWs='" & ufWs & "', VersaoProduto='" & Geral.Parametro("VersaoProduto") & "', homologacao='" & empresa.homologacao & "'")
            End If

            If webservice.contingencia = 1 And nota.statusDaNota <> 51 Then ' webservice não está funcionando
                inserirHistorico("16", "", nota)
                nota.statusDaNota = 5
                notaDAO.alterarNota(nota)

                Continue For
            End If

            'criar lote
            inserirHistorico("10", "", nota)

            Dim enviNFe As New XmlDocument
            Dim pathEnvNFe As String = System.AppDomain.CurrentDomain.BaseDirectory() & "XML\enviNFe.xml"

            Try
                enviNFe.Load(pathEnvNFe)
            Catch ex As Exception
                Log.registrarErro("Erro ao carregar XML: " & pathEnvNFe, "EnvioService")
                Continue For
            End Try


            'numerar o lote
            enviNFe.SelectSingleNode("/*[local-name()='enviNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='idLote' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText = nota.NFe_ide_nNF

            'adicionar 1 nota ao lote
            enviNFe.DocumentElement.AppendChild(enviNFe.ImportNode(nfeXML.ChildNodes(0), True))

            'carrega o cabecalho da mensagem
            Dim cabecMsg As XmlDocument = XmlHelper.obterUmCabecalho(enviNFe.SelectSingleNode("/*[local-name()='enviNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/@versao").InnerXml)

            'cria o webservice
            Dim ws As New NFe.NFeRecepcao.NfeRecepcao2

            'verifica o ambiente

            'ws.Url = Geral.Parametro("servicoRecepcao")

            'Dim certificado = Geral.ObterCertificadoPorEmpresa(empresa.idEmpresa)

            Dim certificado = Geral.ObterCertificadoPorEmpresa(empresa.idEmpresa)

            ws.Url = webservice.url


            Log.registrarInfo("Enviando para " & ws.Url, "EnvioService")
            'adiciona o certificado
            ws.ClientCertificates.Add(certificado)

            ws.nfeCabecMsgValue = New NFe.NFeRecepcao.nfeCabecMsg
            ws.nfeCabecMsgValue.cUF = UFs.ListaDeCodigos(empresa.uf)
            ws.nfeCabecMsgValue.versaoDados = cabecMsg.InnerText

            Log.registrarInfo("Enviando nota - " & nota.NFe_ide_nNF, "EnvioService")
            inserirHistorico("11", "", nota)

            Dim xmlElementRetorno As XmlElement

            'faz o envio
            Try
                xmlElementRetorno = ws.nfeRecepcaoLote2(enviNFe)
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
                'a nota entra em contingencia

                'se a nota ja estiver numa DPEC, nao altera status
                If Not nota.statusDaNota = 51 Then
                    nota.statusDaNota = 19
                    notaDAO.alterarNota(nota)
                End If
                Continue For
            End Try

            If webservice.contingencia = 1 Then
                Log.registrarErro("Sistema normalizado: Saindo da contingência para a UF - '" & empresa.uf & "'", "EnvioService")
                webservice.contingencia = 0
                webserviceDAO.alterarWebservice(webservice)
            End If

            Log.registrarInfo("Recebido o retorno do envio: " & xmlElementRetorno.InnerXml, "EnvioService")

            'verificar o retorno
            Dim xmlRetorno As New XmlDocument
            Dim stringWriter As New StringWriter()
            Dim xmlTextWriter As New XmlTextWriter(stringWriter)

            xmlElementRetorno.WriteTo(xmlTextWriter)

            Dim strRetorno = stringWriter.ToString()

            'TODO: destruir
            'stringWriter = Nothing
            'xmlTextWriter = Nothing
            'xmlRetorno = Nothing

            xmlRetorno.LoadXml(strRetorno.Replace("xmlns=""http://www.portalfiscal.inf.br/nfe""", ""))

            'salva o recibo na pasta de trabalho
            xmlRetorno.Save(nota.pastaDeTrabalho & nota.NFe_ide_nNF & "_recibo.xml")

            If xmlRetorno.SelectSingleNode("/retEnviNFe/cStat").InnerText = 103 Then
                'se for lote recebido com sucesso
                inserirHistorico("13", "", nota)

                If nota.statusDaNota = 51 Then
                    nota.statusDaNota = 52
                Else
                    nota.statusDaNota = 1
                End If

                'pega o recibo
                nota.retEnviNFe_infRec_nRec = xmlRetorno.SelectSingleNode("/retEnviNFe/infRec[1]/nRec[1]").InnerText
                notaDAO.alterarNota(nota)
            Else
                'caso contrario
                'criar historico do retorno 
                inserirHistorico("14", xmlRetorno.SelectSingleNode("/retEnviNFe/xMotivo").InnerText, nota)
                nota.statusDaNota = 3
                notaDAO.alterarNota(nota)
            End If
        Next
    End Sub

    Private Sub enviaNotasDPEC()
        Try
            'obter notas em DPEC
            Dim enviNFe As New XmlDocument
            Dim nota As notaVO
            Dim empresa As empresaVO
            Dim xmlCanonico = TxtXmlHelper.obterXMLCanonico("XML\DPECXMLCanonico.xml")
            Dim xmlNotaDPECCanonico = TxtXmlHelper.obterXMLCanonico("XML\XMLDPECCanonico.xml")
            Dim pathXmlSemTransformar As String
            Dim pathXmlDPEC As String
            Dim cnpjs As List(Of String) = notaDAO.obterCNPJsComNotasEmContingencia()

            'para cada empresa com nota em DPEC
            For Each cnpj As String In cnpjs
                'carrega empresa com base no CNPJ
                empresa = FN4Common.empresaDAO.obterEmpresa(cnpj, String.Empty)
                Log.registrarErro("Gerando DPECs para a Empresa - '" & empresa.cnpj & "'", "EnvioService")

                'carrega notas em contingencia
                Dim notasEmDPEC As List(Of notaVO) = notaDAO.obterNotasEmContingenciaPorCNPJ(cnpj)

                'monta XML DPEC (usando XML canonico)
                xmlCanonico.SelectSingleNode("/*[local-name()='envDPEC' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infDPEC' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='ideDec' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='cUF' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").InnerText = UFs.ListaDeCodigos(empresa.uf)
                xmlCanonico.SelectSingleNode("/*[local-name()='envDPEC' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infDPEC' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='ideDec' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='tpAmb' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").InnerText = Convert.ToString(empresa.homologacao + 1)
                xmlCanonico.SelectSingleNode("/*[local-name()='envDPEC' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infDPEC' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='ideDec' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='verProc' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").InnerText = Geral.Parametro("VersaoProduto")
                xmlCanonico.SelectSingleNode("/*[local-name()='envDPEC' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infDPEC' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='ideDec' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='CNPJ' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").InnerText = empresa.cnpj
                xmlCanonico.SelectSingleNode("/*[local-name()='envDPEC' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infDPEC' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='ideDec' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='IE' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").InnerText = empresa.ie

                Dim ct As Integer = notasEmDPEC.Count
                Dim detxml As XmlNode = xmlCanonico.SelectSingleNode("/*[local-name()='envDPEC' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infDPEC' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='resNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").Clone

                For Each nota In notasEmDPEC
                    pathXmlSemTransformar = nota.pastaDeTrabalho & nota.NFe_ide_nNF & ".xml"
                    Log.registrarErro("Gerando DPEC para a Nota - '" & nota.NFe_ide_nNF & "'", "EnvioService")

                    Dim xmlCanonicoParaNota As XmlDocument = xmlCanonico

                    'obtem XML assinado
                    Dim nfeXML As New XmlDocument
                    nfeXML.Load(pathXmlSemTransformar)

                    'remove assinatura = Alterar para 1
                    gerarXmlDeSaida(pathXmlSemTransformar, nota.pastaDeTrabalho, nota.NFe_ide_nNF, System.AppDomain.CurrentDomain.BaseDirectory & "XSLT\" & Geral.Parametro("stylesheetDPEC"))

                    Dim pathXmlEnvio As String = Path.Combine(nota.pastaDeTrabalho, nota.NFe_ide_nNF & "_dpec_transformado.xml")
                    Log.registrarErro("Gerando DPEC transformado - " & pathXmlEnvio, "EnvioService")

                    nfeXML.Load(pathXmlEnvio)

                    Dim idNFe As String = TxtXmlHelper.gerarChaveDeAcessoComNamespace(nfeXML)

                    nfeXML.SelectSingleNode("/*[local-name()='NFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='ide' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='dhCont' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText = DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss")
                    nfeXML.SelectSingleNode("/*[local-name()='NFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='ide' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='xJust' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText = Geral.Parametro("justificativaDPEC")
                    nfeXML.SelectSingleNode("/*[local-name()='NFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").Attributes("Id").Value = "NFe" & idNFe
                    nfeXML.SelectSingleNode("/*[local-name()='NFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='ide' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='cDV' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText = nfeXML.SelectSingleNode("/*[local-name()='NFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").Attributes("Id").Value.Substring(46)

                    nota.NFe_infNFe_id = idNFe

                    'assinar novamente
                    nfeXML = XmlHelper.assinarNFeXML(nfeXML, nfeXML.GetElementsByTagName("infNFe")(0).Attributes.ItemOf("Id").InnerText, empresa.idEmpresa)

                    Dim pathXmlAssinado As String = Path.Combine(nota.pastaDeTrabalho, nota.NFe_ide_nNF & "_assinado.xml")

                    'salva XML com tpEmis diferente
                    nfeXML.Save(pathXmlAssinado)

                    xmlCanonicoParaNota.SelectSingleNode("/*[local-name()='envDPEC' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infDPEC' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='resNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='chNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").InnerText = nota.NFe_infNFe_id
                    xmlCanonicoParaNota.SelectSingleNode("/*[local-name()='envDPEC' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infDPEC' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").Attributes("Id").Value = "DPEC" + cnpj
                    xmlCanonicoParaNota.SelectSingleNode("/*[local-name()='envDPEC' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").Attributes("versao").Value = "1.01"

                    Dim node As XmlNode

                    If nota.NFe_dest_CNPJ <> String.Empty Then
                        xmlCanonicoParaNota.SelectSingleNode("/*[local-name()='envDPEC' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infDPEC' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='resNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='CNPJ' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").InnerText = nota.NFe_dest_CNPJ
                        node = xmlCanonicoParaNota.SelectSingleNode("/*[local-name()='envDPEC' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infDPEC' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='resNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='CPF' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']")
                        If Not node Is Nothing Then
                            xmlCanonicoParaNota.SelectSingleNode("/*[local-name()='envDPEC' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infDPEC' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='resNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").RemoveChild(node)
                        End If
                    ElseIf nota.NFe_dest_CPF <> String.Empty Then
                        xmlCanonicoParaNota.SelectSingleNode("/*[local-name()='envDPEC' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infDPEC' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='resNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='CPF' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").InnerText = nota.NFe_dest_CPF
                        node = xmlCanonicoParaNota.SelectSingleNode("/*[local-name()='envDPEC' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infDPEC' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='resNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='CNPJ' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']")
                        If Not node Is Nothing Then
                            xmlCanonicoParaNota.SelectSingleNode("/*[local-name()='envDPEC' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infDPEC' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='resNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").RemoveChild(node)
                        End If
                    Else
                        node = xmlCanonicoParaNota.SelectSingleNode("/*[local-name()='envDPEC' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infDPEC' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='resNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='CPF' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']")
                        If Not node Is Nothing Then
                            xmlCanonicoParaNota.SelectSingleNode("/*[local-name()='envDPEC' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infDPEC' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='resNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").RemoveChild(node)
                        End If

                    End If

                    xmlCanonicoParaNota.SelectSingleNode("/*[local-name()='envDPEC' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infDPEC' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='resNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='UF' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").InnerText = nota.NFe_dest_UF
                    xmlCanonicoParaNota.SelectSingleNode("/*[local-name()='envDPEC' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infDPEC' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='resNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='vNF' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").InnerText = nfeXML.SelectSingleNode("/*[local-name()='NFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='total' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='ICMSTot' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='vNF' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText
                    xmlCanonicoParaNota.SelectSingleNode("/*[local-name()='envDPEC' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infDPEC' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='resNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='vICMS' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").InnerText = nfeXML.SelectSingleNode("/*[local-name()='NFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='total' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='ICMSTot' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='vICMS' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText
                    xmlCanonicoParaNota.SelectSingleNode("/*[local-name()='envDPEC' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infDPEC' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='resNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='vST' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").InnerText = nfeXML.SelectSingleNode("/*[local-name()='NFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='total' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='ICMSTot' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='vST' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText

                    'salva

                    pathXmlDPEC = Path.Combine(nota.pastaDeTrabalho, nota.NFe_ide_nNF & "_" & cnpj & "_dpec.xml")

                    xmlCanonicoParaNota.Save(pathXmlDPEC)

                    Dim DPEC As New XmlDocument
                    DPEC.Load(pathXmlDPEC)
                    DPEC.PreserveWhitespace = True
                    DPEC = XmlHelper.assinarNFeXML(DPEC, DPEC.GetElementsByTagName("infDPEC")(0).Attributes.ItemOf("Id").InnerText, empresa.idEmpresa)

                    Dim pathXmlDPECAssinado As String = pathXmlDPEC.Replace(".xml", "_assinado.xml")

                    DPEC.Save(pathXmlDPECAssinado)

                    Dim retornovalidacao As String = TxtXmlHelper.validarXmlDPECDeEnvio(pathXmlDPECAssinado)

                    If Not String.IsNullOrEmpty(retornovalidacao) Then
                        nota.statusDaNota = 3
                        notaDAO.alterarNota(nota)
                        inserirHistorico("24", "Falha no schema XML de DPEC", nota)
                    End If

                    'monta webservice para envio
                    Dim webservice As webserviceVO = webserviceDAO.obterURLWebservice("DPEC", "RecepcaoDPEC", "1.10", empresa.homologacao)
                    enviNFe.Load(System.AppDomain.CurrentDomain.BaseDirectory() & "XML\enviDPEC.xml")
                    Dim cabecMsg As XmlDocument = XmlHelper.obterUmCabecalho(enviNFe.SelectSingleNode("/*[local-name()='enviNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/@versao").InnerXml)
                    Dim ws As New NFe.RecepcaoDPEC.SCERecepcaoRFB
                    Dim certificado = Geral.ObterCertificadoPorEmpresa(empresa.idEmpresa)

                    If webservice Is Nothing Then
                        nota.statusDaNota = 3
                        notaDAO.alterarNota(nota)
                        inserirHistorico("24", "WebService de Contingência não localizado.", nota)
                        Throw New Exception("WebService de Contingência não localizado.")
                    Else
                        Log.registrarInfo("Enviando para Dpec em " & webservice.url, "EnvioService")
                    End If

                    Dim xmlRetorno As XmlNode

                    Try

                        ws.Url = webservice.url
                        ws.sceCabecMsgValue = New NFe.RecepcaoDPEC.sceCabecMsg
                        ws.sceCabecMsgValue.versaoDados = cabecMsg.InnerText
                        ws.ClientCertificates.Add(certificado)

                        'tenta enviar
                        xmlRetorno = ws.sceRecepcaoDPEC(DPEC)

                    Catch ex As Exception
                        nota.statusDaNota = 3
                        notaDAO.alterarNota(nota)
                        inserirHistorico("24", "Erro na conexão com o WebService. Prováveis motivos: Certificado digital expirado ou não instalado ou falha na Sec. da Fazenda.", nota)
                        Throw New Exception("Erro de conexão com o WebService. Prováveis motivos: Certificado digital expirado ou não instalado ou falha na Sec. da Fazenda.")
                    End Try

                    Dim pathXmlRetorno = Path.Combine(nota.pastaDeTrabalho, nota.NFe_ide_nNF & "_reciboDpec.xml")

                    Dim xmlDocumentoRetorno As New XmlDocument
                    Dim stringWriter As New StringWriter()
                    Dim xmlTextWriter As New XmlTextWriter(stringWriter)

                    xmlRetorno.WriteTo(xmlTextWriter)

                    Dim strRetorno = stringWriter.ToString()

                    xmlDocumentoRetorno.LoadXml(strRetorno)

                    xmlDocumentoRetorno.Save(pathXmlRetorno)

                    Dim resultado As String = xmlRetorno.SelectSingleNode("/*[local-name()='infDPECReg' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='cStat' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText
                    Dim motivo As String = xmlRetorno.SelectSingleNode("/*[local-name()='infDPECReg' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='xMotivo' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText
                    Dim tipoDeHistorico As String

                    If resultado = "124" Then
                        nota.statusDaNota = 51
                        nota.impressaoSolicitada = 2
                        nota.impressoEmContingencia = 1
                        tipoDeHistorico = "22"
                    Else
                        nota.statusDaNota = 3
                        nota.impressaoSolicitada = 0
                        tipoDeHistorico = "24"
                    End If

                    notaDAO.alterarNota(nota)

                    inserirHistorico(tipoDeHistorico, motivo, nota)
                Next


            Next

            'para cada nota em DPEC

        Catch ex As Exception
            Throw ex
        End Try
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

    Private Sub gerarXmlDeSaida(ByVal arquivoDeEntrada As String, ByVal pastadetrabalho As String, ByVal numerodanota As Integer, ByVal xslt As String)
        Try
            Log.registrarInfo("Aplicação de stylesheet no arquivo" & arquivoDeEntrada, "EnvioService")

            'carrega o Stylesheet
            Dim xsl As New XslCompiledTransform
            xsl.Load(xslt)

            'aplica o stylesheet ao arquivo de entrada e gera o XML de Envio
            Dim pathXmlEnvio As String = pastadetrabalho & numerodanota & "_dpec_transformado.xml"
            xsl.Transform(arquivoDeEntrada, pathXmlEnvio)

            'Retorna o XML de envio
            '   Dim xmlEnvio As New XmlDocument
            'xmlEnvio.Load(pathXmlEnvio)
        Catch ex As Exception
            Log.registrarErro(ex.Message & vbCrLf & ex.StackTrace, "EnvioService")
            Throw ex
        End Try
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
