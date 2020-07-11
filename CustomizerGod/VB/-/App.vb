Imports System
Imports System.Collections.Generic
Imports System.Windows
Imports Microsoft.Win32

Class App : Inherits Application
    Shared LockFile As IO.FileStream
    Sub LoadStyles()
        Dim hs = New HashSet(Of String)
        Select Case WindowsV
            Case >= 100
                hs.Add("10")
                hs.Add("10-8")
            Case >= 62
                hs.Add("8")
                hs.Add("10-8")
            Case Else
                hs.Add("7")
        End Select
        hs.Add("Generic")
        For Each i In hs
            Resources.MergedDictionaries.Add(New ResourceDictionary With {.Source = New Uri("VB/UI/XAML/" & i & ".xaml", UriKind.Relative)})
        Next
    End Sub
    Sub Me_Startup() Handles Me.Startup
#If Not DEBUG OrElse Code_Analysis Then
        AddHandler DispatcherUnhandledException, Sub(s, e)
                                                     e.Handled = True
                                                     OnException(e.Exception)
                                                     If mw Is Nothing Then Shutdown()
                                                 End Sub
        AddHandler AppDomain.CurrentDomain.UnhandledException, Sub(s, e) OnException(DirectCast(e.ExceptionObject, Exception))
#End If
        RegisterApplicationRestart(Nothing, Nothing)
        UploadExceptions()
        LoadStyles()
        IO.Directory.CreateDirectory(ExceptionDirPath)
        For Attempt = 0 To 3
            Try
                IO.Directory.CreateDirectory(TempDirPath)
                LockFile = IO.File.Create(TempDirPath & "\LockFile")
                Exit For
            Catch e As Exception When TypeOf e Is IO.IOException OrElse TypeOf e Is UnauthorizedAccessException
                If Attempt = 3 Then
                    PopupMessage("This instance will now shut down. Close all other CustomizerGod windows and retry running CustomizerGod.")
                    Shutdown()
                    Exit Sub
                Else
                    PopupMessage("Another instance of CustomizerGod is already running! Please close that instance and click OK." &
                                 If(Attempt > 0, " If you don't see it in the taskbar, use the Task Manager to exit it.", "") &
                                 If(Attempt > 1, " Restarting your PC can also fix this issue.", ""))
                End If
            End Try
        Next
        RevertDefaultShell()
        mw = New MainWindow
        mw.Show()
        dtWebChecker.Start()
    End Sub
    Sub Me_Exit() Handles Me.Exit
        RegTempRevert()
        LockFile?.Dispose()
        DeleteProgramDir()
    End Sub
    Shared Sub DeleteProgramDir()
        If Not IO.Directory.Exists(TempDirPath) Then Exit Sub
        Try
            IO.Directory.Delete(TempDirPath, True)
            Exit Sub
        Catch
        End Try
        Try
            For Each FilePath In IO.Directory.GetFiles(TempDirPath)
                MoveFileEx(FilePath, Nothing, MOVEFILE_DELAY_UNTIL_REBOOT)
            Next
            MoveFileEx(TempDirPath, Nothing, MOVEFILE_DELAY_UNTIL_REBOOT)
        Catch
        End Try
    End Sub
    Shared Sub RevertDefaultShell()
        Using WinLogonKey = Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows NT\CurrentVersion\Winlogon", True)
            Dim Shell = CStr(WinLogonKey?.GetValue("Shell"))
            If Not String.IsNullOrWhiteSpace(Shell) AndAlso Not Shell.ToLowerInvariant.Contains("explorer.exe") Then
                Try
                    WinLogonKey.SetValue("Shell", "explorer.exe")
                Catch uae As UnauthorizedAccessException
                End Try
            End If
        End Using
    End Sub
End Class
