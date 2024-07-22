/* ==============================================================================================
    Created By      : Subahani SM
    Created Date    : 28 July 2010
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

public partial class Masters_DealerMasterAddEdit : System.Web.UI.Page
{
    DealerMasterClass objDealer = new DealerMasterClass();
    ExecutiveMasterClass objExecutive = new ExecutiveMasterClass();
    UserMasterClass objUser = new UserMasterClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null) Response.Redirect("../Logout.aspx");
        try
        {
            if (!IsPostBack)
            {
                CommonFunction.PopulateRecordsWithTwoParam("Common_Values", "Text", "Value", "FieldName", "Region", "Status", "1", "Id", ddlRegion);
                CommonFunction.PopulateRecordsWithOneParam("StateMaster", "StateName", "StateCode", "Active", "1", "StateName", ddlState);
                btnSave.Attributes.Add("onclick", "return fnValidateSave()");
                if (Request.QueryString["New"] != null)
                {
                    if (Request.QueryString["New"].ToString() == "S")
                    {
                        txtDMSCode.ReadOnly = false;
                        lblHeader.Text = "Add - Dealer Master";
                    }
                    else
                    {
                        txtDMSCode.ReadOnly = true;
                        lblHeader.Text = "Edit - Dealer Master";
                    }
                }
                if (Request.QueryString["strCode"] != null && Request.QueryString["strCode"].ToString() != "" && Request.QueryString["New"].ToString() != "S")
                {
                    hdnDealerCode.Value = Request.QueryString["strCode"].ToString();
                    BindData();
                    btnAddNew.Style.Add("display", "table-row");
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }
    protected void BindData()
    {
        objDealer.prpDealerCode= Convert.ToInt32(hdnDealerCode.Value);
        DataSet ds = objDealer.FetchDealerMasterOnId();
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtExecutive.Text = ds.Tables[0].Rows[0]["ExecutiveCode"].ToString().Trim();
            ddlRegion.SelectedValue = ds.Tables[0].Rows[0]["Region"].ToString().Trim();
            txtDealerCode.Text = ds.Tables[0].Rows[0]["DealerCode"].ToString().Trim();            
            txtDMSCode.Text = ds.Tables[0].Rows[0]["DMSCode"].ToString().Trim();
            txtDealerName.Text = ds.Tables[0].Rows[0]["DealerName"].ToString().Trim();
            txtAddress1.Text = ds.Tables[0].Rows[0]["Address1"].ToString().Trim();
            txtAddress2.Text = ds.Tables[0].Rows[0]["Address2"].ToString().Trim();
            txtAddress3.Text = ds.Tables[0].Rows[0]["Address3"].ToString().Trim();
            txtCity.Text = ds.Tables[0].Rows[0]["City"].ToString().Trim();
            ddlState.SelectedValue = ds.Tables[0].Rows[0]["State"].ToString().Trim();
            txtPincode.Text = ds.Tables[0].Rows[0]["Pincode"].ToString().Trim();
            txtContPerson.Text = ds.Tables[0].Rows[0]["ContactPerson"].ToString().Trim();
            txtContNo.Text = ds.Tables[0].Rows[0]["ContactNo"].ToString().Trim();
            txtEmailId.Text = ds.Tables[0].Rows[0]["EmailId"].ToString().Trim();
            txtFaxNo.Text = ds.Tables[0].Rows[0]["FaxNo"].ToString().Trim();
            txtTINNo.Text = ds.Tables[0].Rows[0]["TINNo"].ToString().Trim();
            txtCSTNo.Text = ds.Tables[0].Rows[0]["CSTNo"].ToString().Trim();
            txtLSTNo.Text = ds.Tables[0].Rows[0]["LSTNo"].ToString().Trim();
            txtRespuser.Text = ds.Tables[0].Rows[0]["RespUser"].ToString().Trim();
            chkActive.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Status"].ToString().Trim());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string stat;
        if (hdnDealerCode.Value.Trim() == "0")
            stat = "S";
        else
            stat = "U";

        try
        {
            if (ValidateExecutiveCode() && ValidateUserName())
            {
                objDealer.prpUserId = Session["UserID"].ToString();
                objDealer.prpDealerCode = Convert.ToInt32(hdnDealerCode.Value);
                objDealer.prpExecutiveCode = txtExecutive.Text.Trim();
                objDealer.prpRegion = ddlRegion.SelectedValue;
                objDealer.prpDMSCode = txtDMSCode.Text.Trim();
                objDealer.prpDealerName = txtDealerName.Text.Trim();
                objDealer.prpAddress1 = txtAddress1.Text.Trim();
                objDealer.prpAddress2 = txtAddress2.Text.Trim();
                objDealer.prpAddress3 = txtAddress3.Text.Trim();
                objDealer.prpCity = txtCity.Text.Trim();
                objDealer.prpState = ddlState.SelectedValue;
                objDealer.prpPincode = txtPincode.Text.Trim();
                objDealer.prpContPerson = txtContPerson.Text.Trim();
                objDealer.prpContNo = txtContNo.Text.Trim();
                objDealer.prpEmailId = txtEmailId.Text.Trim();
                objDealer.prpFaxNo = txtFaxNo.Text.Trim();
                objDealer.prpTINNo = txtTINNo.Text.Trim();
                objDealer.prpCSTNo = txtCSTNo.Text.Trim();
                objDealer.prpLSTNo = txtLSTNo.Text.Trim();
                objDealer.prpRespUser = txtRespuser.Text.Trim();
                objDealer.prpStatus = chkActive.Checked;
                string strResult = objDealer.SaveDealerMaster();
                if (strResult == "")
                    Response.Redirect("DealerMaster.aspx?stat=" + stat, false);
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
        Response.Redirect("DealerMasterAddEdit.aspx?strCode=" + hdnDealerCode.Value + "&&New=" + Request.QueryString["New"]);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("DealerMaster.aspx");
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("DealerItemMapping.aspx?DlrCode=" + hdnDealerCode.Value +"&DlrName="+txtDealerName.Text.Trim());
    }

    protected bool ValidateExecutiveCode()
    {
        string strSearchQuery = " where executivecode='" + txtExecutive.Text.Trim() + "'";
        DataSet dsExecutive = objExecutive.FetchExecutiveMaster(strSearchQuery);
            if (dsExecutive.Tables[0].Rows.Count > 0)
            {
                lblMessage.Text = "";
                lblMessage.CssClass = "";
                return  true;
            }
            else
            {
                lblMessage.Text = "ExecutiveCode Not Found.";
                lblMessage.CssClass = "ErrorMessage";
                return false;
            }
    }

    protected bool ValidateUserName()
    {
        string[] RespUser;
        int InvalidUser = 0;
        if (txtRespuser.Text != "")
        {
            RespUser = txtRespuser.Text.Split(',');
            foreach (string user in RespUser)
            {
                string strSearchQuery = " where Active=1 and UserId='" + user.Trim() + "'";
                DataSet dsBudget = objUser.FetchUserMaster(strSearchQuery);
                if (dsBudget.Tables[0].Rows.Count <= 0)
                {
                    InvalidUser++;
                }
            }
        }
        else
        {
            lblMessage.Text = "Please Enter The Responsible Users.";
            lblMessage.CssClass = "ErrorMessage";
            return false;
        }

        if (InvalidUser == 0)
        {            
            lblMessage.Text = "";
            lblMessage.CssClass = "";
            return true;
        }
        else
        {
            if (RespUser.Length == 1)
            {
                lblMessage.Text = "Rep.User Not Found";
                lblMessage.CssClass = "ErrorMessage";
            }
            else
            {
                lblMessage.Text = "One Of Rep.User Not Found Which is Separated By Comma.";
                lblMessage.CssClass = "ErrorMessage";
            }

            return false;
        }
    }

    protected void txtExecutive_TextChanged(object sender, EventArgs e)
    {
        ValidateExecutiveCode();
    }


    protected void txtRespuser_TextChanged(object sender, EventArgs e)
    {
        ValidateUserName();
    }
}
