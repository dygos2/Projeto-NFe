Imports FN4Common
Imports FN4Common.DataAccess
Imports FN4Common.Helpers
Imports System.Xml
Imports System.Security.Cryptography
Imports System.Security.Cryptography.X509Certificates
Imports System.Timers
Imports System.Xml.Schema
Imports System.Net
Imports System.Text
Imports System.IO


Public Class CartaDeCorrecaoMonitor
    Private Shared resultadoValidacao As System.Text.StringBuilder
    Private WithEvents _tm As Timer

    Const Servico As String = "EnvioEventos"

    Public Sub New()

        _tm = New Timer(Geral.Parametro("intervaloCorrecao"))
        _tm.Enabled = False
        Log.registrarInfo("Servico de Eventos Iniciado", Servico)
    End Sub

    Public Sub run()
        _tm.Start()
    End Sub

    Public Sub pause()
        _tm.Stop()
    End Sub

    Private Sub executarEnvioDeCartaDeCorrecao() Handles _tm.Elapsed
        Me.pause()
        Try
            enviarCCe()
            enviarCancelamento()
        Catch ex As Exception
            Log.registrarErro(ex.Message, Servico)
        End Try
        Me.run()
    End Sub

    Private Sub gera_protocolo(ByVal xml_lote_envio As XmlDocument, ByVal xml_retorno As XmlDocument, ByVal path_pasta As String, ByVal evento As eventoVO)

        Try
            Dim proc_Evento, enviar, recebimento As New XmlDocument
            proc_Evento.PreserveWhitespace = True

            Dim path_proc_Evento As String = System.AppDomain.CurrentDomain.BaseDirectory() & "XML\ProcEvento.xml"

            ' Dim path_enviar As String = "c:/temp/lote_enviado.xml"
            'enviar.Load(path_enviar)

            'Dim path_recebimento As String = "c:/temp/Retorno.xml"
            'recebimento.Load(path_recebimento)

            Try
                ' envCCe.PreserveWhitespace = True
                proc_Evento.Load(path_proc_Evento)
            Catch ex As Exception
                Throw New Exception("Erro ao carregar XML: " & path_proc_Evento & " : " & ex.Message)
            End Try
            'inserindo os dados
            proc_Evento.DocumentElement.AppendChild(proc_Evento.ImportNode(xml_lote_envio.ChildNodes(0).ChildNodes(1), True))
            proc_Evento.DocumentElement.AppendChild(proc_Evento.ImportNode(xml_retorno.ChildNodes(0).ChildNodes(6), True))

            Dim arquivo As String = path_pasta & "proc_evento_" & evento.infEvento_nSeqEvento & "_" & evento.infEvento_tpEvento & ".xml"
            proc_Evento.Save(arquivo)

        Catch ex As Exception
            Log.registrarErro(ex.Message, Servico)
        End Try

    End Sub

    Private Sub enviarCCe()
        Dim evs As List(Of eventoVO)
        evs = eventos.obterCartasCorrecaoParaEnvio()

        For Each ev As eventoVO In evs
            Try

                enviarCartacorrecao(ev)
            Catch ex As Exception
                Log.registrarErro(ex.Message, Servico)
            End Try

        Next
    End Sub
    Private Sub enviarCancelamento()
        Dim evs As List(Of eventoVO)
        evs = eventos.obterCancelamentoParaEnvio()

        For Each ev As eventoVO In evs
            Try

                enviarCancelamento(ev)
            Catch ex As Exception
                Log.registrarErro(ex.Message, Servico)
            End Try

        Next
    End Sub
    Private Sub enviarCancelamento(ByVal evento As eventoVO)
        Log.registrarInfo("Enviando cancelamento " & evento.infEvento_nSeqEvento & " para nota " & evento.NFe_infNFe_id, "EnvioEventos")

        'obter a nota correspondente
        ''capturar os campos pela chave de acesso
        Dim serie As Integer = evento.NFe_infNFe_id.Substring(22, 3)
        Dim nnf As Integer = evento.NFe_infNFe_id.Substring(25, 9)

        Dim nota As notaVO = notas.obterNota(nnf, evento.NFe_emit_CNPJ, serie)

        inserirHistorico(19, "", nota)

        Dim empresa As empresaVO = empresaDAO.obterEmpresa(evento.NFe_emit_CNPJ, String.Empty)
        Dim tp_sys = configuracaoDAO.obterConfiguracao("tp_sys", empresa.idEmpresa)

        Dim cce As New XmlDocument
        Dim cabecMsg As New XmlDocument
        Dim pathCCeXML As String = System.AppDomain.CurrentDomain.BaseDirectory() & "XML\infEventocanc.xml"
        Dim pathCabecMsgXml As String = System.AppDomain.CurrentDomain.BaseDirectory & "XML\cabecMsgCCe.xml"

        Try
            cce.Load(pathCCeXML)

        Catch ex As Exception
            Throw New Exception("Erro ao carregar XML: " & pathCCeXML & " : " & ex.Message)
        End Try

        Try
            'cabecMsg.PreserveWhitespace = True
            cabecMsg.Load(pathCabecMsgXml)
        Catch ex As Exception
            Throw New Exception("Erro ao carregar XML: " & pathCabecMsgXml & " : " & ex.Message)
        End Try

        Dim idCCe As String = "ID" & evento.infEvento_tpEvento & evento.NFe_infNFe_id & evento.infEvento_nSeqEvento.ToString.PadLeft(2, "0")

        cce.SelectSingleNode("/evento/infEvento[1]/@Id").InnerText = idCCe
        cce.SelectSingleNode("/evento/infEvento[1]/cOrgao[1]").InnerText = empresa.codigoMunicipio.ToString.Substring(0, 2)
        cce.SelectSingleNode("/evento/infEvento[1]/tpAmb[1]").InnerText = (empresa.homologacao + 1).ToString()
        cce.SelectSingleNode("/evento/infEvento[1]/CNPJ[1]").InnerText = evento.NFe_emit_CNPJ
        cce.SelectSingleNode("/evento/infEvento[1]/chNFe[1]").InnerText = evento.NFe_infNFe_id

        Dim time_tmp As DateTime = DateTime.Parse(evento.infEvento_dhEvento)
        Dim utc_str As String

        Try 'tenta buscaro utc
            Dim utcback As utcVO = utc.obterUTC(empresa.uf)
            utc_str = utcback.utc
        Catch ex As Exception
            Throw New Exception("Erro ao carregar o UTC do estado " & empresa.uf & " : " & ex.Message)
        End Try

        cce.SelectSingleNode("/evento/infEvento[1]/dhEvento[1]").InnerText = time_tmp.ToString(String.Concat("yyyy-MM-ddTHH:mm:ss", utc_str))
        cce.SelectSingleNode("/evento/infEvento[1]/tpEvento[1]").InnerText = evento.infEvento_tpEvento
        cce.SelectSingleNode("/evento/infEvento[1]/nSeqEvento[1]").InnerText = evento.infEvento_nSeqEvento
        cce.SelectSingleNode("/evento/infEvento[1]/detEvento[1]/xJust[1]").InnerText = evento.infEvento_detEvento_xCorrecao
        cce.SelectSingleNode("/evento/infEvento[1]/detEvento[1]/nProt[1]").InnerText = nota.protNfe_nProt

        Dim root As XmlElement = cce.GetElementsByTagName("evento")(0)
        root.SetAttribute("xmlns", "http://www.portalfiscal.inf.br/nfe")

        'salva o cancelamento no formato - [sequencial da cce]_ddMMyyyymmss_CCe.xml
        Dim arquivo As String = nota.pastaDeTrabalho & "evento_" & evento.infEvento_nSeqEvento & "_" & evento.infEvento_tpEvento & "_transformado.xml"
        cce.Save(arquivo)


        Dim ccetemp As New XmlDocument
        ccetemp.Load(arquivo)

        ccetemp.PreserveWhitespace = True
        ccetemp = XmlHelper.assinarNFeXML(ccetemp, ccetemp.GetElementsByTagName("infEvento")(0).Attributes("Id").InnerText, empresa.idEmpresa)
        arquivo = nota.pastaDeTrabalho & "evento_" & evento.infEvento_nSeqEvento & "_" & evento.infEvento_tpEvento & "_assinado.xml"
        ccetemp.Save(arquivo)

        Dim ccenvio As New XmlDocument
        ccenvio.Load(arquivo)

        Dim envCCe As New XmlDocument
        Dim pathEnvCCe As String = System.AppDomain.CurrentDomain.BaseDirectory() & "XML\envEvento.xml"

        Try
            ' envCCe.PreserveWhitespace = True
            envCCe.Load(pathEnvCCe)
        Catch ex As Exception
            Throw New Exception("Erro ao carregar XML: " & pathEnvCCe & " : " & ex.Message)
        End Try


        'numerar o lote
        envCCe.SelectSingleNode("/*[local-name()='envEvento' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='idLote' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText = Right("000000000000000" & evento.controleLote, 15)

        'adicionar 1 evento ao lote
        'cce.PreserveWhitespace = True

        envCCe.DocumentElement.AppendChild(envCCe.ImportNode(ccenvio.ChildNodes(0), True))

        'Valida arquivo
        arquivo = nota.pastaDeTrabalho & "evento_" & evento.infEvento_nSeqEvento & "_" & evento.infEvento_tpEvento & "_lote_enviado.xml"
        envCCe.Save(arquivo)

        Dim ws As New NFe.RecepcaoEvento.RecepcaoEvento
        Dim certificado = Geral.ObterCertificadoPorEmpresa(empresa.idEmpresa)

        If certificado.Handle = IntPtr.Zero Then
            Throw New Exception("Certificado da empresa " & empresa.nome & "/" + empresa.cnpj & " não encontrado.")
        End If

        ws.ClientCertificates.Add(certificado)

        Dim uf As String
        If UfsSemWebServices.SVAN.Contains(empresa.uf) Then
            uf = "SVAN"
        ElseIf UfsSemWebServices.SVRS.Contains(empresa.uf) Then
            uf = "SVRS"
        Else
            uf = empresa.uf
        End If

        Dim webservice As webserviceVO = webservices.obterURLWebservice(uf, "NfeRecepcaoEvento", Geral.Parametro("VersaoProduto"), empresa.homologacao)

        If webservice Is Nothing Then
            Throw New Exception("Webservice de recepcao de evento não localizado. - " & uf & " | NfeRecepcaoEvento " & Geral.Parametro("VersaoProduto"))
        End If

        ws.Url = webservice.url

        Dim cabecalho As New NFe.RecepcaoEvento.nfeCabecMsg
        cabecalho.versaoDados = cabecMsg.InnerText
        cabecalho.cUF = UFs.ListaDeCodigos(empresa.uf).ToString()
        ws.nfeCabecMsgValue = cabecalho

        Dim retorno As XmlNode
        Try
            retorno = ws.nfeRecepcaoEvento(envCCe)
            ws.Dispose()
            ws = Nothing
        Catch ex As Exception
            evento.statusEvento = 3
            evento.retEvento_xMotivo = "Não foi possível enviar o cancelamento, erro no WS."
            eventos.alterarEvento(evento)
            inserirHistorico(31, "Ws não conseguiu comunicação.", nota)
            Throw ex
        End Try

        Log.registrarInfo("Recebido o retorno do envio: " & retorno.InnerXml, Servico)

        Dim xmlRetorno As New XmlDocument

        xmlRetorno.PreserveWhitespace = True
        xmlRetorno.LoadXml(retorno.OuterXml)
        arquivo = nota.pastaDeTrabalho & "evento_" & evento.infEvento_nSeqEvento & "_" & evento.infEvento_tpEvento & "_Retorno.xml"
        xmlRetorno.Save(arquivo)

        Dim statusLote As String = xmlRetorno.SelectSingleNode("/*[local-name()='retEnvEvento' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='cStat' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText
        Dim motivo As String = xmlRetorno.SelectSingleNode("/*[local-name()='retEnvEvento' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='xMotivo' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText

        'lote aprovado
        If statusLote = "128" Then

            evento.retEvento_cStat = xmlRetorno.SelectSingleNode("/*[local-name()='retEnvEvento' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='retEvento' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='infEvento' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='cStat' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText

            If InStr(xmlRetorno.InnerXml, "xEvento") > 0 Then
                evento.retEvento_xMotivo = xmlRetorno.SelectSingleNode("/*[local-name()='retEnvEvento' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='retEvento' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='infEvento' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='xEvento' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText
            End If

            If InStr(xmlRetorno.InnerXml, "xMotivo") > 0 Then
                evento.retEvento_xMotivo = String.Concat(evento.retEvento_xMotivo, " - ", xmlRetorno.SelectSingleNode("/*[local-name()='retEnvEvento' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='retEvento' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='infEvento' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='xMotivo' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText)
            End If

            If evento.retEvento_cStat = "135" Or evento.retEvento_cStat = "101" Or evento.retEvento_cStat = "151" Or evento.retEvento_cStat = "155" Then 'esta ainda indefinido se vai voltar status de nota a cancelada periodo irregular neste campo
                'criando o arquivo do protocolo
                gera_protocolo(envCCe, xmlRetorno, nota.pastaDeTrabalho, evento)

                If empresa.envio_auto_canc Then 'enviar email automático?
                    'registrado, vinculado e enviando email
                    evento.statusEvento = 20
                Else
                    'registrado e vinculado
                    evento.statusEvento = 21
                End If

                inserirHistorico(28, evento.retEvento_xMotivo, nota)

                nota.statusDaNota = 4 'apresenta nota cancelada
                nota.retEnviNFe_cStat = evento.retEvento_cStat
                nota.retEnviNFe_xMotivo = evento.retEvento_xMotivo
                notas.alterarNota(nota)

                'Se tp_sys = 1/ local e gerar o pdf
                If Not tp_sys Is Nothing Then
                    gera_pdf_eventos(nota, evento)
                End If

            ElseIf evento.retEvento_cStat = "136" Then
                'registrado e nao vinculado
                evento.statusEvento = 22
                inserirHistorico(29, evento.retEvento_xMotivo, nota)

            ElseIf evento.retEvento_cStat = "573" Then
                'criando o arquivo do protocolo
                gera_protocolo(envCCe, xmlRetorno, nota.pastaDeTrabalho, evento)
                'duplicidade no evento
                If empresa.envio_auto_canc Then 'enviar email automático?
                    'registrado, vinculado e enviando email
                    evento.statusEvento = 20
                Else
                    'registrado e vinculado
                    evento.statusEvento = 21
                End If
                inserirHistorico(28, evento.retEvento_xMotivo, nota)
                nota.statusDaNota = 4 'apresenta nota cancelada
                nota.retEnviNFe_cStat = 101
                nota.retEnviNFe_xMotivo = "Cancelamento de NF-e homologado"
                notas.alterarNota(nota)
            Else
                'erros
                evento.statusEvento = 3
                inserirHistorico(31, evento.retEvento_xMotivo, nota)
            End If
            eventos.alterarEvento(evento)

        Else
            'lote rejeitado
            evento.statusEvento = 3
            evento.retEvento_cStat = statusLote
            evento.retEvento_xMotivo = motivo

            eventos.alterarEvento(evento)
            inserirHistorico(31, evento.retEvento_xMotivo, nota)
        End If

    End Sub
    Private Sub enviarCartacorrecao(ByVal evento As eventoVO)
        Log.registrarInfo("Enviando carta de Correcao" & evento.infEvento_nSeqEvento & "para nota " & evento.NFe_infNFe_id, "EnvioEventos")

        'obter a nota correspondente
        ''capturar os campos pela chave de acesso
        Dim serie As Integer = evento.NFe_infNFe_id.Substring(22, 3)
        Dim nnf As Integer = evento.NFe_infNFe_id.Substring(25, 9)

        Dim nota As notaVO = notas.obterNota(nnf, evento.NFe_emit_CNPJ, serie)
        Dim pasta_arr As Array = nota.pastaDeTrabalho.Split("\")

        inserirHistorico(27, evento.infEvento_detEvento_xCorrecao, nota)

        Dim empresa As empresaVO = empresaDAO.obterEmpresa(evento.NFe_emit_CNPJ, String.Empty)
        Dim tp_sys = configuracaoDAO.obterConfiguracao("tp_sys", empresa.idEmpresa)

        Dim cce As New XmlDocument
        Dim cabecMsg As New XmlDocument
        Dim pathCCeXML As String = System.AppDomain.CurrentDomain.BaseDirectory() & "XML\infEvento.xml"
        Dim pathCabecMsgXml As String = System.AppDomain.CurrentDomain.BaseDirectory & "XML\cabecMsgCCe.xml"

        Try
            cce.Load(pathCCeXML)

        Catch ex As Exception
            Throw New Exception("Erro ao carregar XML: " & pathCCeXML & " : " & ex.Message)
        End Try

        Try
            'cabecMsg.PreserveWhitespace = True
            cabecMsg.Load(pathCabecMsgXml)
        Catch ex As Exception
            Throw New Exception("Erro ao carregar XML: " & pathCabecMsgXml & " : " & ex.Message)
        End Try

        Dim idCCe As String = "ID" & evento.infEvento_tpEvento & evento.NFe_infNFe_id & evento.infEvento_nSeqEvento.ToString.PadLeft(2, "0")

        cce.SelectSingleNode("/evento/infEvento[1]/@Id").InnerText = idCCe
        cce.SelectSingleNode("/evento/infEvento[1]/cOrgao[1]").InnerText = empresa.codigoMunicipio.ToString.Substring(0, 2)
        cce.SelectSingleNode("/evento/infEvento[1]/tpAmb[1]").InnerText = (empresa.homologacao + 1).ToString()
        cce.SelectSingleNode("/evento/infEvento[1]/CNPJ[1]").InnerText = evento.NFe_emit_CNPJ
        cce.SelectSingleNode("/evento/infEvento[1]/chNFe[1]").InnerText = evento.NFe_infNFe_id

        Dim time_tmp As DateTime = DateTime.Parse(evento.infEvento_dhEvento)
        Dim utc_str As String

        Try 'tenta buscaro utc
            Dim utcback As utcVO = utc.obterUTC(empresa.uf)
            utc_str = utcback.utc
        Catch ex As Exception
            Throw New Exception("Erro ao carregar o UTC do estado " & empresa.uf & " : " & ex.Message)
        End Try

        cce.SelectSingleNode("/evento/infEvento[1]/dhEvento[1]").InnerText = time_tmp.ToString(String.Concat("yyyy-MM-ddTHH:mm:ss", utc_str))
        cce.SelectSingleNode("/evento/infEvento[1]/tpEvento[1]").InnerText = evento.infEvento_tpEvento
        cce.SelectSingleNode("/evento/infEvento[1]/nSeqEvento[1]").InnerText = evento.infEvento_nSeqEvento
        cce.SelectSingleNode("/evento/infEvento[1]/detEvento[1]/xCorrecao[1]").InnerText = evento.infEvento_detEvento_xCorrecao

        Dim root As XmlElement = cce.GetElementsByTagName("evento")(0)
        root.SetAttribute("xmlns", "http://www.portalfiscal.inf.br/nfe")

        'salva a cce no formato - evento_[sequencial da cce]_[tipo do evento]_.xml
        Dim arquivo As String = nota.pastaDeTrabalho & "evento_" & evento.infEvento_nSeqEvento & "_" & evento.infEvento_tpEvento & "_transformado.xml"
        cce.Save(arquivo)

        Dim ccetemp As New XmlDocument
        ccetemp.Load(arquivo)

        ccetemp.PreserveWhitespace = True
        ccetemp = XmlHelper.assinarNFeXML(ccetemp, ccetemp.GetElementsByTagName("infEvento")(0).Attributes("Id").InnerText, empresa.idEmpresa)

        arquivo = nota.pastaDeTrabalho & "evento_" & evento.infEvento_nSeqEvento & "_" & evento.infEvento_tpEvento & "_assinado.xml"
        ccetemp.Save(arquivo)

        Dim ccenvio As New XmlDocument
        ccenvio.Load(arquivo)

        Dim envCCe As New XmlDocument
        Dim pathEnvCCe As String = System.AppDomain.CurrentDomain.BaseDirectory() & "XML\envEvento.xml"

        Try
            ' envCCe.PreserveWhitespace = True
            envCCe.Load(pathEnvCCe)
        Catch ex As Exception
            Throw New Exception("Erro ao carregar XML: " & pathEnvCCe & " : " & ex.Message)
        End Try


        'numerar o lote
        envCCe.SelectSingleNode("/*[local-name()='envEvento' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='idLote' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText = Right("000000000000000" & evento.controleLote, 15)

        'adicionar 1 evento ao lote
        'cce.PreserveWhitespace = True

        envCCe.DocumentElement.AppendChild(envCCe.ImportNode(ccenvio.ChildNodes(0), True))

        'Valida arquivo
        arquivo = nota.pastaDeTrabalho & "evento_" & evento.infEvento_nSeqEvento & "_" & evento.infEvento_tpEvento & "_lote_enviado.xml"
        envCCe.Save(arquivo)

        Dim retornoValidacao As String
        retornoValidacao = validarXmlDeEnvio(arquivo)

        If retornoValidacao <> "" Then
            inserirHistorico(29, retornoValidacao, nota)
            evento.statusEvento = 3
            evento.retEvento_cStat = 29
            evento.retEvento_xMotivo = retornoValidacao
            Log.registrarErro("Erro de envio da carta de correção " & evento.infEvento_nSeqEvento & " da nota " & nota.NFe_infNFe_id & ". " & retornoValidacao, Servico)

            eventos.alterarEvento(evento)
            Return
        End If

        Dim ws As New NFe.RecepcaoEvento.RecepcaoEvento
        Dim certificado = Geral.ObterCertificadoPorEmpresa(empresa.idEmpresa)

        If certificado.Handle = IntPtr.Zero Then
            Throw New Exception("Certificado da empresa " & empresa.nome & "/" + empresa.cnpj & " não encontrado.")
        End If

        ws.ClientCertificates.Add(certificado)

        Dim uf As String


        If UfsSemWebServices.SVAN.Contains(empresa.uf) Then
            uf = "SVAN"
        ElseIf UfsSemWebServices.SVRS.Contains(empresa.uf) Then
            uf = "SVRS"
        Else
            uf = empresa.uf
        End If

        Dim webservice As webserviceVO = webservices.obterURLWebservice(uf, "NfeRecepcaoEvento", Geral.Parametro("VersaoProduto"), empresa.homologacao)

        If webservice Is Nothing Then
            Throw New Exception("Webservice de recepcao de evento não localizado. - " & uf & " | NfeRecepcaoEvento " & Geral.Parametro("VersaoProduto"))
        End If

        ws.Url = webservice.url

        Dim cabecalho As New NFe.RecepcaoEvento.nfeCabecMsg
        cabecalho.versaoDados = cabecMsg.InnerText
        cabecalho.cUF = UFs.ListaDeCodigos(empresa.uf).ToString()
        ws.nfeCabecMsgValue = cabecalho

        Dim retorno As XmlNode
        Log.registrarInfo("Enviando Correcao " & evento.infEvento_nSeqEvento & "da nota " & nota.NFe_infNFe_id, Servico)

        Try
            retorno = ws.nfeRecepcaoEvento(envCCe)
            ws.Dispose()
            ws = Nothing
        Catch ex As Exception
            Throw ex
        End Try

        Log.registrarInfo("Recebido o retorno do envio: " & retorno.InnerXml, Servico)

        Dim xmlRetorno As New XmlDocument

        xmlRetorno.PreserveWhitespace = True
        xmlRetorno.LoadXml(retorno.OuterXml)
        arquivo = nota.pastaDeTrabalho & "evento_" & evento.infEvento_nSeqEvento & "_" & evento.infEvento_tpEvento & "_Retorno.xml"
        xmlRetorno.Save(arquivo)

        Dim statusLote As String = xmlRetorno.SelectSingleNode("/*[local-name()='retEnvEvento' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='cStat' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText
        Dim motivo As String = xmlRetorno.SelectSingleNode("/*[local-name()='retEnvEvento' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='xMotivo' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText

        'lote aprovado
        If statusLote = "128" Then

            evento.retEvento_cStat = xmlRetorno.SelectSingleNode("/*[local-name()='retEnvEvento' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='retEvento' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='infEvento' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='cStat' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText

            If InStr(xmlRetorno.InnerXml, "xEvento") > 0 Then
                evento.retEvento_xMotivo = xmlRetorno.SelectSingleNode("/*[local-name()='retEnvEvento' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='retEvento' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='infEvento' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='xEvento' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText
            End If

            If InStr(xmlRetorno.InnerXml, "xMotivo") > 0 Then
                evento.retEvento_xMotivo = String.Concat(evento.retEvento_xMotivo, " - ", xmlRetorno.SelectSingleNode("/*[local-name()='retEnvEvento' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='retEvento' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='infEvento' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='xMotivo' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText)
            End If

            'salva a cce no formato - [sequencial da cce]_ddMMyyyymmss_CCe.xml

            If evento.retEvento_cStat = "135" Then
                'criando o arquivo do protocolo
                gera_protocolo(envCCe, xmlRetorno, nota.pastaDeTrabalho, evento)

                If empresa.envio_auto_canc Then 'enviar email automático?
                    'registrado, vinculado e enviando email
                    evento.statusEvento = 20
                Else
                    'registrado e vinculado
                    evento.statusEvento = 21
                End If
                inserirHistorico(28, evento.retEvento_xMotivo, nota)
                notas.alterarNota(nota)

                'Se tp_sys = 1/ local e gerar o pdf
                If Not tp_sys Is Nothing Then
                    gera_pdf_eventos(nota, evento)
                End If

                eventos.alterarEvento(evento)

            ElseIf evento.retEvento_cStat = "136" Then
                'registrado e nao vinculado
                evento.statusEvento = 22
                inserirHistorico(29, evento.retEvento_xMotivo, nota)
                eventos.alterarEvento(evento)
            Else
                'erros
                evento.statusEvento = 3
                inserirHistorico(29, evento.retEvento_xMotivo, nota)
                eventos.alterarEvento(evento)
            End If

        Else
            'lote rejeitado
            evento.statusEvento = 3
            evento.retEvento_cStat = statusLote
            evento.retEvento_xMotivo = motivo

            eventos.alterarEvento(evento)

            inserirHistorico(29, evento.retEvento_xMotivo, nota)
        End If

    End Sub
    Public Sub gera_pdf_eventos(ByVal nota As notaVO, ByVal evento As eventoVO)
        Try
            Log.registrarErro("iniciando geração do PDF dos eventos", Servico)
            Dim uriString As String = Geral.Parametro("servidor_pdf_eventos") '"http://72.167.54.28/index_cce.php"
            Dim drive As String = Geral.Parametro("pastaDeProcessadas")
            Dim path_pdf As String

            ' Create a new WebClient instance
            Dim myWebClient As New WebClient()
            'Console.WriteLine(ControlChars.Cr + "Please enter the data to be posted to the URI {0}:", uriString)

            Dim file1, file2 As String

            path_pdf = nota.pastaDeTrabalho & "evento_" & evento.infEvento_nSeqEvento & "_" & evento.infEvento_tpEvento & ".pdf"
            file1 = nota.pastaDeTrabalho & nota.NFe_ide_nNF & "_procNFe.xml"

            file2 = nota.pastaDeTrabalho & "proc_evento_" & evento.infEvento_nSeqEvento & "_" & evento.infEvento_tpEvento & ".xml"

            Dim postData As String = file1
            Log.registrarInfo("enviando arquivo 1 - " & file1 & " | para - " & uriString, Servico)

            ' Apply ASCII Encoding to obtain the string as a byte array.
            Dim byteArray As Byte() = Encoding.ASCII.GetBytes(postData)
            Dim responseArray As Byte() = myWebClient.UploadFile(uriString, postData)
            Dim txt_ret As String = Encoding.ASCII.GetString(responseArray)

            postData = file2
            Log.registrarInfo("enviando arquivo 2 - " & file2 & " | para - " & uriString, Servico)

            ' Apply ASCII Encoding to obtain the string as a byte array.
            byteArray = Encoding.ASCII.GetBytes(postData)
            responseArray = myWebClient.UploadFile(uriString & "?file1=" & txt_ret, postData)

            Dim FS As New FileStream(path_pdf, FileMode.CreateNew, FileAccess.ReadWrite)
            FS.Write(responseArray, 0, responseArray.Length)
            FS.Close()
            Log.registrarInfo("PDF salvo em " & path_pdf, Servico)

        Catch ex As Exception
            Log.registrarErro("Erro de pdf tentando salvar: " & ex.Message, Servico)
            Throw ex
            'Error in accessing the resource, handle it 
        End Try
    End Sub


    Private Shared Sub inserirHistorico(ByVal tipo As String, ByVal texto As String, ByVal nota As notaVO)
        Dim historico As New historicoVO(tipo, texto, nota.NFe_ide_nNF, nota.NFe_emit_CPF & nota.NFe_emit_CNPJ, nota.serie)
        Try
            IBatisNETHelper.Instance.Insert("inserirHistorico", historico)
        Catch ex As Exception
            Throw ex
        End Try

    End Sub


    Private Shared Sub ValidationEvent(ByVal sender As Object, ByVal e As ValidationEventArgs)
        'erros.Add(New erro(vargs.Severity, vargs.Message, vargs.Exception.SourceUri, vargs.Exception.LineNumber, vargs.Exception.LinePosition))
        'If vargs.Message.IndexOf("The element 'NFe' in namespace 'http://www.portalfiscal.inf.br/nfe' has incomplete content. List of possible elements expected: 'Signature' in namespace") = -1 Then
        '    Throw New Exception("Erro de validação de schema. Arquivo " & vargs.Exception.SourceUri & _
        '          " (" & vargs.Exception.LineNumber & "," & vargs.Exception.LinePosition & _
        '          "): severidade " & vargs.Severity & " - " & vargs.Message)
        'End If

        Try
            If Not e.Message.Contains("The element 'NFe' in namespace 'http://www.portalfiscal.inf.br/nfe' has incomplete content. List of possible elements expected: 'Signature' in namespace") _
            And Not e.Message.Contains("O elemento 'NFe' no espaço para nome 'http://www.portalfiscal.inf.br/nfe' apresenta conteúdo incompleto. Lista de possíveis elementos esperados: 'Signature' no espaço para nome") Then
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


    Public Shared Function validarXmlDeEnvio(ByVal PathXmlEnvio As String)
        resultadoValidacao = New System.Text.StringBuilder
        Dim myevent As ValidationEventHandler = New ValidationEventHandler(AddressOf ValidationEvent)
        'carrega o XSD

        Dim pathXSD As String = System.AppDomain.CurrentDomain.BaseDirectory() & "XSD\envEvento_v1.00.xsd"

        Dim xschema As XmlSchema = XmlSchema.Read(New XmlTextReader(pathXSD), myevent)

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

    Protected Overrides Sub Finalize()
        Log.registrarInfo("Serviço de Carta de Correcao Parado.", Servico)
        MyBase.Finalize()
    End Sub
End Class
