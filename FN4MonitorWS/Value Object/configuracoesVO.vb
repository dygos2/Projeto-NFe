Public Class configuracoesVO
    Private _atualizacaoEstatisticas As Integer
    Private _atualizacaoMonitoramento As Integer
    Private _gadget1 As Integer
    Private _gadget2 As Integer
    Private _gadget3 As Integer

    Public Sub New()

    End Sub
    Public Property gadget1() As Integer
        Get
            Return _gadget1
        End Get
        Set(ByVal value As Integer)
            _gadget1 = value
        End Set
    End Property
    Public Property gadget2() As Integer
        Get
            Return _gadget2
        End Get
        Set(ByVal value As Integer)
            _gadget2 = value
        End Set
    End Property
    Public Property gadget3() As Integer
        Get
            Return _gadget3
        End Get
        Set(ByVal value As Integer)
            _gadget3 = value
        End Set
    End Property
End Class
