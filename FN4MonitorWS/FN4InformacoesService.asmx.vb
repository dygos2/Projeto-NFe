Imports System.Web.Services
Imports System.ComponentModel
Imports FN4Common
Imports FN4Common.Helpers

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://fisconet4.com/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class FN4InformacoesService
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function obterInformacoesDoCertificado(ByVal cnpj As String, ByVal token As String) As informacoesCertificadoVO
        Dim empresa As empresaVO = empresaDAO.obterEmpresa(cnpj, String.Empty)

        If empresa Is Nothing Then Return Nothing

        If Not Seguranca.CompararMD5(empresa.validador, token) Then Return Nothing

        If empresa Is Nothing Then Return Nothing

        Dim certificado = Geral.ObterCertificadoPorEmpresa(empresa.idEmpresa)

        Dim cert As New informacoesCertificadoVO(certificado)
        Return cert
    End Function

    <WebMethod()> _
   Public Function obterStatusServico() As statusServicoVO
        Dim retorno As statusServicoVO
        Dim retornoSvc As String
        Dim consStatServ As New System.Xml.XmlDocument
        Dim cabecMsg As New System.Xml.XmlDocument
        Try
            cabecMsg.Load(Server.MapPath(".") & "/XML/cabecMsg.xml")

            consStatServ.Load(Server.MapPath(".") & "/XML/consStatServ.xml")
            consStatServ.SelectSingleNode("/*[local-name()='consStatServ' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='tpAmb' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText = Geral.Parametro("ambienteDeExecucao")


            Dim svc As New NFe.StatusServico.NfeStatusServico
            svc.Url = Geral.Parametro("servicoConsultaStatus")
            svc.ClientCertificates.Add(FN4Common.Geral.Certificado)

            ' svc.Timeout = 1000000

            retornoSvc = svc.nfeStatusServicoNF(cabecMsg.InnerXml, consStatServ.InnerXml)

            Dim xmlretorno As New System.Xml.XmlDocument

            xmlretorno.LoadXml(retornoSvc.Replace("xmlns=""http://www.portalfiscal.inf.br/nfe""", ""))
            Dim cstat As Integer = CInt(xmlretorno.SelectSingleNode("/retConsStatServ/cStat[1]").InnerText)
            Dim xMotivo As String = xmlretorno.SelectSingleNode("/retConsStatServ/xMotivo[1]").InnerText
            retorno = New statusServicoVO(cstat, xMotivo)
            Return retorno
        Catch ex As Exception
            retorno = New statusServicoVO(0, "Erro na conexão com o serviço, " & ex.Message & " " & ex.StackTrace)
            Return retorno
        End Try
    End Function

End Class