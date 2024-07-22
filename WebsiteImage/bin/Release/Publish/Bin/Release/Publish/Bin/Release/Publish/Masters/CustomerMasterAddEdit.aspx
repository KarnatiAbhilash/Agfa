<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerMasterAddEdit.aspx.cs"
    Inherits="Masters_CustomerMasterAddEdit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/HomeTopFrame.ascx" TagName="HomeTopFrame" TagPrefix="huc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Secondary Sales System</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />

    <script type="text/javascript" language="javascript">        
        function popupDealer(strSelectType) {
            window.open("../Popups/popupDealer.aspx?SelectType=" + strSelectType, "CustMaster", "location=0,status=1,scrollbars=1,toolbar=0,width=900,height=400,resizable=no");
        }

        function popupDemoDetails(strSelectType) {
            window.open("../Popups/popupDemoDetails.aspx?SelectType=" + strSelectType, "CustMaster", "location=0,status=1,scrollbars=1,toolbar=0,width=900,height=400,resizable=no");
        }


        //this is for popup cust group
        function popupCustGroup(strSelectType) {
            window.open("../Popups/popupCustGroup.aspx?SelectType=" + strSelectType, "CustMaster", "location=0,status=1,scrollbars=1,toolbar=0,width=900,height=400,resizable=no");
        }

        //this is for popup SalesEmployee
        function popupSalesEmployee(strSelectType) {
            window.open("../Popups/popupSalesEmployee.aspx?SelectType=" + strSelectType, "CustMaster", "location=0,status=1,scrollbars=1,toolbar=0,width=900,height=400,resizable=no");
        }

        //
        function popupSalesEmployee(strSelectType) {
            var SalesCls = document.getElementById("ddlRegion").value;
            window.open("../Popups/popupSalesEmployee.aspx?SelectType=" + strSelectType + "&SalesCls=" + SalesCls, "PMG", "location=0,status=1,scrollbars=1,toolbar=0,width=900,height=400,resizable=no");
        }


        //
        function popupProductFamily(strSelectType) {
            var BudgetCls = document.getElementById("ddlRegion").value;

            window.open("../Popups/popupProductFamily.aspx?SelectType=" + strSelectType + "&BudgetCls=" + BudgetCls, "PMG", "location=0,status=1,scrollbars=1,toolbar=0,width=900,height=400,resizable=no");
        }
        
        function fnValidateSave() {
            var lblMsg = document.getElementById("lblMessage");

            if (fnNoBlank(document.getElementById("txtDealer").value, "Dealer Code", lblMsg) != true) {
                alert("Please Select Dealer Code.");
                document.getElementById("txtDealer").focus();
                return false;
            }
            //this is for cust grropup
            if (fnNoBlank(document.getElementById("txtCustGroup").value, "Cust. Group", lblMsg) != true) {
                alert("Please Select CustGroup.");
                document.getElementById("txtCustGroup").focus();
                return false;
            }



            if (fnNoBlank(document.getElementById("InstBase").value, "Install Base", lblMsg) != true) {
                alert("Please Select Install Base.");
                document.getElementById("InstBase").focus();
                return false;
            }

            //this is for Direct Customer
            if (fnNoBlank(document.getElementById("drtCust").value, "Direct Customer", lblMsg) != true) {
                alert("Please Select Direct Customer.");
                document.getElementById("drtCust").focus();
                return false;
            }


            //validation of SAP code
            if (fnNoBlank(document.getElementById("txtDmsCode").value, "SAP Code", lblMsg) != true) {
                alert("Please Select SAP Code.");
                document.getElementById("txtDmsCode").focus();
                return false;
            }

            if (fnNoBlank(document.getElementById("txtCustName").value, "Customer Name", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtCustName").focus();
                return false;
            }


            if (document.getElementById("txtDmsCode").value != "") {
                if (fnNoSpaceSpecial(document.getElementById("txtDmsCode").value, "DmsCode", lblMsg) != true) {
                    alert(lblMsg.value)
                    document.getElementById("txtDmsCode").focus();
                    return false;
                }
            }

            if (fnNoBlank(document.getElementById("txtAddress1").value, "Address1", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtAddress1").focus();
                return false;
            }

            if (document.getElementById("txtAddress1").value != "") {
                if (fnNoPercentLessGreaterSingleQuoteSpecial(document.getElementById("txtAddress1").value, "Address1", lblMsg) != true) {
                    alert(lblMsg.value)
                    document.getElementById("txtAddress1").focus();
                    return false;
                }
            }

            if (document.getElementById("txtAddress2").value != "") {
                if (fnNoPercentLessGreaterSingleQuoteSpecial(document.getElementById("txtAddress2").value, "Address2", lblMsg) != true) {
                    alert(lblMsg.value)
                    document.getElementById("txtAddress2").focus();
                    return false;
                }
            }

            if (document.getElementById("txtAddress3").value != "") {
                if (fnNoPercentLessGreaterSingleQuoteSpecial(document.getElementById("txtAddress3").value, "Address3", lblMsg) != true) {
                    alert(lblMsg.value)
                    document.getElementById("txtAddress3").focus();
                    return false;
                }

            }

            if (fnNoBlank(document.getElementById("txtCity").value, "City", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtCity").focus();
                return false;
            }

            if (fnNoBlank(document.getElementById("txtEmailId").value, "Email Name", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtEmailId").focus();
                return false;
            }


            if (fnComboSelect(document.getElementById("ddlState").value, "State", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("ddlState").focus();
                return false;
            }

            if (document.getElementById("txtPincode").value != "") {
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

            if (document.getElementById("txtContNo").value != "") {
                if (fnNoChar(document.getElementById("txtContNo").value, "Contact No", lblMsg) != true) {
                    alert(lblMsg.value)
                    document.getElementById("txtContNo").focus();
                    return false;
                }
            }
            if (document.getElementById("txtEmailId").value != "") {
                if (fnEMailValidate(document.getElementById("txtEmailId").value) != true) {
                    document.getElementById("txtEmailId").focus();
                    return false;
                }
            }

            if (fnComboSelect(document.getElementById("ddlRegion").value, "Region", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("ddlRegion").focus();
                return false;
            }
            if (fnComboSelect(document.getElementById("ddlCustType").value, "Cust. Type", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("ddlCustType").focus();
                return false;
            }
        }
    </script>

	<style type="text/css">
        .auto-style1 {
            width: 127px;
        }
    </style>
</head>
<body>
    <form id="frmCustomerMaster" runat="server">
<ajaxToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" />   
    <asp:HiddenField ID="hdnCustCode" Value="0" runat="server" />
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
                <td align="right" class="auto-style1">
                    Dealer Code:<var class="starColor">*</var>
                </td>
				<td align="left">
                    <asp:TextBox type="text" ID="txtDealer" AutoPostBack="true" runat="server" class="commonfont textdropwidth"
                        OnTextChanged="txtDealer_TextChanged"></asp:TextBox>
                    <input type="button" value="..." id="btnDealer" runat="server" class="popButton"
                        onclick="popupDealer('Cust')" /> 
                                 
                </td>
                <td align="right">
                    Customer Code:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtCustCode" ReadOnly="true" runat="server" CssClass="commonfont textdropwidth"></asp:TextBox>
                </td>
                <td align="right">
                    Customer Name:<var class="starColor">*</var>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtCustName" MaxLength="50" runat="server" CssClass="commonfont textdropwidth"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" class="auto-style1">
                    SAP Code:<var class="starColor">*</var>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtDmsCode" MaxLength="20" runat="server" CssClass="commonfont textdropwidth"></asp:TextBox>
                </td>
                <td align="right">
                    Address1:<var class="starColor">*</var>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtAddress1" MaxLength="25" runat="server" CssClass="commonfont textdropwidth"></asp:TextBox>
                </td>
                <td align="right">
                    Address2:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtAddress2" MaxLength="25" runat="server" CssClass="commonfont textdropwidth"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" class="auto-style1">
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
                <td align="right">
                    State:<var class="starColor">*</var>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlState" runat="server" CssClass="commonfont textdropwidth">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" class="auto-style1">
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
                <td align="right">
                    Contact No.:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtContNo" MaxLength="10" runat="server" CssClass="commonfont textdropwidth"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" class="auto-style1">
                    Email Id:<var class="starColor">*</var>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtEmailId" MaxLength="50" runat="server" CssClass="commonfont textdropwidth"></asp:TextBox>
                </td>
<%--                <td align="right">
                    CST No.:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtCSTNo" MaxLength="15" runat="server" CssClass="commonfont textdropwidth"></asp:TextBox>
                </td>--%>
                <%--<td align="right">
                    LST No.:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtLSTNo" MaxLength="15" runat="server" CssClass="commonfont textdropwidth"></asp:TextBox>
                </td>--%>
                <td align="right">
                    Region:<var class="starColor">*</var>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlRegion" runat="server" CssClass="commonfont textdropwidth">
                    </asp:DropDownList>
                </td>

                <td align="right">
                    GST No.:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtGSTNo" MaxLength="15" runat="server" CssClass="commonfont textdropwidth"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" class="auto-style1">
                    Cust. Group:<var class="starColor">*</var>
                </td>
                <td align="left">
              <asp:TextBox ID="txtCustGroup" MaxLength="10" runat="server" CssClass="commonfont textdropwidth"></asp:TextBox>
                
                <%--<asp:DropDownList ID="txtCustGroup1" runat="server" CssClass="commonfont textdropwidth">
                    </asp:DropDownList>--%>

                    <input type="button" value="..." id="btnCustGroup" runat="server" class="popButton"
                        onclick="popupCustGroup('ProbFamily')" />
                    
                </td>
               <td align="right">
                    Sales EmployeeName:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtSalesEmp" MaxLength="10" runat="server" CssClass="commonfont textdropwidth"></asp:TextBox>
                    <input type="button" value="..." id="btnSalesEmp" runat="server" class="popButton"
                        onclick="popupSalesEmployee('ProbFamily')" />

                </td>
                <td align="right">
                    Is Special 
                    .
                </td>
                <td align="left" colspan="3">
                    <asp:CheckBox ID="chkIsSpecialCust" onclick="if(!this.checked) document.getElementById('btnSpecial').style.display='none'; else document.getElementById('btnSpecial').style.display='block';" runat="server" CssClass="commonfont textdropwidth" />
                </td>
                </tr>
            <tr>
                <td align="right" class="auto-style1">
                    Cust. Type:<var class="starColor">*</var>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlCustType" runat="server" CssClass="commonfont textdropwidth">
                    </asp:DropDownList>
                </td>
                <td align="right">
                    Active
                </td>
                <td align="left" >
                    <asp:CheckBox ID="chkActive" Checked="true" runat="server" CssClass="commonfont textdropwidth" />
                </td>
               <td align="right">
                    MOU
                    .
                </td>
                <td align="left" colspan="3">
             <asp:CheckBox ID="chkIsSpecialMOU"
                        onclick="if(!this.checked) document.getElementById('trReg').style.display='none'; else document.getElementById('trReg').style.display='table-row';"
                 runat="server" CssClass="commonfont textdropwidth" />
                    </td>
            </tr>
             <tr>
                <td align="right" colspan="3">
                    Direct Customer:
                </td>
                <td align="left" colspan="3">
                    <asp:CheckBox ID="drtCust" Checked="false" runat="server" CssClass="commonfont textdropwidth" />
                </td>
                 </tr>

            <%-- added a row if mou checked --%>
              <tr id="trReg"  style="display:none;" >
                 <td align="right">
                    Demo Start Date:
                </td>
                <td align="left">
                       <asp:TextBox ID="txtDemoStartDate" MaxLength="10" runat="server" CssClass="commonfont textdropwidth" ></asp:TextBox>
                    <asp:ImageButton ID="imgInvoice" runat="server" ImageUrl="~/Common/Images/datePickerPopup.gif"
                        Style="vertical-align: bottom;" />
                      <ajaxToolkit:CalendarExtender ID="calInvoiceDate" runat="server" TargetControlID="txtDemoStartDate"
                        PopupButtonID="imgInvoice" CssClass="CalendarDatePicker" PopupPosition="BottomRight">
                      </ajaxToolkit:CalendarExtender>
                    </td>
                    <td align="right">
                         Commitment in Sqm P.M:
                    </td>
                    <td align="left" colspan="3">
               
                       <asp:TextBox ID="txtCmtSqm" MaxLength="10" runat="server" CssClass="commonfont textdropwidth"></asp:TextBox>
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
                        OnClick="btnAddNew_Click" />
                    <asp:Button ID="btnSpecial" runat="server" CssClass="btn addnewrow1" Text="Spl. Scheme"
                        OnClick="btnSpecial_Click" Style="display: none;" />
					<asp:Button ID="btnDemoDetails" runat="server" CssClass="btn addnewrow1" Text="Demo Details"
                      OnClick="btnDemo_Details" Style="display: none;" />
              
                     <asp:Button ID="InstBase" runat="server" CssClass="btn addnewrow" 
                           OnClick="btnInstall_Base" Text="Install Base" />
                
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