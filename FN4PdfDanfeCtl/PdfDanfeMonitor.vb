
Imports FN4Common.DataAccess
Imports FN4Common
Imports System.IO

Public Class PdfDanfeMonitor

    Private WithEvents tm As New System.Timers.Timer
    Private Shared resultadoValidacao As System.Text.StringBuilder

    Public Sub New()
        tm.Interval = Geral.Parametro("intervaloDeVarreduraDeSaida")
    End Sub

    Public Sub run()
        Log.registrarInfo("Monitor de Geracao de PDF iniciado", "PdfDanfeService")
        tm.Start()
    End Sub
    Public Sub pause()
        tm.Stop()
        Log.registrarInfo("Monitor de Geracao de PDF parado", "PdfDanfeService")
    End Sub

    Public Sub GerarPDFs() Handles tm.Elapsed
        tm.Stop()

        'declaração das variáveis necessárias
        Dim notasParaDanfe As List(Of notaVO)

        Try
            Log.registrarInfo("Buscando notas para gerar PDF.", "PdfDanfeService")
            notasParaDanfe = notas.obterNotasParaDanfe()

            If notasParaDanfe.Count = 0 Then
                Log.registrarInfo("Nenhuma nota pendente de geração de PDF.", "PdfDanfeService")
                Exit Try
            End If

            For Each nota In notasParaDanfe
                'executa passos para impressão do DANFE
                Dim pathXml As String = Path.Combine(nota.pastaDeTrabalho, nota.NFe_ide_nNF & "_procNFe.xml")
                Dim nomePdf As String = nota.NFe_infNFe_id & ".pdf"
                Dim pathLogo As String = Path.Combine(Geral.Parametro("pastaLogos"), nota.NFe_emit_CNPJ & ".jpg")
                Dim pathDanfe As String = Geral.Parametro("pathDanfe") & " "
                Dim pathDirPdf As String = Path.Combine(Path.GetDirectoryName(pathDanfe), "PDF" & "\" & nota.NFe_ide_dEmi.Year & nota.NFe_ide_dEmi.Month)
                Dim pdfArgs As String = "arquivo={0}"

                'pathDanfe = pathDanfe & "arquivo=" & pathXml

                'If Not Directory.Exists(pathDirPdf) Then
                'Log.registrarErro("Path do diretório para armazenar PDF da nota " & nota.NFe_ide_nNF & " não existe", "PdfDanfeService")
                'Continue For
                If Not File.Exists(pathXml) Then
                    Log.registrarErro("Arquivo XML da nota " & nota.NFe_ide_nNF & " não existe", "PdfDanfeService")
                    nota.imprimeDanfe = False
                    notaDAO.alterarNota(nota)
                    Continue For
                End If

                pdfArgs = String.Format(pdfArgs, pathXml)

                If File.Exists(pathLogo) Then
                    pdfArgs = pdfArgs & " logotipo={0}"
                    pdfArgs = String.Format(pdfArgs, pathLogo)
                End If

                If Not Geral.Parametro("formatoPDF").Equals(String.Empty) Then
                    pdfArgs = pdfArgs & " configuracao={0}"
                    pdfArgs = String.Format(pdfArgs, Geral.Parametro("formatoPDF"))
                End If

                Dim processoUnidanfe = New Process
                processoUnidanfe.StartInfo.FileName = Geral.Parametro("pathDanfe")
                processoUnidanfe.StartInfo.Arguments = pdfArgs
                processoUnidanfe.StartInfo.Verb = "runas"
                processoUnidanfe.StartInfo.UseShellExecute = True
                processoUnidanfe.Start()
                processoUnidanfe.WaitForExit()

                Log.registrarInfo("Executando o comando: " & processoUnidanfe.StartInfo.FileName & " " & processoUnidanfe.StartInfo.Arguments, "PdfDanfeService")

                Dim pathPdf = Path.Combine(pathDirPdf, nomePdf)

                Dim contador As Integer = 0

                While (contador < Int32.Parse(Geral.Parametro("tempoSleepGeracaoPDF")) And Not File.Exists(pathPdf))
                    contador = contador + 1
                    Threading.Thread.Sleep(1000)
                End While

                If Not File.Exists(pathPdf) Then
                    Log.registrarErro("Erro ao criar PDF para a nota " & nota.NFe_ide_nNF & ".", "PdfDanfeService")
                    Continue For
                End If

                Log.registrarInfo("PDF gerado com sucesso: " + pathPdf, "PdfDanfeService")

                nota.imprimeDanfe = 0
                nota.impressaoSolicitada = 1
                notas.alterarNota(nota)

                Log.registrarInfo("DANFE da nota " & nota.NFe_ide_nNF & " - série " & nota.serie & " impresso com sucesso.", "PdfDanfeService")
            Next
        Catch ex As Exception
            Log.registrarErro("Erro: " & Geral.ObterExceptionMessagesEmCascata(ex), "PdfDanfeService")
        End Try

        tm.Start()
    End Sub
End Class
