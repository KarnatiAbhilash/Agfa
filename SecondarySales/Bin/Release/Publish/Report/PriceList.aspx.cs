using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BusinessClass;

public partial class Report_PriceList : Page, IRequiresSessionState
{
    DealerMasterClass objDealer = new DealerMasterClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null) Response.Redirect("../Logout.aspx");
        if (!IsPostBack)
        {
            CommonFunction.PopulateRecords("ExecutiveMaster", "ExecutiveCode", "ExecutiveCode", "emId", ddlExecutiveCode);
            CommonFunction.PopulateRecordsWithTwoParam("Common_Values", "Text", "Value", "FieldName", "Region", "Status", "1", "Id", ddlRegion);
            CommonFunction.PopulateRecords("stateMaster", "StateName", "StateCode", "StateCode", ddlState);
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string strQueryString = "?";
        string strDealer = "";
        strDealer = txtDealerCode.Text.Trim();
        strQueryString = strQueryString + "CustCode=" + txtCustCode.Value.Trim() + "&ItemCode=" + txtItemCode.Value.Trim() + "&DealerCode=" + strDealer + "&ExcutiveCode=" + ddlExecutiveCode.SelectedValue.Trim();
        strQueryString = strQueryString + "&Region=" + ddlRegion.SelectedValue.Trim() + "&CustGroup=" + ddlCustGrp.SelectedValue.Trim() + "&State=" + ddlState.SelectedValue.Trim();

        Response.Redirect("ExcelPriceList.aspx" + strQueryString);
    }
    protected void txtDealerCode_TextChanged(object sender, EventArgs e)
    {
        ValidateDealerCode();
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
}
