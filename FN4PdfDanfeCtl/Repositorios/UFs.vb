Namespace Repositorios
    Public Class UFs
        Public Shared ReadOnly Property ListaDeCodigos As Hashtable
            Get
                Dim ht As New Hashtable

                ht.Add("MS", 50)
                ht.Add("SP", 35)

                Return ht
            End Get
        End Property
    End Class
End Namespace