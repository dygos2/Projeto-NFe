Public Class notaVO
    
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
    Private _impressaoSolicitada As Integer
    Private _impressoEmContingencia As Integer
    Private _historicos As List(Of historicoVO)
    Private _atualizacoes As List(Of atualizacaoVO)
    Private _emailDest As String
    Private _impressora As String
    Private _serie As Integer
    Private _imprimeDanfe As Integer
    Private _ret_post_data As Nullable(Of DateTime)
    Private _postback As Integer
    Private _num_pedido As String
    Private _cfop As Long
    Private _token_loja As String

    Public Sub New()

    End Sub
    Public Property token_loja() As String
        Get
            Return _token_loja
        End Get
        Set(ByVal value As String)
            _token_loja = value
        End Set
    End Property
    Public Property postback() As Integer
        Get
            Return _postback
        End Get
        Set(ByVal value As Integer)
            _postback = value
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
    Public Property dEmiString() As String
        Get
            Return _NFe_ide_dEmi.ToString("dd/MM/yyyy")
        End Get
        Set(ByVal value As String)

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
    Public Property emailDestinatario() As String
        Get
            Return _emailDest
        End Get
        Set(ByVal value As String)
            _emailDest = value
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
    Public Property impressaoSolicitada() As Integer
        Get
            Return _impressaoSolicitada
        End Get
        Set(ByVal value As Integer)
            _impressaoSolicitada = value
        End Set
    End Property
    Public Property impressoEmContingencia() As Integer
        Get
            Return _impressoEmContingencia
        End Get
        Set(ByVal value As Integer)
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

    Public Property statusDaNotaString() As String
        Get
            Select Case statusDaNota
                Case 0
                    Return "Envio não processado"
                Case 1
                    Return "Enviada e aguardando retorno"
                Case 2, 21, 22
                    Return "Autorizado o uso da NF-e"
                Case 3
                    Return "Rejeitada"
                Case 4
                    Return "Cancelada"
                Case 5
                    Return "Contingência"
                Case 6
                    Return "Inutilizada"
                Case Else
                    Return String.Empty
            End Select
        End Get
        Set(ByVal value As String)

        End Set
    End Property

    Public Property atualizacoes() As List(Of atualizacaoVO)
        Get
            Return _atualizacoes
        End Get
        Set(ByVal value As List(Of atualizacaoVO))
            _atualizacoes = value
        End Set
    End Property

    Public Property impressora() As String
        Get
            Return _impressora
        End Get
        Set(ByVal value As String)
            _impressora = value
        End Set
    End Property

    Public Property serie() As Integer
        Get
            Return _serie
        End Get
        Set(ByVal value As Integer)
            _serie = value
        End Set
    End Property

    Public Property imprimeDanfe() As Integer
        Get
            Return _imprimeDanfe
        End Get
        Set(ByVal value As Integer)
            _imprimeDanfe = value
        End Set
    End Property

    Public Property ret_post_data() As Nullable(Of DateTime)
        Get
            Return _ret_post_data
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            _ret_post_data = value
        End Set
    End Property
    Public Property cfop() As Long
        Get
            Return _cfop
        End Get
        Set(ByVal value As Long)
            _cfop = value
        End Set
    End Property
End Class
