/* ==============================================================================================
    Created By      : Subahani SM
    Created Date    : 03 Aug 2010
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


public partial class Popups_popupCustomer : System.Web.UI.Page
{
    CustomerMasterClass objCust = new CustomerMasterClass();

    DataSet dsCust;
    static string SelectType;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null) Response.Redirect("../Logout.aspx");
        try
        {
            if (!IsPostBack)
            {
                BindCust(true, false);
                SelectType = Convert.ToString(Request.QueryString["SelectType"]);
                hdnCount.Value = Request.QueryString["strCount"].ToString();
                hidSelectType.Value = SelectType;
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void BindCust(bool isrebind, bool issearch)
    {
        string strSearchQuery = "";

        if (issearch)
        {
            if (Session["UserType"].ToString() == "Dealer")
            {
                strSearchQuery = " where isnull(DirectCustomer,0) = 0 and  Status=1 and " + ddlsearch.SelectedValue + " like '%" + txtValue.Text.Trim() + "%'";
            }
            else if(Session["UserType"].ToString() == "Agfa")
            {
                strSearchQuery = " where DirectCustomer = 1 and  Status=1 and " + ddlsearch.SelectedValue + " like '%" + txtValue.Text.Trim() + "%'";
            }
            else 
            {
                strSearchQuery = " where Status=1 and " + ddlsearch.SelectedValue + " like '%" + txtValue.Text.Trim() + "%'";

            }
        }
        else
        {
            if (Session["UserType"].ToString() == "Dealer")
            {
                strSearchQuery = " where isnull(DirectCustomer,0) = 0 and Status=1 ";
            }
            else if (Session["UserType"].ToString() == "Agfa")
            {
                strSearchQuery = " where DirectCustomer = 1 and  Status=1 ";
            }
            else
            {
                strSearchQuery = " where Status=1 ";
            }
        }
        if (Request.QueryString["dlr"] != null)
        {
            if (Request.QueryString["dlr"].ToString() != "" && Request.QueryString["dlr"].ToString() != "0")
                strSearchQuery = strSearchQuery + " and DealerCode=" + Request.QueryString["dlr"].ToString();
            //else
            //    strSearchQuery = strSearchQuery + " and DealerCode=0";
        }

        dsCust = objCust.FetchCustomerMaster(strSearchQuery);
        Session["dsCust"] = dsCust;
        if (isrebind == true)
        {
            gvCust.DataSource = dsCust;
            gvCust.DataBind();
            if (dsCust.Tables[0].Rows.Count > 0)
            {
                lblMessage.Text = "";
                lblMessage.CssClass = "";
                btnSave.Visible = true;
            }
            else
            {
                lblMessage.Text = "No Record(s) Found.";
                lblMessage.CssClass = "ErrorMessage";
                btnSave.Visible = false;
            }
        }


    }
    protected void gvCust_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvCust.PageIndex = e.NewPageIndex;
        gvCust.DataSource = Session["dsCust"];
        gvCust.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int intcnt = 0;
        try
        {
            for (int i = 0; i < gvCust.Rows.Count; i++)
            {
                RadioButton rdoButon = gvCust.Rows[i].FindControl("rbtnSelect") as RadioButton;

                if (rdoButon.Checked)
                {
                    intcnt = intcnt + 1;
                    Label lblCustCode = gvCust.Rows[i].FindControl("lblCustCode") as Label;
                    Label lblCustName = gvCust.Rows[i].FindControl("lblCustName") as Label;
                    Label lblCity = gvCust.Rows[i].FindControl("lblCity") as Label;
                    Label lblDealerCode = gvCust.Rows[i].FindControl("lblDealerCode") as Label;

                    ClientScript.RegisterClientScriptBlock(GetType(), "CallCust", "<script type=\"text/javascript\" language=\"javascript\">FillParentWindow(\"" + hidSelectType.Value + "\",\"" + hdnCount.Value + "\",\"" + lblCustCode.Text + "\",\"" + lblCustName.Text + "\",\"" + lblCity.Text + "\",\"" + lblDealerCode.Text + "\")</script>");
                }
            }
            if (intcnt < 1)
            {
                lblMessage.Text = "Please Select Atleast One Record.";
                lblMessage.CssClass = "ErrorMessage";
            }
            else
            {
                lblMessage.CssClass = "";
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtValue.Text = "";
        BindCust(true, false);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            BindCust(true, true);
        }
        catch (Exception ex)
        {

        }
    }
}
