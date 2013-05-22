Imports System.Web.Services
Imports System.ComponentModel

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://fisconet4.com/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class Service1
    Inherits System.Web.Services.WebService

    <WebMethod()> _
Public Function obterStatusAtualDaNota(ByVal nNF As Integer) As StatusNotaVO
        Try
            Dim x As StatusNotaVO
            'x = IBatisNETHelper.Instance.QueryForObject("obterNotaCompletaPorNumero", nNF)
            Return x
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    <WebMethod()> _
Public Function obterHistoricoDaNota(ByVal nNF As Integer) As List(Of StatusNotaVO)
        Try
            Dim x As List(Of StatusNotaVO)
            '  x = IBatisNETHelper.Instance.QueryForObject("obterNotaCompletaPorNumero", nNF)
            Return x
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class