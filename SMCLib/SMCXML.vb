Imports System.Xml

Public Class SMCXML
    Public Shared Function ReadXmlWithXPath(ByVal pathAddress As String, ByVal xPath As String) As XmlNodeList
        Dim root As XmlDocument = New XmlDocument()
        root.Load(AppDomain.CurrentDomain.BaseDirectory + pathAddress)
        Return root.SelectNodes(xPath)
    End Function
End Class
