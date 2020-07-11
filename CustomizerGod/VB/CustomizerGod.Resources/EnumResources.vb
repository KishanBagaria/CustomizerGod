Imports System
Imports System.Collections.Generic
Imports System.Runtime.InteropServices
Namespace Resources
    Module EnumResources
        ReadOnly ResNames, ResTypes As New List(Of String)
        Function GetResourceIDs(FilePath$, Type$) As IEnumerable(Of String)
            ResNames.Clear()
            Using SLH = New SafeLibraryHandle(FilePath)
                EnumResourceNames(SLH, Type, Function(hModule, lpszType, lpszName, lParam) GetStringFromPointerOrInteger(lpszName, ResNames), IntPtr.Zero)
            End Using
            Return ResNames
        End Function
        Function GetResourceTypes(FilePath$) As IEnumerable(Of String)
            ResTypes.Clear()
            Using SLH = New SafeLibraryHandle(FilePath)
                EnumResourceTypes(SLH, Function(hModule, lpszType, lParam) GetStringFromPointerOrInteger(lpszType, ResTypes), IntPtr.Zero)
            End Using
            Return ResTypes
        End Function
        Function GetStringFromPointerOrInteger(p As IntPtr, List As IList(Of String)) As Boolean
            If p.ToInt64 > UShort.MaxValue Then
                List.Add(Marshal.PtrToStringAuto(p))
            Else
                List.Add("#" & CStr(p))
            End If
            Return True
        End Function
    End Module
End Namespace