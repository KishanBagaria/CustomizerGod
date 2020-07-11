Imports System
'Imports System.Windows.Forms
'Imports System.Linq
'Imports System.Collections.Generic

Class About
    'Shared Function Shuffle(Of T)(l As IList(Of T)) As IList(Of T)
    '    Dim rng = New Random
    '    Dim n = l.Count
    '    While n > 1
    '        n -= 1
    '        Dim k = rng.Next(n + 1)
    '        Dim value = l(k)
    '        l(k) = l(n)
    '        l(n) = value
    '    End While
    '    Return l
    'End Function
    Sub New()
        InitializeComponent()
        lVersionNo.Text = AppVersion
        'lLicensedTo.Visible = False
        'lLicenseNameEmail.Visible = False
        'Dim EmptyPadding = New Padding(0)
        'For Each p In Shuffle({"GuardianMajor", "SpringsTS", "burnsplayguitar", "4nt1p0p", "neiio", "esnooze", "cathycatchy"})
        '    Dim ll As New LinkLabel With {.Text = p, .AutoSize = True, .Margin = EmptyPadding}
        '    AddHandler ll.Click, Sub() OpenURL("http://" & p & ".deviantart.com/")
        '    flp.Controls.Add(ll)
        'Next
        'lLicenseNameEmail.Text = LicenseName & " · " & LicenseEmail
        'Width += Math.Max(0, lLicenseNameEmail.Width - 260)
        AddHandler lld2w.LinkClicked, Sub() OpenURL(d2w)
        AddHandler llHowToUse.LinkClicked, Sub() OpenURL(AppLink)
        AddHandler bOK.Click, Sub() Close()
    End Sub
End Class