Imports FN4EntradaTxtCtl

Public Class Service
    Private monitor As New EntradaTxtMonitor
    Protected Overrides Sub OnStart(ByVal args() As String)
        monitor.run()
    End Sub

    Protected Overrides Sub OnStop()
        monitor.pause()
    End Sub

End Class
