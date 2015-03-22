Imports System.Drawing.Printing
Imports System.Xml
Imports System.Xml.Linq.XDocument
Imports System.IO
Imports System.Net
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

        If ds.Tables("date_print").Rows.Count > 0 Then
            MonthCalendar1.SetSelectionRange(ds.Tables("date_print").Rows(0)(0).ToString(), ds.Tables("date_print").Rows(0)(1).ToString())
        End If

        If ds.Tables("process_today").Rows(0)(0).ToString() = "1" Then
            RadioButton1.Checked = True
        Else
            RadioButton2.Checked = True
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



        doc.Load(Application.StartupPath & "\XMLDOC.XML")
        'm_nodelist = doc.SelectNodes("/Detail/printer")
        Dim m_node As XmlNode = doc.SelectSingleNode("/Detail/printer")
        m_node.ChildNodes(0).InnerText = cmbprintername.Text

        m_node = doc.SelectSingleNode("/Detail/Pdf_location")
        m_node.ChildNodes(0).InnerText = txtpdfpath.Text

        m_node = doc.SelectSingleNode("/Detail/System_ip")
        m_node.ChildNodes(0).InnerText = cmbip.Text

        If (RadioButton1.Checked) Then

            m_node = doc.SelectSingleNode("/Detail/date_print")
            m_node.ChildNodes(0).InnerText = System.DateTime.Now
            m_node.ChildNodes(1).InnerText = System.DateTime.Now

            m_node = doc.SelectSingleNode("/Detail/process_today")
            m_node.ChildNodes(0).InnerText = "1"

        Else

            m_node = doc.SelectSingleNode("/Detail/date_print")
            m_node.ChildNodes(0).InnerText = MonthCalendar1.SelectionRange.Start
            m_node.ChildNodes(1).InnerText = MonthCalendar1.SelectionRange.End

            m_node = doc.SelectSingleNode("/Detail/process_today")
            m_node.ChildNodes(0).InnerText = "2"

        End If

        doc.Save(Application.StartupPath & "\XMLDOC.XML")
        MsgBox("Parâmetros configurados com sucesso!")

        If (btnsave.Text = "Configurar parâmetros") Then
            Icon = My.Resources.TrayIcon
            Dim coon As AppContext = New AppContext()

            Dim mdifrm As New MDI()
            mdifrm.Label1.Text = "Status: Parado"
            System.Windows.Forms.Application.DoEvents()

            mdifrm.Show()
            Me.Close()
        End If

        If (btnsave.Text = "Atualizar e iniciar") Then
            ' Icon = My.Resources.TrayIcon
            Dim mdifrm As New MDI()
            mdifrm.Label1.Text = "Status: Parado"
            System.Windows.Forms.Application.DoEvents()

            mdifrm.Show()
            Me.Close()
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


    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.Click

    End Sub

    Private Sub Label4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label4.Click

    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged

    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged

    End Sub

    Private Sub MonthCalendar1_DateChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DateRangeEventArgs) Handles MonthCalendar1.DateChanged
        RadioButton2.Checked = True
    End Sub

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click


        'If (status_while = False) Then
        Dim ds As New DataSet()
        Dim ds_login As New DataSet()
        Dim req As WebRequest
        Dim str As WebResponse

        ds_login.ReadXml(Application.StartupPath & "\XMLDOC_login_detail.xml")
        ds = New DataSet()
        ds.ReadXml(Application.StartupPath & "\XMLDOC.xml")

        Dim datestrstart = String.Empty
        Dim datestrend = String.Empty
        datestrstart = MonthCalendar1.SelectionRange.Start.Date.ToString.Substring(0, 10)
        datestrend = MonthCalendar1.SelectionRange.End.Date.ToString.Substring(0, 10)

        Dim url2 = String.Empty
        If ds.Tables("process_today").Rows(0)(0).ToString() = "1" Then
            url2 = "&process_today=1"
        Else
            url2 = "&process_today=2&date_ini=" + datestrstart.Split("/")(2) + "-" + datestrstart.Split("/")(1) + "-" + datestrstart.Split("/")(0) + "&date_end=" + datestrend.Split("/")(2) + "-" + datestrend.Split("/")(1) + "-" + datestrend.Split("/")(0)
        End If

        Dim url = String.Empty


        url = ds_login.Tables("Login").Rows(0)("url").ToString().Replace("print_params", "renew_nfeprinter") + "?cnpj=" + ds_login.Tables("Login").Rows(0)("CNPJ").ToString() + "&token=" + ds_login.Tables("Login").Rows(0)("TOKEN").ToString() + url2

        req = WebRequest.Create(New Uri(url))
        str = req.GetResponse()
        MsgBox("Registros atualizados.")

    End Sub
End Class