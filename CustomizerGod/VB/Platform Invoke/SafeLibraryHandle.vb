Imports System
Imports Microsoft.Win32.SafeHandles

NotInheritable Class SafeLibraryHandle : Inherits SafeHandleZeroOrMinusOneIsInvalid
    Sub New(FilePath$)
        MyBase.New(True)
        SetHandle(LoadLibraryEx(FilePath, IntPtr.Zero, 2))
        If IsInvalid Then
            If Not IO.File.Exists(FilePath) Then Throw New AppException(StrFormat(PopupStr.File_Does_Not_Exist, FilePath))
            Throw New NativeException("LoadLibraryEx " & FilePath)
        End If
    End Sub
    Protected Overrides Function ReleaseHandle() As Boolean
        Return FreeLibrary(handle)
    End Function
End Class