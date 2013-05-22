Imports FN4Common
Imports System.IO
Imports FN4Common.DataAccess
Imports System.Threading

Public Class envio_emails
    Dim ranges_envios(3) As Array
    Dim ranges_ids(3) As Array

    Private WithEvents tm As New System.Timers.Timer
    Public Sub New()
        
        Dim ids(0) As Integer
        ids(0) = 0

        ranges_ids(0) = ids
        ranges_ids(1) = ids
        ranges_ids(2) = ids
        ranges_ids(3) = ids

        ranges_envios(0) = New Integer() {50, 99}
        ranges_envios(1) = New Integer() {6, 49}
        ranges_envios(2) = New Integer() {1, 5}
        ranges_envios(3) = New Integer() {0, 0}

        tm.Interval = Geral.Parametro("intervaloDeVarreduraDeEmails")
        tm.AutoReset = True

    End Sub

    Public Sub run()

        Log.registrarInfo("Monitor de messageria iniciado", "MessageriaService")
        'rodando primeiro e depois iniciando o timer para 1 dia
        enviarEmails()
        tm.Start()

    End Sub
    Public Sub pause()
        Log.registrarInfo("Monitor de messageria parado", "MessageriaService")
        tm.Stop()
    End Sub

    Private Sub executarEnvioDeEmail() Handles tm.Elapsed
        tm.Stop()
        enviarEmails()
        tm.Start()
    End Sub
    Public Function checar_envio(ByVal id As Integer, ByVal pfrest As Long) As Boolean
        Dim tmpexit, achou As Boolean
        tmpexit = False
        checar_envio = False
        achou = False

        'verificando se o id já está nos arrays
        For i = 0 To 3
            For x = 0 To ranges_ids(i).Length - 1
                If id = ranges_ids(i)(x) Then
                    achou = True
                    'se já está no array do mesmo range, ou seja, já enviou o email daquele range, nao envia de novo
                    If pfrest >= ranges_envios(i)(0) And pfrest <= ranges_envios(i)(1) Then
                        'deixa o id no array do range e nao envia de novo
                        checar_envio = False
                        tmpexit = True
                        Exit For
                    Else
                        'deleta o id do array
                        ranges_ids(i) = RemoveAt(ranges_ids(i), x)
                        'muda o id para o range do pacote e retorna true enviando email
                        For p = 0 To ranges_envios.Length - 1
                            If pfrest >= ranges_envios(p)(0) And pfrest <= ranges_envios(p)(1) Then
                                ranges_ids(p) = addarr(ranges_ids(p), id)
                                Exit For
                            End If
                        Next
                        checar_envio = True
                        tmpexit = True
                        Exit For
                    End If
                End If
            Next
            If tmpexit Then
                Exit For
            End If
        Next

        If Not achou Then 'nao tem id, criar nos registros e mandar email
            For p = 0 To ranges_envios.Length - 1
                If pfrest >= ranges_envios(p)(0) And pfrest <= ranges_envios(p)(1) Then
                    ranges_ids(p) = addarr(ranges_ids(p), id)
                    Exit For
                End If
            Next
            checar_envio = True
        End If

    End Function
    Function RemoveAt(ByVal arr As Array, ByVal index As Integer) As Array
        Dim uBound = arr.GetUpperBound(0)
        Dim lBound = arr.GetLowerBound(0)
        Dim arrLen = uBound - lBound

        If index < lBound OrElse index > uBound Then
            Throw New ArgumentOutOfRangeException( _
            String.Format("Index must be from {0} to {1}.", lBound, uBound))

        Else
            'create an array 1 element less than the input array
            Dim outArr(arrLen - 1) As Integer
            'copy the first part of the input array
            Array.Copy(arr, 0, outArr, 0, index)
            'then copy the second part of the input array
            Array.Copy(arr, index + 1, outArr, index, uBound - index)

            Return outArr
        End If
    End Function
    Function addarr(ByVal arr As Array, ByVal id As Integer) As Array

        Dim uBound = arr.GetUpperBound(0)
        Dim lBound = arr.GetLowerBound(0)
        Dim arrLen = uBound - lBound

        Dim arr_temp(uBound + 1) As Integer

        For y = 0 To arr.Length - 1
            arr_temp(y) = arr(y)
        Next

        arr_temp(arr_temp.GetUpperBound(0)) = id
        addarr = arr_temp

    End Function

    Private Sub enviarEmails()
        'envio de msgs para pacotes em expiração
        Dim ex As Exception
        Try

            Dim clientes As List(Of clientesVO)
            Dim cliente As clientesVO
            Dim corpo As String
            Dim str As StreamReader

            Dim uri As New Uri(System.Reflection.Assembly.GetExecutingAssembly.GetName.CodeBase)
            Dim caminho As String = Path.GetDirectoryName(uri.LocalPath)

            Log.registrarInfo("Rodando verificação de pacotes", "MessageriaService")
            clientes = clientesDao.obterClientesPacotesaExpirar

            For Each cliente In clientes
                Try
                    If checar_envio(cliente.idempresa_Usuarios, cliente.prest + cliente.frest) Then

                        If cliente.prest + cliente.frest > 0 Then
                            str = New StreamReader(caminho & "\body.html")
                            corpo = str.ReadToEnd
                            str.Close()
                            str.Dispose()

                            enviarEmail(cliente, corpo)
                            Log.registrarInfo("Envio de Alerta: " & cliente.nomeEmpresa & " - " & cliente.frest + cliente.prest & " notas restantes", "MessageriaService")
                        Else
                            str = New StreamReader(caminho & "\body2.html")
                            corpo = str.ReadToEnd
                            str.Close()
                            str.Dispose()

                            enviarEmail(cliente, corpo)
                            Log.registrarInfo("[Urgente] Envio de Alerta: " & cliente.nomeEmpresa & " - " & cliente.frest + cliente.prest & " notas restantes", "MessageriaService")
                        End If
                    End If

                Catch e As Exception
                    ex = e
                    Log.registrarErro("Erro inesperado: " & Geral.ObterExceptionMessagesEmCascata(ex) & vbCrLf & ex.StackTrace, "MessageriaService")
                End Try
            Next
        Catch exception As Exception
            Log.registrarErro("Erro inesperado: " & Geral.ObterExceptionMessagesEmCascata(exception), "MessageriaService")
        Finally

        End Try
    End Sub

    Private Sub enviarEmail(ByVal cliente As clientesVO, ByVal template As String)

        Dim assunto As String
        Dim corpo As String
        Dim destinatarios As New List(Of String)

        assunto = "NFe4Web :: Alerta de Processamento"
        corpo = template

        corpo = corpo.Replace("@url_pagamento", "http://www.nfe4web.com.br/manager/creditos_logar.asp?email=@email&token=@token")
        corpo = corpo.Replace("@frest", cliente.frest)
        corpo = corpo.Replace("@prest", cliente.prest)
        corpo = corpo.Replace("@total", cliente.frest + cliente.prest)
        corpo = corpo.Replace("@email", cliente.email)
        corpo = corpo.Replace("@idEmpresa", cliente.idEmpresa)
        corpo = corpo.Replace("@token", cliente.token)
        corpo = corpo.Replace("@nomeCliente", cliente.nome)
        corpo = corpo.Replace("@nomeEmpresa", cliente.nomeEmpresa)
        corpo = corpo.Replace("@cnpj", cliente.cnpj)

        'incluindo os emails
        'destinatarios.Add("comercial@megaideas.net")
        destinatarios.Add(cliente.email)

        Email.Enviar(destinatarios, assunto, corpo)

    End Sub



End Class

