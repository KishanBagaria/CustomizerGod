Imports System.Windows
Imports System

Module Popups
    Function mwHandle() As IntPtr
        Return If(mw IsNot Nothing, mw.WindowHandle, IntPtr.Zero)
    End Function
    Sub PopupError(Text$)
        Popup(Text, TaskDialogIcon.SecurityError, MessageBoxImage.Error)
    End Sub
    Sub PopupMessage(Text$)
        Popup(Text, TaskDialogIcon.Information, MessageBoxImage.Information)
    End Sub
    Sub Popup(Text$, TDI As TaskDialogIcon, MBI As MessageBoxImage)
#If Not Debug OrElse Code_Analysis Then
        Try
            If TaskDialog(mwHandle, IntPtr.Zero, AppName, If(TDI = TaskDialogIcon.SecurityError, "Error", Text), If(TDI = TaskDialogIcon.SecurityError, Text, ""), TaskDialogButton.OK, TDI, Nothing) <> 0 Then Throw New EntryPointNotFoundException
        Catch e As EntryPointNotFoundException
#End If
            MessageBox.Show(Text, AppName, MessageBoxButton.OK, MBI)
#If Not Debug OrElse Code_Analysis Then
        End Try
#End If
    End Sub
    Sub Popup(Heading$, Text$, TDI As TaskDialogIcon, MBI As MessageBoxImage)
#If Not Debug OrElse Code_Analysis Then
        Try
            If TaskDialog(mwHandle, IntPtr.Zero, AppName, Heading, Text, TaskDialogButton.OK, TDI, Nothing) <> 0 Then Throw New EntryPointNotFoundException
        Catch e As EntryPointNotFoundException
#End If
            MessageBox.Show(Heading & DoubleNewLine & Text, AppName, MessageBoxButton.OK, MBI)
#If Not Debug OrElse Code_Analysis Then
        End Try
#End If
    End Sub
    Function Question(Text$, Optional Heading$ = AppName) As Boolean
#If Not Debug OrElse Code_Analysis Then
        Try
            Dim PushedButton%
            Dim ReturnCode%
            If Heading = AppName Then
                ReturnCode = TaskDialog(mwHandle, IntPtr.Zero, AppName, Text, "", TaskDialogButton.Yes Or TaskDialogButton.No, 0, PushedButton)
            Else
                ReturnCode = TaskDialog(mwHandle, IntPtr.Zero, AppName, Heading, Text, TaskDialogButton.Yes Or TaskDialogButton.No, 0, PushedButton)
            End If
            If ReturnCode <> 0 Then Throw New EntryPointNotFoundException
            Return PushedButton = MessageBoxResult.Yes
        Catch e As EntryPointNotFoundException
#End If
            Return MessageBox.Show(Text, Heading, MessageBoxButton.YesNo, MessageBoxImage.Question) = MessageBoxResult.Yes
#If Not Debug OrElse Code_Analysis Then
        End Try
#End If
    End Function
    Sub AskForRestart()
        If Question(PopupStr.Ask_For_Restart) Then ExitWindowsEx(ExitWindowsExFlags.EWX_REBOOT, 0)
    End Sub
End Module
