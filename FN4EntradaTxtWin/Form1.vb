
Public Class Form1

    Dim mon As FN4EntradaTxtCtl.EntradaTxtMonitor
    Dim mon2 As FN4EnvioContCtl.EnvioMonitorCont
    Dim mon3 As FN4RetornoContCtl.RetornoMonitor
    Dim mon4 As FN4EnvioCtl.EnvioMonitor
    Dim mon5 As FN4RetornoCtl.RetornoMonitor
    Dim mon6 As FN4CartaCorrecaoCtl.CartaDeCorrecaoMonitor
    Dim mon7 As FN4InutilizacaoCtl.InutilizacaoMonitor

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        mon = New FN4EntradaTxtCtl.EntradaTxtMonitor
        Label1.Text = "Rodando"
        mon.run()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        mon.pause()
        Label1.Text = "Parado"
    End Sub

    Private Sub GroupBox2_Enter(sender As System.Object, e As System.EventArgs) Handles GroupBox2.Enter
        mon2 = New FN4EnvioContCtl.EnvioMonitorCont
        Label2.Text = "Rodando"
        mon2.run()
    End Sub


    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        mon2.pause()
        Label2.Text = "Parado"
    End Sub

    Private Sub Button5_Click(sender As System.Object, e As System.EventArgs) Handles Button5.Click
        mon3 = New FN4RetornoContCtl.RetornoMonitor
        Label3.Text = "Rodando"
        mon3.run()
    End Sub


    Private Sub Button6_Click(sender As System.Object, e As System.EventArgs) Handles Button6.Click
        mon3.pause()
        Label3.Text = "Parado"
    End Sub

    Private Sub GroupBox4_Enter(sender As System.Object, e As System.EventArgs) Handles GroupBox4.Enter

    End Sub

    Private Sub Button7_Click(sender As System.Object, e As System.EventArgs) Handles Button7.Click
        mon4 = New FN4EnvioCtl.EnvioMonitor
        Label4.Text = "Rodando"
        mon4.run()
    End Sub

    Private Sub Button8_Click(sender As System.Object, e As System.EventArgs) Handles Button8.Click
        mon4.pause()
        Label4.Text = "Parado"
    End Sub

    Private Sub Button9_Click(sender As System.Object, e As System.EventArgs) Handles Button9.Click
        mon5 = New FN4RetornoCtl.RetornoMonitor
        Label5.Text = "Rodando"
        mon5.run()
    End Sub

    Private Sub Button10_Click(sender As System.Object, e As System.EventArgs) Handles Button10.Click
        mon5.pause()
        Label5.Text = "Parado"
    End Sub

    Private Sub Button11_Click(sender As System.Object, e As System.EventArgs) Handles Button11.Click
        mon6 = New FN4CartaCorrecaoCtl.CartaDeCorrecaoMonitor
        mon6.run()
        Label6.Text = "Rodando"
    End Sub

    Private Sub Button12_Click(sender As System.Object, e As System.EventArgs) Handles Button12.Click
        mon6.pause()
        Label6.Text = "Parado"
    End Sub

    Private Sub Button13_Click(sender As System.Object, e As System.EventArgs) Handles Button13.Click
        mon7 = New FN4InutilizacaoCtl.InutilizacaoMonitor
        mon7.Run()
        Label7.Text = "Rodando"
    End Sub

    Private Sub Button14_Click(sender As System.Object, e As System.EventArgs) Handles Button14.Click
        mon7.Pause()
        Label7.Text = "Parado"
    End Sub
End Class
