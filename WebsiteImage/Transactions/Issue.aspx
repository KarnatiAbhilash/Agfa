<%@ page language="C#" autoeventwireup="true" inherits="Transactions_Issue, App_Web_3kgqgt3u" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/HomeTopFrame.ascx" TagName="HomeTopFrame" TagPrefix="huc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Secondary Sales System</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <script type="text/javascript" language="javascript">
    function DelIssue(RowCount)
    {
    if(confirm("Do You Want To Delete"))     
    {
    $get('hdnDummy').value=RowCount;
    __doPostBack('btnDummy','');   
    }
    
    }
    
	function Download(ctrl) {
            var path = document.getElementById("hdnViewPath").value;
            path += document.getElementById(ctrl.id).text;
            window.open(path, "_blank");
          
        }
        function popCustomer(strSelectType) {
            window.open("../Popups/popupCustomer.aspx?SelectType="+strSelectType+"&strCount=0","DCM","location=0,status=1,scrollbars=1,toolbar=0,width=900,height=400,resizable=no");
        }
    </script>
</head>
<body>
    <form id="frmIssue" runat="server">
    <asp:Button ID="btnDummy" runat="server" Text="Button" onclick="btnDummy_Click" 
        style="display:none" />
    <asp:HiddenField ID="hdnDummy" runat="server" />
         <asp:HiddenField ID="hdnViewPath" runat="server" />
                <asp:HiddenField ID="hdnDlrCode" runat="server" />
    <ajaxToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" />
    <asp:UpdatePanel ID="CalPanel" runat="server" UpdateMode="Conditional">
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
                            Issue
                        </td>
                    </tr>
                </table>
                <br />
                <table class="HeaderDetailsGrid" width="95%" border="0" cellspacing="0">
                    <tr>
                        <td align="right">
                            Month:&nbsp;&nbsp;
                        </td>
                        <td align="left" >
                            <asp:TextBox ID="txtMonth" runat="server" CssClass="commonfont textdropwidth" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td align="right">
                            Year:&nbsp;&nbsp;
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtYear" runat="server" CssClass="commonfont textdropwidth" ReadOnly="true"></asp:TextBox>
                        </td>
                        
						<td align="right" >
                            Region:
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlRegion" style="width:72px;" runat="server" CssClass="commonfont textdropwidthSmall"
                                  >
                            </asp:DropDownList>
                        </td>
                      <td align="right">
                            CustCode:
                        </td>
                        <td align="left"  >
                       <asp:TextBox AutoPostBack="true" type="text" id="txtCustCode" runat="server"  Enabled="false"
                            class="commonfont textdropwidthSmall"  ></asp:TextBox>
                       <input type="button" value="..." id="btnCust" runat="server" readonly="readonly" class="popButton" 
                         onclick="popCustomer('IssueList')"/>
                         <asp:HiddenField ID="hdnCustcode" runat="server"/>  
                    </td>
                    <td align="left">
                            <asp:Button CssClass="buttonsearch" ID="btnSearch"
                                runat="server" Text="Search" OnClick="search_click" ></asp:Button>
                            <asp:Button CssClass="buttonclearsearch" ID="btnClear" runat="server" Text="Clear Search" OnClick="btnClear_Click"
                              ></asp:Button>
                        </td>
                        <td>
                                <asp:Button ID="btnAdd" runat="server" CssClass="btn addnew" OnClick="btnAdd_New" Text="Add New" ToolTip="Add New" />
                         </td>
                    </tr>
                </table>
                <asp:Button ID="btnUpload" runat="server" Text="Upload" Visible="false" />
                <asp:GridView ID="gvIssue" runat="server" BorderWidth="1px" BorderColor="#666666" GridLines="None"
                    CellPadding="5" Width="95%" AllowPaging="True" EmptyDataText="No Record(s) Found."
                    AutoGenerateColumns="False" AllowSorting="True" OnRowDeleting="gvIssue_RowDeleting"
                    OnSorting="gvIssue_Sorting" PagerStyle-HorizontalAlign="Left"
                    OnPageIndexChanging="gvIssue_PageIndexChanged" 
                    onrowdatabound="gvIssue_RowDataBound1"  >
                    <Columns>
                        <asp:ButtonField ItemStyle-Width="50px" ButtonType="Image" CommandName="Delete" ImageUrl="../Common/Images/delete.gif" >                       
                         <ItemStyle Width="50px" />
                        </asp:ButtonField>
                        <asp:BoundField DataField="IssueNo" HeaderStyle-CssClass="HiddenField" ItemStyle-CssClass="HiddenField" HeaderText="IssueNo" >
                        <HeaderStyle CssClass="HiddenField" />
                        <ItemStyle CssClass="HiddenField" />
                        </asp:BoundField>
                        <asp:HyperLinkField  ItemStyle-HorizontalAlign="Left" SortExpression="InvoiceNo" DataTextField="InvoiceNo"
                            DataNavigateUrlFields="InvoiceNo,IssueNo,SalesType" DataNavigateUrlFormatString="IssueAddEdit.aspx?InvoiceNo={0}&&IssueNo={1}&&strType={2}"
                            HeaderStyle-ForeColor="White" HeaderText="Invoice No" >
                        <HeaderStyle ForeColor="White" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:HyperLinkField>
                        <asp:BoundField ItemStyle-HorizontalAlign="Left" SortExpression="invoiceDate" DataField="invoiceDate"
                            HeaderText="Invoice Date" DataFormatString="{0:dd/MM/yyyy}" >
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField ItemStyle-HorizontalAlign="Left" SortExpression="CustName" DataField="CustName"
                            HeaderText="Customer" >
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField  ItemStyle-HorizontalAlign="Left" SortExpression="GrossInvoiceAmt" DataField="GrossInvoiceAmt"
                            HeaderText="Gross Invoice Amount" >
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>    
                       <asp:BoundField ItemStyle-HorizontalAlign="Left" SortExpression="Stattus" DataField="Stattus"
                            HeaderText="Status" >
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Download Here">  
                           <ItemTemplate>  
                               <asp:LinkButton ID="lnkDownload" runat="server"  CommandArgument='<%# Eval("FileName") %>' CommandName="Download" Text='<%# Eval("FileName") %>' OnClientClick="Download(this);" />  
                           </ItemTemplate>  
                         </asp:TemplateField>  


                            <asp:TemplateField HeaderText="" ItemStyle-Width="100">
                                <ItemTemplate>
                                <%--    <asp:Button ID="btnDelete1" runat="server" Text="Verify" Visible='<%# Eval("Status1").ToString() == "A" ? true : false %>' />--%>
                                  <asp:Button ID="btnverify" runat="server" CssClass="btn savebtn" Text="Verify" visible="false" OnClick="btnApproved_Click"  />     
                                 <asp:HiddenField ID="hdnIssueNo" runat="server" value='<%#Eval("IssueNo")%>'/>        
                                   

                                
                                </ItemTemplate>
                            </asp:TemplateField>

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
