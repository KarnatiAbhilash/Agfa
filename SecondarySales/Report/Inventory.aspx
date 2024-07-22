   <%@ Page Language="C#" AutoEventWireup="true" CodeFile="Inventory.aspx.cs" Inherits="Report_Inventory" %>

<%@ Register Src="~/HomeTopFrame.ascx" TagName="HomeTopFrame" TagPrefix="huc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Secondary Sales System</title>

    <script language="javascript" type="text/javascript">
           function popupDealer(strSelectType) {   
          
            window.open("../Popups/popupDealer.aspx?SelectType=" + strSelectType , "DCM", "location=0,status=1,scrollbars=1,toolbar=0,width=900,height=400,resizable=no");
        }
        
        function Validate()
        {
        if($get('txtDealerName').value=='' || $get('txtDealerCode').value=='' )
        {alert("Select the DealerCode"); return false;}
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <asp:UpdatePanel ID="CalPanel" runat="server">
            <ContentTemplate>
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
                                <asp:Label ID="lblHeader" runat="server" Text="Report - Inventory"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table width="95%" class="alternatecolorborder" id="TblParam" runat="server" border="0"
                        cellspacing="0" cellpadding="5">
                        <tr>
                            <td align="left">
                                Dealer Code:<var class="starColor">*</var>
                            </td>
                            <td align="left">
                                <asp:TextBox type="text" ID="txtDealerCode" size="10" AutoPostBack="true" runat="server"
                                    class="commonfont textdropwidth" OnTextChanged="txtDealerCode_TextChanged"></asp:TextBox>
                                <input type="button" value="..." id="btnDealer" runat="server" class="popButton"
                                    onclick="popupDealer('InventoryRpt')" />
                            </td>
                            <td align="left">
                                Dealer Name:
                            </td>
                            <td align="left">
                                <input type="text" disabled="true" size="30" id="txtDealerName" runat="server" width="60px"
                                    cssclass="commonfont textdropwidth" />
                            </td>
                            <td align="left">
                                Product Group:
                            </td>
                            <td align="left" colspan="4">
                                <asp:DropDownList ID="ddlProductGroup" runat="server" CssClass="commonfont textdropwidth"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <table width="95%" class="datatableth" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <%--                     <asp:Button ID="btnBack" runat="server" CssClass="btn backbtn" Text="Back" />
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn cancelbtn" Text="Clear" />--%>
                                <asp:Button ID="btnSave" runat="server" CssClass="btn Excelbtn" Style="padding-left: 20px;
                                    font-size: 11px;" OnClientClick="return Validate();" Text="Generate Excel" OnClick="btnSave_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </center>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
