using BusinessClass;
using System;
using System.Data;
using System.Web;
using System.Web.Caching;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Masters_EquipmentProductGroupMaster : System.Web.UI.Page
{
    private EquipmentProductGroupMasterClass objEPRM = new EquipmentProductGroupMasterClass();
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
            this.gvEquipment.PageSize = Convert.ToInt32(this.Session["PageSize"].ToString());
            CommonFunction.PopulateRecordsWithTwoParam("Search_Values", "FieldName", "FieldValue", "SearchName", "Equipment", "Status", "1", "ID", this.ddlsearch);
            this.ddlsearch.Items.Add(new ListItem("Status", "Status"));
            this.ddlsearch.Items.Insert(2, new ListItem("Description", "EquipmentDesc"));
            this.BindData(true, true, false);
            if (this.Request.QueryString["stat"] == null || !(this.Request.QueryString["stat"].ToString() != ""))
                return;
            this.lblMessage.Text = !(this.Request.QueryString["stat"].ToString() == "S") ? "Updated Successfully." : "Saved Successfully.";
            this.lblMessage.CssClass = "SuccessMessage";
        }
        catch (Exception ex)
        {
            this.lblMessage.Text = ex.Message;
        }
    }

    protected void BindData(bool isFromDB, bool IsRebind, bool issearch)
    {
        string strParamValue = this.txtValue.Text;
        DataTable table1;
        if (isFromDB)
        {
            if (this.ddlsearch.SelectedValue == "Status")
                strParamValue = strParamValue.ToLower() == "active" || strParamValue.ToLower() == "1" || strParamValue.ToLower() == "true" ? "1" : "0";
            if (issearch)
            {
                DataTable table2 = CommonFunction.SearchRecordsWithOneParam("EquipmentProductGroupMaster", this.ddlsearch.SelectedValue.Trim(), strParamValue, "EMId").Tables[0];
            }
            table1 = this.objEPRM.FetchEquipmentProductGroupMaster(" where Status=1");
            if (this.Cache.Get("Equipment" + Convert.ToString(this.Session["UserId"])) != null)
                this.Cache.Remove("Equipment" + Convert.ToString(this.Session["UserId"]));
            this.Cache.Insert("Equipment" + Convert.ToString(this.Session["UserId"]), (object)table1, (CacheDependency)null, DateTime.MaxValue, TimeSpan.FromMinutes(30.0), CacheItemPriority.Normal, (CacheItemRemovedCallback)null);
        }
        else
            table1 = (DataTable)this.Cache.Get("Equipment" + Convert.ToString(this.Session["UserId"]));
        DataView dataView1 = new DataView();
        DataView dataView2;
        if (this.ViewState["sortExpr"] != null)
        {
            dataView2 = new DataView(table1);
            dataView2.Sort = (string)this.ViewState["sortExpr"];
        }
        else
            dataView2 = table1.DefaultView;
        this.gvEquipment.DataSource = (object)dataView2;
        if (!IsRebind)
            return;
        this.gvEquipment.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e) {
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

    protected void btnAdd_New(object sender, EventArgs e)
    {
        this.Response.Redirect("EquipmentProductGroupMasterAddEdit.aspx?New=S");
    }

    protected void btnDummy_Click(object sender, EventArgs e)
    {
        if (this.ddlsearch.SelectedIndex > 0)
            this.BindData(true, true, true);
        else
            this.BindData(true, true, false);
    }

    protected void gvEquipment_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (this.ViewState["sortExpr"] == null || this.ViewState["sortExpr"].ToString().Contains("ASC"))
            this.ViewState["sortExpr"] = (object)(e.SortExpression + " DESC");
        else
            this.ViewState["sortExpr"] = (object)(e.SortExpression + " ASC");
        this.BindData(false, true, false);
    }

    protected void gvEquipment_SelectedIndexChanged(object sender, GridViewPageEventArgs e)
    {
        this.gvEquipment.PageIndex = e.NewPageIndex;
        this.BindData(false, true, false);
    }

    protected void gvEquipment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.DataItemIndex != -1)
        {
            if (e.Row.Cells[4].Text.ToLower() == "active" || e.Row.Cells[4].Text.ToLower() == "true")
                e.Row.Cells[4].Text = "Active";
            else
                e.Row.Cells[4].Text = "In-Active";
        }
        if (e.Row.RowType != DataControlRowType.DataRow && e.Row.RowType != DataControlRowType.EmptyDataRow)
            return;
        e.Row.Cells[0].Attributes.Add("OnClick", "Del('" + (object)e.Row.RowIndex + "')");
    }

    protected void gvEquipment_PageIndexChanged(object sender, GridViewPageEventArgs e)
    {
        this.gvEquipment.PageIndex = e.NewPageIndex;
        this.BindData(false, true, false);
    }

    protected void gvEquipment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }

    protected void gvEquipment_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (this.ddlsearch.SelectedIndex > 0)
            this.BindData(true, true, true);
        else
            this.BindData(true, true, false);
    }


}