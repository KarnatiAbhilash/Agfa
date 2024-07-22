<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustSpecialScheme.aspx.cs"
    Inherits="Masters_CustSpecialScheme" %>

<%@ Register Src="~/HomeTopFrame.ascx" TagName="HomeTopFrame" TagPrefix="huc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Secondary Sales System</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />

    <script type="text/javascript" language="javascript">
        function fnValidateSave() {
          var hdnCount=document.getElementById("hidSearchCount").value;
          var i;
          var lblMsg = document.getElementById("lblMessage");
          var returnFlag;
          returnFlag=false;
          for(i=1;i<=hdnCount;i++)
          {
           if(document.getElementById("hdnRow_"+i).value=="S")
             continue;
           if(document.getElementById("txtQtyConsum_"+i).value !="")
           {
                if (fnNoChar(document.getElementById("txtQtyConsum_"+i).value, "Qty Consumption", lblMsg) != true) {
                    alert(lblMsg.value)
                    document.getElementById("txtQtyConsum_"+i).focus();
                    return false;
                 }
                if (document.getElementById("txtQtyConsum_"+i).value <= 0)
                {
                    alert("Qty Consumption Should be Greater Than Zero.")
                    document.getElementById("txtQtyConsum_"+i).focus();
                    return false;
                }
               if (fnNoBlank(document.getElementById("txtQtyEligible_"+i).value, "Qty Eligible", lblMsg) != true) {
                  alert(lblMsg.value)
                  document.getElementById("txtQtyEligible_"+i).focus();
                  return false;
               }
              if (fnNoChar(document.getElementById("txtQtyEligible_"+i).value, "Qty Eligible", lblMsg) != true) {
                  alert(lblMsg.value)
                  document.getElementById("txtQtyEligible_"+i).focus();
                  return false;
               }
              if (document.getElementById("txtQtyEligible_"+i).value <= 0)
                {
                    alert("Qty Eligible Should be Greater Than Zero.")
                    document.getElementById("txtQtyEligible_"+i).focus();
                    return false;
                }
            }
           if(document.getElementById("txtSqmtConsum_"+i).value !="")
            {
              if (fnDecimal(document.getElementById("txtSqmtConsum_"+i).value, "SqMt Consumption", lblMsg) != true) {
                 alert(lblMsg.value)
                 document.getElementById("txtSqmtConsum_"+i).focus();
                 return false;
              }
             if (document.getElementById("txtSqmtConsum_"+i).value <= 0)
                {
                    alert("SqMt Consumption Should be Greater Than Zero.")
                    document.getElementById("txtSqmtConsum_"+i).focus();
                    return false;
                }
             if (fnNoBlank(document.getElementById("txtSqmtEligible_"+i).value, "SqMt Consumption", lblMsg) != true) {
                 alert(lblMsg.value)
                 document.getElementById("txtSqmtEligible_"+i).focus();
                 return false;
              }
             if (fnDecimal(document.getElementById("txtSqmtEligible_"+i).value, "SqMt Consumption", lblMsg) != true) {
                 alert(lblMsg.value)
                 document.getElementById("txtSqmtEligible_"+i).focus();
                 return false;
              }
             if (document.getElementById("txtSqmtEligible_"+i).value <= 0)
                {
                    alert("SqMt Eligible Should be Greater Than Zero.")
                    document.getElementById("txtSqmtEligible_"+i).focus();
                    return false;
                }
            }
           if(document.getElementById("txtValue_"+i).value !="")
            {
              if (fnDecimal(document.getElementById("txtValue_"+i).value, "Value", lblMsg) != true) {
                 alert(lblMsg.value)
                 document.getElementById("txtValue_"+i).focus();
                 return false;
              }
             if (document.getElementById("txtValue_"+i).value <= 0)
                {
                    alert("Value Should be Greater Than Zero.")
                    document.getElementById("txtValue_"+i).focus();
                    return false;
                }
             if (fnNoBlank(document.getElementById("txtValueEligible_"+i).value, "Value Eligible", lblMsg) != true) {
                 alert(lblMsg.value)
                 document.getElementById("txtValueEligible_"+i).focus();
                 return false;
              }
             if (fnDecimal(document.getElementById("txtValueEligible_"+i).value, "Value Eligible", lblMsg) != true) {
                 alert(lblMsg.value)
                 document.getElementById("txtValueEligible_"+i).focus();
                 return false;
              }
             if (document.getElementById("txtValueEligible_"+i).value <= 0)
                {
                    alert("Value Eligible Should be Greater Than Zero.")
                    document.getElementById("txtValueEligible_"+i).focus();
                    return false;
                }
            }
            returnFlag=true;
          }
          if (returnFlag==false)
             return false;
        else
            return true;
        }
    </script>

</head>
<body>
    <form id="frmCustSpecialScheme" runat="server">
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
                    Customer Special Scheme
                </td>
            </tr>
        </table>
        <br />
        <asp:UpdatePanel ID="upReceipt" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hidSearchCount" runat="server" Value="0" />
                <table width="95%" class="alternatecolorborder" id="TblParam" runat="server" border="0"
                    cellspacing="0" cellpadding="5">
                    <tr>
                        <td align="right">
                            <b>Customer Code:</b>
                        </td>
                        <td align="left">
                            <asp:Label ID="lblCustCode" runat="server" CssClass="commonfont textdropwidth"></asp:Label>
                        </td>
                        <td align="right">
                            <b>Customer Name:</b>
                        </td>
                        <td align="left">
                            <asp:Label ID="lblCustName" runat="server" CssClass="commonfont textdropwidth"></asp:Label>
                        </td>
                        <td align="right">
                            <b>Dealer Code:</b>
                        </td>
                        <td align="left">
                            <asp:Label ID="lblDealerCode" runat="server" CssClass="commonfont textdropwidth"></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="PnlHeader" Width="95%" runat="server" Height="100%" ScrollBars="Horizontal"
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
                            <asp:Button ID="btnBack" runat="server" CssClass="btn backbtn" Text="Back" OnClick="btnBack_Click" />
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn cancelbtn" Text="Clear" OnClick="btnCancel_Click" />
                            <asp:Button ID="btnSave" runat="server" CssClass="btn savebtn" Text="Save" OnClientClick="return fnValidateSave()"
                                OnClick="btnSave_Click" />
                            <asp:Button ID="btnDelete" runat="server" CssClass="btn backbtn1" Text="Delete"
                                OnClick="btnDelete_Click1" />
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
