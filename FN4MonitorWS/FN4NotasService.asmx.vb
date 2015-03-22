Imports System.Web.Services
Imports System.ComponentModel
Imports FN4Common.DataAccess
Imports FN4Common
Imports System.Xml
Imports FN4Common.Helpers

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://fisconet4.com/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class FN4NotasService
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function obterListaDeNotas(ByVal status As String, ByVal registroInicial As Integer, ByVal registrosPorPagina As Integer, ByVal cnpj As String, ByVal token As String) As listaDeNotasVO

        Dim empresa As empresaVO = empresaDAO.obterEmpresa(cnpj, String.Empty)

        If empresa Is Nothing Then Return Nothing

        If Not Seguranca.CompararMD5(empresa.validador, token) Then Return Nothing

        Dim x As New listaDeNotasVO

        Dim ht As New Hashtable
        ht.Add("status", status)
        ht.Add("registroInicial", registroInicial)
        ht.Add("registrosPorPagina", registrosPorPagina)
        ht.Add("cnpj", cnpj)
        Try
            x.listaDeNotas = IBatisNETHelper.Instance.QueryForList(Of notaVO)("obterNotasPorStatus", ht)
            x.quantidadeDeRegistros = IBatisNETHelper.Instance.QueryForObject("obterQuantidadeDeNotas2", ht)
        Catch ex As Exception
            Throw ex
        End Try
        Return x
    End Function

    <WebMethod()> _
    Public Function obterUltimasAtualizacoes(ByVal registroInicial As Integer, ByVal registrosPorPagina As Integer, ByVal cnpj As String, ByVal token As String) As List(Of atualizacaoVO)
        Try
            Dim empresa As empresaVO = empresaDAO.obterEmpresa(cnpj, String.Empty)

            If empresa Is Nothing Then Return Nothing

            If Not Seguranca.CompararMD5(empresa.validador, token) Then Return Nothing

            Dim x As New List(Of FN4Common.atualizacaoVO)

            Dim ht As New Hashtable
            ht.Add("registroInicial", registroInicial)
            ht.Add("registrosPorPagina", registrosPorPagina)
            ht.Add("cnpj", cnpj)

            x = IBatisNETHelper.Instance.QueryForList(Of atualizacaoVO)("obterUltimasAtualizacoes", ht)

            Return x
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    <WebMethod()> _
    Public Function buscarNotas(ByVal tipoDeBusca As Integer, ByVal parametro As String, ByVal registroInicial As Integer, ByVal registrosPorPagina As Integer, ByVal cnpj As String, ByVal token As String) As listaDeNotasVO
        'Dim x As New List(Of FN4Common.atualizacaoVO)

        'Dim ht As New Hashtable
        'ht.Add("registroInicial", registroInicial)
        'ht.Add("registrosPorPagina", registrosPorPagina)

        'x = IBatisNETHelper.Instance.QueryForList(Of FN4Common.atualizacaoVO)("obterUltimasAtualizacoes", ht)

        'Return x

        '0-CNPJ
        '1-CPF
        '2-Numero da nota
        '3-UF de destino
        '4-Nome/Razao social
        '5-Email destinatário
        '6-Status da nota (pelo ID)

        Dim empresa As empresaVO = empresaDAO.obterEmpresa(cnpj, String.Empty)

        If empresa Is Nothing Then Return Nothing

        If Not Seguranca.CompararMD5(empresa.validador, token) Then Return Nothing

        Dim nota As New notaVO
        Select Case tipoDeBusca
            Case Is = 0
                nota.NFe_dest_CNPJ = parametro
            Case Is = 1
                nota.NFe_dest_CPF = parametro
            Case Is = 2
                nota.NFe_ide_nNF = parametro
            Case Is = 3
                nota.NFe_dest_UF = parametro
            Case Is = 4
                nota.NFe_dest_xNome = parametro
            Case Is = 5
                nota.emailDestinatario = parametro
            Case Is = 6
                nota.statusDaNota = parametro
            Case Else
                Return Nothing
        End Select

        nota.NFe_emit_CNPJ = empresa.cnpj

        If nota.NFe_ide_nNF = 0 Then
            nota.NFe_ide_nNF = -1
        End If

        If nota.statusDaNota = 0 Then
            nota.statusDaNota = -1
        End If


        Dim ht As New Hashtable
        ht.Add("registroInicial", registroInicial)
        ht.Add("registrosPorPagina", registrosPorPagina)
        ht.Add("nota", nota)

        Try
            Dim x As New listaDeNotasVO
            x.quantidadeDeRegistros = IBatisNETHelper.Instance.QueryForObject("obterQuantidadeDeNotas", ht)

            x.listaDeNotas = IBatisNETHelper.Instance.QueryForList(Of notaVO)("buscarNotas", ht)
            Return x
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    <WebMethod()> _
    Public Function obterNota(ByVal nNF As Integer, ByVal CNPJ As String, ByVal serie As Integer, ByVal token As String) As notaVO
        Try
            Dim empresa As empresaVO = empresaDAO.obterEmpresa(CNPJ, String.Empty)

            If empresa Is Nothing Then Return Nothing

            If Not Seguranca.CompararMD5(empresa.validador, token) Then Return Nothing

            Dim ht As New Hashtable
            ht.Add("numeroDaNota", nNF)
            ht.Add("CNPJEmitente", CNPJ)
            ht.Add("serie", serie)
            Dim x As notaVO
            x = IBatisNETHelper.Instance.QueryForObject("obterNotaCompletaPorNumeroCNPJ", ht)
            Return x
        Catch ex As Exception
            Log.registrarErro("Erro a obter nota." & ex.Message, "WebService")
            Throw ex
        End Try
    End Function
    <WebMethod()> _
    Public Function obterNota_atualizacoes(ByVal nNF As Integer, ByVal CNPJ As String, ByVal serie As Integer, ByVal token As String) As notaVO
        Try
            Dim empresa As empresaVO = empresaDAO.obterEmpresa(CNPJ, String.Empty)

            If empresa Is Nothing Then Return Nothing

            If Not Seguranca.CompararMD5(empresa.validador, token) Then Return Nothing

            Dim ht As New Hashtable
            ht.Add("numeroDaNota", nNF)
            ht.Add("CNPJEmitente", CNPJ)
            ht.Add("serie", serie)
            Dim x As notaVO
            x = IBatisNETHelper.Instance.QueryForObject("obterNotaAtualizacoesPorNumeroCNPJ", ht)
            Return x
        Catch ex As Exception
            Log.registrarErro("Erro a obter nota." & ex.Message, "WebService")
            Throw ex
        End Try
    End Function
    <WebMethod()> _
    Public Function cancelarNota(ByVal nNf As Integer, ByVal serie As Integer, ByVal justificativa As String, ByVal CNPJ As String, ByVal token As string) As String
        Try
            Dim empresa As empresaVO = empresaDAO.obterEmpresa(CNPJ, String.Empty)
            Dim evento As New eventoVO
            If empresa Is Nothing Then Return Nothing

            If Not Seguranca.CompararMD5(empresa.validador, token) Then Return "Erro de segurança: Token inválido."

            Dim nota As notaVO
            Dim retorno As String

            Dim ht As New Hashtable
            ht.Add("numeroDaNota", nNf)
            ht.Add("serie", serie)
            ht.Add("CNPJEmitente", CNPJ)

            nota = notas.obterNota(nNf, CNPJ, serie)

            'cancelar
            retorno = cancelarNFe(nota, justificativa, empresa)

            Return retorno

        Catch ex As Exception
            Log.registrarErro("Erro de cancelamento - " & ex.Message & vbCrLf & ex.StackTrace, "CancelamentoService")
            Return "Ocorreu um erro no cancelamento, contate o suporte. (" & ex.Message & "-" & ex.StackTrace & ")"
        End Try
    End Function

    <WebMethod()> _
    Public Function solicitarImpressao(ByVal nNf As Integer, ByVal serie As Integer, ByVal CNPJ As String, ByVal token As String, ByVal quantidade As Integer) As Integer
        Try
            Dim empresa As empresaVO = empresaDAO.obterEmpresa(CNPJ, String.Empty)

            If empresa Is Nothing Then Return Nothing

            If Not Seguranca.CompararMD5(empresa.validador, token) Then Return Nothing

            Dim nota As notaVO

            nota = notas.obterNota(nNf, CNPJ, serie)

            If nota Is Nothing Then
                Return 0
            End If

            If nota.statusDaNota <> 21 And nota.statusDaNota <> 22 Then
                Return 0
            End If

            nota.impressaoSolicitada = nota.impressaoSolicitada + quantidade

            notas.alterarNota(nota)

            Return 1
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Public Function solicitarHelpDesk(ByVal nNF As Integer, ByVal nome As String, ByVal email As String, ByVal CNPJ As String) As retornoVO
        Dim retorno As retornoVO
        Try
            Dim ht As New Hashtable
            ht.Add("numeroDaNota", nNF)
            ht.Add("CNPJEmitente", CNPJ)

            Dim nota As notaVO
            nota = IBatisNETHelper.Instance.QueryForObject("obterNotaPorNumeroCNPJ", ht)
            If System.IO.File.Exists(nota.pastaDeTrabalho & nota.NFe_ide_nNF & "_assinado.xml") Then
                Dim xmlenvio As New XmlDocument
                xmlenvio.Load(nota.pastaDeTrabalho & nota.NFe_ide_nNF & "_assinado.xml")

                Dim Ret As String

                Dim ws As New FN4.HelpDesk.RecepcaoService
                Ret = ws.ProcessarRequisicao(xmlenvio.InnerXml, nota.NFe_emit_CNPJ, nome, email)
                Retorno = New retornoVO(Ret)

                Return Retorno
            Else
                retorno = New retornoVO
                retorno.numeroMensagem = 4
                retorno.mensagem = "Arquivo XML não encontrado, entre em contato com o Administrador do sistema."
                retorno.tipoDeRetorno = 1
            End If

        Catch ex As Exception
            retorno = New retornoVO
            retorno.numeroMensagem = 4
            retorno.mensagem = "Houve um erro na conexão com o HelpDesk"
            retorno.tipoDeRetorno = 1
        End Try

        Return retorno
    End Function

    <WebMethod()> _
    Public Function solicitarInutilizacao(ByVal inicio As Integer, ByVal fim As Integer, ByVal justificativa As String, ByVal CNPJ As String, ByVal token As String, ByVal serie As String) As String
        Try
            Dim empresa As empresaVO = empresaDAO.obterEmpresa(CNPJ, String.Empty)

            If empresa Is Nothing Then Return Nothing

            If Not Seguranca.CompararMD5(empresa.validador, token) Then Return Nothing

            Dim consultaDeNotas As List(Of notaVO) = notas.obterNotasPorIntervalo(inicio, fim, CNPJ, serie)

            Dim notasInvalidasParaInutilizacao = (From n In consultaDeNotas Where n.statusDaNota <> 3 Select n).ToList()

            If notasInvalidasParaInutilizacao.Count > 0 Then
                Dim numeroDasNotasInvalidas As String = String.Empty

                For Each nota In notasInvalidasParaInutilizacao
                    If String.IsNullOrEmpty(numeroDasNotasInvalidas) Then
                        numeroDasNotasInvalidas = nota.NFe_ide_nNF
                    Else
                        numeroDasNotasInvalidas = numeroDasNotasInvalidas & ", " & nota.NFe_ide_nNF
                    End If
                Next

                Dim mensagem As String

                If notasInvalidasParaInutilizacao.Count > 1 Then
                    mensagem = "O intervalo de notas informado é inválido porque as notas com os IDs: " & numeroDasNotasInvalidas & " não podem ser inutilizadas."
                Else
                    mensagem = "O intervalo de notas informado é inválido porque a nota de ID: " & numeroDasNotasInvalidas & " não podem ser inutilizada."
                End If

                Throw New Exception(mensagem)
            End If


            'insere cada nota a ser inutilizada na tabela de notas.
            For i = inicio To fim
                Dim nota As New notaVO(i)
                nota.NFe_emit_CNPJ = CNPJ
                nota.serie = serie
                nota.statusDaNota = 61
                Try
                    DataAccess.notas.inserirNota(nota)
                Catch ex As Exception When ex.Message.Contains("Duplicate entry")
                    Dim notaExistente As notaVO = notas.obterNota(i, CNPJ, serie)
                    notaExistente.statusDaNota = 61
                    notas.alterarNota(notaExistente)
                End Try

                Dim just As New justificativaVO()
                just.cnpj = CNPJ
                just.descricao = justificativa
                just.idNota = i
                just.serie = serie

                justificativas.inserirJustificativa(just)
            Next

            Return "Notas inseridas na fila de inutilização com sucesso."
        Catch ex As Exception
            Log.registrarErro("Ocorreu um erro na inutilizacao: " & ex.Message & ex.StackTrace, "InutilizacaoService")
            Return "Ocorreu um erro na inutilizacao: " & vbCrLf & ex.Message & vbCrLf & ex.StackTrace
        End Try
    End Function

    <WebMethod()> _
    Public Function solicitarReenvio(ByVal nNF As Integer, ByVal CNPJ As String, ByVal serie As Integer, ByVal token As String) As String
        Try
            Dim empresa As empresaVO = empresaDAO.obterEmpresa(CNPJ, String.Empty)

            If empresa Is Nothing Then Return Nothing

            If Not Seguranca.CompararMD5(empresa.validador, token) Then Return "Erro: token inválido."

            Dim nota As notaVO = obterNota(nNF, CNPJ, serie, token)

            nota.statusDaNota = 0

            IBatisNETHelper.Instance.Update("alterarNota", nota)

            inserirHistorico(11, "Nota colocada na fila de envio novamente.", nota)
            Return "Solicitado o Reenvio da nota com sucesso!"

        Catch ex As Exception
            Log.registrarErro("Ocorreu um erro na solicitação de reenvio: " & ex.Message & ex.StackTrace, "EnvioService")
            Return "Ocorreu um erro na solicitação de reenvio," & ex.Message & " contate o suporte."
        End Try
    End Function
    <WebMethod()> _
    Public Function solicitarConsulta(ByVal nNF As Integer, ByVal CNPJ As String, ByVal serie As Integer, ByVal token As String) As String
        Try
            Dim empresa As empresaVO = empresaDAO.obterEmpresa(CNPJ, String.Empty)

            If empresa Is Nothing Then Return Nothing

            If Not Seguranca.CompararMD5(empresa.validador, token) Then Return "Erro: token inválido."

            Dim nota As notaVO = obterNota(nNF, CNPJ, serie, token)

            nota.statusDaNota = 19

            IBatisNETHelper.Instance.Update("alterarNota", nota)

            inserirHistorico(30, "Solicitação pelo usuário", nota)
            Return "Solicitado a consulta da nota com sucesso!"

        Catch ex As Exception
            Log.registrarErro("Ocorreu um erro na solicitação da consulta: " & ex.Message & ex.StackTrace, "EnvioService")
            Return "Ocorreu um erro na solicitação da consulta," & ex.Message & " contate o suporte."
        End Try
    End Function
    <WebMethod()>
    Public Function AlterarEmailEReenviar(ByVal cnpj As String, ByVal token As String, ByVal nnf As Integer, ByVal serie As Integer, ByVal novoEmail As String) As Integer
        Try
            Dim empresa As empresaVO = empresaDAO.obterEmpresa(cnpj, String.Empty)

            If empresa Is Nothing Then Return Nothing

            If Not Seguranca.CompararMD5(empresa.validador, token) Then Return 1

            Dim nota As notaVO = obterNota(nnf, cnpj, serie, token)

            If nota Is Nothing Then Return 1

            Dim antigoEmail As String = nota.emailDestinatario

            nota.emailDestinatario = novoEmail

            If nota.statusDaNota = 22 Then
                nota.statusDaNota = 21
            End If

            notas.alterarNota(nota)

            Dim historico As New historicoVO(18, "Email de destino da nota alterado de " & antigoEmail & " para " & novoEmail, nnf, cnpj, serie)

            notas.inserirHistorico(historico)

            Return 0

        Catch ex As Exception
            Log.registrarErro("Ocorreu um erro na solicitação de reenvio" & ex.Message & " - " & ex.StackTrace, "EnvioService")
            Return 1
        End Try
    End Function

    <WebMethod()>
    Public Function obterNotasASeremInutilizadas(ByVal cnpj As String, ByVal token As String) As List(Of serieVO)
        Dim empresa As empresaVO = empresaDAO.obterEmpresa(cnpj, String.Empty)

        If empresa Is Nothing Then Return Nothing

        If Not Seguranca.CompararMD5(empresa.validador, token) Then Return Nothing

        Dim listaSeries As List(Of serieVO)
        Dim numerosFaltando As List(Of Integer)
        Dim notasExistentesASeremInutilizadas As List(Of Integer)
        Dim seriesResultado As New List(Of serieVO)

        Try
            'obtem as séries de nota existentes no banco (somente os números)
            listaSeries = DataAccess.series.obterSeriesPorCnpj(cnpj)

            For Each serie In listaSeries
                'obtém as notas da série corrente
                serie.obterNotas(cnpj)

                Dim strNumeros As String = String.Empty

                'transforma os numeros de notas da série corrente num string separado por vírgulas (aceito pelo método de encontrar números faltando)
                For Each numero In serie.notas
                    If strNumeros.Equals(String.Empty) Then
                        strNumeros = numero
                    Else
                        strNumeros = strNumeros & "," & numero
                    End If
                Next

                serie.notas.RemoveRange(0, serie.notas.Count)

                'obtém os números faltando na sequência de notas
                numerosFaltando = Geral.FindMissingNumbers(strNumeros)
                notasExistentesASeremInutilizadas = serie.obterNotasExistentesASeremInutilizadas(cnpj)

                If Not notasExistentesASeremInutilizadas Is Nothing And notasExistentesASeremInutilizadas.Count > 0 Then
                    serie.notas.AddRange(notasExistentesASeremInutilizadas)
                End If

                If numerosFaltando.Count > 0 Then
                    serie.notas.AddRange(numerosFaltando)
                End If

                If serie.notas.Count > 0 Then
                    serie.notas = serie.notas.OrderBy(Function(n) n).ToList()

                    seriesResultado.Add(serie)
                End If
            Next

            Return seriesResultado
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Function inutilizarNotas(ByVal inicio As Integer, ByVal fim As Integer, ByVal CNPJ As String, ByVal UF As String, ByVal textoJustificativa As String, ByVal serie As Integer) As String

        Dim inutNFe As New XmlDocument
        inutNFe.Load(Server.MapPath(".") & "/XML/inutNFe.xml")

        Dim cabecMsg As New System.Xml.XmlDocument
        cabecMsg.Load(Server.MapPath(".") & "/XML/cabecMsg.xml")


        inutNFe.SelectSingleNode("/*[local-name()='inutNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infInut' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='tpAmb' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText = Geral.Parametro("ambienteDeExecucao")
        inutNFe.SelectSingleNode("/*[local-name()='inutNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infInut' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='cUF' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText = UF
        inutNFe.SelectSingleNode("/*[local-name()='inutNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infInut' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='ano' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText = DateTime.Now.ToString("yy")
        inutNFe.SelectSingleNode("/*[local-name()='inutNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infInut' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='CNPJ' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText = CNPJ
        inutNFe.SelectSingleNode("/*[local-name()='inutNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infInut' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='nNFIni' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText = inicio
        inutNFe.SelectSingleNode("/*[local-name()='inutNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infInut' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='nNFFin' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText = fim
        inutNFe.SelectSingleNode("/*[local-name()='inutNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infInut' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='xJust' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText = textoJustificativa
        inutNFe.SelectSingleNode("/*[local-name()='inutNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infInut' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='serie' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText = serie.ToString


        'colocar o ID (UF, CNPJ, modelo, serie, inicial e final)
        inutNFe.SelectSingleNode("/*[local-name()='inutNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infInut' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/@Id").InnerText = obterIDInutilizacao(inutNFe)

        inutNFe = XmlHelper.assinarNFeXML(inutNFe, inutNFe.GetElementsByTagName("infInut")(0).Attributes.ItemOf("Id").InnerText)

        Dim ws As New NFe.Inutilizacao.NfeInutilizacao2
        ws.Url = Geral.Parametro("servicoInutilizacao")

        Dim empresa = empresaDAO.obterEmpresa(CNPJ, String.Empty)

        Dim webservice = webserviceDAO.obterURLWebservice(UF, "NFeInutilizacao", Geral.Parametro("VersaoProduto"), empresa.homologacao)

        If webservice Is Nothing Then
            Throw New Exception("Webservice de inutilização não localizado.")
        Else
            ws.Url = webservice.url
        End If

        ws.ClientCertificates.Add(Geral.Certificado)
        ws.nfeCabecMsgValue = New NFe.Inutilizacao.nfeCabecMsg
        ws.nfeCabecMsgValue.versaoDados = cabecMsg.InnerText
        ws.nfeCabecMsgValue.cUF = Repositorios.UFs.ListaDeCodigos(UF)

        Dim strRetorno As String

        Try
            Log.registrarInfo("Enviando inutilizacao: " & vbCrLf & inutNFe.InnerXml, "InutilizacaoService")

            'envia
            Dim xmlRetornoWs As XmlNode = ws.nfeInutilizacaoNF2(inutNFe)
            strRetorno = xmlRetornoWs.InnerXml
            ws.Dispose()
            ws = Nothing
            Log.registrarInfo("Recebido o retorno: " & vbCrLf & strRetorno, "InutilizacaoService")

            notas.inutilizarNotas(empresa.cnpj, serie, inicio, fim)

            Dim justificativa As justificativaVO

            For i As Integer = inicio To fim
                justificativa = New justificativaVO
                justificativa.cnpj = empresa.cnpj
                justificativa.descricao = textoJustificativa
                justificativa.idNota = i
                justificativa.serie = serie

                justificativas.inserirJustificativa(justificativa)
            Next
        Catch ex As Exception

            Throw ex
        End Try

        Dim xmlRetorno As New XmlDocument
        xmlRetorno.LoadXml(strRetorno.Replace("xmlns=""http://www.portalfiscal.inf.br/nfe""", ""))

        Return xmlRetorno.SelectSingleNode("/retInutNFe/infInut/xMotivo").InnerText

    End Function

    Private Function cancelarNFe(ByVal nota As notaVO, ByVal textoJustificativa As String, ByVal empresa_obj As empresaVO) As String
        Try

            Dim evento As New eventoVO

            evento.NFe_infNFe_id = nota.NFe_infNFe_id
            evento.infEvento_detEvento_xCorrecao = textoJustificativa.Trim()
            evento.NFe_emit_CNPJ = nota.NFe_emit_CNPJ
            evento.infEvento_tpEvento = 110111
            evento.statusEvento = 0

            eventos.inserirEvento(evento)
            inserirHistorico(19, "Nota enviada para fila de eventos", nota)

            Return "Nota Enviada para fila de Processamento"
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#Region "Acessorios"

    Private Sub inserirHistorico(ByVal tipo As String, ByVal descricao As String, ByVal nota As notaVO)
        Dim hist As New historicoVO(tipo, descricao, nota.NFe_ide_nNF, nota.NFe_emit_CNPJ, nota.serie)
        IBatisNETHelper.Instance.Insert("inserirHistorico", hist)

        Retorno.escreverRetorno(nota, DateTime.Now, tipo, descricao)
    End Sub

    Private Function obterIDInutilizacao(ByVal inutnFe As XmlDocument) As String
        Dim ID As String = ""
        ID += "ID" & inutnFe.SelectSingleNode("/*[local-name()='inutNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infInut' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='cUF' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText
        ID += inutnFe.SelectSingleNode("/*[local-name()='inutNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infInut' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='CNPJ' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText
        ID += inutnFe.SelectSingleNode("/*[local-name()='inutNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infInut' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='mod' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText
        ID += Format(CInt(inutnFe.SelectSingleNode("/*[local-name()='inutNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infInut' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='serie' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText), "000")
        ID += Format(CInt(inutnFe.SelectSingleNode("/*[local-name()='inutNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infInut' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='nNFIni' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText), "000000000")
        ID += Format(CInt(inutnFe.SelectSingleNode("/*[local-name()='inutNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infInut' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='nNFFin' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText), "000000000")

        Return ID
    End Function

#End Region
End Class