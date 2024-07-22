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


public partial class Popups_popupDealerItem : System.Web.UI.Page
{
    DealerMasterClass objDlr = new DealerMasterClass();

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
                hdnCount.Value = Request.QueryString["strCount"].ToString();
                hidSelectType.Value = SelectType;
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void BindItem(bool isrebind, bool issearch)
    {
        btnSave.Enabled = false;
        string strSearchQuery = "";
        if (Request.QueryString["PONo"] != null)
        {
            if (Request.QueryString["strType"].ToString() == "Commercial")
                strSearchQuery = "inner join PurchaseOrderDetails POD on DP.Item_Id=POD.ItemId where POD.pono ='" + Request.QueryString["PONo"] + "'";
            else
                strSearchQuery = "inner join PurchaseOrderDetails POD on CSS.ItemId=POD.ItemId where POD.pono ='" + Request.QueryString["PONo"] + "'";
        }
        if (issearch)
        {   
            if(strSearchQuery!="")
            strSearchQuery += " and IM.Status=1 and " + ddlsearch.SelectedValue + " like '%" + txtValue.Text.Trim() + "%'";
            else
            strSearchQuery = " where IM.Status=1 and " + ddlsearch.SelectedValue + " like '%" + txtValue.Text.Trim() + "%'";
               

        }
        else
        {
            if (strSearchQuery != "")
            strSearchQuery += " and IM.Status=1 ";
            else
            strSearchQuery += " where IM.Status=1 ";

        }

        if (Request.QueryString["strType"].ToString() == "Commercial")
        {
            if (Request.QueryString["dlr"] != null)
            {
                if (Request.QueryString["dlr"].ToString() != "")
                    strSearchQuery = strSearchQuery + " and DealerCode=" + Request.QueryString["dlr"].ToString();
                else
                    strSearchQuery = strSearchQuery + " and DealerCode=0";
            }
            else
                strSearchQuery = strSearchQuery + " and DealerCode=0";
        }
        else if (Request.QueryString["strType"].ToString() == "Sample")
        {
            strSearchQuery = strSearchQuery + " and DealerCode=" + Request.QueryString["dlr"].ToString();
        }
        else
            return;

        dsCust = objDlr.FetchDealerItems(strSearchQuery, Request.QueryString["strType"].ToString());
        Session["dsCust"] = dsCust;

        if (isrebind == true && dsCust.Tables.Count>0 )
        {
            gvItem.DataSource = dsCust;
            gvItem.DataBind();
            if (dsCust.Tables[0].Rows.Count > 0)
            {
                lblMessage.Text = "";
                lblMessage.CssClass = "";
                btnSave.Enabled = true;
                btnSave.Visible = true;
            }
            else
            {
                lblMessage.Text = "No Record(s) Found.";
                lblMessage.CssClass = "ErrorMessage";
                btnSave.Visible = false;
            }
        }
        else
            if (dsCust.Tables.Count == 0)
        {
        
                lblMessage.Text = "No Record(s) Found.";
                lblMessage.CssClass = "ErrorMessage";
                btnSave.Visible = false;
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
                    Label lblDIMId = gvItem.Rows[i].FindControl("lblDIMId") as Label;
                    Label lblItemId = gvItem.Rows[i].FindControl("lblItemId") as Label;
                    Label lblItemCode = gvItem.Rows[i].FindControl("lblItemCode") as Label;
                    Label lblDesc1 = gvItem.Rows[i].FindControl("lblDesc1") as Label;
                    Label lblDesc2 = gvItem.Rows[i].FindControl("lblDesc2") as Label;
                    Label lblDlrPrice = gvItem.Rows[i].FindControl("lblDlrPrice") as Label;
                    Label lblPerSqrMt = gvItem.Rows[i].FindControl("lblPerSqrMt") as Label;
                    Label lblMaxQty = gvItem.Rows[i].FindControl("lblMaxQty") as Label;
                    ClientScript.RegisterClientScriptBlock(GetType(), "CallCust", "<script type=\"text/javascript\" language=\"javascript\">FillParentWindow(\"" + hidSelectType.Value + "\",\"" + hdnCount.Value + "\",\"" + lblDIMId.Text + "\",\"" + lblItemId.Text + "\",\"" + lblItemCode.Text + "\",\"" + lblDesc1.Text + "\",\"" + lblDesc2.Text + "\",\"" + lblDlrPrice.Text + "\",\"" + lblPerSqrMt.Text + "\",\"" + lblMaxQty.Text + "\")</script>");
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
