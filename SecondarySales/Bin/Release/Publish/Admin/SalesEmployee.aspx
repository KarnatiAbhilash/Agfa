<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalesEmployee.aspx.cs" Inherits="Admin_SalesEmployee" %>



<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/HomeTopFrame.ascx" TagName="HomeTopFrame" TagPrefix="huc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Secondary Sales System</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    
    <script type="text/javascript" language="javascript">
        function Del(RowCount) {
            if (confirm("Do You Want To Delete")) {
                $get('hdnDummy').value = RowCount;
                __doPostBack('btnDummy', '');
            }

        }
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
    <style type="text/css">
        .auto-style1 {
            margin-left: 0px;
        }
    </style>
</head>
<body>
    <form id="frmBudgetClassMaster" runat="server">

        <asp:Button ID="btnDummy" runat="server" Text="Button" onclick="btnDummy_Click" 
        style="display:none" />

      <%--    <asp:Button ID="Button1" runat="server" Text="Button" onclick="btnDummy_Click1" 
        style="display:none" />--%>

    <asp:HiddenField ID="hdnDummy" runat="server" />
    <ajaxToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" />    
     <asp:UpdatePanel ID="CalPanel" runat="server">
        <ContentTemplate>
            <center>
                <table width="95%" border="0" cellspacing="0" cellpadding="0" style="vertical-align: top;">
                 <tr>
                  <td>                    
                    <huc1:HomeTopFrame runat="server" ID="TopFrame" />
                 </td>
                </tr>
               </table>
               <br />
                <table width="95%" border="0" cellspacing="" cellpadding="0">
                    <tr>
                        <td align="left" class="pagetitleDetail">
                             Sales Employee Master
                        </td>
                    </tr>
                </table>
                <br />                
                <table class="HeaderDetailsGrid" width="95%" border="0" cellspacing="0">
                    <tr>
                        <td align="left">
                            Field Name:
                            <asp:DropDownList ID="ddlsearch" runat="server" CssClass="commonfont textdropwidth">
                            </asp:DropDownList>
                            Value:
                            <asp:TextBox ID="txtValue" CssClass="commonfont textdropwidth" runat="server"></asp:TextBox>
                            <asp:Button CssClass="buttonsearch" OnClientClick="return Validate()" ID="btnSearch"
                                runat="server" Text="Search" OnClick="btnSearch_Click"></asp:Button>
                            <asp:Button CssClass="buttonclearsearch" ID="btnClear" runat="server" Text="Clear Search"
                                OnClick="btnClearSearch_Click"></asp:Button>
                        </td>
                        <td>
                            <asp:Button CssClass="btn addnew" ToolTip="Add New" ID="btnAdd" OnClick="btnAdd_New"
                                Text="Add New" runat="server"></asp:Button>
                        </td>
                    </tr>
                </table>


                <asp:GridView ID="gvBudget" runat="server" BorderWidth="1px" BorderColor="#666666"
                    GridLines="None" CellPadding="5" Width="95%" AllowPaging="True" EmptyDataText="No Record(s) Found."
                    AutoGenerateColumns="False" AllowSorting="True" OnRowDeleting="gvBudget_RowDeleting"
                    OnSorting="gvBudget_Sorting" PagerStyle-HorizontalAlign="Left" OnRowCommand="gvBudget_RowCommand"
                    OnPageIndexChanging="gvBudget_PageIndexChanged" 
                    onrowdatabound="gvBudget_RowDataBound" CssClass="auto-style1">
                    <Columns>
  
             <asp:ButtonField ItemStyle-Width="50px" ButtonType="Image" CommandName="Delete" ImageUrl="../Common/Images/delete.gif" />
                        <asp:BoundField DataField="Id" HeaderStyle-CssClass="HiddenField" ItemStyle-CssClass="HiddenField"
                            HeaderText="Id" />


                        <asp:BoundField DataField="Id" HeaderStyle-CssClass="HiddenField" ItemStyle-CssClass="HiddenField"
                            HeaderText="Id" >
                        <HeaderStyle CssClass="HiddenField" />
                        <ItemStyle CssClass="HiddenField" />
                        </asp:BoundField>
                        <asp:HyperLinkField ItemStyle-HorizontalAlign="Left" SortExpression="Name" DataTextField="Name"
                            DataNavigateUrlFields="Id" DataNavigateUrlFormatString="SalesEmployeeAddEdit.aspx?New=N&&strCode={0}"
                            HeaderStyle-ForeColor="White" HeaderText="Name" >
                        <HeaderStyle ForeColor="White" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:HyperLinkField>
                        <asp:BoundField ItemStyle-HorizontalAlign="Left" SortExpression="Region" DataField="Region"
                            HeaderText="Region" >
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                         <asp:BoundField ItemStyle-HorizontalAlign="Left" SortExpression="EmailId" DataField="EmailId"
                            HeaderText="EmailId" >
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                         <asp:BoundField ItemStyle-HorizontalAlign="Left" SortExpression="ContactNo" DataField="ContactNo"
                            HeaderText="ContactNo" >
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                    </Columns>
                    <AlternatingRowStyle CssClass="datatablethAlternate" />
                    <HeaderStyle HorizontalAlign="Left" CssClass="datatablethNew" ForeColor="White" />
                    <PagerStyle HorizontalAlign="Left" />
                    <RowStyle CssClass="alternateGrid" />
                    <PagerSettings Position="Bottom" Mode="Numeric" />
                </asp:GridView>
                <table width="95%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </center>
        </ContentTemplate>
    </asp:UpdatePanel>    
    </form>
</body>
</html>
