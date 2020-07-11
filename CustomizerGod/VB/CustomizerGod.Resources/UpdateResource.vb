Imports System
Imports System.Linq
Imports System.Collections.Generic
Namespace Resources
    Module UpdateResourceMethods
        Const Language As UShort = 1033
        ReadOnly MuiStore As New Dictionary(Of String, Byte()),
             Extensions$() = "ico png bmp gif jpg".Split
        Structure ResStoreItem
            Dim ID, Type As String, Data() As Byte
            Sub New(Type$, ID$, Data As Byte())
                Me.Type = Type
                Me.ID = ID
                Me.Data = Data
            End Sub
        End Structure
        Sub AddToMuiStore(ResFilePath$, SLH As SafeLibraryHandle)
            If Not MuiStore.ContainsKey(ResFilePath) Then MuiStore.Add(ResFilePath, GetResBytes(SLH, "MUI", "#1", False))
        End Sub
        Function CallUpdateResource(hUpdate As IntPtr, r As ResStoreItem) As Boolean
            Dim ID, Type As IntPtr
            If r.ID(0) = "#" Then ID = New IntPtr(r.ID.ToUShort)
            If r.Type(0) = "#" Then Type = New IntPtr(r.Type.ToUShort)
            If r.Data IsNot Nothing Then
                Dim DataLength = CUInt(r.Data.Length)
                If Type <> IntPtr.Zero AndAlso ID <> IntPtr.Zero Then Return UpdateResource(hUpdate, Type, ID, Language, r.Data, DataLength)
                If Type <> IntPtr.Zero AndAlso ID = IntPtr.Zero Then Return UpdateResource(hUpdate, Type, r.ID, Language, r.Data, DataLength)
                If Type = IntPtr.Zero AndAlso ID <> IntPtr.Zero Then Return UpdateResource(hUpdate, r.Type, ID, Language, r.Data, DataLength)
                If Type = IntPtr.Zero AndAlso ID = IntPtr.Zero Then Return UpdateResource(hUpdate, r.Type, r.ID, Language, r.Data, DataLength)
            Else
                If Type <> IntPtr.Zero AndAlso ID <> IntPtr.Zero Then Return UpdateResource(hUpdate, Type, ID, Language, IntPtr.Zero, 0)
                If Type = IntPtr.Zero AndAlso ID <> IntPtr.Zero Then Return UpdateResource(hUpdate, r.Type, ID, Language, IntPtr.Zero, 0)
            End If
            Throw New NotImplementedException
        End Function
        Sub ChangeUsingWin32API(ResStore As IList(Of ResStoreItem), ResFilePath$, EditFilePath$)
            Dim MUI = MuiStore(ResFilePath)
            If MUI IsNot Nothing Then
                ResStore.Insert(0, New ResStoreItem("MUI", "#1", Nothing))
                ResStore.Add(New ResStoreItem("MUI", "#1", MUI))
            End If
            Dim hUpdate = BeginUpdateResource(EditFilePath, False)
            If hUpdate <> IntPtr.Zero Then
                For Each r In ResStore
                    If Not CallUpdateResource(hUpdate, r) Then Throw New NativeException("UpdateResource " & r.Type & "\" & r.ID)
                Next
                If Not EndUpdateResource(hUpdate, False) Then Throw New NativeException("EndUpdateResource " & EditFilePath)
            Else
                Throw New NativeException("BeginUpdateResource " & EditFilePath)
            End If
        End Sub
        Function CreateTempCopyForEditing$(FilePath$)
            Dim TmpFilePath = IO.Path.Combine(TempDirPath, Guid.NewGuid.ToString & "." & IO.Path.GetFileName(FilePath) & ".tmp")
            For Each FilePath In {FilePath, FilePath.ToBakPath}
                If IO.File.Exists(FilePath) Then
                    IO.File.Copy(FilePath, TmpFilePath, True)
                    Return TmpFilePath
                End If
            Next
            Throw New AppException(StrFormat(PopupStr.File_Does_Not_Exist, FilePath))
        End Function
        Sub FinalizeChange(ResFilePath$, ResStore As IList(Of ResStoreItem))
            UsedIconIDs = Nothing
            If CurrentSLH IsNot Nothing Then
                CurrentSLH.Dispose()
                CurrentSLH = Nothing
            End If
            If ResStore.Count > 0 Then
                Dim EditFilePath = CreateTempCopyForEditing(ResFilePath)
                ChangeUsingWin32API(ResStore, ResFilePath, EditFilePath)
                UpdatePEChecksum(EditFilePath)
                ReplaceFile(EditFilePath, ResFilePath, True)
            End If
        End Sub
        Sub ChangeImageResource(ResFilePath$, ResItem As ResourceItem, DataFilePath$)
            Dim ResStore = New List(Of ResStoreItem)
            Using BackupSLH = New SafeLibraryHandle(ResFilePath.ToBakPath)
                FillResStore(BackupSLH, ResStore, ResItem, ResFilePath, DataFilePath)
                AddToMuiStore(ResFilePath, BackupSLH)
            End Using
            FinalizeChange(ResFilePath, ResStore)
        End Sub
        Sub ChangeImageResources(ResFilePath$, ResItems As IEnumerable(Of ResourceItem), DirectoryName$)
            Dim ResStore = New List(Of ResStoreItem)
            Using BackupSLH = New SafeLibraryHandle(ResFilePath.ToBakPath)
                Dim FilePaths = IO.Directory.GetFiles(DirectoryName)
                For Each ResItem In ResItems
                    Dim FilePath = FindFile(FilePaths, Extensions.SelectMany(Function(Ext) {ResItem.ID & "." & Ext, ResItem.ID.TrimStart("#"c) & "." & Ext}))
                    If FilePath IsNot Nothing Then FillResStore(BackupSLH, ResStore, ResItem, ResFilePath, FilePath)
                Next
                AddToMuiStore(ResFilePath, BackupSLH)
            End Using
            FinalizeChange(ResFilePath, ResStore)
        End Sub
        Sub Restore(ResFilePath$, ResItems As IEnumerable(Of ResourceItem))
            Dim ResStore = New List(Of ResStoreItem)
            Using BackupSLH = New SafeLibraryHandle(ResFilePath.ToBakPath)
                For Each ResItem In ResItems
                    For Each ID In ResItem.IDs
                        Dim ResBytes = GetResBytes(BackupSLH, ResItem.Type, ID)
                        If ResItem.Type = RT_GROUP_ICON Then
                            Dim IconDir = CreateIconDirFromGrpIconDir(BackupSLH, ResBytes)
                            ResBytes = IconFromIcon(ResFilePath, ResStore, IconDir, ID)
                        End If
                        ResStore.Add(New ResStoreItem(ResItem.Type, ID, ResBytes))
                    Next
                Next
                AddToMuiStore(ResFilePath, BackupSLH)
            End Using
            FinalizeChange(ResFilePath, ResStore)
        End Sub
        Function FindFile$(FilePaths$(), SearchNames As IEnumerable(Of String))
            For Each FilePath In FilePaths
                If SearchNames.Contains(IO.Path.GetFileName(FilePath)) Then Return FilePath
            Next
            Return Nothing
        End Function
    End Module
End Namespace