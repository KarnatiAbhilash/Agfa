<%@ page language="C#" autoeventwireup="true" inherits="Admin_SalesEmployeeAddEdit, App_Web_nr1inqul" %>

<%@ Register Src="~/HomeTopFrame.ascx" TagName="HomeTopFrame" TagPrefix="huc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Secondary Sales System</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />

    <script type="text/javascript" language="javascript">        


        function fnValidateSave() {
            var lblMsg = document.getElementById("lblMessage");

            //checks Name must enter
            if (fnNoBlank(document.getElementById("txtName").value, "Name", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtName").focus();
                return false;
            }
           
            //this is Region Must Enter
            if (fnNoBlank(document.getElementById("txtRegion").value, "Region", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtRegion").focus();
                return false;
            }

            //this is EmailId must enter
            if (fnNoBlank(document.getElementById("txtEmail").value, "Email Id", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtEmail").focus();
                return false;
            }

            //this is EmailId must enter @validation
            //if (document.getElementById("txtEmail").value != "") {
            //    if (fnEMailValidate(document.getElementById("txtEmail").value) != true) {
            //        document.getElementById("txtEmail").focus();
            //        return false;
            //    }
            //}

            if (fnEMailValidate(document.getElementById("txtEmail").value) != true) {
                document.getElementById("txtEmail").focus();
                return false;
            }


            //checks contact no  must enter
            if (fnNoBlank(document.getElementById("txtContNo").value, "Contact Number", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtContNo").focus();
                return false;
            }

            if (document.getElementById("txtContNo").value != "") {
                if (fnNoChar(document.getElementById("txtContNo").value, "Contact No", lblMsg) != true) {
                    alert(lblMsg.value)
                    document.getElementById("txtContNo").focus();
                    return false;
                }
            }
        }
    </script>

    <style type="text/css">
        .auto-style1 {
            width: 312px;
        }
        .auto-style2 {
            width: 296px;
        }
    </style>

</head>
<body>
    <form id="frmItemMasterAddEdit" runat="server">
    <asp:HiddenField ID="hdnBCId" Value="0" runat="server" />
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
                      Name:<var class="starColor">*</var>
                    </td>
                    <td align="left" class="auto-style1">
                         <asp:TextBox type="text" id="txtName" AutoPostBack="true"  runat="server" 
                            class="commonfont textdropwidth" ontextchanged="txtProdGroup_TextChanged" ></asp:TextBox>

                    </td>
                    <td align="right">
                          Region:<var class="starColor">*</var>
                        </td>
                        <td align="left"  class="auto-style2">
                            <asp:TextBox type="text" ID="txtRegion" AutoPostBack="true" CssClass="commonfont textdropwidth"
                                runat="server" ></asp:TextBox>
                        </td>
                  </tr>
                  <tr>
                    <td align="right">
                         Emaild:<var class="starColor">*</var>
                    </td>
                    <td align="left" >
                        <asp:TextBox ID="txtEmail" TextMode="MultiLine" runat="server" Rows="3" Columns="4"
                            Width="250px" CssClass="commonfont textdropwidth"></asp:TextBox>
                    </td>
                
                      <td align="right" >
                        Contact Number:<var class="starColor">*</var>
                    </td>

                    <td align="left" colspan="2" >
                        <asp:TextBox ID="txtContNo" TextMode="MultiLine" runat="server" Rows="3" Columns="4"
                            Width="250px" CssClass="commonfont textdropwidth"></asp:TextBox>
                    </td>
                </tr>
        </table>
        <table width="95%" class="datatableth" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <asp:Button ID="btnBack" runat="server" CssClass="btn backbtn" Text="Back" OnClick="btnBack_Click" />
                    <asp:Button ID="btnCancel" runat="server" CssClass="btn cancelbtn" Text="Clear" OnClick="btnCancel_Click" />
                    <asp:Button ID="btnSave" runat="server" CssClass="btn savebtn" Text="Save" OnClick="btnSave_Click" />
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
