Imports System.Timers
Imports FN4Common.DataAccess
Imports FN4Common
Imports System.Xml
Imports System.IO

Public Class ImpressaoMonitor
    Private WithEvents tm As New Timer

    Public Sub New()
        tm.Interval = Geral.Parametro("intervaloDeVarreduraDeSaida")
    End Sub

    Public Sub run()
        tm.Start()
        Log.registrarInfo("Serviço de Impressão Iniciado", "ImpressaoService")
    End Sub

    Public Sub pause()
        tm.Stop()

    End Sub

    Private Sub executarMonitorImpressao() Handles tm.Elapsed
        tm.Stop()
        Try
            imprimirNotasPendentes()
        Catch ex As Exception
            Log.registrarErro("Erro inesperado na impressão - " & ex.Message & " in " & Geral.ObterStackTraceEmCascata(ex), _
                               "ImpressaoService")
        Finally
            tm.Start()
        End Try
    End Sub

    Private listaNotas As List(Of notaVO)
    Private pagina As Integer
    Private files As New ArrayList
    Dim datainicio, datafim As Date

    '  Copias=
    Private Sub imprimirNotasPendentes()
        Dim nota As notaVO

        listaNotas = notas.obterNotasParaDanfe()

        If listaNotas Is Nothing Or listaNotas.Count = 0 Then
            Exit Sub
        Else
            Log.registrarInfo("Imprimindo notas pendentes: " & listaNotas.Count, "ImpressaoService")
        End If

        For Each nota In listaNotas
            Try
                Dim diretorioBase As String = AppDomain.CurrentDomain.BaseDirectory

                Dim pathDanfe As String = Geral.Parametro("pathDanfe") & " "
                Dim pathDirPdf As String = Path.Combine(Path.GetDirectoryName(pathDanfe), "PDF")
                pathDirPdf = Path.Combine(pathDirPdf, nota.NFe_emit_CNPJ)
                pathDirPdf = Path.Combine(pathDirPdf, nota.NFe_ide_dEmi.ToString("yyyy") & nota.NFe_ide_dEmi.ToString("MM"))

                'carrega o caminho do PDF do DANFE
                Dim pathPdf As String = Path.Combine(pathDirPdf, nota.NFe_infNFe_id & ".pdf")

                imprimirPDF(nota)

                Log.registrarInfo("Nota " & nota.NFe_ide_nNF & " de série " & nota.serie & " e do emissor " & nota.NFe_emit_CNPJ & " com PDF gerado e impresso com sucesso.", "ImpressaoService")
            Catch ex As Exception
                Log.registrarErro( _
                                   "Erro na impressão do DANFE - nota: " & IIf(nota Is Nothing, "s/n", nota.NFe_ide_nNF) & "-" & ex.Message & " in " & _
                                   ex.StackTrace, "ImpressaoService")
            Finally
                nota.impressaoSolicitada = 0
                notaDAO.alterarNota(nota)
            End Try
        Next

    End Sub

    Private Sub imprimirPDF(ByVal nota As notaVO)

        Dim unidanfe As New Process
        unidanfe.StartInfo.FileName = Geral.Parametro("pathDanfe")

        'carrega o executável do Adobe Reader para executá-lo em linha de comando, para impressão
        Dim pdfArgs As String

        Dim pathXml As String

        If nota.impressoEmContingencia = 1 And nota.statusDaNota > 50 Then
            pathXml = Path.Combine(nota.pastaDeTrabalho, nota.NFe_ide_nNF & "_assinado.xml")
        Else
            pathXml = Path.Combine(nota.pastaDeTrabalho, nota.NFe_ide_nNF & "_procNFe.xml")
        End If

        Log.registrarInfo("Path do XML a ser impresso: " & pathXml, "ImpressaoService")

        If Not String.IsNullOrEmpty(nota.impressora) Then
            pdfArgs = String.Format("a=""{0}"" i=""{1}"" p={2}", pathXml, nota.impressora, nota.impressaoSolicitada)
        Else
            pdfArgs = String.Format("a=""{0}"" p={1}", pathXml, nota.impressaoSolicitada)
        End If

        Dim pathDpec As String = Path.Combine(nota.pastaDeTrabalho, nota.NFe_ide_nNF & "_reciboDpec.xml")

        If nota.impressoEmContingencia = 1 And nota.statusDaNota > 50 Then
            pdfArgs = String.Format("{0} ad=""{1}""", pdfArgs, pathDpec)
        End If

        Dim pathPastaLogos = Geral.Parametro("pastaLogos")

        If Not String.IsNullOrEmpty(pathPastaLogos) Then
            Dim pathLogo = Path.Combine(pathPastaLogos, nota.NFe_emit_CNPJ & ".jpg")

            If File.Exists(pathLogo) Then
                pdfArgs = String.Format("{0} l=""{1}""", pdfArgs, pathLogo)
            End If
        End If

        Log.registrarInfo("Argumentos configurados para o unidanfe trabalhar: " & pdfArgs, "ImpressaoService")

        Dim tempoEsperaUnidanfe = Integer.Parse(Geral.Parametro("tempoEsperaUnidanfe"))

        unidanfe.StartInfo.Arguments = pdfArgs
        unidanfe.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        unidanfe.StartInfo.CreateNoWindow = True
        unidanfe.Start()
        unidanfe.WaitForExit(tempoEsperaUnidanfe)

        If Not unidanfe.HasExited Then
            Log.registrarWarn("Processo do UniDANFE demorou mais do que o configurado (" & (tempoEsperaUnidanfe / 1000) & " segundos) para executar. Nota: " & nota.NFe_ide_nNF & " - Série: " & nota.serie & " - CNPJ: " & nota.NFe_emit_CNPJ, "ImpressaoService")
            unidanfe.Kill()
        End If

        unidanfe.Close()
    End Sub

#Region "Acessorios"

    Private Sub inserirHistorico(ByVal tipo As String, ByVal descricao As String, ByVal nota As notaVO)
        Dim hist As New historicoVO(tipo, descricao, nota.NFe_ide_nNF, nota.NFe_emit_CNPJ, nota.serie)
        notaDAO.inserirHistorico(hist)

        Retorno.escreverRetorno(nota, DateTime.Now, tipo, descricao)
    End Sub

#End Region
End Class
