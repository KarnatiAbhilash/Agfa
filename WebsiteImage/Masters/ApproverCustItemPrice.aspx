<%@ page language="C#" autoeventwireup="true" inherits="Masters_ApproverCustItemPrice, App_Web_ojonuiur" %>

<%@ Register Src="~/HomeTopFrame.ascx" TagName="HomeTopFrame" TagPrefix="huc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head >
    <title></title>

    <script>

        function CheckChild() {
           
            var countMains = document.getElementById("hidSearchCount").value;
            for (var i = 1; i <= countMains; i++) {              
                if (document.getElementById("ChkItem_" + i).checked == true) {
                    document.getElementById("ChkAll").checked = true;
                    return;
                }
            }
            document.getElementById("ChkAll").checked = false;
        }
        function CheckAll() {          
            var checkedvalue = document.getElementById("ChkAll").checked
            var countMains = document.getElementById("hidSearchCount").value;
            for (var i = 1; i <= countMains; i++) {
                var countSubs = document.getElementById("ChkItem_" + i).checked = checkedvalue;               
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
          <ajaxToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" />
      
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
                    Approver Customer Item Price
                </td>
            </tr>
        </table>
        <br />

             <asp:UpdatePanel ID="upReceipt" runat="server">
            <ContentTemplate>
                <asp:HiddenField runat="server" ID="hidSearchCount" Value="0" />
                <asp:HiddenField ID="hdnCurrentDate" runat="server" />
                <asp:HiddenField ID="hdnDateFormat" runat="server" />
                
                <asp:Panel ID="PnlHeader" Width="95%" runat="server" Height="100%" ScrollBars="Both"
                    Style="overflow: auto;">
                    <asp:PlaceHolder ID="phItem" runat="server">
                        <asp:Table runat="server" CssClass="alternatecolorborder" CellPadding="5" CellSpacing="0"
                            Width="100%" ID="SearchTable" Style="vertical-align: top; white-space: nowrap;
                            word-spacing: normal;">
                        </asp:Table>
                    </asp:PlaceHolder>
                </asp:Panel>
                <table width="95%" class="datatableth" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                           
                            <asp:Button ID="btnApproved" runat="server" CssClass="btn savebtn1" Text="Approved" 
                                OnClick="btnApproved_Click" />
                            <asp:Button ID="btnRejected" runat="server" CssClass="btn rejectbtn" Text="Rejected"
                                OnClick="btnRejected_Click" />
                        </td>
                    </tr>
                </table>
                <table width="95%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        </center>
    </form>
</body>
</html>
