Public Class retornoVO
    Private _tpRetorno As Integer
    Private _nmMensagem As Integer
    Private _mensagem As String

    Public Sub New()

    End Sub
    Public Sub New(ByVal tiporetorno As String, ByVal nummsg As String, ByVal msg As String)
        _tpRetorno = tiporetorno
        _nmMensagem = nummsg
        _mensagem = msg
    End Sub

    Public Property tpRetorno() As Integer
        Get
            Return _tpRetorno
        End Get
        Set(ByVal value As Integer)
            _tpRetorno = value
        End Set
    End Property
    Public Property nmMensagem() As Integer
        Get
            Return _nmMensagem
        End Get
        Set(ByVal value As Integer)
            _nmMensagem = value
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
