Public Class Form1
    Dim x As FN4DpecCtl.enviaDpec
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        x = New FN4DpecCtl.enviaDpec()
        x.run()
    End Sub
End Class
