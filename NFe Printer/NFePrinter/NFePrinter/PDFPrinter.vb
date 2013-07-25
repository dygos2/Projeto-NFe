Imports System.Runtime.InteropServices
Imports System.IO

Public Class PDFPrinter

#Region " CONSTANTS "
    Private Const SW_SHOWNORMAL As Integer = 2
#End Region

#Region " API "
    <DllImport("shell32")> _
    Public Shared Function ShellExecute(ByVal hWnd As IntPtr, _
                                        ByVal lpOperation As String, _
                                        ByVal lpFile As String, _
                                        ByVal lpParameters As String, _
                                        ByVal lpDirectory As String, _
                                        ByVal nShowCmd As Integer) As IntPtr
    End Function
#End Region

#Region " PUBLIC MEMBERS "
    Public Function PrintPDF(ByVal FilePath As String) As Boolean
        If IO.File.Exists(FilePath) Then
            If ShellExecute(1, "Print", FilePath, "", _
            Directory.GetDirectoryRoot(FilePath), SW_SHOWNORMAL).ToInt32 <= 32 Then
                Return False
            Else
                Return True
            End If
        Else
            Return False
        End If
    End Function
#End Region
End Class
