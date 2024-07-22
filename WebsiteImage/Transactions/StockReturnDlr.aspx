<%@ page language="C#" autoeventwireup="true" inherits="Transactions_StockReturnDlr, App_Web_3kgqgt3u" %>

<%@ Register Src="~/HomeTopFrame.ascx" TagName="HomeTopFrame" TagPrefix="huc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Secondary Sales System</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />

    <script type="text/javascript" language="javascript">
     function popupItem(strSelectType,strCount,strDlrCode,strType) {           
         window.open("../Popups/popupDealerItem.aspx?SelectType=" + strSelectType +"&strCount="+strCount+"&Dlr="+strDlrCode+"&strType="+strType , "CPO", "location=0,status=1,scrollbars=1,toolbar=0,width=900,height=400,resizable=no");
              
      }
     function fnValidateSave() {
        var lblMsg = document.getElementById("lblMessage");
        var IsValid;  
        IsValid=false;     
        
          if (fnNoBlank(document.getElementById("txtMonth").value, "Month", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtMonth").focus();
                return false;
            }
          if (fnNoBlank(document.getElementById("txtYear").value, "Year", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtYear").focus();
                return false;
            }
          if (fnNoChar(document.getElementById("txtYear").value, "Year", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtYear").focus();
                return false;
            }
        
        IsValid=fnValidateItems();
        if(IsValid==true)
          return true;
        else
          return false;
     }
     
      function fnValidateItems()
      {
        var hdnCount=document.getElementById("hidSearchCount").value;
        var i,cnt;
        var lblMsg = document.getElementById("lblMessage");
        var returnFlag;
        returnFlag=false;      
        cnt=0;
        
        for(i=1;i<=hdnCount;i++)
         {
         if(document.getElementById("hdnRow_"+i).value=="S")
             continue;
           cnt=cnt+1;
           if (fnNoBlank(document.getElementById("txtItemCode_"+i).value, "Item Code", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtItemCode_"+i).focus();
                return false;
            }
            if (fnNoBlank(document.getElementById("txtReturnQty_"+i).value, "Return Qty", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtReturnQty_"+i).focus();
                return false;
            }
          if (fnNoChar(document.getElementById("txtReturnQty_"+i).value, "Return Qty", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtReturnQty_"+i).focus();
                return false;
            }
          if (document.getElementById("txtReturnQty_"+i).value <= 0)
          {
                alert("Qty Should be Greater Than Zero.")
                document.getElementById("txtReturnQty_"+i).focus();
                return false;
          }
           var Reason = document.getElementById("txtReason_"+i).value;
            if (Reason.length>100)
          {
                alert("Reason length Should be Lesser Than 100.")
                document.getElementById("txtReturnQty_"+i).focus();
                return false;
          }
          
          returnFlag=true;
         }
         
        if(cnt<1)
        {
         alert("Please Add Atleast One Row.");
         return false;
        }
        
         if (returnFlag==false)
             return false;
         else
             return true;
      }
     
    </script>

</head>
<body>
    <form id="frmStockReturnDlr" runat="server">
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
                    Stock Return - Dealer
                </td>
            </tr>
        </table>
        <br />
        <asp:UpdatePanel ID="upStockRet" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hidSearchCount" runat="server" Value="0" />
                <asp:HiddenField ID="hdnIntMonth" runat="server" Value="0" />        
                <asp:HiddenField ID="hdnCurrentItemId" runat="server" Value="0" />
        
                <table width="95%" class="alternatecolorborder" id="TblParam" runat="server" border="0"
                    cellspacing="0" cellpadding="5">
                    <tr>
                        <td align="right">
                            Month:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtMonth" runat="server" CssClass="commonfont textdropwidth" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td align="right">
                            Year:
                        </td>
                        <td align="left" colspan="3">
                            <asp:TextBox ID="txtYear" runat="server" CssClass="commonfont textdropwidth" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Sales Type:<var class="starColor">*</var>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlSalesType" runat="server" CssClass="commonfont textdropwidth"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlSalesType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            Return Date:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtReturnDate" runat="server" CssClass="commonfont textdropwidth"
                               ></asp:TextBox>
                            <asp:ImageButton ID="imgReturnDate" runat="server" ImageUrl="~/Common/Images/datePickerPopup.gif"
                                Style="vertical-align: bottom;" />
                            <ajaxToolkit:CalendarExtender ID="calReturnDate" runat="server" TargetControlID="txtReturnDate"
                                PopupButtonID="imgReturnDate" CssClass="CalendarDatePicker" PopupPosition="BottomRight">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                        <td align="right">
                            Invoice No:<var class="starColor">*</var>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtInvoiceNo" runat="server" CssClass="commonfont textdropwidth"></asp:TextBox>
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
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn cancelbtn" Text="Clear" OnClick="btnCancel_Click" />
                            <asp:Button ID="btnSave" runat="server" CssClass="btn savebtn" Text="Save" OnClientClick="return fnValidateSave()"
                                OnClick="btnSave_Click" />
                            <asp:Button CssClass="btn addnew" ToolTip="Add New Record" ID="btnAdd" Text="Add Item"
                                runat="server" OnClick="btnAdd_Click"></asp:Button>
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
