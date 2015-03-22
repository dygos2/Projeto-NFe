Imports log4net

Public Class Log
    Private Shared logger As ILog
    Public Shared log_global As ArrayList
    Public Shared isdebug As String

    Public Shared Sub registrarDebug(ByVal mensagem As String)
        logger.Debug(mensagem)
        debuga(mensagem)
    End Sub
    Public Shared Sub debuga(ByVal msg As String)
        If isdebug = 1 Then
            log_global.Add("=========================================================================================")
            log_global.Add(msg)
        End If
    End Sub
    Public Shared Sub registrarErro(ByVal mensagem As String, ByVal nomeDoLogger As String)
        Try
            logger.Error(mensagem)
            debuga(mensagem & " - " & nomeDoLogger)
        Catch ex As NullReferenceException
            log4net.Config.XmlConfigurator.Configure()
            logger = LogManager.GetLogger(nomeDoLogger)
            registrarErro(mensagem & vbCrLf, nomeDoLogger)
        End Try
    End Sub
    Public Shared Sub registrarInfo(ByVal mensagem As String, ByVal nomeDoLogger As String)
        Try
            logger.Info(mensagem)
            debuga(mensagem & " - " & nomeDoLogger)

        Catch ex As Exception
            log_global = New ArrayList
            isdebug = FN4Common.Geral.Parametro("isdebug")

            log4net.Config.XmlConfigurator.Configure(New System.IO.FileInfo(FN4Common.Geral.Parametro("arquivoDeConfiguracaoLog4Net")))
            logger = LogManager.GetLogger(nomeDoLogger)
            registrarInfo(vbCrLf & mensagem & vbCrLf, nomeDoLogger)
        End Try

    End Sub
    Public Shared Sub registrarWarn(ByVal mensagem As String, ByVal nomeDoLogger As String)
        Try
            logger.Warn(mensagem)
            debuga(mensagem & " - " & nomeDoLogger)
        Catch ex As Exception
            log_global = New ArrayList
            log4net.Config.XmlConfigurator.Configure()
            logger = LogManager.GetLogger(nomeDoLogger)
            registrarWarn(mensagem & vbCrLf, nomeDoLogger)
        End Try

    End Sub

    Public Shared Sub registrarFatal(ByVal mensagem As String, ByVal nomeDoLogger As String)
        Try
            logger.Fatal(mensagem)
            debuga(mensagem & " - " & nomeDoLogger)
        Catch ex As Exception
            log_global = New ArrayList
            log4net.Config.XmlConfigurator.Configure()
            logger = LogManager.GetLogger(nomeDoLogger)
            registrarFatal(mensagem & vbCrLf, nomeDoLogger)
        End Try
    End Sub
    Public Shared Sub retorna_log(ByRef retorno As ArrayList)
        Try
            retorno = log_global
        Catch ex As Exception
        End Try
    End Sub
End Class
