Public Class eventoVO

    Private _infEvento_nSeqEvento As Integer
    Private _NFe_infNFe_id As String
    Private _infEvento_tpEvento As Integer
    Private _statusEvento As Integer
    Private _NFe_emit_CNPJ As String
    Private _infEvento_dhEvento As String
    Private _infEvento_detEvento_xCorrecao As String
    Private _retEvento_cStat As Integer
    Private _retEvento_xMotivo As String
    Private _controleLote As Integer


    Public Property infEvento_nSeqEvento() As Integer
        Get
            Return _infEvento_nSeqEvento
        End Get
        Set(ByVal value As Integer)
            _infEvento_nSeqEvento = value
        End Set
    End Property

    Public Property NFe_infNFe_id() As String
        Get
            Return _NFe_infNFe_id
        End Get
        Set(ByVal value As String)
            _NFe_infNFe_id = value
        End Set
    End Property

    Public Property infEvento_tpEvento() As Integer
        Get
            Return _infEvento_tpEvento
        End Get
        Set(ByVal value As Integer)
            _infEvento_tpEvento = value
        End Set
    End Property

    Public Property infEvento_dhEvento() As String
        Get
            Return _infEvento_dhEvento
        End Get
        Set(ByVal value As String)
            _infEvento_dhEvento = value
        End Set
    End Property


    Public Property statusEvento() As Integer
        Get
            Return _statusEvento
        End Get
        Set(ByVal value As Integer)
            _statusEvento = value
        End Set
    End Property


    Public Property NFe_emit_CNPJ() As String
        Get
            Return _NFe_emit_CNPJ
        End Get
        Set(ByVal value As String)
            _NFe_emit_CNPJ = value
        End Set
    End Property


    Public Property infEvento_detEvento_xCorrecao() As String
        Get
            Return _infEvento_detEvento_xCorrecao
        End Get
        Set(ByVal value As String)
            _infEvento_detEvento_xCorrecao = value
        End Set
    End Property


    Public Property retEvento_cStat() As Integer
        Get
            Return _retEvento_cStat
        End Get
        Set(ByVal value As Integer)
            _retEvento_cStat = value
        End Set
    End Property

    Public Property retEvento_xMotivo() As String
        Get
            Return _retEvento_xMotivo
        End Get
        Set(ByVal value As String)
            _retEvento_xMotivo = value
        End Set
    End Property

    Public Property controleLote() As Integer
        Get
            Return _controleLote
        End Get
        Set(ByVal value As Integer)
            _controleLote = value
        End Set
    End Property

End Class
