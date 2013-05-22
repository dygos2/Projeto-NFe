Imports System.Web.Services
Imports System.ComponentModel
Imports System.IO

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://fisconet4.com/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class FN4PortalRecepcao
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function enviarDados(ByVal info As InformacoesParaNotificacaoVO)
        Try
            Dim emit As emitenteVO

            emit = obterEmitente(info.emitente)
            'checar o CNPJ do cliente
            If emit Is Nothing Then
                Return New retornoVO(0, 1, "Emitente não cadastrado")
                'checar a atividade do cliente
            ElseIf emit.ativo = False Then
                Return New retornoVO(0, 2, "Serviço de envio de emails inativo para Emitente")
            Else
                info.emitente = emit
            End If

            Dim dest As DestinatarioVO

            'checar o destinatario
            dest = obterDestinatario(info.destinatario)

            If dest Is Nothing Then
                'se não houver, inserir
                If info.destinatario.CPFCNPJ <> "" Then
                    inserirDestinatario(info.destinatario)
                Else
                    Return New retornoVO(0, 3, "CPF ou CNPJ do destinatario não informado")
                End If
            Else
                'se já houver,
                ' checar(Email)

                'atualizar
                If dest.email = "" And info.destinatario.email <> "" Then
                    info.destinatario.codigoDestinatario = dest.codigoDestinatario
                    IBatisNETHelper.Instance.Update("AlterarDestinatario", info.destinatario)
                ElseIf dest.email <> "" Then
                    info.destinatario = dest
                End If

            End If

            'inserir dados da nota


            Try
                'se houver email
                If info.destinatario.email <> "" Then
                    'enviar email para cliente
                    enviarEmail(info)
                End If
            Catch ex As Exception
                Return New retornoVO(0, 4, "Erro no envio de email: " & ex.Message)
            End Try

            Return New retornoVO(1, 5, "Email enviado com sucesso para destinatario")
        Catch ex As Exception
            Return New retornoVO(1, 6, "Erro inesperado: " & ex.Message)
        End Try
    End Function

    <WebMethod()> _
    Public Sub enviarDadosXML(ByVal info As InformacoesParaNotificacaoVO, ByVal xml As String)

    End Sub

    '<WebMethod()> _
    'Public Function testeEnvio() As retornoVO
    '    Try
    '        Dim info As New InformacoesParaNotificacaoVO
    '        Dim n As New FN4PortalWS.NotaVO
    '        Dim emit As New FN4PortalWS.emitenteVO
    '        Dim dest As New FN4PortalWS.DestinatarioVO

    '        n.chaveDeAcessoDaNota = "123456789"
    '        n.numeroDaNota = 1
    '        n.dataHoraDaEmissaoDaNota = DateTime.Now

    '        dest.email = "lucas.a.leite@gmail.com"
    '        dest.nomeDestinatario = "Destinatario"

    '        dest.CPFCNPJ = "12345"

    '        emit.CNPJ = "02549051000100"

    '        Dim par As New FN4PortalWS.InformacoesParaNotificacaoVO(emit, dest, n)

    '        Return enviarDados(par)
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function



    Private Function obterEmitente(ByVal emitente As emitenteVO) As emitenteVO
        Return IBatisNETHelper.Instance.QueryForObject("obterEmitentePorCNPJ", emitente)
    End Function
    Private Function obterDestinatario(ByVal destinatario As DestinatarioVO) As DestinatarioVO
        Return IBatisNETHelper.Instance.QueryForObject("obterDestinatarioPorCPFCNPJ", destinatario)
    End Function

    Private Sub inserirDestinatario(ByRef destinatario As DestinatarioVO)
        IBatisNETHelper.Instance.Insert("inserirDestinatario", destinatario)
    End Sub

    Private Sub inserirNota(ByVal info As InformacoesParaNotificacaoVO)
        IBatisNETHelper.Instance.Insert("inserirNota", info)
    End Sub

    Private Sub enviarEmail(ByVal info As InformacoesParaNotificacaoVO)
        Dim corpo As String
        Dim str As New StreamReader(Server.MapPath(".") & "/body.htm")
        corpo = str.ReadToEnd

        corpo = corpo.Replace("@numeroDaNota", info.nota.numeroDaNota)
        corpo = corpo.Replace("@emitente", info.emitente.nome)
        corpo = corpo.Replace("@destinatario", info.destinatario.nomeDestinatario)
        corpo = corpo.Replace("@chaveDeAcesso", info.nota.chaveDeAcessoDaNota)

        str.Close()
        str.Dispose()
        Try
            Email.Enviar(info.destinatario.email, _
             "suporte@megaideas.net", "123mudar", _
             "NFe n°" & info.nota.numeroDaNota & "emitida em " & info.nota.dataHoraDaEmissaoDaNota.Date.ToString, _
             corpo)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

End Class