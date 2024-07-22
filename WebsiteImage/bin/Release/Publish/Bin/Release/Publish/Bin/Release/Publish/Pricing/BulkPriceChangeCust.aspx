<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BulkPriceChangeCust.aspx.cs"
    Inherits="Pricing_BulkPriceChangeCust" %>

<%@ Register Src="~/HomeTopFrame.ascx" TagName="HomeTopFrame" TagPrefix="huc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Secondary Sales System</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />

    <script type="text/javascript" language="javascript"> 
    function fnValidateSave() {             
        var lblMsg = document.getElementById("lblMessage");
        if(document.getElementById("hdnCustCount").value <=0)
         {
               alert("Please Select Atleast One Customer.")               
               return false;
         }
        if(document.getElementById("hdnItemCount").value <=0)
         {
               alert("Please Select Atleast One Item.")              
               return false;
         }
        if (fnComboSelect(document.getElementById("ddlPriceType").value, "Price Change Type", lblMsg) != true) {
               alert(lblMsg.value)
               document.getElementById("ddlPriceType").focus();
               return false;
         }
        if (fnNoBlank(document.getElementById("txtValue").value, "Value", lblMsg) != true) {
              alert(lblMsg.value)
              document.getElementById("txtValue").focus();
              return false;
        }
       if (fnDecimal(document.getElementById("txtValue").value, "Value", lblMsg) != true) {
             alert(lblMsg.value)
             document.getElementById("txtValue").focus();
             return false;
        }
        
      }
      
      function popupDealer(strSelectType) {           
            window.open("../Popups/popupDealer.aspx?SelectType=" + strSelectType , "CCM", "location=0,status=1,scrollbars=1,toolbar=0,width=900,height=400,resizable=no");
        }
        
      
    </script>

</head>
<body>
    <form id="frmBulkPriceChangeCust" runat="server">
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
                    Bulk Price Change - Customer
                </td>
            </tr>
        </table>
        <br />
        <asp:UpdatePanel ID="upCust" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hdnCustCount" runat="server" Value="0" />
                <asp:HiddenField ID="hdnItemCount" runat="server" Value="0" />
                <table width="95%" class="alternatecolorborder" id="Table1" runat="server" border="0"
                    cellspacing="0" cellpadding="5">
                    <tr>
                        <td align="right">
                            Dealer Code:
                        </td>
                        <td align="left">
                            <asp:TextBox type="text" id="txtDealer" runat="server" 
                                class="commonfont textdropwidthSmall" AutoPostBack="True" 
                                ontextchanged="txtDealer_TextChanged"></asp:TextBox>
                            <input type="button" value="..." id="btnDealer" runat="server" class="popButton"
                                onclick="popupDealer('BulkPriceCust')" />
                            <asp:HiddenField ID="hdnDlrCode" runat="server" />
                            <asp:Button ID="btnGetdata" runat="server" OnClick="btnGetdata_Click" Style="display: none;" />
                        </td>
                        <td align="right">
                            State:
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="true" CssClass="commonfont textdropwidthSmall"
                                OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            Region:
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlRegion" runat="server" AutoPostBack="true" CssClass="commonfont textdropwidthSmall"
                                OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            Cust Group:
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlCustGroup" runat="server" CssClass="commonfont textdropwidthSmall"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlCustGroup_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <table width="95%" class="alternatecolorborder" id="TblParam" runat="server" border="0"
                    cellspacing="0" cellpadding="5">
                    <tr>
                        <td align="right" style="width: 13%;">
                            Customer Code:
                        </td>
                        <td align="left">
                            <asp:CheckBoxList ID="chkLstCust" runat="server" RepeatColumns="10" RepeatDirection="Horizontal"
                                CssClass="commonfont textdropwidth" AutoPostBack="True" OnSelectedIndexChanged="chkLstCust_SelectedIndexChanged">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Budget Class:
                        </td>
                        <td align="left">
                            <asp:CheckBoxList ID="chkLstBudget" runat="server" RepeatColumns="10" RepeatDirection="Horizontal"
                                CssClass="commonfont textdropwidth" AutoPostBack="True" OnSelectedIndexChanged="chkLstBudget_SelectedIndexChanged">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Product Family:
                        </td>
                        <td align="left">
                            <asp:CheckBoxList ID="chkLstProdFamily" runat="server" RepeatColumns="10" RepeatDirection="Horizontal"
                                CssClass="commonfont textdropwidth" AutoPostBack="True" OnSelectedIndexChanged="chkLstProdFamily_SelectedIndexChanged">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Product Group:
                        </td>
                        <td align="left">
                            <asp:CheckBoxList ID="chkLstProdGroup" runat="server" RepeatColumns="10" RepeatDirection="Horizontal"
                                CssClass="commonfont textdropwidth" AutoPostBack="True" OnSelectedIndexChanged="chkLstProdGroup_SelectedIndexChanged">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Items:
                        </td>
                        <td align="left">
                            <asp:CheckBoxList ID="chkLstItem" runat="server" RepeatColumns="10" RepeatDirection="Horizontal"
                                CssClass="commonfont textdropwidth" AutoPostBack="True" OnSelectedIndexChanged="chkLstItem_SelectedIndexChanged">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Price Change Type:<var class="starColor">*</var>
                        </td>
                        <td align="left">
                            <table>
                                <tr>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlPriceType" runat="server" CssClass="commonfont textdropwidth">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlOperator" runat="server" CssClass="commonfont textdropwidth"
                                            Width="50px">
                                            <asp:ListItem Text="+" Value="1" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="-" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtValue" runat="server" CssClass="commonfont textdropwidth right"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="95%" class="datatableth" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn cancelbtn" Text="Clear" OnClientClick="location.href='BulkPriceChangeCust.aspx';" />
                            <asp:Button ID="btnSave" runat="server" CssClass="btn savebtn" Text="Save" OnClientClick="return fnValidateSave()"
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </center>
    </form>
</body>
</html>
