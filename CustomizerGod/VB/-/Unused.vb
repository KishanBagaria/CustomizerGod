Public Class Unused
    'ElseIf TypeOf lbSidebar.SelectedItem Is ResourceItem Then
    'Items = {DirectCast(lbSidebar.SelectedItem, ResourceItem)}

    'If isimageformat(SelectedResList.ResItems(0).Format) Then
    'Else
    '        PopupMessage(PopupStr.Res_Not_Selected)
    '        Exit Sub
    'End If

    'Dim IsEnabled = TypeOf lbSidebar.SelectedItem Is ResourceList OrElse TypeOf lbSidebar.SelectedItem Is ResourceItem
    'For Each c In {bChange, bRestore, bRestoreFileBAK, bRestoreFileSFC, bExport, bRetakeBackup}
    '    Panel.SetZIndex(c, If(IsEnabled, 0, -1))
    '    c.IsEnabled = IsEnabled
    'Next
    'If TypeOf lbSidebar.SelectedItem Is ResourceList Then
    'End If

    'If TypeOf Selected Is ResourceList Then
    'ElseIf TypeOf Selected Is ResourceItem Then
    'Return DirectCast(Selected, ResourceItem).ResList
    'End If
    'Return Nothing

    'Property SubItems As IEnumerable(Of SidebarItem)
    'Property IsSelected As Boolean

    'Public Class StrOrMsgTableItem
    '    Property ID As String
    '    Property Text As String
    'End Class
    'Public Class AcceleratorTableItem
    '    Property CommandID As UShort
    '    Property Ansi As UShort
    '    Property Flags As UShort
    'End Class

    'Binary
    'Text
    'StringTable
    'MessageTable
    'AcceleratorTable
    'Version

    'ReadOnly ImageFormats As ResFormat() = {ResFormat.BMP, ResFormat.PArgbBMP, ResFormat.ArgbBMP, ResFormat.RgbBMP, ResFormat.PNG, ResFormat.GIF, ResFormat.JPG, ResFormat.ICO}
    'Function IsImageFormat(rf As ResFormat) As Boolean
    'Return ImageFormats.Contains(rf)
    'End Function
    'Return If(IsImageFormat(Format), Format.ToString.ToLowerInvariant, "bin")

    'Public Class SidebarItem
    '    Property DisplayName As String
    '    Property ToolTip As String
    'End Class

    'If lbSidebar.Items.Count > 0 Then DirectCast(lbSidebar.Items(0), SidebarItem).IsSelected = True

    '                <!--<TabControl xName="tcView" Background="Transparent" BorderThickness="0" Padding="0" SelectedValuePath="Header">
    '                    <TabControl.Resources>
    '<Style TargetType = "{x:Type TabPanel}" >
    '                            <Setter Property="Visibility" Value="Collapsed"/>
    '                            </Style>
    '                        <Style TargetType = "{x:Type DataGrid}" >
    '                            <Setter Property="HorizontalGridLinesBrush" Value="{DynamicResource {x:Static SystemColors.MenuBarBrushKey}}"/>
    '<Setter Property= "VerticalGridLinesBrush" Value="{DynamicResource {x:Static SystemColors.MenuBarBrushKey}}"/>
    '                            <Setter Property= "HeadersVisibility" Value="Column" />
    '                            <Setter Property= "BorderThickness" Value="0"/>
    '                            <Setter Property= "CanUserResizeRows" Value="False"/>
    '                            <Setter Property= "CanUserResizeColumns" Value="False"/>
    '                            <Setter Property= "CanUserReorderColumns" Value="False"/>
    '                            <Setter Property= "CanUserSortColumns" Value="False"/>
    '                            <Setter Property= "SelectionMode" Value="Single" />
    '                            <Setter Property= "AutoGenerateColumns" Value="False" />
    '                            <Setter Property= "Background" Value="Transparent" />
    '                        </Style>
    '                    </TabControl.Resources>
    '                    <TabItem Header = "Images" > -->
    '<!--</TabItem>
    '                    <TabItem/>
    '                    <TabItem Header="Text">
    '                        <TextBox x:Name="tbView" BorderThickness="0" Background="Transparent" ScrollViewer.VerticalScrollBarVisibility="Auto" ScrollViewer.HorizontalScrollBarVisibility="Auto" />
    '                    </TabItem>
    '                    <TabItem Header="Binary">
    '                        <WindowsFormsHost xmlns:hb="clr-namespace:Be.Windows.Forms;assembly=Be.Windows.Forms.HexBox" x:Name="wfh" Background="Transparent">
    '                            <hb:HexBox x:Name="hb" StringViewVisible="True" ColumnInfoVisible="True" LineInfoVisible="True" BorderStyle="None" Font="Consolas, 10pt" VScrollBarVisible="True" UseFixedBytesPerLine="True" />
    '                        </WindowsFormsHost>
    '                    </TabItem>
    '                    <TabItem Header="StrOrMsgTable">
    '                        <DataGrid x:Name="dgSTView" CanUserAddRows="False" CanUserDeleteRows="False">
    '                            <DataGrid.Columns>
    '                                <DataGridTextColumn Header="ID" Binding="{Binding ID}" IsReadOnly="True" />
    '                                <DataGridTextColumn Header="Text" Binding="{Binding Text}" />
    '                            </DataGrid.Columns>
    '                        </DataGrid>
    '                    </TabItem>
    '                    <TabItem Header="AcceleratorTable">
    '                        <DataGrid x:Name="dgATView">
    '                            <DataGrid.Columns>
    '                                <DataGridTextColumn Header="Command ID" Binding="{Binding CommandID}" />
    '                                <DataGridTextColumn Header="Flags" Binding="{Binding Flags}" />
    '                                <DataGridTextColumn Header="Ansi" Binding="{Binding Ansi}" />
    '                            </DataGrid.Columns>
    '                        </DataGrid>
    '                    </TabItem>
    '                </TabControl>-->
    '                    <!-- x:Name="spBgToggles"-->

    'If TypeOf Selected Is ResourceList Then
    'ElseIf TypeOf Selected Is ResourceItem Then
    'Dim SelectedResItem = DirectCast(Selected, ResourceItem)
    'Dim ResFilePath = SelectedResItem.ResList.FilePath
    'If PreviewBackup Then ResFilePath = ResFilePath.ToBakPath
    '            Using SLH = New SafeLibraryHandle(ResFilePath)
    'GetImageResource(SLH, SelectedResItem, AppErrors)
    'End Using
    'End If
    'stData As New ObservableCollection(Of StrOrMsgTableItem),
    'atData As New ObservableCollection(Of AcceleratorTableItem)
    'dgATView.ItemsSource = atData
    'dgSTView.ItemsSource = stData
    'Sub HideAllViews()
    '    spBgToggles.Visibility = Visibility.Collapsed
    '    tcView.SelectedValue = Nothing
    'End Sub
    'Sub SetView(Type As ResFormat)
    '    Dispatcher.Invoke(
    '        Sub()
    '            Select Case Type
    '                Case ResFormat.Text
    '                    tcView.SelectedValue = "Text"
    '                    spBgToggles.Visibility = Visibility.Visible
    '                Case ResFormat.Binary
    '                    tcView.SelectedValue = "Binary"
    '                    spBgToggles.Visibility = Visibility.Collapsed
    '                Case ResFormat.StringTable, ResFormat.MessageTable
    '                    tcView.SelectedValue = "StrOrMsgTable"
    '                    spBgToggles.Visibility = Visibility.Collapsed
    '                Case ResFormat.AcceleratorTable
    '                    tcView.SelectedValue = "AcceleratorTable"
    '                    spBgToggles.Visibility = Visibility.Collapsed
    '                Case ResFormat.Version
    '                    tcView.SelectedValue = "Text"
    '                    spBgToggles.Visibility = Visibility.Visible
    '                Case Else
    '                    tcView.SelectedValue = "Images"
    '                    spBgToggles.Visibility = Visibility.Visible
    '            End Select
    '        End Sub)
    'End Sub
    'Private Sub AddStringTableRows(SLH As SafeLibraryHandle, ID$)
    '    For Each s In GetStringTableResources(SLH, ID)
    '        Dispatcher.Invoke(Sub() stData.Add(s))
    '    Next
    'End Sub
    'Private Sub AddMessageTableRows(SLH As SafeLibraryHandle, ID$)
    '    For Each s In GetMessageTableResources(SLH, ID)
    '        Dispatcher.Invoke(Sub() stData.Add(s))
    '    Next
    'End Sub
    'Private Sub AddAcceleratorTableRows(SLH As SafeLibraryHandle, ID$)
    '    For Each s In GetAcceleratorResources(SLH, ID)
    '        'TODO: Present data in the right format
    '        Dispatcher.Invoke(Sub() atData.Add(New AcceleratorTableItem With {.CommandID = s.wId, .Flags = s.fFlags, .Ansi = s.wAnsi}))
    '    Next
    'End Sub
    'SetView(SelectedResItem.Format)
    '[...]
    'If IsImageFormat(SelectedResItem.Format) Then
    '[...]
    'ElseIf SelectedResItem.Format = ResFormat.Text Then
    '    Dim Bytes = GetResBytes(SLH, SelectedResItem.Type, SelectedResItem.ID)
    '    Dim Text = Encoding.UTF8.GetString(Bytes)
    '    Dispatcher.Invoke(Sub() tbView.Text = Text)
    'ElseIf SelectedResItem.Format = ResFormat.StringTable Then
    '    Dispatcher.Invoke(Sub() stData.Clear())
    '    AddStringTableRows(SLH, SelectedResItem.ID)
    'ElseIf SelectedResItem.Format = ResFormat.MessageTable Then
    '    Dispatcher.Invoke(Sub() stData.Clear())
    '    AddMessageTableRows(SLH, SelectedResItem.ID)
    'ElseIf SelectedResItem.Format = ResFormat.AcceleratorTable Then
    '    Dispatcher.Invoke(Sub() atData.Clear())
    '    AddAcceleratorTableRows(SLH, SelectedResItem.ID)
    'ElseIf SelectedResItem.Format = ResFormat.Version Then
    '    Dim Bytes = GetResBytes(SLH, RT_VERSION, SelectedResItem.ID)
    '    Dim Text = GetVersionText(Bytes)
    '    Dispatcher.Invoke(Sub() tbView.Text = Text)
    'Else
    '    Dim Bytes = GetResBytes(SLH, SelectedResItem.Type, SelectedResItem.ID)
    '    Dispatcher.Invoke(Sub() hb.ByteProvider = New DynamicByteProvider(Bytes))
    '  End If
    '[...]
    'Else
    'Dispatcher.Invoke(Sub() HideAllViews())
    'Dim FirstResource = SelectedResList.ResItems(0)
    'SetView(FirstResource.Format)
    'If IsImageFormat(FirstResource.Format) Then
    '[...]
    'ElseIf FirstResource.Format = ResFormat.StringTable Then
    '    Dispatcher.Invoke(Sub() stData.Clear())
    '    Using SLH = New SafeLibraryHandle(ResFilePath)
    '        For Each Item In SelectedResList.ResItems
    '            AddStringTableRows(SLH, Item.ID)
    '        Next
    '    End Using
    'ElseIf FirstResource.Format = ResFormat.MessageTable Then
    '    Dispatcher.Invoke(Sub() stData.Clear())
    '    Using SLH = New SafeLibraryHandle(ResFilePath)
    '        For Each Item In SelectedResList.ResItems
    '            AddMessageTableRows(SLH, Item.ID)
    '        Next
    '    End Using
    'ElseIf FirstResource.Format = ResFormat.AcceleratorTable Then
    '    Dispatcher.Invoke(Sub() atData.Clear())
    '    Using SLH = New SafeLibraryHandle(ResFilePath)
    '        For Each Item In SelectedResList.ResItems
    '            AddAcceleratorTableRows(SLH, Item.ID)
    '        Next
    '    End Using
    ' ElseIf FirstResource.Format = ResFormat.Version Then
    'Else
    '    Dispatcher.Invoke(Sub() HideAllViews())
    'End If

    'tbView.Foreground = b.OpacityMask
End Class
