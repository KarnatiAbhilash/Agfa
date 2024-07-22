<%@ page language="C#" autoeventwireup="true" inherits="Report_SchemeAchievement, App_Web_24s0sxlq" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/HomeTopFrame.ascx" TagName="HomeTopFrame" TagPrefix="huc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Secondary Sales System</title>

    <script language="javascript" type="text/javascript">
            function popupDealer(strSelectType) {   
          
            window.open("../Popups/popupDealer.aspx?SelectType=" + strSelectType , "DCM", "location=0,status=1,scrollbars=1,toolbar=0,width=900,height=400,resizable=no");
        }
        
            function popCustomer(strSelectType) { 
            var dlrCode=document.getElementById("txtDealerCode").value;                
                if (dlrCode != "") {
                    window.open("../Popups/popupCustomer.aspx?SelectType=" + strSelectType + "&strCount=0&dlr=" + dlrCode, "DCM", "location=0,status=1,scrollbars=1,toolbar=0,width=900,height=400,resizable=no");
                }
                else {
                    window.open("../Popups/popupCustomer.aspx?SelectType=" + strSelectType + "&strCount=0", "DCM", "location=0,status=1,scrollbars=1,toolbar=0,width=900,height=400,resizable=no");
                }
            }
            function popupItemMaster(strSelectType) {                        
            window.open("../Popups/popupItemMaster.aspx?SelectType=" + strSelectType, "ItemMaster", "location=0,status=1,scrollbars=1,toolbar=0,width=900,height=400,resizable=no");
        }
         function Validate()
        {
        if($get('txtDealerCode').value=='')
        {alert("Select All the Creiteria"); return false;}
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
                                <asp:Label ID="lblHeader" runat="server" Text="Report - Scheme Achievement"></asp:Label>
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
                                    onclick="popupDealer('ItemMaster')" />
                            </td>
                            <td align="left">
                                Customer Code:
                            </td>
                            <td align="left">
                                <input type="text" disabled="true" size="10" id="txtCustCode" runat="server" width="60px"
                                    cssclass="commonfont textdropwidth" />
                                <input type="button" value="..." id="Button2" runat="server" class="popButton" onclick="popCustomer('DebitCreditNoteRpt')" />
                            </td>
                            <td align="left">
                                Item Code:
                            </td>
                            <td align="left" colspan="4">
                                <input type="text" disabled="true" size="10" id="txtItemCode" runat="server" width="60px"
                                    cssclass="commonfont textdropwidth" />
                                <input type="button" value="..." id="Button3" runat="server" class="popButton" onclick="popupItemMaster('ItemMaster')" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                Inv. From Date:
                            </td>
                            <td align="left">
                                <asp:TextBox Enabled="false" ID="txtInvFromDate" Width="100px" MaxLength="10" runat="server"
                                    CssClass="commonfont textdropwidth">
                                </asp:TextBox>
                                <asp:ImageButton ImageAlign="Middle" ID="ImgBtnInvFromDate" runat="server" ImageUrl="~/Common/Images/cal.png" />
                                <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtInvFromDate" PopupButtonID="ImgBtnInvFromDate"
                                    runat="server">
                                </cc1:CalendarExtender>
                            </td>
                            <td align="left">
                                Inv. To Date:
                            </td>
                            <td align="left" colspan="4">
                                <asp:TextBox Enabled="false" ID="txtInvToDate" runat="server" Width="100px" CssClass="commonfont textdropwidth">
                                </asp:TextBox>
                                <asp:ImageButton ImageAlign="Middle" ID="ImgBtnInvTODate" runat="server" ImageUrl="~/Common/Images/cal.png" />
                                <cc1:CalendarExtender ID="CalendarExtender2" TargetControlID="txtInvToDate" PopupButtonID="ImgBtnInvTODate"
                                    runat="server">
                                </cc1:CalendarExtender>
                            </td>
                        </tr>
                    </table>
                    <table width="95%" class="datatableth" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <asp:Button ID="btnSave" runat="server" CssClass="btn Excelbtn" Style="padding-left: 20px;
                                    font-size: 11px;" Text="Generate Excel" OnClientClick="return  Validate();" OnClick="btnSave_Click" />
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
