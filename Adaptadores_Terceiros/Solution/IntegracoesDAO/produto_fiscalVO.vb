Public Class produto_fiscalVO
    Private _NFe_emit_CNPJ As String
    Private _cProd As String
    Private _xProd As String
    Private _NCM As Integer
    Private _uCom As String
    Private _subst As Integer
    Private _fk_id_plataforma As Integer
    Private _orig As Integer
    Private _fk_cadastros_fiscais As Integer


    Public Property NFe_emit_CNPJ() As String
        Get
            Return _NFe_emit_CNPJ
        End Get
        Set(ByVal value As String)
            _NFe_emit_CNPJ = value
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

    Public Property xProd() As String
        Get
            Return _xProd
        End Get
        Set(ByVal value As String)
            _xProd = value
        End Set
    End Property

    Public Property NCM() As Integer
        Get
            Return _NCM
        End Get
        Set(ByVal value As Integer)
            _NCM = value
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

    Public Property subst() As Integer
        Get
            Return _subst
        End Get
        Set(ByVal value As Integer)
            _subst = value
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

    Public Property orig() As Integer
        Get
            Return _orig
        End Get
        Set(ByVal value As Integer)
            _orig = value
        End Set
    End Property

    Public Property fk_cadastros_fiscais() As Integer
        Get
            Return _fk_cadastros_fiscais
        End Get
        Set(ByVal value As Integer)
            _fk_cadastros_fiscais = value
        End Set
    End Property

End Class
