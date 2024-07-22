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

public partial class Report_SchemeAchievement : Page, IRequiresSessionState
{
    DealerMasterClass objDlr = new DealerMasterClass();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserID"] == null) Response.Redirect("../Logout.aspx");
        try
        {
            if (!IsPostBack)
            {
                //CommonFunction.PopulateRecords("ProductGroupMaster", "GroupCode", "GroupCode", "pgId", ddlProductGroup);
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

    protected void btnSave_Click(object sender, EventArgs e)
    {

        String Query = "";
        //     WHERE     (dbo.ItemMaster.ItemCode = @ItemCode) AND (dbo.Issue.SalesType = @SalesType) AND (dbo.DealerMaster.DealerName = @DealerName) AND 
        //--                    (dbo.Receipt.InvoiceDate between @InvFromDate and @InvToDate)
        if (txtDealerCode.Text != "")
            Session["DealerCode"] = txtDealerCode.Text;
        else
            Session["DealerCode"] = "0";

        if (txtItemCode.Value != "")
            Session["ItemCode"] = txtItemCode.Value.Trim();
        else
            Session["ItemCode"] = "0";


        if (txtCustCode.Value != "")
            Session["CustCode"] = txtCustCode.Value.Trim();
        else
            Session["CustCode"] = "0";


        if (txtInvFromDate.Text != "" && txtInvToDate.Text != "")
        {
            Session["InvFromDate"] = txtInvFromDate.Text;
            Session["InvToDate"] = txtInvToDate.Text;
        }
        else
        {
            Session["InvFromDate"] = "0";
            Session["InvToDate"] = "0";
        }
        Response.Redirect("ExcelSchemeAchievementRpt.aspx");
    }
    protected bool ValidateDealerCode()
    {
        string strSearchQuery = " where Status=1 and DealerCode='" + txtDealerCode.Text.Trim() + "'";
        DataSet dsDealer = objDlr.FetchDealerMaster(strSearchQuery);
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
