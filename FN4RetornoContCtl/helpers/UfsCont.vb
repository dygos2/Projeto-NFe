Public Class UfsCont
    Public Shared ReadOnly Property SVCRS As List(Of String)
        Get
            Dim _ufs As List(Of String) = New List(Of String)(New String() {"AM", "BA", "CE", "GO", "MA", "MS", "MT", "PA", "PE", "PI", "PR"})
            Return _ufs
        End Get
    End Property

    Public Shared ReadOnly Property SVCAN As List(Of String)
        Get
            Dim _ufs As List(Of String) = New List(Of String)(New String() {"AC", "AL", "AP", "DF", "ES", "MG", "PB", "RJ", "RN", "RO", "RR", "RS", "SC", "SE", "SP", "TO"})
            Return _ufs
        End Get
    End Property
End Class