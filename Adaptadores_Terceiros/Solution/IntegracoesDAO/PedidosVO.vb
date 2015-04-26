Public Class PedidosVO

    Private _NFe_emit_CNPJ As String
    Private _num_pedido As String
    Private _fk_id_plataforma As Integer
    Private _CNPJ As String
    Private _CPF As String
    Private _xName As String
    Private _xLgr As String
    Private _nro As String
    Private _xCpl As String
    Private _xBairro As String
    Private _cMun As Integer
    Private _xMun As String
    Private _UF As String
    Private _CEP As String
    Private _fone As String
    Private _IE As String
    Private _email As String
    Private _natop As String
    Private _indPag As Integer
    Private _xProcDate As DateTime
    Private _sincronizada As Integer
    Private _xQtyItems As Integer
    Private _fk_xOrderstatusID As Integer
    Private _tot_ped As String

    Public Property NFe_emit_CNPJ() As String
        Get
            Return _NFe_emit_CNPJ
        End Get
        Set(ByVal value As String)
            _NFe_emit_CNPJ = value
        End Set
    End Property



    Public Property num_pedido() As String
        Get
            Return _num_pedido
        End Get
        Set(ByVal value As String)
            _num_pedido = value
        End Set
    End Property

    Public Property fk_id_plataforma() As Integer
        Get
            Return _fk_id_plataforma
        End Get
        Set(ByVal value As Integer)
            _fk_id_plataforma = value
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

    Public Property CPF() As String
        Get
            Return _CPF
        End Get
        Set(ByVal value As String)
            _CPF = value
        End Set
    End Property

    Public Property xName() As String
        Get
            Return _xName
        End Get
        Set(ByVal value As String)
            _xName = value
        End Set
    End Property

    Public Property xLgr() As String
        Get
            Return _xLgr
        End Get
        Set(ByVal value As String)
            _xLgr = value
        End Set
    End Property

    Public Property nro() As String
        Get
            Return _nro
        End Get
        Set(ByVal value As String)
            _nro = value
        End Set
    End Property

    Public Property xCpl() As String
        Get
            Return _xCpl
        End Get
        Set(ByVal value As String)
            _xCpl = value
        End Set
    End Property

    Public Property xBairro() As String
        Get
            Return _xBairro
        End Get
        Set(ByVal value As String)
            _xBairro = value
        End Set
    End Property

    Public Property cMun() As Integer
        Get
            Return _cMun
        End Get
        Set(ByVal value As Integer)
            _cMun = value
        End Set
    End Property

    Public Property xMun() As String
        Get
            Return _xMun
        End Get
        Set(ByVal value As String)
            _xMun = value
        End Set
    End Property

    Public Property UF() As String
        Get
            Return _UF
        End Get
        Set(ByVal value As String)
            _UF = value
        End Set
    End Property

    Public Property CEP() As String
        Get
            Return _CEP
        End Get
        Set(ByVal value As String)
            _CEP = value
        End Set
    End Property

    Public Property fone() As String
        Get
            Return _fone
        End Get
        Set(ByVal value As String)
            _fone = value
        End Set
    End Property

    Public Property IE() As String
        Get
            Return _IE
        End Get
        Set(ByVal value As String)
            _IE = value
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

    Public Property natop() As String
        Get
            Return _natop
        End Get
        Set(ByVal value As String)
            _natop = value
        End Set
    End Property

    Public Property indPag() As Integer
        Get
            Return _indPag
        End Get
        Set(ByVal value As Integer)
            _indPag = value
        End Set
    End Property

    Public Property xProcDate() As DateTime
        Get
            Return _xProcDate
        End Get
        Set(ByVal value As DateTime)
            _xProcDate = value
        End Set
    End Property

    Public Property sincronizada() As Integer
        Get
            Return _sincronizada
        End Get
        Set(ByVal value As Integer)
            _sincronizada = value
        End Set
    End Property

    Public Property xQtyItems() As Integer
        Get
            Return _xQtyItems
        End Get
        Set(ByVal value As Integer)
            _xQtyItems = value
        End Set
    End Property


    Public Property fk_xOrderstatusID() As Integer
        Get
            Return _fk_xOrderstatusID
        End Get
        Set(ByVal value As Integer)
            _fk_xOrderstatusID = value
        End Set
    End Property


    Public Property tot_ped() As String
        Get
            Return _tot_ped
        End Get
        Set(ByVal value As String)
            _tot_ped = value
        End Set
    End Property

End Class
