Imports System.Timers
Imports FN4Common
Imports System.Xml
Imports System.IO
Imports System
Public Class processar_txt
    Private numeroDaNota As Integer
    Private dtProcessamento As String
    Private WithEvents tm As New Timer
    Private WithEvents tm2 As New Timer
    Public arr_notas As New ArrayList()
    Public count_nf As Integer

    Public Sub New()
        count_nf = 0
        tm.Interval = Geral.Parametro("intervaloDeVarreduraDoTXT")
        tm2.Interval = Geral.Parametro("intervaloDeconsulta")
    End Sub
    Public Sub run()
        tm.Start()
        Log.registrarInfo("Serviço de Envio Iniciado", "EnvioTxtNFecommerce")
    End Sub

    Public Sub pause()
        tm.Stop()
        tm2.Stop()
        Log.registrarInfo("Serviço de Envio Parado", "EnvioTxtNFecommerce")
    End Sub

    Private Sub executarMonitorEnvio() Handles tm.Elapsed
        tm.Stop()
        Try
            varrerTxts()
        Catch ex As Exception
            Log.registrarErro("Erro inesperado no serviço de varredura - " & ex.Message & " in " & Geral.ObterStackTraceEmCascata(ex), _
                               "EnvioTxtNFecommerce")
        End Try
    End Sub
    Private Sub verificarretorno() Handles tm2.Elapsed
        tm2.Stop()
        Try
            Dim arr_tmp As Array
            Dim ret As Object
            System.Net.ServicePointManager.Expect100Continue = False

            'verificando os retornos
            For i = 0 To arr_notas.Count - 1
                arr_tmp = Split(arr_notas.Item(i), "|")
                If FN4Common.Geral.Parametro("tm_amb_nfe4web") = "1" Then
                    Dim ws As New nfecommerce2_homolog.NFeCommerce
                    ret = ws.VerificarStatus(arr_tmp(0), arr_tmp(1), arr_tmp(2), arr_tmp(3))
                    ws.Dispose()
                    ws = Nothing
                Else
                    Dim ws As New nfecommerce2_prod.NFeCommerce

                    ret = ws.VerificarStatus(arr_tmp(0), arr_tmp(1), arr_tmp(2), arr_tmp(3))
                    ws.Dispose()
                    ws = Nothing
                End If

                'escrevendo no arquivo de retorno
                Dim oEscrever As System.IO.StreamWriter

                oEscrever = System.IO.File.CreateText(FN4Common.Geral.Parametro("pastaRetorno") & "ret_" & arr_tmp(0) & "_" & arr_tmp(3) & "_" & arr_tmp(2) & ".txt")
                'escrevendo conteudos
                oEscrever.WriteLine("01|" & ret.chave)
                oEscrever.WriteLine("02|" & ret.cnpj)
                oEscrever.WriteLine("03|" & ret.numero)
                oEscrever.WriteLine("04|" & ret.serie)
                oEscrever.WriteLine("05|" & ret.status)
                oEscrever.WriteLine("06|" & ret.cStat)
                oEscrever.WriteLine("07|" & ret.dataUltimaAtualizacao)
                oEscrever.WriteLine("08|" & ret.urlDanfe)
                oEscrever.WriteLine("09|" & ret.motivo)
                oEscrever.Close()

                If ret.status > -1 Then
                    Log.registrarInfo("[Processada] - Retorno de consulta da nota " & ret.numero & "-" & ret.serie, "EnvioTxtNFecommerce")
                    arr_notas.RemoveAt(i)
                    If arr_notas.Count = 0 Then
                        count_nf = 0
                    End If
                Else
                    count_nf = count_nf + 1
                    Log.registrarInfo("[Sem retorno da Sefaz] - Retorno de consulta da nota " & ret.numero & "-" & ret.serie & " - Tentativa " & count_nf & " de 5", "EnvioTxtNFecommerce")
                End If

                If count_nf = 10 Then
                    arr_notas.RemoveAt(i)
                End If
            Next

        Catch ex As Exception
            Log.registrarErro("Erro no acesso ao webservice de retorno - " & ex.Message & " in " & Geral.ObterStackTraceEmCascata(ex), _
                               "EnvioTxtNFecommerce")
        Finally
            tm.Start()
        End Try
    End Sub
    Private Sub varrerTxts()

        Dim path_txts = IIf(FN4Common.Geral.Parametro("tm_amb_nfe4web") = "1", FN4Common.Geral.Parametro("pastaEntrada_homolog"), FN4Common.Geral.Parametro("pastaEntrada_prod"))

        'para cada TXT
        For Each arquivo In Directory.GetFiles(path_txts, "*.txt")
            'SEMAFORO
            Try
                File.Move(arquivo, arquivo.Replace(".txt", ".TXT"))
                arquivo = arquivo.Replace(".txt", ".TXT")
            Catch ex As Exception
                Log.registrarErro("Erro: Arquivo em uso", "EntradaTxtService")
                Continue For
            End Try
            Log.registrarInfo("Entrada do arquivo " & arquivo & " no sistema, processamento", "EnvioTxtNFecommerce")

            Dim serie As Integer = CInt(arquivo.Substring(arquivo.LastIndexOf("\") + 1).Replace(".TXT", "").Split("-")(2))
            numeroDaNota = CInt(arquivo.Substring(arquivo.LastIndexOf("\") + 1).Replace(".TXT", "").Split("-")(1)) 'captura apenas o trecho do nome do arquivo após o traço
            Dim CNPJ As String = arquivo.Substring(arquivo.LastIndexOf("\") + 1).Replace(".TXT", "").Split("-")(0)

            If CNPJ.Trim(" ").Length < 14 Then
                rejeitarArquivo(arquivo, Nothing, "Erro: CNPJ do arquivo de nota inválido")
                Continue For
            End If

            'verificar qual token usar
            Dim arr_tokens = Split(FN4Common.Geral.Parametro("tokens"), "/")
            Dim arr_cnpjs = Split(FN4Common.Geral.Parametro("cnpjs"), "/")
            Dim token_used As String
            token_used = ""

            If UBound(arr_tokens) > 0 Then
                For i = 0 To UBound(arr_tokens)
                    If CNPJ = arr_cnpjs(i) Then
                        token_used = arr_tokens(i)
                    End If
                Next
            Else
                If CNPJ = arr_cnpjs(0) Then
                    token_used = arr_tokens(0)
                End If
            End If

            If CNPJ.Length = 0 Then
                rejeitarArquivo(arquivo, Nothing, "Erro: Nota sem cnpj e token cadastrado")
                Continue For
            End If

            ' Se o nome do arquivo contém a literal "consult"
            If arquivo.ToLower().Contains("consult") Then
                arr_notas.Add(CNPJ & "|" & token_used & "|" & serie & "|" & numeroDaNota)
                Log.registrarInfo("Nova consulta da nota " & numeroDaNota & " da serie " & serie, "EnvioTxtNFecommerce")
                deletarArquivo(arquivo)
                Continue For
            End If

                Dim reader As New StreamReader(arquivo, System.Text.Encoding.GetEncoding("ISO-8859-1"))

                Dim txtDeEntrada As String = reader.ReadToEnd
                reader.Close()

            System.Net.ServicePointManager.Expect100Continue = False

            If FN4Common.Geral.Parametro("tm_amb_nfe4web") = "1" Then
                Dim ws As New nfecommerce2_homolog.NFeCommerce
                ws.EnviarNota(txtDeEntrada, CNPJ, token_used, serie, numeroDaNota)
                ws.Dispose()
                ws = Nothing
            Else
                Dim ws As New nfecommerce2_prod.NFeCommerce

                ws.EnviarNota(txtDeEntrada, CNPJ, token_used, serie, numeroDaNota)
                ws.Dispose()
                ws = Nothing
            End If

                'gravando num array
            arr_notas.Add(CNPJ & "|" & token_used & "|" & serie & "|" & numeroDaNota)
            Log.registrarInfo("O arquivo " & arquivo & " foi enviado com sucesso!", "EnvioTxtNFecommerce")
            deletarArquivo(arquivo)
        Next
        tm2.Start()

    End Sub

    Public Sub rejeitarArquivo(ByVal arquivo As String, ByVal ex As Exception, ByVal mensagem As String)
        Me.dtProcessamento = Format(DateTime.Now, "yyyyMMddhhmmss")
        If Not ex Is Nothing Then
            Log.registrarErro(mensagem & ex.Message & vbCrLf & ex.StackTrace, "EntradaTxtService")
        Else
            Log.registrarErro(mensagem, "EnvioTxtNFecommerce")
        End If
        File.Copy(arquivo, FN4Common.Geral.Parametro("pastaDeRejeitadasNfeCommerce") & numeroDaNota & "_" & dtProcessamento & ".txt", True)
        File.Delete(arquivo)
    End Sub
    Public Sub deletarArquivo(ByVal arquivo As String)
        Me.dtProcessamento = Format(DateTime.Now, "yyyyMMddhhmmss")
        File.Delete(arquivo)
    End Sub
End Class