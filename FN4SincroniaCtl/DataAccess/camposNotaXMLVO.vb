Public Class camposNotaXMLVO

    Private _nomeCli As String
    Private _CPFcli As String
    Private _endCli As String
    Private _nrEndCli As String
    Private _complEnd As String
    Private _bairroCli As String
    Private _cidadeCli As String
    Private _estCli As String
    Private _cepCli As String
    Private _DDD As String
    Private _telefone As String
    Private _nrNota As Long
    Private _serieNota As Integer
    Private _dtNota As String
    Private _vlNFeTot As String
    Private _vlProdsTot As String
    Private _CFOP As Integer
    Private _NFeChave As String
    Private _qtd As Integer
    Private _EAN As String
    Private _partNumber As String
    Private _vlProduto As String
    Private _serviceType As String
    Private _giftPackage As Integer

    Public Property nomeCli() As String
        Get
            Return _nomeCli
        End Get
        Set(ByVal value As String)
            _nomeCli = value
        End Set
    End Property

    Public Property CPFcli() As String
        Get
            Return _CPFcli
        End Get
        Set(ByVal value As String)
            _CPFcli = value
        End Set
    End Property

    Public Property endCli() As String
        Get
            Return _endCli
        End Get
        Set(ByVal value As String)
            _endCli = value
        End Set
    End Property

    Public Property nrEndCli() As String
        Get
            Return _nrEndCli
        End Get
        Set(ByVal value As String)
            _nrEndCli = value
        End Set
    End Property

    Public Property complEnd() As String
        Get
            Return _complEnd
        End Get
        Set(ByVal value As String)
            _complEnd = value
        End Set
    End Property

    Public Property bairroCli() As String
        Get
            Return _bairroCli
        End Get
        Set(ByVal value As String)
            _bairroCli = value
        End Set
    End Property

    Public Property cidadeCli() As String
        Get
            Return _cidadeCli
        End Get
        Set(ByVal value As String)
            _cidadeCli = value
        End Set
    End Property

    Public Property estCli() As String
        Get
            Return _estCli
        End Get
        Set(ByVal value As String)
            _estCli = value
        End Set
    End Property

    Public Property cepCli() As String

        Get
            Return _cepCli
        End Get
        Set(ByVal value As String)
            _cepCli = value
        End Set
    End Property

    Public Property DDD() As String
        Get
            Return _DDD
        End Get
        Set(ByVal value As String)
            _DDD = value
        End Set
    End Property

    Public Property telefone() As String
        Get
            Return _telefone
        End Get
        Set(ByVal value As String)
            _telefone = value
        End Set
    End Property

    Public Property nrNota() As Long
        Get
            Return _nrNota
        End Get
        Set(ByVal value As Long)
            _nrNota = value
        End Set
    End Property

    Public Property serieNota() As Integer
        Get
            Return _serieNota
        End Get
        Set(ByVal value As Integer)
            _serieNota = value
        End Set
    End Property

    Public Property dtNota() As String
        Get
            Return _dtNota
        End Get
        Set(ByVal value As String)
            _dtNota = value
        End Set
    End Property

    Public Property vlNFeTot() As String
        Get
            Return _vlNFeTot
        End Get
        Set(ByVal value As String)
            _vlNFeTot = value
        End Set
    End Property

    Public Property vlProdsTot() As String
        Get
            Return _vlProdsTot
        End Get
        Set(ByVal value As String)
            _vlProdsTot = value
        End Set
    End Property

    Public Property CFOP() As Integer
        Get
            Return _CFOP
        End Get
        Set(ByVal value As Integer)
            _CFOP = value
        End Set
    End Property

    Public Property NFeChave() As String
        Get
            Return _NFeChave
        End Get
        Set(ByVal value As String)
            _NFeChave = value
        End Set
    End Property

    Public Property qtd() As Integer
        Get
            Return _qtd
        End Get
        Set(ByVal value As Integer)
            _qtd = value
        End Set
    End Property

    Public Property EAN() As String
        Get
            Return _EAN
        End Get
        Set(ByVal value As String)
            _EAN = value
        End Set
    End Property
    Public Property partNumber() As String
        Get
            Return _partNumber
        End Get
        Set(ByVal value As String)
            _partNumber = value
        End Set
    End Property

    Public Property vlProduto() As String
        Get
            Return _vlProduto
        End Get
        Set(ByVal value As String)
            _vlProduto = value
        End Set
    End Property

    Public Property serviceType() As String
        Get
            Return _serviceType
        End Get
        Set(ByVal value As String)
            _serviceType = value
        End Set
    End Property

    Public Property giftPackage() As Integer
        Get
            Return _giftPackage
        End Get
        Set(ByVal value As Integer)
            _giftPackage = value
        End Set
    End Property


End Class