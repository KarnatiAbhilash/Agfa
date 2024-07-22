<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoginPage.aspx.cs" Inherits="LoginPage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Secondary Sales System</title>
    <link rel="stylesheet" type="text/css" href="Common/CSS/AjaxStyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="Common/CSS/StyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="Common/CSS/login_style.css" />
    <style type="text/css">
        .style1
        {
            height: 5px;
        }
    </style>
</head>
<body>
    <form id="frmLoginPage" runat="server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" />
       <center>
        <table width="900px" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <table width="90%" cellspacing="0" cellpadding="0" border="0">
                        <tr>
                            <td style="height: 60px">
                            </td>
                        </tr>
                        <tr>
                            
                            <td>
                                <img src="Common/Images/LogoHeading1.bmp" alt="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <img src="Common/Images/AgfaLogoFinal.bmp" alt="" />
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 20px">
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <table width="40%" cellspacing="0" cellpadding="0" border="0" align="center">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblUserId" Text="User Id : " runat="server" CssClass="LabelText"></asp:Label>
                                            &nbsp;
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtUserId" runat="server" AutoCompleteType="Disabled" CssClass="textdropwidth"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblPassword" runat="server" Text="Password : " CssClass="LabelText"></asp:Label>&nbsp;
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="textdropwidth"></asp:TextBox>
                                        </td>
                                    </tr>
                                   <tr><td Style="Height:15px;"></td></tr>
                                    <tr style="Padding-top:10px;">
                                        <td align="right" >
                                        </td>
                                        <td align="left">
                                            <asp:ImageButton ID="LoginButton" runat="server"   CausesValidation="true" Width="58" Height="20"
                                                Text="" OnClick="LoginButton_Click" ImageUrl="~/Common/Images/btn.png"  
                                               />  
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  
                                            <asp:ImageButton ID="loginReset" runat="server"  Text="" 
                                                CausesValidation="false" Width="66px" Height="20px"
                                                OnClick="loginReset_Click" ImageUrl="~/Common/Images/btnreset.png" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" align="center">
                                            <asp:Label ID="lblErrorMessage" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table width="900px" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td colspan="100%" style="height: 30px" align="right">
                                <asp:RequiredFieldValidator runat="server" ID="ReqUserId" ControlToValidate="txtUserId"
                                    Display="None" ErrorMessage="<b>Required Field Missing</b><br />Valid User Id is Required</a></div>" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="UserNameReqE" TargetControlID="ReqUserId"
                                    HighlightCssClass="validatorCalloutHighlight" />
                                <asp:RequiredFieldValidator runat="server" ID="ReqPassword" ControlToValidate="txtPassword"
                                    Display="None" ErrorMessage="<b>Required Field Missing</b><br />Valid Password is Required</a></div>" />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="PasswordReqE" TargetControlID="ReqPassword"
                                    HighlightCssClass="validatorCalloutHighlight" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height:90px">
                </td>
            </tr>
            <tr>
                <td>
                    <table width="900px" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td colspan="100%" style="height: 30px" align="center">
                                This Product is Licenced to Agfa HealthCare India Pvt. Ltd.
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </center>
    </form>
</body>
</html>
