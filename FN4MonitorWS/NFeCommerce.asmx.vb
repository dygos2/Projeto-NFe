Imports System.Web.Services
Imports System.ComponentModel
Imports FN4Common.DataAccess
Imports FN4Common
Imports FN4Common.Helpers
Imports System.IO
Imports FN4MonitorWS.FN4MonitorWS.NFeCommerce

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class NFeCommerce
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function EnviarNota(ByVal nota As String, ByVal cnpj As String, ByVal token As String, ByVal serie As Integer, ByVal nNF As Integer) As String
        Try
            Dim empresa = empresaDAO.obterEmpresa(cnpj, String.Empty)

            If empresa Is Nothing Then
                Throw New Exception("Empresa não localizada")
            End If

            If Not Seguranca.CompararMD5(empresa.validador, token) Then
                Throw New Exception("Token inválido")
            End If

            Dim pastaIn = Geral.Parametro("pastaEntrada")
            Dim nomeTxt = cnpj & "-" & nNF & "-" & serie & ".txt"

            Dim linhas As String() = nota.Split(vbLf)

            Dim pathTxt = Path.Combine(pastaIn, nomeTxt)

            Dim arquivoTexto As New StreamWriter(pathTxt)

            For Each linha As String In linhas
                arquivoTexto.WriteLine(linha)
            Next

            arquivoTexto.Close()

            Return "OK"
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    <WebMethod()>
    Public Function VerificarStatus(ByVal cnpj As String, ByVal token As String, ByVal serie As Integer, ByVal numero As Integer) As statusDaNota
        Dim retorno As New statusDaNota()

        Try
            Dim empresa = empresaDAO.obterEmpresa(cnpj, String.Empty)

            If empresa Is Nothing Then
                Throw New Exception("Empresa não localizada")
            End If

            If Not Seguranca.CompararMD5(empresa.validador, token) Then
                Throw New Exception("Token inválido")
            End If

            Dim nota As notaVO = notas.obterNota(numero, cnpj, serie)

            If nota Is Nothing Then
                Throw New Exception("Nota não encontrada")
            End If

            retorno.numero = nota.NFe_ide_nNF
            retorno.serie = nota.serie
            retorno.cnpj = nota.NFe_emit_CNPJ
            retorno.dataUltimaAtualizacao = nota.dataUltimaAtualizacao

            retorno.status = nota.statusDaNota
            retorno.cStat = nota.retEnviNFe_cStat
            retorno.motivo = nota.retEnviNFe_xMotivo
            retorno.chave = nota.NFe_infNFe_id

            Dim tp_sys = configuracaoDAO.obterConfiguracao("tp_sys", empresa.idEmpresa)
            Dim pasta_trab_arr As Array

            Dim urlDanfe = Geral.Parametro("urlDanfe")
            Dim anoMes = nota.NFe_ide_dEmi.ToString("yyyy") & nota.NFe_ide_dEmi.ToString("MM")

            'Se tiver tp_sys na tabela de configurações do cliente, (Cliente contratou envio do PDF)
            If Not tp_sys Is Nothing Then
                urlDanfe = urlDanfe & nota.NFe_emit_CNPJ & "/" & anoMes & "/" & nota.NFe_infNFe_id & ".pdf"
            Else
                pasta_trab_arr = nota.pastaDeTrabalho.Split("\")
                urlDanfe = Geral.Parametro("servidorDanfe") & "cnpj=" & empresa.cnpj & "&ano=" & pasta_trab_arr(5) & "&mes=" & pasta_trab_arr(6) & "&dia=" & pasta_trab_arr(7) & "&nnfe=" & pasta_trab_arr(8) & "&arq=" & nota.NFe_ide_nNF & "_procNFe&ch=" & nota.NFe_infNFe_id & "&srvid=" & Geral.Parametro("srvid") & "&tp_amb=" & empresa.homologacao + 1 & "&dest_saida=D"
            End If

            retorno.urlDanfe = urlDanfe
        Catch ex As Exception
            retorno.status = -1
            retorno.motivo = ex.Message
        End Try

        Return retorno
    End Function

End Class