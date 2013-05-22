Imports System.Web.Services
Imports System.ComponentModel
Imports FN4Common
Imports FN4Common.Helpers

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class FN4ConfiguracaoService
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function obterConfiguracoes(ByVal cnpj As String, ByVal token As String) As configuracoesVO
        Dim empresa As empresaVO = empresaDAO.obterEmpresa(cnpj, String.Empty)

        If empresa Is Nothing Then Return Nothing

        If Not Seguranca.CompararMD5(empresa.validador, token) Then Return Nothing

        If empresa Is Nothing Then Return Nothing

        Dim document As New System.Xml.XmlDocument
        document.Load(Server.MapPath(".") & "/XML/MonitorConfig.xml")
        Dim config As New configuracoesVO
        With config
            .gadget1 = empresa.gadget1.ToString()
            .gadget2 = empresa.gadget2.ToString()
            .gadget3 = empresa.gadget3.ToString()
        End With
        Return config

    End Function

    <WebMethod()> _
    Public Sub salvarConfiguracoes(ByVal cnpj As String, ByVal token As String, _
       ByVal gadget1 As Integer, ByVal gadget2 As Integer, _
       ByVal gadget3 As Integer)

        Dim empresa As empresaVO = empresaDAO.obterEmpresa(cnpj, String.Empty)

        If empresa Is Nothing Then Return

        If Not Seguranca.CompararMD5(empresa.validador, token) Then Return

        empresa.gadget1 = gadget1
        empresa.gadget2 = gadget2
        empresa.gadget3 = gadget3

        empresaDAO.alterarGadgets(empresa)
    End Sub

End Class