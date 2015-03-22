Imports System.IO

Public Class Log
    Public Shared Sub CriaLog(ByVal mensagem As String)
        Dim data As String
        data = DirectCast(Date.Today.Day.ToString, String) + "-" + DirectCast(Date.Today.Month.ToString, String) +
            "-" + DirectCast(Date.Today.Year.ToString, String)
        Dim sw As New StreamWriter("C:\Log\" + data + ".txt", True)
        With sw
            .WriteLine("Data: " & DateTime.Now())
            .WriteLine("Descrição do Log: " + mensagem)
            .WriteLine("---")
            .Flush()
            .Dispose()
        End With
    End Sub
End Class
