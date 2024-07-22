using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BusinessClass;
public partial class Admin_ChangePassword : System.Web.UI.Page
{
    UserMasterClass objUser = new UserMasterClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null) Response.Redirect("../Logout.aspx");
       
        
        if(!IsPostBack)
        {
            try
            {
                btnSave.Attributes.Add("onclick", "return fnValidateSave()");
                txtUserId.Text = Session["UserName"].ToString();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            objUser.prpUserName = txtUserId.Text;
            objUser.prpPassword=txtPwd.Text;
            objUser.prpNewpwd = EncryptDecryptClass.Encrypt(txtNewPwd.Text.Trim());
            string strResult = objUser.SaveChangePassword();
            lblMessage.Text = strResult;
            if (lblMessage.Text.StartsWith("I"))
                lblMessage.CssClass = "ErrorMessage";
            else
                lblMessage.CssClass = "SuccessMessage";
           
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/ChangePassword.aspx");
    }
}
