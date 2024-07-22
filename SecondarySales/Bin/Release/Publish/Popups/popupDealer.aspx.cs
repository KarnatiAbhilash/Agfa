/* ==============================================================================================
    Created By      : Subahani SM
    Created Date    : 30 July 2010
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

public partial class Popups_popupDealer : System.Web.UI.Page
{
    DealerMasterClass objDealer = new DealerMasterClass();

    DataSet dsDealer;
    static string SelectType;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null) Response.Redirect("../Logout.aspx");
        try
        {
            if (!IsPostBack)
            {
                BindDealer(true, false);
                SelectType = Convert.ToString(Request.QueryString["SelectType"]);
                hidSelectType.Value = SelectType;
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void BindDealer(bool isrebind, bool issearch)
    {
        string strSearchQuery = "";

        if (issearch)
            strSearchQuery = " where Status=1 and " + ddlsearch.SelectedValue + " like '%" + txtValue.Text.Trim() + "%'";
        else
            strSearchQuery = " where Status=1";

        dsDealer = objDealer.FetchDealerMaster(strSearchQuery);
        Session["dsDealer"] = dsDealer;
        if (isrebind == true)
        {
            gvDealer.DataSource = dsDealer;
            gvDealer.DataBind();
            if (dsDealer.Tables[0].Rows.Count > 0)
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
    protected void gvDealer_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvDealer.PageIndex = e.NewPageIndex;
        gvDealer.DataSource = Session["dsDealer"];
        gvDealer.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int intcnt = 0;
        try
        {
            for (int i = 0; i < gvDealer.Rows.Count; i++)
            {
                RadioButton rdoButon = gvDealer.Rows[i].FindControl("rbtnSelect") as RadioButton;

                if (rdoButon.Checked)
                {
                    intcnt = intcnt + 1;
                    Label lblDealerCode = gvDealer.Rows[i].FindControl("lblDealerCode") as Label;
                    Label lblDealerName = gvDealer.Rows[i].FindControl("lblDealerName") as Label;
                    Label lblDMSCode = gvDealer.Rows[i].FindControl("lblDMSCode") as Label;

                    ClientScript.RegisterClientScriptBlock(GetType(), "CallEmp", "<script type=\"text/javascript\" language=\"javascript\">FillParentWindow(\"" + hidSelectType.Value + "\",\"" + lblDealerCode.Text + "\",\"" + lblDealerName.Text + "\",\"" + lblDMSCode.Text + "\")</script>");
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
        BindDealer(true, false);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            BindDealer(true, true);
        }
        catch (Exception ex)
        {

        }
    }
}