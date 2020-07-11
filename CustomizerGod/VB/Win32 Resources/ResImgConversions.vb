Imports System
Imports System.Collections.Generic
Imports System.Drawing.Imaging
Imports System.Linq
Imports CustomizerGod.Resources

Module ResImgConversions
    Sub FillResStore(SLH As SafeLibraryHandle, ResStore As IList(Of ResStoreItem), ResItem As ResourceItem, ResFilePath$, DataFilePath$)
        Dim NewResBytes = IO.File.ReadAllBytes(DataFilePath),
            NewResType = GetImageType(NewResBytes)
        If NewResType Is Nothing Then Throw New InvalidFileException(DataFilePath)
        Using p = Picture.FromBytes(NewResBytes)
            FixFormat(p, ResItem.Format)
            Dim ResImgFormat = ResItem.Format.ToImageFormat
            For Each ID In ResItem.IDs
                Dim NewData() As Byte
                If ResItem.Type = RT_GROUP_ICON Then
                    If NewResType.Equals(ImageFormat.Icon) Then
                        NewData = IconFromIcon(ResFilePath, ResStore, NewResBytes, ID)
                    Else
                        NewData = IconFromBitmap(ResFilePath, ResStore, p, ID)
                    End If
                Else
                    Dim ResSize = GetImageSize(GetCompatibleResBytes(SLH, ResItem.Type, ID))
                    If NewResType.Equals(ImageFormat.Icon) Then
                        NewData = BitmapFromIcon(ResSize, ResItem.Resize, ResItem.Format, ResImgFormat, NewResBytes)
                    ElseIf NewResType.Equals(ResImgFormat) AndAlso (ResItem.Resize = False OrElse p.Size = ResSize) Then
                        NewData = NewResBytes
                    ElseIf Not ResItem.Resize Then
                        NewData = p.ToBytes(ResImgFormat)
                    Else
                        Using pCopy = p.GetResizedCopy(ResSize.Width, ResSize.Height, ImageResizingMode, ImageResamplingMode)
                            FixFormat(pCopy, ResItem.Format)
                            NewData = pCopy.ToBytes(ResImgFormat)
                        End Using
                    End If
                    If ResItem.Type = RT_BITMAP Then NewData = NewData.Skip(14).ToArray
                End If
                ResStore.Add(New ResStoreItem(ResItem.Type, ID, NewData))
            Next
        End Using
    End Sub
    Sub FixFormat(p As Picture, ResFormat As ResFormat)
        If p.BitCount <> 32 Then p.RemoveTransparency()
        Select Case ResFormat
            Case ResFormat.PArgbBMP
                p.SetPixelFormat(PixelFormat.Format32bppPArgb)
                p.PremultiplyAlpha()
            Case ResFormat.ArgbBMP
                p.SetPixelFormat(PixelFormat.Format32bppArgb)
            Case ResFormat.RgbBMP
                p.RemoveTransparency()
        End Select
    End Sub
    Function BitmapFromIcon(ResSize As Drawing.Size, Resize As Boolean, ResFormat As ResFormat, ImgFormat As ImageFormat, NewResBytes As Byte()) As Byte()
        If Not Resize Then ResSize = New Drawing.Size(Integer.MaxValue, Integer.MaxValue)
        Dim PrefIDE = (From i In GetIconDirEntries(NewResBytes, 16)
                       Order By If(i(0) = 0, 256, i(0)) Descending
                       Order By i(6) Descending)(0)
        Using PrefIcon = Picture.FromBytes(CreateIconDirFromIconData(PrefIDE, GetIconImage(NewResBytes, PrefIDE)))
            If Resize Then
                Using Resized = PrefIcon.GetResizedCopy(ResSize.Width, ResSize.Height, ImageResizingMode, ImageResamplingMode)
                    FixFormat(Resized, ResFormat)
                    Return Resized.ToBytes(ImgFormat)
                End Using
            Else
                Return PrefIcon.ToBytes(ImgFormat)
            End If
        End Using
    End Function
    Function IconFromBitmap(ResFilePath$, ResStore As IList(Of ResStoreItem), p As Picture, ResID$) As Byte()
        Dim pSize = p.Size
        Dim Images = From s In BitmapToIconSizes Where s <= pSize.Width OrElse s <= pSize.Height Select p.ToIconBytes(s, s)
        Return IconFromIcon(ResFilePath, ResStore, CreateIconDirFromIconImages(Images), ResID)
    End Function
    Friend UsedIconIDs As IEnumerable(Of UShort),
           CurrentSLH As SafeLibraryHandle
    Function IconFromIcon(ResFilePath$, ResStore As IList(Of ResStoreItem), IconDir As Byte(), ResID$) As Byte()
        If UsedIconIDs Is Nothing Then UsedIconIDs = GetResourceIDs(ResFilePath, RT_ICON).Select(Function(i) i.ToUShort)
        If CurrentSLH Is Nothing Then CurrentSLH = New SafeLibraryHandle(ResFilePath)
        Dim CurrentGID = GetResBytes(CurrentSLH, RT_GROUP_ICON, ResID)
        Dim Delete = GetIconDirEntries(CurrentGID, GrpIconDirEntrySize).Select(Function(i) BitConverter.ToUInt16(i, 12))
        Dim idCount = BitConverter.ToUInt16(IconDir, 4)

        For Each i In Delete.Skip(idCount)
            ResStore.Add(New ResStoreItem(RT_ICON, "#" & i, Nothing))
        Next

        Dim Updating = UsedIconIDs.Except(Delete).ToList
        Dim nIDs = Enumerable.Range(1, UShort.MaxValue).Select(Function(i) CUShort(i)).Except(Updating).Take(idCount).ToArray

        Dim gid = CreateGrpIconDirFromIconDir(IconDir, nIDs)
        For i = 0 To idCount - 1
            Dim nID = nIDs(i)
            Updating.Insert(nID - 1, nID)
            ResStore.Add(New ResStoreItem(RT_ICON, "#" & nID, gid.IconImages(i)))
        Next
        UsedIconIDs = Updating
        Return gid.GrpIconDirEntries
    End Function
End Module
