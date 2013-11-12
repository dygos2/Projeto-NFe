Imports System.Drawing.Printing
Imports System.Xml
Imports System.Xml.Linq.XDocument
Imports System.IO
Imports System.DirectoryServices





Public Class frmprinter

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        cmbprintername.Items.Clear()
        For Each pkInstalledPrinters In _
           PrinterSettings.InstalledPrinters
            cmbprintername.Items.Add(pkInstalledPrinters)
        Next pkInstalledPrinters
        check()
        'Set the combo to the first printer in the list
        cmbprintername.SelectedIndex = 0
        Dim ds As New DataSet()
        ds.ReadXml(Application.StartupPath & "\XMLDOC.xml")
        If ds.Tables("printer").Rows.Count > 0 Then
            cmbprintername.Text = ds.Tables("printer").Rows(0)(0).ToString()
        End If
        If ds.Tables("Pdf_location").Rows.Count > 0 Then
            txtpdfpath.Text = ds.Tables("Pdf_location").Rows(0)(0).ToString()
        End If
        If ds.Tables("System_ip").Rows.Count > 0 Then
            cmbip.Text = ds.Tables("System_ip").Rows(0)(0).ToString()
        End If
        'Add any initialization after the InitializeComponent() call.

    End Sub
    Sub New(ByVal pr As String)
        InitializeComponent()
        '  cmbprintername.Items.Clear()
        For Each pkInstalledPrinters In _
           PrinterSettings.InstalledPrinters
            cmbprintername.Items.Add(pkInstalledPrinters)
        Next pkInstalledPrinters

        check()
        ' Set the combo to the first printer in the list
        'cmbprintername.SelectedIndex = 0
        'Dim ds As New DataSet()
        'ds.ReadXml(Application.StartupPath & "\XMLDOC.xml")
        'If ds.Tables("printer").Rows.Count > 0 Then
        '    cmbprintername.Text = ds.Tables("printer").Rows(0)(0).ToString()
        'End If
        'If ds.Tables("Pdf_location").Rows.Count > 0 Then
        '    txtpdfpath.Text = ds.Tables("Pdf_location").Rows(0)(0).ToString()
        'End If
    End Sub


    Private Sub frmprinter_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Dim pkInstalledPrinters As String

        '' Find all printers installed
        'For Each pkInstalledPrinters In _
        '    PrinterSettings.InstalledPrinters
        '    cmbprintername.Items.Add(pkInstalledPrinters)
        'Next pkInstalledPrinters
        '' Set the combo to the first printer in the list
        'cmbprintername.SelectedIndex = 0

       


        'Me.MdiParent = MDI
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click

        ''Dim xml As XElement("Printer_detail", New XElement("Printer", New XElement("Printer_name", cmbprintername.SelectedText)))
        ''xml.Save(Application.StartupPath & "\XMLDOC.XML")
        'Dim xml_doc As XDocument = XDocument.Load(Application.StartupPath & "\XMLDOC.XML")
        ''Dim xml As XElement = New XElement("printer_detail")
        'Dim xml As New XElement("printer", New XElement("Printer_name", cmbprintername.Text))
        ''xml.Add(New XAttribute("Printer_name", cmbprintername.Text))
        '' xml.Save(Application.StartupPath & "\XMLDOC.XML")
        'xml_doc.Save(xml)
        ''xml_doc.Save(Application.StartupPath & "\XMLDOC.XML")

        If (btnsave.Text = "Configurar parâmetros") Then

            Dim doc As New XmlDocument
            doc.Load(Application.StartupPath & "\XMLDOC.XML")
            Dim visitor As XmlElement = doc.CreateElement("printer")
            Dim nameEl As XmlElement = doc.CreateElement("Printer_name")
            nameEl.InnerText = cmbprintername.Text

            visitor.AppendChild(nameEl)

            doc.DocumentElement.AppendChild(visitor)

            visitor = doc.CreateElement("Pdf_location")
            nameEl = doc.CreateElement("Path")
            nameEl.InnerText = txtpdfpath.Text

            visitor.AppendChild(nameEl)

            doc.DocumentElement.AppendChild(visitor)

            visitor = doc.CreateElement("System_ip")
            nameEl = doc.CreateElement("Ip")
            nameEl.InnerText = cmbip.Text()

            visitor.AppendChild(nameEl)

            doc.DocumentElement.AppendChild(visitor)


            doc.Save(Application.StartupPath & "\XMLDOC.XML")
            Icon = My.Resources.TrayIcon
            Dim coon As AppContext = New AppContext()

            Me.Close()
        End If

        If (btnsave.Text = "Atualizar e iniciar") Then


            doc.Load(Application.StartupPath & "\XMLDOC.XML")
            'm_nodelist = doc.SelectNodes("/Detail/printer")
            Dim m_node As XmlNode = doc.SelectSingleNode("/Detail/printer")
            If (m_node IsNot Nothing) Then
                m_node.ChildNodes(0).InnerText = cmbprintername.Text

                doc.Save(Application.StartupPath & "\XMLDOC.XML")

            End If
            m_node = doc.SelectSingleNode("/Detail/Pdf_location")
            If (m_node IsNot Nothing) Then
                m_node.ChildNodes(0).InnerText = txtpdfpath.Text

                doc.Save(Application.StartupPath & "\XMLDOC.XML")


                ' Icon = My.Resources.TrayIcon
                ' Me.Close()

            End If
            m_node = doc.SelectSingleNode("/Detail/System_ip")
            If (m_node IsNot Nothing) Then
                m_node.ChildNodes(0).InnerText = cmbip.Text

                doc.Save(Application.StartupPath & "\XMLDOC.XML")


                ' Icon = My.Resources.TrayIcon
                Me.Close()

            End If
        End If
        'Dim m As New MDI()
        'm.Show()

       



    End Sub

    Dim doc As XmlDocument = New XmlDocument()
    Dim m_nodelist As XmlNodeList

    Private Sub StreamWriter(ByVal p1 As Object)
        Throw New NotImplementedException
    End Sub

    Private Function sw() As IO.StreamWriter
        Throw New NotImplementedException
    End Function


    Private Sub OpenFileDialog1_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)


    End Sub

    Private Sub btnselectpath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnselectpath.Click
        Dim path As String = String.Empty
        Dim dialog As New FolderBrowserDialog()
        dialog.RootFolder = Environment.SpecialFolder.Desktop
        dialog.SelectedPath = "C:\"
        dialog.Description = "Selecionar o caminho do pdf a ser feito o download"
        If dialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
            path = dialog.SelectedPath
            txtpdfpath.Text = path

        End If
    End Sub

    Dim str(2) As String
    Dim itm As ListViewItem
    Private Sub check()
        cmbip.Items.Clear()
        'ListView1.Columns.Add("Name", 100, HorizontalAlignment.Left)
        'ListView1.Columns.Add("IP Address", 150, HorizontalAlignment.Left)

        Dim childEntry As DirectoryEntry
        Dim ParentEntry As New DirectoryEntry
        Try
            ParentEntry.Path = "WinNT:"
            For Each childEntry In ParentEntry.Children
                Select Case childEntry.SchemaClassName
                    Case "Domain"
                        Dim SubChildEntry As DirectoryEntry
                        Dim SubParentEntry As New DirectoryEntry
                        SubParentEntry.Path = "WinNT://" & childEntry.Name
                        For Each SubChildEntry In SubParentEntry.Children
                            Select Case SubChildEntry.SchemaClassName

                                Case "Computer"

                                    str(0) = SubChildEntry.Name
                                    'str(1) = System.Net.Dns.GetHostByName(SubChildEntry.Name).AddressList(0).ToString() ' get Ip by name
                                    'itm = New ListViewItem(str)
                                    cmbip.Items.Add(str(0))

                                    'ListView1.Items.Add(SubChildEntry.Name)

                                    'MsgBox(System.Net.Dns.GetHostByName(SubChildEntry.Name).AddressList(0).ToString()) ' get Ip by name
                            End Select
                            Dim b As String = SubChildEntry.SchemaClassName.ToString()
                        Next
                End Select
            Next
        Catch Excep As Exception
            MsgBox("Error While Reading Directories")
        Finally
            ParentEntry = Nothing
        End Try
    End Sub


End Class