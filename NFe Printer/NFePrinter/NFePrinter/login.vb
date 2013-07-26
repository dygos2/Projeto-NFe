Imports System.Xml
Imports System.Diagnostics
Imports System.Data
Imports System.Data.SqlClient



Public Class frmlogin
    Dim app_exists As Boolean

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        app_exists = False
        ' Add any initialization after the InitializeComponent() call.
        If System.IO.File.Exists(Application.StartupPath & "\XMLDOC_login_detail.xml") = True Then
            insert_login_time()
            app_exists = True
            Application.Run(New AppContext())
            Me.Hide()
            'Dim main As New MDI
            ' main.Show()
            ' Me.Close()


        End If
       

    End Sub
    Sub insert_login_time()
        Dim doc As New XmlDocument
        doc.Load(Application.StartupPath & "\XMLDOC.XML")
        'm_nodelist = doc.SelectNodes("/Detail/printer")
        Dim m_node As XmlNode = doc.SelectSingleNode("/Detail/Log_detail")

        Dim nameEl As XmlElement = doc.CreateElement("Log_in_Time")
        nameEl.InnerText = System.DateTime.Now
        m_node.AppendChild(nameEl)
        doc.DocumentElement.AppendChild(m_node)
        doc.Save(Application.StartupPath & "\XMLDOC.XML")
    End Sub

    Private Sub btnlogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnlogin.Click
        If (txtcnpj.Text = String.Empty) Then
            MessageBox.Show("CNPJ obrigatório")
            Return

        End If

        If (txttoken.Text = String.Empty) Then
            MessageBox.Show("TOKEN obrigatório")
            Return

        End If
        'create login detail xml
        Dim cr_xml As XmlWriter = XmlWriter.Create(Application.StartupPath & "\XMLDOC_login_detail.XML")
        Dim xml As New XElement("Detail", New XElement("Login", New XElement("CNPJ", txtcnpj.Text), New XElement("TOKEN", txttoken.Text), New XElement("url", "http://amazon-nfe4web.elasticbeanstalk.com/nfe4web/retornos_banco/print_params.php")))
        cr_xml.Close()
        xml.Save(Application.StartupPath & "\XMLDOC_login_detail.XML")

        'create log detail xml
        cr_xml = XmlWriter.Create(Application.StartupPath & "\XMLDOC.XML")
        xml = New XElement("Detail")
        cr_xml.Close()
        xml.Save(Application.StartupPath & "\XMLDOC.XML")
        Dim doc As New XmlDocument
        doc.Load(Application.StartupPath & "\XMLDOC.XML")
        Dim visitor As XmlElement = doc.CreateElement("Log_detail")
        doc.DocumentElement.AppendChild(visitor)
        visitor = doc.CreateElement("Pdf_Print_Detail")
        doc.DocumentElement.AppendChild(visitor)
        visitor = doc.CreateElement("Last_pdf_print")
        doc.DocumentElement.AppendChild(visitor)
        doc.Save(Application.StartupPath & "\XMLDOC.XML")


        doc.Load(Application.StartupPath & "\XMLDOC.XML")
        Dim m_node As XmlNode = doc.SelectSingleNode("/Detail/Last_pdf_print")

        Dim nameEl As XmlElement = doc.CreateElement("Print_Time")
        nameEl.InnerText = System.DateTime.Now
        m_node.AppendChild(nameEl)
        doc.DocumentElement.AppendChild(m_node)
        doc.Save(Application.StartupPath & "\XMLDOC.XML")
        txtcnpj.Text = String.Empty
        txttoken.Text = String.Empty
        insert_login_time()
        Dim main As MDI = New MDI("first")
        ' Application.Run(New AppContext())
        Me.Hide()


        'End If

    End Sub

    Private Sub frmlogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load



        If (app_exists = True) Then
            Me.Close()
        End If

    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click

    End Sub
End Class
