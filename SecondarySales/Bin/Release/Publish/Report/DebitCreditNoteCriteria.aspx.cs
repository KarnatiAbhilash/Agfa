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

public partial class Reports_DebitCreditNoteCriteria : System.Web.UI.Page
{
    DealerMasterClass objDealer = new DealerMasterClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null) Response.Redirect("../Logout.aspx");
        try
        {
            if (!IsPostBack)
            {
                CommonFunction.PopulateRecords("BudgetClassMaster", "BCcode", "BCcode", "bcId", ddlBudgetClassCode);
                CommonFunction.PopulateRecords("ExecutiveMaster", "ExecutiveCode", "ExecutiveCode", "emId", ddlExecutiveCode);
                CommonFunction.PopulateRecordsWithTwoParam("Common_Values", "Text", "Value", "FieldName", "Region", "Status", "1", "Id", ddlRegion);
                CommonFunction.PopulateRecords("stateMaster", "StateName", "StateCode", "StateCode", ddlState);
                CommonFunction.FetchGetMonthYear(1, ddlMonth);
                CommonFunction.FetchGetMonthYear(0, ddlYear);
                CommonFunction.GetCustomerGroup(ddlCustGrp);
                if (Session["UserName"].ToString().ToLower() != "admin")
                {
                    if (Session["UserType"].ToString().ToLower() == "dealer")
                    {
                        btnDealer.Disabled = true;
                        txtDealerCode.Enabled = false;
                        txtDealerCode.Text = Session["DealerCode"].ToString();
                    }
                    else
                    {
                        btnDealer.Disabled = false;
                    }
                }

            }
        }
        catch (Exception ex)
        {
            //lblMessage.Text = ex.Message;
        }
    }
    protected void ddlBudgetClassCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlProductFamily.Items.Clear();
        ddlProductGroupCode.Items.Clear();
        CommonFunction.PopulateRecordsWithOneParam("ProductFamilyMaster", "pfcode", "pfcode", "Bccode", ddlBudgetClassCode.Text, "pfId", ddlProductFamily);
    }
    protected void ddlProductFamily_SelectedIndexChanged(object sender, EventArgs e)
    {
        CommonFunction.PopulateRecordsWithTwoParam("ProductGroupMaster", "GroupCode", "GroupCode", "Bccode", ddlBudgetClassCode.Text, "pfcode", ddlProductFamily.Text, "pgId", ddlProductGroupCode);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ddlBudgetClassCode.SelectedIndex != 0)
            Session["BCCode"] = ddlBudgetClassCode.Text.Trim();
        else
            Session["BCCode"] = "0";

        if (ddlProductFamily.Text != "")
            Session["PFCode"] = ddlProductFamily.Text.Trim();
        else
            Session["PFCode"] = "0";

        if (ddlProductGroupCode.SelectedIndex != 0 && ddlProductGroupCode.SelectedIndex != -1)
            Session["GroupCode"] = ddlProductGroupCode.Text.Trim();
        else
            Session["GroupCode"] = "0";

        if (txtItemCode.Value != "")
            Session["ItemCode"] = txtItemCode.Value.Trim();
        else
            Session["ItemCode"] = "0";

        if (ddlMonth.SelectedIndex != 0)
            Session["Month"] = ddlMonth.Text.Trim();
        else
            Session["Month"] = "0";

        if (ddlYear.SelectedIndex != 0)
            Session["Year"] = ddlYear.Text.Trim();
        else
            Session["Year"] = "0";

        if (txtCustCode.Value != "")
            Session["CustCode"] = txtCustCode.Value.Trim();
        else
            Session["CustCode"] = "0";

        if (ddlExecutiveCode.SelectedIndex != 0)
            Session["ExeCode"] = ddlExecutiveCode.Text.Trim();
        else
            Session["ExeCode"] = "0";

        if (ddlRegion.SelectedIndex != 0)
            Session["Region"] = ddlRegion.Text.Trim();
        else
            Session["Region"] = "0";

        if (txtDealerCode.Text.ToString() != "")
            Session["DealerCode"] = txtDealerCode.Text.Trim();
        else
            Session["DealerCode"] = "0";

        if (ddlState.SelectedIndex != 0)
            Session["State"] = ddlState.Text.Trim();
        else
            Session["State"] = "0";

        if (txtInvFromDate.Text != "" && txtInvToDate.Text != "")
        {
            Session["InvFromDate"] = txtInvFromDate.Text.Trim();
            Session["InvToDate"] = txtInvToDate.Text.Trim();
        }
        else
        {
            Session["InvFromDate"] = "0";
            Session["InvToDate"] = "0";
        }

        if (ddlCustGrp.SelectedIndex != 0)
        {
            Session["@CustGrp"] = ddlCustGrp.Text.Trim();
        }
        else
            Session["CustGrp"] = "0";

      

            

        //if (chkViewAll.Checked == true)
        //{
        //    Session["IsAll"] = "1";
        //    Session["PFCode"] = "0";
        //    Session["GroupCode"] = "0";
        //    Session["ItemCode"] = "0";
        //    Session["Month"] = "0";
        //    Session["Year"] = "0";
        //    Session["CustCode"] = "0";
        //    Session["ExeCode"] = "0";
        //    Session["Region"] = "0";
        //    Session["DealerCode"] = "0";
        //    Session["State"] = "0";
        //    Session["CustGrp"] = "0";
        //    Session["InvFromDate"] = DateTime.Now.ToShortDateString();
        //    Session["InvToDate"] = DateTime.Now.ToShortDateString();
        //}
        //else
        //{
        //    Session["IsAll"] = "0";
        //    Session["CustGrp"] = ddlCustGrp.Text;
        //}
        Response.Redirect("ExcelDebitCreditNoteCriteria.aspx");
    }
    protected bool ValidateDealerCode()
    {
        string strSearchQuery = " where Status=1 and DealerCode='" + txtDealerCode.Text.Trim() + "'";
        DataSet dsDealer = objDealer.FetchDealerMaster(strSearchQuery);
        if (dsDealer.Tables[0].Rows.Count > 0)
        {
            //  txtDmsCode.Text = dsDealer.Tables[0].Rows[0]["DMSCode"].ToString();
            lblMessage.Text = "";
            lblMessage.CssClass = "";
            return true;
        }
        else
        {
            lblMessage.Focus();
            lblMessage.Text = "DealerCode Not Found.";
            lblMessage.CssClass = "ErrorMessage";
            return false;
        }
    }
    protected void txtDealerCode_TextChanged(object sender, EventArgs e)
    {
        ValidateDealerCode();
    }
}
