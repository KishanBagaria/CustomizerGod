Imports System.Xml.Linq
Module ResData7
    Friend ReadOnly Data7 As XContainer =
            <Resources>
                <!--
            <ResourceList Name = "Login Screen !" FilePath=":\Windows\System32\authui.dll" Type="#2" Format=<%= ResFormat.ArgbBMP %> Restart=<%= RestartMode.Explorer %>/>
            <ResourceList Name = "Photo Viewer !" FilePath=":\Program Files\Windows Photo Viewer\PhotoViewer.dll" Type="PNG" Format=<%= ResFormat.PNG %> Restart=<%= RestartMode.Explorer %>/>
            -->
                <ResourceList Name="Start Button" FilePath=":\Windows\explorer.exe" Restart=<%= RestartMode.Explorer %>>
                    <Resource ID="#6801,#6802,#6803,#6804,#6805,#6806,#6807,#6808,#6809,#6810,#6811,#6812" Type="#2" Format=<%= ResFormat.PArgbBMP %>/>
                </ResourceList>
                <ResourceList Name="Start Menu User Picture Frame" FilePath=":\Windows\explorer.exe" Restart=<%= RestartMode.Explorer %>>
                    <Resource ID="#7013,#7014,#7015,#7016" Type="#2" Format=<%= ResFormat.PArgbBMP %>/>
                </ResourceList>
                <ResourceList Name="General Icons" FilePath=":\Windows\System32\imageres.dll" Type="#14" Format=<%= ResFormat.ICO %> Restart=<%= RestartMode.Explorer %>/>
                <ResourceList Name="Shell Icons" FilePath=":\Windows\System32\shell32.dll" Type="#14" Format=<%= ResFormat.ICO %> Restart=<%= RestartMode.User %> PreviewCopy="True"/>
                <ResourceList Name="Control Panel Applets Sidebar" FilePath=":\Windows\System32\shell32.dll" Restart=<%= RestartMode.None %> PreviewCopy="True">
                    <Resource ID="#632" Name="Overlay" Type="#2" Format=<%= ResFormat.ArgbBMP %>/>
                    <Resource ID="#633" Name="Background" Type="#2" Format=<%= ResFormat.RgbBMP %>/>
                </ResourceList>
                <ResourceList Name="Navigation Buttons" FilePath=":\Windows\System32\ExplorerFrame.dll" Type="#2" Format=<%= ResFormat.ArgbBMP %> Restart=<%= RestartMode.Explorer %>>
                    <Resource ID="#280,#589" Name="Frame"/>
                    <Resource ID="#579,#587" Name="Disabled"/>
                    <Resource ID="#577,#585" Name="Default"/>
                    <Resource ID="#578,#586" Name="Hover"/>
                    <Resource ID="#581,#588" Name="Pressed"/>
                    <Resource ID="#288,#295,#296" Name="Go/Stop/Refresh/Dropdown"/>
                    <Resource ID="#34560,#34561,#34562" Name="Search"/>
                    <Resource ID="#34569,#34570,#34571" Name="Stop Default"/>
                    <Resource ID="#34575,#34576,#34577" Name="Stop Hover"/>
                    <Resource ID="#34581,#34582,#34583" Name="Stop Pressed"/>
                </ResourceList>
                <ResourceList Name="Volume Icons" FilePath=":\Windows\System32\SndVolSSO.dll" Type="#14" Format=<%= ResFormat.ICO %> Restart=<%= RestartMode.Explorer %>>
                    <Resource ID="#120" Name="Mute"/>
                    <Resource ID="#121" Name="No Volume"/>
                    <Resource ID="#122" Name="Low Volume"/>
                    <Resource ID="#123" Name="Medium Volume"/>
                    <Resource ID="#124" Name="High Volume"/>
                    <Resource ID="#125" Name="Error"/>
                </ResourceList>
                <ResourceList Name="Network Icons" FilePath=":\Windows\System32\pnidui.dll" Type="#14" Format=<%= ResFormat.ICO %> Restart=<%= RestartMode.Explorer %>>
                    <Resource ID="#3020"/>
                    <Resource ID="#3035"/>
                    <Resource ID="#3048"/>
                    <Resource ID="#3051"/>
                    <Resource ID="#3054"/>
                    <Resource ID="#3081"/>
                    <Resource ID="#3082"/>
                    <Resource ID="#3083"/>
                    <Resource ID="#3084"/>
                    <Resource ID="#3085"/>
                    <Resource ID="#3086"/>
                    <Resource ID="#3087"/>
                    <Resource ID="#3088"/>
                    <Resource ID="#3089"/>
                    <Resource ID="#3021"/>
                    <Resource ID="#3022"/>
                    <Resource ID="#3023"/>
                    <Resource ID="#3024"/>
                    <Resource ID="#3025"/>
                    <Resource ID="#3026"/>
                    <Resource ID="#3027"/>
                    <Resource ID="#3028"/>
                    <Resource ID="#3029"/>
                    <Resource ID="#3030"/>
                    <Resource ID="#3031"/>
                    <Resource ID="#3032"/>
                    <Resource ID="#3066"/>
                    <Resource ID="#3067"/>
                    <Resource ID="#3071"/>
                    <Resource ID="#3072"/>
                    <Resource ID="#3073"/>
                    <Resource ID="#3074"/>
                    <Resource ID="#3075"/>
                    <Resource ID="#3076"/>
                    <Resource ID="#3077"/>
                    <Resource ID="#3078"/>
                    <Resource ID="#3079"/>
                    <Resource ID="#3091"/>
                    <Resource ID="#3092"/>
                    <Resource ID="#3093"/>
                    <Resource ID="#3094"/>
                    <Resource ID="#3095"/>
                    <Resource ID="#3096"/>
                </ResourceList>
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
                <ResourceList Name="Battery" FilePath=":\Windows\System32\batmeter.dll" Type="#2" Restart=<%= RestartMode.Explorer %>>
                    <Resource ID="#303,#304,#305,#306" Format=<%= ResFormat.PArgbBMP %>/>
                    <Resource ID="#301,#302" Format=<%= ResFormat.ArgbBMP %>/>
                </ResourceList>
                <ResourceList Name="Logon Screen Branding" FilePath=":\Windows\Branding\Basebrd\basebrd.dll" Restart=<%= RestartMode.None %>>
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
