<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadData.aspx.cs" Inherits="Admin_UploadData" %>
<%@ Register Src="~/HomeTopFrame.ascx" TagName="HomeTopFrame" TagPrefix="huc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Secondary Sales System</title>
    <script type="text/javascript" language="javascript">
        function validate() {
            var lblMsg = document.getElementById("lblMessage");
            if (fnComboSelect(document.getElementById("ddlUploadType").value, "Upload Type", lblMsg) != true) {
                alert(lblMsg.value);
                document.getElementById("ddlUploadType").focus();
                return false;
            }   
            return true;
        }
        
        function ValidateFileUpload(Source, args)
        {        
            if(validate())
            {
                var fuData = document.getElementById('<%= fuUploadFile.ClientID %>'); 
                var FileUploadPath = fuData.value; 
           
              if(FileUploadPath==''|| FileUploadPath==null) 
              {
                alert('Please Select Upload File.'); 
                return false;
              }
              else
              {
                var Extension = FileUploadPath.substring(FileUploadPath.lastIndexOf('.') + 1).toLowerCase();
             
                if (Extension == "xls")
                {
                  args.IsValid = true; // Valid file type    
                  return true;          
                }
                else
                {
                  // Not valid file type
                  alert('Please select valid .xls file'); 
                  return false;
                }
              }
           }
           return false;
        }
    </script> 
</head>
<body>
    <form id="frmUploadData" runat="server">
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
                    Upload Data
                </td>
            </tr>
        </table>
        <br />
       <table width="95%" class="alternatecolorborder" id="TblParam" runat="server" border="0"
            cellspacing="0" cellpadding="5">
         <tr>
          <td align="right">
            Upload Type:<var class="starColor">*</var>
          </td>
          <td align="left">
            <asp:DropDownList ID="ddlUploadType" runat="server" CssClass="commonfont textdropwidth"></asp:DropDownList>   
          </td>
         </tr>
         <tr>
          <td align="right">
            Upload File:<var class="starColor">*</var>
          </td>
          <td align="left">
            <asp:FileUpload ID="fuUploadFile" runat="server" />
          </td>
         </tr>
        </table>
        <table width="95%" class="datatableth" border="0" cellspacing="0" cellpadding="0">
          <tr> 
           <td>              
             <asp:Button ID="btnDownload" runat="server" Text="Download Template" 
            CssClass="btn Attachment" OnClick="btnDownload_Click" />
             <asp:Button ID="btnUpload" runat="server" Text="Upload Data" 
            CssClass="btn Attachment" OnClientClick=" return ValidateFileUpload();" 
            onclick="btnUpload_Click" />
                   
           </td>
          </tr>
        </table>
       <table width="95%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td align="left">
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Label ID="lblFailed" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </center>
    </form>
</body>
</html>
