Imports System
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Linq
Imports System.Runtime.CompilerServices
Imports System.Windows.Media.Imaging

Module Image
    Friend ReadOnly BMP As Byte() = {66, 77},
                    GIF As Byte() = {71, 73, 70, 56},
                    ICO As Byte() = {0, 0, 1, 0},
                    JPG As Byte() = {255, 216, 255},
                    PNG As Byte() = {137, 80, 78, 71, 13, 10, 26, 10}
    Function GetImageType(File As Byte()) As ImageFormat
        If File.Take(2).SequenceEqual(BMP) Then
            Return ImageFormat.Bmp
        ElseIf File.Take(4).SequenceEqual(GIF) Then
            Return ImageFormat.Gif
        ElseIf File.Take(4).SequenceEqual(ICO) Then
            Return ImageFormat.Icon
        ElseIf File.Take(3).SequenceEqual(JPG) Then
            Return ImageFormat.Jpeg
        ElseIf File.Take(8).SequenceEqual(PNG) Then
            Return ImageFormat.Png
        End If
        Return Nothing
    End Function
    Function GetImageSize(File As Byte(), Optional Type As ImageFormat = Nothing) As Size
        If Type Is Nothing Then Type = GetImageType(File)
        Select Case Type.Guid
            Case ImageFormat.Bmp.Guid
                Return New Size(BitConverter.ToInt32(File, 18), BitConverter.ToInt32(File, 22))
            Case ImageFormat.Gif.Guid
                Return New Size(BitConverter.ToUInt16(File, 6), BitConverter.ToUInt16(File, 8))
            Case ImageFormat.Png.Guid
                Return New Size(BitConverter.ToInt32({File(19), File(18), File(17), File(16)}, 0),
                                BitConverter.ToInt32({File(23), File(22), File(21), File(20)}, 0))
            Case ImageFormat.Jpeg.Guid
                Return GetImageSizeWIC(File)
        End Select
        Return New Size(-1, -1)
    End Function
    Function GetImageSizeWIC(File As Byte()) As Size
        Using ms = New IO.MemoryStream(File)
            Dim bs = BitmapFrame.Create(ms)
            Return New Size(bs.PixelWidth, bs.PixelHeight)
        End Using
    End Function
    Function GetImageSizeGDI(File As Byte()) As Size
        Using p = Picture.FromBytes(File)
            Return p.Size
        End Using
    End Function
    Function PrependBitmapFileHeader(DIB() As Byte) As Byte()
        If DIB IsNot Nothing Then
            If Not DIB.Take(2).SequenceEqual(BMP) Then
                Dim biSize = BitConverter.ToUInt32(DIB, 0)                          'biSize
                Dim Bytes(13 + DIB.Length) As Byte                                  'BITMAPFILEHEADER + (BITMAPINFOHEADER + RGBQUAD = BITMAPINFO)
                Bytes(0) = 66                                                       'bfType(0) = B
                Bytes(1) = 77                                                       'bfType(1) = M
                BitConverter.GetBytes(Bytes.Length).CopyTo(Bytes, 2)                'bfSize
                Dim bfOffBits = 14 + biSize
                If bfOffBits < Bytes.Length Then
                    BitConverter.GetBytes(bfOffBits).CopyTo(Bytes, 10)
                End If
                DIB.CopyTo(Bytes, 14)
                Return Bytes
            Else
                Throw New AppException(PopupStr.BitmapFileHeader_Already_Present)
            End If
        End If
        Return DIB
    End Function
    <Extension> Function ToBitmapSource(ms As IO.MemoryStream) As BitmapSource
        Using ms
            Dim bi = New BitmapImage
            bi.BeginInit()
            bi.CreateOptions = BitmapCreateOptions.PreservePixelFormat
            bi.CacheOption = BitmapCacheOption.OnLoad
            bi.StreamSource = ms
            bi.EndInit()
            bi.Freeze()
            Return bi
        End Using
    End Function
    <Extension> Function ToBitmapSource(Bytes As Byte()) As BitmapSource
        Return New IO.MemoryStream(Bytes).ToBitmapSource
    End Function
End Module
