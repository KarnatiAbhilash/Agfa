<%@ page language="C#" autoeventwireup="true" inherits="Admin_UserMenuMapping, App_Web_nr1inqul" %>
<%@ Register Src="~/HomeTopFrame.ascx" TagName="HomeTopFrame" TagPrefix="huc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Secondary Sales System</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    
    <script language="javascript" type="text/javascript">
        function popupUser(strSelectType) {           
            window.open("../Popups/popupUserMaster.aspx?SelectType=" + strSelectType , "UUM", "location=0,status=1,scrollbars=1,toolbar=0,width=900,height=400,resizable=no");
        }
        
        function CheckParent(MainSelect) {
            var hdmain = document.getElementById("hdMain_" + MainSelect).value
            var checkedvalue = document.getElementById("chk_" + hdmain).checked
            var totalsub = document.getElementById("subcount_" + MainSelect).value

            for (var i = 1; i <= totalsub; i++) {
                var hdsubmain = document.getElementById("hdSub_" + MainSelect + "_" + i).value
                document.getElementById("chk_" + hdmain + "_" + hdsubmain).checked = checkedvalue;
            }
        }

        function CheckChild(MainSelect) {
            var hdmain = document.getElementById("hdMain_" + MainSelect).value
            var totalsub = document.getElementById("subcount_" + MainSelect).value
            for (var i = 1; i <= totalsub; i++) {
                var hdsubmain = document.getElementById("hdSub_" + MainSelect + "_" + i).value
                if (document.getElementById("chk_" + hdmain + "_" + hdsubmain).checked == true) {
                    document.getElementById("chk_" + hdmain).checked = true;
                    return;
                }
            }
            document.getElementById("chk_" + hdmain).checked = false;
        }

        function CheckAll() {
            var checkedvalue = document.getElementById("chkAll").checked
            var countMains = document.getElementById("maincount").value;
            for (var i = 1; i <= countMains; i++) {
                var countSubs = document.getElementById("subcount_" + i).value;
                var hdmain = document.getElementById("hdMain_" + i).value
                document.getElementById("chk_" + hdmain).checked = checkedvalue;
                for (var j = 1; j <= countSubs; j++) {
                    var hdsubmain = document.getElementById("hdSub_" + i + "_" + j).value
                    document.getElementById("chk_" + hdmain + "_" + hdsubmain).checked = checkedvalue;
                }
            }
        }
     
    </script>
</head>
<body>
    <form id="frmUserMenuMapping" runat="server">
    <center>
     <table width="95%" border="0" cellspacing="0" cellpadding="0" style="vertical-align: top;">
            <tr>
                <td>
                    <huc1:HomeTopFrame runat="server" ID="TopFrame" />
                </td>
            </tr>
        </table>
        <br />
        <table width="95%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td align="left" class="pagetitleDetail">
                    User Menu Mapping
                </td>
            </tr>
        </table>
        <br />
        <table class="datatableth" width="95%">
            <tr class="commonfontbold">
                <td align="right">
                    User Id:<var class="starColor">*</var>
                </td>
                <td align="left">
                    <asp:TextBox type="text" id="txtUserId" AutoPostBack="true" runat="server" 
                        class="commonfont textdropwidth" ontextchanged="txtUserId_TextChanged" ></asp:TextBox>
                    <input type="button" value="..." id="btnUser" runat="server" class="popButton"
                        onclick="popupUser('UserMenu')" />                    
                </td>
                <td align="right">
                    User Name:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtUserName" Enabled="false" runat="server" CssClass="commonfont textdropwidth"></asp:TextBox>
                    <asp:Button ID="btnGetdata" runat="server" onclick="btnGetdata_Click"  style="display:none;"/>
                </td>
            </tr>
        </table>
        <asp:PlaceHolder ID="PlaceUserMenuMapping" runat="server"></asp:PlaceHolder>
               <table width="95%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td align="left">
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </center>
    </form>
</body>
</html>
