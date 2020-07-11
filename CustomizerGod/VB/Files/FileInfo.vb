Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Management.Automation
Module Authenticode
    ReadOnly ri As New RunspaceInvoke
    Function GetAuthenticodeSignature(FilePath$) As Signature
        Dim r = ri.Invoke($"Get-AuthenticodeSignature ""{FilePath}""")
        Return DirectCast(r(0).BaseObject, Signature)
    End Function
End Module
Module FileInfo
    Function GetProperties(Of T)(o As T) As Dictionary(Of String, String)
        Dim d = New Dictionary(Of String, String)
        For Each p In GetType(T).GetProperties
            Dim v = p.GetValue(o, Nothing)
            d.Add(p.Name, If(v IsNot Nothing, v.ToString, ""))
        Next
        Return d
    End Function
    Function GetFileInfo(FilePath$) As Dictionary(Of String, String)
        Dim FI = FileVersionInfo.GetVersionInfo(FilePath)
        Dim d = GetProperties(FI)
        d.Remove(NameOf(FI.FileMajorPart))
        d.Remove(NameOf(FI.FileMinorPart))
        d.Remove(NameOf(FI.FileBuildPart))
        d.Remove(NameOf(FI.FilePrivatePart))
        d.Remove(NameOf(FI.ProductMajorPart))
        d.Remove(NameOf(FI.ProductMinorPart))
        d.Remove(NameOf(FI.ProductBuildPart))
        d.Remove(NameOf(FI.ProductPrivatePart))
        d.Add("FileVersionFormatted", GetFileVersion(FI).ToString)
        d.Add("ProductVersionFormatted", GetProductVersion(FI).ToString)
        Return d
    End Function
    Dim PSAvailable As Boolean = True
    Sub PopupFileInfo(FilePath$)
        Dim FileInfo = GetFileInfo(FilePath)
        Dim Others As New Dictionary(Of String, String) From {
            {"Protected by SFC", SfcIsFileProtected(IntPtr.Zero, FilePath).ToString},
            {"Attributes", IO.File.GetAttributes(FilePath).ToString},
            {"Creation Time", IO.File.GetCreationTime(FilePath).ToString},
            {"Last Access Time", IO.File.GetLastAccessTime(FilePath).ToString},
            {"Last Write Time", IO.File.GetLastWriteTime(FilePath).ToString}
        }
        If PSAvailable Then
            Try
                Dim Sig = GetProperties(GetAuthenticodeSignature(FilePath))
                Sig.Remove(NameOf(Signature.StatusMessage))
                DictionaryViewer.Show({"Version Info", "Digital Signature", ""}, {FileInfo, Sig, Others}, FilePath)
                Exit Sub
            Catch e As Exception When TypeOf e Is TypeInitializationException OrElse TypeOf e Is IO.FileNotFoundException
                PSAvailable = False
            End Try
        End If
        DictionaryViewer.Show({"Version Info", ""}, {FileInfo, Others}, FilePath)
    End Sub
    Function GetFileVersion(VI As FileVersionInfo) As Version
        Return New Version(VI.FileMajorPart, VI.FileMinorPart, VI.FileBuildPart, VI.FilePrivatePart)
    End Function
    Function GetProductVersion(VI As FileVersionInfo) As Version
        Return New Version(VI.ProductMajorPart, VI.ProductMinorPart, VI.ProductBuildPart, VI.ProductPrivatePart)
    End Function
End Module
