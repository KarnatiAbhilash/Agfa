<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EquipmentItemMasterAddEdit.aspx.cs" Inherits="Masters_EquipmentItemMasterAddEdit" %>

<%@ Register Src="~/HomeTopFrame.ascx" TagName="HomeTopFrame" TagPrefix="huc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Secondary Sales System</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />

    <script type="text/javascript" language="javascript">        



        function popupItemMaster(strSelectType) {
            window.open("../Popups/popupItemMaster.aspx?SelectType=" + strSelectType, "ItemMaster", "location=0,status=1,scrollbars=1,toolbar=0,width=900,height=400,resizable=no");
        }
        function popupProductGroup(strSelectType1) {

            window.open("../Popups/popupProductGroup.aspx?SelectType=" + strSelectType1, "productMaster", "location=0,status=1,scrollbars=1,toolbar=0,width=900,height=400,resizable=no");
        }

        //this is for popup of productgroup
        function popupEquipmentProductGroup(strSelectType) {

            window.open("../Popups/popupEquipmentProductGroup.aspx?SelectType=" + strSelectType, "productMaster", "location=0,status=1,scrollbars=1,toolbar=0,width=900,height=400,resizable=no");
        }


        function fnValidateSave() {
            var lblMsg = document.getElementById("lblMessage");

            //checks product group must enter
            if (fnNoBlank(document.getElementById("txtProdGroup").value, "Product Group", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtProdGroup").focus();
                return false;
            }
           
            //productgroup length
            if (fnCheckLength(document.getElementById("txtProdGroup").value, "Product Group", "10", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtProdGroup").focus();
                return false;
            }


            //this is item code validation must enter itemcode
            if (fnNoBlank(document.getElementById("txtItemCode").value, "Equipment Item Code", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtItemCode").focus();
                return false;
            }
            //checks item code length upto 10
            if (fnCheckLength(document.getElementById("txtItemCode").value, "Item Code", "10", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtItemCode").focus();
                return false;
            }

            //ckecks spce validation of product group
            if (fnNoSpaceSpecial(document.getElementById("txtItemCode").value, "Item Code", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtItemCode").focus();
                return false;
            }
            //checks description upto 50
            if (fnCheckLength(document.getElementById("txtDescrip").value, "Description", "50", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtDescrip").focus();
                return false;
            }
        }
    </script>

    <style type="text/css">
        .auto-style1 {
            width: 312px;
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
                      Equipment Product Group<var class="starColor">*</var>
                    </td>
                    <td align="left" class="auto-style1">
                         <asp:TextBox type="text" id="txtProdGroup" AutoPostBack="true"  runat="server" 
                            class="commonfont textdropwidth" ontextchanged="txtProdGroup_TextChanged" ></asp:TextBox>
                      <input type="button" value="..." id="btnProbGrp1" runat="server" class="popButton" onclick="popupEquipmentProductGroup('ProbFamily')" />
                    </td>
                    <td align="right">
                          Equipment Item Code<var class="starColor">*</var>
                        </td>
                        <td align="left"  class="auto-style1">
                            <asp:TextBox type="text" ID="txtItemCode" AutoPostBack="true" CssClass="commonfont textdropwidth"
                                runat="server" ></asp:TextBox>
                        </td>
                  </tr>
                  <tr>
                    <td align="right">
                        Description 1:
                    </td>
                    <td align="left" class="auto-style1">
                        <asp:TextBox ID="txtDescr1" TextMode="MultiLine" runat="server" Rows="3" Columns="4"
                            Width="250px" CssClass="commonfont textdropwidth"></asp:TextBox>
                    </td>
                    <td align="right">
                        Description 2:
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtDescr2" TextMode="MultiLine" runat="server" Rows="3" Columns="4"
                            Width="250px" CssClass="commonfont textdropwidth"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        Active
                    </td>

                    <td align="left" colspan="2" >
                        <asp:CheckBox  ID="chkActive" Checked="true" runat="server" CssClass="commonfont textdropwidth" />
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