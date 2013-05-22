Public Class configuracaoVO
    Private _idConfiguracao As Integer
    Private _chave As String
    Private _valor As String

    Public Property idConfiguracao() As Integer
        Get
            Return _idConfiguracao
        End Get
        Set(ByVal value As Integer)
            _idConfiguracao = value
        End Set
    End Property

    Public Property chave() As String
        Get
            Return _chave
        End Get
        Set(ByVal value As String)
            _chave = value
        End Set
    End Property

    Public Property valor() As String
        Get
            Return _valor
        End Get
        Set(ByVal value As String)
            _valor = value
        End Set
    End Property
End Class