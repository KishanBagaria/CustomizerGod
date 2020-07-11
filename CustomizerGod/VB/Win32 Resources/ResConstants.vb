Imports System.Collections.Generic

Module ResConstants
    'RT_NEWRESOURCE      0x2000 
    'RT_ERROR            0x7fff 
    'RT_CURSOR           1 
    'RT_BITMAP           2 
    'RT_ICON             3 
    'RT_MENU             4 
    'RT_DIALOG           5 
    'RT_STRING           6 
    'RT_FONTDIR          7 
    'RT_FONT             8 
    'RT_ACCELERATORS     9 
    'RT_RCDATA           10 
    'RT_MESSAGETABLE     11 
    'RT_GROUP_CURSOR     12 
    'RT_GROUP_ICON       14 
    'RT_VERSION          16 
    'RT_NEWBITMAP        (RT_BITMAP|RT_NEWRESOURCE) 
    'RT_NEWMENU          (RT_MENU|RT_NEWRESOURCE) 
    'RT_NEWDIALOG        (RT_DIALOG|RT_NEWRESOURCE) 
    'Missing: 13,15,18

    Friend Const RT_CURSOR = "#1",
                 RT_BITMAP = "#2",
                 RT_ICON = "#3",
                 RT_MENU = "#4",
                 RT_DIALOG = "#5",
                 RT_STRING = "#6",
                 RT_FONTDIR = "#7",
                 RT_FONT = "#8",
                 RT_ACCELERATORS = "#9",
                 RT_RCDATA = "#10",
                 RT_MESSAGETABLE = "#11",
                 RT_GROUP_CURSOR = "#12",
                 RT_GROUP_ICON = "#14",
                 RT_VERSION = "#16",
                 RT_DLGINCLUDE = "#17",
                 RT_PLUGPLAY = "#19",
                 RT_VXD = "#20",
                 RT_ANICURSOR = "#21",
                 RT_ANIICON = "#22",
                 RT_HTML = "#23",
                 RT_MANIFEST = "#24"
    Friend ReadOnly ResTypeNativeNames As New Dictionary(Of String, String) From {
        {"#1", "RT_CURSOR"},
        {"#2", "RT_BITMAP"},
        {"#3", "RT_ICON"},
        {"#4", "RT_MENU"},
        {"#5", "RT_DIALOG"},
        {"#6", "RT_STRING"},
        {"#7", "RT_FONTDIR"},
        {"#8", "RT_FONT"},
        {"#9", "RT_ACCELERATORS"},
        {"#10", "RT_RCDATA"},
        {"#11", "RT_MESSAGETABLE"},
        {"#12", "RT_GROUP_CURSOR"},
        {"#14", "RT_GROUP_ICON"},
        {"#16", "RT_VERSION"},
        {"#17", "RT_DLGINCLUDE"},
        {"#19", "RT_PLUGPLAY"},
        {"#20", "RT_VXD"},
        {"#21", "RT_ANICURSOR"},
        {"#22", "RT_ANIICON"},
        {"#23", "RT_HTML"},
        {"#24", "RT_MANIFEST"}
    }, ResTypeFriendlyNames As New Dictionary(Of String, String) From {
        {"#1", "Cursor"},
        {"#2", "Bitmap"},
        {"#3", "Icons (Individual)"},
        {"#4", "Menu"},
        {"#5", "Dialog Box"},
        {"#6", "String Table"},
        {"#7", "Font Directory"},
        {"#8", "Font"},
        {"#9", "Accelerator Table"},
        {"#10", "RC Data"},
        {"#11", "Message Table"},
        {"#12", "Cursors"},
        {"#14", "Icons"},
        {"#16", "Version"},
        {"#17", "Dialog Include"},
        {"#19", "Plug and Play"},
        {"#20", "VxD"},
        {"#21", "Animated Cursor"},
        {"#22", "Animated Icon"},
        {"#23", "HTML"},
        {"#24", "Assembly Manifest"}
    }, NativeResFormats As New Dictionary(Of String, ResFormat?) From {
        {RT_BITMAP, ResFormat.BMP},
        {RT_GROUP_ICON, ResFormat.ICO},
        {RT_ICON, Nothing}
    }
    '{RT_MANIFEST, ResFormat.Text},
    '{RT_ACCELERATORS, ResFormat.AcceleratorTable},
    '{RT_STRING, ResFormat.StringTable},
    '{RT_MESSAGETABLE, ResFormat.MessageTable},
    '{RT_VERSION, ResFormat.Version},
End Module
