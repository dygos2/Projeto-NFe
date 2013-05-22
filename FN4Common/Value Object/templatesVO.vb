Public Class templatesVO

    Private _assunto As String
    Private _template As String

    Public Sub New()
    End Sub
    Public Property assunto() As String
        Get
            Return _assunto
        End Get
        Set(ByVal value As String)
            _assunto = value
        End Set
    End Property
    Public Property template() As String
        Get
            Return _template
        End Get
        Set(ByVal value As String)
            _template = value
        End Set
    End Property
End Class