<%@ page language="C#" autoeventwireup="true" inherits="Report_PriceList, App_Web_24s0sxlq" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Src="~/HomeTopFrame.ascx" TagName="HomeTopFrame" TagPrefix="huc1" %>
    
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Secondary Sales System</title>
    <script type="text/javascript" language="javascript"> 
       function popupItemMaster(strSelectType) {
            window.open("../Popups/popupItemMaster.aspx?SelectType=" + strSelectType, "ItemMaster", "location=0,status=1,scrollbars=1,toolbar=0,width=900,height=400,resizable=no");
        }
       function popupDealer(strSelectType) {   
          
            window.open("../Popups/popupDealer.aspx?SelectType=" + strSelectType , "DCM", "location=0,status=1,scrollbars=1,toolbar=0,width=900,height=400,resizable=no");
        }
        function popCustomer(strSelectType) { 
            var dlrCode=document.getElementById("txtDealerCode").value;     
            if(dlrCode!="")     
            window.open("../Popups/popupCustomer.aspx?SelectType=" + strSelectType +"&strCount=0&dlr="+dlrCode , "DCM", "location=0,status=1,scrollbars=1,toolbar=0,width=900,height=400,resizable=no");
        }
        function Validate() {
            if ($get('txtDealerCode').value == '')
            { alert("Select the DealerName"); return false; }
        }      
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager2" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="CalPanel" runat="server">
            <ContentTemplate>
                <center>
                    <table width="95%" border="0" cellspacing="0" cellpadding="0" style="vertical-align: top;
                        height: 18px;">
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
                                <asp:Label ID="lblHeader" runat="server" Text="Report - Item Price List"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table width="95%" class="alternatecolorborder" id="TblParam" runat="server" border="0"
                        cellspacing="0" cellpadding="5">
                        <tr>
                            <td align="left">
                                Item Code:
                            </td>
                            <td align="left">
                                <input type="text" disabled="true" size="16" id="txtItemCode" runat="server" width="60px"
                                    cssclass="commonfont textdropwidth" />
                                <input type="button" value="..." id="btnPopItem" runat="server" class="popButton" onclick="popupItemMaster('ItemMaster')" />
                            </td>
                            <td align="left">
                                Executive Code:
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlExecutiveCode" runat="server" Width="150px" CssClass="commonfont textdropwidth" >
                                </asp:DropDownList>
                            </td>
                            <td align="left">
                                Region:
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlRegion" runat="server" Width="150px" CssClass="commonfont textdropwidth">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                Dealer Code:
                            </td>
                            <td align="left">
                                <asp:TextBox type="text" ID="txtDealerCode" size="10" AutoPostBack="true" runat="server"
                                    class="commonfont textdropwidth" OnTextChanged="txtDealerCode_TextChanged"></asp:TextBox>
                                <input type="button" value="..." id="btnDealer" runat="server" class="popButton"
                                    onclick="popupDealer('DebitCreditNoteRpt')" />
                            </td>
                            <td align="left">
                                Customer Code:
                            </td>
                            <td align="left">
                                <input type="text" disabled="true" size="16" id="txtCustCode" runat="server" width="60px"
                                    cssclass="commonfont textdropwidth" />
                                <input type="button" value="..." id="Button1" runat="server" class="popButton" onclick="popCustomer('DebitCreditNoteRpt')" />
                            </td>
                            <td align="left">
                                State:
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlState" ReadOnly="true" runat="server" Width="150px" CssClass="commonfont textdropwidth">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                Customer Group:
                            </td>
                            <td align="left" colspan="5">
                                <asp:DropDownList ID="ddlCustGrp" ReadOnly="true" runat="server" Width="150px" CssClass="commonfont textdropwidth">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <table width="95%" class="datatableth" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <asp:Button ID="btnSave" runat="server" CssClass="btn Excelbtn" Style="font-size: 11px;" Text="Generate Excel" 
                                     OnClick="btnSave_Click" />
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
                </center>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
