using BusinessClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_SalesEmployeeAddEdit : System.Web.UI.Page
{


    private CommonFunction objComm = new CommonFunction();
    private SalesEmployee objItem = new SalesEmployee();
    private ProductGroupClass objGroup = new ProductGroupClass();
    
    protected void Page_Load(object sender, EventArgs e)
    {


        if (this.Session["UserID"] == null)
            this.Response.Redirect("../Logout.aspx");
        try
        {
            if (this.IsPostBack)
                return;
            CommonFunction.PopulateRecordsWithTwoParam("Common_Values", "Text", "Value", "FieldName", "Region", "Status", "1", "Id", this.ddlRegion);

            this.btnSave.Attributes.Add("onclick", "return fnValidateSave()");
            if (this.Request.QueryString["New"] != null)
                this.lblHeader.Text = !(this.Request.QueryString["New"].ToString() == "S") ? "Edit -Sales Employee  Master" : "Add -Sales Employee Master";
            if (this.Request.QueryString["strCode"] == null || !(this.Request.QueryString["strCode"].ToString() != "") || !(this.Request.QueryString["New"].ToString() != "S"))
                return;
            this.hdnBCId.Value = this.Request.QueryString["strCode"].ToString();
            this.BindData();
            this.txtName.ReadOnly = true;
        }
        catch (Exception ex)
        {
            this.lblMessage.Text = ex.Message;
        }
    }

    protected void BindData()
    {
        this.objItem.prpId = Convert.ToInt32(this.hdnBCId.Value);
        DataSet dataSet = this.objItem.FetchSalesEmployeeClassOnId();
        if (dataSet.Tables[0].Rows.Count <= 0)
            return;
        this.txtName.Text = dataSet.Tables[0].Rows[0]["Name"].ToString().Trim();
        //this.txtRegion.Text = dataSet.Tables[0].Rows[0]["Region"].ToString().Trim();
        this.txtEmail.Text = dataSet.Tables[0].Rows[0]["EmailId"].ToString().Trim();
        this.txtContNo.Text = dataSet.Tables[0].Rows[0]["ContactNo"].ToString().Trim();
        this.ddlRegion.SelectedValue = dataSet.Tables[0].Rows[0]["Region"].ToString().Trim();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string str1 = !(this.hdnBCId.Value.Trim() == "0") ? "U" : "S";
        try
        {
            this.objItem.prpUserId = this.Session["UserID"].ToString();
            this.objItem.prpId = Convert.ToInt32(this.hdnBCId.Value);
            this.objItem.prpName = this.txtName.Text.Trim();
           // this.objItem.prpRegion = this.txtRegion.Text.Trim();
            this.objItem.prpRegion = this.ddlRegion.SelectedValue;
            this.objItem.prpEmailId = this.txtEmail.Text.Trim();
            this.objItem.prpContactNo = this.txtContNo.Text.Trim();
            string str2 = this.objItem.SaveSalesEmployee();
            if (str2 == "")
            {
                this.Response.Redirect("SalesEmployee.aspx?stat=" + str1, false);
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

    protected bool ValidateProductGroupCode()
    {
        if (this.objGroup.FetchProductGroup(" where Status=1 and GroupCode='" + this.txtName.Text.Trim() + "' ").Tables[0].Rows.Count > 0)
        {
            this.lblMessage.Text = "";
            this.lblMessage.CssClass = "";
            return true;
        }
        this.lblMessage.Text = "Name Not Found.";
        this.lblMessage.CssClass = "ErrorMessage";
        return false;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("SalesEmployeeAddEdit.aspx?strCode=" + this.hdnBCId.Value + "&&New=" + this.Request.QueryString["New"]);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("SalesEmployee.aspx");
    }

    protected void txtProdGroup_TextChanged(object sender, EventArgs e)
    {
        this.ValidateProductGroupCode();
    }
}
    
