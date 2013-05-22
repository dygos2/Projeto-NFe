Public Class usuarioVO
    Private _idUsuario As Integer
    Private _email As String
    Private _senha As String
    Private _tipo As Integer
    Private _nome As String

    Public Property idUsuario() As Integer
        Get
            Return _idUsuario
        End Get
        Set(ByVal value As Integer)
            _idUsuario = value
        End Set
    End Property
    Public Property email() As String
        Get
            Return _email
        End Get
        Set(ByVal value As String)
            _email = value
        End Set
    End Property

    Public Property senha() As String
        Get
            Return _senha
        End Get
        Set(ByVal value As String)
            _senha = value
        End Set
    End Property
    Public Property tipo() As Integer
        Get
            Return _tipo
        End Get
        Set(ByVal value As Integer)
            _tipo = value
        End Set
    End Property

    Public Property nome() As String
        Get
            Return _nome
        End Get
        Set(ByVal value As String)
            _nome = value
        End Set
    End Property
End Class
