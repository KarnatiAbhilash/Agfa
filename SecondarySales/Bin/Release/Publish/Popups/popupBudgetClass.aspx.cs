/* ==============================================================================================
    Created By      : Subahani SM
    Created Date    : 22 July 2010
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

public partial class Popups_popupBudgetClass : System.Web.UI.Page
{
    BudgetClassMaster objBudget = new BudgetClassMaster();

    DataSet dsBudget;
    static string SelectType;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null) Response.Redirect("../Logout.aspx");
        try
        {
            if (!IsPostBack)
            {
                bindBC(true, false);
                SelectType = Convert.ToString(Request.QueryString["SelectType"]);
                hidSelectType.Value = SelectType;
            }
        }
        catch (Exception ex)
        {
            
        }
    }
    protected void bindBC(bool isrebind, bool issearch)
    {
        string strSearchQuery = "";

        if (issearch)
            strSearchQuery = " where Status=1 and " + ddlsearch.SelectedValue + " like '%" + txtValue.Text.Trim() + "%'";
        else
            strSearchQuery = " where Status=1";

        dsBudget = objBudget.FetchBudgetClass(strSearchQuery);
        Session["dsBudget"] = dsBudget;
        if (isrebind == true)
        {
            gvBudget.DataSource = dsBudget;
            gvBudget.DataBind();
            if (dsBudget.Tables[0].Rows.Count > 0)
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
    protected void gvBudget_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvBudget.PageIndex = e.NewPageIndex;
        gvBudget.DataSource = Session["dsBudget"];
        gvBudget.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {        
        int intcnt = 0;
        try
        {
            for (int i = 0; i < gvBudget.Rows.Count; i++)
            {
                RadioButton rdoButon = gvBudget.Rows[i].FindControl("rbtnSelect") as RadioButton;

                if (rdoButon.Checked)
                {
                    intcnt = intcnt + 1;
                    Label lblBCCode = gvBudget.Rows[i].FindControl("lblBCCode") as Label;                   
                   
                    ClientScript.RegisterClientScriptBlock(GetType(), "CallEmp", "<script type=\"text/javascript\" language=\"javascript\">FillParentWindow(\"" + hidSelectType.Value + "\",\"" + lblBCCode.Text + "\")</script>");
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
        bindBC(true, false);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {       
        try
        {
            bindBC(true, true);
        }
        catch (Exception ex)
        {
            
        }
    }
}
