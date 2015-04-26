Imports System.Xml
Imports System.IO
Imports System.Security.Cryptography.X509Certificates
Imports System.Text.RegularExpressions

Public Class Geral

    Private Shared _parametro As New XmlDocument

    Public Shared ReadOnly Property Parametro(ByVal nome As String) As String
        Get
            If _parametro.BaseURI = "" Then
                Dim uri As New Uri(System.Reflection.Assembly.GetExecutingAssembly.GetName.CodeBase)
                Dim caminho As String = Path.GetDirectoryName(uri.LocalPath)
                _parametro.Load(caminho & "\XML\FN4Config.xml")
            End If

            Return _parametro.SelectSingleNode("/Fisconet4.Settings/setting[@name='" & nome & "']/value").InnerText

        End Get
    End Property

    Public Shared ReadOnly Property Parametro(ByVal nome As String, ByVal CNPJ As String) As String
        Get
            Dim valor As String = Parametro(nome)
            If _parametro.BaseURI = "" Then
                Dim uri As New Uri(System.Reflection.Assembly.GetExecutingAssembly.GetName.CodeBase)
                Dim caminho As String = Path.GetDirectoryName(uri.LocalPath)
                _parametro.Load(caminho & "\XML\FN4Config.xml")
            End If

            Return _parametro.SelectSingleNode("/Fisconet4.Settings/setting[@name='" & nome & CNPJ & "']/value").InnerText
        End Get
    End Property


    Public Shared Function ObterExceptionMessagesEmCascata(ByVal ex As Exception) As String
        Dim saida As String

        saida = ex.Message & vbCrLf

        If Not ex.InnerException Is Nothing Then
            saida = saida & ObterExceptionMessagesEmCascata(ex.InnerException)
        End If

        Return saida
    End Function

    Public Shared Function ObterStackTraceEmCascata(ByVal ex As Exception) As String
        Dim saida As String

        saida = ex.StackTrace & vbCrLf

        If Not ex.InnerException Is Nothing Then
            saida = saida & ObterStackTraceEmCascata(ex.InnerException)
        End If

        Return saida
    End Function

    Public Shared Function ValidarFormatoDeEmail(ByVal email As String) As Boolean
        Dim padrao As String = "^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
        Dim verificacao As Match = Regex.Match(email, padrao)

        Return verificacao.Success
    End Function

    ''' <summary>
    ''' Returns the missing numbers in a sequence.   
    ''' </summary>   
    ''' <param name="strNumbers">Expects a string of comma-delimited numbers. ex: "1,2,4,6,7"</param>   
    ''' <returns></returns>   
    ''' <remarks></remarks>   
    Public Shared Function FindMissingNumbers(ByVal strNumbers As String) As List(Of Integer)
        Dim MissingNumbers As New List(Of Integer)
        Dim arNumbers() As String
        arNumbers = Split(strNumbers, ",")
        arNumbers = SortNumbers(arNumbers)

        For I = 0 To UBound(arNumbers) - 1
            If CLng(arNumbers(I)) + 1 <> CLng(arNumbers(I + 1)) Then
                For J = 1 To (CLng(arNumbers(I + 1)) - CLng(arNumbers(I))) - 1
                    MissingNumbers.Add(CStr(CLng(arNumbers(I)) + J))
                Next
            End If
        Next

        Return MissingNumbers
    End Function

    ''' <summary>  
    ''' Sorts the array of numbers in value order, least to greatest.  
    ''' </summary>  
    ''' <param name="arNumbers">Send in a string() array of numbers</param>  
    ''' <returns></returns>  
    ''' <remarks></remarks>  
    Private Shared Function SortNumbers(ByVal arNumbers() As String) As String()
        Dim tmpNumber As String
        For J = 0 To UBound(arNumbers) - 1
            For I = J + 1 To UBound(arNumbers)
                If arNumbers(I) - arNumbers(J) < 0 Then
                    tmpNumber = arNumbers(J)
                    arNumbers(J) = arNumbers(J)
                    arNumbers(I) = tmpNumber
                End If
            Next
        Next

        Return arNumbers
    End Function

    Private Shared Function SortNumbers(ByVal numbers As List(Of Integer)) As List(Of Integer)
        Dim tmpNumber As String

        For J = 0 To numbers.Count
            For I = J + 1 To numbers.Count
                If numbers(I) - numbers(J) < 0 Then
                    tmpNumber = numbers(J)
                    numbers(J) = numbers(J)
                    numbers(I) = tmpNumber
                End If
            Next
        Next

        Return numbers
    End Function
End Class
