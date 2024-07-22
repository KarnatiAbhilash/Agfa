/* ==============================================================================================
    Created By      : Subahani SM
    Created Date    : 19 July 2010
    Purpose         :     
=================================================================================================
Change History :
=================================================================================================
S.No    Modified By        Modified Date        Description
1       
2
3 
================================================================================================= */

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
using System.Data.SqlClient;
using BusinessClass;

public partial class LoginPage : System.Web.UI.Page
{
    LoginClass objLogin = new LoginClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        txtUserId.Focus();        
    }
    protected void LoginButton_Click(object sender, EventArgs e)
    {
        string plainText = txtPassword.Text.Trim();     // original plaintext

        string cipherText = EncryptDecryptClass.Encrypt(plainText);
        string str= EncryptDecryptClass.Decrypt("Drt3zy31bq5GjfMbA411Zw==");

        objLogin.prpUserId = txtUserId.Text.Trim();
        objLogin.prpPassword = cipherText;
        DataSet dsUser = objLogin.AuthenticateUser();

        if (dsUser.Tables[0].Rows.Count > 0)
        {
            Session["UserId"] = txtUserId.Text.Trim();
            Session["UserName"] = dsUser.Tables[0].Rows[0]["UserName"].ToString();
            Session["UserType"] = dsUser.Tables[0].Rows[0]["UserType"].ToString();
            Session["DealerCode"] = dsUser.Tables[0].Rows[0]["DealerCode"].ToString();
            Session["DealerName"] = dsUser.Tables[0].Rows[0]["DealerName"].ToString();
            Session["EmailID"] = dsUser.Tables[0].Rows[0]["Email"].ToString();
            Session["PageSize"] = ConfigurationManager.AppSettings["PageSize"].ToString();
            Session["DateFormat"] = ConfigurationManager.AppSettings["DateFormat"].ToString();

            UserMenuMapping UMM = new UserMenuMapping();
            UMM.PopulateMenus(Session["UserId"].ToString());
            Session["dsMenu"] = UMM.prpDS;

            // FormsAuthentication.RedirectFromLoginPage(txtUserId.Text, false);
            Response.Redirect("~/HomePage/HomePage.aspx");
        }
        else
            lblErrorMessage.Text = "Invalid Credentials. Check User Id or Password.";
    }

   
    protected void loginReset_Click(object sender, EventArgs e)
    {
        txtUserId.Text = "";
        txtPassword.Text = "";
        lblErrorMessage.Text = "";
    }

   
}
