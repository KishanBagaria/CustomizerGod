Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Interop
Imports System.Windows.Media

NotInheritable Class MainWindow
    ReadOnly AskedForLogOff As New HashSet(Of String),
        DefaultMinHeight#, ExtraHeight%
    Friend WindowHandle As IntPtr
    Dim IsResListSelected As Boolean
    Function GetSelectedResList() As ResourceList
        Return DirectCast(lbSidebar.SelectedItem, ResourceList)
    End Function
    Private Sub Restart(ResList As ResourceList)
        Select Case ResList.Restart
            Case RestartMode.Explorer
                If tbAutoRestartExplorer.IsChecked Then RestartExplorer(True)
            Case RestartMode.User
                If AskedForLogOff.Add(ResList.OriginalFilePath) Then
                    If Question(PopupStr.Ask_For_Logoff) Then ExitWindowsEx(ExitWindowsExFlags.EWX_LOGOFF, 0)
                End If
            Case RestartMode.System
                AskForRestart()
        End Select
    End Sub
    Sub AddHandlers()
        AddHandler bRestartExplorer.Click, Sub() AsyncDo(bRestartExplorer, spExplorer, "Restarting...", AddressOf RestartExplorer)
        AddHandler bRunExplorer.Click, Sub() AsyncDo(bRunExplorer, spExplorer, "Running...", AddressOf RunExplorer)
        AddHandler bExitExplorer.Click, Sub() AsyncDo(bExitExplorer, spExplorer, "Exiting...", AddressOf ExitExplorer)
        AddHandler bForceCloseExplorer.Click, Sub() AsyncDo(bForceCloseExplorer, spExplorer, "Closing...", AddressOf ForceCloseExplorer)
        AddHandler bClearIconCache.Click, Sub() AsyncDo(bClearIconCache, spIconCache, "Clearing...", AddressOf QuickClearIconCache)
        AddHandler bFullClearIconCache.Click, Sub() AsyncDo(bFullClearIconCache, spIconCache, "Clearing...", AddressOf FullClearIconCache)
        AddHandler tbPreviewBackup.Click, Sub() ReloadRightPane()
        AddHandler bRestoreAll.Click, Sub() SFCRestoreAll()
        AddHandler bFeedback.Click, Sub() Call (New Feedback).ShowDialog()
        AddHandler bAbout.Click, Sub() Call (New About).ShowDialog()
        AddHandler hld2w.Click, Sub() OpenURL(d2w)
        AddHandler StateChanged, Sub() dpContainer.Margin = New Thickness(If(WindowState = WindowState.Maximized, 8, 0))
        AddHandler SizeChanged, Sub(s, e) If e.WidthChanged Then SetWrapPanelMaxWidth()
        AddHandler cbResizeMode.SelectionChanged, Sub() ImageResizingMode = cbResizeMode.SelectedValue.ToString.ToEnum(Of ResizeMode)
        AddHandler cbInterpolation.SelectionChanged, Sub() ImageResamplingMode = cbInterpolation.SelectedValue.ToString.ToEnum(Of System.Drawing.Drawing2D.InterpolationMode)
    End Sub
    Sub New()
        InitializeComponent()
        If WindowsV >= 100 Then
            Me.MinHeight = 555
            bMiddle.Margin = New Thickness(0)
        ElseIf WindowsV >= 62 Then
            Me.MinHeight = 614
        End If
        DefaultMinHeight = Me.MinHeight
        ExtraHeight = CInt(bStash.Tag)
        Select Case WindowsV
            Case 100
                PopulateFromXML(Data10)
            Case 63
                PopulateFromXML(Data8)
            Case 62
                PopupMessage(PopupStr.Windows_Incompatible & DoubleNewLine & PopupStr.Windows_8_Incompatible)
                lbSidebar_SelectedItemChanged()
            Case 61
                PopulateFromXML(Data7)
            Case Else
                PopupMessage(PopupStr.Windows_Incompatible)
                lbSidebar_SelectedItemChanged()
        End Select
        AddHandlers()
        PopulateFromXML()
#If DEBUG Then
        'PopulateFromFile(Sys32DirPath & "\explorerframe.dll")
