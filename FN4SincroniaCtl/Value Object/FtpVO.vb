Public Class FtpVO
    Private _user As String
    Private _pass As String
    Private _txt_path As String
    Private _xml_path As String
    Private _pdf_path As String
    Private _ftp_host As String

    Public Property user() As String
        Get
            Return _user
        End Get
        Set(ByVal value As String)
            _user = value
        End Set
    End Property
    Public Property pass() As String
        Get
            Return _pass
        End Get
        Set(ByVal value As String)
            _pass = value
        End Set
    End Property
    Public Property txt_path() As String
        Get
            Return _txt_path
        End Get
        Set(ByVal value As String)
            _txt_path = value
        End Set
    End Property
    Public Property xml_path() As String
        Get
            Return _xml_path
        End Get
        Set(ByVal value As String)
            _xml_path = value
        End Set
    End Property
    Public Property pdf_path() As String
        Get
            Return _pdf_path
        End Get
        Set(ByVal value As String)
            _pdf_path = value
        End Set
    End Property
    Public Property ftp_host() As String
        Get
            Return _ftp_host
        End Get
        Set(ByVal value As String)
            _ftp_host = value
        End Set
    End Property
End Class
