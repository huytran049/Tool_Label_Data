Imports System.IO
Public Class SMCFile

    Public Shared Function FileExist(ByVal strFilePath As String) As Boolean
        Dim blnRet As Boolean
        Try
            blnRet = File.Exists(strFilePath)
        Catch ex As Exception
            blnRet = False
        End Try
        Return blnRet
    End Function

    Public Shared Function FolderExist(ByVal strFolderPath As String) As Boolean
        Dim blnRet As Boolean
        Try
            blnRet = Directory.Exists(strFolderPath)
        Catch ex As Exception
            blnRet = False
        End Try
        Return blnRet
    End Function

    Public Shared Function FileOrFolderExist(ByVal strFileOrFolderName As String) As Boolean
        If File.Exists(strFileOrFolderName) Then
            Return True
        Else
            If Directory.Exists(strFileOrFolderName) Then
                Return True
            Else
                Return False
            End If
        End If
    End Function

    Public Shared Function getPath(ByVal strSourceFile As String) As String
        Return strSourceFile.Substring(0, strSourceFile.LastIndexOf("\")) + "\"
    End Function

    Public Shared Function getFileName(ByVal strSourceFile As String) As String
        Return strSourceFile.Substring(strSourceFile.LastIndexOf("\") + 1)
    End Function

    Public Shared Function getFileExtention(ByVal strFileName As String) As String
        Return strFileName.Substring(strFileName.LastIndexOf(".") + 1)
    End Function

    Public Shared Function CopyFile(ByVal strSourceFile As String, ByVal strDestinationFile As String, Optional ByVal blnOverWrite As Boolean = True) As Boolean
        If Not FileExist(strSourceFile) Then
            Return False
        End If
        If Not FolderExist(getPath(strDestinationFile)) Then
            Return False
        End If
        Try
            File.Copy(strSourceFile, strDestinationFile, blnOverWrite)
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Public Shared Function DeleteFile(ByVal strDeleteFile As String) As Boolean
        If Not FileExist(strDeleteFile) Then
            Return False
        End If
        Try
            File.Delete(strDeleteFile)
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Public Shared Function DeleteFolder(ByVal strDeleteFolder As String, Optional ByVal blnDeleteFolderSubAndFile As Boolean = False) As Boolean
        If Not FolderExist(strDeleteFolder) Then
            Return False
        End If
        Try
            Directory.Delete(strDeleteFolder, blnDeleteFolderSubAndFile)
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Public Shared Function CreateFolder(ByVal strCreateFolder As String) As Boolean
        If Not FolderExist(strCreateFolder) Then
            'Return False
            Try
                Directory.CreateDirectory(strCreateFolder)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End If
        Return True
    End Function

    Public Shared Function Base64ToFile(ByVal base64string As String, ByVal destFile As String) As Boolean
        Base64ToFile = False
        Try

            'File.WriteAllBytes(strTemp, Convert.FromBase64String(base64string))
            Dim bt64 As Byte() = System.Convert.FromBase64String(base64string)

            If IO.File.Exists(destFile) Then
                IO.File.Delete(destFile)
            End If

            Dim sw As New IO.FileStream(destFile, IO.FileMode.Create)

            sw.Write(bt64, 0, bt64.Length)

            sw.Close()
            Base64ToFile = True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
