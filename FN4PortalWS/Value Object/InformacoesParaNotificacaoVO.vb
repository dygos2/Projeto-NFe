Public Class InformacoesParaNotificacaoVO

    Private _emitente As emitenteVO
    Private _destinatario As DestinatarioVO
    Private _nota As NotaVO


    Public Sub New(ByVal emit As emitenteVO, ByVal dest As DestinatarioVO, ByVal nota As NotaVO)
        Me._emitente = emit
        Me._destinatario = dest
        Me._nota = nota
    End Sub

    Public Sub New()

    End Sub

    Public Property emitente() As emitenteVO
        Get
            Return _emitente
        End Get
        Set(ByVal value As emitenteVO)
            _emitente = value
        End Set
    End Property

    Public Property destinatario() As DestinatarioVO
        Get
            Return _destinatario
        End Get
        Set(ByVal value As DestinatarioVO)
            _destinatario = value
        End Set
    End Property

    Public Property nota() As NotaVO
        Get
            Return _nota
        End Get
        Set(ByVal value As NotaVO)
            _nota = value
        End Set
    End Property


End Class
