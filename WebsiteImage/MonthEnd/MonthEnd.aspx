﻿<%@ page language="C#" autoeventwireup="true" inherits="MonthEnd_MonthEnd, App_Web_uumspmds" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/HomeTopFrame.ascx" TagName="HomeTopFrame" TagPrefix="huc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Secondary Sales System</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <script type="text/javascript" language="javascript">
      function fnValidateSave() {             
        var lblMsg = document.getElementById("lblMessage");
        var CurrMonth = document.getElementById("hdnCurrMonth");
        var CurrYear = document.getElementById("hdnCurrYear");        
        
        if (fnNoBlank(document.getElementById("txtMonth").value, "Month.", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtMonth").focus();                
                return false;
            }
        if (fnNoBlank(document.getElementById("txtYear").value, "Year.", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtYear").focus();                
                return false;
            }
       if (fnNoChar(document.getElementById("txtYear").value, "Year", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtYear").focus();
                return false;
            }
       if(document.getElementById("txtMonth").value==CurrMonth.value && document.getElementById("txtYear").value==CurrYear.value)
       {
             alert("Month End Cannot Be Closed For The Current Month.")             
             return false;
       }
       if(!confirm("Confirm Month End"))
       return false;
      }
    </script>
</head>
<body>
    <form id="frmMonthEnd" runat="server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" />
     <asp:UpdatePanel ID="CalPanel" runat="server">
       <ContentTemplate>
         <center>
          <asp:HiddenField ID="hdnCurrMonth" runat="server" />
          <asp:HiddenField ID="hdnCurrYear" runat="server" />
          <asp:HiddenField ID="hdnOpenMonthNo" runat="server" />
           <table width="95%" border="0" cellspacing="0" cellpadding="0" style="vertical-align: top;">
            <tr>
             <td>                    
              <huc1:HomeTopFrame runat="server" ID="TopFrame" />
             </td>
            </tr>
           </table>
           <br />
        <table width="95%" border="0" cellspacing="" cellpadding="0">
            <tr>
                <td align="left" class="pagetitleDetail">
                    Month End
                </td>
            </tr>
        </table>
        <br /> 
        <table class="HeaderDetailsGrid" width="95%" border="0" cellspacing="0">
         <tr>
           <td align="right">
             Month:&nbsp;&nbsp;
           </td>
           <td align="left">
            <asp:TextBox ID="txtMonth" runat="server" CssClass="commonfont textdropwidth" ReadOnly="true"></asp:TextBox>
           </td>
           <td align="right">
             Year:&nbsp;&nbsp;
           </td>
           <td align="left">
            <asp:TextBox ID="txtYear" runat="server" CssClass="commonfont textdropwidth" ReadOnly="true"></asp:TextBox>
           </td>           
         </tr>
        </table>
        <table width="95%" class="datatableth" border="0" cellspacing="0" cellpadding="0">
         <tr>
          <td>
           <asp:Button CssClass="btn releasebtn" ToolTip="Clsoe The Open Month" ID="btnMonthEnd" 
                  Text="Month End" runat="server" OnClientClick="return fnValidateSave()" onclick="btnMonthEnd_Click"></asp:Button>
           <asp:Button CssClass="btn initializebtn" ToolTip="Initialize Month End" ID="btnMonthInitialize" 
                  Text="Initialize Month" runat="server" onclick="btnMonthInitialize_Click" />
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
