Imports FN4Common
Imports FN4Common.DataAccess
Imports FN4Common.Helpers
Imports System.Xml
Imports System.Security.Cryptography
Imports System.Security.Cryptography.X509Certificates
Imports System.Timers
Imports System.Xml.Schema


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
        Dim arquivo As String = nota.pastaDeTrabalho & evento.infEvento_nSeqEvento & "_" & time_tmp.ToString("ddMMyyymmss") & "_Canc1_transformado.xml"
        cce.Save(arquivo)


        Dim ccetemp As New XmlDocument
        ccetemp.Load(arquivo)

        ccetemp.PreserveWhitespace = True
        ccetemp = XmlHelper.assinarNFeXML(ccetemp, ccetemp.GetElementsByTagName("infEvento")(0).Attributes("Id").InnerText, empresa.idEmpresa)

        arquivo = nota.pastaDeTrabalho & evento.infEvento_nSeqEvento & "_" & time_tmp.ToString("ddMMyyymmss") & "_Canc2_assinado.xml"
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
        arquivo = nota.pastaDeTrabalho & evento.infEvento_nSeqEvento & "_" & time_tmp.ToString("ddMMyyymmss") & "_Canc_lote_enviado.xml"
        envCCe.Save(arquivo)

        'Dim retornoValidacao As String
        'retornoValidacao = validarXmlDeEnvio(arquivo)

        'If retornoValidacao <> "" Then
        'inserirHistorico(29, retornoValidacao, nota)
        'evento.statusEvento = 3
        'evento.retEvento_cStat = 29
        'evento.retEvento_xMotivo = retornoValidacao
        'Log.registrarErro("Erro de envio da carta de correção " & evento.infEvento_nSeqEvento & " da nota " & nota.NFe_infNFe_id & ". " & retornoValidacao, Servico)

        'eventos.alterarEvento(evento)
        'Return
        'End If


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
        arquivo = nota.pastaDeTrabalho & evento.infEvento_nSeqEvento & "_" & time_tmp.ToString("ddMMyyymmss") & "_Canc_Retorno.xml"
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
                'registrado e vinculado
                evento.statusEvento = 21
                inserirHistorico(28, evento.retEvento_xMotivo, nota)
                nota.statusDaNota = 4 'apresenta nota cancelada
                nota.retEnviNFe_cStat = evento.retEvento_cStat
                nota.retEnviNFe_xMotivo = evento.retEvento_xMotivo
                notas.alterarNota(nota)
            ElseIf evento.retEvento_cStat = "136" Then
                'registrado e nao vinculado
                evento.statusEvento = 22
                inserirHistorico(29, evento.retEvento_xMotivo, nota)

            ElseIf evento.retEvento_cStat = "573" Then
                'duplicidade no evento
                evento.statusEvento = 21
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

        inserirHistorico(27, "", nota)

        Dim empresa As empresaVO = empresaDAO.obterEmpresa(evento.NFe_emit_CNPJ, String.Empty)

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

        'salva a cce no formato - [sequencial da cce]_ddMMyyyymmss_CCe.xml
        Dim arquivo As String = nota.pastaDeTrabalho & evento.infEvento_nSeqEvento & "_" & time_tmp.ToString("ddMMyyymmss") & "_CCe1_transformado.xml"
        cce.Save(arquivo)



        Dim ccetemp As New XmlDocument
        ccetemp.Load(arquivo)

        ccetemp.PreserveWhitespace = True
        ccetemp = XmlHelper.assinarNFeXML(ccetemp, ccetemp.GetElementsByTagName("infEvento")(0).Attributes("Id").InnerText, empresa.idEmpresa)

        arquivo = nota.pastaDeTrabalho & evento.infEvento_nSeqEvento & "_" & time_tmp.ToString("ddMMyyymmss") & "_CCe2_assinado.xml"
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
        arquivo = nota.pastaDeTrabalho & evento.infEvento_nSeqEvento & "_" & time_tmp.ToString("ddMMyyymmss") & "_CCe3_lote_enviado.xml"
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
        arquivo = nota.pastaDeTrabalho & evento.infEvento_nSeqEvento & "_" & time_tmp.ToString("ddMMyyymmss") & "_CCe_Retorno.xml"
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
                'registrado e vinculado
                evento.statusEvento = 21
                inserirHistorico(28, evento.retEvento_xMotivo, nota)
                'pegando o protocolo de retorno
                nota.statusDaNota = 19 'pegando o protocolo de consulta para gerar o pdf
                notas.alterarNota(nota)

            ElseIf evento.retEvento_cStat = "136" Then
                'registrado e nao vinculado
                evento.statusEvento = 22
                inserirHistorico(29, evento.retEvento_xMotivo, nota)
            Else
                'erros
                evento.statusEvento = 3
                inserirHistorico(29, evento.retEvento_xMotivo, nota)
            End If
            eventos.alterarEvento(evento)

        Else
            'lote rejeitado
            evento.statusEvento = 3
            evento.retEvento_cStat = statusLote
            evento.retEvento_xMotivo = motivo

            eventos.alterarEvento(evento)

            inserirHistorico(29, evento.retEvento_xMotivo, nota)
        End If

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
