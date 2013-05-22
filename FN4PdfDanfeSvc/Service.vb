Imports FN4PdfDanfeCtl

Public Class Service

    Private mon As New PdfDanfeMonitor
    Protected Overrides Sub OnStart(ByVal args() As String)
        mon.run()
    End Sub

    Protected Overrides Sub OnStop()
        mon.pause()
    End Sub
End Class