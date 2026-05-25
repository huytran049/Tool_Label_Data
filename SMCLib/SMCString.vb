'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'+
'+ Dự án      : Thư viện chung
'+ Lớp          : SMCString 
'+ Giải thích  : Các hàm chung về xử lý xâu, chuỗi, ký tự
'+
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'+ 2009/09/09   SaoMaiSoft/TUAN  Tạo mới
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Option Explicit On
Option Strict On

Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
'Imports System.Windows.Forms
Imports System.Xml

Public Class SMCString

#Region "Enum"

    '''<summary>Giá trị trả về</summary>
    Public Enum genmIsResult As Integer
        '''<summary>Bình thường</summary>
        OK = 0
        '''<summary>Không có giá trị</summary>
        Empty = 1
        '''<summary>Giá trị không có hiệu lực</summary>
        InValid = 2
    End Enum

#End Region

#Region "Các Hàm xử lý chuỗi, xâu ký tự"
    'DBNullToString
    Public Shared Function DBNullToString(ByVal strString As Object) As String

        Dim str As String = ""

        Try
            If (strString Is Nothing) Then
                Return ""
            Else
                str = strString.ToString
            End If
        Catch ex As Exception
        End Try
        Return str
    End Function
#Region "Lấy độ dài của chuỗi (Byte)"

    '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    '+
    '+ Function     : Lấy độ dài của chuỗi (Byte)
    '+
    '+ Argument     : [i/ ]strString (String) Chuỗi ký tự
    '+
    '+ Return value : (Integer) Độ dài xâu ký tự (Số byte)
    '+
    '+ Remarks      : Chuyển đổi sang Unicode (UTF-8) và trả về số byte của chuỗi ký tự
    '+
    '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    ''' <summary>
    ''' Lấy độ dài của chuỗi (Byte)
    ''' </summary>
    ''' <param name="strString">Đối tượng là xâu ký tự</param>
    ''' <returns>Độ dài xâu ký tự (Số byte)</returns>
    ''' <remarks>Chuyển đổi sang Unicode (UTF-8) và trả về số byte của chuỗi ký tự</remarks>
    Public Shared Function LenB(ByVal strString As String) As Integer

        Dim intRet As Integer = 0

        Try
            'Chuyển đổi sang Unicode (UTF-8) và trả về số byte của chuỗi ký tự
            intRet = Encoding.GetEncoding(65001).GetByteCount(strString)

        Catch ex As Exception
        End Try

        Return intRet

    End Function
#End Region

#Region "Cắt chuỗi (Byte)"

    ''' <summary>
    ''' Cắt chuỗi từ bên trái theo số byte
    ''' </summary>
    ''' <param name="strString">Đối tượng chuỗi ký tự</param>
    ''' <param name="intLen">Số ký tự sẽ lấy (từ 1 Byte)</param>
    ''' <returns>Chuỗi ký tự</returns>
    ''' <remarks></remarks>

    Public Shared Function CutStr(ByVal strString As String, ByVal intLen As Integer) As String

        Dim strResult As String = ""

        Try
            If intLen > strString.Length Then
                strResult = strString
            Else
                Dim intIndex As Integer = 0
                Dim intCountSpace As Integer = 0

                intIndex = strString.IndexOf(" ", intIndex)
                Do Until intIndex <= 0
                    intCountSpace += 1
                    If intCountSpace >= intLen Then
                        strResult = strString.Substring(0, intIndex) + "..."
                        Exit Do
                    End If
                    intIndex = strString.IndexOf(" ", intIndex + 1)
                Loop
                If (intCountSpace < intLen) Then
                    strResult = strString
                End If
            End If
        Catch
            strResult = ""
        End Try

        Return strResult
    End Function

    Public Shared Function CutLeft(ByVal strString As String, ByVal intLen As Integer) As String
        Dim strResult As String = String.Empty
        Try

            If strString Is Nothing OrElse strString.Equals(String.Empty) Then Exit Try

            strResult = strString


            If intLen > (strString.Length + 3) Then
                strResult = strString
            Else
                strResult = MidB(strString, 1, intLen)
                strResult += "..."
            End If

        Catch
            strResult = String.Empty
        End Try

        Return strResult

    End Function

    '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    '+
    '+ Function     : Cắt chuỗi từ bên trái (Byte)
    '+
    '+ Argument     : [i/ ]strString (String)  Chuỗi ký tự
    '+　              [i/ ]intLen    (Integer) Số ký tự sẽ lấy (Byte)
    '+
    '+ Return value : (String) Chuỗi ký tự
    '+
    '+ Remarks      :
    '+
    '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    ''' <summary>
    ''' Cắt chuỗi từ bên trái theo số byte
    ''' </summary>
    ''' <param name="strString">Đối tượng chuỗi ký tự</param>
    ''' <param name="intLen">Số ký tự sẽ lấy (từ 1 Byte)</param>
    ''' <returns>Chuỗi ký tự</returns>
    ''' <remarks></remarks>

    Public Shared Function LeftB(ByVal strString As String, ByVal intLen As Integer) As String

        Dim strResult As String = String.Empty

        Try

            'Kết thúc xử lý khi đối tượng chuỗi ký tự là rỗng

            If strString Is Nothing OrElse strString.Equals(String.Empty) Then Exit Try

            strResult = strString

            If intLen = 0 Then
                strResult = String.Empty
                Exit Try
            End If

            strResult = MidB(strString, 1, intLen)

        Catch
            strResult = String.Empty
        End Try

        Return strResult

    End Function

    '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    '+
    '+ Function     : Cắt chuỗi từ bên phải (Byte)
    '+
    '+ Argument     : [i/ ]strString (String)  Chuỗi ký tự
    '+　              [i/ ]intLen    (Integer) Số ký tự sẽ lấy (Byte)
    '+
    '+ Return value : (String) Chuỗi ký tự
    '+
    '+ Remarks      :
    '+
    '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    ''' <summary>
    ''' Cắt chuỗi từ bên phải (Byte)
    ''' </summary>
    ''' <param name="strString">Đối tượng chuỗi ký tự</param>
    ''' <param name="intLen">Số ký tự sẽ lấy (từ 1 Byte)</param>
    ''' <returns>Chuỗi ký tự</returns>
    ''' <remarks></remarks>

    Public Shared Function RightB(ByVal strString As String, ByVal intLen As Integer) As String

        Dim strResult As String = String.Empty
        Dim intStart As Integer = 0

        Try

            'Kết thúc xử lý khi đối tượng chuỗi ký tự là rỗng

            If strString Is Nothing OrElse strString.Trim.Equals(String.Empty) Then Exit Try

            strResult = strString

            If intLen = 0 Then
                strResult = String.Empty
                Exit Try
            End If

            Dim encUNICODE As Encoding = Encoding.GetEncoding(65001)
            Dim bytWork() As Byte = encUNICODE.GetBytes(strString)

            If (bytWork.Length > intLen) Then
                intStart = bytWork.Length - intLen + 1
            Else
                intStart = 1
            End If

            strResult = MidB(strString, intStart, intLen)

        Catch
            strResult = String.Empty
        End Try

        Return strResult

    End Function

    '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    '+
    '+ Function     : Cắt chuỗi theo vị trí (Byte)
    '+
    '+ Argument     : [i/ ]strString (String)  Chuỗi ký tự
    '+　              [i/ ]intStart  (Integer) Vị trí bắt đầu (Byte)
    '+　              [i/ ]intLen    (Integer) Số ký tự sẽ lấy (Byte)
    '+
    '+ Return value : (String) Chuỗi ký tự
    '+
    '+ Remarks      :
    '+
    '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    ''' <summary>
    ''' Cắt chuỗi theo vị trí (Byte)
    ''' </summary>
    ''' <param name="strString">Đối tượng chuỗi ký tự</param>
    ''' <param name="intStart">Vị trí bắt đầu (Byte 1...)</param>
    ''' <param name="intLen">Số ký tự sẽ lấy (Byte)</param>
    ''' <returns>Chuỗi ký tự</returns>
    ''' <remarks></remarks>
    Public Shared Function MidB(ByVal strString As String, ByVal intStart As Integer, Optional ByVal intLen As Integer = 0) As String

        Dim strResult As String = String.Empty

        Try

            'Kết thúc xử lý khi đối tượng chuỗi ký tự là rỗng

            If strString Is Nothing OrElse strString.Trim.Equals(String.Empty) Then Exit Try

            Dim encUNICODE As Encoding = Encoding.GetEncoding(65001)
            Dim bytWork() As Byte = encUNICODE.GetBytes(strString)

            strResult = strString

            If (bytWork.Length < intStart) Then
                strResult = String.Empty
                Exit Try
            End If

            If intLen > 0 AndAlso ((bytWork.Length) > (intStart - 1 + intLen)) Then
                strResult = encUNICODE.GetString(bytWork, intStart - 1, intLen)
            Else
                strResult = encUNICODE.GetString(bytWork, intStart - 1, bytWork.Length - intStart + 1)
            End If

        Catch
            strResult = String.Empty
        End Try

        Return strResult

    End Function

#End Region

#Region "Soạn thảo chuỗi ký tự"

    '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    '+
    '+ Function     : Thay thế chuỗi
    '+
    '+ Argument     : [i/ ]strValue (String) Chuỗi ký tự
    '+　              [i/ ]strOld   (String) Chuỗi sẽ thay thế (Chuỗi cũ)
    '+　              [i/ ]strNew   (String) Chuỗi thay thế (Chuỗi mới)
    '+
    '+ Return value : (String) Chuỗi ký tự sau khi đã thay thế
    '+
    '+ Remarks      : Thay thế một chuỗi bằng một chuỗi mới
    '+
    '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    ''' <summary>
    ''' Thay thế chuỗi
    ''' </summary>
    ''' <param name="strValue">Chuỗi ký tự</param>
    ''' <param name="strOld">Chuỗi sẽ thay thế (Chuỗi cũ)</param>
    ''' <param name="strNew">Chuỗi thay thế (Chuỗi mới)</param>
    ''' <returns>Chuỗi ký tự sau khi đã thay thế</returns>
    ''' <remarks>Thay thế một chuỗi bằng một chuỗi mới</remarks>
    Public Shared Function Replace(ByVal strValue As String, ByVal strOld As String, ByVal strNew As String) As String

        Replace = String.Empty

        If (strValue Is Nothing OrElse strValue.Equals(String.Empty)) Then Exit Function

        Replace = strValue.Replace(strOld, strNew)

    End Function

#End Region

#Region "Soạn thảo chuỗi SQL"

    '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    '+
    '+ Function     : Chuẩn hóa chuỗi SQL
    '+
    '+ Argument     : [i/ ]strValue (String) Chuỗi ký tự sẽ được chuẩn hóa
    '+
    '+ Return value : (String) Chuỗi ký tự (SQL) sau khi chuẩn hóa
    '+
    '+ Remarks      : 
    '+
    '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    ''' <summary>
    ''' Chuẩn hóa chuỗi SQL
    ''' </summary>
    ''' <param name="strValue">Chuỗi ký tự sẽ được chuẩn hóa</param>
    ''' <returns>Chuỗi ký tự sau khi chuẩn hóa</returns>
    ''' <remarks></remarks>
    Public Shared Function SQLStr(ByVal strValue As String) As String
        Dim intIndex As Integer
        Dim strBuf As String
        If (strValue Is Nothing) Then
            strBuf = ""
        Else
            strBuf = strValue
            If (strBuf.Equals("")) Then
                strBuf = ""
            Else
                intIndex = strBuf.IndexOf("'")
                Do
                    If intIndex = -1 Then
                        Exit Do
                    End If
                    strBuf = String.Concat(strBuf.Substring(0, intIndex), "''", strBuf.Substring(intIndex + 1))
                    intIndex = strBuf.IndexOf("'", intIndex + 2)
                Loop
            End If
        End If
        SQLStr = String.Concat("'", strBuf, "'")
    End Function

    Public Shared Function SQLVal(ByVal strValue As String) As String
        Dim intIndex As Integer
        Dim strBuf As String
        If (strValue Is Nothing) Then
            strBuf = ""
        Else
            strBuf = strValue
            If (strBuf.Equals("")) Then
                strBuf = ""
            Else
                intIndex = strBuf.IndexOf("'")
                Do
                    If intIndex = -1 Then
                        Exit Do
                    End If
                    strBuf = String.Concat(strBuf.Substring(0, intIndex), "''", strBuf.Substring(intIndex + 1))
                    intIndex = strBuf.IndexOf("'", intIndex + 2)
                Loop
            End If
        End If
        SQLVal = strBuf
    End Function

    '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    '+
    '+ Function     : Chuẩn hóa chuỗi SQL
    '+
    '+ Argument     : [i/ ]strValue (Integer) Chuỗi ký tự sẽ được chuẩn hóa
    '+
    '+ Return value : (String) Chuỗi ký tự (SQL) sau khi chuẩn hóa
    '+
    '+ Remarks      : 
    '+
    '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    ''' <summary>
    ''' Chuẩn hóa chuỗi SQL
    ''' </summary>
    ''' <param name="intValue">Chuỗi ký tự sẽ được chuẩn hóa</param>
    ''' <returns>Chuỗi ký tự sau khi chuẩn hóa</returns>
    ''' <remarks></remarks>
    Public Shared Function SQLStr(ByVal intValue As Integer) As String
        SQLStr = String.Concat("'", intValue.ToString, "'")
    End Function

#End Region

#Region "Chuyển đổi kiểu dữ liệu"

    '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    '+
    '+ Function     : Chuyển đổi thành kiểu chuỗi ký tự
    '+
    '+ Argument     : [i/ ]objData       (Object) Đối tượng dữ liệu sẽ được chuyển đổi thành kiểu chuỗi ký tự
    '+　              [i/ ]strErrValue   (String) Giá trị trả về khi không thể chuyển đổi
    '+
    '+ Return value : (String) Chuỗi ký tự sau khi đã chuyển đổi
    '+
    '+ Remarks      : Đối tượng có thể chuyển đổi Object,Integer, ...
    '+
    '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    ''' <summary>
    ''' Chuyển đổi thành kiểu chuỗi ký tự
    ''' </summary>
    ''' <param name="objData">Đối tượng dữ liệu sẽ được chuyển đổi thành kiểu chuỗi ký tự(Object(DataRow),Integer ...)</param>
    ''' <param name="strErrValue">Giá trị trả về khi không thể chuyển đổi:""]</param>
    ''' <returns>Chuỗi ký tự sau khi đã chuyển đổi</returns>
    ''' <remarks></remarks>

    Public Shared Function ConvStr(ByVal objData As Object, Optional ByVal strErrValue As String = "") As String

        Dim strResult As String = String.Empty

        Try

            strResult = strErrValue

            If (objData Is Nothing) OrElse objData.ToString = String.Empty Then Exit Try

            strResult = objData.ToString

        Catch ex As Exception
            strResult = strErrValue
        End Try

        Return strResult

    End Function

    '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    '+
    '+ Function     : Chuyển đổi thành kiểu ký tự (Char)
    '+
    '+ Argument     : [i/ ]objData (Object) Đối tượng dữ liệu sẽ được chuyển đổi thành kiểu ký tự
    '+
    '+ Return value : (String) Chuỗi ký tự sau khi đã chuyển đổi
    '+
    '+ Remarks      : Đối tượng có thể chuyển đổi Object,Integer, ...
    '+
    '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    ''' <summary>
    ''' Chuyển đổi thành kiểu chuỗi ký tự
    ''' </summary>
    ''' <param name="objData">Đối tượng dữ liệu sẽ được chuyển đổi thành kiểu ký tự(Object(DataRow),Integer ...)</param>
    ''' <returns>Ký tự sau khi đã chuyển đổi</returns>
    ''' <remarks></remarks>
    Public Shared Function ConvCha(ByVal objData As Object) As Char

        Dim chaResult As Char = New Char

        Try

            chaResult = Char.Parse(String.Empty)

            If (objData Is Nothing) Then Exit Try

            If Char.TryParse(objData.ToString, chaResult) = False Then
                chaResult = Char.Parse(String.Empty)
                Exit Try
            End If

        Catch ex As Exception
            chaResult = Char.Parse(String.Empty)
        End Try

        Return chaResult

    End Function
#End Region

#Region "Kiểm tra chuỗi"

    '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    '+
    '+ Function     : Kiểm tra chuỗi rỗng
    '+
    '+ Argument     : [i/ ]strValue (String) Chuỗi ký tự
    '+
    '+ Return value : (genmIsResult) Kết qủa
    '+                               OK        Bình thường
    '+                               Empty   Không có giá trị

    '+                               InValid  Giá trị không có hiệu lực
    '+
    '+ Remarks      :
    '+
    '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    ''' <summary>
    ''' Kiểm tra chuỗi rỗng
    ''' </summary>
    ''' <param name="strValue">Chuỗi ký tự</param>
    ''' <returns>Kết qủa[OK:Bình thường, Empty:Không có giá trị, InValid:Giá trị không có hiệu lực]</returns>
    ''' <remarks></remarks>
    Public Shared Function IsEmpty(ByVal strValue As String) As genmIsResult

        Dim enmResult As genmIsResult

        Try
            enmResult = genmIsResult.Empty

            If strValue Is Nothing OrElse strValue.Equals(String.Empty) Then Exit Try

        Catch ex As Exception
            enmResult = genmIsResult.InValid
        End Try

        Return enmResult

    End Function


    ''' <summary>
    ''' Kiểm tra chuỗi rỗng
    ''' </summary>
    ''' <param name="strValue">Chuỗi ký tự</param>
    ''' <returns>Kết qủa[OK:Bình thường, Empty:Không có giá trị, InValid:Giá trị không có hiệu lực]</returns>
    ''' <remarks></remarks>
    Public Shared Function IsNullOrEmpty(ByVal strValue As String) As Boolean

        Dim flg As Boolean = False

        Try
            If String.IsNullOrEmpty(strValue) Then
                flg = True
            End If

            If strValue = "" Then
                flg = True
            End If

        Catch ex As Exception

        End Try

        Return flg

    End Function


    '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    '+
    '+ Function     : Kiểm tra chuỗi chỉ có số và ký tự (a-z A-Z 0-9)
    '+
    '+ Argument     : [i/ ]strAlNu (String) Chuỗi ký tự
    '+
    '+ Return value : (genmIsResult) Kết qủa
    '+                               OK        Bình thường
    '+                               Empty   Không có giá trị

    '+                               InValid  Giá trị không có hiệu lực
    '+
    '+ Remarks      : Chuỗi có ký tự ngoài số và chuỗi là không hợp lệ

    '+
    '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    ''' <summary>
    ''' Kiểm tra chuỗi chỉ có số và ký tự (a-z A-Z 0-9)
    ''' </summary>
    ''' <param name="strAlNu">Chuỗi ký tự</param>
    ''' <returns>Kết qủa[OK:Bình thường, Empty:Không có giá trị, InValid:Giá trị không có hiệu lực]</returns>
    ''' <remarks>Chuỗi có ký tự ngoài số 0-9 và chuỗi a-zA-Z  là không hợp lệ</remarks>

    Public Shared Function IsAluNum(ByVal strAlNu As String) As genmIsResult

        Dim enmResult As genmIsResult

        Try

            enmResult = genmIsResult.Empty

            If strAlNu Is Nothing OrElse strAlNu.Equals(String.Empty) Then Exit Try

            enmResult = genmIsResult.InValid

            'Kiểm tra chuỗi ký tự không phải là số 0-9 hoặc ký tự a-z, A-Z
            If Regex.IsMatch(strAlNu, "^[a-zA-Z0-9]+$") = False Then Exit Try

            enmResult = genmIsResult.OK

        Catch ex As Exception
            enmResult = genmIsResult.InValid
        End Try

        Return enmResult

    End Function

    '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    '+
    '+ Function     : Kiểm tra chuỗi ký tự một byte
    '+
    '+ Argument     : [i/ ]strValue (String) Chuỗi ký tự
    '+
    '+ Return value : (genmIsResult) Kết qủa
    '+                               OK        Bình thường
    '+                               Empty   Không có giá trị

    '+                               InValid  Giá trị không có hiệu lực
    '+
    '+ Remarks      : Chuỗi có ký tự 2 byte là không hợp lệ

    '+
    '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    ''' <summary>
    ''' Kiểm tra chuỗi ký tự một byte
    ''' </summary>
    ''' <param name="strValue">Chuỗi ký tự</param>
    ''' <returns>Kết qủa[OK:Bình thường, Empty:Không có giá trị, InValid:Giá trị không có hiệu lực]</returns>
    ''' <remarks></remarks>
    Public Shared Function IsOneByte(ByVal strValue As String) As genmIsResult

        Dim enmResult As genmIsResult
        Dim intLength As Integer
        Dim intByteLength As Integer
        Dim strText As String

        Try

            enmResult = genmIsResult.Empty

            If strValue Is Nothing OrElse strValue.Equals(String.Empty) Then Exit Try

            enmResult = genmIsResult.InValid

            'Xóa dòng mới trong chuỗi
            strText = strValue.Replace(Microsoft.VisualBasic.ControlChars.NewLine, "")

            'Số ký tự
            intLength = strText.Length
            'Số byte
            intByteLength = LenB(strText)

            'Trường hợp độ dài 2 chuỗi không giống nhau、Lỗi
            If (intLength = intByteLength) = False Then Exit Try

            enmResult = genmIsResult.OK

        Catch ex As Exception
            enmResult = genmIsResult.InValid
        End Try

        Return enmResult

    End Function


#End Region

#Region "Hàm chuyển xâu ký tự từ tiếng việt sang tiếng anh"
    '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    '+
    '+ Function     : Chuyển đổi xâu tiếng việt sang tiếng anh
    '+
    '+ Argument     : [i/ ]strSource (String) Chuỗi ký tự tiếng việt
    '+                [i/ ]iContentsType (Integer) Chuỗi ký tự là một văn bản hay chỉ là một câu 
    '+ Return value : (String) Chuỗi ký tự sau khi đã chuyển đổi
    '+
    '+ Remarks      : Thường xử dụng cho module upload, vì một số trình duyệt không thể upload khi file
    '+                được chọn có tên file là tiếng việt  
    '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    ''' <summary>
    ''' Chuyển đổi xâu tiếng việt sang tiếng anh
    ''' </summary>
    ''' <param name="strSource">Chuỗi ký tự tiếng việt</param>
    ''' <param name="iContentsType">Chuỗi ký tự là một văn bản hay chỉ là một câu</param>
    ''' <returns>Chuỗi ký tự tiếng anh sau khi chuyển đổi</returns>
    ''' <remarks></remarks>
    Public Shared Function VietnamToEnglish(ByVal strSource As String, Optional ByVal iContentsType As Integer = 0) As String
        'Nếu nội dung là văn bản
        If (iContentsType = 1) Then
            strSource = strSource.Replace(".", " .")
            strSource = strSource.Replace("?", " ?")
        End If
        '---------------------------------a^
        strSource = strSource.Replace("ấ", "a")
        strSource = strSource.Replace("ầ", "a")
        strSource = strSource.Replace("ẩ", "a")
        strSource = strSource.Replace("ẫ", "a")
        strSource = strSource.Replace("ậ", "a")
        '---------------------------------A^
        strSource = strSource.Replace("Ấ", "A")
        strSource = strSource.Replace("Ầ", "A")
        strSource = strSource.Replace("Ẩ", "A")
        strSource = strSource.Replace("Ẫ", "A")
        strSource = strSource.Replace("Ậ", "A")
        '---------------------------------a(
        strSource = strSource.Replace("ắ", "a")
        strSource = strSource.Replace("ằ", "a")
        strSource = strSource.Replace("ẳ", "a")
        strSource = strSource.Replace("ẵ", "a")
        strSource = strSource.Replace("ặ", "a")
        '---------------------------------A(
        strSource = strSource.Replace("Ắ", "A")
        strSource = strSource.Replace("Ằ", "A")
        strSource = strSource.Replace("Ẳ", "A")
        strSource = strSource.Replace("Ẵ", "A")
        strSource = strSource.Replace("Ặ", "A")
        '---------------------------------a
        strSource = strSource.Replace("á", "a")
        strSource = strSource.Replace("à", "a")
        strSource = strSource.Replace("ả", "a")
        strSource = strSource.Replace("ã", "a")
        strSource = strSource.Replace("ạ", "a")
        strSource = strSource.Replace("â", "a")
        strSource = strSource.Replace("ă", "a")
        '---------------------------------A
        strSource = strSource.Replace("Á", "A")
        strSource = strSource.Replace("À", "A")
        strSource = strSource.Replace("Ả", "A")
        strSource = strSource.Replace("Ã", "A")
        strSource = strSource.Replace("Ạ", "A")
        strSource = strSource.Replace("Â", "A")
        strSource = strSource.Replace("Ă", "A")
        '---------------------------------e^
        strSource = strSource.Replace("ế", "e")
        strSource = strSource.Replace("ề", "e")
        strSource = strSource.Replace("ể", "e")
        strSource = strSource.Replace("ễ", "e")
        strSource = strSource.Replace("ệ", "e")
        '---------------------------------E^
        strSource = strSource.Replace("Ế", "E")
        strSource = strSource.Replace("Ề", "E")
        strSource = strSource.Replace("Ể", "E")
        strSource = strSource.Replace("Ễ", "E")
        strSource = strSource.Replace("Ệ", "E")
        '---------------------------------e
        strSource = strSource.Replace("é", "e")
        strSource = strSource.Replace("è", "e")
        strSource = strSource.Replace("ẻ", "e")
        strSource = strSource.Replace("ẽ", "e")
        strSource = strSource.Replace("ẹ", "e")
        strSource = strSource.Replace("ê", "e")
        '---------------------------------E
        strSource = strSource.Replace("É", "E")
        strSource = strSource.Replace("È", "E")
        strSource = strSource.Replace("Ẻ", "E")
        strSource = strSource.Replace("Ẽ", "E")
        strSource = strSource.Replace("Ẹ", "E")
        strSource = strSource.Replace("Ê", "E")
        '---------------------------------i
        strSource = strSource.Replace("í", "i")
        strSource = strSource.Replace("ì", "i")
        strSource = strSource.Replace("ỉ", "i")
        strSource = strSource.Replace("ĩ", "i")
        strSource = strSource.Replace("ị", "i")
        '---------------------------------I
        strSource = strSource.Replace("Í", "I")
        strSource = strSource.Replace("Ì", "I")
        strSource = strSource.Replace("Ỉ", "I")
        strSource = strSource.Replace("Ĩ", "I")
        strSource = strSource.Replace("Ị", "I")
        '---------------------------------o^
        strSource = strSource.Replace("ố", "o")
        strSource = strSource.Replace("ồ", "o")
        strSource = strSource.Replace("ổ", "o")
        strSource = strSource.Replace("ỗ", "o")
        strSource = strSource.Replace("ộ", "o")
        '---------------------------------O^
        strSource = strSource.Replace("Ố", "O")
        strSource = strSource.Replace("Ồ", "O")
        strSource = strSource.Replace("Ổ", "O")
        strSource = strSource.Replace("Ô", "O")
        strSource = strSource.Replace("Ộ", "O")
        '---------------------------------o*
        strSource = strSource.Replace("ớ", "o")
        strSource = strSource.Replace("ờ", "o")
        strSource = strSource.Replace("ở", "o")
        strSource = strSource.Replace("ỡ", "o")
        strSource = strSource.Replace("ợ", "o")
        '---------------------------------O*
        strSource = strSource.Replace("Ớ", "O")
        strSource = strSource.Replace("Ờ", "O")
        strSource = strSource.Replace("Ở", "O")
        strSource = strSource.Replace("Ỡ", "O")
        strSource = strSource.Replace("Ợ", "O")
        '---------------------------------u*
        strSource = strSource.Replace("ứ", "u")
        strSource = strSource.Replace("ừ", "u")
        strSource = strSource.Replace("ử", "u")
        strSource = strSource.Replace("ữ", "u")
        strSource = strSource.Replace("ự", "u")
        '---------------------------------U*
        strSource = strSource.Replace("Ứ", "U")
        strSource = strSource.Replace("Ừ", "U")
        strSource = strSource.Replace("Ử", "U")
        strSource = strSource.Replace("Ữ", "U")
        strSource = strSource.Replace("Ự", "U")
        '---------------------------------y
        strSource = strSource.Replace("ý", "y")
        strSource = strSource.Replace("ỳ", "y")
        strSource = strSource.Replace("ỷ", "y")
        strSource = strSource.Replace("ỹ", "y")
        strSource = strSource.Replace("ỵ", "y")
        '---------------------------------Y
        strSource = strSource.Replace("Ý", "Y")
        strSource = strSource.Replace("Ỳ", "Y")
        strSource = strSource.Replace("Ỷ", "Y")
        strSource = strSource.Replace("Ỹ", "Y")
        strSource = strSource.Replace("Ỵ", "Y")
        '---------------------------------DD
        strSource = strSource.Replace("Đ", "D")
        strSource = strSource.Replace("Đ", "D")
        strSource = strSource.Replace("đ", "d")
        '---------------------------------o
        strSource = strSource.Replace("ó", "o")
        strSource = strSource.Replace("ò", "o")
        strSource = strSource.Replace("ỏ", "o")
        strSource = strSource.Replace("õ", "o")
        strSource = strSource.Replace("ọ", "o")
        strSource = strSource.Replace("ô", "o")
        strSource = strSource.Replace("ơ", "o")
        '---------------------------------O
        strSource = strSource.Replace("Ó", "O")
        strSource = strSource.Replace("Ò", "O")
        strSource = strSource.Replace("Ỏ", "O")
        strSource = strSource.Replace("Õ", "O")
        strSource = strSource.Replace("Ọ", "O")
        strSource = strSource.Replace("Ô", "O")
        strSource = strSource.Replace("Ơ", "O")
        '---------------------------------u
        strSource = strSource.Replace("ú", "u")
        strSource = strSource.Replace("ù", "u")
        strSource = strSource.Replace("ủ", "u")
        strSource = strSource.Replace("ũ", "u")
        strSource = strSource.Replace("ụ", "u")
        strSource = strSource.Replace("ư", "u")
        '---------------------------------U
        strSource = strSource.Replace("Ú", "U")
        strSource = strSource.Replace("Ù", "U")
        strSource = strSource.Replace("Ủ", "U")
        strSource = strSource.Replace("Ũ", "U")
        strSource = strSource.Replace("Ụ", "U")
        strSource = strSource.Replace("Ư", "U")
        '---------------------------------

        Return strSource
    End Function
