Imports FN4Common
Imports System.Xml
Imports FN4Common.Helpers
Imports FN4EnvioCtl
Imports System.IO

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
            'obterretornos
            'Log.registrarInfo("Obtendo Retorno das NFes", "RetornoService")
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
                    ws = New NFeRetAutorizacao.NfeRetAutorizacao
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
                    ws.nfeCabecMsgValue = New NFeRetAutorizacao.nfeCabecMsg
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
                        retorno = ws.nfeRetAutorizacaoLote(consReciNFe)
                    End If

                    Log.registrarInfo("Recebido o retorno " & vbCrLf & retorno.InnerXml, "RetornoService")
                    ws.Dispose()
                    ws = Nothing
                Catch ex As Exception
                    Throw ex
                End Try

                'tratar retorno utilizando função única no Common

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

                        nota.statusDaNota = 21

                        nota.protNfe_nProt = xmlprotocolo.SelectSingleNode("/*[local-name()='retConsReciNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='protNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='nProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText
                        gerarAnexo(nota, xmlprotocolo, empresa)

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

                        'requisição da shockmetais - se tiver no fn4config caminho de pasta configurado, então, copiar para lá o _protocolo
                        If Geral.Parametro("copy_prot") <> "" Then
                            Log.registrarInfo("salvando protocolo em " & Geral.Parametro("copy_prot") & empresa.cnpj & "_" & nota.NFe_ide_nNF & "_" & nota.serie & "_protocolo.xml", "RetornoService")
                            Try
                                xmlprotocolo.Save(Geral.Parametro("copy_prot") & empresa.cnpj & "_" & nota.NFe_ide_nNF & "_" & nota.serie & "_protocolo.xml")
                            Catch ex As Exception
                                Log.registrarInfo("Erro ao salvar o protocolo - " & ex.Message, "RetornoService")
                            End Try
                        End If

                    ElseIf nota.retEnviNFe_cStat = "110" Then 'DENEGADA

                        nota.statusDaNota = 7

                        nota.protNfe_nProt = xmlprotocolo.SelectSingleNode("/*[local-name()='retConsReciNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='protNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='nProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText
                        gerarAnexo(nota, xmlprotocolo, empresa)
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

                        'requisição da shockmetais - se tiver no fn4config caminho de pasta configurado, então, copiar para lá o _protocolo
                        If Geral.Parametro("copy_prot") <> "" Then
                            Log.registrarInfo("salvando protocolo em " & Geral.Parametro("copy_prot") & empresa.cnpj & "_" & nota.NFe_ide_nNF & "_" & nota.serie & "_protocolo.xml", "RetornoService")
                            Try
                                xmlprotocolo.Save(Geral.Parametro("copy_prot") & empresa.cnpj & "_" & nota.NFe_ide_nNF & "_" & nota.serie & "_protocolo.xml")
                            Catch ex As Exception
                                Log.registrarInfo("Erro ao salvar o protocolo - " & ex.Message, "RetornoService")
                            End Try
                        End If

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

    Public Sub gerarAnexo(ByVal nota As notaVO, ByVal protnfe As XmlDocument, ByVal empresa As empresaVO)
        Try
            Dim nfe As New XmlDocument

            'carrega os arquivos

            nfe.Load(nota.pastaDeTrabalho & nota.NFe_ide_nNF & "_assinado.xml")
            nfe.PreserveWhitespace = True

            protnfe.PreserveWhitespace = True

            Dim proc As New XmlDocument
            If empresa.versao_nfe = "2.00" Then
                proc.Load(System.AppDomain.CurrentDomain.BaseDirectory & "XML\procNFe200.xml")
            Else
                proc.Load(System.AppDomain.CurrentDomain.BaseDirectory & "XML\procNFe.xml")
            End If
            proc.PreserveWhitespace = True

            proc.ChildNodes(1).AppendChild(proc.ImportNode(nfe.ChildNodes(0), True))
            If protnfe.SelectSingleNode("/*[local-name()='retConsReciNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='protNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]") Is Nothing Then
                proc.ChildNodes(1).AppendChild(proc.ImportNode(protnfe.SelectSingleNode("/retConsReciNFe/protNFe[1]"), True))
            Else
                proc.ChildNodes(1).AppendChild(proc.ImportNode(protnfe.SelectSingleNode("/*[local-name()='retConsReciNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='protNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]"), True))
            End If


            proc.Save(nota.pastaDeTrabalho & nota.NFe_ide_nNF & "_procNFe.xml")
        Catch ex As Exception
            Log.registrarErro("Ocorreu um erro na geração do procNFe" & ex.Message & vbCrLf & ex.StackTrace, "RetornoService")
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