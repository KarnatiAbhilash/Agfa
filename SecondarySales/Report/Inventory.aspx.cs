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

public partial class Report_Inventory : Page, IRequiresSessionState
{
    DealerMasterClass objDlr = new DealerMasterClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null) Response.Redirect("../Logout.aspx");
        try
        {
            if (!IsPostBack)
            {
                CommonFunction.PopulateRecords("ProductGroupMaster", "GroupCode", "GroupCode", "pgId", ddlProductGroup);
                if (Session["UserName"].ToString().ToLower() != "admin")
                {
                    if (Session["UserType"].ToString().ToLower() == "dealer")
                    {
                        btnDealer.Disabled = true;
                        txtDealerCode.Enabled = false;
                        txtDealerCode.Text = Session["DealerCode"].ToString();
                        txtDealerName.Value = Session["DealerName"].ToString();
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtDealerCode.Text != "" && txtDealerName.Value != "")
        {
            Session["DealerName"] = txtDealerName.Value.Trim();
            Session["DealerCode"] = txtDealerCode.Text.Trim();
        }
        else
        {
            Session["DealerName"] = "0";
            Session["DealerCode"] = "0";
        }
        if (ddlProductGroup.SelectedIndex != 0)

            Session["ProductGroup"] = ddlProductGroup.Text.Trim();
        else
            Session["ProductGroup"] = "0";

        Response.Redirect("ExcelInventoryReport.aspx");
    }
    protected bool ValidateDealerCode()
    {
        string strSearchQuery = " where Status=1 and DealerCode='" + txtDealerCode.Text.Trim() + "'";
        DataSet dsDealer = objDlr.FetchDealerMaster(strSearchQuery);
        if (dsDealer.Tables[0].Rows.Count > 0)
        {
            txtDealerName.Value = dsDealer.Tables[0].Rows[0][4].ToString();
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
