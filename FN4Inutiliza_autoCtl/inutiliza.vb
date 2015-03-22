Imports System.Xml.Schema
Imports FN4Common.DataAccess
Imports FN4Common
Imports System.Xml
Imports FN4Common.Helpers
Imports System.IO
Imports System.Xml.Xsl

Public Class inutiliza

    Private WithEvents tm As New System.Timers.Timer
    Private Shared resultadoValidacao As System.Text.StringBuilder

    Public Sub New()
        ' tm.Interval = Geral.Parametro("intervaloDeVarreduraDeSaida")
    End Sub

    Public Sub run()
        'Log.registrarInfo("Monitor de Envio iniciado", "EnvioService")
        'tm.Start()

    End Sub
    Public Sub pause()
        'tm.Stop()
    End Sub

    Public Sub Inutilizarnotas()
        'pegando primeiro todos os clientes
        Dim clientes As List(Of empresaVO) = empresaDAO.obterTodasEmpresas()

        'Dim nota As notaVO
        'Dim empresa As empresaVO
        'Dim webservice As webserviceVO


        For Each empresa In clientes

        Next
        '    empresa = FN4Common.empresaDAO.obterEmpresa(nota.NFe_emit_CNPJ, String.Empty)

        '    Dim ufWs As String

        '    If UfsSemWebServices.SVAN.Contains(empresa.uf) Then
        '        ufWs = "SVAN"
        '    ElseIf UfsSemWebServices.SVRS.Contains(empresa.uf) Then
        '        ufWs = "SVRS"
        '    Else
        '        ufWs = empresa.uf
        '    End If

        '    webservice = webserviceDAO.obterURLWebservice(ufWs,
        '                                                  "NfeConsultaProtocolo",
        '                                                  Geral.Parametro("Versao_ConsultaProtocolo"),
        '                                                  empresa.homologacao)

        '    If webservice Is Nothing Then
        '        inserirHistorico("12", "Não foi possível localizar o webservice de consulta de protocolo. Versao -" & Geral.Parametro("Versao_ConsultaProtocolo") & " / Homologacao -" & empresa.homologacao & " / UF - " & ufWs & " / WS - NfeConsultaProtocolo", nota)
        '        nota.statusDaNota = 3
        '        notas.alterarNota(nota)

        '        Continue For
        '    End If

        '    Dim ws As New NFe.NfeConsultaProtocolo.NfeConsulta2
        '    ws.Url = webservice.url
        '    ws.nfeCabecMsgValue = New NFe.NfeConsultaProtocolo.nfeCabecMsg
        '    ws.nfeCabecMsgValue.cUF = UFs.ListaDeCodigos(empresa.uf)
        '    ws.nfeCabecMsgValue.versaoDados = Geral.Parametro("Versao_ConsultaProtocolo")

        '    Log.registrarErro("Iniciando consulta de situação no WS: " & ws.Url & " para UF " & ws.nfeCabecMsgValue.cUF, "EnvioService")

        '    Dim envConsulta As New XmlDocument
        '    Dim pathXmlConsultaCanonico = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "XML\ConsultaNFeCanonico.xml")
        '    Dim pathXmlConsulta = nota.pastaDeTrabalho
        '    Dim xmlRetorno As XmlNode

        '    ' Carrega o XML canonico de envio para esse webservice de consulta de situação
        '    Try
        '        envConsulta.Load(pathXmlConsultaCanonico)
        '        envConsulta.PreserveWhitespace = True
        '    Catch ex As Exception
        '        Log.registrarErro("Erro ao carregar XML para consulta de situação: " & pathXmlConsultaCanonico, "EnvioService")
        '        rejeitarNota(nota)
        '        Continue For
        '    End Try

        '    ' preenche os dados necessários no XML
        '    envConsulta.SelectSingleNode("/*[local-name()='consSitNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='tpAmb' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").InnerText = Convert.ToString(empresa.homologacao + 1)
        '    envConsulta.SelectSingleNode("/*[local-name()='consSitNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='chNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").InnerText = nota.NFe_infNFe_id

        '    'Salva o XML de consulta
        '    Try
        '        pathXmlConsulta = Path.Combine(pathXmlConsulta, nota.NFe_ide_nNF & "_consultaProt.xml")
        '        envConsulta.Save(pathXmlConsulta)
        '    Catch ex As Exception
        '        Log.registrarErro("Não foi possível salvar o XML de consulta: " & pathXmlConsulta, "EnvioService")
        '        rejeitarNota(nota)
        '    End Try

        '    'Log.registrarErro("Aguardando 7 segundos o processamento do Sefaz...", "EnvioService")

        '    'aguarda 7 segundos para caso o sefaz ainda esteja em processamento
        '    'Threading.Thread.Sleep(7000)

        '    Log.registrarErro("Retorno do processamento", "EnvioService")

        '    ' Faz a consulta no webservice
        '    Try
        '        Dim certificado = Geral.ObterCertificadoPorEmpresa(empresa.idEmpresa)
        '        ws.ClientCertificates.Add(certificado)

        '        'TxtXmlHelper.validarXmlGeral(Path.Combine(pathXmlConsulta, nota.NFe_ide_nNF & "_consultaProt.xml"), "consSitNFe_v2.01")

        '        xmlRetorno = ws.nfeConsultaNF2(envConsulta)
        '    Catch ex As Exception
        '        Log.registrarErro("Não foi possível consultar o status da nota " & nota.NFe_ide_nNF & " série " & nota.serie & " Erro - " & ex.Message, "EnvioService")
        '        rejeitarNota(nota)

        '        Continue For
        '    End Try

        '    Dim pathXmlRetorno As String = Path.Combine(nota.pastaDeTrabalho, nota.NFe_ide_nNF & "_retornoConsultaProt.xml")

        '    Dim xmlDocumentoRetorno As New XmlDocument
        '    Dim XmlDocumentoProtocolo As New XmlDocument
        '    Dim stringWriter As New StringWriter()
        '    Dim xmlTextWriter As New XmlTextWriter(stringWriter)

        '    xmlRetorno.WriteTo(xmlTextWriter)

        '    Dim strRetorno = stringWriter.ToString()

        '    xmlDocumentoRetorno.LoadXml(strRetorno)

        '    ' Salva o retorno da consulta do webservice
        '    xmlDocumentoRetorno.Save(pathXmlRetorno)

        '    Log.registrarErro("Retorno do Status - " & xmlRetorno.ChildNodes(3).InnerText & " (" & xmlRetorno.ChildNodes(2).InnerText & ")", "EnvioService")

        '    ' carrega alguns campos necessários do retorno
        '    'Dim resultado As String = xmlRetorno.SelectSingleNode("/*[local-name()='retConsSitNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='cStat' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][0]").InnerText
        '    Dim resultado As String = xmlRetorno.ChildNodes(2).InnerText

        '    Try
        '        'gerando o proc_nfe nos casos que se aplicam
        '        If resultado = "100" Or resultado = "150" Or resultado = "101" Or resultado = "151" Or resultado = "110" Then
        '            ' gera o procNFe com base na nota assinada e no protNFe que veio no retorno do webservice.
        '            gerarProcNfe(nota, xmlDocumentoRetorno)
        '        End If
        '    Catch ex As Exception
        '        Log.registrarErro("Não foi possível gerar o procNFe. Motivo: " & ex.Message, "EnvioService")
        '        rejeitarNota(nota)
        '    End Try

        '    If resultado = "101" Or resultado = "151" Or resultado = "155" Then
        '        Try
        '            'Dim motivo As String = xmlRetorno.SelectSingleNode("/*[local-name()='retConsSitNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='protNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='xMotivo' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").InnerText
        '            Dim motivo As String = xmlRetorno.ChildNodes(3).InnerText

        '            'Dim protocolo As String = xmlRetorno.SelectSingleNode("/*[local-name()='retConsSitNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='protNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='nProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").InnerText
        '            Dim protocolo As String = xmlRetorno.ChildNodes(6).ChildNodes(0).ChildNodes(4).InnerText

        '            'altera para nota cancelada
        '            nota.statusDaNota = 4

        '            'grava as informações na Base
        '            nota.retEnviNFe_xMotivo = motivo
        '            nota.protNfe_nProt = protocolo
        '            nota.retEnviNFe_cStat = resultado
        '            notas.alterarNota(nota)
        '            inserirHistorico(15, motivo, nota)

        '        Catch ex As Exception

        '            Log.registrarErro("Não foi possível salvar na base o retorno da nota cancelada. Motivo: " & ex.Message, "EnvioService")
        '            rejeitarNota(nota)
        '            'grava histórico de consulta da NFe
        '            inserirHistorico(30, ex.Message, nota)
        '        End Try


        '    ElseIf resultado = "100" Or resultado = "150" Then

        '        Try
        '            'Dim motivo As String = xmlRetorno.SelectSingleNode("/*[local-name()='retConsSitNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='protNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='xMotivo' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").InnerText
        '            Dim motivo As String = xmlRetorno.ChildNodes(3).InnerText

        '            'Dim protocolo As String = xmlRetorno.SelectSingleNode("/*[local-name()='retConsSitNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='protNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='nProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").InnerText
        '            Dim protocolo As String = xmlRetorno.ChildNodes(6).ChildNodes(0).ChildNodes(4).InnerText

        '            'altera as flags para continuarmos o fluxo
        '            nota.statusDaNota = 21
        '            'nota.impressaoSolicitada = 1'comentei, nao vi utilidade

        '            'grava as informações na Base
        '            nota.retEnviNFe_xMotivo = motivo
        '            nota.protNfe_nProt = protocolo
        '            nota.retEnviNFe_cStat = 100
        '            notas.alterarNota(nota)

        '            'grava histórico de nota autorizada
        '            inserirHistorico(15, motivo, nota)

        '        Catch ex As Exception
        '            Log.registrarErro("Não foi possível salvar na base o retorno da nota autorizada. Motivo: " & ex.Message, "EnvioService")
        '            rejeitarNota(nota)
        '            'grava histórico de consulta da NFe
        '            inserirHistorico(30, ex.Message, nota)
        '        End Try

        '    ElseIf resultado = "110" Then 'nota denegada
        '        Try
        '            'Dim motivo As String = xmlRetorno.SelectSingleNode("/*[local-name()='retConsSitNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='protNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='xMotivo' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").InnerText
        '            Dim motivo As String = xmlRetorno.ChildNodes(3).InnerText

        '            'Dim protocolo As String = xmlRetorno.SelectSingleNode("/*[local-name()='retConsSitNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='protNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='nProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").InnerText
        '            Dim protocolo As String = xmlRetorno.ChildNodes(6).ChildNodes(0).ChildNodes(4).InnerText

        '            'altera para status de nota denegada
        '            nota.statusDaNota = 7

        '            'grava as informações na Base
        '            nota.retEnviNFe_xMotivo = motivo
        '            nota.protNfe_nProt = protocolo
        '            nota.retEnviNFe_cStat = 110
        '            notas.alterarNota(nota)
        '            inserirHistorico(15, motivo, nota)

        '        Catch ex As Exception
        '            Log.registrarErro("Não foi possível salvar na base o retorno da nota denegada. Motivo: " & ex.Message, "EnvioService")
        '            rejeitarNota(nota)
        '            'grava histórico de consulta da NFe
        '            inserirHistorico(30, ex.Message, nota)
        '        End Try
        '    ElseIf resultado = "102" Then 'inutilizada
        '        Try
        '            'Dim motivo As String = xmlRetorno.SelectSingleNode("/*[local-name()='retConsSitNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='protNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='xMotivo' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").InnerText
        '            Dim motivo As String = xmlRetorno.ChildNodes(3).InnerText

        '            'Dim protocolo As String = xmlRetorno.SelectSingleNode("/*[local-name()='retConsSitNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='protNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='nProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").InnerText
        '            'Dim protocolo As String = xmlRetorno.ChildNodes(6).ChildNodes(0).ChildNodes(4).InnerText

        '            'altera para status de nota denegada
        '            nota.statusDaNota = 6

        '            'grava as informações na Base
        '            nota.retEnviNFe_xMotivo = motivo
        '            'nota.protNfe_nProt = protocolo
        '            nota.retEnviNFe_cStat = 102
        '            notas.alterarNota(nota)
        '            inserirHistorico(15, motivo, nota)

        '        Catch ex As Exception
        '            Log.registrarErro("Não foi possível salvar na base o retorno da nota inutilizada. Motivo: " & ex.Message, "EnvioService")
        '            rejeitarNota(nota)
        '            'grava histórico de consulta da NFe
        '            inserirHistorico(30, ex.Message, nota)
        '        End Try

        '    ElseIf resultado = "217" Then 'nao encontrada
        '        Try
        '            'Dim motivo As String = xmlRetorno.SelectSingleNode("/*[local-name()='retConsSitNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='protNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='xMotivo' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").InnerText
        '            Dim motivo As String = xmlRetorno.ChildNodes(3).InnerText

        '            'Dim protocolo As String = xmlRetorno.SelectSingleNode("/*[local-name()='retConsSitNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='protNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='nProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").InnerText
        '            'Dim protocolo As String = xmlRetorno.ChildNodes(6).ChildNodes(0).ChildNodes(4).InnerText

        '            'altera para status de nota rejeitada, pois nao foi encontrada
        '            nota.statusDaNota = 3

        '            'grava as informações na Base
        '            nota.retEnviNFe_xMotivo = "Rejeição: NF-e não consta na base de dados da SEFAZ. Envie a NF-e novamente para o sistema."
        '            'nota.protNfe_nProt = protocolo
        '            nota.retEnviNFe_cStat = 217
        '            notas.alterarNota(nota)
        '            inserirHistorico(15, motivo, nota)

        '        Catch ex As Exception
        '            Log.registrarErro("Não foi possível salvar na base o retorno da nota nao localizada. Motivo: " & ex.Message, "EnvioService")
        '            rejeitarNota(nota)
        '            'grava histórico de consulta da NFe
        '            inserirHistorico(30, ex.Message, nota)
        '        End Try
        '    Else
        '        nota.statusDaNota = 5
        '        notaDAO.alterarNota(nota)

        '        Dim motivo As String = xmlRetorno.ChildNodes(3).InnerText
        '        inserirHistorico(15, motivo & " - Contingencia ativada", nota)
        '        'passando a Nota Para Contingência pois a mesma não foi autorizada
        '        inserirHistorico("16", "Passando para Contingência depois da tentativa de consulta de Status. - Cstat - " & resultado & " / Motivo - " & motivo, nota)
        '    End If


        'Next
    End Sub
End Class
