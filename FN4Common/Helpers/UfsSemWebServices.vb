Namespace Helpers
    Public Class UfsSemWebServices
        Public Shared ReadOnly Property SVRS As List(Of String)
            Get
                Dim _ufs As List(Of String) = New List(Of String)(New String() {"AC", "AL", "AM", "AP", "DF", "PB", "RJ", "RO", "RR", "SC", "SE", "TO"})
                Return _ufs
            End Get
        End Property

        Public Shared ReadOnly Property SVAN As List(Of String)
            Get
                Dim _ufs As List(Of String) = New List(Of String)(New String() {"ES", "MA", "PA", "PI", "RN"})
                Return _ufs
            End Get
        End Property
    End Class
End Namespace