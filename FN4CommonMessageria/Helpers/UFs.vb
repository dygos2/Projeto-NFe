Namespace Helpers
    Public Class UFs
        Public Shared ReadOnly Property ListaDeCodigos As Hashtable
            Get
                Dim ht As New Hashtable

                ht.Add("RO", 11)
                ht.Add("AC", 12)
                ht.Add("AM", 13)
                ht.Add("RR", 14)
                ht.Add("PA", 15)
                ht.Add("AP", 16)
                ht.Add("TO", 17)
                ht.Add("MA", 21)
                ht.Add("PI", 22)
                ht.Add("CE", 23)
                ht.Add("RN", 24)
                ht.Add("PB", 25)
                ht.Add("PE", 26)
                ht.Add("AL", 27)
                ht.Add("SE", 28)
                ht.Add("BA", 29)
                ht.Add("MG", 31)
                ht.Add("ES", 32)
                ht.Add("RJ", 33)
                ht.Add("SP", 35)
                ht.Add("PR", 41)
                ht.Add("SC", 42)
                ht.Add("RS", 43)
                ht.Add("MS", 50)
                ht.Add("MT", 51)
                ht.Add("GO", 52)
                ht.Add("DF", 53)

                Return ht
            End Get
        End Property
    End Class
End Namespace