'Imports System
'Imports System.Collections.Generic
'Imports System.Runtime.InteropServices
'Imports System.Text

'Module VersionResource
'    <StructLayout(LayoutKind.Explicit)> Structure HighLowDWORD
'        <FieldOffset(0)> Public DWORD As UInteger
'        <FieldOffset(0)> Public Low As UShort
'        <FieldOffset(2)> Public High As UShort
'    End Structure
'    Structure VersionHead
'        Dim wLength, wValueLength, wType As UShort
'        Dim szKey As String
'    End Structure
'    Function GetVerSubResHead(br As IO.BinaryReader) As VersionHead
'        GetVerSubResHead = New VersionHead With {.wLength = br.ReadUInt16, .wValueLength = br.ReadUInt16, .wType = br.ReadUInt16, .szKey = br.ReadUTF16}
'        br.BaseStream.DWORDAlign
'    End Function
'    Function GetVersionFromDWORD$(DWORD As UInteger)
'        Dim hln = New HighLowDWORD With {.DWORD = DWORD}
'        Return $"{hln.High}.{hln.Low}"
'    End Function
'    Sub ReadFFI(br As IO.BinaryReader, sb As StringBuilder)
'        Dim ffi = br.ReadBytes(13 * 4).ToStructure(Of VS_FixedFileInfo)
'        If ffi.dwSignature <> VSFixedFileInfoSignature Then sb.AppendLine("Version resource corrupted")
'        sb.AppendLine($"File Version: {GetVersionFromDWORD(ffi.dwFileVersionMS)}.{GetVersionFromDWORD(ffi.dwFileVersionLS)}")
'        sb.AppendLine($"Product Version: {GetVersionFromDWORD(ffi.dwProductVersionMS)}.{GetVersionFromDWORD(ffi.dwProductVersionLS)}")
'        sb.AppendLine($"Structure Version: {GetVersionFromDWORD(ffi.dwStrucVersion)}")
'        sb.AppendLine("File Flag Mask: " & ffi.dwFileFlagMask)
'        sb.AppendLine("File Flags: " & ffi.dwFileFlags.ToString)
'        sb.AppendLine("File OS: " & ffi.dwFileOS.ToString)
'        sb.AppendLine("File Type: " & ffi.dwFileType.ToString)
'        sb.AppendLine("File Subtype: " & ffi.dwFileSubtype.ToString)
'    End Sub
'    Sub ReadBlock(br As IO.BinaryReader, ms As IO.MemoryStream, sb As StringBuilder, Optional InsideBlock As Boolean = False)
'        If ms.Position = ms.Length Then Exit Sub
'        Dim InitialOffset = ms.Position
'        Dim vh = GetVerSubResHead(br)
'        If vh.wValueLength = 0 Then
'            If InsideBlock = False Then
'                Dim EndBlockOffset = vh.wLength + InitialOffset
'                sb.AppendLine($"<{vh.szKey}>")
'            End If
'            ReadBlock(br, ms, sb, True)
'            If InsideBlock = False Then
'                sb.AppendLine($"</{vh.szKey}>")
'            End If
'        Else
'            If vh.wType = 1 Then
'                sb.AppendLine($"<String Key=""{vh.szKey}"" Value=""{Encoding.Unicode.GetString(br.ReadBytes(vh.wValueLength * 2).TrimEnd(2))}"" />")
'            Else
'                sb.AppendLine($"<Var Key=""{vh.szKey}"" Value=""{String.Join(", ", GetWORDArray(br, vh.wValueLength))}"" />")
'            End If
'        End If
'        ms.DWORDAlign
'        ReadBlock(br, ms, sb)
'    End Sub
'    Function GetVersionText$(Bytes() As Byte)
'        Using ms = New IO.MemoryStream(Bytes)
'            Using br = New IO.BinaryReader(ms)
'                Dim sb = New StringBuilder
'                Dim viHead = GetVerSubResHead(br)
'                sb.AppendLine($"<{viHead.szKey}>")
'                ReadFFI(br, sb)
'                Dim Blocks As New Dictionary(Of Long, List(Of String))
'                ReadBlock(br, ms, sb)
'                sb.AppendLine($"</{viHead.szKey}>")
'                Echo(sb.ToString)
'                Return sb.ToString
'            End Using
'        End Using
'    End Function
'    Iterator Function GetWORDArray(br As IO.BinaryReader, Length%) As IEnumerable(Of UShort)
'        If Length Mod 2 <> 0 Then Throw New ArgumentException("Length not divisible by 2")
'        While Length <> 0
'            Length -= 2
'            Yield br.ReadUInt16
'        End While
'    End Function
'End Module
