<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExecutiveMasterAddEdit.aspx.cs" Inherits="Masters_ExecutiveMasterAddEdit" %>
<%@ Register Src="~/HomeTopFrame.ascx" TagName="HomeTopFrame" TagPrefix="huc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Secondary Sales System</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    
    <script type="text/javascript" language="javascript">
        function popupUser(strSelectType) {
            window.open("../Popups/popupUserMaster.aspx?SelectType=" + strSelectType, "UUM", "location=0,status=1,scrollbars=1,toolbar=0,width=900,height=400,resizable=no");
        }

        function fnValidateSave() {
        var lblMsg = document.getElementById("lblMessage");
        if (fnNoBlank(document.getElementById("txtExecutiveCode").value, "Executive Code", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtExecutiveCode").focus();
                return false;
            }
         if (fnNoSpaceSpecial(document.getElementById("txtExecutiveCode").value, "Executive Code", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtExecutiveCode").focus();
                return false;
            }
         if (fnCheckLength(document.getElementById("txtExecutiveCode").value, "Executive Code", "10", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtExecutiveCode").focus();
                return false;
            }
         if (fnCheckLength(document.getElementById("txtDescrip").value, "Description", "50", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtDescrip").focus();
                return false;
            }
        if (fnComboSelect(document.getElementById("ddlRegion").value, "Region", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("ddlRegion").focus();
                return false;
            }
        }
    </script>
</head>
<body>
    <form id="frmExecutiveMasterAddEdit" runat="server">
    <asp:HiddenField ID="hdnEMId" Value="0" runat="server" />
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
                    <asp:Label ID="lblHeader" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
        <br />
       <table width="95%" class="alternatecolorborder" id="TblParam" runat="server" border="0"
            cellspacing="0" cellpadding="5">
         <tr>
          <td align="right">
              Executive Code:<var class="starColor">*</var>
          </td>
          <td align="left">
             <asp:TextBox ID="txtExecutiveCode" MaxLength="10" runat="server" CssClass="commonfont textdropwidth"></asp:TextBox>
          </td>    
              <td align="right">
                    User Id:<var class="starColor">*</var>
                </td>
                <td align="left">
                    <asp:TextBox type="text" id="txtUserId"  runat="server" 
                        class="commonfont textdropwidth"  ></asp:TextBox>
                    <input type="button" value="..." id="btnUser" runat="server" class="popButton"
                        onclick="popupUser('ExecutiveMaster')" />                    
                </td>
               
          <td align="right">
              Description:
          </td>
          <td align="left">
             <asp:TextBox ID="txtDescrip" TextMode="MultiLine" runat="server" Rows="3" Columns="4" Width="250px" CssClass="commonfont textdropwidth"></asp:TextBox>
          </td>
         </tr>
         <tr>
          <td align="right">
              Region:<var class="starColor">*</var>
          </td>
          <td align="left">
             <asp:DropDownList ID="ddlRegion" runat="server" CssClass="commonfont textdropwidth"></asp:DropDownList>
          </td>
          <td align="right" >
              Active
          </td>
          <td align="left" colspan="3">
             <asp:CheckBox ID="chkActive" Checked="true" runat="server" CssClass="commonfont textdropwidth" />
          </td>
         </tr>
       </table>
       <table width="95%" class="datatableth" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <asp:Button ID="btnBack" runat="server" CssClass="btn backbtn" Text="Back" 
                        onclick="btnBack_Click"/>
                    <asp:Button ID="btnCancel" runat="server" CssClass="btn cancelbtn" 
                        Text="Clear" onclick="btnCancel_Click"/>
                    <asp:Button ID="btnSave" runat="server" CssClass="btn savebtn" Text="Save" 
                        onclick="btnSave_Click"  />
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
    </form>
</body>
</html>

