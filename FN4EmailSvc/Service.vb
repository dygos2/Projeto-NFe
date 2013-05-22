Imports FN4EmailCtl

Public Class Service
    Private mon As New EnvioEmailMonitor
    Protected Overrides Sub OnStart(ByVal args() As String)
        ' Add code here to start your service. This method should set things
        ' in motion so your service can do its work.
        mon.run()
    End Sub

    Protected Overrides Sub OnStop()
        ' Add code here to perform any tear-down necessary to stop your service.
        mon.pause()
    End Sub

End Class
