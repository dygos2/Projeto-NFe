Imports FN4Common
Imports System.Xml
Imports FN4Common.Helpers
Imports FN4EnvioCtl
Imports System.IO
Imports System.Text.RegularExpressions


Public Class Contingencia
    Private WithEvents tm As New System.Timers.Timer
    Private pathXMLAssinado As String
    Private pathXMLEnvio As String

    Public Sub New()
        Log.registrarInfo("Novo", "Contingencia")
        tm.Interval = Geral.Parametro("intervaloDeVarreduraDeSaida")
    End Sub

    Public Sub run()
        Log.registrarInfo("Monitor de Contingencia iniciado", "Contingencia")
        tm.Start()
    End Sub
    Public Sub pause()
        tm.Stop()
    End Sub

    Public Sub executarMonitorDeContingencia() Handles tm.Elapsed
        tm.Stop()
        Try
            'Verificar as notas com status 5 se os estados estão em contingencia
            'para todos os estados que estão em contingencia, alterar notas para status 50 (pendente de geração do xml de contingencia e envio)
            'ativar o webservice para contingencia (1)

            'TODO: para os webservices em contingencia, verificar também se o sistema vai sair de contingencia
            consultar_status()

            'TODO: criar novo xml de contingencia e enviar para o estado destino de contingencia 
            enviar_notas_contingencia()

        Catch ex As Exception
            Log.registrarErro("Erro de execução: " & ex.Message & vbCrLf & ex.StackTrace, "Contingencia")
        Finally
            tm.Start()
        End Try
    End Sub
    Public Sub enviar_notas_contingencia()

        Dim naoProcessadas As List(Of notaVO) = notaDAO.obterNotasEnviarContingencia

        Dim nota As notaVO
        Dim empresa As empresaVO

        'para cada nota em contingencia não processada
        For Each nota In naoProcessadas

            'Carregar o xml transformado
            Dim pathXmlTransformado As String = nota.pastaDeTrabalho & nota.NFe_ide_nNF & "_transformado.xml"
            Dim NFe As New XmlDocument

            'carrega empresa emitente
            empresa = FN4Common.empresaDAO.obterEmpresa(nota.NFe_emit_CNPJ, String.Empty)

            Try
                NFe.Load(pathXmlTransformado)
            Catch ex As Exception
                Log.registrarErro("Erro ao carregar o XML: " & pathXmlTransformado, "Contingencia")
                Continue For
            End Try

            'Verificar servidor contingencia
            Dim ufWs As String
            Dim tpEmis As Integer
            ufWs = ""
            If UfsCont.SVCAN.Contains(empresa.uf) Then
                ufWs = "SVCAN"
                tpEmis = 6
            ElseIf UfsCont.SVCRS.Contains(empresa.uf) Then
                ufWs = "SVCRS"
                tpEmis = 7
            End If

            'Alterar conteudo para contingencia e salvar
            Try
                NFe.LoadXml(NFe.OuterXml.Replace(NFe.DocumentElement.NamespaceURI, ""))
                NFe.DocumentElement.RemoveAllAttributes()

                Dim dhCont As XmlNode = NFe.CreateElement("dhCont")
                Dim xJust As XmlNode = NFe.CreateElement("xJust")

                Dim time As DateTime = DateTime.Now
                Dim format As String = "yyyy-MM-ddTHH:mm:ss"

                dhCont.InnerXml = String.Concat(time.ToString(format), empresa.utc)
                xJust.InnerXml = "Sistema de contingencia ativado devido a erro no envio pelo sistema normal"

                NFe.GetElementsByTagName("ide")(0).AppendChild(dhCont)
                NFe.GetElementsByTagName("ide")(0).AppendChild(xJust)
                NFe.GetElementsByTagName("tpEmis")(0).InnerXml = tpEmis
                NFe.GetElementsByTagName("infNFe")(0).Attributes("Id").Value = String.Concat("NFe", FN4EntradaTxtCtl.TxtXmlHelper.gerarChaveDeAcesso(NFe))
                NFe.GetElementsByTagName("cDV")(0).InnerXml = Right(NFe.GetElementsByTagName("infNFe")(0).Attributes("Id").Value, 1)

                NFe.DocumentElement.SetAttribute("xmlns", "http://www.portalfiscal.inf.br/nfe")
            Catch ex As Exception
                Log.registrarErro("Erro ao transformar o XML de contingencia: " & pathXmlTransformado, "Contingencia")
                Continue For
            End Try

            'Assinar nota
            pathXMLAssinado = pathXmlTransformado.Replace("_transformado.xml", "_transf_cont.xml")
            NFe.Save(pathXMLAssinado)

            'Assinar o XML
            Dim NFe_env As New XmlDocument
            NFe.Load(pathXMLAssinado)
            NFe.PreserveWhitespace = True
            NFe = XmlHelper.assinarNFeXML(NFe, NFe.GetElementsByTagName("infNFe")(0).Attributes.ItemOf("Id").InnerText, empresa.idEmpresa)
            'Gravar o XML assinado
            pathXMLAssinado = pathXmlTransformado.Replace("_transformado.xml", "_assinado_cont.xml")
            NFe.Save(pathXMLAssinado)

            Dim webservice
            If empresa.versao_nfe = "2.00" Then
                '2.00
                webservice = webserviceDAO.obterURLWebservice(ufWs, "NfeRecepcao", empresa.versao_nfe, empresa.homologacao)
            Else
                '3.10
                webservice = webserviceDAO.obterURLWebservice(ufWs, "NFeAutorizacao", empresa.versao_nfe, empresa.homologacao)
            End If

            Dim enviNFe As New XmlDocument
            Dim pathEnvNFe As String

            If empresa.versao_nfe = "2.00" Then
                '2.00
                pathEnvNFe = System.AppDomain.CurrentDomain.BaseDirectory() & "XML\enviNFe200.xml"
            Else
                '3.10
                pathEnvNFe = System.AppDomain.CurrentDomain.BaseDirectory() & "XML\enviNFe.xml"
            End If

            'Nota entrando em contingencia
            inserirHistorico("16", "Inicio de envio da nota para contingencia", nota)

            Try
                enviNFe.Load(pathEnvNFe)
            Catch ex As Exception
                Log.registrarErro("Erro ao carregar XML: " & pathEnvNFe, "Contingencia")
                Continue For
            End Try

            'numerar o lote
            enviNFe.SelectSingleNode("/*[local-name()='enviNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='idLote' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText = nota.NFe_ide_nNF

            If empresa.versao_nfe <> "2.00" Then
                'sincrono/assincrono
                enviNFe.SelectSingleNode("/*[local-name()='enviNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='indSinc' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText = webservice.sincrono
            End If

            'adicionar 1 nota ao lote
            enviNFe.DocumentElement.AppendChild(enviNFe.ImportNode(NFe.ChildNodes(0), True))

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

            Log.registrarInfo("Enviando para " & ws.Url, "Contingencia")
            'adiciona o certificado
            ws.ClientCertificates.Add(certificado)

            ws.nfeCabecMsgValue.cUF = UFs.ListaDeCodigos(empresa.uf)
            ws.nfeCabecMsgValue.versaoDados = cabecMsg.InnerText

            Log.registrarInfo("Enviando nota para contingencia  - " & nota.NFe_ide_nNF, "Contingencia")
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
                'tirar de contingencia o webservice pois a contingencia esta com pau???
                If webservice.contingencia = 1 Then
                    Log.registrarErro("Erro ao enviar a Nota EM contingencia: Voltando para operação normal para UF - '" & empresa.uf & "'", "Contingencia")
                    webservice.contingencia = 0
                    webserviceDAO.alterarWebservice(webservice)
                End If

                'apenas em caso de erro no webservice
                Log.registrarErro("Erro ao enviar nota para contingencia: " & ex.Message & vbCrLf & ex.StackTrace, "Contingencia")
                inserirHistorico("12", "Erro no envio da nota em contingência, sistema do Sefaz com erro no retorno", nota)

                'voltar a nota para envio normal pois a contingencia nao esta retornando
                nota.statusDaNota = 0
                notaDAO.alterarNota(nota)
                Continue For
            End Try

            Log.registrarInfo("Recebido o retorno do envio: " & xmlElementRetorno.InnerXml, "Contingencia")

            'verificar o retorno
            Dim xmlRetorno As New XmlDocument
            Dim stringWriter As New StringWriter()
            Dim xmlTextWriter As New XmlTextWriter(stringWriter)

            xmlElementRetorno.WriteTo(xmlTextWriter)

            Dim strRetorno = stringWriter.ToString()
            xmlRetorno.LoadXml(strRetorno.Replace("xmlns=""http://www.portalfiscal.inf.br/nfe""", ""))

            'salva o recibo na pasta de trabalho
            xmlRetorno.Save(nota.pastaDeTrabalho & nota.NFe_ide_nNF & "_recibo_cont.xml")

            'verificar se retornou como lote recebido com sucesso (103/Assincrono)
            If xmlRetorno.SelectSingleNode("/retEnviNFe/cStat").InnerText = 103 Then 'assincrono
                'se for lote recebido com sucesso
                inserirHistorico("13", "", nota)
                nota.statusDaNota = 51

                'pega o recibo
                nota.retEnviNFe_infRec_nRec = xmlRetorno.SelectSingleNode("/retEnviNFe/infRec[1]/nRec[1]").InnerText
                notaDAO.alterarNota(nota)
            ElseIf xmlRetorno.SelectSingleNode("/retEnviNFe/cStat").InnerText = 204 Then 'Duplicidade da NFe

                If InStr(xmlRetorno.SelectSingleNode("/retEnviNFe/xMotivo").InnerText, "nRec") > 0 Then 'se retornar o recibo
                    Dim recibo_arr = Replace(Split(xmlRetorno.SelectSingleNode("/retEnviNFe/xMotivo").InnerText, "nRec:")(1), "]", "") 'pegar recibo
                    nota.retEnviNFe_infRec_nRec = recibo_arr
                    'gravar recibo
                End If

                nota.statusDaNota = 19 'consultar o protocolo na sefaz de contingencia
                nota.impressoEmContingencia = 1
                notaDAO.alterarNota(nota)

            ElseIf xmlRetorno.SelectSingleNode("/retEnviNFe/cStat").InnerText = 104 Then 'sincrono / Processado
                'tratar o retorno quando for sincrono
                'TODO: nao utilizado no momento, ainda falta testar
                FN4Common.Geral.tratar_retorno_xml(nota, xmlElementRetorno, empresa, 1)
            Else
                'retorno nao identificado, rejeitar nota
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
    Public Sub consultar_status()
        Dim ufs_verificar As List(Of empresaVO) = empresaDAO.obterUfsContingencia()
        Try
            For Each ufs In ufs_verificar

                'criar um consStatServ
                Dim consStatServ As New XmlDocument
                consStatServ.Load(System.AppDomain.CurrentDomain.BaseDirectory() & "XML\consStatServ.xml")
                consStatServ.PreserveWhitespace = True
                'acertar o ambiente
                Dim amb_geral As Integer = Geral.Parametro("tp_amb")

                If amb_geral = 2 Then '2 é produção no Fn4 -> mudar para 1 (Procucao na Sefaz)
                    amb_geral = 1
                Else
                    amb_geral = 2
                End If
                'tipo do ambiente
                consStatServ.SelectSingleNode("/*[local-name()='consStatServ' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='tpAmb' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText = amb_geral

                'UF
                consStatServ.SelectSingleNode("/*[local-name()='consStatServ' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='cUF' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText = FN4Common.Helpers.UFs.ListaDeCodigos(ufs.uf)

                'carrega o cabecalho da mensagem
                Dim cabecMsg As XmlDocument = XmlHelper.obterUmCabecalho(consStatServ.SelectSingleNode("/*[local-name()='consStatServ' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/@versao").InnerText)

                Dim retorno As XmlElement
                Dim ws As New FN4EnvioCtl.NFe.ConsultaServicos1.NfeStatusServico2()

                Dim ufWs As String
                ufWs = ""
                If UfsCont.SVCAN.Contains(ufs.uf) Then
                    ufWs = "SVCAN"
                ElseIf UfsCont.SVCRS.Contains(ufs.uf) Then
                    ufWs = "SVCRS"
                End If

                If amb_geral = 1 Then '1 é produção, colocar 0 para achar o WS
                    amb_geral = 0
                Else
                    amb_geral = 1
                End If

                Dim webservice = webserviceDAO.obterURLWebservice(
                   ufWs,
                   "NfeStatusServico",
                   Geral.Parametro("VersaoProduto"),
                   amb_geral)

                Dim certificado = Geral.ObterCertificadoPorEmpresa(ufs.idEmpresa)

                ws.Url = webservice.url
                ws.ClientCertificates.Add(certificado)
                ws.nfeCabecMsgValue = New FN4EnvioCtl.NFe.ConsultaServicos1.nfeCabecMsg
                ws.nfeCabecMsgValue.cUF = FN4Common.Helpers.UFs.ListaDeCodigos(ufs.uf)
                ws.nfeCabecMsgValue.versaoDados = cabecMsg.InnerText

                Try
                    Log.registrarInfo("Buscando status da contingencia, estado  " & ufs.uf & " com problemas de conexao - URL " & ws.Url, "Contingencia")
                    retorno = ws.nfeStatusServicoNF2(consStatServ)
                    ws.Dispose()
                    ws = Nothing
                Catch ex As Exception
                    Log.registrarInfo("Erro na consulta de Status! - URL " & ws.Url, "Contingencia")
                    Throw ex
                End Try

                Dim xmlRetorno As New XmlDocument
                Dim stringWriter As New StringWriter()
                Dim xmlTextWriter As New XmlTextWriter(stringWriter)

                retorno.WriteTo(xmlTextWriter)

                Dim strRetorno = stringWriter.ToString()

                Dim xmlstatusproc As New XmlDocument
                xmlstatusproc.LoadXml(strRetorno)
                xmlstatusproc.PreserveWhitespace = True

                Dim resultado_cstat As String = xmlstatusproc.GetElementsByTagName("cStat")(0).InnerXml
                Dim resultado_motivo As String = xmlstatusproc.GetElementsByTagName("xMotivo")(0).InnerXml

                Log.registrarInfo("Resultado da consulta - Cstat (" & resultado_cstat & ") - Info - " & resultado_motivo, "Contingencia")

                'identifica ambiente emissor normal do cliente
                If UfsSemWebServices.SVAN.Contains(ufs.uf) Then
                    ufWs = "SVAN"
                ElseIf UfsSemWebServices.SVRS.Contains(ufs.uf) Then
                    ufWs = "SVRS"
                Else
                    ufWs = ufs.uf
                End If

                If resultado_cstat = "107" Or resultado_cstat = "113" Then 'se o serviço estiver em operação ou a Desativar, ativar o envio de todas as notas para contingencia (51)
                    'Trocar todas as notas do estado X para 51
                    notaDAO.alterar_notas_contingencia(ufs.uf, "50", "5")

                    'Ativar o webservice para contingencia
                    webserviceDAO.alterarWebserviceContingencia(ufWs, 1)
                    Exit For
                Else
                    'serviço não está em operação ou outro erro
                    'tentar enviar pelo ambiente normal as notas novamente
                    'Trocar todas as notas do estado 5 para 0
                    notaDAO.alterar_notas_contingencia(ufs.uf, "0", "5")
                    'Desativar o webservice do estado X da contingencia
                    webserviceDAO.alterarWebserviceContingencia(ufWs, 0)
                    Exit For
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub gerarAnexo_old(ByVal nota As notaVO, ByVal protnfe As XmlDocument)
        Try
            Dim nfe As New XmlDocument

            'carrega os arquivos

            nfe.Load(nota.pastaDeTrabalho & nota.NFe_ide_nNF & "_assinado.xml")
            nfe.PreserveWhitespace = True

            protnfe.PreserveWhitespace = True

            Dim proc As New XmlDocument
            proc.Load(System.AppDomain.CurrentDomain.BaseDirectory & "XML\procNFe.xml")
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