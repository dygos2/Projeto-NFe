Imports System.Timers
'Imports System.Threading
Imports retornoItau
Imports System
Imports System.IO
Imports System.Net
Imports System.Text
Public Class tlProcessa

    Dim ProcRetorno As Boolean
    Dim data_proc As Date
    Dim data_proc_bkp As Date

    Private WithEvents _tm As Timer

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcRetorno.Click
        'processa franquias mensais
        fazProcFranquias()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnParaProc.Click

        If ProcRetorno = False Then
            _tm.Start()
            Log.CriaLog("Processos Iniciados")
            btnParaProc.Text = "Parar Processos"
            ProcRetorno = True
        Else
            _tm.Stop()
            Log.CriaLog("Processos Parados")
            btnParaProc.Text = "Iniciar Processos"
            ProcRetorno = False
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcBoleto.Click
        ' processa os boletos
        ProcRetorno_boletos()
    End Sub


    Public Sub ProcRetorno_boletos()

        Try
            'verifica se o proc. esta ativo

            ' Create a request for the URL
            Dim request As WebRequest = _
              WebRequest.Create("https://srv1.nfecommerce.com.br/nfe4web/itau/consulta.php")
            ' If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials
            'seta o timeout
            request.Timeout = 600000
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
            Log.CriaLog("Retorno do proc_boletos: " & responseFromServer.ToString)

            ' Clean up the streams and the response.
            reader.Close()
            response.Close()
            'End If
        Catch ex As Exception
            Dim mensagem = ex.Message
            Log.CriaLog("Erro no retorno dos boletos: " & mensagem)
        End Try

    End Sub

    Public Sub verifica_processa() Handles _tm.Elapsed
        _tm.Stop()

        Log.CriaLog("Inicio dos processamentos: ")

        'boletos
        Log.CriaLog("Início Proc. Boletos: ")
        'ProcRetorno_boletos()

        Dim hj = Microsoft.VisualBasic.Right("0" & (Date.Today.Day.ToString), 2)

        Dim data_hoje = Date.Today

        'verifica se pode ser feito o proc.
        If (hj = "01" And data_hoje <> data_proc) Then
            data_proc = data_hoje
            Log.CriaLog("Início Proc. Franquias: ")
            fazProcFranquias()
        End If

        If (Now.Hour > 1 And Now.Hour < 4) Then
            Log.CriaLog("Início rotina de bkp")
            Rotina.rotinaBkp()
        End If

        Log.CriaLog("Final dos processamentos: ")
        _tm.Start()

    End Sub

    Public Sub fazProcFranquias()

        'pega o dia atual
        Try

            ' Create a request for the URL. 
            Dim request As WebRequest = _
              WebRequest.Create("http://srv1.nfe4web.com.br/core_funcs/carrega_franquias.php")
            ' If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials
            request.Timeout = 600000
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
            Log.CriaLog("Retorno do proc_franquias: " & responseFromServer.ToString)

            ' Clean up the streams and the response.
            reader.Close()
            response.Close()

        Catch ex As Exception
            Dim mensagem = ex.Message
            Log.CriaLog("Erro no retorno das franquias: " & mensagem)
        End Try
    End Sub



    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        _tm = New Timer(10800000) 'proc a cada 3 horas

        _tm.Enabled = False
        data_proc = DateAdd(DateInterval.Day, -1, Date.Today)
        data_proc_bkp = DateAdd(DateInterval.Day, -1, Date.Today)

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub btnProcRotina_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcRotina.Click
        Dim rotina = New Rotina
        rotina.rotinaBkp()
    End Sub

    Private Sub tlProcessa_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class