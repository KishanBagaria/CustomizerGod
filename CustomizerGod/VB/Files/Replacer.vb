Imports System
Imports System.Diagnostics

Module Replacer
    Sub NukeFile(FilePath$, Optional Backup As Boolean = False)
        If IO.File.Exists(FilePath) Then
            TakeOwnership(FilePath)
            If Backup Then TakeBackup(FilePath)
            Try
                IO.File.Move(FilePath, IO.Path.Combine(TempDirPath, Guid.NewGuid.ToString))
            Catch e As Exception When TypeOf e Is UnauthorizedAccessException OrElse TypeOf e Is IO.IOException
                Throw New AppException(StrFormat(PopupStr.Unable_To_Remove_File, FilePath, $"---
{e.Message}
---"))
            End Try
        End If
        If IO.File.Exists(FilePath) Then Throw New AppException(StrFormat(PopupStr.Unable_To_Remove_File, FilePath, ""))
    End Sub
    Sub ReplaceFile(SourceFilePath$, TargetFilePath$, Optional Move As Boolean = False)
        NukeFile(TargetFilePath, True)
        If Move Then
            IO.File.Move(SourceFilePath, TargetFilePath)
        Else
            IO.File.Copy(SourceFilePath, TargetFilePath)
        End If
    End Sub
    Function RunProcessAndGetOutput$(FilePath$, Arguments$, Optional Restorable As Boolean = False, Optional PopupErrors As Boolean = True)
        If Restorable AndAlso Not IO.File.Exists(FilePath) Then AskForFileRestore(FilePath)
        If Not IO.File.Exists(FilePath) Then Throw New AppException(PopupStr.File_Does_Not_Exist)
        Dim p = New Process() With {.StartInfo = New ProcessStartInfo(FilePath, Arguments) With {.UseShellExecute = False, .RedirectStandardOutput = True, .RedirectStandardError = True, .CreateNoWindow = True}}
        p.Start()
        Dim StandardOutput = p.StandardOutput.ReadToEnd.Trim,
            StandardError = p.StandardError.ReadToEnd.Trim
        p.WaitForExit()
        p.Dispose()
        If PopupErrors AndAlso Not String.IsNullOrEmpty(StandardError) Then PopupError(StrFormat(PopupStr.Process_Arguments_Returned_Error, FilePath, Arguments, StandardError))
        Return StandardOutput
    End Function
    Sub TakeOwnership(FilePath$)
        RunProcessAndGetOutput(TakeOwnPath, "/F """ & FilePath & """", True)
        RunProcessAndGetOutput(IcaclsPath, """" & FilePath & """ /Grant """ & Environment.UserName & """:F", True)
    End Sub
End Module
