Public Class Pedidos_ItensVO


    Private _NFe_emit_CNPJ As String
    Private _fk_id_plataforma As Integer
    Private _num_pedido As String
    Private _cProd As String
    Private _cEAN As String
    Private _xProd As String
    Private _cfop As String
    Private _Orig As Integer
    Private _NCM As String
    Private _CST As Integer
    Private _CSOSN As Integer
    Private _uCom As String
    Private _qCom As Double
    Private _subst As Integer
    Private _vUnCom As Double
    Private _vFrete As Double
    Private _vDesc As Double
    Private _modFrete As Integer
    Private _CNPJ As String
    Private _CPF As String
    Private _xNome As String
    Private _IE As String
    Private _xEnder As String
    Private _xMun As String
    Private _UF As String
    Private _placa As String
    Private _UF_veic As String


    Public Property NFe_emit_CNPJ() As String
        Get
            Return _NFe_emit_CNPJ
        End Get
        Set(ByVal value As String)
            _NFe_emit_CNPJ = value
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

    Public Property num_pedido() As String

        Get
            Return _num_pedido
        End Get
        Set(ByVal value As String)
            _num_pedido = value
        End Set
    End Property


    Public Property cProd() As String
        Get
            Return _cProd
        End Get
        Set(ByVal value As String)
            _cProd = value
        End Set
    End Property

    Public Property cEAN() As String
        Get
            Return _cEAN
        End Get
        Set(ByVal value As String)
            _cEAN = value
        End Set
    End Property

    Public Property xProd() As String
        Get
            Return _xProd
        End Get
        Set(ByVal value As String)
            _xProd = value
        End Set
    End Property


    Public Property cfop() As Integer
        Get
            Return _cfop
        End Get
        Set(ByVal value As Integer)
            _cfop = value
        End Set
    End Property

    Public Property Orig() As Integer
        Get
            Return _Orig
        End Get
        Set(ByVal value As Integer)
            _Orig = value
        End Set
    End Property

    Public Property NCM() As String
        Get
            Return _NCM
        End Get
        Set(ByVal value As String)
            _NCM = value
        End Set
    End Property


    Public Property CST() As Integer
        Get
            Return _CST
        End Get
        Set(ByVal value As Integer)
            _CST = value
        End Set
    End Property

    Public Property CSOSN() As Integer
        Get
            Return _CSOSN
        End Get
        Set(ByVal value As Integer)
            _CSOSN = value
        End Set
    End Property

    Public Property uCom() As String
        Get
            Return _uCom
        End Get
        Set(ByVal value As String)
            _uCom = value
        End Set
    End Property

    Public Property qCom() As Double
        Get
            Return _qCom
        End Get
        Set(ByVal value As Double)
            _qCom = value
        End Set
    End Property


    Public Property subst() As Integer
        Get
            Return _subst
        End Get
        Set(ByVal value As Integer)
            _subst = value
        End Set
    End Property

    Public Property vUnCom() As Double
        Get
            Return _vUnCom
        End Get
        Set(ByVal value As Double)
            _vUnCom = value
        End Set
    End Property

    Public Property vFrete() As Double
        Get
            Return _vFrete
        End Get
        Set(ByVal value As Double)
            _vFrete = value
        End Set
    End Property


    Public Property vDesc() As Double
        Get
            Return _vDesc
        End Get
        Set(ByVal value As Double)
            _vDesc = value
        End Set
    End Property

    Public Property modFrete() As Integer
        Get
            Return _modFrete
        End Get
        Set(ByVal value As Integer)
            _modFrete = value
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

    Public Property xNome() As String
        Get
            Return _xNome
        End Get
        Set(ByVal value As String)
            _xNome = value
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

    Public Property xEnder() As String
        Get
            Return _xEnder
        End Get
        Set(ByVal value As String)
            _xEnder = value
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

    Public Property placa() As String
        Get
            Return _placa
        End Get
        Set(ByVal value As String)
            _placa = value
        End Set
    End Property

    Public Property UF_veic() As String
        Get
            Return _UF_veic
        End Get
        Set(ByVal value As String)
            _UF_veic = value
        End Set
    End Property
End Class
