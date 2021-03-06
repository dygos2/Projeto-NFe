﻿Imports System.Web.Services
Imports System.ComponentModel
Imports FN4Common
Imports FN4Common.Helpers

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://fisconet4.com/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class FN4ConsultaStatusService
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function obterNota(ByVal nNF As Integer, ByVal CNPJ As String, ByVal serie As Integer, ByVal token As String) As statusNotaVO
        Dim empresa As empresaVO = empresaDAO.obterEmpresa(CNPJ, String.Empty)

        If empresa Is Nothing Then Return Nothing

        If Not Seguranca.CompararMD5(empresa.validador, token) Then Return Nothing
        
        Try
            Dim ht As New Hashtable
            ht.Add("numeroDaNota", nNF)
            ht.Add("CNPJEmitente", CNPJ)
            ht.Add("serie", serie)
            Dim x As notaVO

            x = IBatisNETHelper.Instance.QueryForObject("obterNotaCompletaPorNumeroCNPJ", ht)
            Dim retorno As New statusNotaVO(x)
            x = Nothing

            Return retorno
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

End Class