Public Class emitenteVO

    Private pCNPJ As String
    Private pNome As String
    Private pAtivo As Boolean
    Private pcodigo As Integer

    Public Property ativo() As Boolean
        Get
            Return pAtivo
        End Get
        Set(ByVal value As Boolean)
            pAtivo = value
        End Set
    End Property
    Public Property codigo() As Integer
        Get
            Return pcodigo
        End Get
        Set(ByVal value As Integer)
            pcodigo = value
        End Set
    End Property

    Public Property CNPJ() As String
        Get
            Return pCNPJ
        End Get
        Set(ByVal value As String)
            pCNPJ = value
        End Set
    End Property
    Public Property nome() As String
        Get
            Return pNome
        End Get
        Set(ByVal value As String)
            pNome = value
        End Set
    End Property
End Class
