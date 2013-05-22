Imports Fn4IntegradorNFeCommerceCtrl

Public Class Form1
    Dim x As New processar_txt

    Public Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        x.run()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        x.pause()
    End Sub
End Class
