
Public Class Form1

    Dim mon As FN4EntradaTxtCtl.EntradaTxtMonitor

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        mon = New FN4EntradaTxtCtl.EntradaTxtMonitor
        mon.run()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        mon.pause()
    End Sub
End Class
