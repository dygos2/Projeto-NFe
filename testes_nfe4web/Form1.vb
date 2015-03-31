Imports System.Text.RegularExpressions
Imports System.Xml

Public Class Form1


    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        'Carregar o xml transformado
        Dim pathXmlTransformado As String = "C:\Fisconet4\process\11790941000192\2015\3\28\38387-1\38387_transformado.xml"
        Dim nfeXML As New XmlDocument

        nfeXML.Load(pathXmlTransformado)

        nfeXML.LoadXml(nfeXML.OuterXml.Replace(nfeXML.DocumentElement.NamespaceURI, ""))
        nfeXML.DocumentElement.RemoveAllAttributes()

        Dim dhCont As XmlNode = nfeXML.CreateElement("dhCont")
        Dim xJust As XmlNode = nfeXML.CreateElement("xJust")

        Dim time As DateTime = DateTime.Now
        Dim format As String = "yyyy-MM-ddTHH:mm:ss"

        dhCont.InnerXml = time.ToString(format)
        xJust.InnerXml = "Sistema de contingencia ativado devido a erro no envio pelo sistema normal"

        nfeXML.GetElementsByTagName("ide")(0).AppendChild(dhCont)
        nfeXML.GetElementsByTagName("ide")(0).AppendChild(xJust)

        nfeXML.DocumentElement.SetAttribute("xmlns", "http://www.portalfiscal.inf.br/nfe")
        nfeXML.Save("C:\Fisconet4\process\11790941000192\2015\3\28\38387-1\teste2.xml")

    End Sub
End Class
