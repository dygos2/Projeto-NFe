Public Class ClienteVO
    Public Sub New()

    End Sub
    Private _codigoCliente As Integer
    Private _nomeDaEmpresa As String
    Private _CNPJ As String
    Private _ativo As Boolean

    Public Property codigoCliente() As Integer
        Get
            Return _codigoCliente
        End Get
        Set(ByVal value As Integer)
            _codigoCliente = value
        End Set
    End Property

    Public Property nomeDaEmpresa() As String
        Get
            Return _nomeDaEmpresa
        End Get
        Set(ByVal value As String)
            _nomeDaEmpresa = value
        End Set
    End Property

    Public Property CNPJ() As String
        Get
            Return _CNPJ
        End Get
        Set(ByVal value As String)
            _CNPJ = value
        End Set
    End Property
    Public Property ativo() As Boolean
        Get
            Return _ativo
        End Get
        Set(ByVal value As Boolean)
            _ativo = value
        End Set
    End Property
End Class
