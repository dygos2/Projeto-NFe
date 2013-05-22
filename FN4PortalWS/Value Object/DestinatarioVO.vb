Public Class DestinatarioVO

    Private pCPFCNPJDestinatario As String = ""
    Private pNomeDestinatario As String = ""
    Private pEmailDestinatario As String = ""
    Private pcodigoDestinatario As Integer
    Private pSenha As String

    Public Property senha() As String
        Get
            Return pSenha
        End Get
        Set(ByVal value As String)
            pSenha = value
        End Set
    End Property

    Public Property codigoDestinatario() As Integer
        Get
            Return pcodigoDestinatario
        End Get
        Set(ByVal value As Integer)
            pcodigoDestinatario = value
        End Set
    End Property

    Public Property CPFCNPJ() As String
        Get
            Return pCPFCNPJDestinatario
        End Get
        Set(ByVal value As String)
            pCPFCNPJDestinatario = value
        End Set
    End Property

    Public Property nomeDestinatario() As String
        Get
            Return pNomeDestinatario
        End Get
        Set(ByVal value As String)
            pNomeDestinatario = value
        End Set
    End Property

    Public Property email() As String
        Get
            Return pEmailDestinatario
        End Get
        Set(ByVal value As String)
            pEmailDestinatario = value
        End Set
    End Property
End Class
