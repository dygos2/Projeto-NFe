Imports System.ServiceModel

Public Class AcessaWebService


    Public Shared Function GetVtexService(ByVal strWebServiceUrl As String, ByVal strUser As String, ByVal strPassword As String) As wsVtex.ServiceClient

        Dim hasValidation As Boolean = Not (String.IsNullOrWhiteSpace(strUser)) And Not (String.IsNullOrWhiteSpace(strPassword))

        Dim objBinding As BasicHttpBinding = New BasicHttpBinding

        Dim nDefaultLength As Integer = 2000000

        objBinding.ReaderQuotas.MaxDepth = nDefaultLength
        objBinding.ReaderQuotas.MaxArrayLength = nDefaultLength
        objBinding.ReaderQuotas.MaxBytesPerRead = nDefaultLength
        objBinding.ReaderQuotas.MaxNameTableCharCount = nDefaultLength
        objBinding.ReaderQuotas.MaxStringContentLength = nDefaultLength
        objBinding.MaxReceivedMessageSize = nDefaultLength
        objBinding.MaxBufferPoolSize = nDefaultLength
        objBinding.MaxBufferSize = nDefaultLength

        objBinding.CloseTimeout = New TimeSpan(0, 10, 0)
        objBinding.OpenTimeout = objBinding.CloseTimeout
        objBinding.ReceiveTimeout = objBinding.CloseTimeout
        objBinding.SendTimeout = objBinding.CloseTimeout

        If (hasValidation) Then
            objBinding.Security.Mode = BasicHttpSecurityMode.Transport
            objBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic
        End If

        Dim objServiceClient As wsVtex.ServiceClient = New wsVtex.ServiceClient(objBinding, New EndpointAddress(strWebServiceUrl))

        If (hasValidation) Then
            objServiceClient.ClientCredentials.UserName.UserName = strUser
            objServiceClient.ClientCredentials.UserName.Password = strPassword
        End If

        Return objServiceClient
    End Function

    Sub Main()

        Dim objServiceClient As wsVtex.ServiceClient = GetVtexService("https://webservice-jackvartanian.vtexcommerce.com.br/service.svc", "admin", "vtexsa1")

        Try

            Dim objCLient As wsVtex.ClientDTO = objServiceClient.ClientGet(2)


            Dim objImage As wsVtex.ImageDTO = New wsVtex.ImageDTO()
            objImage.Url = "http://www.capta.com.br/teste/AN01175_20000071.jpg"
            objImage.Name = "AN01175-"
            objImage.StockKeepingUnitId = 2000018
            objImage.Label = "teste"

            objServiceClient.ImageInsertUpdate(objImage)


            Console.Write(objCLient.FirstName)
            Console.ReadKey()
        Catch ex As Exception
            Dim str As String = ex.ToString()
            str += "afadfadfa"
        End Try

    End Sub

End class
