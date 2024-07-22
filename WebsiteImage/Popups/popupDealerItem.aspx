<%@ page language="C#" autoeventwireup="true" inherits="Popups_popupDealerItem, App_Web_l1ennj3y" %>

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
   
        function resizewindow() {
            window.moveTo(10, 10);
        }

        function closeWin() {
            window.close();
        }
        function FillParentWindow(strSelectType,strCount,strDimId,strItemId,strItemCode,strDesc1,strDesc2,strDlrPrice,strPerSqrMt,strMaxQty) {
         if (strSelectType == "PO") { 
                window.opener.document.getElementById("txtItemCode_" + strCount).value=strItemCode; 
                window.opener.document.getElementById("hdnItemId_" + strCount).value=strItemId; 
                window.opener.document.getElementById("txtUnitPrice_" + strCount).value=strDlrPrice; 
                //window.opener.document.getElementById("txtQty_" + strCount).value=strMaxQty; 
                window.opener.document.getElementById("txtDesc1_" + strCount).value=strDesc1;
                window.opener.document.getElementById("txtDesc2_" + strCount).value=strDesc2; 
                window.opener.document.getElementById("txtQty_" + strCount).select();
                window.opener.document.getElementById("txtQty_" + strCount).focus();
                var hdnMaxQty =window.opener.document.getElementById("hdnMaxQty_" + strCount);

                window.opener.document.getElementById("txtQty_" + strCount).value=0;
                if(hdnMaxQty!=null)
                hdnMaxQty.value=0;
                window.opener.document.getElementById("lblMessage").innerHTML="";
                window.opener.document.getElementById("lblMessage").className="";              
                
                if(window.opener.document.getElementById('hdnCurrentItemId')!="")
                window.opener.document.getElementById('hdnCurrentItemId').value=("txtItemCode_" + strCount);
                            
             }
          else if (strSelectType == "StockRetDlr") { 
                window.opener.document.getElementById("txtItemCode_" + strCount).value=strItemCode; 
                window.opener.document.getElementById("hdnItemId_" + strCount).value=strItemId;                 
                window.opener.document.getElementById("txtDesc1_" + strCount).value=strDesc1; 
                window.opener.document.getElementById("txtDesc2_" + strCount).value=strDesc2;           
                window.opener.document.getElementById("lblMessage").innerHTML="";
                window.opener.document.getElementById("lblMessage").className="";   
             }
             
            window.close();
        }
        function CheckOtherIsCheckedByGVID(spanChk) {

            var IsChecked = spanChk.checked;

            var CurrentRdbID = spanChk.id;

            var Chk = spanChk;

            Parent = document.getElementById('gvItem');

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
    <form id="frmpopupDealerItem" runat="server">
    <asp:HiddenField ID="hidSelectType" runat="server" Value="" />
    <asp:HiddenField ID="hdnCount" runat="server" Value="0" />
    <center>
        <table width="95%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="pagetitle" align="left">
                    Dealer Item
                </td>
            </tr>
        </table>
        <table class="HeaderDetailsGrid" width="95%" border="0" cellspacing="0">
            <tr>
                <td align="left">
                    Field Name:
                    <asp:DropDownList ID="ddlsearch" runat="server" CssClass="commonfont textdropwidth">
                        <asp:ListItem Value="ItemCode">Item Code</asp:ListItem>
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
                    <asp:GridView ID="gvItem" runat="server" AutoGenerateColumns="false" AllowPaging="True"
                        OnPageIndexChanging="gvItem_PageIndexChanging" Width="100%" GridLines="None"
                        CellPadding="2" PagerStyle-HorizontalAlign="Left" PageSize="20">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:RadioButton ID="rbtnSelect" runat="server" onclick="javascript:CheckOtherIsCheckedByGVID(this);" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ItemCode">
                                <ItemTemplate>
                                    <asp:Label ID="lblDIMId" runat="server" Text='<%#Eval("DIMId")%>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblItemId" runat="server" Text='<%#Eval("ItemId")%>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblItemCode" runat="server" Text='<%#Eval("ItemCode")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description1">
                                <ItemTemplate>
                                    <asp:Label ID="lblDesc1" runat="server" Text='<%#Eval("Description1")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description2">
                                <ItemTemplate>
                                    <asp:Label ID="lblDesc2" runat="server" Text='<%#Eval("Description2")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dealer Price">
                                <ItemTemplate>
                                    <asp:Label ID="lblDlrPrice" runat="server" Text='<%#Eval("DlrPrice")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PerSqrMt">
                                <ItemTemplate>
                                    <asp:Label ID="lblPerSqrMt" runat="server" Text='<%#Eval("PerSqrMt")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Max. Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lblMaxQty" runat="server" Text='<%#Eval("MaxQty")%>'></asp:Label>
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
