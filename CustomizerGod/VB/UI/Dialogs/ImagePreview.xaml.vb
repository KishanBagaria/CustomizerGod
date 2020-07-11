Imports System
Imports System.Linq
Imports System.Windows
Imports System.Windows.Interop
Imports System.Windows.Controls
Imports System.Collections.Generic
Imports System.Windows.Input

Class ImagePreview
    Dim wpResItems As WrapPanel, MaxResItemWidth%
    Private Sub wpResItems_Loaded(s As WrapPanel, e As RoutedEventArgs)
        wpResItems = s
    End Sub
    Private Sub SetWrapPanelMaxWidth()
        If wpResItems IsNot Nothing Then wpResItems.MaxWidth = Math.Max(lbResPreview.ActualWidth - 20, MaxResItemWidth + 20)
    End Sub
    Sub New()
        InitializeComponent()
        AddHandler MouseLeftButtonDown, Sub() DragMove()
        MaxWidth = SystemParameters.WorkArea.Width
        MaxHeight = SystemParameters.WorkArea.Height
        lbResPreview.Background = mw.gRight.Background
        lbResPreview.Foreground = mw.lbImageView.Foreground
        Dim ResList = mw.GetSelectedResList,
            ResFilePath = ResList.FilePath
        If mw.tbPreviewBackup.IsChecked Then ResFilePath = ResFilePath.ToBakPath
        Dim ResPreviews = New List(Of ResPreview)
        Using SLH = New SafeLibraryHandle(ResFilePath)
            For Each ResItem As ResourceItem In mw.lbImageView.SelectedItems
                If ResItem.Type <> RT_GROUP_ICON Then
                    For Each ID In ResItem.IDs
                        Dim Img = CustomizerGod.Resources.GetResBmpSrc(SLH, ResItem.Type, ID, ResItem.Format)
                        ResPreviews.Add(New ResPreview With {.Name = ID, .Image = Img, .ToolTip = Img.PixelWidth & "x" & Img.PixelHeight})
                    Next
                Else
                    Dim GrpIconDir = CustomizerGod.Resources.GetResBytes(SLH, ResItem.Type, ResItem.ID)
                    For Each i In GetIconDirEntries(GrpIconDir, 14)
                        Dim gide = i.ToStructure(Of GrpIconDirEntry)()
                        Dim Name = "#" & gide.nID
                        Dim IconImage = CustomizerGod.Resources.GetResBytes(SLH, RT_ICON, Name)
                        Dim Img = CreateIconDirFromIconImage(IconImage).ToBitmapSource
                        ResPreviews.Add(New ResPreview With {.Image = Img, .ToolTip = $"{Img.PixelWidth}x{Img.PixelHeight} ({gide.wBitCount}bpp)"})
                    Next
                End If
            Next
        End Using
        For Each ResPreview In ResPreviews.Where(Function(rp) rp.Image IsNot Nothing).OrderBy(Function(rp) rp.Image.PixelHeight)
            Dim iw = ResPreview.Image.PixelWidth
            If iw > MaxResItemWidth Then MaxResItemWidth = iw
            lbResPreview.Items.Add(ResPreview)
        Next
        AddHandler SizeChanged, Sub() SetWrapPanelMaxWidth()
    End Sub
    Private Sub Window_SourceInitialized() Handles Me.SourceInitialized
        If lbResPreview.Background Is Nothing Then
            Dim WindowHandle = New WindowInteropHelper(Me).Handle
            If WindowsV >= 100 Then
                lbResPreview.Margin = New Thickness(0, 25, 0, 0)
                Dim __ = New UI.Titleless(Me, WindowHandle, 25, False)
            Else
                If UI.Titleless.IsDwmEnabled Then
                    Dim hWnd = HwndSource.FromHwnd(WindowHandle)
                    hWnd.CompositionTarget.BackgroundColor = Media.Colors.Transparent
                    UI.Titleless.SetTransparentBackground(Me, WindowHandle)
                End If
            End If
        End If
        SetWrapPanelMaxWidth()
        Me.SizeToContent = SizeToContent.WidthAndHeight
        CenterWindow(Me)
    End Sub
    Private Sub Window_KeyDown(s As Object, e As KeyEventArgs) Handles Me.PreviewKeyDown
        If Keyboard.IsKeyDown(Key.LeftCtrl) OrElse Keyboard.IsKeyDown(Key.RightCtrl) Then
            If e.Key = Key.T Then
                PopupMessage(lbResPreview.Items.Count.ToString)
            End If
        End If
    End Sub
    Shared Sub CenterWindow(Window As Window)
        Dim ScreenWidth = SystemParameters.PrimaryScreenWidth
        Dim ScreenHeight = SystemParameters.PrimaryScreenHeight
        Dim WindowWidth = Window.ActualWidth
        Dim WindowHeight = Window.ActualHeight
        Window.Left = (ScreenWidth / 2) - (WindowWidth / 2)
        Window.Top = (ScreenHeight / 2) - (WindowHeight / 2)
    End Sub
End Class
