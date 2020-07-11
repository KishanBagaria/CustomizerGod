Imports System
Imports System.Reflection
Imports System.Windows.Forms
Class SelectFolderDialog
    ReadOnly OFD As New OpenFileDialog With {.CheckFileExists = False},
        SWF As New ReflectionHelper("System.Windows.Forms"),
        IFileDialogType As Type = SWF.GetPrivateType("FileDialogNative.IFileDialog")
    Function ShowDialog(hWndOwner As IntPtr) As Boolean
        Dim Dialog = SWF.CallPrivate(OFD, "CreateVistaDialog")
        SWF.CallPrivate(OFD, "OnBeforeVistaDialog", Dialog)
        Dim Options = CUInt(SWF.CallPrivate(GetType(FileDialog), OFD, "GetOptions")) Or
            CUInt(SWF.GetEnum("FileDialogNative.FOS", "FOS_PICKFOLDERS"))
        SWF.CallPrivate(IFileDialogType, Dialog, "SetOptions", Options)
        Dim Events = SWF.CallConstructor("FileDialog.VistaDialogEvents", OFD)
        Dim EventCookie As UInteger
        Dim AdviseArray = {Events, EventCookie}
        SWF.CallPrivate(IFileDialogType, Dialog, "Advise", AdviseArray)
        Try
            Return 0 = CInt(SWF.CallPrivate(IFileDialogType, Dialog, "Show", hWndOwner))
        Finally
            SWF.CallPrivate(IFileDialogType, Dialog, "Unadvise", AdviseArray(1))
            GC.KeepAlive(Events)
        End Try
    End Function
    ReadOnly Property SelectedPath$
        Get
            Return OFD.FileName
        End Get
    End Property
End Class
Class ReflectionHelper
    ReadOnly CurrentAssembly As Assembly,
        NS$
    Sub New(NamespaceName$)
        NS = NamespaceName
        For Each an In Assembly.GetExecutingAssembly.GetReferencedAssemblies
            If an.FullName.StartsWith(NamespaceName) Then
                CurrentAssembly = Assembly.Load(an)
                Exit For
            End If
        Next
    End Sub
    Function GetPrivateType(TypeName$) As Type
        Dim type As Type = Nothing
        Dim names = TypeName.Split("."c)
        If names.Length > 0 Then type = CurrentAssembly.[GetType](NS & "." & names(0))
        For i = 1 To names.Length - 1
            type = type.GetNestedType(names(i), BindingFlags.NonPublic)
        Next
        Return type
    End Function
    Function CallConstructor(TypeName$, ParamArray Parameters As Object()) As Object
        For Each ci In GetPrivateType(TypeName).GetConstructors()
            Try
                Return ci.Invoke(Parameters)
            Catch
            End Try
        Next
        Return Nothing
    End Function
    Function CallPrivate(Type As Type, Obj As Object, FunctionName$, ParamArray Parameters As Object()) As Object
        Dim mi = Type.GetMethod(FunctionName, BindingFlags.Instance Or BindingFlags.[Public] Or BindingFlags.NonPublic)
        Return mi.Invoke(Obj, Parameters)
    End Function
    Function CallPrivate(Of T)(Obj As T, FunctionName$, ParamArray Parameters As Object()) As Object
        Dim mi = GetType(T).GetMethod(FunctionName, BindingFlags.Instance Or BindingFlags.[Public] Or BindingFlags.NonPublic)
        Return mi.Invoke(Obj, Parameters)
    End Function
    Function GetEnum(TypeName$, FieldName$) As Object
        Return GetPrivateType(TypeName).GetField(FieldName).GetValue(Nothing)
    End Function
End Class
