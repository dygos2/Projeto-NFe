Imports FN4ProtocoloCtl

Public Class Service

    Private mon As New FN4ProtocoloCtl.ProtocoloMonitor
    Protected Overrides Sub OnStart(ByVal args() As String)
        mon.run()
    End Sub

    Protected Overrides Sub OnStop()
        mon.pause()
    End Sub
End Class