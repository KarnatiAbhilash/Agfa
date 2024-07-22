<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DealerMaster.aspx.cs" Inherits="Masters_DealerMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/HomeTopFrame.ascx" TagName="HomeTopFrame" TagPrefix="huc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Secondary Sales System</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    
    <script type="text/javascript" language="javascript">
       function Del(RowCount)
    {
    if(confirm("Do You Want To Delete"))     
    {
    $get('hdnDummy').value=RowCount;
    __doPostBack('btnDummy','');   
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
</head>
<body>
    <form id="frmDealerMaster" runat="server">
     <asp:Button ID="btnDummy" runat="server" Text="Button" onclick="btnDummy_Click" 
        style="display:none" />
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
                            Dealer Master
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
                <asp:GridView ID="gvDealer" runat="server" BorderWidth="1" BorderColor="#666666"
                    GridLines="None" CellPadding="5" Width="95%" AllowPaging="true" EmptyDataText="No Record(s) Found."
                    AutoGenerateColumns="false" AllowSorting="true" OnRowDeleting="gvDealer_RowDeleting"
                    OnSorting="gvDealer_Sorting" PagerStyle-HorizontalAlign="Left" OnRowCommand="gvDealer_RowCommand"
                    OnPageIndexChanging="gvDealer_PageIndexChanged" 
                    onrowdatabound="gvDealer_RowDataBound">
                    <Columns>
                        <asp:ButtonField ItemStyle-Width="50px" ButtonType="Image" CommandName="Delete" ImageUrl="../Common/Images/delete.gif"/>
                        <asp:BoundField DataField="DealerCode" HeaderStyle-CssClass="HiddenField" ItemStyle-CssClass="HiddenField"
                            HeaderText="ID" />
                        <asp:HyperLinkField ItemStyle-HorizontalAlign="Left" SortExpression="DealerCode" DataTextField="DealerCode"
                            DataNavigateUrlFields="DealerCode" DataNavigateUrlFormatString="DealerMasterAddEdit.aspx?New=N&&strCode={0}"
                            HeaderStyle-ForeColor="White" HeaderText="Dealer Code" />
                        <asp:BoundField ItemStyle-HorizontalAlign="Left" SortExpression="DealerName" DataField="DealerName"
                            HeaderText="Dealer Name" />
                        <asp:BoundField ItemStyle-HorizontalAlign="Left" SortExpression="City" DataField="City"
                            HeaderText="City" />
                        <asp:BoundField ItemStyle-HorizontalAlign="Left" SortExpression="Region" DataField="Region"
                            HeaderText="Region" />
                        <asp:BoundField ItemStyle-HorizontalAlign="Left" SortExpression="Status" DataField="Status"
                            HeaderText="Status" />
                    </Columns>
                    <AlternatingRowStyle CssClass="datatablethAlternate" />
                    <HeaderStyle HorizontalAlign="Left" CssClass="datatablethNew" ForeColor="White" />
                    <RowStyle CssClass="alternateGrid" />
                    <PagerSettings Position="Bottom" Mode="Numeric" />
                </asp:GridView>
                <table width="95%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblMessage" CssClass="SuccessMessage" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </center>
        </ContentTemplate>
    </asp:UpdatePanel>    
    </form>
</body>
</html>
