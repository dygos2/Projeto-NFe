Public Class notasProcVO


    Private _NFe_emit_CNPJ As String
    Private _idEmpresa As Integer
    Private _qtdNotas As Integer

    Public Property NFe_emit_CNPJ() As String
        Get
            Return _NFe_emit_CNPJ
        End Get
        Set(ByVal value As String)
            _NFe_emit_CNPJ = value
        End Set
    End Property

    Public Property idEmpresa() As Integer
        Get
            Return _idEmpresa
        End Get
        Set(ByVal value As Integer)
            _idEmpresa = value
        End Set
    End Property

    Public Property qtdNotas() As Integer
        Get
            Return _qtdNotas
        End Get
        Set(ByVal value As Integer)
            _qtdNotas = value
        End Set
    End Property


End Class
