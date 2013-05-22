Imports System.Security.Cryptography
Imports System.Text

Namespace Helpers
    Public Class Seguranca

        Public Shared Function CompararMD5(ByVal textPlain As String, ByVal textMD5 As String) As Boolean
            Dim textPlainInMd5 As String = GerarMD5(textPlain)

            Return textMD5.Equals(textPlainInMd5.ToString())
        End Function

        Public Shared Function GerarMD5(ByVal textPlain As String) As String
            Dim provider As MD5 = MD5.Create()

            Dim hashData As Byte() = provider.ComputeHash(Encoding.Default.GetBytes(textPlain))

            Dim retorno As New StringBuilder

            For i As Integer = 0 To hashData.Length - 1 Step 1
                retorno.Append(hashData(i).ToString())
            Next

            Return retorno.ToString()
        End Function
    End Class
End Namespace