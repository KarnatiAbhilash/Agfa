/* ==============================================================================================
    Created By      : Subahani SM
    Created Date    : 03 Sep 2010
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
using System.Web.Caching;
using BusinessClass;

public partial class MonthEnd_MonthEnd : System.Web.UI.Page
{
    MonthEndClass objMonth = new MonthEndClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null) Response.Redirect("../Logout.aspx");
        try
        {
            if (!IsPostBack)
            {
                objMonth.prpDealerCode = Convert.ToInt32(Session["DealerCode"].ToString());
                objMonth.IsAgfaUser = false;
                DataSet ds = objMonth.GetOpenMonth();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtMonth.Text = ds.Tables[0].Rows[0]["Month"].ToString();
                    txtYear.Text = ds.Tables[0].Rows[0]["Year"].ToString();
                    hdnOpenMonthNo.Value = ds.Tables[0].Rows[0]["MonthNo"].ToString();
                    hdnCurrMonth.Value = ds.Tables[0].Rows[0]["CurrMonth"].ToString();
                    hdnCurrYear.Value = ds.Tables[0].Rows[0]["CurrYear"].ToString();
                    btnMonthEnd.Style.Add("display", "inline");
                    btnMonthInitialize.Style.Add("display", "none");
                }
                else
                {
                    btnMonthEnd.Style.Add("display", "none");
                    btnMonthInitialize.Style.Add("display", "inline");
                }

                if (Request.QueryString["stat"] != null && Request.QueryString["stat"].ToString() != "")
                {
                    if (Request.QueryString["stat"].ToString() == "S")
                        lblMessage.Text = "Month End Closed Sucessfully.";
                    else
                        lblMessage.Text = "Month End Initialization Has Done Sucessfully.";

                    lblMessage.CssClass = "SuccessMessage";
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
            lblMessage.CssClass = "ErrorMessage";
        }
    }
    protected void btnMonthEnd_Click(object sender, EventArgs e)
    {
        string strResult = "";
        int intMonthNo=0;
        int intYear=0;
        try
        {
            intMonthNo = Convert.ToInt32(hdnOpenMonthNo.Value.Trim());
            intYear=Convert.ToInt32(txtYear.Text.Trim());
            if (intMonthNo == 12)
            {
                intMonthNo = 1;
                intYear = intYear + 1;
            }
            else
                intMonthNo += 1;

            objMonth.prpUserId = Session["UserID"].ToString();
            objMonth.prpDealerCode = Convert.ToInt32(Session["DealerCode"].ToString());
            objMonth.prpMonthNo = intMonthNo;
            objMonth.prpYear = intYear;
            objMonth.prpDebitCreditNote = "";
            strResult = objMonth.CloseMonthEnd();
            if (strResult == "")
                Response.Redirect("MonthEnd.aspx?stat=S");
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
    protected void btnMonthInitialize_Click(object sender, EventArgs e)
    {
        string strResult = "";
        try
        {
            objMonth.prpUserId = Session["UserID"].ToString();
            objMonth.prpDealerCode = Convert.ToInt32(Session["DealerCode"].ToString());
            objMonth.prpDebitCreditNote = "";
            strResult = objMonth.MonthEndInitialize();
            if (strResult == "")
                Response.Redirect("MonthEnd.aspx?stat=I");
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
}
