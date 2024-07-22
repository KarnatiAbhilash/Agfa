using BusinessClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_SalesEmployee : System.Web.UI.Page
{
    private ProductGroupClass objBC = new ProductGroupClass();
    private SalesEmployee objBC1 = new SalesEmployee();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Session["UserID"] == null)
            this.Response.Redirect("../Logout.aspx");
        try
        {
            if (this.IsPostBack)
                return;
            this.lblMessage.Text = "";
            this.lblMessage.CssClass = "";
            this.gvBudget.PageSize = Convert.ToInt32(this.Session["PageSize"].ToString());
            CommonFunction.PopulateRecordsWithTwoParam("Search_Values", "FieldName", "FieldValue", "SearchName", "SalesEmployee", "Name", "1", "Id", this.ddlsearch);
            this.ddlsearch.Items.Add(new ListItem("Name", "Name"));
            this.ddlsearch.Items.Add(new ListItem("Region", "Region"));
            this.ddlsearch.Items.Add(new ListItem("EmailId", "EmailId"));
            this.ddlsearch.Items.Add(new ListItem("ContactNo", "ContactNo"));
            this.BindData(true, true, false);
            if (this.Request.QueryString["stat"] == null || !(this.Request.QueryString["stat"].ToString() != ""))
                return;
            this.lblMessage.Text = !(this.Request.QueryString["stat"].ToString() == "S") ? "Updated Successfully." : "Saved Successfully.";
            this.lblMessage.CssClass = "SuccessMessage";
        }
        catch (Exception ex)
        {
            this.lblMessage.Text = ex.Message;
            this.lblMessage.CssClass = "ErrorMessage";
        }
    }

    protected void BindData(bool isFromDB, bool IsRebind, bool issearch)
    {
        string text = this.txtValue.Text;
        DataTable table;
        if (isFromDB)
        {
            table = !issearch ? this.objBC1.FetchSalesEmployeeClass("").Tables[0] : CommonFunction.SearchRecordsWithOneParam("SalesEmployee", this.ddlsearch.SelectedValue.Trim(), text, "Id").Tables[0];
            if (this.Cache.Get("Budget" + Convert.ToString(this.Session["UserId"])) != null)
                this.Cache.Remove("Budget" + Convert.ToString(this.Session["UserId"]));
            this.Cache.Insert("Budget" + Convert.ToString(this.Session["UserId"]), (object)table, (CacheDependency)null, DateTime.MaxValue, TimeSpan.FromMinutes(30.0), CacheItemPriority.Normal, (CacheItemRemovedCallback)null);
        }
        else
            table = (DataTable)this.Cache.Get("Budget" + Convert.ToString(this.Session["UserId"]));
        DataView dataView1 = new DataView();
        DataView dataView2;
        if (this.ViewState["sortExpr"] != null)
        {
            dataView2 = new DataView(table);
            dataView2.Sort = (string)this.ViewState["sortExpr"];
        }
        else
            dataView2 = table.DefaultView;
        this.gvBudget.DataSource = (object)dataView2;
        if (!IsRebind)
            return;
        this.gvBudget.DataBind();
    }

    protected void btnAdd_New(object sender, EventArgs e)
    { 
        this.Response.Redirect("SalesEmployeeAddEdit.aspx?New=S");
    }

    protected void gvBudget_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (this.ddlsearch.SelectedIndex > 0)
            this.BindData(true, true, true);
        else
            this.BindData(true, true, false);
    }

    protected void gvBudget_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (this.ViewState["sortExpr"] == null || this.ViewState["sortExpr"].ToString().Contains("ASC"))
            this.ViewState["sortExpr"] = (object)(e.SortExpression + " DESC");
        else
            this.ViewState["sortExpr"] = (object)(e.SortExpression + " ASC");
        this.BindData(false, true, false);
    }

    protected void gvBudget_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }

    protected void gvBudget_PageIndexChanged(object sender, GridViewPageEventArgs e)
    {
        this.gvBudget.PageIndex = e.NewPageIndex;
        this.BindData(false, true, false);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.BindData(true, true, true);
    }

    protected void btnClearSearch_Click(object sender, EventArgs e)
    {
        this.lblMessage.Text = "";
        this.lblMessage.CssClass = "";
        this.ddlsearch.SelectedIndex = 0;
        this.txtValue.Text = "";
        this.BindData(true, true, false);
    }

    protected void gvBudget_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow && e.Row.RowType != DataControlRowType.EmptyDataRow)
            return;
        e.Row.Cells[0].Attributes.Add("OnClick", "Del('" + (object)e.Row.RowIndex + "')");
    }

    protected void btnDummy_Click(object sender, EventArgs e)
    {
        this.objBC1.prpId = int.Parse(this.gvBudget.Rows[int.Parse(this.hdnDummy.Value)].Cells[1].Text.ToString());
        string str = this.objBC1.DeleteSalesEmployee();
        if (str == "")
        {
            this.lblMessage.Text = "Deleted Succesfully.";
            this.lblMessage.CssClass = "SuccessMessage";
        }
        else
        {
            this.lblMessage.Text = str.Trim();
            this.lblMessage.CssClass = "ErrorMessage";
        }
        this.BindData(true, true, false);
    }
}