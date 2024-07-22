<%@ page language="C#" autoeventwireup="true" inherits="Masters_ProductGroupMasterAddEdit, App_Web_ojonuiur" %>
<%@ Register Src="~/HomeTopFrame.ascx" TagName="HomeTopFrame" TagPrefix="huc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Secondary Sales System</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />

    <script type="text/javascript" language="javascript">        
        function popupBudget(strSelectType) {           
            window.open("../Popups/popupBudgetClass.aspx?SelectType=" + strSelectType , "PMG", "location=0,status=1,scrollbars=1,toolbar=0,width=900,height=400,resizable=no");
        }
        function popupProductFamily(strSelectType) {
            var BudgetCls=document.getElementById("txtBudgetClassCode").value;
           
            window.open("../Popups/popupProductFamily.aspx?SelectType=" + strSelectType +"&BudgetCls="+BudgetCls , "PMG", "location=0,status=1,scrollbars=1,toolbar=0,width=900,height=400,resizable=no");
        }
        
        function fnValidateSave() {
        var lblMsg = document.getElementById("lblMessage");
        if (fnNoBlank(document.getElementById("txtBudgetClassCode").value, "Budget Class", lblMsg) != true) {
                alert("Please Select Budget Class.");
                document.getElementById("txtBudgetClassCode").focus();
                return false;
            }
         if (fnNoBlank(document.getElementById("txtProdFamily").value, "Product Family", lblMsg) != true) {
                alert("Please Select Product Family.");
                document.getElementById("txtProdFamily").focus();
                return false;
            }
         if (fnNoBlank(document.getElementById("txtProdGroup").value, "Product Group", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtProdGroup").focus();
                return false;
            }
         if (fnNoSpaceSpecial(document.getElementById("txtProdGroup").value, "Product Group", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtProdGroup").focus();
                return false;
            }
         if (fnCheckLength(document.getElementById("txtProdGroup").value, "Product Group", "10", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtProdGroup").focus();
                return false;
            }
         if (fnCheckLength(document.getElementById("txtDescrip").value, "Product Group Description", "50", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtDescrip").focus();
                return false;
            }
        }
    </script>

</head>
<body>
    <form id="frmProductGroupMasterAddEdit" runat="server">
    <asp:HiddenField ID="hdnPGId" Value="0" runat="server" />
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
                    Budget Class:<var class="starColor">*</var>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtBudgetClassCode" runat="server" 
                        CssClass="commonfont textdropwidth" AutoPostBack="True" 
                        ontextchanged="txtBudgetClassCode_TextChanged"></asp:TextBox>
                     <input type="button" value="..." id="btnBudget" runat="server" class="popButton"
                        onclick="popupBudget('ProbGroup')" />
                </td>
                <td align="right">
                    Product Family<var class="starColor">*</var>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtProdFamily" runat="server" AutoPostBack="true" 
                        CssClass="commonfont textdropwidth" ontextchanged="txtProdFamily_TextChanged"></asp:TextBox>                     
                    <input type="button" value="..." id="btnProd" runat="server" class="popButton" onclick="popupProductFamily('ProbGroup')" />
                </td>                
                </tr>
                <tr>
                    <td align="right">
                       Product Group<var class="starColor">*</var>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtProdGroup" MaxLength="10" runat="server" CssClass="commonfont textdropwidth"></asp:TextBox>
                    </td>
                    <td align="right">
                        Description:
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtDescrip" TextMode="MultiLine" runat="server" Rows="3" Columns="4"
                            Width="250px" CssClass="commonfont textdropwidth"></asp:TextBox>
                    </td>
                </tr>
                <tr>
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
