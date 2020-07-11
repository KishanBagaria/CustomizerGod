Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Documents

Module CalloutParser
    Function MakeHyperLink(Text$, URL$) As Hyperlink
        Dim hl = New Hyperlink With {.NavigateUri = New Uri(URL)}
        AddHandler hl.RequestNavigate, AddressOf Hyperlink_NavigateUri
        hl.Inlines.Add(Text)
        Return hl
    End Function
    Sub Hyperlink_NavigateUri(hl As Object, e As Windows.Navigation.RequestNavigateEventArgs)
        OpenURL(e.Uri.AbsoluteUri)
    End Sub
    Function GetInlines(TextToParse$) As IEnumerable(Of Inline)
        Dim Inlines = New List(Of Inline)
        Dim LinkTextStarted = False, LinkURLStarted = False
        Dim Text, URL As New Text.StringBuilder
        For Each c In TextToParse
            If c = "[" Then
                LinkTextStarted = True
                Text.Clear()
            ElseIf c = "]" Then
                LinkTextStarted = False
            ElseIf c = "<" Then
                LinkURLStarted = True
                URL.Clear()
            ElseIf c = ">" Then
                LinkURLStarted = False
                Inlines.Add(MakeHyperLink(Text.ToString, URL.ToString))
            ElseIf LinkTextStarted Then
                Text.Append(c)
            ElseIf LinkURLStarted Then
                URL.Append(c)
            Else
                If Inlines.Count > 0 AndAlso TypeOf Inlines.Last Is Run Then
                    DirectCast(Inlines.Last, Run).Text += c
                Else
                    Inlines.Add(New Run(c))
                End If
            End If
        Next
        Return Inlines
    End Function
End Module
