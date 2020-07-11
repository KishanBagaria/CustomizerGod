Imports System
Imports System.Diagnostics
Imports Microsoft.Win32

Module Explorer
    Function GetTaskbar() As IntPtr
        Return FindWindow("Shell_TrayWnd", "")
    End Function
    Function IsExplorerRunning() As Boolean
        Return Process.GetProcessesByName("explorer").Length > 0
    End Function
    Sub SetAutoRestartShell()
        RegTempChange(Registry.LocalMachine, "SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", "AutoRestartShell", 1, RegistryValueKind.DWord)
    End Sub
    Sub RestartExplorer(Optional RunIfNotRunning As Boolean = False)
        If IsExplorerRunning() Then
            SetAutoRestartShell()
            Try
                For Each p In Process.GetProcessesByName("explorer")
                    p.Kill()
                Next
            Catch w32e As ComponentModel.Win32Exception
                If IO.File.Exists(TsKillPath) Then
                    RunProcessAndGetOutput(TsKillPath, "explorer")
                Else
                    PopupError(PopupStr.Access_Denied_For_Restarting_Explorer)
                End If
            End Try
            WaitForExplorerStart()
        Else
            If RunIfNotRunning Then RunExplorer()
        End If
    End Sub
    Sub RunExplorer()
        If IO.File.Exists(ExplorerPath) Then
            RunProcessAndGetOutput(PsExecPath, "-accepteula -d " & ExplorerPath, PopupErrors:=False)
            WaitForExplorerStart()
        Else
            AskForFileRestore(ExplorerPath)
        End If
    End Sub
    Sub ExitExplorer()
        If IsExplorerRunning() Then
            PostMessage(GetTaskbar, 1460, IntPtr.Zero, IntPtr.Zero)
            WaitForExplorerExit()
        Else
            PopupMessage(PopupStr.Explorer_Not_Running)
        End If
    End Sub
    Sub ForceCloseExplorer()
        If IsExplorerRunning() Then
            RunProcessAndGetOutput(TaskKillPath, "/F /IM Explorer.exe", True)
            WaitForExplorerExit()
        Else
            PopupMessage(PopupStr.Explorer_Not_Running)
        End If
    End Sub
    Sub WaitForExplorerExit(Optional Timeout% = 5000)
        Dim sw = Stopwatch.StartNew
        While IsExplorerRunning() AndAlso sw.ElapsedMilliseconds < Timeout
            Threading.Thread.Sleep(50)
        End While
    End Sub
    Sub WaitForExplorerStart(Optional Timeout% = 5000)
        Dim sw = Stopwatch.StartNew
        While Not IsExplorerRunning() AndAlso sw.ElapsedMilliseconds < Timeout
            Threading.Thread.Sleep(50)
        End While
    End Sub
    Sub OpenFolder(FolderPath$)
        If IsExplorerRunning() Then Process.Start(ExplorerPath, FolderPath)
    End Sub
End Module
