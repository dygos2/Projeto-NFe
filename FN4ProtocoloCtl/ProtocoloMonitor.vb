Imports System.Xml.Schema
Imports FN4Common.DataAccess
Imports FN4Common
Imports System.Xml
Imports System.Xml.XPath
Imports FN4EntradaTxtCtl
Imports FN4Common.Helpers
Imports System.IO
Imports System.Xml.Xsl

Public Class ProtocoloMonitor

    Private WithEvents tm As New System.Timers.Timer
    Private Shared resultadoValidacao As System.Text.StringBuilder

    Public Sub New()
        tm.Interval = Geral.Parametro("intervaloProtocolo")
    End Sub

    Public Sub run()
        Log.registrarInfo("Monitor do Protocolo iniciado", "ProtocoloService")
        tm.Start()

    End Sub
    Public Sub pause()
        tm.Stop()
    End Sub

    Public Sub executarMonitorDeEnvio() Handles tm.Elapsed
        'parar o timer
        tm.Stop()
        Try
            obterProtocolos()
        Catch ex As Exception
            Log.registrarErro("Erro ao processar- " & Geral.ObterExceptionMessagesEmCascata(ex) & vbCrLf & ex.StackTrace, "ProtocoloService")
        Finally
            tm.Start()
        End Try

    End Sub

    Private Sub test_xml()


        Dim envConsulta As New XmlDocument
        Dim doc As New Xml.XmlDocument

        Dim pathXmlConsultaCanonico = "C:\tmp\12385_retornoConsultaProt.xml"
        ' Carrega o XML canonico de envio para esse webservice de consulta de situação

        envConsulta.Load(pathXmlConsultaCanonico)

        If Not (IsNothing(envConsulta.GetElementsByTagName("protNFe")(0))) Then
            'consultando protocolo
            Dim prot = envConsulta.GetElementsByTagName("protNFe")(0)
            doc.LoadXml(prot.OuterXml)

            Dim cStat = doc.GetElementsByTagName("cStat")(0).InnerXml

        End If

    End Sub

    Private Sub obterProtocolos()
        'TODO: 
        'test_xml()

        Dim notasSemProtocolo As List(Of notaVO) = notaDAO.obterNotasSemProtocolo
        Dim nota As notaVO
        Dim empresa As empresaVO
        Dim webservice As webserviceVO
        Dim protocolo As String
        protocolo = ""

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
                                                          empresa.versao_nfe,
                                                          empresa.homologacao)

            If webservice Is Nothing Then
                inserirHistorico("12", "Não foi possível localizar o webservice de consulta de protocolo. Versao -" & empresa.versao_nfe & " / Homologacao -" & empresa.homologacao & " / UF - " & ufWs & " / WS - NfeConsultaProtocolo", nota)
                nota.statusDaNota = 3
                notas.alterarNota(nota)

                Continue For
            End If

            Dim ws As New NFe.NfeConsultaProtocolo.NfeConsulta2
            ws.Url = webservice.url
            ws.nfeCabecMsgValue = New NFe.NfeConsultaProtocolo.nfeCabecMsg
            ws.nfeCabecMsgValue.cUF = UFs.ListaDeCodigos(empresa.uf)
            'ws.nfeCabecMsgValue.versaoDados = Geral.Parametro("Versao_ConsultaProtocolo")
            ws.nfeCabecMsgValue.versaoDados = empresa.versao_nfe
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

            Log.registrarErro("Retorno do processamento", "EnvioService")

            ' Faz a consulta no webservice
            Try
                Dim certificado = Geral.ObterCertificadoPorEmpresa(empresa.idEmpresa)
                ws.ClientCertificates.Add(certificado)

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

            'Dim resultado As String = xmlRetorno.ChildNodes(2).InnerText
            Dim resultado As String = xmlDocumentoRetorno.GetElementsByTagName("cStat")(0).InnerXml
            Dim motivo As String = xmlDocumentoRetorno.GetElementsByTagName("xMotivo")(0).InnerXml

            'se retornar protocolo
            If Not (IsNothing(xmlDocumentoRetorno.GetElementsByTagName("protNFe")(0))) Then
                'consultando protocolo
                Dim prot = xmlDocumentoRetorno.GetElementsByTagName("protNFe")(0)
                XmlDocumentoProtocolo.LoadXml(prot.OuterXml)
                protocolo = XmlDocumentoProtocolo.GetElementsByTagName("nProt")(0).InnerXml
            End If

            Try
                Select Case resultado
                    Case 100 'Autorizado
                    Case 101 '
                    Case 151
                    Case 110
                        gerarProcNfe(nota, xmlDocumentoRetorno)
                End Select


            Catch ex As Exception
                Log.registrarErro("Não foi possível processar o retorno do protocolo. Motivo: " & ex.Message, "EnvioService")
                rejeitarNota(nota)
            End Try


            Try
                'gerando o proc_nfe nos casos que se aplicam
                If resultado = "100" Or resultado = "150" Or resultado = "101" Or resultado = "151" Or resultado = "110" Then
                    gerarProcNfe(nota, xmlDocumentoRetorno)
                End If
            Catch ex As Exception
                Log.registrarErro("Não foi possível gerar o procNFe. Motivo: " & ex.Message, "EnvioService")
                rejeitarNota(nota)
            End Try

            If resultado = "101" Or resultado = "151" Then
                Try
                    'altera para nota cancelada
                    nota.statusDaNota = 4

                    'grava as informações na Base
                    nota.retEnviNFe_xMotivo = motivo
                    nota.protNfe_nProt = protocolo
                    nota.retEnviNFe_cStat = 101
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
                    'altera as flags para continuarmos o fluxo
                    nota.statusDaNota = 21
                    nota.impressaoSolicitada = 1
                    'manda imprimir

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

                'Dim motivo As String = xmlRetorno.ChildNodes(3).InnerText
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
            proc.ChildNodes(1).AppendChild(proc.ImportNode(protnfe.ChildNodes(0).ChildNodes(7), True))
        Catch ex As Exception
            Log.registrarErro("Erro ao importar o ProtNFe - " & ex.Message, "EnvioService")
        End Try

        proc.Save(nota.pastaDeTrabalho & nota.NFe_ide_nNF & "_procNFe.xml")
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
