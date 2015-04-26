Public Class EmpresaVO

    Private _contador As Integer

    Public Property contador() As String
        Get
            Return _contador
        End Get
        Set(ByVal value As String)
            _contador = value
        End Set
    End Property

End Class
