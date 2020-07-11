Imports System
Imports System.Runtime.InteropServices

Module Windows10Blur
    <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)> Function SetWindowCompositionAttribute%(hwnd As IntPtr, ByRef data As WindowCompositionAttributeData)
    End Function
    <StructLayout(LayoutKind.Sequential)> Structure WindowCompositionAttributeData
        Public Attribute As WindowCompositionAttribute
        Public Data As IntPtr
        Public SizeOfData As Integer
    End Structure
    <StructLayout(LayoutKind.Sequential)> Structure AccentPolicy
        Public AccentState As AccentState
        Public AccentFlags As Integer
        Public GradientColor As Integer
        Public AnimationId As Integer
    End Structure
    Enum WindowCompositionAttribute
        WCA_ACCENT_POLICY = 19
    End Enum
    Enum AccentState
        ACCENT_DISABLED = 0
        ACCENT_ENABLE_GRADIENT = 1
        ACCENT_ENABLE_TRANSPARENTGRADIENT = 2
        ACCENT_ENABLE_BLURBEHIND = 3
        ACCENT_INVALID_STATE = 4
    End Enum
    Sub ChangeWindowCompositionAttribute(WindowsHandle As IntPtr)
        Dim Accent = New AccentPolicy() With {.AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND}
        Dim AccentStructSize = Marshal.SizeOf(Accent)
        Dim PointerToAccent = Marshal.AllocHGlobal(AccentStructSize)
        Marshal.StructureToPtr(Accent, PointerToAccent, False)
        SetWindowCompositionAttribute(WindowsHandle, New WindowCompositionAttributeData() With {
            .Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY,
            .SizeOfData = AccentStructSize,
            .Data = PointerToAccent
        })
        Marshal.FreeHGlobal(PointerToAccent)
    End Sub
    '<DllImport("uxtheme.dll", CharSet:=CharSet.Auto, SetLastError:=True, EntryPoint:="#95")> Function GetImmersiveColorFromColorSetEx(dwImmersiveColorSet As UInteger, dwImmersiveColorType As UInteger, bIgnoreHighContrast As Boolean, dwHighContrastCacheMode As UInteger) As UInteger
    'End Function
    '<DllImport("uxtheme.dll", CharSet:=CharSet.Auto, SetLastError:=True, EntryPoint:="#96")> Function GetImmersiveColorTypeFromName(pName As IntPtr) As UInteger
    'End Function
    '<DllImport("uxtheme.dll", CharSet:=CharSet.Auto, SetLastError:=True, EntryPoint:="#98")> Function GetImmersiveUserColorSetPreference(bForceCheckRegistry As Boolean, bSkipCheckOnFail As Boolean) As Integer
    'End Function
    'Function GetAccentColor() As Windows.Media.Color
    '    Dim ColorSet = GetImmersiveUserColorSetPreference(False, False)
    '    Dim ElementName = Marshal.StringToHGlobalUni("ImmersiveStartSelectionBackground")
    '    Dim Type = GetImmersiveColorTypeFromName(ElementName)
    '    Marshal.FreeCoTaskMem(ElementName)
    '    Dim ColorDWORD = GetImmersiveColorFromColorSetEx(CUInt(ColorSet), Type, False, 0)
    '    Dim ColorBytes = {CByte((&HFF000000UI And ColorDWORD) >> 24),
    '                      CByte((&HFF0000 And ColorDWORD) >> 16),
    '                      CByte((&HFF00 And ColorDWORD) >> 8),
    '                      CByte(&HFF And ColorDWORD)}
    '    Return Windows.Media.Color.FromArgb(ColorBytes(0), ColorBytes(3), ColorBytes(2), ColorBytes(1))
    'End Function
End Module
