Public Class retornoVO
    Private _tpRetorno As Integer
    Private _nmMensagem As Integer
    Private _mensagem As String
    Private _numProt As Integer
    Public Sub New()

    End Sub
    Public Sub New(ByVal xmlRetorno As String)
        Dim xml As New System.Xml.XmlDocument
        xml.LoadXml(xmlRetorno)
        If xml.SelectSingleNode("/protocolo/tpRetorno").InnerText <> "" Then
            _tpRetorno = CInt(xml.SelectSingleNode("/protocolo/tpRetorno").InnerText)
        End If
        If xml.SelectSingleNode("/protocolo/nmMensagem").InnerText <> "" Then
            _nmMensagem = CInt(xml.SelectSingleNode("/protocolo/nmMensagem").InnerText)
        End If

        If xml.SelectSingleNode("/protocolo/mensagem").InnerText <> "" Then
            _mensagem = xml.SelectSingleNode("/protocolo/mensagem").InnerText
        End If
        If xml.SelectSingleNode("/protocolo/numProt").InnerText <> "" Then
            _numProt = CInt(xml.SelectSingleNode("/protocolo/numProt").InnerText)
        End If

    End Sub
    Public Property tipoDeRetorno() As Integer
        Get
            Return _tpRetorno
        End Get
        Set(ByVal value As Integer)
            _tpRetorno = value
        End Set
    End Property
    Public Property numeroMensagem() As Integer
        Get
            Return _nmMensagem
        End Get
        Set(ByVal value As Integer)
            _nmMensagem = value
        End Set
    End Property
    Public Property numeroProtocolo() As Integer
        Get
            Return _numProt
        End Get
        Set(ByVal value As Integer)
            _numProt = value
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
End Class
