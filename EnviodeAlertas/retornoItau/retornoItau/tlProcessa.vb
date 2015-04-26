Imports System.Timers
Imports System.Threading
Imports retornoItau

Imports System
Imports System.IO
Imports System.Net
Imports System.Text
Public Class tlProcessa

    Dim procBoleto As Boolean
    Dim ProcRetorno As Boolean



    Private Sub tlProcessa_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcRetorno.Click
        'processa o retorno do banco 
        ProcRetorno = True
        btnProcRetorno.Enabled = True
        btnProcBoleto.Enabled = True
        fazProcRetorno()

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnParaProc.Click
        'para os processos
        ProcRetorno = False
        procBoleto = False

        btnProcRetorno.Enabled = False
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcBoleto.Click
        ' processa os boletos de franquia mensal
        procBoleto = True
        btnProcBoleto.Enabled = False
        fazProcBoletos()

    End Sub


    Public Sub fazProcRetorno()


        Try
            'verifica se o proc. esta ativo
            If (ProcRetorno = True) Then

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
            End If
        Catch ex As Exception
            Dim mensagem = ex.Message

        End Try

        Thread.Sleep(14400000)

        fazProcRetorno()


    End Sub


    Public Sub fazProcBoletos()

       
        'pega o dia atual
        Dim hj = (Date.Today.Day.ToString)
        Try
            'verifica se pode ser feito o proc.
            If ((procBoleto = True) And (hj = "1")) Then


                ' Create a request for the URL. 
                Dim request As WebRequest = _
                  WebRequest.Create("http://srv1.nfe4web.com.br/core_funcs/carrega_franquias.php")
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
                'Cria log da franquia
                Log.CriaLog(responseFromServer.ToString)

                ' Clean up the streams and the response.
                reader.Close()
                response.Close()
            End If

            'para o proc por 1 dia
            Thread.Sleep(14400000)
            fazProcBoletos()


        Catch ex As Exception
            Dim mensagem = ex.Message

        End Try

    
    End Sub



    Private Sub btnProcRotina_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcRotina.Click





    End Sub
End Class