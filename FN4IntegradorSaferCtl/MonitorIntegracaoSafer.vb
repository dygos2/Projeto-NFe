Imports FN4Common
Imports System.IO

Public Class MonitorIntegracaoSafer
    Private WithEvents tm As New System.Timers.Timer

    Public Sub New()
        Log.registrarInfo("Monitor de integração iniciado", "IntegradorSaferService")
        tm.Interval = Geral.Parametro("intervaloIntegracao")
    End Sub

    Public Sub run()
        tm.Start()
    End Sub

    Public Sub pause()
        tm.Stop()
    End Sub

    Private Sub executarMonitorDeIntegracao() Handles tm.Elapsed

        Try
            tm.Stop()
            Dim lista As List(Of notaVO) = IBatisNETHelper.Instance.QueryForList(Of notaVO)("obterNotasSafer", 2)

            For Each nota As notaVO In lista
                Log.registrarInfo("Integrando nota" & nota.NFe_infNFe_id & " ao NFeSafer", "IntegradorSaferService")

                File.Copy(nota.pastaDeTrabalho & nota.NFe_ide_nNF & "_procNFe.xml", Geral.Parametro("pastaSafer") & nota.NFe_infNFe_id & "-procnfe.xml")
                'atualizar a flag
                IBatisNETHelper.Instance.Update("marcarSincronizada", nota)
            Next
        Catch ex As Exception
            Log.registrarErro("Erro ao tentar integrar a nota - " & ex.Message & vbCrLf & ex.StackTrace, "IntegradorSaferService")
        Finally
            tm.Start()
        End Try


    End Sub
End Class
