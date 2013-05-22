Public Class NotaVO


    Private pNumeroDaNota As Integer
    Private pDataHoraDaEmissaoDaNota As String = ""
    Private pChaveDeAcessoDaNota As String = ""
    Private pCodigoNota As Integer
    Private pCodigoEmitente As Integer
    Private pCodigoDestinatario As Integer

    Public Property codigoEmitente() As Integer
        Get
            Return pCodigoEmitente
        End Get
        Set(ByVal value As Integer)
            pCodigoEmitente = value
        End Set
    End Property
    Public Property codigoDestinatario() As Integer
        Get
            Return pCodigoEmitente
        End Get
        Set(ByVal value As Integer)
            pCodigoEmitente = value
        End Set
    End Property

    Public Property codigoNota() As Integer
        Get
            Return pCodigoNota
        End Get
        Set(ByVal value As Integer)
            pChaveDeAcessoDaNota = value
        End Set
    End Property

    Public Property numeroDaNota() As Integer
        Get
            Return pNumeroDaNota
        End Get
        Set(ByVal value As Integer)
            pNumeroDaNota = value
        End Set
    End Property

    Public Property chaveDeAcessoDaNota() As String
        Get
            Return pChaveDeAcessoDaNota
        End Get
        Set(ByVal value As String)
            pChaveDeAcessoDaNota = value
        End Set
    End Property

    Public Property dataHoraDaEmissaoDaNota() As DateTime
        Get
            Return pDataHoraDaEmissaoDaNota
        End Get
        Set(ByVal value As DateTime)
            pDataHoraDaEmissaoDaNota = value
        End Set
    End Property

End Class
