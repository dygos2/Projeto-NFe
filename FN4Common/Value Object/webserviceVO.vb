Public Class webserviceVO
    Private _idWebservice As Integer
    Private _uf As String
    Private _nome As String
    Private _versao As String
    Private _url As String
    Private _homologacao As String
    Private _contingencia As String

    Public Property idWebservice() As Integer
        Get
            Return _idWebservice
        End Get
        Set(ByVal value As Integer)
            _idWebservice = value
        End Set
    End Property

    Public Property uf() As String
        Get
            Return _uf
        End Get
        Set(ByVal value As String)
            _uf = value
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

    Public Property versao() As String
        Get
            Return _versao
        End Get
        Set(ByVal value As String)
            _versao = value
        End Set
    End Property

    Public Property url() As String
        Get
            Return _url
        End Get
        Set(ByVal value As String)
            _url = value
        End Set
    End Property

    Public Property homologacao() As String
        Get
            Return _homologacao
        End Get
        Set(ByVal value As String)
            _homologacao = value
        End Set
    End Property

    Public Property contingencia() As String
        Get
            Return _contingencia
        End Get
        Set(ByVal value As String)
            _contingencia = value
        End Set
    End Property
End Class