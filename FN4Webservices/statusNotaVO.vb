Public Class statusNotaVO

    Private _numeroDaNota As Integer
    Private _CNPJemitente As String = ""
    Private _statusDaNota As Integer
    Private _xStatusDaNota As String = ""
    Private _chaveDeAcesso As String = ""
    Private _protocolo As String = ""

    Public Sub New()

    End Sub

    Public Sub New(ByVal nota As FN4Common.notaVO)
        _numeroDaNota = nota.NFe_ide_nNF
        _CNPJemitente = nota.NFe_emit_CNPJ
        _statusDaNota = nota.statusDaNota
        _xStatusDaNota = nota.statusDaNotaString
        _chaveDeAcesso = nota.NFe_infNFe_id
        _protocolo = nota.protNfe_nProt

    End Sub

    Public Property numeroDaNota() As Integer
        Get
            Return _numeroDaNota
        End Get
        Set(ByVal value As Integer)
            _numeroDaNota = value
        End Set
    End Property

    Public Property cnpjEmitetente() As String
        Get
            Return _CNPJemitente

        End Get
        Set(ByVal value As String)
            _CNPJemitente = value
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
    Public Property xStatusDaNota() As String
        Get
            Return _xStatusDaNota
        End Get
        Set(ByVal value As String)
            _statusDaNota = value
        End Set
    End Property

    Public Property ChaveDeAcesso() As String
        Get
            Return _chaveDeAcesso
        End Get
        Set(ByVal value As String)
            _chaveDeAcesso = value
        End Set
    End Property

    Public Property Protocolo() As String
        Get
            Return _protocolo
        End Get
        Set(ByVal value As String)
            _protocolo = value
        End Set
    End Property



End Class
