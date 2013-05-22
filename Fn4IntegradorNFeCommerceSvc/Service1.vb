Imports Fn4IntegradorNFeCommerceCtrl
Public Class Service1
    Dim x As New processar_txt
    Protected Overrides Sub OnStart(ByVal args() As String)
        x.run()
    End Sub

    Protected Overrides Sub OnStop()
        x.pause()
    End Sub

End Class

