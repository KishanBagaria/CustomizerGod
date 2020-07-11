Imports System.Collections.Generic
Imports System.Drawing.Imaging
Imports System.Runtime.CompilerServices
Imports System.Windows
Imports System.Windows.Media.Imaging

Public Class ResourceList
    Property DisplayText As String
    Property FilePath As String
    Property OriginalFilePath As String
    Property Restart As RestartMode
    Property [ReadOnly] As Boolean
    Property ResItems As IEnumerable(Of ResourceItem)
    Property ToolTip As String
    ReadOnly Property AsideText As String
        Get
            Return If([ReadOnly], " Read Only", "")
        End Get
    End Property
End Class
Public Class ResourceItem
    ReadOnly Property ID As String
        Get
            Return IDs(0)
        End Get
    End Property
    Property IDs As String()
    Property FriendlyName As String
    Property Type As String
    Property Format As ResFormat
    Property OriginalFormat As ResFormat?
    Property Image As BitmapSource
    Property Resize As Boolean
    ReadOnly Property NameVisibility As Visibility
        Get
            Return If(FriendlyName Is Nothing, Visibility.Collapsed, Visibility.Visible)
        End Get
    End Property
    ReadOnly Property ToolTip$
        Get
            Dim l = New List(Of String) From {"ID: " & String.Join(", ", IDs),
                                              $"Type: {Format} "}
            If Format <> OriginalFormat Then l.Add($"Original Type: {OriginalFormat}")
            If Image IsNot Nothing AndAlso Format <> ResFormat.ICO Then
                l.AddRange({$"Dimensions: {Image.PixelWidth}x{Image.PixelHeight}",
                            "Fixed Size: " & If(Resize, "Yes", "No")})
            End If
            Return String.Join(DoubleNewLine, l)
        End Get
    End Property
End Class
Public Class ResPreview
    Property Image As BitmapSource
    Property Name As String
    Property ToolTip As String
    ReadOnly Property NameVisibility As Visibility
        Get
            Return If(Name Is Nothing, Visibility.Collapsed, Visibility.Visible)
        End Get
    End Property
End Class
Public Module Enums
    Enum RestartMode
        None
        Explorer
        User
        System
    End Enum
    Enum ResFormat
        Unknown
        BMP
        PArgbBMP
        ArgbBMP
        RgbBMP
        PNG
        GIF
        JPG
        ICO
    End Enum
    <Extension> Function GetFileExtension$(Format As ResFormat)
        If Format.ToString.Contains("BMP") Then Return "bmp"
        Return Format.ToString.ToLowerInvariant
    End Function
    <Extension> Function ToImageFormat(Format As ResFormat) As ImageFormat
        Select Case Format
            Case ResFormat.PArgbBMP, ResFormat.ArgbBMP, ResFormat.RgbBMP, ResFormat.BMP
                Return ImageFormat.Bmp
            Case ResFormat.ICO
                Return ImageFormat.Icon
            Case ResFormat.PNG
                Return ImageFormat.Png
            Case ResFormat.GIF
                Return ImageFormat.Gif
            Case ResFormat.JPG
                Return ImageFormat.Jpeg
        End Select
        Return Nothing
    End Function
    <Extension> Function ToResFormat(Format As ImageFormat) As ResFormat
        Select Case Format.Guid
            Case ImageFormat.Bmp.Guid
                Return ResFormat.BMP
            Case ImageFormat.Icon.Guid
                Return ResFormat.ICO
            Case ImageFormat.Png.Guid
                Return ResFormat.PNG
            Case ImageFormat.Gif.Guid
                Return ResFormat.GIF
            Case ImageFormat.Jpeg.Guid
                Return ResFormat.JPG
        End Select
        Return Nothing
    End Function
End Module
