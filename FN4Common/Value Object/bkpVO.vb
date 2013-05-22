Public Class bkpVO

    Private _id_bkp As Long
    Private _cnpj_emit As String
    Private _cod_token As Integer
    Private _data As Date

    Public Sub New()

    End Sub

    Public Property id_bkp() As Long
        Get
            Return _id_bkp
        End Get
        Set(ByVal value As Long)
            _id_bkp = value
        End Set
    End Property
    Public Property cnpj_emit() As String
        Get
            Return _cnpj_emit
        End Get
        Set(ByVal value As String)
            _cnpj_emit = value
        End Set
    End Property
    Public Property cod_token() As Integer
        Get
            Return _cod_token
        End Get
        Set(ByVal value As Integer)
            _cod_token = value
        End Set
    End Property
    Public Property data() As Nullable(Of DateTime)
        Get
            Return _data
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            _data = value
        End Set
    End Property
End Class
