Imports FN4CartaCorrecaoCtl

Public Class CartaCorrecaoService

    Private mon As New CartaDeCorrecaoMonitor
    Protected Overrides Sub OnStart(ByVal args() As String)
        mon.run()
    End Sub

    Protected Overrides Sub OnStop()
        mon.pause()
    End Sub

End Class
