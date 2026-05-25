Imports System.Net.Mail
Imports Microsoft.VisualBasic

Public Class SMCEmail
    Private m_SMTPServer As String
    Private m_EmailPass As String

    Private m_FromAddress As String
    Private m_ToAddress As String

    Private m_Subject As String
    Private m_Body As String
    Private m_FileList As String
    ' Private m_FileList() As String

    Public Property EmailPass() As String
        Get
            Return m_EmailPass
        End Get
        Set(ByVal value As String)
            m_EmailPass = value
        End Set
    End Property

    Public Property Subject() As String
        Get
            Return m_Subject
        End Get
        Set(ByVal value As String)
            m_Subject = value
        End Set
    End Property

    Public Property Body() As String
        Get
            Return m_Body
        End Get
        Set(ByVal value As String)
            m_Body = value
        End Set
    End Property

    Public Property FromAddress() As String
        Get
            Return m_FromAddress
        End Get
        Set(ByVal value As String)
            m_FromAddress = value
        End Set
    End Property

    Public Property ToAddress() As String
        Get
            Return m_ToAddress
        End Get
        Set(ByVal value As String)
            m_ToAddress = value
        End Set
    End Property

    Public Property FileList() As String
        Get
            Return m_FileList
        End Get
        Set(ByVal value As String)
            m_FileList = value
        End Set
    End Property

    Public Property SMTPServer() As String
        Get
            Return m_SMTPServer
        End Get
        Set(ByVal value As String)
            m_SMTPServer = value
        End Set
    End Property

    Public Sub Send()
        'This procedure takes string array parameters for multiple recipients and files
        Try
            Dim m_ToAddressList() As String = m_ToAddress.Split(";")
            For Each item As String In m_ToAddressList
                'For each to address create a mail message
                Dim MailMsg As New MailMessage(New MailAddress(m_FromAddress.Trim()), New MailAddress(item))
                MailMsg.BodyEncoding = System.Text.Encoding.UTF8
                MailMsg.Subject = m_Subject.Trim()
                MailMsg.Body = m_Body.Trim() & vbCrLf
                ' MailMsg.Priority = MailPriority.High
                MailMsg.IsBodyHtml = False

                ''attach each file attachment
                'For Each strFile As String In m_FileList
                '    If Not strFile = "" Then
                'Dim MsgAttach As New Attachment(m_FileList)
                'MailMsg.Attachments.Add(MsgAttach)
                '        End If
                'Next

                'Smtpclient to send the mail message
                Dim SmtpMail As New SmtpClient

                SmtpMail.Host = m_SMTPServer
                SmtpMail.Credentials = New System.Net.NetworkCredential(m_FromAddress, m_EmailPass)
                ' SmtpMail.Credentials = System.Net.Mail.SmtpPermissionAttribute.
                SmtpMail.Send(MailMsg)
            Next
            'Message Successful
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class

