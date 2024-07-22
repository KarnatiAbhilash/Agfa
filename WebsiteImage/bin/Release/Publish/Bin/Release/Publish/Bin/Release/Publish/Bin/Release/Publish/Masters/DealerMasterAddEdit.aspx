<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DealerMasterAddEdit.aspx.cs"
    Inherits="Masters_DealerMasterAddEdit" %>

<%@ Register Src="~/HomeTopFrame.ascx" TagName="HomeTopFrame" TagPrefix="huc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Secondary Sales System</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />

    <script type="text/javascript" language="javascript">        
        function popupExecutive(strSelectType) {           
            window.open("../Popups/popupExecutive.aspx?SelectType=" + strSelectType , "DEP", "location=0,status=1,scrollbars=1,toolbar=0,width=900,height=400,resizable=no");
        }
        function popupUser(strSelectType) {           
            window.open("../Popups/popupMultiUserMaster.aspx?SelectType=" + strSelectType , "DUM", "location=0,status=1,scrollbars=1,toolbar=0,width=900,height=400,resizable=no");
        }
        
        function fnValidateSave() {
        var lblMsg = document.getElementById("lblMessage");
        if (fnNoBlank(document.getElementById("txtExecutive").value, "Executive Code", lblMsg) != true) {
                alert("Please Select Executive Code.");
                document.getElementById("txtExecutive").focus();
                return false;
            }
         if (fnComboSelect(document.getElementById("ddlRegion").value, "Region", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("ddlRegion").focus();
                return false;
            }
         if (fnNoBlank(document.getElementById("txtDMSCode").value, "DMS Code", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtDMSCode").focus();
                return false;
            } 
         if (fnNoSpaceSpecial(document.getElementById("txtDMSCode").value, "DMS Code", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtDMSCode").focus();
                return false;
            }
         if (fnNoBlank(document.getElementById("txtDealerName").value, "Dealer Name", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtDealerName").focus();
                return false;
            } 
//         if (fnSpecialWithSpace(document.getElementById("txtDealerName").value, "Dealer Name", lblMsg) != true) {
//                alert(lblMsg.value)
//                document.getElementById("txtDealerName").focus();
//                return false;
//            } 
           if (fnNoBlank(document.getElementById("txtAddress1").value, "Address1", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtAddress1").focus();
                return false;
            }
            
//           if (fnNoPercentLessGreaterSingleQuoteSpecial(document.getElementById("txtAddress1").value, "Address1", lblMsg) != true) {
//                alert(lblMsg.value)
//                document.getElementById("txtAddress1").focus();
//                return false;                
//            }     

//               if(document.getElementById("txtAddress2").value!="")
//            { 
//           if (fnNoPercentLessGreaterSingleQuoteSpecial(document.getElementById("txtAddress2").value, "Address2", lblMsg) != true) {
//                alert(lblMsg.value)
//                document.getElementById("txtAddress2").focus();
//                return false;
//            } 
//            }

//               if(document.getElementById("txtAddress3").value!="")
//            {       
//             if (fnNoPercentLessGreaterSingleQuoteSpecial(document.getElementById("txtAddress3").value, "Address3", lblMsg) != true) {
//                alert(lblMsg.value)
//                document.getElementById("txtAddress3").focus();
//                return false;
//            }
//            }
            
           if (fnNoBlank(document.getElementById("txtCity").value, "City", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtCity").focus();
                return false;
            }
//             if (fnNoNumeric(document.getElementById("txtCity").value, "City", lblMsg) != true) {
//                    alert("Please Remove Number(s) From City");
//                    document.getElementById("txtCity").focus();
//                    return false;
//                }
//                
//                if (fnNoSpaceSpecial(document.getElementById("txtCity").value, "City", lblMsg) != true) {
//                alert(lblMsg.value)
//                document.getElementById("txtCity").focus();
//                return false;
//                }
        
         if (fnComboSelect(document.getElementById("ddlState").value, "State", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("ddlState").focus();
                return false;
            }      


         if(document.getElementById("txtPincode").value !="")
         {
                if (fnNoSpaceSpecial(document.getElementById("txtPincode").value, "Pin Code", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtPincode").focus();
                return false;
                }
             if (fnNoChar(document.getElementById("txtPincode").value, "Pincode", lblMsg) != true) {
                    alert(lblMsg.value)
                    document.getElementById("txtPincode").focus();
                    return false;
                }
                
         }
//         if(document.getElementById("txtContPerson").value !="")
//         {
//             if (fnNoNumeric(document.getElementById("txtContPerson").value, "Contact Person", lblMsg) != true) {
//                    alert(lblMsg.value)
//                    document.getElementById("txtContPerson").focus();
//                    return false;
//                }
//            if (fnSpecialWithSpace(document.getElementById("txtContPerson").value, "Contact Person", lblMsg) != true) {
//            alert(lblMsg.value)
//            document.getElementById("txtContPerson").focus();
//            return false;
//             }
//         }

         if(document.getElementById("txtContNo").value !="")
         {    
            
//            if (fnNoSpaceSpecial(document.getElementById("txtContNo").value, "Contact No.", lblMsg) != true) {
//            alert(lblMsg.value)
//            document.getElementById("txtContNo").focus();
//            return false;
//             }
             if (fnNoChar(document.getElementById("txtContNo").value, "Contact No.", lblMsg) != true) {
                    alert(lblMsg.value)
                    document.getElementById("txtContNo").focus();
                    return false;
                }
        
         }
         if (fnNoBlank(document.getElementById("txtEmailId").value, "Email Id", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtEmailId").focus();
                return false;
            }
         if (fnEMailValidate(document.getElementById("txtEmailId").value) != true) {                
                document.getElementById("txtEmailId").focus();
                return false;
            }
//         if(document.getElementById("txtFaxNo").value !="")
//         {
//             if (fnNoSpaceSpecial(document.getElementById("txtFaxNo").value, "Fax No", lblMsg) != true) {
//            alert(lblMsg.value)
//            document.getElementById("txtFaxNo").focus();
//            return false;
//            }
//             if (fnNoChar(document.getElementById("txtFaxNo").value, "Fax No", lblMsg) != true) {
//                    alert(lblMsg.value)
//                    document.getElementById("txtFaxNo").focus();
//                    return false;
//                }
//        
//         }

         
//           if(document.getElementById("txtTINNo").value!="")
//            { 
//               if (fnNoSpaceSpecial(document.getElementById("txtTINNo").value, "TIN No", lblMsg) != true) {
//                    alert(lblMsg.value)
//                    document.getElementById("txtTINNo").focus();
//                    return false;
//                }
//            }
//                  
//            if(document.getElementById("txtLSTNo").value!="")
//            {     
//                 if (fnNoSpaceSpecial(document.getElementById("txtLSTNo").value, "LST No", lblMsg) != true) {
//                    alert(lblMsg.value)
//                    document.getElementById("txtLSTNo").focus();
//                    return false;
//                }
//            }
             
//            if(document.getElementById("txtCSTNo").value!="")
//            {     
//             if (fnNoSpaceSpecial(document.getElementById("txtCSTNo").value, "CST No", lblMsg) != true) {
//                alert(lblMsg.value)
//                document.getElementById("txtCSTNo").focus();
//                return false;
//            }
//            }
//           
            if (fnNoBlank(document.getElementById("txtRespuser").value, "Resp. User", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtRespuser").focus();
                return false;
            } 
         }
    </script>

</head>
<body>
    <form id="frmDealerMasterAddEdit" runat="server">
    <asp:HiddenField ID="hdnDealerCode" Value="0" runat="server" />
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
                    <asp:TextBox type="text" ID="txtExecutive" AutoPostBack="true" runat="server" class="commonfont textdropwidth"
                        OnTextChanged="txtExecutive_TextChanged"></asp:TextBox>
                    <input type="button" value="..." id="btnExecutive" runat="server" class="popButton"
                        onclick="popupExecutive('Dealer')" />
                </td>
                <td align="right">
                    Region:<var class="starColor">*</var>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlRegion" runat="server" CssClass="commonfont textdropwidth">
                    </asp:DropDownList>
                </td>
                <td align="right">
                    Dealer Code:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtDealerCode" ReadOnly="true" runat="server" CssClass="commonfont textdropwidth"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    DMS Code:<var class="starColor">*</var>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtDMSCode" MaxLength="20" runat="server" CssClass="commonfont textdropwidth"></asp:TextBox>
                </td>
                <td align="right">
                    Dealer Name:<var class="starColor">*</var>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtDealerName" MaxLength="50" runat="server" CssClass="commonfont textdropwidth"></asp:TextBox>
                </td>
                <td align="right">
                    Address1:<var class="starColor">*</var>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtAddress1" MaxLength="25" runat="server" CssClass="commonfont textdropwidth"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Address2:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtAddress2" MaxLength="25" runat="server" CssClass="commonfont textdropwidth"></asp:TextBox>
                </td>
                <td align="right">
                    Address3:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtAddress3" MaxLength="25" runat="server" CssClass="commonfont textdropwidth"></asp:TextBox>
                </td>
                <td align="right">
                    City:<var class="starColor">*</var>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtCity" MaxLength="25" runat="server" CssClass="commonfont textdropwidth"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    State:<var class="starColor">*</var>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlState" runat="server" CssClass="commonfont textdropwidth">
                    </asp:DropDownList>
                </td>
                <td align="right">
                    Pincode:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtPincode" MaxLength="10" runat="server" CssClass="commonfont textdropwidth"></asp:TextBox>
                </td>
                <td align="right">
                    Contact Person:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtContPerson" MaxLength="50" runat="server" CssClass="commonfont textdropwidth"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Contact No.:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtContNo" MaxLength="15" runat="server" CssClass="commonfont textdropwidth"></asp:TextBox>
                </td>
                <td align="right">
                    Email Id:<var class="starColor">*</var>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtEmailId" MaxLength="50" runat="server" CssClass="commonfont textdropwidth"></asp:TextBox>
                </td>
                <td align="right">
                    Fax No.:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtFaxNo" MaxLength="15" runat="server" CssClass="commonfont textdropwidth"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    TIN No.:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtTINNo" MaxLength="15" runat="server" CssClass="commonfont textdropwidth"></asp:TextBox>
                </td>
                <td align="right">
                    CST No.:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtCSTNo" MaxLength="15" runat="server" CssClass="commonfont textdropwidth"></asp:TextBox>
                </td>
                <td align="right">
                    LST No.:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtLSTNo" MaxLength="15" runat="server" CssClass="commonfont textdropwidth"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Resp. User:<var class="starColor">*</var>
                </td>
                <td align="left">
                    <asp:TextBox type="text" ID="txtRespuser" runat="server" class="commonfont textdropwidth"
                        AutoPostBack="True" TextMode="MultiLine" Rows="4" Width="200px" OnTextChanged="txtRespuser_TextChanged"></asp:TextBox>
                    <input type="button" value="..." id="btnRespuser" runat="server" class="popButton"
                        onclick="popupUser('Dealer')" />
                    <asp:HiddenField ID="hdnRespUser" runat="server" />
                </td>
                <td align="right">
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
                    <asp:Button ID="btnBack" runat="server" CssClass="btn backbtn" Text="Back" OnClick="btnBack_Click" />
                    <asp:Button ID="btnCancel" runat="server" CssClass="btn cancelbtn" Text="Clear" OnClick="btnCancel_Click" />
                    <asp:Button ID="btnSave" runat="server" CssClass="btn savebtn" Text="Save" OnClick="btnSave_Click" />
                    <asp:Button ID="btnAddNew" runat="server" CssClass="btn addnewrow" Text="Item Price"
                        OnClick="btnAddNew_Click" Style="display: none;" />
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
