<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HomeTopFrame.ascx.cs" Inherits="HomeTopFrame" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta id="Meta1" http-equiv="X-UA-Compatible" content="IE=EmulateIE7" runat="server" />
    <title>Secondary Sales System</title>    
    <link rel="stylesheet" type="text/css" href="Common/CSS/AjaxStyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="Common/Css/StyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="Common/CSS/HomePageStyleSheet.css" />

    <script type="text/javascript" language="javascript" src="../Common/Scripts/GetScreen.js"></script>

    <script type="text/javascript" language="javascript" src="../Common/Scripts/Common.js"></script>

    <script type="text/javascript" language="javascript" src="../Common/Scripts/Validation.js"></script>

</head>
<body>
    <center>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td style="width: 10%" align="left">
                    <img id="imgPureitLogo" runat="server" src="~/Common/Images/AgfaLogoFinalSmall.gif"
                        alt="" />
                </td>
                
                <td align="left" style="width: 60%">
                    <table border="0" cellpadding="0" cellspacing="0" style="height: 35px; width: 100%;
                        vertical-align: top;">
                        <tr>
                            <td valign="top" align="left">
                                <label id="lblWelcome" runat="server" class="TopFrameWelcome">
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" align="left">
                                <label id="lblDateTime" runat="server" class="TopFrameDateTime">
                                </label>
                            </td>
                        </tr>
                    </table>
                </td> 
                <td align="left" style="width: 30%">
                    <div id="logout_sub_container">
                        <div class="logout_link_txt" style="background-color: White;">
                            <a target="_parent" id="aLogout" runat="server" href="Logout.aspx">Log Out</a></div>
                        <div id="logout_icon">
                        </div>
                        <div id="logout_divider_line">
                        </div>
                        <div class="home_link_txt" style="background-color: White;">
                            <a target="_parent" id="aHome" runat="server" href="~/HomePage/HomePage.aspx">Home</a></div>
                        <div class="home_icon_image">
                        </div>
                    </div>
                </td>               
            </tr>
        </table>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="center">
                    <asp:Menu ID="MenuAgfa"  runat="server" DynamicHorizontalOffset="0" Orientation="Horizontal"
                        StaticEnableDefaultPopOutImage="false" Width="100%" StaticBottomSeparatorImageUrl="~/Common/Images/arrow_button_white.png">
                        <StaticMenuItemStyle Height="25px" CssClass="HeaderMenuStaticHome" />
                        <DynamicHoverStyle Height="15px" CssClass="HeaderMenuHoverHome" />
                        <DynamicMenuItemStyle Height="15px" ItemSpacing="0" CssClass="HeaderMenuStaticHome IE8Fix" />
                        <StaticHoverStyle Height="25px" CssClass="HeaderMenuHoverHome" />
                        <StaticMenuStyle Height="25px" CssClass="MenuStyleHome" />
                    </asp:Menu>
                </td>
            </tr>
        </table>
    </center>
</body>
</html>