#End Region

#Region "Unicode to TCVN3 va nguoc lai"

    Public Shared Function ContainsUnicode(ByVal inputstr As String) As Boolean
        Dim inputCharArray() As Char = inputstr.ToCharArray

        For i As Integer = 0 To inputCharArray.Length - 1
            If CInt(AscW(inputCharArray(i))) > 255 Then Return True
        Next
        Return False
    End Function

    Public Shared Function TCVN3ToUnicode(ByVal value As String) As String
        If ContainsUnicode(value) Then Return value

        Dim tcvnchars As Char() = { _
 "µ"c, "¸"c, "¶"c, "·"c, "¹"c, "¨"c, _
 "»"c, "¾"c, "¼"c, "½"c, "Æ"c, "©"c, _
 "Ç"c, "Ê"c, "È"c, "É"c, "Ë"c, "®"c, _
 "Ì"c, "Ð"c, "Î"c, "Ï"c, "Ñ"c, "ª"c, _
 "Ò"c, "Õ"c, "Ó"c, "Ô"c, "Ö"c, "×"c, _
 "Ý"c, "Ø"c, "Ü"c, "Þ"c, "ß"c, "ã"c, _
 "á"c, "â"c, "ä"c, "«"c, "å"c, "è"c, _
 "æ"c, "ç"c, "é"c, "¬"c, "ê"c, "í"c, _
 "ë"c, "ì"c, "î"c, "ï"c, "ó"c, "ñ"c, _
 "ò"c, "ô"c, "­"c, "õ"c, "ø"c, "ö"c, _
 "÷"c, "ù"c, "ú"c, "ý"c, "û"c, "ü"c, _
 "þ"c, "¡"c, "¢"c, "§"c, "£"c, "¤"c, _
 "¥"c, "¦"c}

        Dim unichars As Char() = { _
    "à"c, "á"c, "ả"c, "ã"c, "ạ"c, "ă"c, _
     "ằ"c, "ắ"c, "ẳ"c, "ẵ"c, "ặ"c, "â"c, _
     "ầ"c, "ấ"c, "ẩ"c, "ẫ"c, "ậ"c, "đ"c, _
     "è"c, "é"c, "ẻ"c, "ẽ"c, "ẹ"c, "ê"c, _
     "ề"c, "ế"c, "ể"c, "ễ"c, "ệ"c, "ì"c, _
     "í"c, "ỉ"c, "ĩ"c, "ị"c, "ò"c, "ó"c, _
     "ỏ"c, "õ"c, "ọ"c, "ô"c, "ồ"c, "ố"c, _
     "ổ"c, "ỗ"c, "ộ"c, "ơ"c, "ờ"c, "ớ"c, _
     "ở"c, "ỡ"c, "ợ"c, "ù"c, "ú"c, "ủ"c, _
     "ũ"c, "ụ"c, "ư"c, "ừ"c, "ứ"c, "ử"c, _
     "ữ"c, "ự"c, "ỳ"c, "ý"c, "ỷ"c, "ỹ"c, _
     "ỵ"c, "Ă"c, "Â"c, "Đ"c, "Ê"c, "Ô"c, _
     "Ơ"c, "Ư"c}

        Dim convertTable As Char()

        convertTable = New Char(255) {}
        For i As Integer = 0 To 255
            convertTable(i) = ChrW(i)
        Next
        For i As Integer = 0 To tcvnchars.Length - 1
            convertTable(Convert.ToInt32(tcvnchars(i))) = unichars(i)
        Next
        '
        Dim chars As Char() = value.ToCharArray()
        For i As Integer = 0 To chars.Length - 1
            If chars(i) < ChrW(256) Then
                chars(i) = convertTable(Convert.ToInt32(chars(i)))
            End If
        Next
        Return New String(chars)
    End Function

    Public Shared Function UnicodeToTCVN3(ByVal objData As Object) As String
        Dim strvalue As String = ""

        Try
            If (objData Is Nothing) OrElse objData.ToString = String.Empty Then
                strvalue = ""
            Else
                strvalue = objData.ToString
            End If

        Catch ex As Exception
            strvalue = ""
        End Try

        Return UnicodeToTCVN3(strvalue)
    End Function

    Public Shared Function UnicodeToTCVN3(ByVal value As String) As String
        Dim tcvnchars As Char()
        Dim tcvnchars_int As Integer() = {184, 181, 182, 183, 185, 168, 190, 187, 188, 189, 198, 169, 202, 199, 200, 201, 203, 208, 204, 206, 207, 209, 170, 213, 210, 211, 212, 214, 221, 215, 216, 220, 222, 227, 223, 225, 226, 228, 171, 232, 229, 230, 231, 233, 172, 237, 234, 235, 236, 238, 243, 239, 241, 242, 244, 173, 248, 245, 246, 247, 249, 253, 250, 251, 252, 254, 174, 184, 181, 182, 183, 185, 161, 190, 187, 188, 189, 198, 162, 202, 199, 200, 201, 203, 208, 204, 206, 207, 209, 163, 213, 210, 211, 212, 214, 221, 215, 216, 220, 222, 227, 223, 225, 226, 228, 164, 232, 229, 230, 231, 233, 165, 237, 234, 235, 236, 238, 243, 239, 241, 242, 244, 166, 248, 245, 246, 247, 249, 253, 250, 251, 252, 254, 167}

        Dim unichars As Char()

        Dim unichars_int As Integer() = {225, 224, 7843, 227, 7841, 259, 7855, 7857, 7859, 7861, 7863, 226, 7845, 7847, 7849, 7851, 7853, 233, 232, 7867, 7869, 7865, 234, 7871, 7873, 7875, 7877, 7879, 237, 236, 7881, 297, 7883, 243, 242, 7887, 245, 7885, 244, 7889, 7891, 7893, 7895, 7897, 417, 7899, 7901, 7903, 7905, 7907, 250, 249, 7911, 361, 7909, 432, 7913, 7915, 7917, 7919, 7921, 253, 7923, 7927, 7929, 7925, 273, 193, 192, 7842, 195, 7840, 258, 7854, 7856, 7858, 7860, 7862, 194, 7844, 7846, 7848, 7850, 7852, 201, 200, 7866, 7868, 7864, 202, 7870, 7872, 7874, 7876, 7878, 205, 204, 7880, 296, 7882, 211, 210, 7886, 213, 7884, 212, 7888, 7890, 7892, 7894, 7896, 416, 7898, 7900, 7902, 7904, 7906, 218, 217, 7910, 360, 7908, 431, 7912, 7914, 7916, 7918, 7920, 221, 7922, 7926, 7928, 7924, 272}

        tcvnchars = New Char(tcvnchars_int.Length) {}
        For i As Integer = 0 To tcvnchars_int.Length - 1
            tcvnchars(i) = ChrW(tcvnchars_int(i))
        Next

        unichars = New Char(unichars_int.Length) {}
        For i As Integer = 0 To unichars_int.Length - 1
            unichars(i) = ChrW(unichars_int(i))
        Next

        Dim chars As Char() = value.ToCharArray()
        For i As Integer = 0 To chars.Length - 1
            For j As Integer = 0 To tcvnchars.Length - 1
                If chars(i) = unichars(j) Then
                    chars(i) = tcvnchars(j)
                    Exit For
                End If
            Next
        Next
        Return New String(chars)
    End Function
#End Region
#End Region
#Region "Hàm định dạng chuỗi với kí tự đầu tiên chữ hoa, các ki tự còn lại chữ thường"
    'BichNN update 27/05/2013
    Public Shared Function UppercaseFirst(ByVal s As String) As String
        If String.IsNullOrEmpty(s) Then
            Return String.Empty
        End If
        s = s.ToLower()
        Dim a As Char() = s.ToCharArray()
        a(0) = Char.ToUpper(a(0))
        Return New String(a)
    End Function

    Public Shared Function ToWordByUpperCase(text As String) As String
        'Use positive lookbehind to locate all upper-case letters
        'that are preceded by a lower-case letter.
        Dim patternPart1 As String = "(?<=[a-z])([A-Z])"
        ' Used positive lookbehind and lookahead to locate all
        ' upper-case letters that are preceded by an upper-case
        ' letter and followed by a lower-case letter.
        Dim patternPart2 As String = "(?<=[A-Z])([A-Z])(?=[a-z])"

        Dim pattern As String = patternPart1 + "|" + patternPart2
        Dim rgx = New Regex(pattern)
        Dim result As String = rgx.Replace(text, " $1$2")

        Return result
    End Function
#End Region

End Class

