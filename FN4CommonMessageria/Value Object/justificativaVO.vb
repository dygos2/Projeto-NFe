Public Class justificativaVO
    Private _idJustificativa As Integer
    Private _idNota As Integer
    Private _cnpj As String
    Private _serie As Integer
    Private _descricao As String

    Public Property idJustificativa As Integer
        Get
            Return _idJustificativa
        End Get
        Set(ByVal value As Integer)
            _idJustificativa = value
        End Set
    End Property

    Public Property idNota As Integer
        Get
            Return _idNota
        End Get
        Set(ByVal value As Integer)
            _idNota = value
        End Set
    End Property

    Public Property cnpj As String
        Get
            Return _cnpj
        End Get
        Set(ByVal value As String)
            _cnpj = value
        End Set
    End Property

    Public Property serie As Integer
        Get
            Return _serie
        End Get
        Set(ByVal value As Integer)
            _serie = value
        End Set
    End Property

    Public Property descricao As String
        Get
            Return _descricao
        End Get
        Set(ByVal value As String)
            _descricao = value
        End Set
    End Property
End Class
