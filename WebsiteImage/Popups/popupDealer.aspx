<%@ page language="C#" autoeventwireup="true" inherits="Popups_popupDealer, App_Web_l1ennj3y" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>Secondary Sales System</title>
    <link rel="stylesheet" type="text/css" href="../Common/CSS/AjaxStyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../Common/Css/StyleSheet.css" />

    <script type="text/javascript" language="javascript" src="../Common/Scripts/GetScreen.js"></script>

    <script type="text/javascript" language="javascript" src="../Common/Scripts/Validation.js"></script>

    <script type="text/javascript" language="javascript">
        function Validate() {
            var lblMsg = document.getElementById("lblMessage");
            var tempValue = document.getElementById("txtValue").value;
            if (fnComboSelect(document.getElementById("ddlsearch").value, "Field Name", lblMsg) != true) {
                alert(lblMsg.value);
                document.getElementById("ddlsearch").focus();
                return false;
            }
            if (fnNoBlank(tempValue, "Value", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("txtValue").focus();
                return false;
            }
        }
     
    </script>

    <script type="text/javascript" language="javascript">
        function resizewindow() {
            window.moveTo(10, 10);
        }

        function closeWin() {
            window.close();
        }
        function FillParentWindow(strSelectType, strDealercode,strDealerName,strDMSCode) {
            if (strSelectType == "Cust") {                
                window.opener.document.getElementById("txtDealer").value = strDealercode;
                //window.opener.document.getElementById("txtDMSCode").value = strDMSCode;        
            }
           else if (strSelectType == "DlrCustLink") {                
                window.opener.document.getElementById("txtDealer").value = strDealercode;
                window.opener.document.getElementById("txtDealerName").value = strDealerName;
                window.opener.__doPostBack('btnGetdata', '');
            }
           else if (strSelectType == "PriceList") {                
                window.opener.document.getElementById("txtDealer").value = strDealercode;
                window.opener.document.getElementById("txtDealerName").value = strDealerName;
                window.opener.document.getElementById("txtCustCode").value = '';
                window.opener.document.getElementById("txtCustName").value = '';
                window.opener.__doPostBack('btnGetdata', '');
            }
           else if (strSelectType == "BulkPriceCust") {                
                window.opener.document.getElementById("hdnDlrCode").value = strDealercode;  
                window.opener.document.getElementById("txtDealer").value = strDealercode              
                window.opener.__doPostBack('btnGetdata', '');
            }
           else if (strSelectType == "UserMaster") {                
                window.opener.document.getElementById("txtDealer").value = strDealercode;
                window.opener.document.getElementById("txtDlrName").value = strDealerName;                
            }
           else if (strSelectType == "DebitCreditNoteRpt") {                
                window.opener.document.getElementById("txtDealerCode").value = strDealercode;               
            }
            else if (strSelectType == "DealerReceiptRpt") {                
                window.opener.document.getElementById("txtDealerName").value = strDealerName;               
            }
            else if (strSelectType == "InventoryRpt") {                
                window.opener.document.getElementById("txtDealerName").value = strDealerName;               
                window.opener.document.getElementById("txtDealerCode").value = strDealercode;               
            }
          else if (strSelectType == "DlrMonthEnd") {                
                window.opener.document.getElementById("txtDealerCode").value = strDealercode; 
                window.opener.__doPostBack('txtDealer','');              
            }
            else if (strSelectType == "ItemMaster") {

                window.opener.document.getElementById("txtDealerCode").value = strDealercode;
            }
        
            
            
            window.close();
        }
        function CheckOtherIsCheckedByGVID(spanChk) {

            var IsChecked = spanChk.checked;

            var CurrentRdbID = spanChk.id;

            var Chk = spanChk;

            Parent = document.getElementById('gvDealer');

            var items = Parent.getElementsByTagName('input');


            for (i = 0; i < items.length; i++) {

                if (items[i].id != CurrentRdbID && items[i].type == "radio") {

                    if (items[i].checked) {

                        items[i].checked = false;

                    }

                }

            }

        }
        
       
    </script>

</head>
<body onload="resizewindow()">
    <form id="frmpopupDealer" runat="server">
    <asp:HiddenField ID="hidSelectType" runat="server" Value="" />
    <center>
        <table width="95%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="pagetitle" align="left">
                    Dealer Master
                </td>
            </tr>
        </table>
        <table class="HeaderDetailsGrid" width="95%" border="0" cellspacing="0">
            <tr>
                <td align="left">
                    Field Name:
                    <asp:DropDownList ID="ddlsearch" runat="server" CssClass="commonfont textdropwidth">
                        <asp:ListItem Value="DealerCode">Dealer Code</asp:ListItem>
                        <asp:ListItem Value="DealerName">Dealer Name</asp:ListItem>
                        <asp:ListItem Value="City">City</asp:ListItem>
                        <asp:ListItem Value="Region">Region</asp:ListItem>
                    </asp:DropDownList>
                    Value:
                    <asp:TextBox ID="txtValue" CssClass="commonfont textdropwidth" runat="server"></asp:TextBox>
                    <asp:Button CssClass="buttonsearch" OnClientClick="return Validate()" ID="btnSearch"
                        runat="server" Text="Search" OnClick="btnSearch_Click"></asp:Button>
                    <asp:Button CssClass="buttonclearsearch" ID="btnClear" runat="server" Text="Clear Search"
                        OnClick="btnClear_Click"></asp:Button>
                </td>
                <td>
                    <input type="button" id="btnClose" value="Close" class="btn close" onclick="closeWin()" />
                </td>
            </tr>
        </table>
        <table id="Table1" width="95%" runat="server" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <asp:GridView ID="gvDealer" runat="server" AutoGenerateColumns="false" AllowPaging="True"
                        OnPageIndexChanging="gvDealer_PageIndexChanging" Width="100%" GridLines="None"
                        CellPadding="2" PagerStyle-HorizontalAlign="Left" PageSize="20">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:RadioButton ID="rbtnSelect" runat="server" onclick="javascript:CheckOtherIsCheckedByGVID(this);" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dealer Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblDealerCode" runat="server" Text='<%#Eval("DealerCode")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dealer Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblDealerName" runat="server" Text='<%#Eval("DealerName")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="City">
                                <ItemTemplate>
                                    <asp:Label ID="lblCity" runat="server" Text='<%#Eval("City")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Region">
                                <ItemTemplate>
                                    <asp:Label ID="lblRegion" runat="server" Text='<%#Eval("Region")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DMS Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblDMSCode" runat="server" Text='<%#Eval("DMSCode")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                        </Columns>
                        <AlternatingRowStyle CssClass="datatablethAlternate" />
                        <HeaderStyle HorizontalAlign="Left" CssClass="datatablethNew" ForeColor="White" />
                        <RowStyle CssClass="alternateGrid" />
                        <PagerSettings Position="Bottom" Mode="Numeric" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <table width="95%" class="datatableth" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <asp:Button ID="btnSave" runat="server" CssClass="btn selectbtn" Text="Select" OnClick="btnSave_Click" />
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
