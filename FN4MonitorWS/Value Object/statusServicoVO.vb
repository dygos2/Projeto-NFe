Public Class statusServicoVO
    Private _cStat As Integer
    Private _xMotivo As String
    Public Sub New()

    End Sub
    Public Sub New(ByVal stat As Integer, ByVal motivo As String)
        _xMotivo = motivo
        _cStat = stat
    End Sub
    Public Property cStat() As Integer
        Get
            Return _cStat
        End Get
        Set(ByVal value As Integer)
            _cStat = value
        End Set
    End Property
    Public Property xMotivo() As String
        Get
            Return _xMotivo
        End Get
        Set(ByVal value As String)
            _xMotivo = value
        End Set
    End Property
End Class
