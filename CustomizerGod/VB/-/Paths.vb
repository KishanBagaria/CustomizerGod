Imports System
Imports System.Runtime.CompilerServices
Imports System.Text

Module Paths
    Friend ReadOnly Sys32DirPath$ = Environment.SystemDirectory.TrimTrailingSlash,
                    WindowsDirPath$ = Environment.GetFolderPath(Environment.SpecialFolder.Windows).TrimTrailingSlash,
                    TempDirPath$ = GetTempDirPath(),
                    AppDirPath$ = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData).TrimTrailingSlash & "\D83C629D-C688-4A07-8615-94974D65F157",
                    ExceptionDirPath$ = AppDirPath & "\6E8D6B19-A55D-4AE3-8986-A29F363D9E8A",
                    OfflineCalloutPath$ = AppDirPath & "\47E47EA8-82D0-4166-A58D-4CC7C88D86D3",
                    SysDriveLetter$ = Sys32DirPath(0),
                    ExplorerPath$ = WindowsDirPath & "\explorer.exe",
                    CmdPath$ = Sys32DirPath & "\cmd.exe",
                    TakeOwnPath$ = Sys32DirPath & "\takeown.exe",
                    IcaclsPath$ = Sys32DirPath & "\icacls.exe",
                    TaskKillPath$ = Sys32DirPath & "\taskkill.exe",
                    TsKillPath$ = Sys32DirPath & "\tskill.exe",
                    Kernel32Path$ = Sys32DirPath & "\kernel32.dll",
                    IE4UInitPath$ = Sys32DirPath & "\ie4uinit.exe",
                    IconCachePath$ = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData).TrimTrailingSlash & "\IconCache.db"

    Function GetTempDirPath$()
        Dim Path = IO.Path.GetTempPath.TrimTrailingSlash & "\62C7F226-6F79-4B04-971D-9ACF966D07EA"
        If WindowsDirPath(0) <> Path(0) Then Path = IO.Path.Combine(WindowsDirPath, AppName)
        Return Path
    End Function
    ReadOnly Property PsExecPath$
        Get
            Dim Path = AppDirPath & "\8F57FB2F-8BF8-455F-B15F-66DA067F8371.exe"
            If Not IO.File.Exists(Path) Then IO.File.WriteAllBytes(Path, Res.PsExec)
            Return Path
        End Get
    End Property
    ReadOnly Property _7zaPath$
        Get
            Dim Path = AppDirPath & "\47E47EA8-82D0-4166-A58D-4CC7C88D86D3.exe"
            If Not IO.File.Exists(Path) Then IO.File.WriteAllBytes(Path, Res._7za)
            Return Path
        End Get
    End Property
    Function Base64Encode$(Str$)
        Return Convert.ToBase64String(Encoding.UTF8.GetBytes(Str))
    End Function
    <Extension> Function TrimTrailingSlash$(Path$)
        Return Path.TrimEnd({IO.Path.DirectorySeparatorChar, IO.Path.AltDirectorySeparatorChar})
    End Function
    Function GetFileLnkPath(FilePath$) As String
        Dim FileDirPath = IO.Path.GetDirectoryName(FilePath)
        Dim LnkDirPath = TempDirPath & "\" & Base64Encode(FileDirPath)
        If Not IO.Directory.Exists(LnkDirPath) Then
            If Not CreateSymbolicLink(LnkDirPath, FileDirPath, 1) Then Throw New NativeException("CreateSymbolicLink") '1 = SYMBOLIC_LINK_FLAG_DIRECTORY
        End If
        Return FilePath.Replace(FileDirPath, LnkDirPath)
    End Function
    Function GetNumberedFilePath$(FolderPath$, FileName$, Extension$)
        Dim NewFilePath = IO.Path.Combine(FolderPath, FileName & "." & Extension)
        Dim No%
        While IO.File.Exists(NewFilePath)
            No += 1
            NewFilePath = IO.Path.Combine(FolderPath, $"{FileName} ({No}).{Extension}")
        End While
        Return NewFilePath
    End Function
End Module
