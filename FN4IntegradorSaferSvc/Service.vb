Imports FN4IntegradorSaferCtl

Public Class Service

    Private monitor As New MonitorIntegracaoSafer

    Protected Overrides Sub OnStart(ByVal args() As String)
        monitor.run()
    End Sub

    Protected Overrides Sub OnStop()
        monitor.pause()
    End Sub

End Class
