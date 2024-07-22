<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DealerItemMapping.aspx.cs"
    Inherits="Masters_DealerItemMapping" %>

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
               if(document.getElementById("txtDLRPrice_"+i).value!='' || document.getElementById("txtPerSqMt_"+i).value!='' || document.getElementById("txtMaxQty_"+i).value!='')
               {
               
                  if (fnNoBlank(document.getElementById("txtDLRPrice_"+i).value, "Dealer Price", lblMsg) != true && fnNoBlank(document.getElementById("txtPerSqMt_"+i).value, "PerSqrMt", lblMsg) != true) {
                        alert("Please Enter Either Dealer Price or PerSqrMt. Both Should not Be Blank.");
                        document.getElementById("txtDLRPrice_"+i).focus();
                        return false;
                    }
                 if (fnNoCharWithDot(document.getElementById("txtDLRPrice_"+i).value, "Dealer Price", lblMsg) != true) {
                   alert(lblMsg.value)
                   document.getElementById("txtDLRPrice_"+i).focus();
                   return false;
                 }
                if (document.getElementById("txtDLRPrice_"+i).value<=0) {
                   alert("DLRPrice Must be Greater than zero");
                   document.getElementById("txtDLRPrice_"+i).focus();
                   return false;
                 }
                 if (fnNoBlank(document.getElementById("txtMaxQty_"+i).value, "Max.Qty", lblMsg) != true) {
                    alert(lblMsg.value)
                    document.getElementById("txtMaxQty_"+i).focus();
                     return false;
                }
                if (fnNoCharWithDot(document.getElementById("txtMaxQty_"+i).value, "Max.Qty", lblMsg) != true) {
                   alert(lblMsg.value)
                   document.getElementById("txtMaxQty_"+i).focus();
                   return false;
                }                
                if (document.getElementById("txtMaxQty_"+i).value<=0) {
                   alert("Max Qty Must be Greater than zero");
                   document.getElementById("txtMaxQty_"+i).focus();
                   return false;
                 }
               if (fnNoCharWithDot(document.getElementById("txtPerSqMt_"+i).value, "PerSqMt", lblMsg) != true) {
                   alert(lblMsg.value)
                   document.getElementById("txtPerSqMt_"+i).focus();
                   return false;
                }
                if (document.getElementById("txtPerSqMt_"+i).value<=0) {
                   alert("Per SqMt Must be Greater than zero");
                   document.getElementById("txtPerSqMt_"+i).focus();
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
              
//          if (fnNoBlank(document.getElementById("txtDLRPrice_"+i).value, "Dealer Price", lblMsg) != true && fnNoBlank(document.getElementById("txtPerSqMt_"+i).value, "PerSqrMt", lblMsg) != true) {
//                alert("Please Enter Either Dealer Price or PerSqrMt. Both Should not Be Blank.");
//                document.getElementById("txtDLRPrice_"+i).focus();
//                return false;
//            }
//          if (fnDecimal(document.getElementById("txtDLRPrice_"+i).value, "DLR Price", lblMsg) != true) {
//                alert(lblMsg.value)
//                document.getElementById("txtDLRPrice_"+i).focus();
//                return false;
//            }
//          if (fnDecimal(document.getElementById("txtPerSqMt_"+i).value, "PerSqrMt", lblMsg) != true) {
//                alert(lblMsg.value)
//                document.getElementById("txtPerSqMt_"+i).focus();
//                return false;
//            }
//          if (fnNoBlank(document.getElementById("txtMaxQty_"+i).value, "Max.Qty", lblMsg) != true) {
//                alert(lblMsg.value)
//                document.getElementById("txtMaxQty_"+i).focus();
//                return false;
//            }
//          if (fnNoChar(document.getElementById("txtMaxQty_"+i).value, "Max.Qty", lblMsg) != true) {
//                alert(lblMsg.value)
//                document.getElementById("txtMaxQty_"+i).focus();
//                return false;
//            }
//          if (document.getElementById("txtMaxQty_"+i).value <= 0)
//          {
//                alert("Max. Qty Should be Greater Than Zero.")
//                document.getElementById("txtMaxQty_"+i).focus();
//                return false;
//          }
//          if (fnCheckLength(document.getElementById("txtRemarks_"+i).value, "Remarks", "200", lblMsg) != true) {
//             alert(lblMsg.value)
//             document.getElementById("txtRemarks_"+i).focus();
//             return false;
//           }
 //      

     
     function CalcPerSqrMt(rowCount)
     {
       var lblConFact=document.getElementById("lblConvFactor_"+rowCount);
       var decConvFactor=0;
       var decDlrPrice=0;
       var decPerSqrMt=0;
       
       decConvFactor=lblConFact.innerText;
       decDlrPrice=document.getElementById("txtDLRPrice_"+rowCount).value;
       
       if(decConvFactor!=null && decDlrPrice!=null && decDlrPrice!='' && decConvFactor!='' && isNaN(decConvFactor) == false && isNaN(decDlrPrice) == false)
       {       
          decPerSqrMt=decDlrPrice/decConvFactor;
          document.getElementById("txtPerSqMt_"+rowCount).value=decPerSqrMt;
       }
     }
    function CalcDlrPrice(rowCount)
     {
       var lblConFact=document.getElementById("lblConvFactor_"+rowCount);
       var decConvFactor=0;
       var decDlrPrice=0;
       var decPerSqrMt=0;
       
       decConvFactor=lblConFact.innerText;
       decPerSqrMt=document.getElementById("txtPerSqMt_"+rowCount).value;
       
       if(decConvFactor!=null && decPerSqrMt!=null && decPerSqrMt!='' && decConvFactor!='' && isNaN(decConvFactor) == false && isNaN(decPerSqrMt) == false)
       {       
          decDlrPrice=decPerSqrMt*decConvFactor;
          document.getElementById("txtDLRPrice_"+rowCount).value=decDlrPrice;
       }
     }
    </script>

</head>
<body>
    <form id="frmDealerItemMapping" runat="server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" />
    <table width="95%" class="alternatecolorborder" id="TblParam" runat="server" border="0"
        cellspacing="0" cellpadding="5">
        <tr>
            <td align="right">
                <b>Dealer Code:</b>
            </td>
            <td align="left">
                <asp:Label ID="lblDealerCode" runat="server" CssClass="commonfont textdropwidth"></asp:Label>
            </td>
            <td align="right">
                <b>Dealer Name:</b>
            </td>
            <td align="left">
                <asp:Label ID="lblDealerName" runat="server" CssClass="commonfont textdropwidth"></asp:Label>
            </td>
        </tr>
    </table>
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
                    Dealer Item Price
                </td>
            </tr>
        </table>
        <br />
        <asp:UpdatePanel ID="upReceipt" runat="server">
            <ContentTemplate>
                <asp:HiddenField runat="server" ID="hidSearchCount" Value="0" />
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
