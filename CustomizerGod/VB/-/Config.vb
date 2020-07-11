Imports System
Imports System.Collections.Generic
Imports System.Windows

Module Config
    Friend ReadOnly IconDisplayWidth As Byte = 0,
                    IconDisplayHeight As Byte = 0,
                    BitmapToIconSizes As IEnumerable(Of Integer) = {256, 64, 48, 32, 24, 16},
                    AnimationDuration As New Duration(TimeSpan.FromMilliseconds(160))

End Module
