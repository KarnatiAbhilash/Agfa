﻿<%@ page language="C#" autoeventwireup="true" inherits="Popups_popupSalesEmployee, App_Web_l1ennj3y" %>

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
        
        function FillParentWindow(strSelectType, strSalesEmpName) {
            if (strSelectType == "ProbFamily") {                
                window.opener.document.getElementById("txtSalesEmp").value = strSalesEmpName;
            }
            else if (strSelectType == "ProbGroup") {                
                window.opener.document.getElementById("txtBudgetClassCode").value = strSalesEmpName;
                window.opener.document.getElementById("txtProdFamily").value="";
            }
            else if (strSelectType == "ItemMaster") {                
                window.opener.document.getElementById("txtBudgetClassCode").value = strSalesEmpName;
                window.opener.document.getElementById("txtProdFamily").value="";
                window.opener.document.getElementById("txtSalesEmp").value="";
            }
           else if (strSelectType == "PriceList") {                
                window.opener.document.getElementById("txtBudgetCode").value = strSalesEmpName;               
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
    <style type="text/css">
        .auto-style1 {
            margin-top: 8;
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
                    Sales Employee Master:
                </td>
            </tr>
        </table>
        <table class="HeaderDetailsGrid" width="95%" border="0" cellspacing="0">
            <tr>
                <td align="left">
                    Field Name:
                    <asp:DropDownList ID="ddlsearch" runat="server" CssClass="commonfont textdropwidth">
           
                          <asp:ListItem Value="Region">Region</asp:ListItem>
                        <asp:ListItem Value="EmailId">EmailId</asp:ListItem>    
                        <asp:ListItem Value="ContactNo">ContactNo</asp:ListItem>    
                         

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


                    <asp:GridView ID="gvBudget" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                        OnPageIndexChanging="gvBudget_PageIndexChanging" Width="100%" GridLines="None"
                        CellPadding="2" PagerStyle-HorizontalAlign="Left" PageSize="20" CssClass="auto-style1">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:RadioButton ID="rbtnSelect" runat="server" onclick="javascript:CheckOtherIsCheckedByGVID(this);" />
                                </ItemTemplate>
                            <ItemStyle Width="5%"></ItemStyle>
                            </asp:TemplateField>     
                            
                            <asp:TemplateField HeaderText="Sales Employee Master">
                                <ItemTemplate>
                                    <asp:Label ID="lblId" runat="server" Text='<%#Eval("Id")%>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                                </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>     
                             
                 
  
                            <asp:TemplateField HeaderText="Region">
                                <ItemTemplate>
                                     <asp:Label ID="lblRegion" runat="server" Text='<%#Eval("Region")%>'></asp:Label>
                                </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>   


                            <asp:TemplateField HeaderText="Email Id">
                                <ItemTemplate>
                           <asp:Label ID="lblEmailId" runat="server" Text='<%#Eval("EmailId")%>'></asp:Label>  
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>        

                              <asp:TemplateField HeaderText="Contact No">
                                <ItemTemplate>
                           <asp:Label ID="lblContactNo" runat="server" Text='<%#Eval("ContactNo")%>'></asp:Label>  
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
