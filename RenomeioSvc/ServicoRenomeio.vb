Imports RenomeioCtl

Public Class ServicoRenomeio

    Dim mon As New MonitorRenomeio
    Protected Overrides Sub OnStart(ByVal args() As String)
        mon.run()
    End Sub

    Protected Overrides Sub OnStop()
        mon.pause()
    End Sub

End Class
