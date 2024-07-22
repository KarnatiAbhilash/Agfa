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

public partial class Popups_popupEquipmentProductGroup : System.Web.UI.Page
{

    ProductGroupClass objBudget = new ProductGroupClass();

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
    //this method is used for 
    protected void bindBC(bool isrebind, bool issearch)
    {
        string strSearchQuery = "";

        if (issearch)
            strSearchQuery = " where Status=1 and " + ddlsearch.SelectedValue + " like '%" + txtValue.Text.Trim() + "%'";
        else
            strSearchQuery = " where Status=1";

        //this is for binding records equipment productgroup 
        dsBudget = objBudget.FetchEquipmentProductClass(strSearchQuery);
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
                    Label lblGroupCode = gvBudget.Rows[i].FindControl("lblGroupCode") as Label;

                    ClientScript.RegisterClientScriptBlock(GetType(), "CallEmp", "<script type=\"text/javascript\" language=\"javascript\">FillParentWindow(\"" + hidSelectType.Value + "\",\"" + lblGroupCode.Text + "\")</script>");
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

    protected void gvBudget_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvBudget_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}