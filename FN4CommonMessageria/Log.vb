Imports log4net

Public Class Log
    Private Shared logger As ILog
    Public Shared Sub registrarDebug(ByVal mensagem As String)
        logger.Debug(mensagem)
    End Sub
    Public Shared Sub registrarErro(ByVal mensagem As String, ByVal nomeDoLogger As String)
        Try
            logger.Error(mensagem)
        Catch ex As NullReferenceException
            log4net.Config.XmlConfigurator.Configure()
            logger = LogManager.GetLogger(nomeDoLogger)

            registrarErro(mensagem & vbCrLf, nomeDoLogger)
        End Try
    End Sub
    Public Shared Sub registrarInfo(ByVal mensagem As String, ByVal nomeDoLogger As String)
        Try
            logger.Info(mensagem)
        Catch ex As Exception
            log4net.Config.XmlConfigurator.Configure(New System.IO.FileInfo(FN4CommonMessageria.Geral.Parametro("arquivoDeConfiguracaoLog4Net")))
            logger = LogManager.GetLogger(nomeDoLogger)
            registrarInfo(vbCrLf & mensagem & vbCrLf, nomeDoLogger)
        End Try
    End Sub
    Public Shared Sub registrarWarn(ByVal mensagem As String, ByVal nomeDoLogger As String)
        Try
            logger.Warn(mensagem)
        Catch ex As Exception
            log4net.Config.XmlConfigurator.Configure()
            logger = LogManager.GetLogger(nomeDoLogger)
            registrarWarn(mensagem & vbCrLf, nomeDoLogger)
        End Try

    End Sub

    Public Shared Sub registrarFatal(ByVal mensagem As String, ByVal nomeDoLogger As String)
        Try
            logger.Fatal(mensagem)
        Catch ex As Exception
            log4net.Config.XmlConfigurator.Configure()
            logger = LogManager.GetLogger(nomeDoLogger)
            registrarFatal(mensagem & vbCrLf, nomeDoLogger)
        End Try
    End Sub
End Class