#End If
    End Sub
    Private Sub Window_SourceInitialized() Handles Me.SourceInitialized
        WindowHandle = New WindowInteropHelper(Me).Handle
        Dim __ = New UI.Titleless(Me, WindowHandle, 68)
        AsyncDo(AddressOf TakeBackups, AddressOf FinishTakeBackups)
    End Sub
    Private Sub Window_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.PreviewKeyDown
        If Keyboard.IsKeyDown(Key.LeftCtrl) OrElse Keyboard.IsKeyDown(Key.RightCtrl) Then
            If e.Key = Key.O Then
                OpenFile()
            ElseIf e.Key = Key.C Then
                Dim ResourcesXML = String.Join(Environment.NewLine,
                                       lbImageView.SelectedItems.Cast(Of ResourceItem).Select(Function(i) $"<Resource ID=""{IDsToXMLFormat(i.IDs)}""/>"))
                If Not String.IsNullOrWhiteSpace(ResourcesXML) Then
                    If IsResListSelected Then
                        Dim ResList = GetSelectedResList()
                        Clipboard.SetDataObject(
$"<ResourceList Name=""{ResList.DisplayText}"" FilePath=""{ResList.OriginalFilePath}"" Type=""{ResList.ResItems(0).Type}"" Format=""{ResList.ResItems(0).Format}"">
{ResourcesXML}
</ResourceList>")
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub OpenFile() Handles bOpenFile.Click
        Dim OFD = FileDialog(FilterPE)
        If OFD.ShowDialog = Forms.DialogResult.OK Then PopulateFromFile(OFD.FileName)
    End Sub
    Private Sub bFileInfo_Click() Handles bFileInfo.Click
        Dim fp = GetSelectedResList.OriginalFilePath
        AsyncDo(bFileInfo, "...", Sub() PopupFileInfo(fp))
    End Sub
    Private Shared Function ChangeSingle(ResList As ResourceList, ResItem As ResourceItem) As Boolean
        Dim OFD = FileDialog(FilterImage)
        If OFD.ShowDialog = Forms.DialogResult.OK Then
            If IO.File.Exists(OFD.FileName) Then
                CustomizerGod.Resources.ChangeImageResource(ResList.FilePath, ResItem, OFD.FileName)
                Return True
            Else
                PopupError(StrFormat(PopupStr.File_Does_Not_Exist, OFD.FileName))
            End If
        End If
        Return False
    End Function
    Private Sub bChange_Click() Handles bChange.Click
        Dim SelectedResList = GetSelectedResList()
        If lbImageView.Items.Count > 1 AndAlso lbImageView.SelectedItems.Count < 1 Then
            PopupMessage(PopupStr.Res_Not_Selected)
        Else
            Dim DidSelect = False
            If lbImageView.Items.Count = 1 Then
                DidSelect = ChangeSingle(SelectedResList, DirectCast(lbImageView.Items(0), ResourceItem))
            ElseIf lbImageView.SelectedItems.Count > 0 Then
                If lbImageView.SelectedItems.Count = 1 Then
                    DidSelect = ChangeSingle(SelectedResList, DirectCast(lbImageView.SelectedItem, ResourceItem))
                ElseIf lbImageView.SelectedItems.Count > 1 Then
                    If FBD.Value.ShowDialog(WindowHandle) Then
                        CustomizerGod.Resources.ChangeImageResources(SelectedResList.FilePath, lbImageView.SelectedItems.Cast(Of ResourceItem), FBD.Value.SelectedPath)
                        DidSelect = True
                    End If
                End If
            End If
            If DidSelect Then
                ReloadRightPane()
                Restart(SelectedResList)
            End If
        End If
    End Sub
    Private Sub bRestore_Click() Handles bRestore.Click
        Dim SelectedResList = GetSelectedResList()
        If lbImageView.Items.Count > 1 AndAlso lbImageView.SelectedItems.Count < 1 Then
            PopupMessage(PopupStr.Res_Not_Selected)
        Else
            Dim ResFilePath = SelectedResList.FilePath
            If lbImageView.Items.Count = 1 Then
                CustomizerGod.Resources.Restore(ResFilePath, lbImageView.Items.Cast(Of ResourceItem))
            ElseIf lbImageView.SelectedItems.Count > 0 Then
                CustomizerGod.Resources.Restore(ResFilePath, lbImageView.SelectedItems.Cast(Of ResourceItem))
            End If
            ReloadRightPane()
            Restart(SelectedResList)
        End If
    End Sub
    Private Sub bAdvanced_Click() Handles bAdvanced.Click
        Dim StashOpacity, StashHeight, WindowExtendHeight As Integer
        If bAdvanced.IsChecked Then
            StashOpacity = 1
            StashHeight = ExtraHeight
            WindowExtendHeight = ExtraHeight
        Else
            WindowExtendHeight = -ExtraHeight
            MinHeight = DefaultMinHeight
        End If
        bStash.BeginAnimation(HeightProperty, New Animation.DoubleAnimation(StashHeight, AnimationDuration))
        bStash.BeginAnimation(OpacityProperty, New Animation.DoubleAnimation(StashOpacity, AnimationDuration))
        If SystemParameters.WorkArea.Height - (ActualHeight + Top) >= WindowExtendHeight Then
            Dim WindowHeightAnim = New Animation.DoubleAnimation(ActualHeight, ActualHeight + WindowExtendHeight, AnimationDuration)
            If bAdvanced.IsChecked Then AddHandler WindowHeightAnim.Completed, Sub() MinHeight = DefaultMinHeight + ExtraHeight
            BeginAnimation(HeightProperty, WindowHeightAnim)
        End If
    End Sub
    Private Sub bRestoreFileBAK_Click() Handles bRestoreFileBAK.Click
        Dim ResList = GetSelectedResList()
        If IO.File.Exists(ResList.OriginalFilePath.ToBakPath) Then
            AsyncDo(bRestoreFileBAK, "Restoring...",
                    Sub() RestoreFromBackup(ResList.OriginalFilePath),
                    Sub()
                        ReloadRightPane()
                        Restart(ResList)
                    End Sub)
        Else
            PopupMessage(StrFormat(PopupStr.Backup_File_Does_Not_Exist, ResList.OriginalFilePath))
        End If
    End Sub
    Private Sub bRestoreFileSFC_Click() Handles bRestoreFileSFC.Click
        Dim ResFilePath = GetSelectedResList.OriginalFilePath
        If SfcIsFileProtected(IntPtr.Zero, ResFilePath) Then
            SFCRestore(ResFilePath)
        Else
            PopupError(String.Format(PopupStr.File_Not_SFC_Protected, ResFilePath))
        End If
    End Sub
    Private Sub bExport_Click() Handles bExport.Click
        Dim SelectedResList = GetSelectedResList()
        Dim Items As IEnumerable(Of ResourceItem)
        If lbImageView.SelectedItems.Count > 0 Then
            Items = lbImageView.SelectedItems.Cast(Of ResourceItem).ToArray
        ElseIf lbImageView.Items.Count = 1 Then
            Items = {DirectCast(lbImageView.Items(0), ResourceItem)}
        Else
            PopupMessage(PopupStr.Res_Not_Selected)
            Exit Sub
        End If
        If FBD.Value.ShowDialog(WindowHandle) Then
            Dim ResFilePath = If(tbPreviewBackup.IsChecked, SelectedResList.FilePath.ToBakPath, SelectedResList.FilePath)
            AsyncDo(bExport, "Exporting...", Sub() ExportResources(ResFilePath, Items, FBD.Value.SelectedPath))
        End If
    End Sub
    Shared Sub ExportResources(ResFilePath$, ResItems As IEnumerable(Of ResourceItem), FolderPath$)
        Using SLH = New SafeLibraryHandle(ResFilePath)
            For Each ResItem In ResItems
                Dim Win32Ext = ResItem.Format.GetFileExtension
                For Each ID In ResItem.IDs
                    Dim Bytes = CustomizerGod.Resources.GetCompatibleResBytes(SLH, ResItem.Type, ID)
                    IO.File.WriteAllBytes(GetNumberedFilePath(FolderPath, ID, Win32Ext), Bytes)
                Next
            Next
        End Using
        OpenFolder(FolderPath)
    End Sub
    Private Sub bRetakeBackups_Click() Handles bRetakeBackups.Click
        If Question(PopupStr.Confirm_ReBackups) Then
            AsyncDo(bRetakeBackups, spRetakeBackup, "Retaking...", AddressOf RetakeAllBackups)
        End If
    End Sub
    Private Sub bRetakeBackup_Click() Handles bRetakeBackup.Click
        Dim FilePath = GetSelectedResList.OriginalFilePath
        If Question(StrFormat(PopupStr.Confirm_ReBackup, FilePath)) Then
            AsyncDo(bRetakeBackup, spRetakeBackup, "Retaking...", Sub() RetakeBackup(FilePath))
        End If
    End Sub
    Private Sub lbSidebar_SelectedItemChanged() Handles lbSidebar.SelectionChanged
        IsResListSelected = lbSidebar.SelectedItem IsNot Nothing
        Dim IsReadOnly = IsResListSelected AndAlso DirectCast(lbSidebar.SelectedItem, ResourceList).ReadOnly
        For Each c In {bChange, bRestore}
            c.IsEnabled = IsResListSelected AndAlso Not IsReadOnly
        Next
        For Each c In {bRestoreFileBAK, bRestoreFileSFC, bRetakeBackup, bFileInfo}
            c.IsEnabled = c.IsEnabled AndAlso IsResListSelected
        Next
        scBMPFormat.IsEnabled = IsResListSelected
        ReloadRightPane()
    End Sub
    Private Sub Me_StateChanged(sender As Object, e As EventArgs) Handles Me.StateChanged
        lTitle.Height = If(WindowState = WindowState.Maximized, 44, 68)
    End Sub
    Private Sub lbSidebar_Drop(sender As Object, e As DragEventArgs) Handles lbSidebar.Drop
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            For Each FilePath In DirectCast(e.Data.GetData(DataFormats.FileDrop), String())
                'FIX: errors swallowed
                PopulateFromFile(FilePath)
            Next
        End If
    End Sub
    Sub ShowCallout(Inlines As IEnumerable(Of Inline))
        tbCallout.Inlines.AddRange(Inlines)
        bCallout.BeginAnimation(MaxHeightProperty, New Animation.DoubleAnimation(300, AnimationDuration))
    End Sub
    Private Sub CloseCallout() Handles bCloseCallout.Click
        bCallout.BeginAnimation(MaxHeightProperty, New Animation.DoubleAnimation(0, AnimationDuration))
    End Sub
End Class
