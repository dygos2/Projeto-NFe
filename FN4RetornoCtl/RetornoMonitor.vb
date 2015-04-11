Imports FN4Common
Imports System.Xml
Imports FN4Common.Helpers
Imports FN4EnvioCtl
Imports System.IO
Imports FN4Contingencia

Public Class RetornoMonitor
    Private WithEvents tm As New System.Timers.Timer

    Public Sub New()
        Log.registrarInfo("Novo", "RetornoService")
        tm.Interval = Geral.Parametro("intervaloDeVarreduraDeSaida")
    End Sub

    Public Sub run()
        Log.registrarInfo("Monitor de Retorno iniciado", "RetornoService")
        tm.Start()
    End Sub
    Public Sub pause()
        tm.Stop()
    End Sub

    Public Sub executarMonitorDeRetorno() Handles tm.Elapsed
        'parar o timer
        tm.Stop()
        Try
            obterRetornoDasNotas()
        Catch ex As Exception
            Log.registrarErro("Erro de execução: " & ex.Message & vbCrLf & ex.StackTrace, "RetornoService")
        Finally
            tm.Start()
        End Try
    End Sub
    Public Sub obterRetornoDasNotas()
        Dim naoEnviadas As List(Of notaVO) = notaDAO.obterNotasPendentesDeRetorno
        Dim nota As notaVO
        Dim empresa As empresaVO
        Try

            For Each nota In naoEnviadas
                'localiza a empresa
                empresa = FN4Common.empresaDAO.obterEmpresa(nota.NFe_emit_CNPJ, String.Empty)

                '   criar um consReciNFe
                Dim consReciNFe As New XmlDocument
                If empresa.versao_nfe = "2.00" Then
                    consReciNFe.Load(System.AppDomain.CurrentDomain.BaseDirectory() & "XML\consReciNFe200.xml")
                Else
                    consReciNFe.Load(System.AppDomain.CurrentDomain.BaseDirectory() & "XML\consReciNFe.xml")
                End If

                consReciNFe.PreserveWhitespace = True
                'acertar o ambiente
                consReciNFe.SelectSingleNode("/*[local-name()='consReciNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='tpAmb' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText = (empresa.homologacao + 1).ToString()

                'adicionar o numero do recibo
                consReciNFe.SelectSingleNode("/*[local-name()='consReciNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='nRec' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText = nota.retEnviNFe_infRec_nRec

                'carrega o cabecalho da mensagem
                Dim cabecMsg As XmlDocument = XmlHelper.obterUmCabecalho(consReciNFe.SelectSingleNode("/*[local-name()='consReciNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/@versao").InnerText)

                Dim retorno As XmlElement
                Dim ws

                If empresa.versao_nfe = "2.00" Then
                    '2.0
                    ws = New NFeRetRecepcao.NfeRetRecepcao2
                Else
                    '3.10
                    If empresa.uf = "PR" Then
                        ws = New NFeRetAutorizacao_PR.NfeRetAutorizacao3
                    Else
                        ws = New NFeRetAutorizacao.NfeRetAutorizacao
                    End If
                End If

                Dim ufWs As String
                ufWs = ""

                If nota.statusDaNota = 51 Then
                    'se a nota foi emitida em contingencia, buscar o retorno na contingencia
                    If UfsCont.SVCAN.Contains(empresa.uf) Then
                        ufWs = "SVCAN"
                    ElseIf UfsCont.SVCRS.Contains(empresa.uf) Then
                        ufWs = "SVCRS"
                    End If
                Else 'é status de retorno normal (1)
                    If UfsSemWebServices.SVAN.Contains(empresa.uf) Then
                        ufWs = "SVAN"
                    ElseIf UfsSemWebServices.SVRS.Contains(empresa.uf) Then
                        ufWs = "SVRS"
                    Else
                        ufWs = empresa.uf
                    End If
                End If

                Dim webservice
                If empresa.versao_nfe = "2.00" Then
                    '2.0
                    webservice = webserviceDAO.obterURLWebservice(ufWs, "NfeRetRecepcao", "2.00", empresa.homologacao)
                Else
                    '3.10
                    webservice = webserviceDAO.obterURLWebservice(ufWs, "NFeRetAutorizacao", Geral.Parametro("VersaoProduto"), empresa.homologacao)
                End If

                Dim certificado = Geral.ObterCertificadoPorEmpresa(empresa.idEmpresa)

                ws.Url = webservice.url
                ws.ClientCertificates.Add(certificado)
                If empresa.versao_nfe = "2.00" Then
                    '2.0
                    ws.nfeCabecMsgValue = New NFeRetRecepcao.nfeCabecMsg
                Else
                    '3.10
                    If empresa.uf = "PR" Then
                        ws.nfeCabecMsgValue = New NFeRetAutorizacao_PR.nfeCabecMsg
                    Else
                        ws.nfeCabecMsgValue = New NFeRetAutorizacao.nfeCabecMsg
                    End If
                End If

                ws.nfeCabecMsgValue.cUF = UFs.ListaDeCodigos(empresa.uf)
                ws.nfeCabecMsgValue.versaoDados = cabecMsg.InnerText

                Try
                    Log.registrarInfo("Buscando nota" & nota.NFe_ide_nNF & " do serviço " & ws.Url, "RetornoService")
                    If empresa.versao_nfe = "2.00" Then
                        '2.0
                        retorno = ws.nfeRetRecepcao2(consReciNFe)
                    Else
                        '3.10
                        If empresa.uf = "PR" Then
                            retorno = ws.nfeRetAutorizacao(consReciNFe)
                        Else
                            retorno = ws.nfeRetAutorizacaoLote(consReciNFe)
                        End If
                    End If

                    Log.registrarInfo("Recebido o retorno " & vbCrLf & retorno.InnerXml, "RetornoService")
                    ws.Dispose()
                    ws = Nothing
                Catch ex As Exception
                    'apenas em caso de erro no webservice
                    Log.registrarErro("Erro ao retornar a nota: " & ex.Message & vbCrLf & ex.StackTrace, "RetornoService")
                    inserirHistorico("15", "Erro na consulta da nota, lentidão na Sefaz", nota)

                    'consultar a nota e caso retorno negativo, mandar para contingencia
                    nota.statusDaNota = 17
                    notaDAO.alterarNota(nota)
                    'End If
                    Continue For
                End Try

                Dim xmlRetorno As New XmlDocument
                Dim stringWriter As New StringWriter()
                Dim xmlTextWriter As New XmlTextWriter(stringWriter)

                retorno.WriteTo(xmlTextWriter)

                Dim strRetorno = stringWriter.ToString()

                Dim xmlprotocolo As New XmlDocument
                xmlprotocolo.LoadXml(strRetorno)
                xmlprotocolo.PreserveWhitespace = True
                xmlprotocolo.Save(nota.pastaDeTrabalho & nota.NFe_ide_nNF & "_protocolo.xml")

                Dim resultado As String = xmlprotocolo.SelectSingleNode("/*[local-name()='retConsReciNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='cStat' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").InnerXml
                Dim xMotivo As String = xmlprotocolo.SelectSingleNode("/*[local-name()='retConsReciNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='xMotivo' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").InnerXml

                If resultado = "104" Then 'lote processado, processar o retorno dele

                    nota.retEnviNFe_xMotivo = xmlprotocolo.SelectSingleNode("/*[local-name()='retConsReciNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='protNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='xMotivo' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText
                    nota.retEnviNFe_cStat = xmlprotocolo.SelectSingleNode("/*[local-name()='retConsReciNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='protNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='cStat' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText

                    If nota.retEnviNFe_cStat = "100" Or nota.retEnviNFe_cStat = "150" Then 'AUTORIZADA OU AUTORIZADA COM ATRASO
                        'nota autorizada
                        Dim tp_sys_user = configuracaoDAO.obterConfiguracao("tp_sys", empresa.idEmpresa)
                        'verifica se o usuário está cadastrado no banco como 1, ou seja, ele vai receber o Danfe em PDF
                        If nota.statusDaNota = 1 And Not tp_sys_user Is Nothing Then
                            nota.impressaoSolicitada = 1
                        End If

                        'se nota estiver em contingencia, marcar nota processada em cont.
                        If nota.statusDaNota = 51 Then
                            nota.impressoEmContingencia = 1
                        End If
                        nota.statusDaNota = 21

                        nota.protNfe_nProt = xmlprotocolo.SelectSingleNode("/*[local-name()='retConsReciNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='protNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='nProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText
                        nota.NFe_infNFe_id = xmlprotocolo.SelectSingleNode("/*[local-name()='retConsReciNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='protNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='chNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText

                        Geral.gerarAnexo(nota, xmlprotocolo, empresa)
                        notaDAO.alterarNota(nota)

                        inserirHistorico(15, nota.retEnviNFe_xMotivo, nota)

                        Dim tp_sys = FN4Common.Geral.Parametro("tp_sys")
                        Dim tipoSistema = Integer.Parse(tp_sys)

                        If tipoSistema = 1 Then
                            If empresa.frest > 0 Then
                                empresa.frest = empresa.frest - 1
                            ElseIf empresa.prest > 0 Then
                                empresa.prest = empresa.prest - 1
                            Else
                                empresa.frest = 0
                                empresa.prest = 0
                            End If

                            empresaDAO.alterarPFrest(empresa)
                        End If

                        'se tiver no fn4config caminho de pasta configurado, então, copiar para lá o _protocolo
                        'If Geral.Parametro("copy_prot") <> "" Then
                        'Log.registrarInfo("salvando protocolo em " & Geral.Parametro("copy_prot") & empresa.cnpj & "_" & nota.NFe_ide_nNF & "_" & nota.serie & "_protocolo.xml", "RetornoService")
                        'Try
                        'xmlprotocolo.Save(Geral.Parametro("copy_prot") & empresa.cnpj & "_" & nota.NFe_ide_nNF & "_" & nota.serie & "_protocolo.xml")
                        'Catch ex As Exception
                        'Log.registrarInfo("Erro ao salvar o protocolo - " & ex.Message, "RetornoService")
                        'End Try
                        'End If

                    ElseIf nota.retEnviNFe_cStat = "110" Then 'DENEGADA

                        nota.statusDaNota = 7

                        nota.protNfe_nProt = xmlprotocolo.SelectSingleNode("/*[local-name()='retConsReciNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='protNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='nProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText
                        Geral.gerarAnexo(nota, xmlprotocolo, empresa)
                        notaDAO.alterarNota(nota)

                        inserirHistorico(15, nota.retEnviNFe_xMotivo, nota)

                        Dim tp_sys = FN4Common.Geral.Parametro("tp_sys")
                        Dim tipoSistema = Integer.Parse(tp_sys)

                        If tipoSistema = 1 Then
                            If empresa.frest > 0 Then
                                empresa.frest = empresa.frest - 1
                            ElseIf empresa.prest > 0 Then
                                empresa.prest = empresa.prest - 1
                            Else
                                empresa.frest = 0
                                empresa.prest = 0
                            End If

                            empresaDAO.alterarPFrest(empresa)
                        End If

                        'se tiver no fn4config caminho de pasta configurado, então, copiar para lá o _protocolo
                        'If Geral.Parametro("copy_prot") <> "" Then
                        'Log.registrarInfo("salvando protocolo em " & Geral.Parametro("copy_prot") & empresa.cnpj & "_" & nota.NFe_ide_nNF & "_" & nota.serie & "_protocolo.xml", "RetornoService")
                        'Try
                        'xmlprotocolo.Save(Geral.Parametro("copy_prot") & empresa.cnpj & "_" & nota.NFe_ide_nNF & "_" & nota.serie & "_protocolo.xml")
                        'Catch ex As Exception
                        'Log.registrarInfo("Erro ao salvar o protocolo - " & ex.Message, "RetornoService")
                        'End Try
                        'End If

                    ElseIf nota.retEnviNFe_cStat = "204" Then 'duplicidade
                        Dim tp_sys_user = configuracaoDAO.obterConfiguracao("tp_sys", empresa.idEmpresa)
                        If nota.statusDaNota = 1 And Not tp_sys_user Is Nothing Then
                            nota.impressaoSolicitada = 1 'alterado por Rodrigo, já seta impressao
                        End If
                        nota.statusDaNota = 19 'alterado por Rodrigo, se der duplicidade, buscar novamente o status e o recibo
                        notaDAO.alterarNota(nota)
                        inserirHistorico(15, nota.retEnviNFe_xMotivo, nota)
                    Else
                        nota.statusDaNota = 3
                        notaDAO.alterarNota(nota)
                        inserirHistorico(15, nota.retEnviNFe_xMotivo, nota)
                    End If

                ElseIf resultado = "105" Then 'lote em processamento
                    inserirHistorico(14, nota.retEnviNFe_xMotivo, nota)
                ElseIf resultado = "656" Then 'consumo indevido
                    nota.statusDaNota = 19 'se der como consumo indevido, o sistema tentará buscar o status
                    nota.retEnviNFe_cStat = "656"
                    nota.retEnviNFe_xMotivo = "Rejeição: Consumo Indevido na Sefaz."
                    notaDAO.alterarNota(nota)
                    inserirHistorico(15, "Rejeição: Consumo Indevido, sistema tentando consultar novamente em 60 segundos.", nota)
                Else 'lote rejeitado
                    nota.statusDaNota = 3
                    nota.retEnviNFe_cStat = resultado
                    nota.retEnviNFe_xMotivo = xMotivo
                    notaDAO.alterarNota(nota)
                    inserirHistorico(9, nota.retEnviNFe_xMotivo, nota)
                End If

            Next
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
#End Region


End Class