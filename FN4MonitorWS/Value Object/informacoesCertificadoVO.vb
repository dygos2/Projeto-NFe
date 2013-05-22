Public Class informacoesCertificadoVO

    Private _numeroDeSerie As String
    Private _proprietario As String
    Private _emissor As String
    Private _inicioValidade As Date
    Private _fimValidade As Date




    Public Sub New(ByVal certificado As System.Security.Cryptography.X509Certificates.X509Certificate2)
        _numeroDeSerie = certificado.SerialNumber
        _proprietario = certificado.Subject
        _proprietario = _proprietario.Substring((_proprietario.IndexOf("=") + 1), (_proprietario.IndexOf(",") - 2))
        _emissor = certificado.Issuer
        _emissor = _emissor.Substring((_emissor.IndexOf("=") + 1), (_emissor.IndexOf(",") - 2))
        _inicioValidade = certificado.NotBefore
        _fimValidade = certificado.NotAfter

    End Sub
    Public Sub New()

    End Sub

    Public Property numeroDeSerie() As String
        Get
            Return _numeroDeSerie
        End Get
        Set(ByVal value As String)

        End Set
    End Property
    Public Property proprietario() As String
        Get
            Return _proprietario
        End Get
        Set(ByVal value As String)

        End Set
    End Property
    Public Property emissor() As String
        Get
            Return _emissor
        End Get
        Set(ByVal value As String)

        End Set
    End Property
    Public Property inicioValidade() As String
        Get
            With _inicioValidade
                Return .ToLongDateString & ", 00:00"
            End With
        End Get
        Set(ByVal value As String)

        End Set
    End Property
    Public Property fimValidade() As String
        Get
            Return _fimValidade.ToLongDateString & ", 00:00"
        End Get
        Set(ByVal value As String)

        End Set
    End Property

    Public Property tempoAteExpiracao() As Integer
        Get
            Return _fimValidade.Subtract(DateTime.Now).Days
        End Get
        Set(ByVal value As Integer)

        End Set
    End Property
End Class
