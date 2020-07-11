Imports System
Imports System.Runtime.InteropServices
Imports Microsoft.Win32.SafeHandles

Module NativeMethods
    <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True, BestFitMapping:=False, ThrowOnUnmappableChar:=True)> Function LoadLibraryEx(lpFileName$, hFile As IntPtr, dwFlags As UInteger) As IntPtr
    End Function
    <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True)> Function FreeLibrary(hModule As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function
    <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True, BestFitMapping:=False, ThrowOnUnmappableChar:=True)> Function FindResource(hModule As SafeLibraryHandle, lpName$, lpType$) As IntPtr
    End Function
    <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True)> Function LoadResource(hModule As SafeLibraryHandle, hResInfo As IntPtr) As IntPtr
    End Function
    <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True)> Function SizeofResource(hModule As SafeLibraryHandle, hResInfo As IntPtr) As UInteger
    End Function
    <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True)> Function LockResource(hResData As IntPtr) As IntPtr
    End Function
    <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True, BestFitMapping:=False, ThrowOnUnmappableChar:=True)> Function EnumResourceNames(hModule As SafeLibraryHandle, lpszType$, lpEnumFunc As EnumResNameProc, lParam As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function
    <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True)> Function EnumResourceTypes(hModule As SafeLibraryHandle, lpEnumFunc As EnumResTypeProc, lParam As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function
    <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True, BestFitMapping:=False, ThrowOnUnmappableChar:=True)> Function BeginUpdateResource(pFileName$, <MarshalAs(UnmanagedType.Bool)> bDeleteExistingResources As Boolean) As IntPtr
    End Function
    <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True)> Function UpdateResource(hUpdate As IntPtr, lpType As IntPtr, lpName As IntPtr, wLanguage As UShort, lpData() As Byte, cbData As UInteger) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function
    <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True)> Function UpdateResource(hUpdate As IntPtr, lpType As IntPtr, lpName As IntPtr, wLanguage As UShort, lpData As IntPtr, cbData As UInteger) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function
    <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True, BestFitMapping:=False, ThrowOnUnmappableChar:=True)> Function UpdateResource(hUpdate As IntPtr, lpType$, lpName$, wLanguage As UShort, lpData() As Byte, cbData As UInteger) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function
    <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True, BestFitMapping:=False, ThrowOnUnmappableChar:=True)> Function UpdateResource(hUpdate As IntPtr, lpType As IntPtr, lpName$, wLanguage As UShort, lpData() As Byte, cbData As UInteger) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function
    <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True, BestFitMapping:=False, ThrowOnUnmappableChar:=True)> Function UpdateResource(hUpdate As IntPtr, lpType$, lpName As IntPtr, wLanguage As UShort, lpData() As Byte, cbData As UInteger) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function
    <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True, BestFitMapping:=False, ThrowOnUnmappableChar:=True)> Function UpdateResource(hUpdate As IntPtr, lpType$, lpName As IntPtr, wLanguage As UShort, lpData As IntPtr, cbData As UInteger) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function
    <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True)> Function EndUpdateResource(hUpdate As IntPtr, <MarshalAs(UnmanagedType.Bool)> fDiscard As Boolean) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function
    <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True, BestFitMapping:=False, ThrowOnUnmappableChar:=True)> Function CreateFile(lpFileName$, dwDesiredAccess%, dwShareMode As IO.FileShare, lpSecurityAttributes As IntPtr, dwCreationDisposition As IO.FileMode, dwFlagsAndAttributes%, hTemplateFile As IntPtr) As SafeFileHandle
    End Function
    <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True, BestFitMapping:=False, ThrowOnUnmappableChar:=True)> Function CreateFileMapping(hFile As SafeFileHandle, lpAttributes As IntPtr, flProtect As UInteger, dwMaximumSizeHigh As UInteger, dwMaximumSizeLow As UInteger, lpName$) As IntPtr
    End Function
    <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True)> Function MapViewOfFile(hFileMappingObject As IntPtr, dwDesiredAccess As UInteger, dwFileOffsetHigh As UInteger, dwFileOffsetLow As UInteger, dwNumerOfBytesToMap As UIntPtr) As IntPtr
    End Function
    <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True)> Function UnmapViewOfFile(lpBaseAddress As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function
    <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True)> Function CloseHandle(hObject As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function
    <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True, BestFitMapping:=False, ThrowOnUnmappableChar:=True)> Function CreateSymbolicLink(lpSymlinkFileName$, lpTargetFileName$, dwFlags As UInteger) As <MarshalAs(UnmanagedType.U1)> Boolean
    End Function
    <DllImport("kernel32.dll", CharSet:=CharSet.Auto, BestFitMapping:=False, ThrowOnUnmappableChar:=True)> Function RegisterApplicationRestart%(pwzCommandline$, dwFlags As UInteger)
    End Function
    <DllImport("kernel32.dll", CharSet:=CharSet.Auto, BestFitMapping:=False, ThrowOnUnmappableChar:=True)> Function MoveFileEx(lpExistingFileName$, lpNewFileName$, dwFlags As UInteger) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function
    <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True)> Function GetFileSizeEx(hFile As SafeFileHandle, ByRef lpFileSize As LargeInteger) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True, BestFitMapping:=False, ThrowOnUnmappableChar:=True)> Function LoadImage(hinst As SafeLibraryHandle, lpszName$, uType As LoadImageType, cxDesired%, cyDesired%, fuLoad As LoadImageOptions) As IntPtr
    End Function
    <DllImport("user32.dll", CharSet:=CharSet.Auto)> Function DestroyIcon(hIcon As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function
    <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True, BestFitMapping:=False, ThrowOnUnmappableChar:=True)> Function FindWindow(lpClassName$, lpWindowName$) As IntPtr
    End Function
    <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)> Function PostMessage(hWnd As IntPtr, Msg As UInteger, wParam As IntPtr, lParam As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function
    <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)> Function SendMessage(hWnd As IntPtr, Msg As UInteger, wParam As IntPtr, lParam As IntPtr) As IntPtr
    End Function
    <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)> Function GetWindowRect(hWnd As IntPtr, ByRef lpRect As Rect) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function
    <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)> Function ExitWindowsEx(uFlags As ExitWindowsExFlags, dwReason As UInteger) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    <DllImport("dwmapi.dll", CharSet:=CharSet.Auto, SetLastError:=True)> Function DwmIsCompositionEnabled%(<MarshalAs(UnmanagedType.Bool)> ByRef pfEnabled As Boolean)
    End Function
    <DllImport("dwmapi.dll", CharSet:=CharSet.Auto, SetLastError:=True)> Function DwmExtendFrameIntoClientArea%(hWnd As IntPtr, ByRef pMarInset As Margins)
    End Function
    <DllImport("dwmapi.dll", CharSet:=CharSet.Auto, SetLastError:=True)> Function DwmGetColorizationColor%(ByRef pcrColorization As UInteger, <MarshalAs(UnmanagedType.Bool)> ByRef pfOpaqueBlend As Boolean)
    End Function
    <DllImport("dwmapi.dll", CharSet:=CharSet.Auto)> Function DwmDefWindowProc(hwnd As IntPtr, msg As UInteger, wParam As IntPtr, lParam As IntPtr, ByRef plResult As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function
    <DllImport("uxtheme.dll", CharSet:=CharSet.Auto, BestFitMapping:=False, ThrowOnUnmappableChar:=True)> Function SetWindowTheme%(hwnd As IntPtr, pszSubAppName$, pszSubIdList$)
    End Function
    <DllImport("imagehlp.dll", CharSet:=CharSet.Auto, SetLastError:=True)> Function CheckSumMappedFile(BaseAddress As IntPtr, FileLength As UInteger, ByRef HeaderSum As UInteger, ByRef CheckSum As UInteger) As IntPtr
    End Function
    <DllImport("comctl32.dll", CharSet:=CharSet.Auto, SetLastError:=True, BestFitMapping:=False, ThrowOnUnmappableChar:=True)> Function TaskDialog%(hWndParent As IntPtr, hInstance As IntPtr, pszWindowTitle$, pszMainInstruction$, pszContent$, dwCommonButtons As TaskDialogButton, pszIcon As TaskDialogIcon, ByRef pnButton%)
    End Function
    <DllImport("gdi32.dll", CharSet:=CharSet.Auto)> Function DeleteObject(hObject As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function
    <DllImport("sfc.dll", CharSet:=CharSet.Auto, SetLastError:=True, BestFitMapping:=False, ThrowOnUnmappableChar:=True)> Function SfcIsFileProtected(RpcHandle As IntPtr, ProtFileName$) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    Delegate Function EnumChildProc(hwnd As IntPtr, lParam As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    Delegate Function EnumResNameProc(hModule As IntPtr, <MarshalAs(UnmanagedType.LPTStr)> lpszType$, lpszName As IntPtr, lParam As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    Delegate Function EnumResTypeProc(hModule As IntPtr, lpszType As IntPtr, lParam As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
End Module