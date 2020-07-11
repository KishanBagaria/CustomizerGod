Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Globalization
Imports System.Windows.Forms
Imports System.Collections.Generic

Module Common
    Friend Const AppName = "CustomizerGod",
                 AppVersion = "1.7.7",
                 d2w = "http://www.door2windows.com/",
                 AppServices = "https://a.kishan.info/",
                 FeedbackURL = AppServices & "f/",
                 ExceptionURL = AppServices & "e/",
                 AppLink = d2w & "customizergod/",
                 DeveloperEmail = "mailto:hi@kishan.info"

    Friend ReadOnly FBD As New Lazy(Of SelectFolderDialog),
                    OSVersion As Version = Environment.OSVersion.Version,
                    WindowsV% = CInt(OSVersion.Major & "" & OSVersion.Minor),
                    DoubleNewLine$ = Environment.NewLine & Environment.NewLine,
                    LF As Char = Convert.ToChar(10),
                    FilterImage$ = "Image Files (*.bmp, *.gif, *.jpg, *.png, *.ico)|*.bmp;*.gif;*.jpg;*.png;*.ico",
                    FilterPE$ = "Portable Executable Files|*.exe;*.dll;*.scr;*.cpl;*.ocx;*.sys;*.msstyles;*.mui|All Files|*",
                    FilterRC$ = "RC Files|*.rc",
                    FilterRES$ = "RES Files|*.res",
                    FilterEXE$ = "EXE Files|*.exe"

    Friend mw As MainWindow
    Friend ImageResizingMode As ResizeMode = ResizeMode.Fit
    Friend ImageResamplingMode As Drawing.Drawing2D.InterpolationMode = Drawing.Drawing2D.InterpolationMode.HighQualityBicubic

    ReadOnly OFDs As New Dictionary(Of String, OpenFileDialog)
    Function FileDialog(Filter$) As OpenFileDialog
        If Not OFDs.ContainsKey(Filter) Then OFDs.Add(Filter, New OpenFileDialog With {.Filter = Filter})
        Return OFDs(Filter)
    End Function

    Sub OpenURL(URL$)
        Try
            Process.Start(URL)
        Catch DefaultBrowserNotSet As Win32Exception
            Try
                Process.Start("iexplore", URL)
            Catch IEUninstalled As Win32Exception
            End Try
        End Try
    End Sub
    Function StrFormat$(Format$, ParamArray Args As Object())
        Return String.Format(CultureInfo.CurrentCulture, Format, Args)
    End Function
    Function StrInvFormat$(Format$, ParamArray Args As Object())
        Return String.Format(CultureInfo.InvariantCulture, Format, Args)
    End Function
End Module
