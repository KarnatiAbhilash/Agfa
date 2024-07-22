<%@ page language="C#" autoeventwireup="true" inherits="Masters_InstallBase, App_Web_ojonuiur" %>

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
        var strCurrDate = document.getElementById("hdnCurrentDate").value;
        var strDateFormat = document.getElementById("hdnDateFormat").value;
        var returnFlag;
        returnFlag=false;
        for(i=1;i<=hdnCount;i++)
        {
          if(document.getElementById("hdnRow_"+i).value=="S")
             continue;
             
             
                     if(document.getElementById("txtEUPrice_"+i).value!='' || document.getElementById("txtEUPerSqMt_"+i).value!='' || document.getElementById("txtProfitActual_"+i).value!='' ||  document.getElementById("txtProfitAgreed_"+i).value!='' ||document.getElementById("txtValidUpto_"+i).value!='')
               {
                      if (fnNoBlank(document.getElementById("txtEUPrice_"+i).value, "EU Price", lblMsg) != true && fnNoBlank(document.getElementById("txtEUPerSqMt_"+i).value, "EU PerSqrMt", lblMsg) != true) {
                            alert("Please Enter Either EU Price or EU PerSqrMt. Both Should not Be Blank.");
                            document.getElementById("txtEUPrice_"+i).focus();
                            return false;
                        }
                      if (fnDecimal(document.getElementById("txtEUPrice_"+i).value, "EU Price", lblMsg) != true) {
                            alert(lblMsg.value)
                            document.getElementById("txtEUPrice_"+i).focus();
                            return false;
                        }
                        if (document.getElementById("txtEUPrice_"+i).value<=0) {
                           alert("EUPrice Must be Greater than zero");
                           document.getElementById("txtEUPrice_"+i).focus();
                           return false;
                        }
                      if (fnDecimal(document.getElementById("txtEUPerSqMt_"+i).value, "EU PerSqrMt", lblMsg) != true) {
                            alert(lblMsg.value)
                            document.getElementById("txtEUPerSqMt_"+i).focus();
                            return false;
                        }
                      if (document.getElementById("txtEUPerSqMt_"+i).value<=0) {
                           alert("EUPerSqMt Must be Greater than zero");
                           document.getElementById("txtEUPerSqMt_"+i).focus();
                           return false;
                        }
                      if (fnNoBlank(document.getElementById("txtProfitActual_"+i).value, "Profit Actual", lblMsg) != true) {
                            alert(lblMsg.value)
                            document.getElementById("txtProfitActual_"+i).focus();
                            return false;
                        }
                      if (fnDecimal(document.getElementById("txtProfitActual_"+i).value, "Profit Actual", lblMsg) != true) {
                            alert(lblMsg.value)
                            document.getElementById("txtProfitActual_"+i).focus();
                            return false;
                        }

                      if (fnNoBlank(document.getElementById("txtProfitAgreed_"+i).value, "Profit Agreed", lblMsg) != true) {
                            alert(lblMsg.value)
                            document.getElementById("txtProfitAgreed_"+i).focus();
                            return false;
                        }
                      if (fnDecimal(document.getElementById("txtProfitAgreed_"+i).value, "Profit Agreed", lblMsg) != true) {
                            alert(lblMsg.value)
                            document.getElementById("txtProfitAgreed_"+i).focus();
                            return false;
                        }

                      if (fnNoBlank(document.getElementById("txtValidUpto_"+i).value, "Valid Upto", lblMsg) != true) {
                            alert(lblMsg.value)
                            document.getElementById("txtValidUpto_"+i).focus();
                            return false;
                        }            
                      if (!DateDiffVal(strCurrDate, document.getElementById("txtValidUpto_"+i).value, strDateFormat, "Valid Upto", "Current Date", "G", "", "")) {
                            document.getElementById("txtValidUpto_"+i).focus();
                            return false;
                        }     
                      if (fnCheckLength(document.getElementById("txtRemarks_"+i).value, "Remarks", "200", lblMsg) != true) {
                         alert(lblMsg.value)
                         document.getElementById("txtRemarks_"+i).focus();
                         return false;
                       }
                        returnFlag=true;
                }
           
           
        }
        
        if (returnFlag==false)
             return false;
        else
            return true;
            
      }
     function CalcPerSqrMt(rowCount)
     {
       var lblConFact=document.getElementById("lblConvFactor_"+rowCount);
       var decConvFactor=0;
       var decEUPrice=0;
       var decPerSqrMt=0;
       
       decConvFactor=lblConFact.innerText;
       decEUPrice=document.getElementById("txtEUPrice_"+rowCount).value;
       
       if(decConvFactor!=null && decEUPrice!=null && decEUPrice!='' && decConvFactor!='' && isNaN(decConvFactor) == false && isNaN(decEUPrice) == false)
       {       
          decPerSqrMt=decEUPrice/decConvFactor;
          document.getElementById("txtEUPerSqMt_"+rowCount).value=decPerSqrMt;
       }
     }
     
     function CalcDlrPrice(rowCount)
     {
       var lblConFact=document.getElementById("lblConvFactor_"+rowCount);
       var decConvFactor=0;
       var decEUPrice=0;
       var decPerSqrMt=0;
       
       decConvFactor=lblConFact.innerText;
       decPerSqrMt=document.getElementById("txtEUPerSqMt_"+rowCount).value;
       
       if(decConvFactor!=null && decPerSqrMt!=null && decPerSqrMt!='' && decConvFactor!='' && isNaN(decConvFactor) == false && isNaN(decPerSqrMt) == false)
       {       
          decEUPrice=decPerSqrMt*decConvFactor;
          document.getElementById("txtEUPrice_"+rowCount).value=decEUPrice;
       }
      }
     
     function CalcProfitActual(rowCount)
     {
       var decEUPrice=0;
       var decDlrPrice=0
       var decProfitActual=0;
       var lblDlrPrice=document.getElementById("lblDlrPrice_"+rowCount);       
       decEUPrice=document.getElementById("txtEUPrice_"+rowCount).value
       decDlrPrice=lblDlrPrice.innerText;
       
       if(decDlrPrice!=null && decEUPrice!=null && decEUPrice!='' && decDlrPrice!='' && isNaN(decDlrPrice) == false && isNaN(decEUPrice) == false)
       {
         decProfitActual=((decEUPrice-decDlrPrice)/decEUPrice)*100;         
         document.getElementById("txtProfitActual_"+rowCount).value=decProfitActual;
       }
     } 
    </script>

</head>
<body>
    <form id="frmCustItemPrice" runat="server">
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
                    Customer Install Base:
                </td>
            </tr>
        </table>
        <br />
        <asp:UpdatePanel ID="upReceipt" runat="server">
            <ContentTemplate>
                <asp:HiddenField runat="server" ID="hidSearchCount" Value="0" />
                <asp:HiddenField ID="hdnCurrentDate" runat="server" />
                <asp:HiddenField ID="hdnDateFormat" runat="server" />
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
