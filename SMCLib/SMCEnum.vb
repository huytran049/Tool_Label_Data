Imports System.ComponentModel
Imports System.Reflection
Imports System.Linq

Public Class SMCEnum
    Public Shared Function EnumParse(Of T)(value As String) As Object
        If [Enum].IsDefined(GetType(T), value) Then
            Return DirectCast([Enum].Parse(GetType(T), value), T)
        End If

        Dim num As Integer
        If Integer.TryParse(value, num) Then
            If [Enum].IsDefined(GetType(T), num) Then
                Return DirectCast([Enum].ToObject(GetType(T), num), T)
            End If
        End If

        Return Nothing
    End Function
    Public Shared Function GetAttribute(Of TAttribute As Attribute)(value As [Enum]) As TAttribute
        Dim type = value.[GetType]()
        Dim name = [Enum].GetName(type, value)
        Return type.GetField(name).GetCustomAttributes(False).OfType(Of TAttribute)().SingleOrDefault()
    End Function
    Public Shared Function GetEnumDescription(Of T)(enumValue As String) As String
        If String.IsNullOrEmpty(enumValue) Then
            Return "Không xác định"
        End If
        Dim e = EnumParse(Of T)(enumValue)
        If e Is Nothing Then
            Return enumValue
        End If
        Dim fi As FieldInfo = GetType(T).GetField(e.ToString())
        If fi Is Nothing Then
            Return e.ToString()
        End If
        Dim attributes = DirectCast(fi.GetCustomAttributes(GetType(DescriptionAttribute), False), DescriptionAttribute())
        Return If(attributes.Length > 0, attributes(0).Description, e.ToString())
    End Function
    Public Shared Function GetEnumDescription(Of T)(index As Integer) As String
        Const result As String = "Unknow"
        If Not GetType(T).IsEnum Then
            Throw New Exception("Type given T must be an Enum")
        End If
        Dim obj = [Enum].GetValues(GetType(T))
        If obj.Length > 0 Then
            Dim enumName As String = [Enum].GetName(GetType(T), index)
            Return GetEnumDescription(Of T)(enumName)
        End If
        Return result
    End Function
    Public Shared Function EnumToDescriptionDictionary(Of T)() As Dictionary(Of String, String)
        If Not GetType(T).IsEnum Then
            Throw New Exception("Type given T must be an Enum")
        End If
        Dim array = DirectCast([Enum].GetValues(GetType(T)).Cast(Of T)(), T())
        Dim dic = New Dictionary(Of String, String)()
        For Each e As T In array
            Dim description As String = GetEnumDescription(Of T)(e.ToString())
            Dim value As Integer = Convert.ToInt32(e)
            dic.Add(value.ToString(), description)
        Next
        Return dic
    End Function
    Public Shared Function EnumToDescriptionDictionary(Of T)(ParamArray arr() As String) As Dictionary(Of String, String)
        If Not GetType(T).IsEnum Then
            Throw New Exception("Type given T must be an Enum")
        End If
        Dim split As Char = ";"
        Dim str As String = split
        For Each item As String In arr
            str += item + split
        Next
        Dim array = DirectCast([Enum].GetValues(GetType(T)).Cast(Of T)(), T())
        Dim dic = New Dictionary(Of String, String)()
        For Each e As T In array
            If (str.IndexOf(split + e.ToString() + split, System.StringComparison.Ordinal) >= 0) Then
                Dim description As String = GetEnumDescription(Of T)(e.ToString())
                Dim value As Integer = Convert.ToInt32(e)
                dic.Add(value.ToString(), description)
            End If
        Next
        Return dic
    End Function
    Public Shared Function EnumToDescriptionDictionary(Of T)(ParamArray arr() As Int32) As Dictionary(Of String, String)
        If Not GetType(T).IsEnum Then
            Throw New Exception("Type given T must be an Enum")
        End If
        Dim split As Char = ";"
        Dim str As String = split
        For Each item As Int32 In arr
            str += item.ToString() + split
        Next
        Dim array = DirectCast([Enum].GetValues(GetType(T)).Cast(Of T)(), T())
        Dim dic = New Dictionary(Of String, String)()
        For Each e As T In array
            If (str.IndexOf(split + Convert.ToInt32(e).ToString() + split, System.StringComparison.Ordinal) >= 0) Then
                Dim description As String = GetEnumDescription(Of T)(e.ToString())
                Dim value As Integer = Convert.ToInt32(e)
                dic.Add(value.ToString(), description)
            End If
        Next
        Return dic
    End Function
    Public Shared Function EnumToDescriptionDictionary(Of T)(ParamArray arr() As [Enum]) As Dictionary(Of String, String)
        If Not GetType(T).IsEnum Then
            Throw New Exception("Type given T must be an Enum")
        End If
        Dim split As Char = ";"
        Dim str As String = split
        For Each item As [Enum] In arr
            str += item.ToString() + split
        Next
        Dim array = DirectCast([Enum].GetValues(GetType(T)).Cast(Of T)(), T())
        Dim dic = New Dictionary(Of String, String)()
        For Each e As T In array
            If (str.IndexOf(split + e.ToString() + split, System.StringComparison.Ordinal) >= 0) Then
                Dim description As String = GetEnumDescription(Of T)(e.ToString())
                Dim value As Integer = Convert.ToInt32(e)
                dic.Add(value.ToString(), description)
            End If
        Next
        Return dic
    End Function
End Class
