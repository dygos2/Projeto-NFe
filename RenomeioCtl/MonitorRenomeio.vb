Imports FN4Common
Imports System.IO

Public Class MonitorRenomeio
    Private WithEvents tm As New System.Timers.Timer

    Public Sub New()
        tm.Interval = 3000
        Log.registrarInfo("Serviço de pre entrada Iniciado", "EntradaTxtService")
    End Sub
    Public Sub run()
        tm.Start()
    End Sub
    Public Sub pause()
        tm.Stop()
    End Sub

    Private Sub executarMonitorRenomeio() Handles tm.Elapsed
        Try
            tm.Stop()
            Dim arquivo As String
            Dim sr As StreamReader
            Dim sw As StreamWriter
            Dim content As String
            Dim newcontent As String
            Dim cnpj As String
            Dim serie As String
            Dim numero As String

            For Each arquivo In Directory.GetFiles(Geral.Parametro("pastaPreEntrada"), "*.TXT")
                Try
                    Try
                        File.Move(arquivo, arquivo.Replace(".TXT", ".txt"))
                        arquivo = arquivo.Replace(".TXT", ".txt")
                    Catch ex As Exception
                        Log.registrarErro("Erro: Arquivo em uso", "EntradaTxtService")
                        Continue For
                    End Try

                    Log.registrarInfo("Entrada do arquivo " & arquivo & " na pasta de pré-entrada", "EntradaTxtService")
                    sr = New StreamReader(arquivo, System.Text.Encoding.GetEncoding("ISO-8859-1"))

                    Log.registrarInfo("Alterando conteúdo do arquivo ", "EntradaTxtService")

                    content = sr.ReadToEnd

                    cnpj = Geral.Parametro("CNPJEmitente")
                    serie = content.Split("|")(CInt(Geral.Parametro("posicaoCampoSerieNota")) - 1)
                    numero = content.Split("|")(CInt(Geral.Parametro("posicaoCampoNumeroNota")) - 1)


                    newcontent = removerultimalinha(content, arquivo)
                    sr.Close()

                    Log.registrarInfo("Conteúdo alterado", "EntradaTxtService")


                    Log.registrarInfo("Salvando alterações", "EntradaTxtService")
                    sw = New StreamWriter(arquivo, False)
                    sw.Write(newcontent)
                    Log.registrarInfo("Alterações salvas", "EntradaTxtService")
                    sw.Close()

                    File.Move(arquivo, Path.Combine(Geral.Parametro("pastaEntrada"), cnpj & "-" & numero & "-" & serie & ".txt"))
                Catch ex As Exception
                    Log.registrarErro("Erro no processamento da nota: " & ex.Message & " - " & ex.StackTrace, "EntradaTxtService")
                    Continue For
                End Try
            Next
        Catch ex As Exception
            Log.registrarErro("Erro no processamento da nota: " & ex.Message & " - " & ex.StackTrace, "EntradaTxtService")
        Finally
            tm.Start()
        End Try
    End Sub
    Private Function removerultimalinha(ByVal texto As String, ByVal caminhoArquivo As String) As String
        Try

            Dim retorno As String = ""
            If texto.LastIndexOf(vbCr) = texto.Length - 2 Then
                retorno = texto.Substring(0, texto.LastIndexOf(vbCr))
            Else
                retorno = texto
            End If
            Return retorno


            'teste 0
            'Dim txt As List(Of String)
            'Dim retorno As String = ""


            'txt = texto.Split(vbCrLf).ToList


            'txt.RemoveAt(txt.Count - 1)

            'For Each linha As String In txt
            '    retorno += linha

            'Next

            'Return retorno

            'tyeste 1
            '     For ct As Integer = texto.Length To 1 Step -1
            '         If texto.Substring(ct, 1) = vbCr Then

            '             Continue For
            '         ElseIf texto.Substring(ct, 1) = vbLf Then
            '             ' replace com nada
            '             Continue For
            'else
            '             'break porque eh um caractere valido
            '         End If
            '     Next

        Catch ex As Exception
            Throw ex
        End Try



    End Function
End Class
