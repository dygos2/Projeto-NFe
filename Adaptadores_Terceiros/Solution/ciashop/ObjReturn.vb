Public Class ObjReturn
    Dim _novo = 0
    Dim _atualizados = 0
    Dim _sincronizados = 0
    Dim _baixados = 0
    Dim _erro = ""

    Public Property novo() As Integer
        Get
            Return _novo
        End Get
        Set(ByVal value As Integer)
            _novo = value
        End Set
    End Property

    Public Property atualizados() As Integer
        Get
            Return _atualizados
        End Get
        Set(ByVal value As Integer)
            _atualizados = value
        End Set
    End Property

    Public Property sincronizados() As Integer
        Get
            Return _sincronizados
        End Get
        Set(ByVal value As Integer)
            _sincronizados = value
        End Set
    End Property

    Public Property baixados() As Integer
        Get
            Return _baixados
        End Get
        Set(ByVal value As Integer)
            _baixados = value
        End Set
    End Property

    Public Property erro() As String
        Get
            Return _erro
        End Get
        Set(ByVal value As String)
            _erro = value
        End Set
    End Property
End Class
