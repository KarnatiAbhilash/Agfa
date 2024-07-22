<%@ Page Language="C#" AutoEventWireup="true" CodeFile="popupIssueCustomer.aspx.cs" Inherits="Popups_popupCustomer" %>

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
        function FillParentWindow(strSelectType,strCount, strCustCode,strCustName,strCity,strDealerCode, strDealerType) {
             if (strSelectType == "DlrCustLink") {                
                window.opener.document.getElementById("txtCustCode_" + strCount).value = strCustCode;
                window.opener.document.getElementById("hdnCustCode_" + strCount).value = strCustCode;
                window.opener.document.getElementById("lblCustName_" + strCount).innerText = strCustName;
                window.opener.document.getElementById("lblCity_" + strCount).innerText = strCity;
                window.opener.document.getElementById("ddlDealer_" + strCount).value = strDealerCode;
                window.opener.document.getElementById("ddlDealerType_" + strCount).value = strDealerType;
            }
           else if (strSelectType == "PriceList") {                
                window.opener.document.getElementById("txtCustCode").value = strCustCode;
                window.opener.document.getElementById("txtCustName").value = strCustName;
                window.opener.__doPostBack('btnGetdata', ''); 
            } 
           else if (strSelectType == "Issue") {                
                window.opener.document.getElementById("txtCustCode").value = strCustCode;
                 window.opener.document.getElementById("txtCustName").value = strCustName; 
                 window.opener.document.getElementById("hndDlrDirect").value = strDealerType;
                 window.opener.__doPostBack('btnGetdata', '');
             } 
             else if (strSelectType == "IssueList") {
                
                 window.opener.document.getElementById("txtCustCode").value = strCustCode + "~" + strCustName;
                 window.opener.document.getElementById("hdnCustcode").value = strCustCode;
             } 
           else if (strSelectType == "StockRetCust") {                
                window.opener.document.getElementById("txtCustCode").value = strCustCode;
                window.opener.document.getElementById("hdnCustCode").value = strCustCode;   
                window.opener.__doPostBack('btnGetdata', '');               
            }       
           else if (strSelectType == "DebitCreditNoteRpt") {                
                window.opener.document.getElementById("txtCustCode").value = strCustCode;            
            }  
            window.close();
        }
        function CheckOtherIsCheckedByGVID(spanChk) {

            var IsChecked = spanChk.checked;

            var CurrentRdbID = spanChk.id;

            var Chk = spanChk;

            Parent = document.getElementById('gvCust');

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
    <form id="frmpopupCustomer" runat="server">
    <asp:HiddenField ID="hidSelectType" runat="server" Value="" />
    <asp:HiddenField ID="hdnCount" runat="server" Value="0" />
    <center>
        <table width="95%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="pagetitle" align="left">
                    Customer Master
                </td>
            </tr>
        </table>
        <table class="HeaderDetailsGrid" width="95%" border="0" cellspacing="0">
            <tr>
                <td align="left">
                    Field Name:
                    <asp:DropDownList ID="ddlsearch" runat="server" CssClass="commonfont textdropwidth">
                        <asp:ListItem Value="CustCode">Customer Code</asp:ListItem>
                        <asp:ListItem Value="CustName">Customer Name</asp:ListItem>  
                        <asp:ListItem Value="City">City</asp:ListItem> 
                        <asp:ListItem Value="DealerCode">Dealer Code</asp:ListItem>                       
                    </asp:DropDownList>
                    Value:
                    <asp:TextBox ID="txtValue" CssClass="commonfont textdropwidth" runat="server"></asp:TextBox>
                    <asp:Button CssClass="buttonsearch" OnClientClick="return Validate()" ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click">
                    </asp:Button>
                    <asp:Button CssClass="buttonclearsearch"  ID="btnClear"
                        runat="server" Text="Clear Search" OnClick="btnClear_Click"></asp:Button>
                </td>
                <td>
                    <input type="button" id="btnClose" value="Close" class="btn close" onclick="closeWin()" />
                </td>
            </tr>
        </table>
        <table id="Table1" width="95%" runat="server" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <asp:GridView ID="gvCust" runat="server" AutoGenerateColumns="false" AllowPaging="True"
                        OnPageIndexChanging="gvCust_PageIndexChanging" Width="100%" GridLines="None"
                        CellPadding="2" PagerStyle-HorizontalAlign="Left" PageSize="20">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:RadioButton ID="rbtnSelect" runat="server" onclick="javascript:CheckOtherIsCheckedByGVID(this);" />
                                </ItemTemplate>
                            </asp:TemplateField>                            
                            <asp:TemplateField HeaderText="Customer Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblCustCode" runat="server" Text='<%#Eval("CustCode")%>' ></asp:Label>                                    
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblCustName" runat="server" Text='<%#Eval("CustName")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="City">
                                <ItemTemplate>
                                    <asp:Label ID="lblCity" runat="server" Text='<%#Eval("City")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dealer Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblDealerCode" runat="server" Text='<%#Eval("DealerCode")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>                             
							<asp:TemplateField HeaderText="IsSpecial">
                                <ItemTemplate>
                                    <asp:Label ID="lblDirectCust" runat="server" Text='<%#Eval("SpecialCustomer")%>'></asp:Label>
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