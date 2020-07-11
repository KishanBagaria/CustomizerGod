Imports System

Module NativeEnums
    <Flags> Enum FileOS As UInteger
        VOS_DOS = &H00010000
        VOS_NT = &H00040000
        VOS__WINDOWS16 = &H00000001
        VOS__WINDOWS32 = &H00000004
        VOS_OS216 = &H00020000
        VOS_OS232 = &H00030000
        VOS__PM16 = &H00000002
        VOS__PM32 = &H00000003
        VOS_UNKNOWN = &H00000000
        VOS_DOS_WINDOWS16 = &H00010001
        VOS_DOS_WINDOWS32 = &H00010004
        VOS_NT_WINDOWS32 = &H00040004
        VOS_OS216_PM16 = &H00020002
        VOS_OS232_PM32 = &H00030003
    End Enum
    <Flags> Enum FileFlags As UInteger
        VS_FF_DEBUG = &H00000001
        VS_FF_INFOINFERRED = &H00000010
        VS_FF_PATCHED = &H00000004
        VS_FF_PRERELEASE = &H00000002
        VS_FF_PRIVATEBUILD = &H00000008
        VS_FF_SPECIALBUILD = &H00000020
    End Enum
    <Flags> Enum FileType As UInteger
        VFT_APP = &H00000001
        VFT_DLL = &H00000002
        VFT_DRV = &H00000003
        VFT_FONT = &H00000004
        VFT_STATIC_LIB = &H00000007
        VFT_UNKNOWN = &H00000000
        VFT_VXD = &H00000005
    End Enum
    <Flags> Enum FileSubtype As UInteger
        VFT2_DRV_COMM = &H0000000A
        VFT2_DRV_DISPLAY = &H00000004
        VFT2_DRV_INSTALLABLE = &H00000008
        VFT2_DRV_KEYBOARD = &H00000002
        VFT2_DRV_LANGUAGE = &H00000003
        VFT2_DRV_MOUSE = &H00000005
        VFT2_DRV_NETWORK = &H00000006
        VFT2_DRV_PRINTER = &H00000001
        VFT2_DRV_SOUND = &H00000009
        VFT2_DRV_SYSTEM = &H00000007
        VFT2_DRV_VERSIONED_PRINTER = &H0000000C
        VFT2_UNKNOWN = &H00000000
        VFT2_FONT_RASTER = &H00000001
        VFT2_FONT_TRUETYPE = &H00000003
        VFT2_FONT_VECTOR = &H00000002
    End Enum
    <Flags> Enum MemoryFlags As UShort
        MOVEABLE = &H10
        FIXED = Not MOVEABLE
        PURE = &H20
        IMPURE = Not PURE
        PRELOAD = &H40
        LOADONCALL = Not PRELOAD
        DISCARDABLE = &H1000
    End Enum
    <Flags> Enum DllCharacteristics As UShort
        RESERVEDx0001 = &H0001
        RESERVEDx0002 = &H0002
        RESERVEDx0004 = &H0004
        RESERVEDx0008 = &H0008
        DYNAMIC_BASE = &H0040
        FORCE_INTEGRITY = &H0080
        NX_COMPAT = &H0100
        NO_ISOLATION = &H0200
        NO_SEH = &H0400
        NO_BIND = &H0800
        RESERVEDx1000 = &H1000
        WDM_DRIVER = &H2000
        RESERVEDx4000 = &H4000
        TERMINAL_SERVER_AWARE = &H8000
    End Enum
    Enum ExitWindowsExFlags As UInteger
        EWX_HYBRID_SHUTDOWN = &H400000
        EWX_LOGOFF = 0
        EWX_POWEROFF = 8
        EWX_REBOOT = 2
        EWX_RESTARTAPPS = 64
        EWX_SHUTDOWN = 1
        EWX_FORCE = 4
        EWX_FORCEIFHUNG = 16
    End Enum
    Enum FileMapAccess As UInteger
        FILE_MAP_COPY = 1
        FILE_MAP_WRITE = 2
        FILE_MAP_READ = 4
        FILE_MAP_ALL_ACCESS = 31
        FILE_MAP_EXECUTE = 32
    End Enum
    Enum FileMapProtection As UInteger
        PAGE_EXECUTE_READ = 32
        PAGE_EXECUTE_READWRITE = 64
        PAGE_EXECUTE_WRITECOPY = 128
        PAGE_READONLY = 2
        PAGE_READWRITE = 4
        PAGE_WRITECOPY = 8
        SEC_COMMIT = &H8000000
        SEC_IMAGE = &H1000000
        SEC_IMAGE_NO_EXECUTE = &H11000000
        SEC_LARGE_PAGES = &H80000000UI
        SEC_NOCACHE = &H10000000
        SEC_RESERVE = &H4000000
        SEC_WRITECOMBINE = &H40000000
    End Enum
    <Flags> Enum AccelTableEntryFlags As UShort
        FVIRTKEY = 0
        FNOINVERT = 2
        FSHIFT = 4
        FCONTROL = 8
        FALT = 16
        FLAST = 128
    End Enum
    Enum LoadImageType As UInteger
        IMAGE_BITMAP = 0
        IMAGE_CURSOR = 2
        IMAGE_ICON = 1
    End Enum
    Enum LoadImageOptions As UInteger
        LR_CREATEDIBSECTION = 8192
        LR_DEFAULTCOLOR = 0
        LR_DEFAULTSIZE = 64
        LR_LOADFROMFILE = 16
        LR_LOADMAP3DCOLORS = 4096
        LR_LOADTRANSPARENT = 32
        LR_MONOCHROME = 1
        LR_SHARED = 32768
        LR_VGACOLOR = 128
    End Enum
    Enum NonClient
        HTBORDER = 18
        HTBOTTOM = 15
        HTBOTTOMLEFT = 16
        HTBOTTOMRIGHT = 17
        HTCAPTION = 2
        HTCLIENT = 1
        HTCLOSE = 20
        HTERROR = -2
        HTGROWBOX = 4
        HTHELP = 21
        HTHSCROLL = 6
        HTLEFT = 10
        HTMENU = 5
        HTMAXBUTTON = 9
        HTMINBUTTON = 8
        HTNOWHERE = 0
        HTREDUCE = 8
        HTRIGHT = 11
        HTSIZE = 4
        HTSYSMENU = 3
        HTTOP = 12
        HTTOPLEFT = 13
        HTTOPRIGHT = 14
        HTTRANSPARENT = -1
        HTVSCROLL = 7
        HTZOOM = 9
    End Enum
    Enum TaskDialogButton
        OK = 1
        Yes = 2
        No = 4
        Cancel = 8
        Retry = 16
        Close = 32
    End Enum
    Enum TaskDialogIcon
        Information = UShort.MaxValue - 2
        Warning = UShort.MaxValue
        [Stop] = UShort.MaxValue - 1
        SecurityWarning = UShort.MaxValue - 5
        SecurityError = UShort.MaxValue - 6
        SecuritySuccess = UShort.MaxValue - 7
        SecurityShield = UShort.MaxValue - 3
        SecurityShieldBlue = UShort.MaxValue - 4
        SecurityShieldGray = UShort.MaxValue - 8
    End Enum
End Module
