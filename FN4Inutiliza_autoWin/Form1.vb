Public Class Form1

    Dim x As FN4Inutiliza_autoCtl.inutiliza

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        x = New FN4Inutiliza_autoCtl.inutiliza
        x.Inutilizarnotas()

    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class