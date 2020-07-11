Imports System.Xml.Linq
Module ResData10
    Friend ReadOnly Data10 As XContainer =
            <Resources>
                <ResourceList Name="Taskbar" FilePath=":\Windows\explorer.exe" Type="Image" Format=<%= ResFormat.PNG %> Restart=<%= RestartMode.Explorer %> ReadOnly="True">
                    <Resource ID="#124:#135" Name="Task View Default (Dark)"/>
                    <Resource ID="#136:#147" Name="Task View Hover (Dark)"/>
                    <Resource ID="#163:#174" Name="Search Icon (Dark)"/>
                    <Resource ID="#213:#224" Name="Cortana (Dark)"/>
                    <Resource ID="#260:#265" Name="Search Box (Dark)"/>
                    <Resource ID="#307:#312" Name="Touch Keyboard (Dark)"/>
                    <Resource ID="#363:#374" Name="Face (Dark)"/>
                    <Resource ID="#410:#415" Name="Face Hover (Dark)"/>
                    <Resource ID="#463:#474" Name="Back (Dark)"/>
                    <Resource ID="#502:#513" Name="Forward (Dark)"/>
                    <Resource ID="#100:#111" Name="Task View Default (Light)"/>
                    <Resource ID="#112:#123" Name="Task View Hover (Light)"/>
                    <Resource ID="#151:#162" Name="Search Icon (Light)"/>
                    <Resource ID="#201:#212" Name="Cortana (Light)"/>
                    <Resource ID="#251:#256" Name="Search Box (Light)"/>
                    <Resource ID="#301:#306" Name="Touch Keyboard (Light)"/>
                    <Resource ID="#351:#362" Name="Face (Light)"/>
                    <Resource ID="#401:#406" Name="Face Hover (Light)"/>
                    <Resource ID="#451:#462" Name="Back (Light)"/>
                    <Resource ID="#490:#501" Name="Forward (Light)"/>
                </ResourceList>
                <ResourceList Name="Action Center Icons" FilePath=":\Windows\explorer.exe" Type="#14" Format=<%= ResFormat.ICO %> Restart=<%= RestartMode.Explorer %> ReadOnly="True">
                    <Resource ID="#20101" Name="Default"/>
                    <Resource ID="#20102" Name="Quiet Hours On"/>
                    <Resource ID="#20103" Name="New Notifications"/>
                    <Resource ID="#20104" Name="New Notifications &amp; Quiet Hours On"/>
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
                <ResourceList Name="Safely Remove Hardware Tray Icon" FilePath=":\Windows\System32\stobject.dll" Restart=<%= RestartMode.Explorer %>>
                    <Resource ID="#210" Type="#14" Format=<%= ResFormat.ICO %>/>
                </ResourceList>
                <ResourceList Name="File Explorer Address Bar" FilePath=":\Windows\System32\ExplorerFrame.dll" Type="#2" Format=<%= ResFormat.ArgbBMP %> Restart=<%= RestartMode.Explorer %> PreviewCopy="True">
                    <Resource ID="#288,#295,#296,#320:#323"/>
                    <Resource ID="#297:#299,#324:#327"/>
                    <Resource ID="#300:#302,#328:#331"/>
                </ResourceList>
                <ResourceList Name="File Explorer Ribbon" FilePath=":\Windows\System32\UIRibbon.dll" Type="PNG" Format=<%= ResFormat.PNG %> Restart=<%= RestartMode.Explorer %>>
                    <Resource ID="#6105:#6111" Name="Help"/>
                    <Resource ID="#6112:#6118" Name="Expand"/>
                    <Resource ID="#6119:#6125" Name="Collapse"/>
                    <Resource ID="#6126:#6132" Name="Pin"/>
                </ResourceList>
                <ResourceList Name="General Icons" FilePath=":\Windows\System32\imageres.dll" Type="#14" Format=<%= ResFormat.ICO %> Restart=<%= RestartMode.Explorer %>/>
                <ResourceList Name="ZIP Folder Icons" FilePath=":\Windows\System32\zipfldr.dll" Type="#14" Format=<%= ResFormat.ICO %> Restart=<%= RestartMode.Explorer %>/>
                <ResourceList Name="Battery" FilePath=":\Windows\System32\batmeter.dll" Type="#2" Format=<%= ResFormat.ArgbBMP %> Restart=<%= RestartMode.Explorer %>>
                    <Resource ID="#357,#358,#359"/>
                    <Resource ID="#360,#361,#362"/>
                    <Resource ID="#353,#354,#355,#356"/>
                    <Resource ID="#363,#364,#365,#366"/>
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
