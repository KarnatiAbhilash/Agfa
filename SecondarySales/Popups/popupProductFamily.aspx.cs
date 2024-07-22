/* ==============================================================================================
    Created By      : Subahani SM
    Created Date    : 23 July 2010
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

public partial class Popups_popupProductFamily : System.Web.UI.Page
{
    ProductFamilyClass objProb = new ProductFamilyClass();

    DataSet dsProd;
    static string SelectType;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null) Response.Redirect("../Logout.aspx");
        try
        {
            if (!IsPostBack)
            {
                bindProb(true, false);
                SelectType = Convert.ToString(Request.QueryString["SelectType"]);
                hidSelectType.Value = SelectType;
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void bindProb(bool isrebind, bool issearch)
    {
        string strSearchQuery = "";

        if (issearch)
            strSearchQuery = " where Status=1 and " + ddlsearch.SelectedValue + " like '%" + txtValue.Text.Trim() + "%' and BCCode='" + Request.QueryString["BudgetCls"].ToString() + "'";
        else
            strSearchQuery = " where Status=1 and BCCode='" + Request.QueryString["BudgetCls"].ToString() + "'";

        dsProd = objProb.FetchProductFamily(strSearchQuery);
        Session["dsProd"] = dsProd;
        if (isrebind == true)
        {
            gvProdGrp.DataSource = dsProd;
            gvProdGrp.DataBind();
            if (dsProd.Tables[0].Rows.Count > 0)
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
    protected void gvProdGrp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvProdGrp.PageIndex = e.NewPageIndex;
        gvProdGrp.DataSource = Session["dsProd"];
        gvProdGrp.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int intcnt = 0;
        try
        {
            for (int i = 0; i < gvProdGrp.Rows.Count; i++)
            {
                RadioButton rdoButon = gvProdGrp.Rows[i].FindControl("rbtnSelect") as RadioButton;

                if (rdoButon.Checked)
                {
                    intcnt = intcnt + 1;
                    Label lblPFCode = gvProdGrp.Rows[i].FindControl("lblPFCode") as Label;

                    ClientScript.RegisterClientScriptBlock(GetType(), "CallEmp", "<script type=\"text/javascript\" language=\"javascript\">FillParentWindow(\"" + hidSelectType.Value + "\",\"" + lblPFCode.Text + "\")</script>");
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
        bindProb(true, false);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            bindProb(true, true);
        }
        catch (Exception ex)
        {

        }
    }
}
