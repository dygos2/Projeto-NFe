Imports FN4ImpressaoCtl

Public Class Form1

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Button2.Enabled = True
        Button1.Enabled = False
        Label1.Text = "Processamento iniciado"

        Dim x As New ImpressaoMonitor
        x.run()
    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Button2.Enabled = False
        Button1.Enabled = True

        Label1.Text = "Processamento parado"

        Dim x As New ImpressaoMonitor
        x.pause()
    End Sub
End Class
