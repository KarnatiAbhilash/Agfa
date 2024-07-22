<%@ page language="C#" autoeventwireup="true" inherits="Transactions_PurchaseOrder, App_Web_3kgqgt3u" %>

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
        
      function fnCheckDuplicate(value)
      {          
      
         var ItemId =  $get('hdnCurrentItemId').value;            
         var hdnCount=document.getElementById("hidSearchCount").value;
         var i;
         var hdnRow;
         for(i=1;i<=hdnCount;i++)
          {            
          hdnRow=document.getElementById("txtItemCode_"+i);  
          if(ItemId!=("txtItemCode_"+i) && ItemId!=0)
          {
          if(value.toLowerCase() ==hdnRow.value.toLowerCase() && value!="" && hdnRow.value!="")
          {
          alert("You Cannot Insert Same Item Twice..")
          return false;
          }          
          }    
          }
          return true;      
      }
      
      function CalcInvoiceAmt(rowCount)
      {
        var Qty=0;
        var UnitPrice=0;
        var InvoiceAmt=0;
        Qty=document.getElementById("txtQty_"+rowCount).value
        UnitPrice=document.getElementById("txtUnitPrice_"+rowCount).value
        if(Qty!=null && UnitPrice!=null && Qty!='' && UnitPrice!='' && isNaN(Qty)==false && isNaN(UnitPrice)==false)
        {
          InvoiceAmt=Qty * UnitPrice;
          document.getElementById("txtInvoiceAmt_"+rowCount).value=InvoiceAmt;
        }
      }
      function CalcNetInvoiceAmt(rowCount)
      {
         var InvoiceAmt=0;
         var NetInvoiceAmt=0;
         var hdnCount=document.getElementById("hidSearchCount").value;
         var i;
         var hdnRow;
         for(i=1;i<=hdnCount;i++)
          {
            hdnRow=document.getElementById("hdnRow_"+i);                     
            if(hdnRow.value == "" || hdnRow.value == null)
            {
             InvoiceAmt=document.getElementById("txtInvoiceAmt_"+i).value;
             NetInvoiceAmt=parseFloat(NetInvoiceAmt) + parseFloat(InvoiceAmt);
            }
          }
          document.getElementById("txtNetInvoiceAmt").value=NetInvoiceAmt;
          
          if(document.getElementById("txtGrossInvoice").value=='NaN')
          document.getElementById("txtGrossInvoice").value='';
          
          if(document.getElementById("txtNetInvoiceAmt").value=='NaN')
          document.getElementById("txtNetInvoiceAmt").value=''; 
      }
      function CalcGrossAmount()
      {
       
        var NetInvoiceAmt=0;
        var SalesTax=0;
        var Locallevi=0;
        var Insurance=0;
        var Others=0;
        var SalesTaxAmt=0;
        var GrossAmt=0;
        
        CalcNetInvoiceAmt(0);
        NetInvoiceAmt=document.getElementById("txtNetInvoiceAmt").value;
        SalesTax=document.getElementById("txtSalesTax").value;
        Locallevi=document.getElementById("txtLocallevi").value;
        SalesTaxAmt=(SalesTax * NetInvoiceAmt)/100;
        Locallevi=((parseFloat(NetInvoiceAmt) + parseFloat(SalesTaxAmt))* Locallevi)/100;
        Insurance=document.getElementById("txtInsurence").value;
        Others=document.getElementById("txtOthers").value;
        
        if(Insurance!=null && Insurance!='' && isNaN(Insurance)== false && Others!=null && Others!='' && isNaN(Others)== false)
          GrossAmt=parseFloat(NetInvoiceAmt) + parseFloat(SalesTaxAmt) + parseFloat(Locallevi) + parseFloat(Insurance) + parseFloat(Others);
        else if(Insurance!=null && Insurance!='' && isNaN(Insurance)== false)
          GrossAmt=parseFloat(NetInvoiceAmt) + parseFloat(SalesTaxAmt) + parseFloat(Locallevi) + parseFloat(Insurance);
        else if(Others!=null && Others!='' && isNaN(Others)== false)
          GrossAmt=parseFloat(NetInvoiceAmt) + parseFloat(SalesTaxAmt) + parseFloat(Locallevi) + parseFloat(Others);
        else
         GrossAmt=parseFloat(NetInvoiceAmt) + parseFloat(SalesTaxAmt) + parseFloat(Locallevi);         
       
       
        document.getElementById("txtGrossInvoice").value=GrossAmt;
        
          if(document.getElementById("txtGrossInvoice").value=='NaN')
          document.getElementById("txtGrossInvoice").value='';
          
          if(document.getElementById("txtNetInvoiceAmt").value=='NaN')
          document.getElementById("txtNetInvoiceAmt").value=''; 
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
        if (fnNoBlank(document.getElementById("txtPONo").value, "PO.No", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtPONo").focus();
                return false;
            }
            
        IsValid=fnValidateItems();
        
        if(IsValid==true)
        {
          if (fnNoBlank(document.getElementById("txtSalesTax").value, "Sales Tax", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtSalesTax").focus();
                return false;
            }
          if (fnDecimal(document.getElementById("txtSalesTax").value, "Sales Tax", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtSalesTax").focus();
                return false;
            }
         
         if (document.getElementById("txtSalesTax").value >100)
          {
                alert("Sales Tax Should Not be Greater Than 100.")
                document.getElementById("txtSalesTax").focus();
                return false;
          }
          
              if (document.getElementById("txtSalesTax").value <=0)
          {
                alert("Sales Tax Should be Greater Than 0.")
                document.getElementById("txtSalesTax").focus();
                return false;
          }
          
          if (document.getElementById("txtLocallevi").value >100 && document.getElementById("txtLocallevi").value!="")
          {
                alert("Local Levi Should Not be Greater Than 100.")
                document.getElementById("txtLocallevi").focus();
                return false;
          }
          
          if(document.getElementById("txtLocallevi").value !="")
          {
            if (fnDecimal(document.getElementById("txtLocallevi").value, "Local Levi", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtLocallevi").focus();
                return false;
            }
          } 
          
          if(document.getElementById("txtInsurence").value !="")
          {
            if (fnDecimal(document.getElementById("txtInsurence").value, "Insurance", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtInsurence").focus();
                return false;
            }
          }
          
        if(document.getElementById("txtOthers").value !="")
          {
            if (fnDecimal(document.getElementById("txtOthers").value, "Others", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtOthers").focus();
                return false;
            }
          }
          
            var Remarks = document.getElementById('txtRemarks').value;      
            if(Remarks.length>200)
            {
            alert('Remarks Length Must not be Greater then 200');
            return false;            
            }

        }
        else
           return false;      
           
           CalcGrossAmount();  
           
           if(confirm('Do You Want To Save Data.'))
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
           if (fnNoBlank(document.getElementById("txtQty_"+i).value, "Qty", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtQty_"+i).focus();
                return false;
            }
          if (fnNoChar(document.getElementById("txtQty_"+i).value, "Qty", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtQty_"+i).focus();
                return false;
            }
          if (document.getElementById("txtQty_"+i).value <= 0)
          {
                alert("Qty Should be Greater Than Zero.")
                document.getElementById("txtQty_"+i).focus();
                return false;
          }        
          returnFlag=true;
         }
         
       if(cnt<1)
        {
         alert("Please Add Atleast One Row.");
         return false;
        }
      
         return fnCheckDuplicate(document.getElementById("txtItemCode_"+hdnCount).value);        
         
         if (returnFlag==false)
             return false;
         else
             return true;
     }
    </script>

</head>
<body>
    <form id="frmPurchaseOrder" runat="server">
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
                    Purchase Order
                </td>
            </tr>
        </table>
        <br />
        <asp:UpdatePanel ID="upPO" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hdnCurrentItemId" runat="server" Value="0" />
                <asp:HiddenField ID="hidSearchCount" runat="server" Value="0" />
                <asp:HiddenField ID="hdnDlrName" runat="server" />
                <table width="95%" class="alternatecolorborder" id="TblParam" runat="server" border="0"
                    cellspacing="0" cellpadding="5">
                    <tr>
                        <td align="right">
                            Supplier:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtSupplier" runat="server" Text="Agfa" CssClass="commonfont textdropwidth"
                                ReadOnly="true"></asp:TextBox>
                        </td>
                        <td align="right">
                            Month:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtMonth" runat="server" CssClass="commonfont textdropwidth" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td align="right">
                            Year:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtYear" runat="server" CssClass="commonfont textdropwidth" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            PO Type:<var class="starColor">*</var>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlPOType" runat="server" CssClass="commonfont textdropwidth"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlPOType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            PO No:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtPONo" runat="server" CssClass="commonfont textdropwidth" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td align="right">
                            PO Date:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtPODate" runat="server" CssClass="commonfont textdropwidth" ReadOnly="true"></asp:TextBox>
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
                <table width="95%" class="alternatecolorborder" id="Table1" runat="server" border="0"
                    cellspacing="0" cellpadding="5">
                    <tr>
                        <td align="right">
                            Sales Tax (CST/VAT %):<var class="starColor">*</var>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtSalesTax" MaxLength="6" runat="server" CssClass="commonfont textdropwidth right"></asp:TextBox>
                        </td>
                        <td align="right">
                            Local Levi (%):
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtLocallevi" MaxLength="6" runat="server" CssClass="commonfont textdropwidth right"></asp:TextBox>
                        </td>
                        <td align="right">
                            Net Invoice Amt:
                        </td>
                        <td align="left">
                            <%--<asp:TextBox ID="txtNetInvoiceAmt" runat="server" CssClass="commonfont textdropwidth" ReadOnly="true"></asp:TextBox>--%>
                            <input type="text" id="txtNetInvoiceAmt" runat="server" class="commonfont textdropwidth right"
                                readonly="readonly" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Insurance (Rs.):
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtInsurence" MaxLength="12" runat="server" CssClass="commonfont textdropwidth right"></asp:TextBox>
                        </td>
                        <td align="right">
                            Others (Rs.):
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtOthers" MaxLength="12" runat="server" CssClass="commonfont textdropwidth right"></asp:TextBox>
                        </td>
                        <td align="right">
                            Gross Invoice Amt:
                        </td>
                        <td align="left">
                            <%--<asp:TextBox ID="txtGrossInvoice" runat="server" CssClass="commonfont textdropwidth" ReadOnly="true"></asp:TextBox>--%>
                            <input type="text" id="txtGrossInvoice" runat="server" class="commonfont textdropwidth right"
                                readonly="readonly" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Remarks:
                        </td>
                        <td align="left" colspan="5">
                            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Rows="3" Width="600px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table width="95%" class="datatableth" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn cancelbtn" Text="Clear" OnClick="btnCancel_Click" />
                            <asp:Button ID="btnSave" runat="server" CssClass="btn savebtn" Text="Save" OnClientClick="return fnValidateSave();"
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
