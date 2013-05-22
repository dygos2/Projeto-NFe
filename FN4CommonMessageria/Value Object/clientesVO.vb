Public Class clientesVO

    Private _nome As String
    Private _email As String
    Private _idlogin As Long
    Private _idEmpresa As Long
    Private _nomeEmpresa As String
    Private _data_vencimento As Date

    Public Sub New()
        Me.nome = ""
        Me.email = ""
        Me.idlogin = 0
    End Sub
    Public Property data_vencimento() As Date
        Get
            Return _data_vencimento
        End Get
        Set(ByVal value As Date)
            _data_vencimento = value
        End Set
    End Property
    Public Property idEmpresa() As Long
        Get
            Return _idEmpresa
        End Get
        Set(ByVal value As Long)
            _idEmpresa = value
        End Set
    End Property
    Public Property nomeEmpresa() As String
        Get
            Return _nomeEmpresa
        End Get
        Set(ByVal value As String)
            _nomeEmpresa = value
        End Set
    End Property
    Public Property nome() As String
        Get
            Return _nome
        End Get
        Set(ByVal value As String)
            _nome = value
        End Set
    End Property
    Public Property email() As String
        Get
            Return _email
        End Get
        Set(ByVal value As String)
            _email = value
        End Set
    End Property
    Public Property idlogin() As Long
        Get
            Return _idlogin
        End Get
        Set(ByVal value As Long)
            _idlogin = value
        End Set
    End Property
End Class
