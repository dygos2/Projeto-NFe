﻿Imports FN4EmailCtl

Public Class Form1

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim x As New EnvioEmailMonitor
        x.run()
    End Sub
End Class
