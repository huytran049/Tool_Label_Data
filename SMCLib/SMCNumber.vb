Imports System.Globalization
Imports System.Threading
'Imports SEI_Utils

Public Class SMCNumber
    Public Shared Function ToCurrency(ByVal objValue As Object) As String
        Dim strReturn As String
        Dim specifier As String = "#,#.00#"
        Try
            If ToDecimal(objValue) = 0 Or (ToDecimal(objValue) < 1 And ToDecimal(objValue) > 0) Then
                strReturn = "0" & ToDecimal(objValue).ToString(specifier)
            Else
                strReturn = ToDecimal(objValue).ToString(specifier)
            End If
            'strReturn = Utils.DecimalObjectToString(objValue)
        Catch ex As Exception
            strReturn = String.Empty
        End Try
        Return strReturn
    End Function

    Public Shared Function ToVNCurrency(ByVal objValue As String) As String
        Dim strReturn As String
        Try
            strReturn = ToLong(objValue).ToString("#,###")
            strReturn = strReturn.Replace(",", ".")
            'strReturn = Utils.DecimalObjectToString(objValue, True)
        Catch ex As Exception
            strReturn = String.Empty
        End Try
        Return strReturn
    End Function

    Public Shared Function ToVNCurrency(ByVal objValue As Object) As String
        Dim strReturn As String
        Try
            strReturn = ToLong(objValue).ToString("#,##0")
            strReturn = strReturn.Replace(",", ".")
            'strReturn = Utils.DecimalObjectToString(objValue, True)
        Catch ex As Exception
            strReturn = String.Empty
        End Try
        Return strReturn
    End Function

    Public Shared Function ToVNChieuDai(ByVal objValue As Object) As String
        Dim strReturn As String
        Try
            strReturn = ToDecimal(objValue)
            strReturn = strReturn.ToString().Replace(".", ",")
        Catch ex As Exception
            strReturn = String.Empty
        End Try
        Return strReturn
    End Function
    Public Shared Function ConvertToRoman(ByVal objValue As Object) As String
        If (objValue Is Nothing) OrElse objValue.Equals(String.Empty) Then
            Return String.Empty
        End If
        Dim pstrDecimalNumber As String = objValue.ToString()
        Const strPOS_VAL As String = "IXCM"
        Const strFIVE_VAL As String = "VLD"
        Dim strRoman As String = String.Empty
        Dim strCurrRomanPos As String
        Dim strLetter1 As String
        Dim strLetter2 As String
        Dim intCurrPos As Integer
        Dim intDigit As Integer
        Dim intDigitPos As Integer
        intCurrPos = 1
        For intDigitPos = Len(pstrDecimalNumber) To 1 Step -1
            intDigit = Val(Mid$(pstrDecimalNumber, intDigitPos, 1))
            strCurrRomanPos = Mid$(strPOS_VAL, intCurrPos, 1)
            Select Case intDigit
                Case 9
                    strLetter1 = strCurrRomanPos
                    strLetter2 = Mid$(strPOS_VAL, intCurrPos + 1, 1)
                Case Is > 4
                    strLetter1 = Mid$(strFIVE_VAL, intCurrPos, 1)
                    strLetter2 = New String(strCurrRomanPos, intDigit - 5)
                Case 4
                    strLetter1 = strCurrRomanPos
                    strLetter2 = Mid$(strFIVE_VAL, intCurrPos, 1)
                Case Else
                    strLetter1 = New String(strCurrRomanPos, intDigit)
                    strLetter2 = String.Empty
            End Select
            strRoman = strLetter1 & strLetter2 & strRoman
            intCurrPos = intCurrPos + 1
        Next

        Return strRoman

    End Function
    Public Shared Function RomanToDecimal(ByVal objValue As String) As Decimal
        If (objValue Is Nothing) OrElse objValue.Equals(String.Empty) Then
            Return 0
        End If
        Dim pstrRomanNumeral As String = objValue.ToString()
        Dim aintRomanValues() As Integer
        Dim intInputLen As Integer
        Dim intX As Integer
        Dim intSum As Decimal

        intInputLen = Len(pstrRomanNumeral)

        If intInputLen = 0 Then
            Return 0
        End If
        ReDim aintRomanValues(intInputLen)
        For intX = 1 To intInputLen
            Select Case Mid$(pstrRomanNumeral, intX, 1)
                Case "M" : aintRomanValues(intX) = 1000
                Case "D" : aintRomanValues(intX) = 500
                Case "C" : aintRomanValues(intX) = 100
                Case "L" : aintRomanValues(intX) = 50
                Case "X" : aintRomanValues(intX) = 10
                Case "V" : aintRomanValues(intX) = 5
                Case "I" : aintRomanValues(intX) = 1
            End Select
        Next

        For intX = 1 To intInputLen
            If intX = intInputLen Then
                intSum = intSum + aintRomanValues(intX)
            Else
                If aintRomanValues(intX) >= aintRomanValues(intX + 1) Then
                    intSum = intSum + aintRomanValues(intX)
                Else
                    intSum = intSum - aintRomanValues(intX)
                End If
            End If
        Next

        Return intSum

    End Function
    Public Shared Function IsNumeric(ByVal objValue As Object) As Boolean
        Dim dteBuf As Decimal
        Try
            If (objValue Is Nothing) OrElse objValue.Equals(String.Empty) Then
                Return False
            End If
            Return Decimal.TryParse(objValue.ToString.Replace(".", String.Empty), dteBuf)
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function IsInteger(ByVal objValue As Object) As Boolean
        Dim dteBuf As Integer
        Try
            If (objValue Is Nothing) OrElse objValue.Equals(String.Empty) Then
                Return False
            End If
            Return Integer.TryParse(objValue.ToString.Replace(".", String.Empty), dteBuf)
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function IsLong(ByVal objValue As Object) As Boolean
        Dim dteBuf As Long
        Try
            If (objValue Is Nothing) OrElse objValue.Equals(String.Empty) Then
                Return False
            End If
            Return Long.TryParse(objValue.ToString.Replace(".", String.Empty), dteBuf)
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function IsDecimal(ByVal objValue As Object) As Boolean
        Dim dteBuf As Decimal
        Try
            If (objValue Is Nothing) OrElse objValue.Equals(String.Empty) Then
                Return False
            End If
            Return Decimal.TryParse(objValue.ToString, dteBuf)
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function IsDouble(ByVal objValue As Object) As Boolean
        Dim dteBuf As Double
        Try
            If (objValue Is Nothing) OrElse objValue.Equals(String.Empty) Then
                Return False
            End If
            Return Double.TryParse(objValue.ToString, dteBuf)
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function IsSingle(ByVal objValue As Object) As Boolean
        Dim dteBuf As Single
        Try
            If (objValue Is Nothing) OrElse objValue.Equals(String.Empty) Then
                Return False
            End If
            Return Single.TryParse(objValue.ToString, dteBuf)
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function ToBoolean(ByVal objData As Object) As Boolean
        Dim blnResult As Boolean = False
        Try
            Dim intResult As Integer = Integer.Parse(objData)
            If intResult <> 0 Then
                blnResult = True
            End If
        Catch ex As Exception
            blnResult = False
        End Try
        Return blnResult
    End Function

    Public Shared Function ToInteger(ByVal objData As Object, Optional ByVal intErrValue As Integer = 0, Optional ByVal decimalSplit As String = ",") As Integer
        Dim intResult As Integer = 0
        Try
            intResult = intErrValue
            'If (objData Is Nothing) OrElse objData.Equals(String.Empty) Then Exit Try
            'If Integer.TryParse(objData.ToString, intResult) = False Then
            '    intResult = intErrValue
            '    Exit Try
            'End If
            Dim val As String = objData.ToString()
            val = val.Replace(decimalSplit, String.Empty)
            intResult = Integer.Parse(val)
        Catch ex As Exception
            intResult = intErrValue
        End Try
        Return intResult
    End Function

    Public Shared Function DecToInt(ByVal objData As Object, Optional ByVal intErrValue As Integer = 0) As Integer
        Dim intResult As Integer = 0
        Try
            intResult = intErrValue
            Dim val As Decimal = ToDecimal(objData)
            intResult = Decimal.Parse(val)
        Catch ex As Exception
            intResult = intErrValue
        End Try
        Return intResult
    End Function

    Public Shared Function ToIntegerMoney(ByVal objData As Object, Optional ByVal decErrValue As Integer = 0) As Integer
        Dim decResult As Integer = 0
        Try
            decResult = decErrValue
            If (objData Is Nothing) OrElse objData.Equals(String.Empty) Then Exit Try
            objData = objData.ToString()
            'objData = Decimal.Parse(objData.ToString()).ToString("#,##")
            If Integer.TryParse(objData.ToString, NumberStyles.Currency, CultureInfo.CreateSpecificCulture("en-US"), decResult) = False Then
                decResult = decErrValue
                Exit Try
            End If
        Catch ex As Exception
            decResult = decErrValue
        End Try
        If (decResult <= decErrValue) Then
            decResult = decErrValue
        End If
        Return decResult
    End Function
    Public Shared Function ToLong(ByVal objData As Object, Optional ByVal lngErrValue As Long = 0) As Long

        Dim lngResult As Long = 0
        Dim objDataResult As String
        Try
            lngResult = lngErrValue
            If (objData Is Nothing) OrElse objData.Equals(String.Empty) Then Exit Try
            objDataResult = objData.ToString().Replace(",", String.Empty).Replace(".", String.Empty).Replace(" ", String.Empty)
            If Long.TryParse(objDataResult, lngResult) = False Then
                lngResult = lngErrValue
                Exit Try
            End If
            lngResult = objDataResult
        Catch ex As Exception
            lngResult = lngErrValue
        End Try
        Return lngResult
    End Function

    Public Shared Function ToDouble(ByVal objData As Object, Optional ByVal dblErrValue As Double = 0) As Double
        Dim dblResult As Double = 0
        Try
            dblResult = dblErrValue
            If (objData Is Nothing) OrElse objData.Equals(String.Empty) Then Exit Try
            If Double.TryParse(objData.ToString, dblResult) = False Then
                dblResult = dblErrValue
                Exit Try
            End If
        Catch ex As Exception
            dblResult = dblErrValue
        End Try
        Return dblResult
    End Function

    Public Shared Function ToDecimal(ByVal objData As Object, Optional ByVal decErrValue As Decimal = 0) As Decimal
        Dim decResult As Decimal = 0
        Try
            decResult = decErrValue
            If (objData Is Nothing) OrElse objData.Equals(String.Empty) Then Exit Try
            decResult = ObjectToDecimal(objData)
            'If Decimal.TryParse(objData.ToString, NumberStyles.Currency, CultureInfo.CreateSpecificCulture("en-US"), decResult) = False Then
            '    decResult = decErrValue
            '    Exit Try
            'End If
        Catch ex As Exception
            decResult = decErrValue
        End Try
        Return decResult
    End Function
    Private Shared Function ObjectToDecimal(obj As Object) As Decimal
        Dim returndata As Decimal = 0
        Try
            If obj IsNot Nothing Then
                Dim currentCulture As CultureInfo = Thread.CurrentThread.CurrentCulture
                Dim currentCultureFormat As New System.Globalization.CultureInfo("en-US")
                Dim objNumber As String = obj.ToString()
                If Not currentCulture.Name.Equals("en-US") Then
                    objNumber = objNumber.Replace(",", ".")
                    currentCulture = currentCultureFormat
                End If
                If objNumber.Contains(",") AndAlso objNumber.Contains(".") Then
                Else
                    If objNumber.Contains(",") AndAlso Not objNumber.Contains(".") AndAlso (objNumber.EndsWith(",00") OrElse objNumber.EndsWith(",0")) Then
                        objNumber = objNumber.Replace(",", ".")

                    End If
                End If

                returndata = Convert.ToDecimal(objNumber, currentCulture)
            Else
                returndata = 0
            End If
        Catch
            returndata = 0
        End Try
        Return returndata
    End Function

    Private Shared Function DecimalObjectToString(obj As Object, isVietnameseFormat As Boolean, decimals As Integer) As String
        Try
            Dim usCulture As New System.Globalization.CultureInfo("en-US")
            Dim vnCulture As New System.Globalization.CultureInfo("vi-VN")

            Dim currentCulture As CultureInfo = Thread.CurrentThread.CurrentCulture
            If Not currentCulture.Name.Equals("en-US") Then
                obj = obj.ToString().Replace(",", ".")
            End If
            Dim data As Decimal = ObjectToDecimal(obj)
            Dim datastring As String = data.ToString()
            If Not currentCulture.Name.Equals("en-US") Then
                datastring = datastring.Replace(",", ".")
            End If


            If datastring.Contains(".") Then
                Dim decPart As Decimal = data - Math.Truncate(data)
                If decimals > 0 Then
                    If decPart > 0 Then
                        If decimals > 2 Then
                            Dim dec As String = decPart.ToString().Substring(0, 4)
                            Dim decNum As Decimal = ObjectToDecimal(dec)
                            If decNum > 0 Then
                                If isVietnameseFormat Then
                                    Return data.ToString("N" + 2, vnCulture)
                                End If
                                Return data.ToString("N" + 2, usCulture)
                            Else
                                If isVietnameseFormat Then
                                    Return data.ToString("N" + 0, vnCulture)
                                End If
                                Return data.ToString("N" + 0, usCulture)
                            End If
                        Else
                            If isVietnameseFormat Then
                                Return data.ToString("N" + decimals, vnCulture)
                            End If
                            Return data.ToString("N" + decimals, usCulture)

                        End If
                    Else
                        If isVietnameseFormat Then
                            Return data.ToString("N" + 0, vnCulture)
                        End If
                        Return data.ToString("N" + 0, usCulture)

                    End If
                Else
                    If isVietnameseFormat Then
                        Return data.ToString("N" + 0, vnCulture)
                    End If
                    Return data.ToString("N" + 0, usCulture)
                End If
            Else
                If isVietnameseFormat Then
                    Return data.ToString("N" + 0, vnCulture)
                End If
                Return data.ToString("N" + 0, usCulture)
            End If
        Catch
            Return "0"
        End Try
    End Function


    Public Shared Function ToDecimalMoney(ByVal objData As Object, Optional ByVal decErrValue As Decimal = 0) As Decimal
        Dim decResult As Decimal = 0
        Try
            decResult = decErrValue
            If (objData Is Nothing) OrElse objData.Equals(String.Empty) Then Exit Try
            objData = objData.ToString()
            'decResult = Utils.ObjectToDecimal(objData)
            objData = Decimal.Parse(objData.ToString()).ToString("#,##")
            If Decimal.TryParse(objData.ToString, NumberStyles.Currency, CultureInfo.CreateSpecificCulture("en-US"), decResult) = False Then
                decResult = decErrValue
                Exit Try
            End If
        Catch ex As Exception
            decResult = decErrValue
        End Try
        If (decResult <= decErrValue) Then
            decResult = decErrValue
        End If
        Return decResult
    End Function
End Class
