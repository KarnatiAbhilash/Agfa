<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PriceListChange.aspx.cs"
    Inherits="Pricing_PriceListChange" %>

<%@ Register Src="~/HomeTopFrame.ascx" TagName="HomeTopFrame" TagPrefix="huc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Secondary Sales System</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <script type="text/javascript" language="javascript">
        function popupDealer(strSelectType) {
            window.open("../Popups/popupDealer.aspx?SelectType=" + strSelectType, "DCM", "location=0,status=1,scrollbars=1,toolbar=0,width=900,height=400,resizable=no");
        }

        function popCustomer(strSelectType) {
            var dlrCode = document.getElementById("txtDealer").value;
            window.open("../Popups/popupCustomer.aspx?SelectType=" + strSelectType + "&strCount=0&dlr=" + dlrCode, "DCM", "location=0,status=1,scrollbars=1,toolbar=0,width=900,height=400,resizable=no");
        }

        function popupBudget(strSelectType) {

            window.open("../Popups/popupBudgetClass.aspx?SelectType=" + strSelectType, "PCP", "location=0,status=1,scrollbars=1,toolbar=0,width=900,height=400,resizable=no");
        }

        function popupProductFamily(strSelectType) {
            var BudgetCls = document.getElementById("txtBudgetCode").value;

            window.open("../Popups/popupProductFamily.aspx?SelectType=" + strSelectType + "&BudgetCls=" + BudgetCls, "PMG", "location=0,status=1,scrollbars=1,toolbar=0,width=900,height=400,resizable=no");
        }

        function popupProductGroup(strSelectType) {
            var BudgetCls = document.getElementById("txtBudgetCode").value;
            var ProdFamily = document.getElementById("txtProdFamily").value;
            window.open("../Popups/popupProductGroup.aspx?SelectType=" + strSelectType + "&BudgetCls=" + BudgetCls + "&ProdFamily=" + ProdFamily, "IMG", "location=0,status=1,scrollbars=1,toolbar=0,width=900,height=400,resizable=no");
        }

        function CalcDlrPerSqrMt(rowCount) {
            var lblConFact = document.getElementById("lblConvFactor_" + rowCount);
            var decConvFactor = 0;
            var decDlrPrice = 0;
            var decPerSqrMt = 0;

            decConvFactor = lblConFact.innerText;
            decDlrPrice = document.getElementById("txtDlrPrice_" + rowCount).value;

            if (decConvFactor != null && decDlrPrice != null && decDlrPrice != '' && decConvFactor != '' && isNaN(decConvFactor) == false && isNaN(decDlrPrice) == false) {
                decPerSqrMt = decDlrPrice / decConvFactor;
                document.getElementById("txtDlrPerSqrMt_" + rowCount).value = decPerSqrMt;
            }
        }
        function CalcDlrPrice(rowCount) {
            var lblConFact = document.getElementById("lblConvFactor_" + rowCount);
            var decConvFactor = 0;
            var decDlrPrice = 0;
            var decPerSqrMt = 0;

            decConvFactor = lblConFact.innerText;
            decPerSqrMt = document.getElementById("txtDlrPerSqrMt_" + rowCount).value;

            if (decConvFactor != null && decPerSqrMt != null && decPerSqrMt != '' && decConvFactor != '' && isNaN(decConvFactor) == false && isNaN(decPerSqrMt) == false) {
                decDlrPrice = decPerSqrMt * decConvFactor;
                document.getElementById("txtDlrPrice_" + rowCount).value = decDlrPrice;
            }
        }

        function CalcEUPerSqrMt(rowCount) {
            var lblConFact = document.getElementById("lblConvFactor_" + rowCount);
            var decConvFactor = 0;
            var decEUPrice = 0;
            var decPerSqrMt = 0;

            decConvFactor = lblConFact.innerText;
            decEUPrice = document.getElementById("txtEUPrice_" + rowCount).value;

            if (decConvFactor != null && decEUPrice != null && decEUPrice != '' && decConvFactor != '' && isNaN(decConvFactor) == false && isNaN(decEUPrice) == false) {
                decPerSqrMt = decEUPrice / decConvFactor;
                document.getElementById("txtEUPerSqMt_" + rowCount).value = decPerSqrMt;
            }
        }

        function CalcEUPrice(rowCount) {
            var lblConFact = document.getElementById("lblConvFactor_" + rowCount);
            var decConvFactor = 0;
            var decEUPrice = 0;
            var decPerSqrMt = 0;

            decConvFactor = lblConFact.innerText;
            decPerSqrMt = document.getElementById("txtEUPerSqMt_" + rowCount).value;

            if (decConvFactor != null && decPerSqrMt != null && decPerSqrMt != '' && decConvFactor != '' && isNaN(decConvFactor) == false && isNaN(decPerSqrMt) == false) {
                decEUPrice = decPerSqrMt * decConvFactor;
                document.getElementById("txtEUPrice_" + rowCount).value = decEUPrice;
            }
        }

        function CalcProfitActual(rowCount) {
            var decEUPrice = 0;
            var decDlrPrice = 0
            var decProfitActual = 0;

            decDlrPrice = document.getElementById("txtDlrPrice_" + rowCount).value;
            decEUPrice = document.getElementById("txtEUPrice_" + rowCount).value

            if (decDlrPrice != null && decEUPrice != null && decEUPrice != '' && decDlrPrice != '' && isNaN(decDlrPrice) == false && isNaN(decEUPrice) == false) {
                decProfitActual = ((decEUPrice - decDlrPrice) / decEUPrice) * 100;
                document.getElementById("txtProfitActual_" + rowCount).value = decProfitActual;
            }
        }

        function fnValidateSave() {

            if (fnNoBlank(document.getElementById("txtDealer").value, "Dealer Code", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtDealer").focus();
                return false;
            }

            var hdnCount = document.getElementById("hidSearchCount").value;
            var i;
            var lblMsg = document.getElementById("lblMessage");
            var strPriceType = document.getElementById("ddlPriceType").value;
            var returnFlag;
            returnFlag = false;

            for (i = 1; i <= hdnCount; i++) {
                if (strPriceType != "EU Price") {
                    if (fnNoBlank(document.getElementById("txtDlrPrice_" + i).value, "DLR Price", lblMsg) != true && fnNoBlank(document.getElementById("txtDlrPerSqrMt_" + i).value, "DLR PerSqrMt", lblMsg) != true) {
                        alert("Please Enter Either DLR Price or DLR PerSqrMt. Both Should not Be Blank.");
                        document.getElementById("txtDlrPrice_" + i).focus();
                        return false;
                    }
                    if (fnDecimal(document.getElementById("txtDlrPrice_" + i).value, "DLR Price", lblMsg) != true) {
                        alert(lblMsg.value)
                        document.getElementById("txtDlrPrice_" + i).focus();
                        return false;
                    }
                    if (fnDecimal(document.getElementById("txtDlrPerSqrMt_" + i).value, "DLR PerSqrMt", lblMsg) != true) {
                        alert(lblMsg.value)
                        document.getElementById("txtDlrPerSqrMt_" + i).focus();
                        return false;
                    }
                    if (document.getElementById("txtDlrPerSqrMt_" + i).value <= 0) {
                        alert("DlrPerSqrMt Must be greater than 0")
                        document.getElementById("txtDlrPerSqrMt_" + i).focus();
                        return false;
                    }
                    if (document.getElementById("txtDlrPrice_" + i).value <= 0) {
                        alert("txtDlrPrice Must be greater than 0")
                        document.getElementById("txtDlrPrice_" + i).focus();
                        return false;
                    }
                    var remarks = document.getElementById("txtRemarks_" + i).value;
                    //alert(remarks.length);
                    if (remarks.length > 100) {
                        alert("Remarks length must be Lesser than 100")
                        document.getElementById("txtRemarks_" + i).focus();
                        return false;
                    }
                }
                if (strPriceType != "Dealer Price") {
                    if (fnNoBlank(document.getElementById("txtEUPrice_" + i).value, "EU Price", lblMsg) != true && fnNoBlank(document.getElementById("txtEUPerSqMt_" + i).value, "EU PerSqrMt", lblMsg) != true) {
                        alert("Please Enter Either EU Price or EU PerSqrMt. Both Should not Be Blank.");
                        document.getElementById("txtEUPrice_" + i).focus();
                        return false;
                    }
                    if (fnDecimal(document.getElementById("txtEUPrice_" + i).value, "EU Price", lblMsg) != true) {
                        alert(lblMsg.value)
                        document.getElementById("txtEUPrice_" + i).focus();
                        return false;
                    }
                    if (fnDecimal(document.getElementById("txtEUPerSqMt_" + i).value, "EU PerSqrMt", lblMsg) != true) {
                        alert(lblMsg.value)
                        document.getElementById("txtEUPerSqMt_" + i).focus();
                        return false;
                    }
                    if (fnNoBlank(document.getElementById("txtProfitActual_" + i).value, "Profit Actual", lblMsg) != true) {
                        alert(lblMsg.value)
                        document.getElementById("txtProfitActual_" + i).focus();
                        return false;
                    }
                    if (fnDecimal(document.getElementById("txtProfitActual_" + i).value, "Profit Actual", lblMsg) != true) {
                        alert(lblMsg.value)
                        document.getElementById("txtProfitActual_" + i).focus();
                        return false;
                    }
                    if (fnNoBlank(document.getElementById("txtProfitAgreed_" + i).value, "Profit Agreed", lblMsg) != true) {
                        alert(lblMsg.value)
                        document.getElementById("txtProfitAgreed_" + i).focus();
                        return false;
                    }
                    if (fnDecimal(document.getElementById("txtProfitAgreed_" + i).value, "Profit Agreed", lblMsg) != true) {
                        alert(lblMsg.value)
                        document.getElementById("txtProfitAgreed_" + i).focus();
                        return false;
                    }

                    if (document.getElementById("txtEUPrice_" + i).value <= 0) {
                        alert("EUPrice Must be greater than 0")
                        document.getElementById("txtEUPrice_" + i).focus();
                        return false;
                    }
                    if (document.getElementById("txtEUPerSqMt_" + i).value <= 0) {
                        alert("EUPerSqMt Must be greater than 0")
                        document.getElementById("txtEUPerSqMt_" + i).focus();
                        return false;
                    }
                    var remarks = document.getElementById("txtRemarks_" + i).value;
                    //alert(remarks.length);
                    if (remarks.length > 100) {
                        alert("Remarks length must be Lesser than 100")
                        document.getElementById("txtRemarks_" + i).focus();
                        return false;
                    }
                }


                returnFlag = true;
            }

            if (returnFlag == false)
                return false;
            else
                return true;
        }
      
    </script>
</head>
<body>
    <form id="frmPriceListChange" runat="server">
    <asp:HiddenField ID="hidSearchCount" runat="server" Value="0" />
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
                    Price List Change
                </td>
            </tr>
        </table>
        <br />
        <table width="95%" class="alternatecolorborder" id="TblParam" runat="server" border="0"
            cellspacing="0" cellpadding="5">
            <tr>
                <td align="right">
                    Dealer Code:<var class="starColor">*</var>
                </td>
                <td align="left">
                    <asp:TextBox type="text" ID="txtDealer" runat="server" class="commonfont textdropwidth"
                        AutoPostBack="true" OnTextChanged="txtDealer_TextChanged"></asp:TextBox>
                    <input type="button" value="..." id="btnDealer" runat="server" class="popButton"
                        onclick="popupDealer('PriceList')" />
                </td>
                <td align="right">
                    Dealer Name:
                </td>
                <td align="left">
                    <input type="text" id="txtDealerName" readonly="true" maxlength="50" runat="server"
                        cssclass="commonfont textdropwidth" />
                    <asp:Button ID="btnGetdata" runat="server" OnClick="btnGetdata_Click" Style="display: none;" />
                </td>
                <td align="right">
                    Effective Date:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtEffectiveDate" ReadOnly="true" runat="server" CssClass="commonfont textdropwidth"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Customer Code:
                </td>
                <td align="left">
                    <asp:TextBox type="text" ID="txtCustCode" AutoPostBack="true" runat="server" class="commonfont textdropwidth"
                        OnTextChanged="txtCustCode_TextChanged"></asp:TextBox>
                    <input type="button" value="..." id="btnCust" runat="server" class="popButton" onclick="popCustomer('PriceList')" />
                </td>
                <td align="right">
                    Customer Name:
                </td>
                <td align="left">
                    <input type="text" id="txtCustName" runat="server" readonly="true" cssclass="commonfont textdropwidth" />
                </td>
                <td align="right">
                    Budget Class:
                </td>
                <td align="left">
                    <asp:TextBox type="text" ID="txtBudgetCode" AutoPostBack="true" runat="server" class="commonfont textdropwidth"
                        OnTextChanged="txtBudgetCode_TextChanged"></asp:TextBox>
                    <input type="button" value="..." id="btnBudget" runat="server" class="popButton"
                        onclick="popupBudget('PriceList')" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    Product Family:
                </td>
                <td align="left">
                    <asp:TextBox type="text" ID="txtProdFamily" AutoPostBack="true" runat="server" class="commonfont textdropwidth"
                        OnTextChanged="txtProdFamily_TextChanged"></asp:TextBox>
                    <input type="button" value="..." id="btnProd" runat="server" class="popButton" onclick="popupProductFamily('PriceList')" />
                </td>
                <td align="right">
                    Product Group:
                </td>
                <td align="left">
                    <asp:TextBox type="text" ID="txtProdGroup" runat="server" class="commonfont textdropwidth"
                        OnTextChanged="txtProdGroup_TextChanged" AutoPostBack="True"></asp:TextBox>
                    <input type="button" value="..." id="btnProbGrp" runat="server" class="popButton"
                        onclick="popupProductGroup('PriceList')" />
                </td>
                <td align="right">
                    Price Type:
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlPriceType" runat="server" CssClass="commonfont textdropwidth"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlPriceType_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <br />
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
                    <asp:Button ID="btnCancel" runat="server" CssClass="btn cancelbtn" Text="Clear" OnClick="btnCancel_Click" />
                    <asp:Button ID="btnSave" runat="server" CssClass="btn savebtn" Text="Save" OnClientClick="return fnValidateSave()"
                        OnClick="btnSave_Click" Enabled="false" />
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
