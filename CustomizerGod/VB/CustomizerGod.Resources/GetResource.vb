Imports System
Imports System.Runtime.InteropServices
Imports System.Windows.Interop
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Collections.Generic
Imports System.Runtime.CompilerServices
Imports System.Text
Imports System.Linq
Namespace Resources
    Module GetResource
        Function GetResBytes(SLH As SafeLibraryHandle, ResType$, ResID$, Optional ErrOnNotFound As Boolean = True) As Byte()
            Dim FindHandle = FindResource(SLH, ResID, ResType)
            If FindHandle <> IntPtr.Zero Then
                Dim Size = SizeofResource(SLH, FindHandle)
                If Size <> 0 Then
                    Dim LoadHandle = LoadResource(SLH, FindHandle)
                    If LoadHandle <> IntPtr.Zero Then
                        Dim LockHandle = LockResource(LoadHandle)
                        If LockHandle <> IntPtr.Zero Then
                            Dim Bytes(CInt(Size) - 1) As Byte
                            Marshal.Copy(LockHandle, Bytes, 0, Bytes.Length)
                            Return Bytes
                        Else
                            Throw New NativeException("LockResource " & ResID)
                        End If
                    Else
                        Throw New NativeException("LoadResource " & ResID)
                    End If
                Else
                    Throw New NativeException("SizeofResource " & ResID)
                End If
            Else
                If ErrOnNotFound Then Throw New NativeException("FindResource " & ResID)
            End If
            Return Nothing
        End Function
        Function GetCompatibleResBytes(SLH As SafeLibraryHandle, ResType$, ResID$) As Byte()
            Dim ResBytes = GetResBytes(SLH, ResType, ResID)
            Select Case ResType
                Case RT_BITMAP
                    Return PrependBitmapFileHeader(ResBytes)
                Case RT_GROUP_ICON
                    Return CreateIconDirFromGrpIconDir(SLH, ResBytes)
            End Select
            Return ResBytes
        End Function
        Function GetResBmpSrc(SLH As SafeLibraryHandle, ResType$, ResID$, ResFormat As ResFormat) As BitmapSource
            Select Case ResFormat
                Case ResFormat.PArgbBMP
                    Return GetPArgbBitmapBmpSrc(SLH, ResID)
                Case ResFormat.ArgbBMP
                    Return GetBitmapBmpSrcArgb(SLH, ResID)
                Case ResFormat.ICO
                    Return GetIcoBmpSrc(SLH, ResID)
                Case Else
                    Try
                        Return GetCompatibleResBytes(SLH, ResType, ResID).ToBitmapSource
                    Catch e As IO.FileFormatException
                        Throw New AppException(StrFormat(PopupStr.Unknown_Image_Format, $"{ResType}\{ResID}"))
                    End Try
            End Select
            Return Nothing
        End Function
        Function GetPArgbBitmapBmpSrc(SLH As SafeLibraryHandle, ResID$) As BitmapSource
            Dim DibBytes = GetResBytes(SLH, RT_BITMAP, ResID)
            Dim biSize = BitConverter.ToInt32(DibBytes, 0),
            biWidth = BitConverter.ToInt32(DibBytes, 4),
            biHeight = BitConverter.ToInt32(DibBytes, 8),
            bpp = BitConverter.ToUInt16(DibBytes, 14) \ 8,
            biHeightAbs = Math.Abs(biHeight),
            s = biWidth * bpp
            If biWidth + biHeightAbs > Short.MaxValue OrElse bpp <> 4 Then Return GetBitmapBmpSrcArgb(SLH, ResID)
            Dim wb = New WriteableBitmap(biWidth, biHeightAbs, 0, 0, PixelFormats.Pbgra32, Nothing)
            If biHeight > 0 Then
                For i = 0 To biHeight - 1
                    Marshal.Copy(DibBytes, biSize + (i * s), wb.BackBuffer + ((biHeight - 1 - i) * s), s)
                Next
            Else
                Marshal.Copy(DibBytes, biSize, wb.BackBuffer, biHeightAbs * s)
            End If
            wb.Lock()
            wb.AddDirtyRect(New Windows.Int32Rect(0, 0, biWidth, biHeightAbs))
            wb.Unlock()
            Return wb
        End Function
        Function GetBitmapBmpSrcArgb(SLH As SafeLibraryHandle, ResID$) As BitmapSource
            Dim hBmp = LoadImage(SLH, ResID, LoadImageType.IMAGE_BITMAP, 0, 0, LoadImageOptions.LR_DEFAULTCOLOR)
            If hBmp = IntPtr.Zero Then Throw New NativeException("LoadImage " & ResID)
            Try
                Return Imaging.CreateBitmapSourceFromHBitmap(hBmp, Nothing, Nothing, Nothing)
            Finally
                DeleteObject(hBmp)
            End Try
        End Function
        Function GetIcoBmpSrc(SLH As SafeLibraryHandle, ResID$) As BitmapSource
            Dim hIcon = LoadImage(SLH, ResID, LoadImageType.IMAGE_ICON, IconDisplayWidth, IconDisplayHeight, LoadImageOptions.LR_DEFAULTSIZE)
            If hIcon = IntPtr.Zero Then Throw New NativeException("LoadImage " & ResID)
            Try
                Return Imaging.CreateBitmapSourceFromHIcon(hIcon, Nothing, Nothing)
            Finally
                DestroyIcon(hIcon)
            End Try
        End Function
        'Iterator Function GetStringTableResources(SLH As SafeLibraryHandle, ResID$) As IEnumerable(Of StrOrMsgTableItem)
        '    Dim StringID = (CUShort(ResID.Substring(1)) * 16) - 16
        '    Dim Bytes = GetResBytes(SLH, RT_STRING, ResID)
        '    Using ms = New IO.MemoryStream(Bytes)
        '        Using br = New IO.BinaryReader(ms)
        '            While ms.Position <> ms.Length
        '                Yield New StrOrMsgTableItem With {.ID = CStr(StringID), .Text = br.ReadPUTF16}
        '                StringID += 1
        '            End While
        '        End Using
        '    End Using
        'End Function
        'Iterator Function GetMessageTableResources(SLH As SafeLibraryHandle, ResID$) As IEnumerable(Of StrOrMsgTableItem)
        '    Dim Bytes = GetResBytes(SLH, RT_MESSAGETABLE, ResID)
        '    Using ms = New IO.MemoryStream(Bytes)
        '        Using br = New IO.BinaryReader(ms)
        '            Dim NumberOfBlocks = br.ReadUInt32
        '            Dim mrbs = New List(Of MessageResourceBlock)
        '            For i = 1UI To NumberOfBlocks
        '                Dim mrb = br.ReadBytes(3 * 4).ToStructure(Of MessageResourceBlock)
        '                mrbs.Add(mrb)
        '            Next
        '            For Each mrb In mrbs
        '                ms.Position = mrb.OffsetToEntries
        '                Dim Length = br.ReadUInt16
        '                Dim Flags = br.ReadUInt16
        '                Dim TextEncoding = If(Flags = 1, Encoding.Unicode, Encoding.ASCII)
        '                For i = mrb.LowId To mrb.HighId
        '                    Dim TextBytes = br.ReadBytes(Length)
        '                    Dim Text = TextEncoding.GetString(TextBytes)
        '                    '
        '                    Dim DelimiterIndex = Text.LastIndexOf(Convert.ToChar(10)) + 1
        '                    Dim TextKnown = Text.Remove(DelimiterIndex)
        '                    Dim TextUnknown = Text.Substring(DelimiterIndex)
        '                    Text = TextKnown & BitConverter.ToString(TextEncoding.GetBytes(TextUnknown))
        '                    '
        '                    Yield New StrOrMsgTableItem With {.ID = "0x" & i.ToString("x"), .Text = Text}
        '                Next
        '            Next
        '        End Using
        '    End Using
        'End Function
        'Iterator Function GetAcceleratorResources(SLH As SafeLibraryHandle, ResID$) As IEnumerable(Of AccelTableEntry)
        '    Dim Bytes = GetResBytes(SLH, RT_ACCELERATORS, ResID)
        '    Using ms = New IO.MemoryStream(Bytes)
        '        Using br = New IO.BinaryReader(ms)
        '            While ms.Position <> ms.Length
        '                Yield br.ReadBytes(8).ToStructure(Of AccelTableEntry)
        '            End While
        '        End Using
        '    End Using
        'End Function
    End Module
End Namespace