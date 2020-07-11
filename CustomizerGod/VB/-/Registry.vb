Imports Microsoft.Win32
Imports System.Collections.Generic

Module RegistryHelpers
    ReadOnly RegOriginalValues As New Dictionary(Of RegistryValue, Object)
    Structure RegistryValue
        Dim RootKey As RegistryKey, SubKeyName$, ValueName$
        Sub New(Root As RegistryKey, SubKeyName$, ValueName$)
            Me.RootKey = Root
            Me.SubKeyName = SubKeyName
            Me.ValueName = ValueName
        End Sub
    End Structure
    Sub RegTempChange(RootKey As RegistryKey, SubKeyName$, ValueName$, Value As Object, ValueKind As RegistryValueKind)
        Using Key = RootKey.OpenSubKey(SubKeyName, True)
            Dim RV = New RegistryValue(RootKey, SubKeyName$, ValueName$)
            If Not RegOriginalValues.ContainsKey(RV) Then RegOriginalValues(RV) = Key.GetValue(ValueName, Nothing)
            Key.SetValue(ValueName, Value, ValueKind)
        End Using
    End Sub
    Sub RegTempRevert()
        For Each RegValue In RegOriginalValues
            Using Key = RegValue.Key.RootKey.OpenSubKey(RegValue.Key.SubKeyName, True)
                If RegValue.Value Is Nothing Then Key.DeleteValue(RegValue.Key.ValueName) Else Key.SetValue(RegValue.Key.ValueName, RegValue.Value)
            End Using
        Next
        RegOriginalValues.Clear()
    End Sub
End Module
