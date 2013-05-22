Public Class historicoVO
    Private _idHistorico As Integer
    Private _idTpHistorico As Integer
    Private _NFe_ide_nNF As Integer
    Private _NFe_emit_CNPJ As String
    Private _dataHora As Date
    Private _mensagem As String
    Private _complemento As String
    Private _serie As Integer
    Public Sub New()

    End Sub

    Public Sub New(ByVal tipoDeHistorico As String, ByVal descricao As String, ByVal nNF As Integer, ByVal CNPJ As String, ByVal serie As Integer)
        Me.idTpHistorico = tipoDeHistorico
        Me.complemento = descricao
        Me.NFe_ide_nNF = nNF
        Me.NFe_emit_CNPJ = CNPJ
        Me.serie = serie
    End Sub
    Public Property NFe_emit_CNPJ() As String
        Get
            Return _NFe_emit_CNPJ

        End Get
        Set(ByVal value As String)
            _NFe_emit_CNPJ = value
        End Set
    End Property
    Public Property idHistorico() As String
        Get
            Return _idHistorico
        End Get
        Set(ByVal value As String)
            _idHistorico = value
        End Set
    End Property

    Public Property idTpHistorico() As String
        Get
            Return _idTpHistorico
        End Get
        Set(ByVal value As String)
            _idTpHistorico = value
        End Set
    End Property

    Public Property NFe_ide_nNF() As Integer
        Get
            Return _NFe_ide_nNF
        End Get
        Set(ByVal value As Integer)
            _NFe_ide_nNF = value
        End Set
    End Property

    Public Property dataHora() As Date
        Get
            Return _dataHora
        End Get
        Set(ByVal value As Date)
            _dataHora = value
        End Set
    End Property

    Public Property complemento() As String
        Get
            Return _complemento
        End Get
        Set(ByVal value As String)
            _complemento = value
        End Set
    End Property
    Public Property mensagem() As String
        Get
            Return _mensagem
        End Get
        Set(ByVal value As String)
            _mensagem = value
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
End Class
