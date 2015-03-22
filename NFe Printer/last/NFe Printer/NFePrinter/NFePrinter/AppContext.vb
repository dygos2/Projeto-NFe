Imports System.IO
Imports System.Drawing.Printing
Imports System.Net
Imports System.Windows.Forms
Imports System.Xml
Public Class AppContext
    Inherits ApplicationContext

#Region " Storage "

    Private WithEvents Tray As NotifyIcon
    Private WithEvents MainMenu As ContextMenuStrip
    Private WithEvents mnuDisplayForm As ToolStripMenuItem
    Private WithEvents mnuSep1 As ToolStripSeparator
    Private WithEvents mnuExit As ToolStripMenuItem

#End Region

#Region " Constructor "

    Public Sub New()
        'Initialize the menus
        mnuDisplayForm = New ToolStripMenuItem("Display form")
        mnuSep1 = New ToolStripSeparator()
        mnuExit = New ToolStripMenuItem("Exit")
        MainMenu = New ContextMenuStrip
        MainMenu.Items.AddRange(New ToolStripItem() {mnuDisplayForm, mnuSep1, mnuExit})

        'Initialize the tra
        Tray = New NotifyIcon
        Tray.Icon = My.Resources.TrayIcon
        Tray.ContextMenuStrip = MainMenu
        Tray.Text = "Sistema NFePrinter"
        Dim m As MDI = New MDI()
        'Display
        Tray.Visible = True
        ' process_start()
    End Sub

#End Region

#Region " Event handlers "

    Private Sub AppContext_ThreadExit(ByVal sender As Object, ByVal e As System.EventArgs) _
    Handles Me.ThreadExit
        'Guarantees that the icon will not linger.
        Tray.Visible = False
    End Sub

    Private Sub mnuDisplayForm_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
    Handles mnuDisplayForm.Click
        ShowDialog()
    End Sub

    Private Sub mnuExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
    Handles mnuExit.Click
        Tray.Visible = False


        ExitApplication()
    End Sub

    Private Sub Tray_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) _
    Handles Tray.DoubleClick
        Dim m As MDI = New MDI()
        m.ShowDialog()
    End Sub

#End Region


