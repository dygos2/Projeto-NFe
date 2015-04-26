Imports System.Timers
Imports System.Threading
Imports retornoItau
Imports System
Imports System.IO
Imports System.Net
Imports System.Text

Public Class varreduraShopLine

    Public i = 0




    Public Shared Sub Main(ByVal i As Integer)


    End Sub

    Public Shared Sub fazConsulta()


        Dim tela = New tlProcessa

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


        'If (tela.ProcRetorno = True) Then
        ' fazConsulta()
        ' End If


        Thread.Sleep(24)






    End Sub

    Public Shared Sub NaofazNada(ByVal state As Object)
        Thread.Sleep(1000)

    End Sub

    Public Shared Sub paraProcesso()

        Main(1)


    End Sub


    Public Shared Sub iniciaProcesso()
        Main(0)
    End Sub



End Class
