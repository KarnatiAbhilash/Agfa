<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HomePage.aspx.cs" Inherits="HomePage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<%@ Register Src="~/HomeMenuSection.ascx" TagName="MenuSection" TagPrefix="HMS" %>--%>
<%@ Register Src="~/TopFrame.ascx" TagName="TopFrame" TagPrefix="uc1" %>
<%@ Register Src="~/HomeTopFrame.ascx" TagName="HomeTopFrame" TagPrefix="huc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Secondary Sales System</title>
    <link href="../Common/CSS/HomePageStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="frmHomePage" runat="server">
    <div>
        <center>
            <table width="95%" border="0" cellspacing="0" cellpadding="0" style="vertical-align: top;">
                <tr>
                    <td style="height: 10px;">
                        &nbsp;
                    </td>
                </tr>
            </table>
            <table width="95%" border="0" cellspacing="0" cellpadding="0" style="vertical-align: top;">
                <tr>
                    <td>
                        <huc1:HomeTopFrame runat="server" ID="TopFrame" />
                    </td>
                </tr>
            </table>
            <%--<table width="900px" border="0" cellspacing="0" cellpadding="0" style="vertical-align: top;">
            <tr>
                <td>
                    <HMS:MenuSection runat="server" ID="MenuSection"  />
                </td>
            </tr>
        </table>--%>
        </center>
    </div>
    </form>
</body>
</html>
