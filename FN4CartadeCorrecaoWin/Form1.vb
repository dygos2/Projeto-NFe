Imports FN4CartaCorrecaoCtl

Public Class Form1
    Private mon As New CartaDeCorrecaoMonitor

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        mon.run()

    End Sub
End Class
