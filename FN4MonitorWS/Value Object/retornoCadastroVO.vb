Public Class retornoCadastroVO
    Private _idEmpresa As Integer
    Private _token As String
    Private _mensagem As string


    Public Property idEmpresa() As Integer
        Get
            Return _idEmpresa
        End Get
        Set(ByVal value As Integer)
            _idEmpresa = value
        End Set
    End Property

    Public Property token() As String
        Get
            Return _token
        End Get
        Set(ByVal value As String)
            _token = value
        End Set
    End Property

    Public Property mensagem() As String
        Get
            Return _mensagem
        End Get
        Set(ByVal value As String)
            _mensagem = value
        End Set
    End Property
End Class
