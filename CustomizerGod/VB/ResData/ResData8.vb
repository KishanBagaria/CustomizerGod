Imports System.Xml.Linq
Module ResData78
    Friend ReadOnly Data8 As XContainer =
        <Resources>
            <!--
            <ResourceList Name="!Navigation &amp; Ribbon Buttons" FilePath=":\Windows\System32\ExplorerFrame.dll" Type="#2" Format=<%= ResFormat.ArgbBMP %> Restart=<%= RestartMode.Explorer %>/>
            <ResourceList Name="!Explorer Ribbon BMPs" FilePath=":\Windows\System32\UIRibbon.dll" Type="#2" Format=<%= ResFormat.ArgbBMP %> Restart=<%= RestartMode.Explorer %>/>
            <ResourceList Name="!IE Frame BMPs" FilePath=":\Windows\System32\ieframe.dll" Type="#2" Format=<%= ResFormat.ArgbBMP %> Restart=<%= RestartMode.Explorer %>/>
            <ResourceList Name="!IE Frame" FilePath=":\Windows\System32\ieframe.dll" Type="Image" Format=<%= ResFormat.PNG %> Restart=<%= RestartMode.Explorer %>/>
            <ResourceList Name="!Photo Viewer" FilePath=":\Program Files\Windows Photo Viewer\PhotoViewer.dll" Type="PNG" Format=<%= ResFormat.PNG %> Restart=<%= RestartMode.Explorer %>/>
            <ResourceList Name="!Calculator" FilePath=":\Windows\System32\calc.exe" Type="IMAGE" Format=<%= ResFormat.PNG %> Restart=<%= RestartMode.None %>/>
            <ResourceList Name="Shell Icons" FilePath=":\Windows\System32\shell32.dll" Type="#14" Format=<%= ResFormat.ICO %> Restart=<%= RestartMode.LogOff %> PreviewCopy="True"/>
            -->

            <ResourceList Name="Start Button" FilePath=":\Windows\System32\twinui.dll" Type="Image" Format=<%= ResFormat.PNG %> Restart=<%= RestartMode.Explorer %>>
                <Resource ID="#11701,#11702,#11705,#11706,#11709,#11710,#11713,#11714" Name="Default"/>
                <Resource ID="#11703,#11704,#11707,#11708,#11711,#11712,#11715,#11716" Name="Hover &amp; Pressed"/>
            </ResourceList>
            <ResourceList Name="Charms Bar" FilePath=":\Windows\System32\twinui.dll" Type="Image" Format=<%= ResFormat.PNG %> Restart=<%= RestartMode.Explorer %>>
                <Resource ID="#7291,#7296,#7301" Name="Search Default"/>
                <Resource ID="#7293,#7298,#7303" Name="Share Default"/>
                <Resource ID="#7294,#7299,#7304" Name="Start Default"/>
                <Resource ID="#7290,#7295,#7300" Name="Devices Default"/>
                <Resource ID="#7292,#7297,#7302" Name="Settings Default"/>
                <Resource ID="#7221,#7226,#7231" Name="Search Hover"/>
                <Resource ID="#7223,#7228,#7233" Name="Share Hover"/>
                <Resource ID="#7320,#7321,#7322" Name="Start Hover"/>
                <Resource ID="#7220,#7225,#7230" Name="Devices Hover"/>
                <Resource ID="#7222,#7227,#7232" Name="Settings Hover"/>
            </ResourceList>
            <ResourceList Name="General Icons" FilePath=":\Windows\System32\imageres.dll" Type="#14" Format=<%= ResFormat.ICO %> Restart=<%= RestartMode.Explorer %>/>
            <ResourceList Name="File Explorer Ribbon" FilePath=":\Windows\System32\UIRibbon.dll" Type="PNG" Format=<%= ResFormat.PNG %> Restart=<%= RestartMode.Explorer %>>
                <Resource ID="#6105,#6106,#6107,#6108" Name="Help"/>
                <Resource ID="#6109,#6110,#6111,#6112" Name="Expand"/>
                <Resource ID="#6113,#6114,#6115,#6116" Name="Collapse"/>
                <Resource ID="#6117,#6118,#6119,#6120" Name="Pin"/>
            </ResourceList>
            <ResourceList Name="Volume Icons" FilePath=":\Windows\System32\SndVolSSO.dll" Type="#14" Format=<%= ResFormat.ICO %> Restart=<%= RestartMode.Explorer %>>
                <Resource ID="#120" Name="Mute"/>
                <Resource ID="#121" Name="No Volume"/>
                <Resource ID="#122" Name="Low Volume"/>
                <Resource ID="#123" Name="Medium Volume"/>
                <Resource ID="#124" Name="High Volume"/>
                <Resource ID="#125" Name="Error"/>
            </ResourceList>
            <ResourceList Name="Network Icons" FilePath=":\Windows\System32\pnidui.dll" Type="#14" Format=<%= ResFormat.ICO %> Restart=<%= RestartMode.Explorer %>/>
            <ResourceList Name="Action Center Icons" FilePath=":\Windows\System32\ActionCenter.dll" Type="#14" Format=<%= ResFormat.ICO %> Restart=<%= RestartMode.Explorer %>>
                <Resource ID="#1"/>
                <Resource ID="#2"/>
                <Resource ID="#3"/>
                <Resource ID="#4"/>
                <Resource ID="#9"/>
                <Resource ID="#5"/>
                <Resource ID="#6"/>
                <Resource ID="#7"/>
                <Resource ID="#8"/>
            </ResourceList>
            <ResourceList Name="Drive Icons" FilePath=":\Windows\System32\imagesp1.dll" Type="#14" Format=<%= ResFormat.ICO %> Restart=<%= RestartMode.Explorer %>/>
            <ResourceList Name="ZIP Folder Icons" FilePath=":\Windows\System32\zipfldr.dll" Type="#14" Format=<%= ResFormat.ICO %> Restart=<%= RestartMode.Explorer %>/>
            <ResourceList Name="Safely Remove Hardware Tray Icon" FilePath=":\Windows\System32\stobject.dll" Restart=<%= RestartMode.Explorer %>>
                <Resource ID="#210" Type="#14" Format=<%= ResFormat.ICO %>/>
            </ResourceList>
            <ResourceList Name="Battery" FilePath=":\Windows\System32\batmeter.dll" Type="#2" Format=<%= ResFormat.ArgbBMP %> Restart=<%= RestartMode.Explorer %>>
                <Resource ID="#353,#354,#355,#356"/>
                <Resource ID="#357,#358,#359"/>
                <Resource ID="#360,#361,#362"/>
                <Resource ID="#363,#364,#365,#366"/>
            </ResourceList>
            <ResourceList Name="Login Screen" FilePath=":\Windows\System32\authui.dll" Type="#2" Format=<%= ResFormat.RgbBMP %> Restart=<%= RestartMode.None %>>
                <Resource ID="#13500"/>
                <Resource ID="#17000"/>
                <Resource ID="#17542"/>
            </ResourceList>
            <ResourceList Name="Logon Screen Branding" FilePath=":\Windows\Branding\Basebrd\basebrd.dll" Restart=<%= RestartMode.None %> Resize="False" Preview="Ctrl+Alt+Delete">
                <Resource ID="#120,#1120,#2120" Type="#2" Format=<%= ResFormat.ArgbBMP %> Name="Logon Screen Branding"/>
            </ResourceList>
            <ResourceList Name="Base Branding" FilePath=":\Windows\Branding\Basebrd\basebrd.dll" Restart=<%= RestartMode.None %> Resize="False" Preview="winver">
                <Resource ID="#121,#1121,#2121" Type="#2" Format=<%= ResFormat.RgbBMP %> Name=<%= DataStrings.BaseBrdContent %>/>
            </ResourceList>
            <ResourceList Name="System Branding" FilePath=":\Windows\Branding\Shellbrd\shellbrd.dll" Restart=<%= RestartMode.None %> Resize="False" Preview="control system">
                <Resource ID="#1050,#2050,#3050" Type="#2" Format=<%= ResFormat.ArgbBMP %>/>
            </ResourceList>
            <ResourceList Name="Time &amp; Date" FilePath=":\Windows\System32\timedate.cpl" Type="PNGFILE" Format=<%= ResFormat.PNG %> Restart=<%= RestartMode.Explorer %> Preview="timedate.cpl">
                <Resource ID="#5000,#5004,#5008" Name="Main Clock"/>
                <Resource ID="#5001,#5005,#5009" Name="Main Clock Overlay"/>
                <Resource ID="#5002,#5006,#5010" Name="Additional Clock"/>
                <Resource ID="#5003,#5007,#5011" Name="Additional Clock Overlay"/>
            </ResourceList>
        </Resources>
End Module
