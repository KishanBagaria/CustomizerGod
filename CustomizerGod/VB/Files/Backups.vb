Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Runtime.CompilerServices
Imports System.Windows.Input

Module BackupsModule
    ReadOnly FilePathsToBackup As New HashSet(Of String)
    Sub AddToBackupList(FilePath$)
        FilePathsToBackup.Add(FilePath)
    End Sub
    <Extension> Function ToBakPath$(FilePath$)
        Return FilePath & ".backup"
    End Function
    <Extension> Function ToNormalPath$(FilePath$)
        Return FilePath.Remove(FilePath.LastIndexOf(".backup"))
    End Function
    Function IsBakPath(FilePath$) As Boolean
        Return FilePath.ToLowerInvariant.EndsWith(".backup")
    End Function
    Sub TakeBackup(FilePath$)
        Dim BackupPath = FilePath.ToBakPath
        If Not IO.File.Exists(BackupPath) Then
            Backup(FilePath, BackupPath)
        Else
            CompareBackupAndOriginalVersions(FilePath, BackupPath)
        End If
    End Sub
    Sub CompareBackupAndOriginalVersions(FilePath$, BackupPath$)
        Dim viF = FileVersionInfo.GetVersionInfo(FilePath)
        Dim viB = FileVersionInfo.GetVersionInfo(BackupPath)
        Dim FileVersion = GetFileVersion(viF)
        Dim BackupVersion = GetFileVersion(viB)
        If FileVersion = BackupVersion Then
        ElseIf FileVersion > BackupVersion
            Dim MSFT = viF.CompanyName.Contains("Microsoft")
            If Question(StrFormat(PopupStr.Backup_Version_Older, FilePath, FileVersion, BackupVersion, If(MSFT, PopupStr.Windows_Update_Original_File, ""))) Then RetakeBackup(FilePath)
        ElseIf FileVersion < BackupVersion
            If Question(StrFormat(PopupStr.Backup_Version_Newer, FilePath, FileVersion, BackupVersion)) Then RestoreFromBackup(FilePath)
        End If
    End Sub
    Sub RestoreFromBackup(FilePath$)
        ReplaceFile(FilePath.ToBakPath, FilePath)
    End Sub
    Sub TakeBackups()
        If mw Is Nothing Then PopupError(NameOf(mw) & " is null")
        If mw.Dispatcher Is Nothing Then PopupError(NameOf(mw.Dispatcher) & "is null")
        mw?.Dispatcher?.Invoke(Sub()
                                   mw.IsHitTestVisible = False
                                   Mouse.OverrideCursor = Cursors.Wait
                               End Sub)
        For Each FilePath In FilePathsToBackup
            If IO.File.Exists(FilePath) Then
                Dim BackupPath = FilePath.ToBakPath
                If Not IO.File.Exists(BackupPath) Then
                    mw?.Dispatcher?.Invoke(Sub() mw.tbTitle.Text = $"{AppName} | Taking Backups... ({FilePath})")
                    Backup(FilePath, BackupPath)
                Else
                    CompareBackupAndOriginalVersions(FilePath, BackupPath)
                End If
            Else
                AskForFileRestore(FilePath)
            End If
        Next
    End Sub
    Sub Backup(FilePath$, BackupFilePath$)
        Try
            IO.File.Copy(FilePath, BackupFilePath)
        Catch uae As UnauthorizedAccessException
            PopupError(StrFormat(PopupStr.Access_Denied_For_Taking_Backup, FilePath, BackupFilePath))
        End Try
    End Sub
    Sub FinishTakeBackups()
        Mouse.OverrideCursor = Nothing
        mw.tbTitle.Text = AppName
        mw.IsHitTestVisible = True
    End Sub
    Sub SFCRestore(FilePath$)
        Process.Start(CmdPath, "/C " & StrInvFormat(Res.SFC, FilePath, $"/ScanFile=""{FilePath}""").Replace(Environment.NewLine, " & "))
    End Sub
    Sub SFCRestoreAll()
        Process.Start(CmdPath, "/C " & StrInvFormat(Res.SFC, "All", "/ScanNow").Replace(Environment.NewLine, " & "))
    End Sub
    Sub AskForFileRestore(FilePath$)
        If Question(StrFormat(PopupStr.System_File_Not_Found, FilePath)) Then
            For Each BackupPath In {FilePath & ".backup", FilePath & ".bak", FilePath.ToBakPath}
                If IO.File.Exists(BackupPath) Then
                    IO.File.Copy(BackupPath, FilePath, False)
                    PopupMessage(StrFormat(PopupStr.File_Restored, FilePath, BackupPath))
                    Exit Sub
                End If
            Next
            SFCRestore(FilePath)
        End If
    End Sub
    Sub RetakeBackup(FilePath$)
        Dim BackupPath = FilePath.ToBakPath
        If Not IO.File.Exists(FilePath) Then AskForFileRestore(FilePath)
        IO.File.Copy(FilePath, BackupPath, True)
    End Sub
    Sub RetakeAllBackups()
        For Each FilePath In FilePathsToBackup
            RetakeBackup(FilePath)
        Next
    End Sub
End Module
