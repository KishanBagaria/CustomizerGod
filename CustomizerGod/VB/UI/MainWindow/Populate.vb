Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text.RegularExpressions
Imports System.Xml.Linq
Imports CustomizerGod.Resources
Partial Class MainWindow
    Shared ReadOnly ResIDRegex As New Regex("#(?<from>\d+):#(?<to>\d+)")
    Shared Function GetResIDs(ResIDs$) As String()
        For Each m As Match In ResIDRegex.Matches(ResIDs)
            Dim FromID = CInt(m.Groups("from").Value),
                ToID = CInt(m.Groups("to").Value)
            ResIDs = ResIDs.Replace(m.Value, String.Join(",", Enumerable.Range(FromID, ToID - FromID + 1).Select(Function(i) $"#{i}")))
        Next
        Return ResIDs.Split(","c)
    End Function
    Shared Function IDsToXMLFormat$(IDs As IEnumerable(Of String))
        Dim Result = New List(Of String)
        Dim Ignore = New HashSet(Of String)
        For Each Item In IDs
            If Ignore.Contains(Item) Then Continue For
            Dim ID = Item
            If ID.StartsWith("#") Then
                Dim FromInt As UShort
                If UShort.TryParse(ID.Substring(1), FromInt) Then
                    Dim Less1 = $"#{FromInt - 1}"
                    Dim Plus1 = $"#{FromInt + 1}"
                    Dim HasPlus1 = IDs.Contains(Plus1)
                    If IDs.Contains(Less1) AndAlso HasPlus1 Then Continue For
                    Dim Plus2 = $"#{FromInt + 2}"
                    If HasPlus1 AndAlso IDs.Contains(Plus2) Then
                        Ignore.Add(Plus1)
                        Ignore.Add(Plus2)
                        Dim ToInt = FromInt + 2
                        While IDs.Contains($"#{ToInt + 1}")
                            ToInt += 1
                            Ignore.Add($"#{ToInt}")
                        End While
                        ID = $"#{FromInt}:#{ToInt}"
                    End If
                End If
            End If
            Result.Add(ID)
        Next
        Return String.Join(",", Result)
    End Function
    Private Sub PopulateFromXML(ResData As XContainer, Optional DriveLetter$ = Nothing)
        If DriveLetter Is Nothing Then DriveLetter = SysDriveLetter
        For Each ResList In ResData.<ResourceList>
            Dim ResItemList = New List(Of ResourceItem),
                ResItems = ResList.<Resource>,
                FilePath = ResList.@FilePath,
                Resize = ResList.@Resize <> "False"
            If FilePath(0) = ":" Then FilePath = DriveLetter & FilePath
            AddToBackupList(FilePath)
            'Echo(FilePath, GetAuthenticodeSigStatus(FilePath))
            Dim rl As New ResourceList With {.DisplayText = ResList.@Name,
                                             .OriginalFilePath = FilePath,
                                             .FilePath = If(ResList.@PreviewCopy = "True", GetFileLnkPath(FilePath), FilePath),
                                             .Restart = ResList.@Restart.ToEnum(Of RestartMode)(),
                                             .ToolTip = FilePath,
                                             .ReadOnly = (ResList.@ReadOnly = "True")}
            If ResItems.Count > 0 Then
                For Each ResItem In ResItems
                    ResItemList.Add(New ResourceItem With {.IDs = GetResIDs(ResItem.@ID),
                                                           .FriendlyName = ResItem.@Name,
                                                           .Type = If(ResList.@Type, ResItem.@Type),
                                                           .Format = If(ResList.@Format, ResItem.@Format).ToEnum(Of ResFormat)(),
                                                           .Resize = Resize})
                Next
            Else
                For Each ResID In GetResourceIDs(FilePath, ResList.@Type)
                    ResItemList.Add(New ResourceItem With {.IDs = {ResID},
                                                           .Type = ResList.@Type,
                                                           .Format = ResList.@Format.ToEnum(Of ResFormat)(),
                                                           .Resize = Resize})
                Next
            End If
            rl.ResItems = ResItemList
            lbSidebar.Items.Add(rl)
        Next
    End Sub
    Shared Function IsFileReadOnly(FilePath$) As Boolean
        FilePath = FilePath.ToLowerInvariant
        Select Case WindowsV
            Case >= 100
                Return FilePath.EndsWith(":\windows\system32\shell32.dll") OrElse FilePath.EndsWith(":\windows\syswow64\shell32.dll") OrElse FilePath.EndsWith(":\windows\explorer.exe")
            Case >= 63
                Return FilePath.EndsWith(":\windows\system32\shell32.dll") OrElse FilePath.EndsWith(":\windows\syswow64\shell32.dll")
        End Select
        Return False
    End Function
    Shared ReadOnly FilesLoaded As New HashSet(Of String)
    Private Shared Function HasPEMagicNumber(FilePath$) As Boolean
        Using f = IO.File.Open(FilePath, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read)
            If f.Length < 2 Then Return False
            If f.ReadByte <> 77 Then Return False
            If f.ReadByte <> 90 Then Return False
        End Using
        Return True
    End Function
    Private Sub PopulateFromFile(FilePath$)
        If IsBakPath(FilePath) Then FilePath = FilePath.ToNormalPath
        If FilesLoaded.Contains(FilePath) Then
            PopupMessage(StrFormat(PopupStr.Already_Open, FilePath))
            Dim rl = lbSidebar.Items.OfType(Of ResourceList).FirstOrDefault(Function(i) i.OriginalFilePath = FilePath)
            If rl IsNot Nothing Then lbSidebar.SelectedItem = rl
        Else
            Dim IsReadOnly = IsFileReadOnly(FilePath)
            If IsReadOnly Then PopupMessage(StrFormat(PopupStr.Embedded_Digital_Signature, FilePath))
            If Not HasPEMagicNumber(FilePath) Then
                PopupMessage(StrFormat(PopupStr.Invalid_File, FilePath, "portable executable (PE)"))
                Exit Sub
            End If
            Dim OldCount = lbSidebar.Items.Count
            Dim Resize = False
            Dim FileName = IO.Path.GetFileName(FilePath)
            For Each Type In GetResourceTypes(FilePath)
                Dim FriendlyTypeName = ResTypeFriendlyNames.GetValueOrDefault(Type, Type)
                Dim ResIDs = GetResourceIDs(FilePath, Type)
                Dim Format = GetResFormat(Type, FilePath, ResIDs(0))
                If Format Is Nothing Then Continue For
                lbSidebar.Items.Add(New ResourceList With {
                    .DisplayText = $"{FileName} - {FriendlyTypeName}",
                    .OriginalFilePath = FilePath,
                    .FilePath = FilePath,
                    .Restart = RestartMode.None,
                    .ToolTip = $"{FilePath} - {ResTypeNativeNames.GetValueOrDefault(Type, Type)}",
                    .ReadOnly = IsReadOnly,
                    .ResItems = ResIDs.Select(Function(ID) New ResourceItem With {.IDs = {ID}, .Type = Type, .Format = Format.Value, .Resize = Resize}).ToList
                })
            Next
            If OldCount = lbSidebar.Items.Count Then
                PopupMessage(PopupStr.No_Resources)
            Else
                TakeBackup(FilePath)
                FilesLoaded.Add(FilePath)
            End If
        End If
    End Sub
    Const CustomXMLName = "Custom.xml"
    Sub PopulateFromXML(Optional XMLPath$ = CustomXMLName)
        Try
            If IO.File.Exists(XMLPath) Then PopulateFromXML(XElement.Load(XMLPath))
        Catch ex As Exception When TypeOf ex Is Xml.XmlException OrElse TypeOf ex Is IO.IOException
            PopupError(ex.Message)
        End Try
    End Sub
    Shared Function GetResFormat(ResType$, FilePath$, FirstResID$) As ResFormat?
        If NativeResFormats.ContainsKey(ResType) Then Return NativeResFormats(ResType)
        Using SLH = New SafeLibraryHandle(FilePath)
            Dim FirstResBytes = GetResBytes(SLH, ResType, FirstResID),
                ImgType = GetImageType(FirstResBytes)
            If ImgType IsNot Nothing Then Return ImgType.ToResFormat
        End Using
    End Function
End Class
