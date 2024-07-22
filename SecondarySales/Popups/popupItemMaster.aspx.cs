/* ==============================================================================================
    Created By      : Subahani SM
    Created Date    : 20 Aug 2010
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

public partial class Popups_popupItemMaster : System.Web.UI.Page
{
    ItemMasterClass objItem = new ItemMasterClass();

    DataSet dsCust;
    static string SelectType;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null) Response.Redirect("../Logout.aspx");
        try
        {
            if (!IsPostBack)
            {
                BindItem(true, false);
                SelectType = Convert.ToString(Request.QueryString["SelectType"]);
                //hdnCount.Value = Request.QueryString["strCount"].ToString();
                hidSelectType.Value = SelectType;
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void BindItem(bool isrebind, bool issearch)
    {
        string strSearchQuery = "";

        if (issearch)
            strSearchQuery = " where Status=1 and " + ddlsearch.SelectedValue + " like '%" + txtValue.Text.Trim() + "%'";
        else
            strSearchQuery = " where Status=1 ";

        if (Request.QueryString["GC"] != null && Request.QueryString["GC"] != "")
        {
            if (Request.QueryString["GC"].ToString() != "")
                strSearchQuery = strSearchQuery + " and GroupCode=" + "'" + Request.QueryString["GC"].ToString() + "'";
        }

        if (Request.QueryString["PFCode"] != null && Request.QueryString["PFCode"] != "")
        {
            if (Request.QueryString["PFCode"].ToString() != "")
                strSearchQuery = strSearchQuery + " and PFCode=" + "'" + Request.QueryString["PFCode"].ToString() + "'";
        }


        if (Request.QueryString["BCCode"] != null && Request.QueryString["BCCode"] != "")
        {
            if (Request.QueryString["BCCode"].ToString() != "")
                strSearchQuery = strSearchQuery + " and BCCode=" + "'" + Request.QueryString["BCCode"].ToString() + "'";
        }

        dsCust = objItem.FetchItemMaster(strSearchQuery);
        Session["dsCust"] = dsCust;
        if (isrebind == true)
        {
            gvItem.DataSource = dsCust;
            gvItem.DataBind();
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
    protected void gvItem_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvItem.PageIndex = e.NewPageIndex;
        gvItem.DataSource = Session["dsCust"];
        gvItem.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int intcnt = 0;
        try
        {
            for (int i = 0; i < gvItem.Rows.Count; i++)
            {
                RadioButton rdoButon = gvItem.Rows[i].FindControl("rbtnSelect") as RadioButton;

                if (rdoButon.Checked)
                {
                    intcnt = intcnt + 1;
                    Label lblId = gvItem.Rows[i].FindControl("lblId") as Label;
                    Label lblItemCode = gvItem.Rows[i].FindControl("lblItemCode") as Label;
                    Label lblDesc1 = gvItem.Rows[i].FindControl("lblDesc1") as Label;
                    Label lblDesc2 = gvItem.Rows[i].FindControl("lblDesc2") as Label;
                    Label lblConvFactor = gvItem.Rows[i].FindControl("lblConvFactor") as Label;
                    string strScript = "<script type=\"text/javascript\" language=\"javascript\">FillParentWindow(\"" + hidSelectType.Value + "\",\"" + hdnCount.Value + "\",\"" + lblId.Text + "\",\"" + lblItemCode.Text.Replace("\"", "") + "\",\"" + lblDesc1.Text.Replace("\"", "") + "\",\"" + lblDesc2.Text.Replace("\"","") + "\",\"" + lblConvFactor.Text + "\")</script>";
                    ClientScript.RegisterClientScriptBlock(GetType(), "CallCust", strScript);
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
        BindItem(true, false);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            BindItem(true, true);
        }
        catch (Exception ex)
        {

        }
    }
}
