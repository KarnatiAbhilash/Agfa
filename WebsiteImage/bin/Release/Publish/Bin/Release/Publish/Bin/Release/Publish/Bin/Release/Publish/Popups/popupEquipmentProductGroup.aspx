<%@ Page Language="C#" AutoEventWireup="true" CodeFile="popupEquipmentProductGroup.aspx.cs" Inherits="Popups_popupEquipmentProductGroup" %>

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
        
        function FillParentWindow(strSelectType, strBCCode) {
            if (strSelectType == "ProbFamily") {                
                window.opener.document.getElementById("txtProdGroup").value = strBCCode;
            }
            else if (strSelectType == "ProbGroup") {                
                window.opener.document.getElementById("txtBudgetClassCode").value = strBCCode;
                window.opener.document.getElementById("txtProdFamily").value="";
            }
            else if (strSelectType == "ItemMaster") {                
                window.opener.document.getElementById("txtBudgetClassCode").value = strBCCode;
                window.opener.document.getElementById("txtProdFamily").value="";
                window.opener.document.getElementById("txtProdGroup").value="";
            }
           else if (strSelectType == "PriceList") {                
                window.opener.document.getElementById("txtBudgetCode").value = strBCCode;               
                window.opener.__doPostBack('btnGetdata', '');                
            } 
            window.close();
        }
        
        
        function CheckOtherIsCheckedByGVID(spanChk) {

            var IsChecked = spanChk.checked;

            var CurrentRdbID = spanChk.id;

            var Chk = spanChk;

            Parent = document.getElementById('gvBudget');

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
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $("[id*=gvBudget] td").hover(function () {
                $("td", $(this).closest("tr")).addClass("hover_row");
            }, function () {
                $("td", $(this).closest("tr")).removeClass("hover_row");
            });
        });
    </script>


<script type="text/javascript">

</script>
    <style type="text/css">
        .auto-style1 {
            margin-top: 8;
        }
       body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        td
        {
            cursor: pointer;
        }
        .hover_row
        {
            background-color: #aab2b5;
        }


    </style>
</head>
<body onload="resizewindow()">
    <form id="frmpopupBudgetClass" runat="server">
    <asp:HiddenField ID="hidSelectType" runat="server" Value="" />
    <center>
        <table width="95%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="pagetitle" align="left">
                    Equipment Product Group
                </td>
            </tr>
        </table>
        <table class="HeaderDetailsGrid" width="95%" border="0" cellspacing="0">
            <tr>
                <td align="left">
                    Field Name:
                    <asp:DropDownList ID="ddlsearch" runat="server" CssClass="commonfont textdropwidth">
                        <asp:ListItem Value="GroupCode">Product Group</asp:ListItem>
                        <asp:ListItem Value="GroupDesc">Description</asp:ListItem>                        
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
                    <asp:GridView ID="gvBudget" runat="server" AutoGenerateColumns="False" AllowPaging="True"  ClientIDMode="Static" RowStyle-CssClass="GvGrid" 
                        OnPageIndexChanging="gvBudget_PageIndexChanging" Width="100%" GridLines="None"
                        CellPadding="2" PagerStyle-HorizontalAlign="Left" PageSize="20" CssClass="auto-style1"
                        OnRowDataBound="gvBudget_RowDataBound" OnSelectedIndexChanged="gvBudget_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:RadioButton ID="rbtnSelect" runat="server" onclick="javascript:CheckOtherIsCheckedByGVID(this);" />
                                </ItemTemplate>
							<ItemStyle Width="5%"></ItemStyle>
                            </asp:TemplateField>                            
                            <asp:TemplateField HeaderText="Equipment ProductGroup">
                                <ItemTemplate>
                                    <asp:Label ID="lblPGId" runat="server" Text='<%#Eval("PGId")%>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblGroupCode" runat="server" Text='<%#Eval("GroupCode")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Equipment Description">
                                <ItemTemplate>
                                    <asp:Label ID="lblGroupDesc" runat="server" Text='<%#Eval("GroupDesc")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>                            
                        </Columns>
                        <AlternatingRowStyle CssClass="datatablethAlternate" />
                        <HeaderStyle HorizontalAlign="Left" CssClass="datatablethNew" ForeColor="White" />

<PagerStyle HorizontalAlign="Left"></PagerStyle>

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

