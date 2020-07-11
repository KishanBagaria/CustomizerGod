Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Linq
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
Imports System.Windows.Media

Partial Class MainWindow
    Dim wpResItems As WrapPanel
    Dim MaxResItemWidth%
    Private Sub SetWrapPanelMaxWidth()
        If wpResItems IsNot Nothing Then wpResItems.MaxWidth = Math.Max(lbImageView.ActualWidth - 20, MaxResItemWidth + 20)
    End Sub
    Private Sub wpResItems_Loaded(s As Object, e As RoutedEventArgs)
        wpResItems = DirectCast(s, WrapPanel)
    End Sub
    ReadOnly imgData As New List(Of ResourceItem)
    ReadOnly CursorSW As New Stopwatch
    Dim IsBusy As Boolean = False
    Private Sub ReloadRightPane()
        If Not IsBusy AndAlso lbSidebar.SelectedItem IsNot Nothing Then
            lbSidebar.IsHitTestVisible = False
            lbImageView.Items.Clear()
            AsyncDo(AddressOf ReloadRightPaneStart, AddressOf ReloadRightPaneFinish)
        End If
    End Sub
    Private Sub GetImageResource(SLH As SafeLibraryHandle, Resource As ResourceItem, AppErrors As HashSet(Of String))
        Try
            Resource.Image = Dispatcher.Invoke(Function() CustomizerGod.Resources.GetResBmpSrc(SLH, Resource.Type, Resource.ID, Resource.Format))
            imgData.Add(Resource)
        Catch e As AppException
            AppErrors.Add(e.Message)
        End Try
    End Sub
    Private Sub ReloadRightPaneStart()
        IsBusy = True
        CursorSW.Restart()
        If Not ChangingBMPFormat Then
            For Each i In imgData
                If i.OriginalFormat.HasValue Then i.Format = i.OriginalFormat.Value
            Next
        End If
        imgData.Clear()
        Dim Selected = Dispatcher.Invoke(Function() lbSidebar.SelectedItem)
        If Selected IsNot Nothing Then
            Dim AppErrors = New HashSet(Of String)
            Dim SelectedResList = DirectCast(Selected, ResourceList)
            If SelectedResList.ResItems.Count > 0 Then
                Dim ResFilePath = If(Dispatcher.Invoke(Function() tbPreviewBackup.IsChecked), SelectedResList.FilePath.ToBakPath, SelectedResList.FilePath)
                Using SLH = New SafeLibraryHandle(ResFilePath)
                    Dim IsCursorChanged = False
                    For Each Resource In SelectedResList.ResItems
                        If Not IsCursorChanged AndAlso CursorSW.ElapsedMilliseconds > 100 Then
                            Dispatcher.Invoke(Sub() Mouse.OverrideCursor = Cursors.Wait)
                            IsCursorChanged = True
                        End If
                        GetImageResource(SLH, Resource, AppErrors)
                    Next
                End Using
            End If
            If AppErrors.Count > 0 Then PopupError(String.Join(Environment.NewLine, AppErrors))
        End If
        CursorSW.Stop()
    End Sub
    Private Sub ReloadRightPaneFinish()
        Try
            MaxResItemWidth = 0
            For Each Resource In imgData
                If Resource.Image IsNot Nothing Then
                    Dim w = Resource.Image.PixelWidth
                    If w > MaxResItemWidth Then MaxResItemWidth = w
                    lbImageView.Items.Add(Resource)
                End If
            Next
            SetWrapPanelMaxWidth()
            UpdateStatusBar()
            If Not ChangingBMPFormat Then
                For Each rb As RadioButton In scBMPFormat.Children
                    rb.IsChecked = False
                Next
                scBMPFormat.IsEnabled = (lbImageView.Items.Count > 0) AndAlso DirectCast(lbImageView.Items(0), ResourceItem).Format.ToString.Contains("BMP")
                If lbImageView.Items.Count > 0 Then
                    Dim FirstResource = DirectCast(lbImageView.Items(0), ResourceItem)
                    If FirstResource.Format.ToString.Contains("BMP") Then
                        DirectCast(scBMPFormat.Children(0), RadioButton).IsChecked = True
                    End If
                End If
            End If
        Finally
            IsBusy = False
            ChangingBMPFormat = False
            lbSidebar.IsHitTestVisible = True
            Mouse.OverrideCursor = Nothing
        End Try
    End Sub
    Shared ReadOnly BMPFormats As ResFormat() = {ResFormat.RgbBMP, ResFormat.ArgbBMP, ResFormat.PArgbBMP}
    Shared ChangingBMPFormat As Boolean = False
    Private Sub scBMPFormat_Click(sc As RadioButton, e As RoutedEventArgs)
        Dim ResList = GetSelectedResList()
        If ResList IsNot Nothing Then
            Dim Index = scBMPFormat.Children.IndexOf(sc)
            ChangingBMPFormat = True
            If Index = 0 Then
                For Each i In ResList.ResItems
                    If i.OriginalFormat.HasValue Then i.Format = i.OriginalFormat.Value
                Next
            Else
                Dim NewFormat = BMPFormats(Index - 1)
                For Each i In ResList.ResItems
                    If Not i.OriginalFormat.HasValue Then i.OriginalFormat = i.Format
                    i.Format = NewFormat
                Next
            End If
            ReloadRightPane()
        End If
    End Sub
    Private Sub ResItem_MouseDown(s As Object, e As MouseButtonEventArgs)
        If e.ClickCount = 2 Then Call (New ImagePreview With {.Owner = Me}).Show()
    End Sub
    Private Sub tbColor_Click(tb As RadioButton, e As RoutedEventArgs)
        Dim b = DirectCast(tb.Content, Border)
        gRight.Background = b.Background
        lbImageView.Foreground = b.OpacityMask
        If b.Child IsNot Nothing Then
            DirectCast(FindResource("sbScrollingBackground"), Animation.Storyboard).Begin(gRight)
        End If
    End Sub
    Private Sub UpdateStatusBar() Handles lbImageView.SelectionChanged
        If lbImageView.Items.Count = 1 Then
            tbStatus.Text = "One item"
        ElseIf lbImageView.SelectedItems.Count > 0 Then
            tbStatus.Text = $"{lbImageView.SelectedItems.Count}/{lbImageView.Items.Count} selected"
        Else
            tbStatus.Text = $"{lbImageView.Items.Count} items"
        End If
    End Sub
End Class