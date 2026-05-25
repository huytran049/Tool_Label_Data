'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'+
'+ Dự án      : Thư viện chung
'+ Lớp          : SMCString 
'+ Giải thích  : Các hàm chung về xử lý xâu, chuỗi, ký tự
'+
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'+ 2013/05/13   SaoMaiSoft/Tiepnx  Tạo mới
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Option Explicit On
Option Strict On

Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
'Imports System.Windows.Forms
Imports System.Xml
Imports System
Imports System.Collections.Generic
Imports System.Linq
Public Class SMCReadNumberToWord
    Public Shared Function NumberToWords(ByVal number As Integer) As String
        If number = 0 Then
            Return ("zero")
        End If

        If number < 0 Then
            Return ("minus " + NumberToWords(Math.Abs(number)))
        End If
        Dim words As String = ""
        If (number / 1000000) > 0 Then
            words += NumberToWords(Convert.ToInt32((number / 1000000).ToString())) + " million "
            number = number Mod 1000000
        End If

        If (number / 1000) > 0 Then
            words += NumberToWords(Convert.ToInt32((number / 1000).ToString())) + " thousand "
            number = number Mod 1000
        End If

        If (number / 100) > 0 Then
            words += NumberToWords(Convert.ToInt32((number / 100).ToString())) + " hundred "
            number = number Mod 100
        End If
        If number > 0 Then
            If words <> "" Then
                words += "and "
            End If

            Dim unitsMap() As String = {"zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen"}
            Dim tensMap() As String = {"zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety"}

            If number < 20 Then
                words += unitsMap(number)
            Else
                words += tensMap(Convert.ToInt32(number / 10))
                If (number Mod 10) > 0 Then
                    words += "-" + unitsMap(number Mod 10)
                End If
            End If
        End If
        Return (words)
    End Function

    Public Shared Function ToNumberString(ByVal number As Decimal) As String
        Dim s As String = number.ToString("#")
        Dim so As String() = New String() {"không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín"}
        Dim hang As String() = New String() {"", "nghìn", "triệu", "tỷ"}
        Dim i As Integer, j As Integer, donvi As Integer, chuc As Integer, tram As Integer
        Dim str As String = " "
        Dim booAm As Boolean = False
        Dim decS As Decimal = 0
        'Tung addnew
        Try
            decS = Convert.ToDecimal(s.ToString())
        Catch
        End Try
        If decS < 0 Then
            decS = -decS
            s = decS.ToString()
            booAm = True
        End If
        i = s.Length
        If i = 0 Then
            str = so(0) + str
        Else
            j = 0
            While (i > 0)
                donvi = Convert.ToInt32(s.Substring(i - 1, 1))
                System.Math.Max(System.Threading.Interlocked.Decrement(i), i + 1)
                If i > 0 Then
                    chuc = Convert.ToInt32(s.Substring(i - 1, 1))
                Else
                    chuc = -1
                End If
                System.Math.Max(System.Threading.Interlocked.Decrement(i), i + 1)
                If i > 0 Then
                    tram = Convert.ToInt32(s.Substring(i - 1, 1))
                Else
                    tram = -1
                End If
                System.Math.Max(System.Threading.Interlocked.Decrement(i), i + 1)
                If (donvi > 0) OrElse (chuc > 0) OrElse (tram > 0) OrElse (j = 3) Then
                    str = hang(j) + str
                End If
                System.Math.Max(System.Threading.Interlocked.Increment(j), j - 1)
                If j > 3 Then
                    j = 1
                End If
                If (donvi = 1) AndAlso (chuc > 1) Then
                    str = "một " + str
                Else
                    If (donvi = 5) AndAlso (chuc > 0) Then
                        str = "lăm " + str
                    ElseIf donvi > 0 Then
                        str = so(donvi) + " " + str
                    End If
                End If
                If chuc < 0 Then
                    Exit While
                Else
                    If (chuc = 0) AndAlso (donvi > 0) Then
                        str = "lẻ " + str
                    End If
                    If chuc = 1 Then
                        str = "mười " + str
                    End If
                    If chuc > 1 Then
                        str = so(chuc) + " mươi " + str
                    End If
                End If
                If tram < 0 Then
                    Exit While
                Else
                    If (tram > 0) OrElse (chuc > 0) OrElse (donvi > 0) Then
                        str = so(tram) + " trăm " + str
                    End If
                End If
                str = " " + str
            End While
        End If
        If booAm Then
            str = "Âm " + str
        End If
        Return (str)
    End Function

    Public Shared Function ToNumberString(ByVal number As Double) As String
        Dim s As String = number.ToString("#")
        Dim so As String() = New String() {"không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín"}
        Dim hang As String() = New String() {"", "nghìn", "triệu", "tỷ"}
        Dim i As Integer, j As Integer, donvi As Integer, chuc As Integer, tram As Integer
        Dim str As String = " "
        Dim booAm As Boolean = False
        Dim decS As Double = 0
        'Tung addnew
        Try
            decS = Convert.ToDouble(s.ToString())
        Catch
        End Try
        If decS < 0 Then
            s = decS.ToString()
            booAm = True
        End If
        i = s.Length
        If i = 0 Then
            str = so(0) + str
        Else
            j = 0
            While (i > 0)
                donvi = Convert.ToInt32(s.Substring(i - 1, 1))
                System.Math.Max(System.Threading.Interlocked.Decrement(i), i + 1)
                If i > 0 Then
                    chuc = Convert.ToInt32(s.Substring(i - 1, 1))
                Else
                    chuc = -1
                End If
                System.Math.Max(System.Threading.Interlocked.Decrement(i), i + 1)
                If i > 0 Then
                    tram = Convert.ToInt32(s.Substring(i - 1, 1))
                Else
                    tram = -1
                End If
                System.Math.Max(System.Threading.Interlocked.Decrement(i), i + 1)
                If (donvi > 0) OrElse (chuc > 0) OrElse (tram > 0) OrElse (j = 3) Then
                    str = hang(j) + str
                End If
                System.Math.Max(System.Threading.Interlocked.Increment(j), j - 1)
                If j > 3 Then
                    j = 1
                End If
                If (donvi = 1) AndAlso (chuc > 1) Then
                    str = "một " + str
                Else
                    If (donvi = 5) AndAlso (chuc > 0) Then
                        str = "lăm " + str
                    ElseIf donvi > 0 Then
                        str = so(donvi) + " " + str
                    End If
                End If
                If chuc < 0 Then
                    Exit While
                Else
                    If (chuc = 0) AndAlso (donvi > 0) Then
                        str = "lẻ " + str
                    End If
                    If chuc = 1 Then
                        str = "mười " + str
                    End If
                    If chuc > 1 Then
                        str = so(chuc) + " mươi " + str
                    End If
                End If
                If tram < 0 Then
                    Exit While
                Else
                    If (tram > 0) OrElse (chuc > 0) OrElse (donvi > 0) Then
                        str = so(tram) + " trăm " + str
                    End If
                End If
                str = " " + str
            End While
        End If
        If booAm Then
            str = "Âm " + str
        End If
        Return (str)
    End Function

    Public Shared Function changeCurrencyToWords(ByVal numb As Double) As [String]
        Return (changeToWords(numb.ToString(), True))
    End Function

    Private Shared Function changeToWords(ByVal numb As [String], ByVal isCurrency As Boolean) As [String]
        Dim val As [String] = "", wholeNo As [String] = numb, points As [String] = "", andStr As [String] = "", pointStr As [String] = ""
        Dim endStr As [String] = If((isCurrency), ("Only"), (""))
        Try
            Dim decimalPlace As Integer = numb.IndexOf(".")
            If decimalPlace > 0 Then
                wholeNo = numb.Substring(0, decimalPlace)
                points = numb.Substring(decimalPlace + 1)
                If Convert.ToInt32(points) > 0 Then
                    andStr = If((isCurrency), ("and"), ("point"))
                    ' just to separate whole numbers from points/cents
                    endStr = If((isCurrency), ("Cents " + endStr), (""))
                    pointStr = translateCents(points)
                End If
            End If
            val = [String].Format("{0} {1}{2} {3}", translateWholeNumber(wholeNo).Trim(), andStr, pointStr, endStr)
        Catch

        End Try
        Return (val)
    End Function
    Private Shared Function translateWholeNumber(ByVal number As [String]) As [String]
        Dim word As String = ""
        Try
            Dim beginsZero As Boolean = False
            'tests for 0XX
            Dim isDone As Boolean = False
            'test if already translated
            Dim dblAmt As Double = (Convert.ToDouble(number))
            'if ((dblAmt > 0) && number.StartsWith("0"))
            If dblAmt > 0 Then
                'test for zero or digit zero in a nuemric
                beginsZero = number.StartsWith("0")

                Dim numDigits As Integer = number.Length
                Dim pos As Integer = 0
                'store digit grouping
                Dim place As [String] = ""
                'digit grouping name:hundres,thousand,etc...
                Select Case numDigits
                    Case (1)
                        'ones' range
                        word = ones(number)
                        isDone = True
                        Exit Select
                    Case (2)
                        'tens' range
                        word = tens(number)
                        isDone = True
                        Exit Select
                    Case (3)
                        'hundreds' range
                        pos = (numDigits Mod 3) + 1
                        place = " Hundred "
                        Exit Select
                        'thousands' range
                    Case (4)
                        pos = (numDigits Mod 4) + 1
                        place = " Thousand "
                        Exit Select
                    Case (5)
                        pos = (numDigits Mod 4) + 1
                        place = " Thousand "
                        Exit Select
                    Case (6)
                        pos = (numDigits Mod 4) + 1
                        place = " Thousand "
                        Exit Select
                        'millions' range
                    Case (7)
                        pos = (numDigits Mod 7) + 1
                        place = " Million "
                        Exit Select
                    Case (8)
                        pos = (numDigits Mod 7) + 1
                        place = " Million "
                        Exit Select
                    Case (9)
                        pos = (numDigits Mod 7) + 1
                        place = " Million "
                        Exit Select
                    Case (10)
                        'Billions's range
                        pos = (numDigits Mod 10) + 1
                        place = " Billion "
                        Exit Select
                    Case Else
                        'add extra case options for anything above Billion...
                        isDone = True
                        Exit Select
                End Select
                If Not isDone Then
                    'if transalation is not done, continue...(Recursion comes in now!!)
                    word = translateWholeNumber(number.Substring(0, pos)) + place + translateWholeNumber(number.Substring(pos))
                    'check for trailing zeros
                    If beginsZero Then
                        word = " and " + word.Trim()
                    End If
                End If
                'ignore digit grouping names
                If word.Trim().Equals(place.Trim()) Then
                    word = ""
                End If
            End If
        Catch

        End Try
        Return (word.Trim())
    End Function
    Private Shared Function tens(ByVal digit As [String]) As [String]
        Dim digt As Integer = Convert.ToInt32(digit)
        Dim name As [String] = Nothing
        Select Case digt
            Case (10)
                name = "Ten"
                Exit Select
            Case (11)
                name = "Eleven"
                Exit Select
            Case (12)
                name = "Twelve"
                Exit Select
            Case (13)
                name = "Thirteen"
                Exit Select
            Case (14)
                name = "Fourteen"
                Exit Select
            Case (15)
                name = "Fifteen"
                Exit Select
            Case (16)
                name = "Sixteen"
                Exit Select
            Case (17)
                name = "Seventeen"
                Exit Select
            Case (18)
                name = "Eighteen"
                Exit Select
            Case (19)
                name = "Nineteen"
                Exit Select
            Case (20)
                name = "Twenty"
                Exit Select
            Case (30)
                name = "Thirty"
                Exit Select
            Case (40)
                name = "Fourty"
                Exit Select
            Case (50)
                name = "Fifty"
                Exit Select
            Case (60)
                name = "Sixty"
                Exit Select
            Case (70)
                name = "Seventy"
                Exit Select
            Case (80)
                name = "Eighty"
                Exit Select
            Case (90)
                name = "Ninety"
                Exit Select
            Case Else
                If digt > 0 Then
                    name = tens(digit.Substring(0, 1) + "0") + " " + ones(digit.Substring(1))
                End If
                Exit Select
        End Select
        Return (name)
    End Function
    Private Shared Function ones(ByVal digit As [String]) As [String]
        Dim digt As Integer = Convert.ToInt32(digit)
        Dim name As [String] = ""
        Select Case digt
            Case (1)
                name = "One"
                Exit Select
            Case (2)
                name = "Two"
                Exit Select
            Case (3)
                name = "Three"
                Exit Select
            Case (4)
                name = "Four"
                Exit Select
            Case (5)
                name = "Five"
                Exit Select
            Case (6)
                name = "Six"
                Exit Select
            Case (7)
                name = "Seven"
                Exit Select
            Case (8)
                name = "Eight"
                Exit Select
            Case (9)
                name = "Nine"
                Exit Select
        End Select
        Return (name)
    End Function
    Private Shared Function translateCents(ByVal cents As [String]) As [String]
        Dim cts As [String] = "", digit As [String] = "", engOne As [String] = ""
        Dim i As Integer = 0
        While (i < cents.Length)
            digit = cents(i).ToString()
            If digit.Equals("0") Then
                engOne = "Zero"
            Else
                engOne = ones(digit)
            End If
            cts += " " + engOne
            System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1)
        End While
        Return (cts)
    End Function
End Class
