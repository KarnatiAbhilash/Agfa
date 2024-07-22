/* ==============================================================================================
    Created By      : Subahani SM
    Created Date    : 29 July 2010
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

public partial class Popups_popupExecutive : System.Web.UI.Page
{
    ExecutiveMasterClass objExecutive = new ExecutiveMasterClass();

    DataSet dsExecutive;
    static string SelectType;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null) Response.Redirect("../Logout.aspx");
        try
        {
            if (!IsPostBack)
            {
                BindData(true, false);
                SelectType = Convert.ToString(Request.QueryString["SelectType"]);
                hidSelectType.Value = SelectType;
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void BindData(bool isrebind, bool issearch)
    {
        string strSearchQuery = "";

        if (issearch)
            strSearchQuery = " where Status=1 and " + ddlsearch.SelectedValue + " like '%" + txtValue.Text.Trim() + "%'";
        else
            strSearchQuery = " where Status=1";

        dsExecutive = objExecutive.FetchExecutiveMaster(strSearchQuery);
        Session["dsExecutive"] = dsExecutive;
        if (isrebind == true)
        {
            gvExecutive.DataSource = dsExecutive;
            gvExecutive.DataBind();
            if (dsExecutive.Tables[0].Rows.Count > 0)
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
    protected void gvExecutive_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvExecutive.PageIndex = e.NewPageIndex;
        gvExecutive.DataSource = Session["dsExecutive"];
        gvExecutive.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int intcnt = 0;
        try
        {
            for (int i = 0; i < gvExecutive.Rows.Count; i++)
            {
                RadioButton rdoButon = gvExecutive.Rows[i].FindControl("rbtnSelect") as RadioButton;

                if (rdoButon.Checked)
                {
                    intcnt = intcnt + 1;
                    Label lblExecutiveCode = gvExecutive.Rows[i].FindControl("lblExecutiveCode") as Label;

                    ClientScript.RegisterClientScriptBlock(GetType(), "CallEmp", "<script type=\"text/javascript\" language=\"javascript\">FillParentWindow(\"" + hidSelectType.Value + "\",\"" + lblExecutiveCode.Text + "\")</script>");
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
        BindData(true, false);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            BindData(true, true);
        }
        catch (Exception ex)
        {

        }
    }
}
