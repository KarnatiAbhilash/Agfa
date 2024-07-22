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

public partial class Masters_ExecutiveMasterAddEdit : System.Web.UI.Page
{
    ExecutiveMasterClass objExecutive = new ExecutiveMasterClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null) Response.Redirect("../Logout.aspx");
        try
        {
            if (this.IsPostBack)
                return;
            CommonFunction.PopulateRecordsWithTwoParam("Common_Values", "Text", "Value", "FieldName", "Region", "Status", "1", "Id", this.ddlRegion);
            this.btnSave.Attributes.Add("onclick", "return fnValidateSave()");
            if (this.Request.QueryString["New"] != null)
                this.lblHeader.Text = !(this.Request.QueryString["New"].ToString() == "S") ? "Edit - Executive Master" : "Add - Executive Master";
            if (this.Request.QueryString["strCode"] == null || !(this.Request.QueryString["strCode"].ToString() != "") || !(this.Request.QueryString["New"].ToString() != "S"))
                return;
            this.hdnEMId.Value = this.Request.QueryString["strCode"].ToString();
            this.BindData();
            this.txtExecutiveCode.ReadOnly = true;
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }
    protected void BindData()
    {
        this.objExecutive.prpEMId = Convert.ToInt32(this.hdnEMId.Value);
        DataSet dataSet = this.objExecutive.FetchExecutiveMasterOnId();
        if (dataSet.Tables[0].Rows.Count <= 0)
            return;
        this.txtExecutiveCode.Text = dataSet.Tables[0].Rows[0]["ExecutiveCode"].ToString().Trim();
        this.txtUserId.Text = dataSet.Tables[0].Rows[0]["UserId"].ToString().Trim();
        this.txtDescrip.Text = dataSet.Tables[0].Rows[0]["ExecutiveDesc"].ToString().Trim();
        this.ddlRegion.SelectedValue = dataSet.Tables[0].Rows[0]["Region"].ToString().Trim();
        this.chkActive.Checked = Convert.ToBoolean(dataSet.Tables[0].Rows[0]["Status"].ToString().Trim());
        
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string str1 = !(this.hdnEMId.Value.Trim() == "0") ? "U" : "S";
        try
        {
            this.objExecutive.prpUserId = this.Session["UserID"].ToString();
            this.objExecutive.prpEMId = Convert.ToInt32(this.hdnEMId.Value);
            this.objExecutive.prpExecutiveCode = this.txtExecutiveCode.Text.Trim();
            this.objExecutive.prpExecutiveDesc = this.txtDescrip.Text.Trim();
            this.objExecutive.prpRegion = this.ddlRegion.SelectedValue;
            this.objExecutive.prpStatus = this.chkActive.Checked;
            this.objExecutive.prpExecutiveUserId = this.txtUserId.Text.Trim();
            string str2 = this.objExecutive.SaveExecutiveMaster();
            if (str2 == "")
            {
                this.Response.Redirect("ExecutiveMaster.aspx?stat=" + str1, false);
            }
            else
            {
                this.lblMessage.Text = str2;
                this.lblMessage.CssClass = "ErrorMessage";
            }
        }
        catch (Exception ex)
        {
            this.lblMessage.Text = ex.Message;
            this.lblMessage.CssClass = "ErrorMessage";
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ExecutiveMasterAddEdit.aspx?strCode=" + hdnEMId.Value + "&&New=" + Request.QueryString["New"]);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ExecutiveMaster.aspx");
    }
}

