﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.17929
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization

'
'This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.17929.
'
Namespace NFe.ConsultaServicos
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Web.Services.WebServiceBindingAttribute(Name:="NfeStatusServico2Soap", [Namespace]:="http://www.portalfiscal.inf.br/nfe/wsdl/NfeStatusServico2")>  _
    Partial Public Class NfeStatusServico2
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol
        
        Private nfeCabecMsgValueField As nfeCabecMsg
        
        Private nfeStatusServicoNF2OperationCompleted As System.Threading.SendOrPostCallback
        
        Private useDefaultCredentialsSetExplicitly As Boolean
        
        '''<remarks/>
        Public Sub New()
            MyBase.New
            Me.Url = Global.FN4EnvioCtl.My.MySettings.Default.FN4EnvioCtl_NFe_ConsultaServicos_NfeStatusServico2
            If (Me.IsLocalFileSystemWebService(Me.Url) = true) Then
                Me.UseDefaultCredentials = true
                Me.useDefaultCredentialsSetExplicitly = false
            Else
                Me.useDefaultCredentialsSetExplicitly = true
            End If
        End Sub
        
        Public Property nfeCabecMsgValue() As nfeCabecMsg
            Get
                Return Me.nfeCabecMsgValueField
            End Get
            Set
                Me.nfeCabecMsgValueField = value
            End Set
        End Property
        
        Public Shadows Property Url() As String
            Get
                Return MyBase.Url
            End Get
            Set
                If (((Me.IsLocalFileSystemWebService(MyBase.Url) = true)  _
                            AndAlso (Me.useDefaultCredentialsSetExplicitly = false))  _
                            AndAlso (Me.IsLocalFileSystemWebService(value) = false)) Then
                    MyBase.UseDefaultCredentials = false
                End If
                MyBase.Url = value
            End Set
        End Property
        
        Public Shadows Property UseDefaultCredentials() As Boolean
            Get
                Return MyBase.UseDefaultCredentials
            End Get
            Set
                MyBase.UseDefaultCredentials = value
                Me.useDefaultCredentialsSetExplicitly = true
            End Set
        End Property
        
        '''<remarks/>
        Public Event nfeStatusServicoNF2Completed As nfeStatusServicoNF2CompletedEventHandler
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapHeaderAttribute("nfeCabecMsgValue"),  _
         System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.portalfiscal.inf.br/nfe/wsdl/NfeStatusServico2/nfeStatusServicoNF2", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Bare)>  _
        Public Function nfeStatusServicoNF2(<System.Xml.Serialization.XmlElementAttribute([Namespace]:="http://www.portalfiscal.inf.br/nfe/wsdl/NfeStatusServico2")> ByVal nfeDadosMsg As System.Xml.XmlNode) As <System.Xml.Serialization.XmlElementAttribute([Namespace]:="http://www.portalfiscal.inf.br/nfe/wsdl/NfeStatusServico2")> System.Xml.XmlNode
            Dim results() As Object = Me.Invoke("nfeStatusServicoNF2", New Object() {nfeDadosMsg})
            Return CType(results(0),System.Xml.XmlNode)
        End Function
        
        '''<remarks/>
        Public Overloads Sub nfeStatusServicoNF2Async(ByVal nfeDadosMsg As System.Xml.XmlNode)
            Me.nfeStatusServicoNF2Async(nfeDadosMsg, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub nfeStatusServicoNF2Async(ByVal nfeDadosMsg As System.Xml.XmlNode, ByVal userState As Object)
            If (Me.nfeStatusServicoNF2OperationCompleted Is Nothing) Then
                Me.nfeStatusServicoNF2OperationCompleted = AddressOf Me.OnnfeStatusServicoNF2OperationCompleted
            End If
            Me.InvokeAsync("nfeStatusServicoNF2", New Object() {nfeDadosMsg}, Me.nfeStatusServicoNF2OperationCompleted, userState)
        End Sub
        
        Private Sub OnnfeStatusServicoNF2OperationCompleted(ByVal arg As Object)
            If (Not (Me.nfeStatusServicoNF2CompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent nfeStatusServicoNF2Completed(Me, New nfeStatusServicoNF2CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        Public Shadows Sub CancelAsync(ByVal userState As Object)
            MyBase.CancelAsync(userState)
        End Sub
        
        Private Function IsLocalFileSystemWebService(ByVal url As String) As Boolean
            If ((url Is Nothing)  _
                        OrElse (url Is String.Empty)) Then
                Return false
            End If
            Dim wsUri As System.Uri = New System.Uri(url)
            If ((wsUri.Port >= 1024)  _
                        AndAlso (String.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) = 0)) Then
                Return true
            End If
            Return false
        End Function
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17929"),  _
     System.SerializableAttribute(),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.portalfiscal.inf.br/nfe/wsdl/NfeStatusServico2"),  _
     System.Xml.Serialization.XmlRootAttribute([Namespace]:="http://www.portalfiscal.inf.br/nfe/wsdl/NfeStatusServico2", IsNullable:=false)>  _
    Partial Public Class nfeCabecMsg
        Inherits System.Web.Services.Protocols.SoapHeader
        
        Private versaoDadosField As String
        
        Private cUFField As String
        
        Private anyAttrField() As System.Xml.XmlAttribute
        
        '''<remarks/>
        Public Property versaoDados() As String
            Get
                Return Me.versaoDadosField
            End Get
            Set
                Me.versaoDadosField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property cUF() As String
            Get
                Return Me.cUFField
            End Get
            Set
                Me.cUFField = value
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlAnyAttributeAttribute()>  _
        Public Property AnyAttr() As System.Xml.XmlAttribute()
            Get
                Return Me.anyAttrField
            End Get
            Set
                Me.anyAttrField = value
            End Set
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")>  _
    Public Delegate Sub nfeStatusServicoNF2CompletedEventHandler(ByVal sender As Object, ByVal e As nfeStatusServicoNF2CompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class nfeStatusServicoNF2CompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As System.Xml.XmlNode
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),System.Xml.XmlNode)
            End Get
        End Property
    End Class
End Namespace
