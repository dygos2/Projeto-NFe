Public Class notaVO
    '   	NFe_ide_nNF Bigint UNSIGNED NOT NULL,
    'pastaDeTrabalho Varchar(100),
    'statusDaNota Tinyint,
    'NFe_ide_dEmi Date,
    'NFe_infNFe_id Varchar(50),
    'NFe_emit_CNPJ Varchar(14),
    'NFe_emit_CPF Varchar(11),
    'NFe_emit_xNome Varchar(60),
    'NFe_dest_CNPJ Varchar(14),
    'NFe_dest_CPF Varchar(11),
    'NFe_dest_xNome Varchar(60),
    'NFe_dest_UF Varchar(2),
    'NFe_total_ICMSTot_vNF Decimal(15,2),
    'retEnviNFe_infRec_nRec Varchar(15),
    'retEnviNFe_cStat Varchar(3),
    'retEnviNFe_xMotivo Varchar(255),
    'protNfe_nProt Char(20),
    'dataUltimaAtualizacao Timestamp,
    'impressaoSolicitada Bool,
    'impressoEmContingencia Bool,

    Private _NFe_ide_nNF As Long
    Private _pastaDeTrabalho As String
    Private _tentativasDeInclusao As Integer
    Private _statusDaNota As Integer
    Private _NFe_ide_dEmi As Date
    Private _NFe_infNFe_id As String
    Private _NFe_emit_CNPJ As String
    Private _NFe_emit_CPF As String
    Private _NFe_emit_xNome As String
    Private _NFe_dest_CNPJ As String
    Private _NFe_dest_CPF As String
    Private _NFe_dest_xNome As String
    Private _NFe_dest_UF As String
    Private _NFe_total_ICMSTot_vNF As Double
    Private _retEnviNFe_infRec_nRec As String
    Private _retEnviNFe_cStat As String
    Private _retEnviNFe_xMotivo As String
    Private _protNfe_nProt As String
    Private _dataUltimaAtualizacao As DateTime
    Private _impressaoSolicitada As Boolean
    Private _impressoEmContingencia As Boolean
    Private _historicos As List(Of historicoVO)

    Public Sub New()

    End Sub

    Public Sub New(ByVal numeroDaNota As Long)
        Me.tentativasDeInclusao = 0
        Me.NFe_ide_nNF = numeroDaNota
        Me.impressoEmContingencia = False
        Me.impressaoSolicitada = False
    End Sub



    Public Property NFe_ide_nNF() As Long
        Get
            Return _NFe_ide_nNF
        End Get
        Set(ByVal value As Long)
            _NFe_ide_nNF = value
        End Set
    End Property
    Public Property pastaDeTrabalho() As String
        Get
            Return _pastaDeTrabalho
        End Get
        Set(ByVal value As String)
            _pastaDeTrabalho = value
        End Set
    End Property
    Public Property statusDaNota() As Integer
        Get
            Return _statusDaNota
        End Get
        Set(ByVal value As Integer)
            _statusDaNota = value
        End Set
    End Property
    Public Property tentativasDeInclusao() As Integer
        Get
            Return _tentativasDeInclusao
        End Get
        Set(ByVal value As Integer)
            _tentativasDeInclusao = value
        End Set
    End Property
    Public Property NFe_ide_dEmi() As Date
        Get
            Return _NFe_ide_dEmi
        End Get
        Set(ByVal value As Date)
            _NFe_ide_dEmi = value
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
    Public Property NFe_emit_CNPJ() As String
        Get
            Return _NFe_emit_CNPJ
        End Get
        Set(ByVal value As String)
            _NFe_emit_CNPJ = value
        End Set
    End Property
    Public Property NFe_emit_CPF() As String
        Get
            Return _NFe_emit_CPF
        End Get
        Set(ByVal value As String)
            _NFe_emit_CPF = value
        End Set
    End Property
    Public Property NFe_emit_xNome() As String
        Get
            Return _NFe_emit_xNome
        End Get
        Set(ByVal value As String)
            _NFe_emit_xNome = value
        End Set
    End Property
    Public Property NFe_dest_CNPJ() As String
        Get
            Return _NFe_dest_CNPJ
        End Get
        Set(ByVal value As String)
            _NFe_dest_CNPJ = value
        End Set
    End Property
    Public Property NFe_dest_CPF() As String
        Get
            Return _NFe_dest_CPF
        End Get
        Set(ByVal value As String)
            _NFe_dest_CPF = value
        End Set
    End Property
    Public Property NFe_dest_xNome() As String
        Get
            Return _NFe_dest_xNome
        End Get
        Set(ByVal value As String)
            _NFe_dest_xNome = value
        End Set
    End Property
    Public Property NFe_dest_UF() As String
        Get
            Return _NFe_dest_UF
        End Get
        Set(ByVal value As String)
            _NFe_dest_UF = value
        End Set
    End Property
    Public Property NFe_total_ICMSTot_vNF() As Double
        Get
            Return _NFe_total_ICMSTot_vNF
        End Get
        Set(ByVal value As Double)
            _NFe_total_ICMSTot_vNF = value
        End Set
    End Property
    Public Property retEnviNFe_infRec_nRec() As String
        Get
            Return _retEnviNFe_infRec_nRec
        End Get
        Set(ByVal value As String)
            _retEnviNFe_infRec_nRec = value
        End Set
    End Property
    Public Property retEnviNFe_cStat() As String
        Get
            Return _retEnviNFe_cStat
        End Get
        Set(ByVal value As String)
            _retEnviNFe_cStat = value
        End Set
    End Property
    Public Property retEnviNFe_xMotivo() As String
        Get
            Return _retEnviNFe_xMotivo
        End Get
        Set(ByVal value As String)
            _retEnviNFe_xMotivo = value
        End Set
    End Property
    Public Property protNfe_nProt() As String
        Get
            Return _protNfe_nProt
        End Get
        Set(ByVal value As String)
            _protNfe_nProt = value
        End Set
    End Property
    Public Property dataUltimaAtualizacao() As DateTime
        Get
            Return _dataUltimaAtualizacao
        End Get
        Set(ByVal value As DateTime)
            _dataUltimaAtualizacao = value
        End Set
    End Property
    Public Property impressaoSolicitada() As Boolean
        Get
            Return _impressaoSolicitada
        End Get
        Set(ByVal value As Boolean)
            _impressaoSolicitada = value
        End Set
    End Property
    Public Property impressoEmContingencia() As Boolean
        Get
            Return _impressoEmContingencia
        End Get
        Set(ByVal value As Boolean)
            _impressoEmContingencia = value
        End Set
    End Property

    Public Property historicos() As List(Of historicoVO)
        Get
            Return _historicos
        End Get
        Set(ByVal value As List(Of historicoVO))
            _historicos = value
        End Set
    End Property



End Class
