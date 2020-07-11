Imports System.Runtime.InteropServices
Imports System.Runtime.CompilerServices

Module NativeStructs
    Structure BitmapInfoHeader
        Dim biSize As UInteger,
            biWidth, biHeight As Integer,
            biPlanes, biBitCount As UShort,
            biCompression, biSizeImage As UInteger,
            biXPelsPerMeter, biYPelsPerMeter As Integer,
            biClrUsed, biClrImportant As UInteger
    End Structure
    <StructLayout(LayoutKind.Sequential, Pack:=1)> Structure GrpIconDirEntry
        Dim bWidth, bHeight, bColorCount, bReserved As Byte,
            wPlanes, wBitCount As UShort,
            dwBytesInRes As UInteger,
            nID As UShort
    End Structure
    <StructLayout(LayoutKind.Sequential, Pack:=1)> Structure IconDirEntry
        Dim bWidth, bHeight, bColorCount, bReserved As Byte,
            wPlanes, wBitCount As UShort,
            dwBytesInRes, dwImageOffset As UInteger
    End Structure
    <StructLayout(LayoutKind.Explicit, Size:=8)> Structure LargeInteger
        <FieldOffset(0)> Public QuadPart As Long
        <FieldOffset(0)> Public LowPart As UInteger
        <FieldOffset(4)> Public HighPart As Integer
    End Structure
    Structure AccelTableEntry
        Dim fFlags, wAnsi, wId, padding As UShort
    End Structure
    Structure MessageResourceBlock
        Dim LowId, HighId, OffsetToEntries As UInteger
    End Structure
    Structure VS_FixedFileInfo
        Dim dwSignature As UInteger        ' signature - always 0xfeef04bd
        Dim dwStrucVersion As UInteger     ' structure version - currently 0
        Dim dwFileVersionMS As UInteger    ' Most Significant file version dword
        Dim dwFileVersionLS As UInteger    ' Least Significant file version dword
        Dim dwProductVersionMS As UInteger ' Most Significant product version
        Dim dwProductVersionLS As UInteger ' Least Significant product version
        Dim dwFileFlagMask As UInteger     ' file flag mask
        Dim dwFileFlags As FileFlags       ' debug/retail/prerelease/...
        Dim dwFileOS As FileOS             ' OS type.  Will always be Windows32 value
        Dim dwFileType As FileType         ' Type of file (dll/exe/drv/... )
        Dim dwFileSubtype As FileSubtype   ' file subtype
        Dim dwFileDateMS As UInteger       ' Most Significant part of date
        Dim dwFileDateLS As UInteger       ' Least Significant part of date
    End Structure
    Structure ResourceHeader
        Dim DataSize As UInteger
        Dim HeaderSize As UInteger
        Dim Type As String
        Dim Name As String
        Dim DataVersion As UInteger
        Dim MemoryFlags As MemoryFlags
        Dim LanguageId As UShort
        Dim Version As UInteger
        Dim Characteristics As UInteger
    End Structure
    Structure Margins
        Dim cxLeftWidth, cxRightWidth, cyTopHeight, cyBottomHeight As Integer
    End Structure
    Structure Rect
        Dim Left, Top, Right, Bottom As Integer
    End Structure
    <Extension> Function ToBytes(Of T As Structure)(Struct As T) As Byte()
        Dim Size = Marshal.SizeOf(Struct)
        Dim Bytes(Size - 1) As Byte
        Dim Pointer = Marshal.AllocHGlobal(Size)
        Marshal.StructureToPtr(Struct, Pointer, True)
        Marshal.Copy(Pointer, Bytes, 0, Size)
        Marshal.FreeHGlobal(Pointer)
        Return Bytes
    End Function
    <Extension> Function ToStructure(Of T As Structure)(Bytes As Byte()) As T
        Dim Struct As T = Nothing
        Dim Size = Marshal.SizeOf(Struct)
        Dim Pointer = Marshal.AllocHGlobal(Size)
        Marshal.Copy(Bytes, 0, Pointer, Size)
        Struct = DirectCast(Marshal.PtrToStructure(Pointer, Struct.GetType), T)
        Marshal.FreeHGlobal(Pointer)
        Return Struct
    End Function
End Module