#Region " process for printing pdf "
    Public dt_check_url As DateTime = System.DateTime.Now
    Public Sub process_start()
        While (True)

            Dim ds As New DataSet()
            ds.ReadXml(Application.StartupPath & "\XMLDOC.xml")
            Dim url As String
            url = ds.Tables("Login").Rows(0)("url").ToString() + "?cnpj=" + ds.Tables("Login").Rows(0)("CNPJ").ToString() + "&token=" + ds.Tables("Login").Rows(0)("TOKEN").ToString()
            Dim req As WebRequest
            req = WebRequest.Create(New Uri(url))
            Dim str As WebResponse
            str = req.GetResponse()
            Dim reader As StreamReader
            reader = New StreamReader(str.GetResponseStream())
            Dim Data As String = String.Empty
            Data = reader.ReadToEnd()
            Dim status() As String = New String() {2}
            status = Data.Split(",")
            Dim newurl() As String = New String() {2}
            Dim newurl_new() As String = New String() {2}

            If (status(0).Contains("on")) Then
                url = String.Empty
                newurl = status(1).Split("""")
                Dim remove() As Char = newurl(3).ToCharArray(0, newurl(3).Length - 2)
                url = newurl(3) + "?cnpj=" + ds.Tables("Login").Rows(0)("CNPJ").ToString() + "&token=" + ds.Tables("Login").Rows(0)("TOKEN").ToString()


                req = WebRequest.Create(New Uri(url))

                str = req.GetResponse()

                reader = New StreamReader(str.GetResponseStream())
                Dim data_arr() As String = New String() {10}
                Dim data_arr_first_for_loop() As String = New String() {10}
                Data = reader.ReadToEnd()

                data_arr_first_for_loop = Data.Split("},")
                Dim for_id_n_path() As String
                Dim pdf_id As String = String.Empty
                Dim url_new As String
                For pdf_count As Integer = 0 To data_arr_first_for_loop.Count - 2
                    url_new = String.Empty
                    Dim pdfname1 As String
                    for_id_n_path = New String() {3}
                    pdf_id = String.Empty
                    'If (data_arr(pdf_count).Contains("([{")) Then
                    '    'for_id_n_path = data_arr(pdf_count).Split("([{")
                    '    url_new = data_arr(pdf_count).Replace("([{", String.Empty)
                    'ElseIf (data_arr(pdf_count).Contains(",{")) Then
                    '    'for_id_n_path = data_arr(pdf_count).Split(",{")
                    '    url_new = data_arr(pdf_count).Replace(",{", String.Empty)
                    'End If
                    for_id_n_path = data_arr_first_for_loop(pdf_count).Split("""")
                    pdf_id = for_id_n_path(3)
                    url = for_id_n_path(15)
                    req = WebRequest.Create(New Uri(url))

                    str = req.GetResponse()

                    reader = New StreamReader(str.GetResponseStream())

                    Data = reader.ReadToEnd()

                    data_arr = url.Split("=")
                    data_arr = data_arr(7).Split("&")

                    pdfname1 = data_arr(0).ToString()



                    Dim wc1 As New WebClient()

                    If (File.Exists(ds.Tables("Pdf_location").Rows(0)("Path").ToString() + "\" + pdfname1 + ".pdf") = False) Then

                        Try
                            'wc.OpenRead(ds.Tables("Pdf_location").Rows(0)("Path").ToString() + "\" + pdfname + ".pdf")
                            'wc.Dispose()

                            wc1.DownloadFile(New Uri(url), ds.Tables("Pdf_location").Rows(0)("Path").ToString() + "\" + pdfname1 + ".pdf")
                            Dim print_pdf() As String = New String() {5}
                            print_pdf = Directory.GetFiles(ds.Tables("Pdf_location").Rows(0)("Path").ToString())
                            Dim proc As New ProcessStartInfo
                            proc.WindowStyle = ProcessWindowStyle.Hidden
                            proc.Verb = "Print"
                            proc.CreateNoWindow = False
                            proc.FileName = ds.Tables("Pdf_location").Rows(0)("Path").ToString() + "\" + pdfname1 + ".pdf"

                            proc.Arguments = String.Format("/p {0}", pdfname1)
                            proc.UseShellExecute = True

                            insert_pdf_name_n_print_time(pdfname1)
                            Process.Start(proc)


                            Dim p As PrintAction
                            Dim pname As New PrinterSettings
                            pname.PrinterName = "aaa"
                            p = PrintAction.PrintToPrinter

                        Catch ex As Exception
                            ' wc.DownloadFile(New Uri(url), ds.Tables("Pdf_location").Rows(0)("Path").ToString() + "\" + pdfname + ".pdf")
                            'Dim print_pdf() As String = New String() {5}
                            'print_pdf = Directory.GetFiles(ds.Tables("Pdf_location").Rows(0)("Path").ToString())
                            'Dim proc As New ProcessStartInfo
                            'proc.WindowStyle = ProcessWindowStyle.Hidden
                            'proc.Verb = "Print"
                            'proc.FileName = ds.Tables("Pdf_location").Rows(0)("Path").ToString() + "\" + pdfname + ".pdf"
                            'proc.Arguments = String.Format("/p /h {0}", pdfname)
                            'proc.UseShellExecute = True
                            'proc.CreateNoWindow = True
                            'Process.Start(proc)
                            'Dim p As PrintAction
                            'Dim pname As New PrinterSettings
                            'pname.PrinterName = "aaa"
                            'p = PrintAction.PrintToPrinter
                        End Try
                    Else
                    End If
                Next

                
                
            Else
                MessageBox.Show("Custome is Deactivate")
            End If


        End While



    End Sub
#End Region

#Region " Save Pdf Details and Time "


    Sub insert_pdf_name_n_print_time(ByVal pdf_name As String)
        Dim doc As New XmlDocument
        doc.Load(Application.StartupPath & "\XMLDOC.XML")
        'm_nodelist = doc.SelectNodes("/Detail/printer")
        Dim m_node As XmlNode = doc.SelectSingleNode("/Detail/Pdf_Print_Detail")

        Dim nameEl As XmlElement = doc.CreateElement("Pdf_Name")
        nameEl.InnerText = pdf_name
        m_node.AppendChild(nameEl)
        doc.DocumentElement.AppendChild(m_node)
        nameEl = doc.CreateElement("Pdf_Print_DateTime")
        nameEl.InnerText = System.DateTime.Now
        m_node.AppendChild(nameEl)
        doc.DocumentElement.AppendChild(m_node)
        doc.Save(Application.StartupPath & "\XMLDOC.XML")
    End Sub
#End Region
End Class
