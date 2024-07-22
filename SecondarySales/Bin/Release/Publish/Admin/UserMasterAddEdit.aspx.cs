/* ==============================================================================================
    Created By      : Subahani SM
    Created Date    : 04 Aug 2010
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
using BusinessClass;

public partial class Admin_UserMasterAddEdit : System.Web.UI.Page
{
    UserMasterClass objUser = new UserMasterClass();
    DealerMasterClass objDealer = new DealerMasterClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null) Response.Redirect("../Logout.aspx");
        try
        {
            if (!IsPostBack)
            {
                btnSave.Attributes.Add("onclick", "return fnValidateSave()");
                CommonFunction.PopulateRecordsWithTwoParam("Common_Values", "Text", "Value", "FieldName", "UserType", "Status", "1", "Id", ddlUserType);
                if (Request.QueryString["New"] != null)
                {
                    if (Request.QueryString["New"].ToString() == "S")
                    {
                        lblHeader.Text = "Add - User Master";
                        trPwd.Visible = true;
                        trPwd.Style.Add("display", "block");
                        btnChangePwd.Style.Add("display", "none");
                    }
                    else
                    {
                        lblHeader.Text = "Edit - User Master";
                        trPwd.Visible = false;
                        trPwd.Style.Add("display", "none");
                        btnChangePwd.Style.Add("display", "table-row");
                    }


                }
                if (Request.QueryString["strCode"] != null && Request.QueryString["strCode"].ToString() != "")
                {
                    hdnUserId.Value = Request.QueryString["strCode"].ToString();
                    hdnRowId.Value = Request.QueryString["strRowId"].ToString();
                    BindData();
                    txtUserId.ReadOnly = true;
                    if (ddlUserType.SelectedValue == "Dealer")
                        trDlr.Style.Add("display", "table-row");
                    else
                        trDlr.Style.Add("display", "none");
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
            lblMessage.CssClass = "ErrorMessage";
        }
    }
    protected void BindData()
    {
        objUser.prpUserId = hdnUserId.Value;
        DataSet ds = objUser.FetchUserMasterOnId();
        if (ds.Tables[0].Rows.Count > 0)
        {           
            txtUserName.Text = ds.Tables[0].Rows[0]["UserName"].ToString().Trim();
            txtUserId.Text = ds.Tables[0].Rows[0]["UserId"].ToString().Trim();
            if (ds.Tables[0].Rows[0]["Password"] != null && ds.Tables[0].Rows[0]["Password"].ToString().Trim() != "")
            {
                hdnPwd.Value = ds.Tables[0].Rows[0]["Password"].ToString();
                txtPassword.Text = EncryptDecryptClass.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString().Trim());
            }
            ddlUserType.SelectedValue = ds.Tables[0].Rows[0]["UserType"].ToString().Trim();
            chkActive.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Active"].ToString().Trim());
            txtDealer.Text = ds.Tables[0].Rows[0]["DealerCode"].ToString().Trim();
            txtDlrName.Value = ds.Tables[0].Rows[0]["DealerName"].ToString().Trim();
            txtEmail.Text = ds.Tables[0].Rows[0]["Email"].ToString().Trim();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string stat;
        if (hdnRowId.Value.Trim() == "0" || hdnRowId.Value.Trim()=="")
            stat = "S";
        else
            stat = "U";

        try
        {
            if (ValidateDealerCode())
            {
                objUser.prpLoginId = Session["UserID"].ToString();
                objUser.prpRowId = Convert.ToInt32(hdnRowId.Value);
                objUser.prpUserId = txtUserId.Text.Trim();
                objUser.prpUserName = txtUserName.Text.Trim();
                //if (txtPassword.Text != "")
                //    objUser.prpPassword = EncryptDecryptClass.Encrypt(txtPassword.Text.Trim());
                //else
                //    objUser.prpPassword = "";
                if (stat == "S" || txtPassword.Text != "")
                    objUser.prpPassword = EncryptDecryptClass.Encrypt(txtPassword.Text.Trim());
                else
                    objUser.prpPassword = hdnPwd.Value;

                objUser.prpUserType = ddlUserType.SelectedValue;
                if (ddlUserType.SelectedValue == "Dealer")
                    objUser.prpDealerCode = Convert.ToInt32(txtDealer.Text.Trim());
                else
                    objUser.prpDealerCode = 0;
                objUser.prpEmail = txtEmail.Text.Trim();
                objUser.prpStatus = chkActive.Checked;
                string strResult = objUser.SaveUserMaster();
                if (strResult == "")
                    Response.Redirect("UserMaster.aspx?stat=" + stat, false);
                else
                {
                    lblMessage.Text = strResult;
                    lblMessage.CssClass = "ErrorMessage";
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
            lblMessage.CssClass = "ErrorMessage";
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("UserMasterAddEdit.aspx?strCode=" + hdnUserId.Value + "&strRowId=" + hdnRowId.Value + "&&New=" + Request.QueryString["New"]);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("UserMaster.aspx");
    }
    protected void btnChangePwd_Click(object sender, EventArgs e)
    {
        trPwd.Visible = true;
        trPwd.Style.Add("display", "block");

    }
    protected void ddlUserType_SelectedIndexChanged(object sender, EventArgs e)
    {   
        if (ddlUserType.SelectedValue == "Dealer")
            trDlr.Style.Add("display", "table-row");
        else
            trDlr.Style.Add("display", "none");

        txtDealer.Text = "";

    }

    protected bool ValidateDealerCode()
    {

        if (ddlUserType.Text != "Dealer")
        {
            lblMessage.Text = "";
            lblMessage.CssClass = "";
            return true;
        }       
        
        string strSearchQuery = " where Status=1 and DealerCode='" + txtDealer.Text.Trim() + "'";
        DataSet dsDealer = objDealer.FetchDealerMaster(strSearchQuery);
        if (dsDealer.Tables[0].Rows.Count > 0)
        {
            txtDlrName.Value = dsDealer.Tables[0].Rows[0]["Dealername"].ToString();
            lblMessage.Text = "";
            lblMessage.CssClass = "";
            return true;
        }
        else
        {
            txtDealer.Focus();
            txtDlrName.Value = "";
            lblMessage.Text = "DealerCode Not Found.";
            lblMessage.CssClass = "ErrorMessage";
            return false;
        }

       
         

    }
    protected void txtDealer_TextChanged(object sender, EventArgs e)
    {
        ValidateDealerCode();
    }
}
