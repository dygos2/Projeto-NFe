Imports FN4SincroniaCtl

Public Class Form1
    Dim proc As New ProcFTP
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        proc.run()

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        proc.executarEnvioDeArquivosFTP()

    End Sub
End Class
