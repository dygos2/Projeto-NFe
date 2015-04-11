Imports FN4Common

Public Class Form1

    Dim mon As FN4EntradaTxtCtl.EntradaTxtMonitor
    Dim mon3 As FN4Contingencia.Contingencia
    Dim mon4 As FN4EnvioCtl.EnvioMonitor
    Dim mon5 As FN4RetornoCtl.RetornoMonitor
    Dim mon6 As FN4CartaCorrecaoCtl.CartaDeCorrecaoMonitor
    Dim mon7 As FN4InutilizacaoCtl.InutilizacaoMonitor
    Dim mon8 As FN4ImpressaoCtl.ImpressaoMonitor
    Dim mon9 As FN4ProtocoloCtl.ProtocoloMonitor
    Dim mon10 As FN4EmailCtl.EnvioEmailMonitor
    Dim mon11 As FN4Contingencia.Contingencia
    Dim mon12 As FN4ZipXmlCtl.ZipMonitor

    Dim last_id As Integer

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        mon = New FN4EntradaTxtCtl.EntradaTxtMonitor
        Label1.Text = "Rodando"
        mon.run()
        refresh_logs()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        mon.pause()
        Label1.Text = "Parado"
    End Sub



    Private Sub Button5_Click(sender As System.Object, e As System.EventArgs) Handles Button5.Click
        mon11 = New FN4Contingencia.Contingencia
        Label3.Text = "Rodando"
        mon11.run()
    End Sub


    Private Sub Button6_Click(sender As System.Object, e As System.EventArgs) Handles Button6.Click

        mon11.pause()
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

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        last_id = 0
    End Sub

    Private Sub Button15_Click(sender As System.Object, e As System.EventArgs) Handles Button15.Click
        mon8 = New FN4ImpressaoCtl.ImpressaoMonitor
        mon8.run()
        Label8.Text = "Rodando"
    End Sub

    Private Sub Button16_Click(sender As System.Object, e As System.EventArgs) Handles Button16.Click
        mon8.pause()
        Label8.Text = "Parado"
    End Sub

    Private Sub TextBox1_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub

    Public Function refresh_logs()
        Dim arr As New ArrayList
        Log.retorna_log(arr)

        If Not IsNothing(arr) Then
            For i As Integer = last_id To arr.Count - 1
                Dim val As String = arr(i).ToString()
                TextBox1.AppendText(val & Environment.NewLine)
            Next
            last_id = arr.Count
        End If

        Return 0
    End Function

    Private Sub Button17_Click(sender As System.Object, e As System.EventArgs) Handles Button17.Click

        refresh_logs()

    End Sub

    Private Sub Button18_Click(sender As System.Object, e As System.EventArgs) Handles Button18.Click
        mon9 = New FN4ProtocoloCtl.ProtocoloMonitor
        mon9.run()
        Label9.Text = "Rodando"
    End Sub

    Private Sub Button19_Click(sender As System.Object, e As System.EventArgs) Handles Button19.Click
        mon9.pause()
        Label9.Text = "Parado"
    End Sub

    Private Sub Button20_Click(sender As System.Object, e As System.EventArgs) Handles Button20.Click
        mon10 = New FN4EmailCtl.EnvioEmailMonitor
        mon10.run()
        Label10.Text = "Rodando"
    End Sub

    Private Sub Button21_Click(sender As System.Object, e As System.EventArgs) Handles Button21.Click
        mon10.pause()
        Label10.Text = "Parado"
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub Label3_Click(sender As System.Object, e As System.EventArgs) Handles Label3.Click

    End Sub

    Private Sub Button3_Click_1(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        mon12 = New FN4ZipXmlCtl.ZipMonitor
        mon12.run()
        Label2.Text = "Rodando"
    End Sub

    Private Sub Label2_Click(sender As System.Object, e As System.EventArgs) Handles Label2.Click

    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        mon12.pause()
        Label2.Text = "Parado"
    End Sub
End Class