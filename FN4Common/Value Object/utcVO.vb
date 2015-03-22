Public Class utcVO

    Private _uf As String
    Private _utc As String
    Private _versao_canc As Integer
    Private _versao_nfe As String
    Public Property versao_nfe() As String
        Get
            Return _versao_nfe
        End Get
        Set(ByVal value As String)
            _versao_nfe = value
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

    Public Property utc() As String
        Get
            Return _utc
        End Get
        Set(ByVal value As String)
            _utc = value
        End Set
    End Property
    Public Property versao_canc() As Integer
        Get
            Return _versao_canc
        End Get
        Set(ByVal value As Integer)
            _versao_canc = value
        End Set
    End Property

End Class
