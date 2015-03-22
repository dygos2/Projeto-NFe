Imports System.Xml
Imports System.Security.Cryptography.Xml
Imports System.Security.Cryptography.X509Certificates

Namespace Helpers

    Public Class XmlHelper

        Public Shared Function assinarNFeXML(ByVal xmlDoc As XmlDocument, ByVal referenceURI As String) As XmlDocument


            ' Checar argumentos
            If xmlDoc Is Nothing Then
                Throw New ArgumentException("Doc")
            End If

            Dim cert As X509Certificate2

            cert = Geral.Certificado

            ' Cria um objeto SignedXML
            'Dim xmltemp As New XmlDocument
            'xmltemp.LoadXml(xmlDoc.InnerXml.Replace("xmlns=""http://www.portalfiscal.inf.br/nfe""", ""))
            'por defaul assina o elemento abaixo
            If xmlDoc.ChildNodes(0).Name = "xml" Then
                xmlDoc.RemoveChild(xmlDoc.ChildNodes(0))
            End If

            Dim signedXml As New SignedXml(xmlDoc)

            ' Adiciona a key ao SignedXML
            signedXml.SigningKey = cert.PrivateKey

            ' Cria o elemento referencia
            Dim reference As New Reference()
            reference.Uri = "#" & referenceURI

            ' Adiciona os elementos Enveloped Transformation
            Dim transEnveloped As New XmlDsigEnvelopedSignatureTransform()
            reference.AddTransform(transEnveloped)

            ' reference.DigestMethod = "http://www.w3.org/2000/09/xmldsig#sha1"

            ' E canonic transformation ao elemento Reference
            Dim transCanonic As New XmlDsigC14NTransform
            reference.AddTransform(transCanonic)

            ' Adiciona o elemente referencia ao XML Assinado
            signedXml.AddReference(reference)

            'adiciona a KeyInfo à assinatura
            signedXml.KeyInfo.AddClause(New KeyInfoX509Data(cert))

            'Calcula a Assinatura
            signedXml.ComputeSignature()


            'Converte a assinatura em um XmlElement 
            Dim xmlDigitalSignature As XmlElement = signedXml.GetXml()


            ' Adiciona o elemento assinatura à NFe
            xmlDoc.DocumentElement.AppendChild(xmlDoc.ImportNode(xmlDigitalSignature, True))
            Return xmlDoc

            '' Checar argumentos
            'If xmlDoc Is Nothing Then
            '    Throw New ArgumentException("Doc")
            'End If

            ''1o. abrir o repositorio de certificados
            'Dim store As New X509Store(StoreName.My, StoreLocation.CurrentUser)
            'store.Open(OpenFlags.ReadOnly)

            ''2o. localizar o certificado pelo serial number
            'Dim cert As X509Certificate2 = FN4Common.Geral.Certificado

            '' Cria um objeto SignedXML
            ''Dim xmltemp As New XmlDocument
            ''xmltemp.LoadXml(xmlDoc.InnerXml.Replace("xmlns=""http://www.portalfiscal.inf.br/nfe""", ""))
            ''por defaul assina o elemento abaixo
            'If xmlDoc.ChildNodes(0).Name = "xml" Then
            '    xmlDoc.RemoveChild(xmlDoc.ChildNodes(0))
            'End If

            'Dim signedXml As New SignedXml(CType(xmlDoc.ChildNodes(0).ChildNodes(0), XmlElement))

            '' Cria o elemento referencia
            'Dim reference As New Reference()
            'reference.Uri = "#" & referenceURI

            '' Adiciona os elementos Enveloped Transformation
            'Dim transEnveloped As New XmlDsigEnvelopedSignatureTransform()
            'reference.AddTransform(transEnveloped)

            ''  reference.DigestMethod = "http://www.w3.org/2000/09/xmldsig#sha1"

            '' E canonic transformation ao elemento Reference
            'Dim transCanonic As New XmlDsigC14NTransform
            'reference.AddTransform(transCanonic)

            '' Adiciona o elemente referencia ao XML Assinado
            'signedXml.AddReference(reference)

            ''adiciona a KeyInfo à assinatura
            'Dim kinfo As New KeyInfoX509Data(cert)
            'signedXml.KeyInfo.AddClause(kinfo)

            ''dim chave as new rsacryptoserviceprovider

            '' Adiciona a key ao SignedXML
            'signedXml.SigningKey = cert.PrivateKey

            ''Calcula a Assinatura
            'signedXml.ComputeSignature()


            ''Converte a assinatura em um XmlElement 
            'Dim xmlDigitalSignature As XmlElement = signedXml.GetXml()


            '' Adiciona o elemento assinatura à NFe
            'xmlDoc.DocumentElement.AppendChild(xmlDoc.ImportNode(xmlDigitalSignature, True))
            'Return xmlDoc
        End Function

        Public Shared Function assinarNFeXML(ByVal xmlDoc As XmlDocument, ByVal referenceURI As String, ByVal idEmpresa As Integer) As XmlDocument


            ' Checar argumentos
            If xmlDoc Is Nothing Then
                Throw New ArgumentException("Doc")
            End If

            Dim cert As X509Certificate2

            cert = Geral.ObterCertificadoPorEmpresa(idEmpresa)


            ' Cria um objeto SignedXML
            'Dim xmltemp As New XmlDocument
            'xmltemp.LoadXml(xmlDoc.InnerXml.Replace("xmlns=""http://www.portalfiscal.inf.br/nfe""", ""))
            'por defaul assina o elemento abaixo
            If xmlDoc.ChildNodes(0).Name = "xml" Then
                xmlDoc.RemoveChild(xmlDoc.ChildNodes(0))
            End If

            Dim signedXml As New SignedXml(xmlDoc)

            ' Adiciona a key ao SignedXML
            signedXml.SigningKey = cert.PrivateKey

            ' Cria o elemento referencia
            Dim reference As New Reference()
            reference.Uri = "#" & referenceURI

            ' Adiciona os elementos Enveloped Transformation
            Dim transEnveloped As New XmlDsigEnvelopedSignatureTransform()
            reference.AddTransform(transEnveloped)

            ' reference.DigestMethod = "http://www.w3.org/2000/09/xmldsig#sha1"

            ' E canonic transformation ao elemento Reference
            Dim transCanonic As New XmlDsigC14NTransform
            reference.AddTransform(transCanonic)

            ' Adiciona o elemente referencia ao XML Assinado
            signedXml.AddReference(reference)

            'adiciona a KeyInfo à assinatura
            signedXml.KeyInfo.AddClause(New KeyInfoX509Data(cert))

            'Calcula a Assinatura
            signedXml.ComputeSignature()


            'Converte a assinatura em um XmlElement 
            Dim xmlDigitalSignature As XmlElement = signedXml.GetXml()


            ' Adiciona o elemento assinatura à NFe
            xmlDoc.DocumentElement.AppendChild(xmlDoc.ImportNode(xmlDigitalSignature, True))
            Return xmlDoc

            '' Checar argumentos
            'If xmlDoc Is Nothing Then
            '    Throw New ArgumentException("Doc")
            'End If

            ''1o. abrir o repositorio de certificados
            'Dim store As New X509Store(StoreName.My, StoreLocation.CurrentUser)
            'store.Open(OpenFlags.ReadOnly)

            ''2o. localizar o certificado pelo serial number
            'Dim cert As X509Certificate2 = FN4Common.Geral.Certificado

            '' Cria um objeto SignedXML
            ''Dim xmltemp As New XmlDocument
            ''xmltemp.LoadXml(xmlDoc.InnerXml.Replace("xmlns=""http://www.portalfiscal.inf.br/nfe""", ""))
            ''por defaul assina o elemento abaixo
            'If xmlDoc.ChildNodes(0).Name = "xml" Then
            '    xmlDoc.RemoveChild(xmlDoc.ChildNodes(0))
            'End If

            'Dim signedXml As New SignedXml(CType(xmlDoc.ChildNodes(0).ChildNodes(0), XmlElement))

            '' Cria o elemento referencia
            'Dim reference As New Reference()
            'reference.Uri = "#" & referenceURI

            '' Adiciona os elementos Enveloped Transformation
            'Dim transEnveloped As New XmlDsigEnvelopedSignatureTransform()
            'reference.AddTransform(transEnveloped)

            ''  reference.DigestMethod = "http://www.w3.org/2000/09/xmldsig#sha1"

            '' E canonic transformation ao elemento Reference
            'Dim transCanonic As New XmlDsigC14NTransform
            'reference.AddTransform(transCanonic)

            '' Adiciona o elemente referencia ao XML Assinado
            'signedXml.AddReference(reference)

            ''adiciona a KeyInfo à assinatura
            'Dim kinfo As New KeyInfoX509Data(cert)
            'signedXml.KeyInfo.AddClause(kinfo)

            ''dim chave as new rsacryptoserviceprovider

            '' Adiciona a key ao SignedXML
            'signedXml.SigningKey = cert.PrivateKey

            ''Calcula a Assinatura
            'signedXml.ComputeSignature()


            ''Converte a assinatura em um XmlElement 
            'Dim xmlDigitalSignature As XmlElement = signedXml.GetXml()


            '' Adiciona o elemento assinatura à NFe
            'xmlDoc.DocumentElement.AppendChild(xmlDoc.ImportNode(xmlDigitalSignature, True))
            'Return xmlDoc
        End Function

        Public Shared Function obterUmCabecalho(ByVal versaoDosDados As String) As XmlDocument
            Try
                Dim cabecMsg As New XmlDocument
                cabecMsg.Load(System.AppDomain.CurrentDomain.BaseDirectory() & "XML\cabecMsg.xml")
                cabecMsg.SelectSingleNode("/*[local-name()='cabecMsg' and namespace-uri()='http://www.portalfiscal.inf.br/nfe']/*[local-name()='versaoDados' and namespace-uri()='http://www.portalfiscal.inf.br/nfe'][1]").InnerText = versaoDosDados
                Return cabecMsg
            Catch ex As Exception
                Log.registrarErro("Erro ao obter o cabecalho: " & ex.Message & vbCrLf & ex.StackTrace, "EnvioService")
                Throw ex
            End Try
        End Function

    End Class
End Namespace