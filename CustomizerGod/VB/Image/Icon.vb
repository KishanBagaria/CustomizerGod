Imports System
Imports System.Linq
Imports System.Collections.Generic

Module Icon
    Friend Const IconHeaderSize = 6,
                 IconDirEntrySize = 16,
                 IcoCommonEntrySize = 12,
                 GrpIconDirEntrySize = 14
    Function CastByte(Value As Long) As Byte
        If Value > Byte.MaxValue Then Return 0
        Return CByte(Value)
    End Function
    Class GrpIconDir
        Friend GrpIconDirEntries As Byte(),
               IconImages As IEnumerable(Of Byte())
    End Class
    Function GetIconDirEntries(IconDir As Byte(), Size%) As IEnumerable(Of Byte())
        Dim idCount = BitConverter.ToUInt16(IconDir, 4),
            GrpIconDirEntries = New List(Of Byte())
        For i = 0 To idCount - 1
            Dim EntryBytes(Size - 1) As Byte
            Buffer.BlockCopy(IconDir, IconHeaderSize + i * Size, EntryBytes, 0, Size)
            GrpIconDirEntries.Add(EntryBytes)
        Next
        Return GrpIconDirEntries
    End Function
    Function GetIconDirEntry(IconImage As Byte()) As IconDirEntry
        Dim ide As IconDirEntry
        If Not IconImage.Take(8).SequenceEqual(PNG) Then
            ide.bWidth = CastByte(BitConverter.ToInt32(IconImage, 4))
            ide.bHeight = CastByte(BitConverter.ToInt32(IconImage, 8))
            ide.bColorCount = CastByte(BitConverter.ToUInt32(IconImage, 32))
            ide.wPlanes = BitConverter.ToUInt16(IconImage, 12)
            ide.wBitCount = BitConverter.ToUInt16(IconImage, 14)
        Else
            ide.wPlanes = 1
            ide.wBitCount = 32
        End If
        ide.dwBytesInRes = CUInt(IconImage.Length)
        Return ide
    End Function
    Function GetIconImage(IconDir As Byte(), IconDirEntry As Byte()) As Byte()
        Dim dwBytesInRes = BitConverter.ToUInt32(IconDirEntry, 8)
        Dim dwImageOffset = BitConverter.ToUInt32(IconDirEntry, 12)
        Dim IconImage(CInt(dwBytesInRes - 1)) As Byte
        Try
            Buffer.BlockCopy(IconDir, CInt(dwImageOffset), IconImage, 0, CInt(dwBytesInRes))
        Catch e As ArgumentException
            PopupError("Image file is likely corrupted. Please email the file you're using to hi@kishan.info so that I can look into this and fix it.")
        End Try
        Return IconImage
    End Function
    Function CreateIconDirFromIconsData(DirEntries As IEnumerable(Of Byte()), Icons As IEnumerable(Of Byte())) As Byte()
        Dim idCount = DirEntries.Count
        Using ms = New IO.MemoryStream
            ms.Write({0, 0, 1, 0}, 0, 4)                                                  'idReserved, idType
            ms.Write(BitConverter.GetBytes(idCount), 0, 2)                                'idCount
            Dim dwImageOffset = IconHeaderSize + idCount * IconDirEntrySize
            For i = 0 To idCount - 1
                ms.Write(DirEntries(i), 0, IcoCommonEntrySize)
                ms.Write(BitConverter.GetBytes(dwImageOffset), 0, 4)
                dwImageOffset += Icons(i).Length
            Next
            For Each Icon In Icons
                ms.Write(Icon, 0, Icon.Length)                                            'IconImage: icHeader, icColors, icXOR, icAND
            Next
            Return ms.ToArray
        End Using
    End Function
    Function CreateIconDirFromIconData(IconDirEntry As Byte(), IconImage As Byte()) As Byte()
        Dim Size = IconImage.Length
        Dim Bytes(IconHeaderSize + IconDirEntrySize + Size - 1) As Byte
        Bytes(2) = 1                                                                      'idType
        Bytes(4) = 1                                                                      'idCount
        Bytes(18) = IconHeaderSize + IconDirEntrySize                                     'IconDirEntry.dwImageOffset
        Buffer.BlockCopy(IconDirEntry, 0, Bytes, IconHeaderSize, IcoCommonEntrySize)      'IconDirEntry
        Buffer.BlockCopy(IconImage, 0, Bytes, IconHeaderSize + IconDirEntrySize, Size)    'IconImage: icHeader, icColors, icXOR, icAND
        Return Bytes
    End Function
    Function CreateIconDirFromIconImage(IconImage As Byte()) As Byte()
        Return CreateIconDirFromIconData(GetIconDirEntry(IconImage).ToBytes, IconImage)
    End Function
    Function CreateIconDirFromIconImages(Images As IEnumerable(Of Byte())) As Byte()
        Using ms = New IO.MemoryStream
            Using bw = New IO.BinaryWriter(ms)
                bw.Write({0, 0, 1, 0})
                bw.Write(CUShort(Images.Count))
                Dim ImageOffset = 0
                For Each i In Images
                    Dim IDE = GetIconDirEntry(i)
                    IDE.dwImageOffset = CUInt(IconHeaderSize + (Images.Count * IconDirEntrySize) + ImageOffset)
                    ImageOffset += i.Length
                    bw.Write(IDE.ToBytes)
                Next
                For Each i In Images
                    bw.Write(i)
                Next
            End Using
            Return ms.ToArray
        End Using
    End Function
    Function CreateIconDirFromGrpIconDir(SLH As SafeLibraryHandle, GrpIconDir As Byte()) As Byte()
        Dim GrpIconDirEntries = GetIconDirEntries(GrpIconDir, GrpIconDirEntrySize),
            Icons = From gide In GrpIconDirEntries Select CustomizerGod.Resources.GetResBytes(SLH, RT_ICON, "#" & BitConverter.ToUInt16(gide, 12)) 'offset of nID = 12
        Return CreateIconDirFromIconsData(GrpIconDirEntries, Icons)
    End Function
    Function CreateGrpIconDirFromIconDir(IconDir As Byte(), nIDs As UShort()) As GrpIconDir
        Dim IconDirEntries = GetIconDirEntries(IconDir, IconDirEntrySize)
        Dim idCount = CUShort(IconDirEntries.Count)
        If idCount <> nIDs.Length Then Throw New ReportedAppException("idCount != nIDs.Count")
        Dim GrpIconDir((IconHeaderSize - 1) + (idCount * GrpIconDirEntrySize)) As Byte
        Buffer.BlockCopy(IconDir, 0, GrpIconDir, 0, IconHeaderSize)
        Dim IconImages = New List(Of Byte())
        For i = 0 To idCount - 1
            Dim ide = IconDirEntries(i)
            Dim o = IconHeaderSize + (i * GrpIconDirEntrySize)
            Dim nID = BitConverter.GetBytes(nIDs(i))
            Buffer.BlockCopy(ide, 0, GrpIconDir, o, IcoCommonEntrySize)
            Buffer.BlockCopy(nID, 0, GrpIconDir, o + 12, 2)
            IconImages.Add(GetIconImage(IconDir, ide))
        Next
        Return New GrpIconDir With {.GrpIconDirEntries = GrpIconDir, .IconImages = IconImages}
    End Function
    Function ConvertBMPToIconImageDIB(BMP As Byte()) As Byte()
        Return ConvertDIBToIconImageDIB(BMP.Skip(14).ToArray)
    End Function
    Function ConvertDIBToIconImageDIB(DIB As Byte()) As Byte()
        Dim h = BitConverter.ToInt32(DIB, 8)
        Buffer.BlockCopy(BitConverter.GetBytes(h * 2), 0, DIB, 8, 4)
        Return DIB
    End Function
    'Function ConvertDIBIconImageToDIB(IconImage As Byte()) As Byte()
    '    Dim h = BitConverter.ToInt32(IconImage, 8)
    '    Buffer.BlockCopy(BitConverter.GetBytes(h \ 2), 0, IconImage, 8, 4)
    '    Return IconImage
    'End Function
    'Function ConvertDIBIconImageToBMP(IconImage As Byte()) As Byte()
    '    Return PrependBitmapFileHeader(ConvertDIBIconImageToDIB(IconImage))
    'End Function
    'Function ConvertAnyIconImageToBMP(IconImage As Byte()) As Byte()
    '    If GetImageType(IconImage) Is Nothing Then Return ConvertDIBIconImageToBMP(IconImage)
    '    Return IconImage
    'End Function
End Module
