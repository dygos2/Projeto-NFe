Imports FN4IntegradorSaferCtl

Public Class Form1

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim mon As New MonitorIntegracaoSafer
        mon.run()

    End Sub
End Class
