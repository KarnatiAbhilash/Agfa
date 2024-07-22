/* ==============================================================================================
    Created By      : Subahani SM
    Created Date    : 22 July 2010
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

public partial class Masters_BudgetClassMasterAddEdit : System.Web.UI.Page
{
    CommonFunction objComm = new CommonFunction();
    BudgetClassMaster objBC = new BudgetClassMaster();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null) Response.Redirect("../Logout.aspx");
        try
        {
            if (!IsPostBack)
            {
                btnSave.Attributes.Add("onclick", "return fnValidateSave()");
                if (Request.QueryString["New"] != null)
                {
                    if (Request.QueryString["New"].ToString() == "S")
                        lblHeader.Text = "Add - Budget Class Master";
                    else
                        lblHeader.Text = "Edit - Budget Class Master";
                       
                    
                }
                if (Request.QueryString["strCode"] != null && Request.QueryString["strCode"].ToString() != "" && Request.QueryString["New"].ToString() != "S")
                {
                    hdnBCId.Value = Request.QueryString["strCode"].ToString();
                    BindData();
                    txtBudgetClassCode.ReadOnly = true;
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
        objBC.prpBCId = Convert.ToInt32(hdnBCId.Value);
        DataSet ds = objBC.FetchBudgetClassOnId();
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtBudgetClassCode.Text = ds.Tables[0].Rows[0]["BCCode"].ToString().Trim();
            txtDescrip.Text = ds.Tables[0].Rows[0]["BCDescription"].ToString().Trim();
            chkActive.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Status"].ToString().Trim());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string stat;
        if (hdnBCId.Value.Trim() == "0")
            stat = "S";
        else
            stat = "U";

        try
        {
            objBC.prpUserId = Session["UserID"].ToString();
            objBC.prpBCId = Convert.ToInt32(hdnBCId.Value);
            objBC.prpBCCode = txtBudgetClassCode.Text.Trim();
            objBC.prpBCDesc = txtDescrip.Text.Trim();
            objBC.prpStatus = chkActive.Checked;
            string strResult = objBC.SaveBudgetClass();
            if (strResult == "")
                Response.Redirect("BudgetClassMaster.aspx?stat=" + stat, false);
            else
            {
                lblMessage.Text = strResult;
                lblMessage.CssClass = "ErrorMessage";
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
        Response.Redirect("BudgetClassMasterAddEdit.aspx?strCode=" + hdnBCId.Value + "&&New=" + Request.QueryString["New"]);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("BudgetClassMaster.aspx");
    }
}
