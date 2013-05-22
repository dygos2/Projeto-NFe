Imports FN4Common
Imports System.IO
Imports FN4Common.DataAccess
Imports Ionic.Zip

Public Class ZipMonitor

    Private WithEvents tm As New System.Timers.Timer
    Public data_proc As String

    Public Sub New()
        tm.Interval = Geral.Parametro("intervaloDeVarreduraDebkp")
        data_proc = ""
    End Sub

    Public Sub run()
        Log.registrarInfo("Monitor de geração de Zip iniciado", "ZipService")
        Dim proc_imediato As String
        proc_imediato = Geral.Parametro("processar_imediato")

        If proc_imediato = "1" Or proc_imediato = "0" Then '0 inicia de imediato processando as notas do mês anterior / 1 - processando todas as nfes / 2 - só liga e não processa
            geraZip(proc_imediato)
        End If
        tm.Start()
    End Sub
    Public Sub pause()
        tm.Stop()
    End Sub


    Public Sub executargeraZip() Handles tm.Elapsed
        'executa somente uma vez no mes todo dia 1
        If data_proc = "" And Format(Now, "dd") = "01" Then
            data_proc = Format(Now, "MM/yyyy")
            geraZip("2")
        ElseIf data_proc <> Format(Now, "MM/yyyy") And Format(Now, "dd") = "01" Then
            data_proc = Format(Now, "MM/yyyy")
            geraZip("2")
        End If

    End Sub

    Public Sub geraZip(ByVal proc_now As String)

        Dim ex As Exception
        Dim bkp As New bkpVO
        Dim tmp_file As String
        tmp_file = ""

        Try
            tm.Stop()
            Log.registrarInfo("Iniciando processamento em '" & Now, "ZipService")

            Dim zipFileName As String
            Dim cnpjtmp As String

            zipFileName = ""
            cnpjtmp = ""
            Dim Generator As System.Random = New System.Random()
            Dim zip As Ionic.Zip.ZipFile
            Dim nota As notaVO
            Dim temp_num As Integer
            Dim pasta_inut As String
            Dim fsi As IO.FileSystemInfo
            Dim notas As List(Of notaVO)


            If proc_now = "1" Then
                notas = notaDAO.obterNotasTotaisParaZip
            Else
                notas = notaDAO.obterNotasParaZip
            End If

            For Each nota In notas
                Try
                    If cnpjtmp = "" Then

                        temp_num = Generator.Next(1, 900000000)
                        zipFileName = Left(nota.pastaDeTrabalho, InStr(nota.pastaDeTrabalho, nota.NFe_emit_CNPJ) + 14) & "bkp\01_" & Right("0" & Month(Now), 2) & "_" & Year(Now) & "_" & nota.NFe_emit_CNPJ & "_" & temp_num & ".zip"
                        cnpjtmp = nota.NFe_emit_CNPJ
                        zip = New Ionic.Zip.ZipFile

                    End If

                    If cnpjtmp <> nota.NFe_emit_CNPJ Then
                        If Not Directory.Exists(Path.GetDirectoryName(zipFileName)) Then
                            Directory.CreateDirectory(Path.GetDirectoryName(zipFileName))
                        End If

                        'adicionando notas inutilizadas
                        pasta_inut = zipFileName.Substring(0, InStr(zipFileName, "bkp") - 1) & "inutilizadas\"
                        If Directory.Exists(pasta_inut) Then
                            Dim fileNames_inut As String() = Directory.GetFiles(pasta_inut)
                            For Each file_inut In fileNames_inut
                                fsi = New FileInfo(file_inut)
                                If Format(fsi.CreationTime, "MM/yyyy") = Format(Now, "MM/yyyy") Then
                                    zip.AddFile(file_inut, "inutilizadas")
                                End If
                            Next
                        End If

                        If zip.Count > 0 Then
                            zip.Save(zipFileName)
                            bkp.cnpj_emit = cnpjtmp
                            bkp.data = Year(Now) & "-" & Right("0" & Month(Now), 2) & "-" & Day(Now)
                            bkp.cod_token = temp_num

                            bkpDAO.incluirbkp(bkp)
                            Log.registrarInfo("Novo bkp criado: CNPJ - '" & cnpjtmp & "' Caminho " & zipFileName, "ZipService")
                        End If
                        zip.Dispose()

                        cnpjtmp = nota.NFe_emit_CNPJ
                        temp_num = Generator.Next(1, 900000000)
                        zipFileName = Left(nota.pastaDeTrabalho, InStr(nota.pastaDeTrabalho, nota.NFe_emit_CNPJ) + 14) & "bkp\01_" & Right("0" & Month(Now), 2) & "_" & Year(Now) & "_" & nota.NFe_emit_CNPJ & "_" & temp_num & ".zip"
                        zip = New Ionic.Zip.ZipFile
                    End If

                    'se for empresa nova, abrir novo zip
                    'Directory.CreateDirectory(nota.pastaDeTrabalho)
                    'se possui o diretorio
                    If Directory.Exists(nota.pastaDeTrabalho) Then

                        'verificando os arquivos da pasta
                        Dim fileNames As String() = Directory.GetFiles(nota.pastaDeTrabalho)

                        For Each file In fileNames
                            tmp_file = file
                            'se o arquivo encontrado for procNFe ou cancNFe/retorno_cancNFe
                            'colocar dentro do zip
                            If InStr(Path.GetFileName(file), "procNFe") > 0 Then
                                zip.AddFile(file, "processadas\Serie " & nota.serie)
                            End If
                            If InStr(Path.GetFileName(file), "cancNFe") > 0 Or InStr(Path.GetFileName(file), "Canc_lote_enviado") > 0 Or InStr(Path.GetFileName(file), "Canc_Retorno") > 0 Then
                                zip.AddFile(file, "canceladas\Serie " & nota.serie & "\Num. " & nota.NFe_ide_nNF)
                            End If
                            If InStr(Path.GetFileName(file), "dpec_assinado") > 0 Or InStr(Path.GetFileName(file), "reciboDpec") > 0 Then
                                zip.AddFile(file, "contingencia-dpec\Serie " & nota.serie & "\Num. " & nota.NFe_ide_nNF)
                            End If
                            If InStr(Path.GetFileName(file), "CCe3_lote_enviado") > 0 Or InStr(Path.GetFileName(file), "CCe_Retorno") > 0 Then
                                zip.AddFile(file, "cce\Serie " & nota.serie & "\Num. " & nota.NFe_ide_nNF)
                            End If
                        Next
                    End If

                Catch e As Exception
                    ex = e
                    Log.registrarErro("Erro inesperado: " & tmp_file & " - " & Geral.ObterExceptionMessagesEmCascata(ex) & vbCrLf & ex.StackTrace, "ZipService")
                End Try
            Next

            If Not Directory.Exists(Path.GetDirectoryName(zipFileName)) Then
                Directory.CreateDirectory(Path.GetDirectoryName(zipFileName))
            End If

            'adicionando notas inutilizadas
            pasta_inut = zipFileName.Substring(0, InStr(zipFileName, "bkp") - 1) & "inutilizadas\"
            If Directory.Exists(pasta_inut) Then
                Dim fileNames_inut As String() = Directory.GetFiles(pasta_inut)
                For Each file_inut In fileNames_inut
                    fsi = New FileInfo(file_inut)
                    If Format(fsi.CreationTime, "MM/yyyy") = Format(Now, "MM/yyyy") Then
                        zip.AddFile(file_inut, "inutilizadas")
                    End If
                Next
            End If

            If zip.Count > 0 Then
                zip.Save(zipFileName)
                bkp.cnpj_emit = cnpjtmp
                bkp.data = Year(Now) & "-" & Right("0" & Month(Now), 2) & "-" & Day(Now)
                bkp.cod_token = temp_num

                bkpDAO.incluirbkp(bkp)
                Log.registrarInfo("Novo bkp criado: CNPJ - '" & cnpjtmp & "' Caminho " & zipFileName, "ZipService")

            End If
            zip.Dispose()
            Log.registrarInfo("Monitor de geração de Zip Finalizado", "ZipService")

        Catch exception As Exception
            Log.registrarErro("Erro inesperado: " & tmp_file & " - " & Geral.ObterExceptionMessagesEmCascata(exception), "ZipService")
        Finally
            tm.Start()
        End Try
    End Sub
End Class
