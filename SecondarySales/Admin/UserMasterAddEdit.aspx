<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserMasterAddEdit.aspx.cs"
    Inherits="Admin_UserMasterAddEdit" %>
<%@ Register Src="~/HomeTopFrame.ascx" TagName="HomeTopFrame" TagPrefix="huc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Secondary Sales System</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />

    <script type="text/javascript" language="javascript">
        function fnValidateSave() {
        var lblMsg = document.getElementById("lblMessage");
        
        if (fnNoBlank(document.getElementById("txtUserName").value, "User Name", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtUserName").focus();
                return false;
            }
        if (fnNoNumeric(document.getElementById("txtUserName").value, "User Name", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtUserName").focus();
                return false;
            }
        if (fnCheckLength(document.getElementById("txtUserName").value, "User Name", "50", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtUserName").focus();
                return false;
            }
        if (fnNoBlank(document.getElementById("txtUserId").value, "User Id", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtUserId").focus();
                return false;
            }
        if (fnNoSpaceSpecial(document.getElementById("txtUserId").value, "User Id", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtUserId").focus();
                return false;
            }
        if (fnCheckLength(document.getElementById("txtUserId").value, "User Id", "20", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtUserId").focus();
                return false;
            }
        
        if (fnComboSelect(document.getElementById("ddlUserType").value, "User Type", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("ddlUserType").focus();
                return false;
            }
        if (fnNoBlank(document.getElementById("txtEmail").value, "Email Id", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtEmail").focus();
                return false;
            }
         if (fnEMailValidate(document.getElementById("txtEmail").value) != true) {                
                document.getElementById("txtEmail").focus();
                return false;
            } 
        if(document.getElementById("ddlUserType").value =="Dealer")
        {           
            if (fnNoBlank(document.getElementById("txtDealer").value, "Dealer Code", lblMsg) != true) {
                    alert(lblMsg.value)
                    document.getElementById("txtDealer").focus();
                    return false;
                }
        }
        
        if (fnSpecialWithSpace(document.getElementById("txtUserName").value, "UserName", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtUserName").focus();
                return false;
        }
        
        if (fnNoSpaceSpecial(document.getElementById("txtUserId").value, "UserId", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtUserId").focus();
                return false;
        }
        

        

        
       if(document.getElementById("trPwd").style.display=='block')
        {
            if (fnNoBlank(document.getElementById("txtPassword").value, "Password", lblMsg) != true) {
                    alert(lblMsg.value)
                    document.getElementById("txtPassword").focus();
                    return false;
                }
                
            if (fnCheckLength(document.getElementById("txtPassword").value, "Password", "25", lblMsg) != true) {
                    alert(lblMsg.value)
                    document.getElementById("txtPassword").focus();
                    return false;
                }
                
           if (fnNoSpaceSpecial(document.getElementById("txtPassword").value, "Password", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtPassword").focus();
                return false;
                }  
                
           if (fnNoBlank(document.getElementById("txtConfirmPassword").value, "Confirm Password", lblMsg) != true) {
                    alert(lblMsg.value)
                    document.getElementById("txtConfirmPassword").focus();
                    return false;
                }
                
            if (fnCheckLength(document.getElementById("txtConfirmPassword").value, "Confirm Password", "25", lblMsg) != true) {
                    alert(lblMsg.value)
                    document.getElementById("txtConfirmPassword").focus();
                    return false;
                }
        
           if (fnNoSpaceSpecial(document.getElementById("txtConfirmPassword").value, "Confirm Password", lblMsg) != true) {
                    alert(lblMsg.value)
                    document.getElementById("txtConfirmPassword").focus();
                    return false;
            }
            if (document.getElementById("txtPassword").value != document.getElementById("txtConfirmPassword").value)
               {
                    alert("Invalid Password. Password and Confirm Password Should be Same.");
                    document.getElementById("txtConfirmPassword").focus();
                    return false;
               }
           }
      }
      
      function popupDealer(strSelectType) {           
            window.open("../Popups/popupDealer.aspx?SelectType=" + strSelectType , "UMM", "location=0,status=1,scrollbars=1,toolbar=0,width=900,height=400,resizable=no");
        }
        
    </script>

</head>
<body>
    <form id="frmUserMasterAddEdit" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
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
        <asp:UpdatePanel ID="upDlr" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hdnUserId" Value="" runat="server" />
                <asp:HiddenField ID="hdnRowId" Value="0" runat="server" />
                <table width="95%" class="alternatecolorborder" id="TblParam" runat="server" border="0"
                    cellspacing="0" cellpadding="5">
                    <tr>
                        <td align="right">
                            User Name:<var class="starColor">*</var>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtUserName" MaxLength="50" runat="server" CssClass="commonfont textdropwidth"></asp:TextBox>
                        </td>
                        <td align="right">
                            User Id:<var class="starColor">*</var>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtUserId" MaxLength="20" runat="server" CssClass="commonfont textdropwidth"></asp:TextBox>
                        </td>
                    </tr>                    
                    <tr>
                        <td align="right">
                            User Type:<var class="starColor">*</var>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlUserType" runat="server" CssClass="commonfont textdropwidth"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlUserType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                          Email Id:<var class="starColor">*</var>
                        </td>
                        <td align="left">
                         <asp:TextBox ID="txtEmail" runat="server" MaxLength="50" CssClass="commonfont textdropwidth"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="trDlr" runat="server" style="display: none;">
                        <td align="right">
                            Dealer Code:<var class="starColor">*</var>
                        </td>
                        <td align="left">
                            <asp:TextBox type="text" id="txtDealer" runat="server" 
                                class="commonfont textdropwidth" AutoPostBack="True" 
                                ontextchanged="txtDealer_TextChanged" ></asp:TextBox>
                            <input type="button" value="..." id="btnDealer" runat="server" class="popButton"
                                onclick="popupDealer('UserMaster')" />
                        </td>
                        <td align="right">
                            Dealer Name:
                        </td>
                        <td align="left">
                            <input type="text" id="txtDlrName" readonly="readonly" runat="server" class="commonfont textdropwidth" />
                        </td>
                    </tr>
                    <tr id="trPwd" runat="server">
                        <td align="right">
                            Password:<var class="starColor">*</var>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtPassword" MaxLength="50" runat="server" CssClass="commonfont textdropwidth"
                                TextMode="Password"></asp:TextBox>
                            <asp:HiddenField ID="hdnPwd" runat="server" />
                        </td>
                        <td align="right">
                            Confirm Password:<var class="starColor">*</var>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtConfirmPassword" MaxLength="50" runat="server" CssClass="commonfont textdropwidth"
                                TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                      <td align="right">
                            Active:
                        </td>
                        <td align="left" colspan="3">
                            <asp:CheckBox ID="chkActive" Checked="true" runat="server" CssClass="commonfont textdropwidth" />
                        </td>
                    </tr>
                </table>         
        <table width="95%" class="datatableth" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <asp:Button ID="btnBack" runat="server" CssClass="btn backbtn" Text="Back" OnClick="btnBack_Click" />
                    <asp:Button ID="btnCancel" runat="server" CssClass="btn cancelbtn" Text="Clear" OnClick="btnCancel_Click" />
                    <asp:Button ID="btnSave" runat="server" CssClass="btn savebtn" Text="Save" OnClick="btnSave_Click" />
                    <asp:Button ID="btnChangePwd" runat="server" CssClass="btn editbtn" Text="Change Password"
                        OnClick="btnChangePwd_Click" />
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
