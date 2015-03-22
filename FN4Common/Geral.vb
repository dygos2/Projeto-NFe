Imports System.Xml
Imports System.IO
Imports System.Security.Cryptography.X509Certificates
Imports System.Text.RegularExpressions
Imports FN4Common.DataAccess
Imports System.Xml.Schema



Public Class Geral
    Private Shared resultadoValidacao As System.Text.StringBuilder
    Private Shared _certificado As X509Certificate2
    Private Shared _parametro As New XmlDocument

    Public Shared ReadOnly Property Certificado() As X509Certificate2
        Get
            If _certificado Is Nothing Then
                _certificado = obterCertificado()
            End If
            Return _certificado
        End Get
    End Property

    Public Shared Function ObterCertificadoPorEmpresa(ByVal idEmpresa As Integer) As X509Certificate2
        Return obterCertificado(idEmpresa)
    End Function

    Public Shared Function ObterCertificadoPorEmpresa(ByVal idEmpresa As Integer, ByVal Servico As String) As X509Certificate2
        Return obterCertificado(idEmpresa, Servico)
    End Function

    Public Shared ReadOnly Property Parametro(ByVal nome As String) As String
        Get
            If _parametro.BaseURI = "" Then
                Dim uri As New Uri(System.Reflection.Assembly.GetExecutingAssembly.GetName.CodeBase)
                Dim caminho As String = Path.GetDirectoryName(uri.LocalPath)
                _parametro.Load(caminho & "\XML\FN4Config.xml")
            End If

            Return _parametro.SelectSingleNode("/Fisconet4.Settings/setting[@name='" & nome & "']/value").InnerText

        End Get
    End Property

    Public Shared ReadOnly Property Parametro(ByVal nome As String, ByVal CNPJ As String) As String
        Get
            Dim valor As String = Parametro(nome)
            If _parametro.BaseURI = "" Then
                Dim uri As New Uri(System.Reflection.Assembly.GetExecutingAssembly.GetName.CodeBase)
                Dim caminho As String = Path.GetDirectoryName(uri.LocalPath)
                _parametro.Load(caminho & "\XML\FN4Config.xml")
            End If

            Return _parametro.SelectSingleNode("/Fisconet4.Settings/setting[@name='" & nome & CNPJ & "']/value").InnerText
        End Get
    End Property
    Public Shared Function validarXmlGeral(ByVal PathXmlEnvio As String, ByVal xsd_path As String)

        resultadoValidacao = New System.Text.StringBuilder
        Dim myevent As ValidationEventHandler = New ValidationEventHandler(AddressOf ValidationEvent)
        'carrega o XSD

        Dim pathXSD As String = System.AppDomain.CurrentDomain.BaseDirectory() & xsd_path

        Dim xschema As XmlSchema = XmlSchema.Read(New XmlTextReader(pathXSD), myevent)

        'configura a validação
        Dim xsettings As New XmlReaderSettings

        xsettings.ValidationType = ValidationType.Schema

        xsettings.Schemas.Add(xschema)

        'atribui o evento de validação
        AddHandler xsettings.ValidationEventHandler, myevent
        'faz a validação
        Using xreader As XmlReader = XmlReader.Create(PathXmlEnvio, xsettings)
            While xreader.Read
            End While
        End Using
        '"http://www.portalfiscal.inf.br/nfe", System.AppDomain.CurrentDomain.BaseDirectory() & "XSD\leiauteNFe_v1.10.xsd")

        Return resultadoValidacao.ToString
    End Function
    Public Shared Function cancelarNFe(ByVal nota As notaVO, ByVal textoJustificativa As String, ByVal empresa_obj As empresaVO) As String
        Try

            'Dim envio_versao As Integer

            Try 'tenta buscaro utc
                'Dim utcback As utcVO = utc.obterUTC(empresa_obj.uf)
                'envio_versao = utcback.versao_canc
            Catch ex As Exception
                Throw New Exception("Erro ao carregar a configuração do UTC. Envio por Eventou 1 ou 2. " & empresa_obj.uf & " : " & ex.Message)
            End Try

            Dim evento As New eventoVO

            evento.NFe_infNFe_id = nota.NFe_infNFe_id
            evento.infEvento_detEvento_xCorrecao = textoJustificativa.Trim()
            evento.NFe_emit_CNPJ = nota.NFe_emit_CNPJ
            evento.infEvento_tpEvento = 110111
            evento.statusEvento = 0

            eventos.inserirEvento(evento)
            inserirHistorico(19, "Nota enviada para fila de eventos", nota)

            Return "Nota Enviada para fila de Processamento"
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function tratar_retorno_xml(ByVal nota As notaVO, ByVal retorno As System.Xml.XmlElement, ByVal empresa As empresaVO, ByVal sinc As Integer)

        Dim xmlRetorno As New XmlDocument
        Dim stringWriter As New StringWriter()
        Dim xmlTextWriter As New XmlTextWriter(stringWriter)
        Dim resultado As String
        Dim xMotivo As String

        retorno.WriteTo(xmlTextWriter)

        Dim strRetorno = stringWriter.ToString()

        Dim xmlprotocolo As New XmlDocument
        xmlprotocolo.LoadXml(strRetorno)
        xmlprotocolo.PreserveWhitespace = True
        xmlprotocolo.Save(nota.pastaDeTrabalho & nota.NFe_ide_nNF & "_protocolo.xml")

        If sinc = 1 Then
            resultado = xmlprotocolo.SelectSingleNode("/*[local-name()='retEnviNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='cStat' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").InnerXml
            xMotivo = xmlprotocolo.SelectSingleNode("/*[local-name()='retEnviNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='xMotivo' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").InnerXml
        Else
            resultado = xmlprotocolo.SelectSingleNode("/*[local-name()='retConsReciNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='cStat' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").InnerXml
            xMotivo = xmlprotocolo.SelectSingleNode("/*[local-name()='retConsReciNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='xMotivo' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']").InnerXml
        End If


        If resultado = "104" Then 'lote processado, processar o retorno dele

            If sinc = 1 Then
                nota.retEnviNFe_xMotivo = xmlprotocolo.SelectSingleNode("/*[local-name()='retEnviNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='protNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='xMotivo' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText
                nota.retEnviNFe_cStat = xmlprotocolo.SelectSingleNode("/*[local-name()='retEnviNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='protNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='cStat' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText
            Else
                nota.retEnviNFe_xMotivo = xmlprotocolo.SelectSingleNode("/*[local-name()='retConsReciNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='protNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='xMotivo' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText
                nota.retEnviNFe_cStat = xmlprotocolo.SelectSingleNode("/*[local-name()='retConsReciNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='protNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='cStat' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText
            End If


            If nota.retEnviNFe_cStat = "100" Or nota.retEnviNFe_cStat = "150" Then 'AUTORIZADA OU AUTORIZADA COM ATRASO
                'nota autorizada
                Dim tp_sys_user = configuracaoDAO.obterConfiguracao("tp_sys", empresa.idEmpresa)
                'verifica se o usuário está cadastrado no banco como 1, ou seja, ele vai receber o Danfe em PDF
                If nota.statusDaNota = 1 And Not tp_sys_user Is Nothing Then
                    nota.impressaoSolicitada = 1
                End If

                nota.statusDaNota = 21

                If sinc = 1 Then
                    nota.protNfe_nProt = xmlprotocolo.SelectSingleNode("/*[local-name()='retEnviNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='protNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='nProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText
                Else
                    nota.protNfe_nProt = xmlprotocolo.SelectSingleNode("/*[local-name()='retConsReciNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='protNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='nProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText
                End If

                gerarAnexo(nota, xmlprotocolo)

                notaDAO.alterarNota(nota)

                inserirHistorico(15, nota.retEnviNFe_xMotivo, nota)

                Dim tp_sys = FN4Common.Geral.Parametro("tp_sys")
                Dim tipoSistema = Integer.Parse(tp_sys)

                If tipoSistema = 1 Then
                    If empresa.frest > 0 Then
                        empresa.frest = empresa.frest - 1
                    ElseIf empresa.prest > 0 Then
                        empresa.prest = empresa.prest - 1
                    Else
                        empresa.frest = 0
                        empresa.prest = 0
                    End If

                    empresaDAO.alterarPFrest(empresa)
                End If

                'requisição da shockmetais - se tiver no fn4config caminho de pasta configurado, então, copiar para lá o _protocolo
                If Geral.Parametro("copy_prot") <> "" Then
                    Log.registrarInfo("salvando protocolo em " & Geral.Parametro("copy_prot") & empresa.cnpj & "_" & nota.NFe_ide_nNF & "_" & nota.serie & "_protocolo.xml", "RetornoService")
                    Try
                        xmlprotocolo.Save(Geral.Parametro("copy_prot") & empresa.cnpj & "_" & nota.NFe_ide_nNF & "_" & nota.serie & "_protocolo.xml")
                    Catch ex As Exception
                        Log.registrarInfo("Erro ao salvar o protocolo - " & ex.Message, "RetornoService")
                    End Try
                End If

            ElseIf nota.retEnviNFe_cStat = "110" Then 'DENEGADA

                nota.statusDaNota = 7

                nota.protNfe_nProt = xmlprotocolo.SelectSingleNode("/*[local-name()='retConsReciNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='protNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='infProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]/*[local-name()='nProt' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText
                gerarAnexo(nota, xmlprotocolo)
                notaDAO.alterarNota(nota)

                inserirHistorico(15, nota.retEnviNFe_xMotivo, nota)

                Dim tp_sys = FN4Common.Geral.Parametro("tp_sys")
                Dim tipoSistema = Integer.Parse(tp_sys)

                If tipoSistema = 1 Then
                    If empresa.frest > 0 Then
                        empresa.frest = empresa.frest - 1
                    ElseIf empresa.prest > 0 Then
                        empresa.prest = empresa.prest - 1
                    Else
                        empresa.frest = 0
                        empresa.prest = 0
                    End If

                    empresaDAO.alterarPFrest(empresa)
                End If

                'requisição da shockmetais - se tiver no fn4config caminho de pasta configurado, então, copiar para lá o _protocolo
                If Geral.Parametro("copy_prot") <> "" Then
                    Log.registrarInfo("salvando protocolo em " & Geral.Parametro("copy_prot") & empresa.cnpj & "_" & nota.NFe_ide_nNF & "_" & nota.serie & "_protocolo.xml", "RetornoService")
                    Try
                        xmlprotocolo.Save(Geral.Parametro("copy_prot") & empresa.cnpj & "_" & nota.NFe_ide_nNF & "_" & nota.serie & "_protocolo.xml")
                    Catch ex As Exception
                        Log.registrarInfo("Erro ao salvar o protocolo - " & ex.Message, "RetornoService")
                    End Try
                End If

            ElseIf nota.retEnviNFe_cStat = "204" Then 'duplicidade
                Dim tp_sys_user = configuracaoDAO.obterConfiguracao("tp_sys", empresa.idEmpresa)
                If nota.statusDaNota = 1 And Not tp_sys_user Is Nothing Then
                    nota.impressaoSolicitada = 1 'alterado por Rodrigo, já seta impressao
                End If
                nota.statusDaNota = 19 'alterado por Rodrigo, se der duplicidade, buscar novamente o status e o recibo
                notaDAO.alterarNota(nota)
                inserirHistorico(15, nota.retEnviNFe_xMotivo, nota)
            Else
                nota.statusDaNota = 3
                notaDAO.alterarNota(nota)
                inserirHistorico(15, nota.retEnviNFe_xMotivo, nota)
            End If

        ElseIf resultado = "105" Then 'lote em processamento
            inserirHistorico(14, nota.retEnviNFe_xMotivo, nota)
        ElseIf resultado = "656" Then 'consumo indevido
            nota.statusDaNota = 19 'se der como consumo indevido, o sistema tentará buscar o status
            nota.retEnviNFe_cStat = "656"
            nota.retEnviNFe_xMotivo = "Rejeição: Consumo Indevido na Sefaz."
            notaDAO.alterarNota(nota)
            inserirHistorico(15, "Rejeição: Consumo Indevido, sistema tentando consultar novamente em 60 segundos.", nota)
        Else 'lote rejeitado
            nota.statusDaNota = 3
            nota.retEnviNFe_cStat = resultado
            nota.retEnviNFe_xMotivo = xMotivo
            notaDAO.alterarNota(nota)
            inserirHistorico(9, nota.retEnviNFe_xMotivo, nota)
        End If
        Return True

    End Function
    Private Shared Function gerarAnexo(ByVal nota As notaVO, ByVal protnfe As XmlDocument)
        Try
            Dim nfe As New XmlDocument

            'carrega os arquivos

            nfe.Load(nota.pastaDeTrabalho & nota.NFe_ide_nNF & "_assinado.xml")
            nfe.PreserveWhitespace = True

            protnfe.PreserveWhitespace = True

            Dim proc As New XmlDocument
            proc.Load(System.AppDomain.CurrentDomain.BaseDirectory & "XML\procNFe.xml")
            proc.PreserveWhitespace = True

            proc.ChildNodes(1).AppendChild(proc.ImportNode(nfe.ChildNodes(0), True))
            If protnfe.SelectSingleNode("/*[local-name()='retConsReciNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='protNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]") Is Nothing Then
                proc.ChildNodes(1).AppendChild(proc.ImportNode(protnfe.SelectSingleNode("/retConsReciNFe/protNFe[1]"), True))
            Else
                proc.ChildNodes(1).AppendChild(proc.ImportNode(protnfe.SelectSingleNode("/*[local-name()='retConsReciNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='protNFe' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]"), True))
            End If


            proc.Save(nota.pastaDeTrabalho & nota.NFe_ide_nNF & "_procNFe.xml")
            Return True

        Catch ex As Exception
            Log.registrarErro("Ocorreu um erro na geração do procNFe" & ex.Message & vbCrLf & ex.StackTrace, "RetornoService")
            Return False

        End Try
    End Function
    Private Shared Function obterCertificado() As X509Certificate2
        '1o. abrir o repositorio de certificados
        Dim store As New X509Store(StoreName.My, StoreLocation.LocalMachine)
        store.Open(OpenFlags.ReadOnly)

        'estava testando aqui a rotina de exportar o xml

        Dim todosOsCertificados = store.Certificates

        '2o. localizar o certificado pelo serial number
        Dim cert As New X509Certificate2
        Dim certCollection As New X509Certificate2Collection
        Dim configuracao = configuracaoDAO.obterConfiguracao("serialSSL", 1)

        If configuracao Is Nothing Then
            Log.registrarErro("Serial do certificado não configurado.", "EntradaTxtService")
            Return cert
        End If

        Dim serialSSL = configuracao.valor

        certCollection = store.Certificates.Find(X509FindType.FindBySerialNumber, serialSSL, False)

        If certCollection.Count > 0 Then
            cert = certCollection(0)
        Else
            certCollection = store.Certificates.Find(X509FindType.FindBySerialNumber, serialSSL.Replace(" ", String.Empty), False)

            If certCollection.Count > 0 Then
                cert = certCollection(0)
            Else
                Log.registrarErro("Certificado não encontrado", "EntradaTxtService")
            End If
        End If

        Return cert
    End Function

    Private Shared Function obterCertificado(ByVal idEmpresa As Integer) As X509Certificate2
        '1o. abrir o repositorio de certificados
        Dim store As New X509Store(StoreName.My, StoreLocation.LocalMachine)
        Dim achou As Integer = 0

        store.Open(OpenFlags.ReadOnly)

        Dim todosOsCertificados = store.Certificates

        'estava testando aqui a rotina de exportar o xml

        '2o. localizar o certificado pelo serial number
        Dim cert As New X509Certificate2
        Dim certCollection As New X509Certificate2Collection
        Dim configuracao = configuracaoDAO.obterConfiguracao("serialSSL", idEmpresa)

        If configuracao Is Nothing Then
            Log.registrarErro("Serial do certificado não configurado.", "EntradaTxtService")
            Return cert
        End If

        Dim serialSSL = configuracao.valor

        'certCollection = todosOsCertificados.Find(X509FindType.FindBySerialNumber, serialSSL, False)

        certCollection = todosOsCertificados.Find(X509FindType.FindBySerialNumber, serialSSL, False)

        If certCollection.Count > 0 Then
            cert = certCollection(0)
        Else
            certCollection = store.Certificates.Find(X509FindType.FindBySerialNumber, serialSSL.Replace(" ", String.Empty), False)

            If certCollection.Count > 0 Then
                cert = certCollection(0)
            Else
                certCollection = store.Certificates.Find(X509FindType.FindBySerialNumber, serialSSL.Replace(" ", String.Empty).ToUpper(), False)

                If certCollection.Count > 0 Then
                    cert = certCollection(0)
                Else
                    For Each item As X509Certificate2 In todosOsCertificados
                        If item.SerialNumber = serialSSL.Replace(Space(1), String.Empty).ToUpper() Then
                            cert = item
                            achou = 1
                            Exit For
                        End If
                    Next
                    If achou = 0 Then
                        Log.registrarErro("Certificado não encontrado", "EntradaTxtService")
                    End If
                End If
            End If
        End If
        Return cert
    End Function

    Private Shared Function obterCertificado(ByVal idEmpresa As Integer, ByVal Servico As String) As X509Certificate2
        '1o. abrir o repositorio de certificados
        Dim store As New X509Store(StoreName.My, StoreLocation.LocalMachine)
        store.Open(OpenFlags.ReadOnly)

        'estava testando aqui a rotina de exportar o xml


        '2o. localizar o certificado pelo serial number
        Dim cert As New X509Certificate2
        Dim certCollection As New X509Certificate2Collection
        Dim configuracao = configuracaoDAO.obterConfiguracao("serialSSL", idEmpresa)

        If configuracao Is Nothing Then
            Log.registrarErro("Serial do certificado não configurado.", Servico)
            Return cert
        End If

        Dim serialSSL = configuracao.valor

        Log.registrarInfo("Serial number localizado para esta empresa: " + serialSSL, Servico)

        certCollection = store.Certificates.Find(X509FindType.FindBySerialNumber, serialSSL, False)

        If certCollection.Count > 0 Then
            cert = certCollection(0)
        Else
            certCollection = store.Certificates.Find(X509FindType.FindBySerialNumber, serialSSL.Replace(" ", String.Empty), False)

            If certCollection.Count > 0 Then
                cert = certCollection(0)
            Else
                Log.registrarErro("Certificado não encontrado", Servico)
            End If
        End If

        Return cert
    End Function

    Public Shared Function ObterExceptionMessagesEmCascata(ByVal ex As Exception) As String
        Dim saida As String

        saida = ex.Message & vbCrLf

        If Not ex.InnerException Is Nothing Then
            saida = saida & ObterExceptionMessagesEmCascata(ex.InnerException)
        End If

        Return saida
    End Function

    Public Shared Function ObterStackTraceEmCascata(ByVal ex As Exception) As String
        Dim saida As String

        saida = ex.StackTrace & vbCrLf

        If Not ex.InnerException Is Nothing Then
            saida = saida & ObterStackTraceEmCascata(ex.InnerException)
        End If

        Return saida
    End Function

    Public Shared Function ValidarFormatoDeEmail(ByVal email As String) As Boolean
        Dim padrao As String = "^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
        Dim verificacao As Match = Regex.Match(email, padrao)

        Return verificacao.Success
    End Function

    ''' <summary>
    ''' Returns the missing numbers in a sequence.   
    ''' </summary>   
    ''' <param name="strNumbers">Expects a string of comma-delimited numbers. ex: "1,2,4,6,7"</param>   
    ''' <returns></returns>   
    ''' <remarks></remarks>   
    Public Shared Function FindMissingNumbers(ByVal strNumbers As String) As List(Of Integer)
        Dim MissingNumbers As New List(Of Integer)
        Dim arNumbers() As String
        arNumbers = Split(strNumbers, ",")
        arNumbers = SortNumbers(arNumbers)

        For I = 0 To UBound(arNumbers) - 1
            If CLng(arNumbers(I)) + 1 <> CLng(arNumbers(I + 1)) Then
                For J = 1 To (CLng(arNumbers(I + 1)) - CLng(arNumbers(I))) - 1
                    MissingNumbers.Add(CStr(CLng(arNumbers(I)) + J))
                Next
            End If
        Next

        Return MissingNumbers
    End Function

    ''' <summary>  
    ''' Sorts the array of numbers in value order, least to greatest.  
    ''' </summary>  
    ''' <param name="arNumbers">Send in a string() array of numbers</param>  
    ''' <returns></returns>  
    ''' <remarks></remarks>  
    Private Shared Function SortNumbers(ByVal arNumbers() As String) As String()
        Dim tmpNumber As String
        For J = 0 To UBound(arNumbers) - 1
            For I = J + 1 To UBound(arNumbers)
                If arNumbers(I) - arNumbers(J) < 0 Then
                    tmpNumber = arNumbers(J)
                    arNumbers(J) = arNumbers(J)
                    arNumbers(I) = tmpNumber
                End If
            Next
        Next

        Return arNumbers
    End Function

    Private Shared Function SortNumbers(ByVal numbers As List(Of Integer)) As List(Of Integer)
        Dim tmpNumber As String

        For J = 0 To numbers.Count
            For I = J + 1 To numbers.Count
                If numbers(I) - numbers(J) < 0 Then
                    tmpNumber = numbers(J)
                    numbers(J) = numbers(J)
                    numbers(I) = tmpNumber
                End If
            Next
        Next

        Return numbers
    End Function
    Private Shared Sub ValidationEvent(ByVal sender As Object, ByVal e As ValidationEventArgs)
        'erros.Add(New erro(vargs.Severity, vargs.Message, vargs.Exception.SourceUri, vargs.Exception.LineNumber, vargs.Exception.LinePosition))
        'If vargs.Message.IndexOf("The element 'NFe' in namespace 'http://www.portalfiscal.inf.br/nfe' has incomplete content. List of possible elements expected: 'Signature' in namespace") = -1 Then
        '    Throw New Exception("Erro de validação de schema. Arquivo " & vargs.Exception.SourceUri & _
        '          " (" & vargs.Exception.LineNumber & "," & vargs.Exception.LinePosition & _
        '          "): severidade " & vargs.Severity & " - " & vargs.Message)
        'End If

        Try
            If Not e.Message.Contains("The element 'NFe' in namespace 'http://www.portalfiscal.inf.br/nfe' has incomplete content. List of possible elements expected: 'Signature' in namespace") _
            And Not e.Message.Contains("O elemento 'NFe' no espaço para nome 'http://www.portalfiscal.inf.br/nfe' apresenta conteúdo incompleto. Lista de possíveis elementos esperados: 'Signature' no espaço para nome") Then
                Select Case e.Severity
                    Case XmlSeverityType.Error
                        resultadoValidacao.AppendLine("Erro(linha " & e.Exception.LineNumber & " posição " & e.Exception.LinePosition & "): " & e.Message)
                        resultadoValidacao.AppendLine("---")
                    Case XmlSeverityType.Warning
                        resultadoValidacao.AppendLine("Alerta: " & e.Message)
                        resultadoValidacao.AppendLine("---")
                End Select
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#Region "Acessorios"

    Public Shared Function inserirHistorico(ByVal tipo As String, ByVal descricao As String, ByVal nota As notaVO)
        Dim hist As New historicoVO(tipo, descricao, nota.NFe_ide_nNF, nota.NFe_emit_CNPJ, nota.serie)
        notaDAO.inserirHistorico(hist)

        Retorno.escreverRetorno(nota, DateTime.Now, tipo, descricao)

        Return True


    End Function

#End Region

End Class
