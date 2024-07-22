<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DealerCustLink.aspx.cs" Inherits="Masters_DealerCustLink" %>
<%@ Register Src="~/HomeTopFrame.ascx" TagName="HomeTopFrame" TagPrefix="huc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Secondary Sales System</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <script type="text/javascript" language="javascript">        
        function popupDealer(strSelectType) {           
            window.open("../Popups/popupDealer.aspx?SelectType=" + strSelectType , "DCM", "location=0,status=1,scrollbars=1,toolbar=0,width=900,height=400,resizable=no");
        }
        function popCustomer(strSelectType,strCount) {           
            window.open("../Popups/popupCustomer.aspx?SelectType=" + strSelectType +"&strCount="+strCount , "DCM", "location=0,status=1,scrollbars=1,toolbar=0,width=900,height=400,resizable=no");
        }
        
        function fnValidateSave() {
          var hdnCount=document.getElementById("hidSearchCount").value;
          var i;
          var lblMsg = document.getElementById("lblMessage");
          var returnFlag;
          returnFlag=false;
          for(i=1;i<=hdnCount;i++)
          {
            if(document.getElementById("hdnRow_"+i).value=="S")
             continue;
            if (fnNoChar(document.getElementById("txtCustCode_"+i).value, "Customer Code", lblMsg) != true) {
                    alert(lblMsg.value)
                    document.getElementById("txtCustCode_"+i).focus();
                    return false;
                 }
            if (fnComboSelect(document.getElementById("ddlDealer_"+i).value, "Dealer Name", lblMsg) != true) {
                alert(lblMsg.value)
                document.getElementById("ddlDealer_"+i).focus();
                return false;
               }
               returnFlag=true;
          }
          if (returnFlag==false)
             return false;
          else
             return true;
        }
    </script>

</head>
<body>
    <form id="frmDealerCustLink" runat="server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" />   
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
                    Dealer Customer Link
                </td>
            </tr>
        </table>
        <br />
        <asp:UpdatePanel ID="upReceipt" runat="server">
        <ContentTemplate>
        <asp:HiddenField ID="hidSearchCount" runat="server" Value="0" />
        <table width="95%" class="alternatecolorborder" id="TblParam" runat="server" border="0"
            cellspacing="0" cellpadding="5">
            <tr>
                <td align="right">
                    Dealer Code:<var class="starColor">*</var>
                </td>
                <td align="left">
                    <asp:TextBox type="text" AutoPostBack="true" id="txtDealer" runat="server" 
                        class="commonfont textdropwidth" ontextchanged="txtDealer_TextChanged" ></asp:TextBox>
                    <input type="button" value="..." id="btnDealer" runat="server" class="popButton"
                        onclick="popupDealer('DlrCustLink')" />
                </td>
                <td align="right">
                    Dealer Name:
                </td>
                <td align="left">
                    <input type="text" ID="txtDealerName"  disabled="true" runat="server" CssClass="commonfont textdropwidth" />
                    <asp:Button ID="btnGetdata" runat="server" onclick="btnGetdata_Click"  style="display:none;"/>
                </td>
            </tr>
        </table>
        <br />
        <%--<table id="Table1" width="95%" runat="server" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <asp:GridView ID="gvDlrCustLink" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                        Width="100%" GridLines="None" CellPadding="2" EmptyDataText="No Record(s) Found." BorderWidth="1" BorderColor="#666666">
                        <Columns>
                            <asp:TemplateField>
                             <ItemTemplate>
                              <asp:CheckBox ID="chkDlrCustLink" runat="server" />
                             </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblCustCode" runat="server" Text='<%#Eval("CustCode")%>'></asp:Label>
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
                        </Columns>
                        <AlternatingRowStyle CssClass="datatablethAlternate" />
                        <HeaderStyle HorizontalAlign="Left" CssClass="datatablethNew" ForeColor="White" />
                        <RowStyle CssClass="alternateGrid" />
                    </asp:GridView>
                </td>
                <td>
                 <asp:Button ID="btnAdd" runat="server" Text="<--" onclick="btnAdd_Click" />
                 <br /><br />
                 <asp:Button ID="btnRemove" runat="server" Text="-->" onclick="btnRemove_Click" />
                </td>
                <td>
                    <asp:GridView ID="gvDlrCust" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                        Width="100%" GridLines="None" CellPadding="2" EmptyDataText="No Record(s) Found." BorderWidth="1" BorderColor="#666666">
                        <Columns>
                            <asp:TemplateField>
                             <ItemTemplate>
                              <asp:CheckBox ID="chkDlrCust" runat="server" />
                             </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblCustCode" runat="server" Text='<%#Eval("CustCode")%>'></asp:Label>
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
                            <asp:TemplateField HeaderText="Dealer">
                                <ItemTemplate>
                                    <asp:Label ID="lblDealer" runat="server" Text='<%#Eval("DealerCode")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                        </Columns>
                        <AlternatingRowStyle CssClass="datatablethAlternate" />
                        <HeaderStyle HorizontalAlign="Left" CssClass="datatablethNew" ForeColor="White" />
                        <RowStyle CssClass="alternateGrid" />
                    </asp:GridView>
                </td>
            </tr>
        </table>--%>
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
                        OnClick="btnSave_Click"/>
                    <asp:Button CssClass="btn addcustomers" ToolTip="Add New Record" ID="btnAdd" 
                                Text="Add Customer" runat="server" onclick="btnAdd_Click"></asp:Button>
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
        </ContentTemplate>
      </asp:UpdatePanel>
    </center>
    </form>
</body>
</html>
