<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StockReturnApproval.aspx.cs" Inherits="Transactions_StockReturnApproval" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/HomeTopFrame.ascx" TagName="HomeTopFrame" TagPrefix="huc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Secondary Sales System</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
</head>
<body>
    <form id="frmStockReturnApproval" runat="server">
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
                    Stock Return Approval
                </td>
            </tr>
        </table>
        <br /> 
        <asp:GridView ID="gvStockRet" runat="server" BorderWidth="1" BorderColor="#666666"
            GridLines="None" CellPadding="5" Width="95%" AllowPaging="true" EmptyDataText="No Record(s) Found."
            AutoGenerateColumns="false" AllowSorting="true" OnSorting="gvStockRet_Sorting" PagerStyle-HorizontalAlign="Left" 
            OnPageIndexChanging="gvStockRet_PageIndexChanged">
          <Columns>
           <asp:TemplateField>
            <ItemTemplate>
             <asp:CheckBox ID="chkStockRet" runat="server" />
             <asp:HiddenField ID="hdnReturnId" runat="server" value='<%#Eval("ReturnId")%>'/> 
             <asp:HiddenField ID="hdnItemId" runat="server" value='<%#Eval("ItemId")%>'/>             
            </ItemTemplate>
           </asp:TemplateField>
           <asp:BoundField ItemStyle-HorizontalAlign="Left" SortExpression="DealerName" DataField="DealerName" HeaderText="Dealer Name" />
           <asp:BoundField ItemStyle-HorizontalAlign="Left" SortExpression="InvoiceNo" DataField="InvoiceNo" HeaderText="InvoiceNo" />
           <asp:BoundField ItemStyle-HorizontalAlign="Left" SortExpression="SalesType" DataField="SalesType" HeaderText="Sales Type" />
           <asp:BoundField ItemStyle-HorizontalAlign="Left" SortExpression="ReturnDate" DataField="ReturnDate" HeaderText="Return Date" DataFormatString="{0:dd/MM/yyyy}" />
           <asp:BoundField ItemStyle-HorizontalAlign="Left" SortExpression="ItemCode" DataField="ItemCode" HeaderText="Item Code" />
           <asp:BoundField ItemStyle-HorizontalAlign="Center" SortExpression="ReturnQty" DataField="ReturnQty" HeaderText="Return Qty" />
           <asp:BoundField ItemStyle-HorizontalAlign="Left"  SortExpression="Reason" DataField="Reason" HeaderText="Reason" ItemStyle-Width="30%"/>
          </Columns>
          <AlternatingRowStyle CssClass="datatablethAlternate" />
          <HeaderStyle HorizontalAlign="Left" CssClass="datatablethNew" ForeColor="White" />
          <RowStyle CssClass="alternateGrid" />
          <PagerSettings Position="Bottom" Mode="Numeric" />
        </asp:GridView>
        <table id="tblBtn" runat="server" width="95%" class="datatableth" border="0" cellspacing="0" cellpadding="0">
         <tr>
           <td>
             <asp:Button ID="btnCancel" runat="server" CssClass="btn cancelbtn" Text="Reject" onclick="btnCancel_Click" />
             <asp:Button ID="btnSave" runat="server" CssClass="btn savebtn" Text="Approve" onclick="btnSave_Click" />
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
      </ContentTemplate>
     </asp:UpdatePanel> 
    </form>
</body>
</html>
