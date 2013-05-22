Public Class clientesVO

    Private _nome As String
    Private _email As String
    Private _idlogin As Long
    Private _idEmpresa As Long
    Private _nomeEmpresa As String
    Private _data_vencimento As Date
    Private _cert_fim As Date
    Private _dias_exp As Long
    Private _dias_vencidos As Long
    Private _cnpj As String
    Private _id_fk_produtos_status As Long
    Private _frest As Long
    Private _prest As Long
    Private _token As String
    Private _idempresa_Usuarios As Long

    Public Sub New()
        Me.nome = ""
        Me.email = ""
        Me.idlogin = 0
    End Sub
    Public Property idempresa_Usuarios() As Long
        Get
            Return _idempresa_Usuarios
        End Get
        Set(ByVal value As Long)
            _idempresa_Usuarios = value
        End Set
    End Property
    Public Property frest() As Long
        Get
            Return _frest
        End Get
        Set(ByVal value As Long)
            _frest = value
        End Set
    End Property
    Public Property token() As String
        Get
            Return _token
        End Get
        Set(ByVal value As String)
            _token = value
        End Set
    End Property
    Public Property prest() As Long
        Get
            Return _prest
        End Get
        Set(ByVal value As Long)
            _prest = value
        End Set
    End Property
    Public Property id_fk_produtos_status() As Long
        Get
            Return _id_fk_produtos_status
        End Get
        Set(ByVal value As Long)
            _id_fk_produtos_status = value
        End Set
    End Property
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
    Public Property cnpj() As String
        Get
            Return _cnpj
        End Get
        Set(ByVal value As String)
            _cnpj = value
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
    Public Property dias_exp() As Long
        Get
            Return _dias_exp
        End Get
        Set(ByVal value As Long)
            _dias_exp = value
        End Set
    End Property
    Public Property cert_fim() As Date
        Get
            Return _cert_fim
        End Get
        Set(ByVal value As Date)
            _cert_fim = value
        End Set
    End Property
    Public Property dias_vencidos() As Long
        Get
            Return _dias_vencidos
        End Get
        Set(ByVal value As Long)
            _dias_vencidos = value
        End Set
    End Property

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class
