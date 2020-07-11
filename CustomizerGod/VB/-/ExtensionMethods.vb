Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Runtime.CompilerServices
Imports System.Text

Module ExtensionMethods
    <Extension> Function ToEnum(Of T)(Str$) As T
        If String.IsNullOrEmpty(Str) Then
            Dim i As T
            Return i
        End If
        Return DirectCast([Enum].Parse(GetType(T), Str), T)
    End Function
    <Extension> Function GetValueOrDefault(Of TKey, TValue)(d As Dictionary(Of TKey, TValue), Key As TKey, DefaultValue As TValue) As TValue
        If d.ContainsKey(Key) Then Return d(Key)
        Return DefaultValue
    End Function
    <Extension> Function ReadUTF16$(br As IO.BinaryReader)
        Dim Str = New List(Of Byte)
        While True
            Dim c = br.ReadBytes(2)
            If c(0) = 0 AndAlso c(1) = 0 Then Exit While 'UNICODE_NULL
            Str.AddRange(c)
        End While
        Return Encoding.Unicode.GetString(Str.ToArray)
    End Function
    <Extension> Sub DWORDAlign(s As IO.Stream)
#If False Then 'this doesn't work
        Dim PaddingCount = CInt(s.Position Mod 4)
        If PaddingCount > 0 Then
            Dim Buffer(PaddingCount - 1) As Byte
            s.Read(Buffer, 0, PaddingCount)
            If Not Buffer.All(Function(b) b = 0) Then Echo("DWORD Aligned: ", BitConverter.ToString(Buffer))
        End If
#Else
        s.Position = s.Position + 3 And -4
#End If
    End Sub
    <Extension> Function ReadPUTF16$(br As IO.BinaryReader)
        Dim StrLen = br.ReadUInt16
        Return Encoding.Unicode.GetString(br.ReadBytes(StrLen * 2))
    End Function
    <Extension> Function TrimEnd(Bytes As Byte(), Length%) As Byte()
        If Bytes Is Nothing OrElse Bytes.Length = 0 Then Throw New ArgumentException(NameOf(Bytes) & " is null or empty")
        Dim Count = Bytes.Length - Length
        Dim TrimmedBytes(Count - 1) As Byte
        Buffer.BlockCopy(Bytes, 0, TrimmedBytes, 0, Count)
        Return TrimmedBytes
    End Function
    <Extension> Function ToUShort(ResID$) As UShort
        Return CUShort(ResID.Substring(1))
    End Function
    <Extension> Function ToPrettyString$(Of K, V)(d As Dictionary(Of K, V), Optional Separator$ = ": ", Optional EndString$ = "")
        Return String.Join(Environment.NewLine, From e In d Select $"{e.Key}{Separator}{e.Value}{EndString}") & Environment.NewLine
    End Function
    <Extension> Function ToDetailedString$(Ex As Exception)
        Return $"==Exception: {Ex.GetType.FullName}==
{Ex.Message}

{If(String.IsNullOrWhiteSpace(Ex.StackTrace), "", Ex.StackTrace.TrimEnd)}
"
    End Function
End Module
