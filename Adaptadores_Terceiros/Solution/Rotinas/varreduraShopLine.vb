Imports System.Timers
Imports System.Threading

Imports System
Imports System.IO
Imports System.Net
Imports System.Text

Public Class varreduraShopLine


    Public Shared Sub Main()
        ' Inicia uma thread do pool

        Dim i = 0

        While (i < 1)

            ThreadPool.QueueUserWorkItem(AddressOf NaofazNada)
            fazConsulta()

            Thread.Sleep(600000)

        End While



    End Sub

    Public Shared Sub fazConsulta()
        Try
            ' Create a request for the URL. 
            Dim request As WebRequest = _
              WebRequest.Create("https://srv1.nfecommerce.com.br/nfe4web/itau/consulta.php")
            ' If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials
            ' Get the response.
            Dim response As WebResponse = request.GetResponse()
            ' Display the status.
            Console.WriteLine(CType(response, HttpWebResponse).StatusDescription)
            ' Get the stream containing content returned by the server.
            Dim dataStream As Stream = response.GetResponseStream()
            ' Open the stream using a StreamReader for easy access.
            Dim reader As New StreamReader(dataStream)
            ' Read the content.
            Dim responseFromServer As String = reader.ReadToEnd()
            ' Display the content.
            'Console.WriteLine(responseFromServer)
            'Cria log da consulta
            Log.CriaLog(responseFromServer.ToString)

            ' Clean up the streams and the response.
            reader.Close()
            response.Close()
        Catch ex As Exception
            Dim mensagem = ex.Message

        End Try
        
    End Sub

    Public Shared Sub NaofazNada(ByVal state As Object)
        Thread.Sleep(1000)

    End Sub


End Class
