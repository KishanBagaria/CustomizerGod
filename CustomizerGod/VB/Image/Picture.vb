Imports System
Imports System.Linq
Imports System.Drawing
Imports System.Text.RegularExpressions
Imports System.Runtime.InteropServices

Class Picture : Implements IDisposable
    Dim BMP As Bitmap
    ReadOnly Property BitCount%
        Get
            Return Bitmap.GetPixelFormatSize(BMP.PixelFormat)
        End Get
    End Property
    ReadOnly Property Size As Size
        Get
            Return BMP.Size
        End Get
    End Property
    Sub New(BMP As Bitmap)
        Me.BMP = BMP
    End Sub
    Sub Dispose() Implements IDisposable.Dispose
        BMP.Dispose()
    End Sub
    Shared Function FromStream(Stream As IO.Stream) As Picture
        Return New Picture(New Bitmap(Stream))
    End Function
    Shared Function FromBytes(Bytes() As Byte) As Picture
        Dim MS = New IO.MemoryStream(Bytes)
        Return Picture.FromStream(MS)
    End Function
    Function GetResizedCopy(NewWidth%, NewHeight%, ResizeMode As ResizeMode, InterpolationMode As Drawing2D.InterpolationMode) As Picture
        Dim RatioWidth = NewWidth / BMP.Width
        Dim RatioHeight = NewHeight / BMP.Height
        Dim Ratio = If(RatioHeight < RatioWidth, RatioHeight, RatioWidth)

        Dim x, y, w, h As Integer
        Select Case ResizeMode
            Case ResizeMode.Fit
                w = CInt(BMP.Width * Ratio)
                h = CInt(BMP.Height * Ratio)
            Case ResizeMode.Crop
                If RatioHeight > RatioWidth Then Ratio = RatioHeight Else Ratio = RatioWidth
                w = CInt(BMP.Width * Ratio)
                h = CInt(BMP.Height * Ratio)
                If w > NewWidth Then x = -((w - NewWidth) \ 2)
                If h > NewHeight Then y = -((h - NewHeight) \ 2)
            Case ResizeMode.Stretch
                w = NewWidth
                h = NewHeight
            Case ResizeMode.Center
                w = CInt(BMP.Width * Ratio)
                h = CInt(BMP.Height * Ratio)
                If w < NewWidth Then x = (NewWidth - w) \ 2
                If h < NewHeight Then y = (NewHeight - h) \ 2
        End Select
        Dim NewImage = New Bitmap(NewWidth, NewHeight)
        Using g As Graphics = Graphics.FromImage(NewImage)
            g.CompositingMode = Drawing2D.CompositingMode.SourceCopy
            g.CompositingQuality = Drawing2D.CompositingQuality.HighQuality
            g.InterpolationMode = InterpolationMode
            g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
            Using attr = New Imaging.ImageAttributes
                attr.SetWrapMode(Drawing2D.WrapMode.TileFlipXY)
                g.DrawImage(BMP, New Rectangle(x, y, w, h), 0, 0, BMP.Width, BMP.Height, GraphicsUnit.Pixel, attr)
            End Using
        End Using
        Return New Picture(NewImage)
    End Function
    Function ToBytes(Format As Imaging.ImageFormat) As Byte()
        Using ms = New IO.MemoryStream
            BMP.Save(ms, Format)
            Return ms.ToArray
        End Using
    End Function
    Function ToIconBytes(Width%, Height%) As Byte()
        Using Resized = GetResizedCopy(Width, Height, ImageResizingMode, ImageResamplingMode)
            If Width > 255 OrElse Height > 255 Then
                Return Resized.ToBytes(Imaging.ImageFormat.Png)
            Else
                Return ConvertBMPtoIconImageDIB(Resized.ToBytes(Imaging.ImageFormat.Bmp))
            End If
        End Using
    End Function
    Sub SetPixelFormat(PixelFormat As Imaging.PixelFormat)
        Dim BmpData As Imaging.BitmapData = BMP.LockBits(New Rectangle(Point.Empty, BMP.Size), Imaging.ImageLockMode.ReadOnly, BMP.PixelFormat)
        Dim NewBMP = New Bitmap(BMP.Width, BMP.Height, BmpData.Stride, PixelFormat, BmpData.Scan0)
        BMP.UnlockBits(BmpData)
        'Don't call BMP.Dispose()
        BMP = NewBMP
    End Sub
    Sub PremultiplyAlpha()
        Dim BMPData = BMP.LockBits(New Rectangle(Point.Empty, BMP.Size), Imaging.ImageLockMode.ReadWrite, BMP.PixelFormat)
        For p = 0 To Math.Abs(BMPData.Height * BMPData.Stride) - 1 Step 4
            Dim o = BMPData.Scan0
            Dim ad = Marshal.ReadByte(o + p + 3) / 255
            For i = 0 To 2
                Marshal.WriteByte(o + p + i, CByte(Marshal.ReadByte(o + p + i) * ad))
            Next
        Next
        BMP.UnlockBits(BMPData)
    End Sub
    Sub RemoveTransparency()
        Dim NewImg = New Bitmap(BMP.Width, BMP.Height)
        Using g = Graphics.FromImage(NewImg)
            g.Clear(Color.White)
            g.DrawImage(BMP, New Rectangle(Point.Empty, BMP.Size))
        End Using
        BMP.Dispose()
        BMP = NewImg
    End Sub
End Class
Enum ResizeMode
    Fit
    Crop
    Stretch
    Center
End Enum
