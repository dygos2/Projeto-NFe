Public Class utcVO

    Private _uf As String
    Private _utc As String
    Private _versao_canc As Integer
    
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
