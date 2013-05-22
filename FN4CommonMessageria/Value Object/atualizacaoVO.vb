Public Class atualizacaoVO
    'classe com os dados resumidos de uma nota recém-atualizada

    Private _idHistorico As Integer
    Private _NFe_ide_nNF As Integer
    Private _NFe_dest_xNome As String
    Private _dataHora As DateTime
    Private _atualizacao As String
    Private _NFe_emit_CNPJ As String
    Private _serie As Integer

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


    Public Property NFe_ide_nNF() As Long
        Get
            Return _NFe_ide_nNF
        End Get
        Set(ByVal value As Long)
            _NFe_ide_nNF = value
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
    Public Property dataHora() As Date
        Get
            Return _dataHora
        End Get
        Set(ByVal value As Date)
            _dataHora = value
        End Set
    End Property

    Public Property dataString() As String
        Get
            Return _dataHora.ToString("dd/MM/yyyy - hh:mm")
        End Get
        Set(ByVal value As String)
            _dataHora = value
        End Set
    End Property

    Public Property atualizacao() As String
        Get
            Return _atualizacao
        End Get
        Set(ByVal value As String)
            _atualizacao = value
        End Set
    End Property

    Public Property serie As Integer
        Get
            Return _serie
        End Get
        Set(ByVal value As Integer)
            _serie = value
        End Set
    End Property
End Class
