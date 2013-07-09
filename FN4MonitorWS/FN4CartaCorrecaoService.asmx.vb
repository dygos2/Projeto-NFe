Imports System.Web.Services
Imports System.Web.Services.Protocols
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
Public Class FN4CartaCorrecaoService
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function adicionarCartaDeCorrecao(ByVal NFe_emit_CNPJ As String, ByVal TOKEN As String, ByVal nfe_infNfe_id As String, ByVal xCorrecao As String) As Integer

        Dim empresa As empresaVO = empresaDAO.obterEmpresa(NFe_emit_CNPJ, String.Empty)
        If empresa Is Nothing Then Return Nothing
        Dim evento As New eventoVO

        If Seguranca.CompararMD5(empresa.validador, TOKEN) Then
            evento.NFe_infNFe_id = nfe_infNfe_id
            evento.infEvento_detEvento_xCorrecao = xCorrecao.Trim()
            evento.NFe_emit_CNPJ = nfe_infNfe_id.Substring(6, 14)

            evento.infEvento_tpEvento = 110110
            evento.statusEvento = 0

            Return eventos.inserirEvento(evento)
        Else
            Return Nothing
        End If

    End Function

    <WebMethod()> _
    Public Function obterCartaDeCorrecao(ByVal infEvento_nSeqEvento As Integer, ByVal nfe_infNfe_id As String, ByVal CNPJ As String, ByVal TOKEN As String) As FN4Common.eventoVO
        Dim empresa As empresaVO = empresaDAO.obterEmpresa(CNPJ, String.Empty)

        If empresa Is Nothing Then Return Nothing

        If Seguranca.CompararMD5(empresa.validador, TOKEN) Then
            Return eventos.obterEvento(infEvento_nSeqEvento, nfe_infNfe_id, 110110)
        Else
            Return Nothing
        End If

    End Function

    <WebMethod()> _
    Public Function obterListaDeEventos(ByVal NFe_emit_CNPJ As String, ByVal NFe_infNFe_id As String, ByVal TOKEN As String) As listaDeEventoVO

        Dim empresa As empresaVO = empresaDAO.obterEmpresa(NFe_emit_CNPJ, String.Empty)
        If empresa Is Nothing Then Return Nothing

        If Seguranca.CompararMD5(empresa.validador, TOKEN) Then
            Dim listaeventos As New listaDeEventoVO
            listaeventos.ListaDeEventos = eventos.obterListaDeEventos(NFe_emit_CNPJ, NFe_infNFe_id)
            Return listaeventos
        Else
            Return Nothing
        End If
    End Function

End Class