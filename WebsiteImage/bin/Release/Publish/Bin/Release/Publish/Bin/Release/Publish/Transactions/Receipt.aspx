<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Receipt.aspx.cs" Inherits="Transactions_Receipt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/HomeTopFrame.ascx" TagName="HomeTopFrame" TagPrefix="huc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Secondary Sales System</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <script language="javascript"  type="text/javascript">
    function DelReceipt(RowCount)  
    {
    if(confirm("Do You Want To Delete"))     
    {
    $get('hdnDummy').value=RowCount;
    __doPostBack('btnDummy','');   
    }    
    }
    </script>
</head>
<body>
    <form id="frmReceipt" runat="server">
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
                            Receipt
                        </td>
                    </tr>
                </table>
                <br />
                <table class="HeaderDetailsGrid" width="95%" border="0" cellspacing="0">
                    <tr>
                        <td align="right">
                            Month:&nbsp;&nbsp;
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtMonth" runat="server" CssClass="commonfont textdropwidth" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td align="right">
                            Year:&nbsp;&nbsp;
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtYear" runat="server" CssClass="commonfont textdropwidth" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button CssClass="btn addnew" ToolTip="Add New" ID="btnAdd" OnClick="btnAdd_New"
                                Text="Add New" runat="server"></asp:Button>
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="gvReceipt" runat="server" BorderWidth="1" BorderColor="#666666"
                    GridLines="None" CellPadding="5" Width="95%" AllowPaging="true" EmptyDataText="No Record(s) Found."
                    AutoGenerateColumns="false" AllowSorting="true" OnRowDeleting="gvReceipt_RowDeleting"
                    OnSorting="gvReceipt_Sorting" PagerStyle-HorizontalAlign="Left" OnRowCommand="gvReceipt_RowCommand"
                    OnPageIndexChanging="gvReceipt_PageIndexChanged" 
                    onrowdatabound="gvReceipt_RowDataBound">
                    <Columns>
                        <asp:ButtonField ItemStyle-Width="50px" ButtonType="Image" CommandName="Delete" ImageUrl="../Common/Images/delete.gif" />
                        <asp:BoundField DataField="ReceiptNo" HeaderStyle-CssClass="HiddenField" ItemStyle-CssClass="HiddenField"
                            HeaderText="ReceiptNo" />
                        <asp:HyperLinkField ItemStyle-HorizontalAlign="Left" SortExpression="PONo" DataTextField="PONo"
                            DataNavigateUrlFields="PONo,ReceiptNo,POType" DataNavigateUrlFormatString="ReceiptAddEdit.aspx?PONo={0}&&ReceiptNo={1}&&strType={2}"
                            HeaderStyle-ForeColor="White" HeaderText="PO. No" />
                        <asp:BoundField ItemStyle-HorizontalAlign="Left" SortExpression="PODate" DataField="PODate"
                            HeaderText="PO. Date" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField ItemStyle-HorizontalAlign="Left" SortExpression="POType" DataField="POType"
                            HeaderText="PO. Type" />
                        <asp:BoundField ItemStyle-HorizontalAlign="Left" SortExpression="POStatus" DataField="POStatus"
                            HeaderText="PO. Status" />
                        <asp:BoundField DataField="pono" HeaderStyle-CssClass="HiddenField" ItemStyle-CssClass="HiddenField" ItemStyle-HorizontalAlign="Left" SortExpression="POno"
                            HeaderText="PONO" />
                    </Columns>
                    <AlternatingRowStyle CssClass="datatablethAlternate" />
                    <HeaderStyle HorizontalAlign="Left" CssClass="datatablethNew" ForeColor="White" />
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
