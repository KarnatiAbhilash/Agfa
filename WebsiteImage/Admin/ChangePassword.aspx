<%@ page language="C#" autoeventwireup="true" inherits="Admin_ChangePassword, App_Web_nr1inqul" %>
<%@ Register Src="~/HomeTopFrame.ascx" TagName="HomeTopFrame" TagPrefix="huc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head >
    <title>Secondary Sales System</title>
    <script type="text/javascript" language="javascript">
        function fnValidateSave() {
          var lblMsg = document.getElementById("lblMessage");
           if (fnNoBlank(document.getElementById("txtPwd").value, "Old Password", lblMsg) != true)
              
                 {
                    alert(lblMsg.value);
                    document.getElementById("txtPwd").focus();
                    return false;
                }
                
                 if (fnCheckLength(document.getElementById("txtPwd").value, "Old Password", "25", lblMsg) != true) {
                    alert(lblMsg.value)
                    document.getElementById("txtPwd").focus();
                    return false;
                }
                if (fnNoSpaceSpecial(document.getElementById("txtPwd").value, "Old Password", lblMsg) != true) {
                    alert(lblMsg.value)
                    document.getElementById("txtPwd").focus();
                    return false;
            }
                
               if (fnNoBlank(document.getElementById("txtNewPwd").value, "New Password", lblMsg) != true) {
                    alert(lblMsg.value)
                    document.getElementById("txtNewPwd").focus();
                    return false;
                }
                if (document.getElementById("txtNewPwd").value != document.getElementById("txtConfirmPwd").value)
               {
                    alert("Invalid Password. New Password and Confirm Password Should be Same.");
                    document.getElementById("txtConfirmPwd").focus();
                    
                    return false;
               }
               
               
                if (fnCheckLength(document.getElementById("txtNewPwd").value, "New Password", "25", lblMsg) != true) {
                    alert(lblMsg.value)
                    document.getElementById("txtNewPwd").focus();
                    return false;
                }
                if (fnNoSpaceSpecial(document.getElementById("txtNewPwd").value, "New Password", lblMsg) != true) {
                    alert(lblMsg.value)
                    document.getElementById("txtNewPwd").focus();
                    return false;
            }
               
               if (fnCheckLength(document.getElementById("txtConfirmPwd").value, "Confirm Password", "25", lblMsg) != true) {
                    alert(lblMsg.value)
                    document.getElementById("txtConfirmPwd").focus();
                    return false;
                }
                if (fnNoSpaceSpecial(document.getElementById("txtConfirmPwd").value, "Confirm Password", lblMsg) != true) {
                    alert(lblMsg.value)
                    document.getElementById("txtConfirmPwd").focus();
                    return false;
            }
                

   
           }
         
    </script>

    
    </head>
<body>
    <form id="formchangePassword" runat="server" >
    <div>
    <center>
        <table width="95%" border="0" cellspacing="0" cellpadding="0" style="vertical-align: top;">
            <tr>
                <td>
                    <huc1:HomeTopFrame runat="server" ID="TopFrame" />
                </td>
            </tr>
        </table>
        <br />
        <table width="95%" border="0" cellspacing="0" cellpadding="0" >
            <tr>
                <td align="left" class="pagetitleDetail">
                    Change Password
                </td>
            </tr>
        </table>
       <table width="95%" class="alternatecolorborder" id="TblParam" runat="server" border="0"
            cellspacing="0" cellpadding="5">
         <tr>
          <td align="right">
                            User Name:
          </td>
          <td align="left">
                            <asp:TextBox ID="txtUserId" class="commonfont textdropwidth" runat="server" 
                                Width="171px" ReadOnly="True"></asp:TextBox>
          </td>         
          <td align="right">
                            Old Password</td>
          <td align="left">
                            <asp:TextBox ID="txtPwd" class="commonfont textdropwidth" TextMode="Password" runat="server" Width="171px"></asp:TextBox>
          </td>
         </tr>
         <tr>
          <td align="right">
                            New Password</td>
          <td align="left">
                            <asp:TextBox ID="txtNewPwd" class="commonfont textdropwidth" TextMode="Password" runat="server" Width="171px"></asp:TextBox>
          </td>         
          <td align="right">
                            Confirm Password</td>
          <td align="left">
                            <asp:TextBox ID="txtConfirmPwd" class="commonfont textdropwidth"  TextMode="Password" runat="server" 
                                Width="171px"></asp:TextBox>
          </td>
         </tr>
         </table>
       <table width="95%" class="datatableth" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <asp:Button ID="btnCancel" runat="server" CssClass="btn cancelbtn" 
                        Text="Clear" onclick="btnCancel_Click" />
                    <asp:Button ID="btnSave" runat="server" CssClass="btn savebtn" Text="Save" onclick="btnSave_Click" 
                         />
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
       
        <br />
       
    </center>
    </div>
    </form>
</body>
</html>
