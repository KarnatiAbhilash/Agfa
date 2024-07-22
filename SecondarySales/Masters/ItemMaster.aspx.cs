/* ==============================================================================================
    Created By      : Subahani SM
    Created Date    : 27 July 2010
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
using AjaxControlToolkit;
using System.Web.SessionState;

public partial class Masters_ItemMaster : Page, IRequiresSessionState
{
    
    ItemMasterClass objItem = new ItemMasterClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null) Response.Redirect("../Logout.aspx");
        try
        {
            if (!IsPostBack)
            {
                lblMessage.Text = "";
                lblMessage.CssClass = "";
                gvItem.PageSize = Convert.ToInt32(Session["PageSize"].ToString());
                CommonFunction.PopulateRecordsWithTwoParam("Search_Values", "FieldName", "FieldValue", "SearchName", "ItemMaster", "Status", "1", "ID", ddlsearch);
                ddlsearch.Items.Add(new ListItem("Status", "Status"));
                ddlsearch.Items.RemoveAt(2);
                ddlsearch.Items.RemoveAt(2);
                ddlsearch.Items.RemoveAt(2);
                ddlsearch.Items.Insert(2, "Description1");
                ddlsearch.Items.Insert(3, "Description2");
                BindData(true, true, false);
                if (Request.QueryString["stat"] != null && Request.QueryString["stat"].ToString() != "")
                {
                    if (Request.QueryString["stat"].ToString() == "S")
                        lblMessage.Text = "Saved Successfully.";
                    else
                        lblMessage.Text = "Updated Successfully.";
                    lblMessage.CssClass = "SuccessMessage";
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }
    protected void BindData(bool isFromDB, bool IsRebind, bool issearch)
    {
        string strParamValue = this.txtValue.Text;
        DataTable table;
        if (isFromDB)
        {
            if (this.ddlsearch.SelectedValue == "Status")
                strParamValue = strParamValue.ToLower() == "active" || strParamValue.ToLower() == "1" || strParamValue.ToLower() == "true" ? "1" : "0";
            table = !issearch ? this.objItem.FetchItemMaster(" where Status=1").Tables[0] : CommonFunction.SearchRecordsWithOneParam("ItemMaster", this.ddlsearch.SelectedValue.Trim(), strParamValue, "Id").Tables[0];
            if (this.Cache.Get("Item" + Convert.ToString(this.Session["UserId"])) != null)
                this.Cache.Remove("Item" + Convert.ToString(this.Session["UserId"]));
            this.Cache.Insert("Item" + Convert.ToString(this.Session["UserId"]), (object)table, (CacheDependency)null, DateTime.MaxValue, TimeSpan.FromMinutes(30.0), CacheItemPriority.Normal, (CacheItemRemovedCallback)null);
        }
        else
            table = (DataTable)this.Cache.Get("Item" + Convert.ToString(this.Session["UserId"]));
        DataView dataView1 = new DataView();
        DataView dataView2;
        if (this.ViewState["sortExpr"] != null)
        {
            dataView2 = new DataView(table);
            dataView2.Sort = (string)this.ViewState["sortExpr"];
        }
        else
            dataView2 = table.DefaultView;
        this.gvItem.DataSource = (object)dataView2;
        if (!IsRebind)
            return;
        this.gvItem.DataBind();
    }

    protected void btnAdd_New(object sender, EventArgs e)
    {
        Response.Redirect("ItemMasterAddEdit.aspx?New=S");
    }

    protected void gvItem_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (ddlsearch.SelectedIndex > 0)
            BindData(true, true, true);
        else
            BindData(true, true, false);
    }

    protected void gvItem_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (ViewState["sortExpr"] == null || ViewState["sortExpr"].ToString().Contains("ASC"))
        {
            ViewState["sortExpr"] = e.SortExpression + " " + "DESC";
        }
        else
        {
            ViewState["sortExpr"] = e.SortExpression + " " + "ASC";
        }

        BindData(false, true, false);
    }

    protected void gvItem_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //GridView grid = (sender as GridView);
        //switch (e.CommandName.Trim())
        //{
        //    case "Delete":
        //        {
        //            int Index = Int32.Parse(e.CommandArgument.ToString());
        //            GridViewRow row = gvItem.Rows[Index];
        //            objItem.prpId = Int32.Parse(row.Cells[1].Text.ToString());

        //            string strResult = objItem.DeleteItemMaster();
        //            if (strResult == "")
        //            {
        //                lblMessage.Text = "Deleted Succesfully.";
        //                lblMessage.CssClass = "SuccessMessage";
        //            }
        //            else
        //            {
        //                lblMessage.Text = strResult.Trim();
        //                lblMessage.CssClass = "ErrorMessage";
        //            }
        //        }
        //        break;
        //}
    }

    protected void gvItem_PageIndexChanged(object sender, GridViewPageEventArgs e)
    {
        gvItem.PageIndex = e.NewPageIndex;
        BindData(false, true, false);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData(true, true, true);
    }

    protected void btnClearSearch_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        lblMessage.CssClass = "";
        ddlsearch.SelectedIndex = 0;
        txtValue.Text = "";
        BindData(true, true, false);
    }
    protected void gvItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.DataItemIndex != -1)
        {
            if (e.Row.Cells[6].Text.ToLower() == "active" || e.Row.Cells[6].Text.ToLower() == "true")
                e.Row.Cells[6].Text = "Active";
            else
                e.Row.Cells[6].Text = "In-Active";
        }
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.EmptyDataRow)
        {
            e.Row.Cells[0].Attributes.Add("OnClick", "Del('" + e.Row.RowIndex + "')");
        }
    }

    protected void btnDummy_Click(object sender, EventArgs e)
    {
        this.objItem.prpId = int.Parse(this.gvItem.Rows[int.Parse(this.hdnDummy.Value)].Cells[1].Text.ToString());
        string str = this.objItem.DeleteItemMaster();
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


