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

public partial class Masters_ProductGroupMaster : System.Web.UI.Page
{
    ProductGroupClass objGroup = new ProductGroupClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null) Response.Redirect("../Logout.aspx");
        try
        {
            if (!IsPostBack)
            {
                lblMessage.Text = "";
                lblMessage.CssClass = "";
                gvGroup.PageSize = Convert.ToInt32(Session["PageSize"].ToString());
                CommonFunction.PopulateRecordsWithTwoParam("Search_Values", "FieldName", "FieldValue", "SearchName", "ProductGroup", "Status", "1", "ID", ddlsearch);
                ddlsearch.Items.Add(new ListItem("Status", "Status"));

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
        DataTable dtgroup;
        DataView dvgroup;
        DataSet dsSetgroup;
        string strVal = txtValue.Text;
        if (isFromDB)
        {
            if (ddlsearch.SelectedValue == "Status")
            {
                if (strVal.ToLower() == "active" || strVal.ToLower() == "1" || strVal.ToLower() == "true")
                    strVal = "1";
                else
                    strVal = "0";
            }

            if (issearch)
            {
                dsSetgroup = CommonFunction.SearchRecordsWithOneParam("ProductGroupMaster", ddlsearch.SelectedValue.Trim(), strVal, "PGId");
                dtgroup = dsSetgroup.Tables[0];
            }
            else
                dtgroup = objGroup.FetchProductGroup(" where Status=1").Tables[0];


            if (Cache.Get("Group" + Convert.ToString(Session["UserId"])) != null)
                Cache.Remove("Group" + Convert.ToString(Session["UserId"]));

            Cache.Insert("Group" + Convert.ToString(Session["UserId"]), dtgroup, null, DateTime.MaxValue, TimeSpan.FromMinutes(30), CacheItemPriority.Normal, null);

        }
        else
        {
            dtgroup = (DataTable)Cache.Get("Group" + Convert.ToString(Session["UserId"]));
        }

        dvgroup = new DataView();
        if (ViewState["sortExpr"] != null)
        {
            dvgroup = new DataView(dtgroup);
            dvgroup.Sort = (string)ViewState["sortExpr"];
        }
        else
            dvgroup = dtgroup.DefaultView;
        gvGroup.DataSource = dvgroup;

        if (IsRebind)
            gvGroup.DataBind();
    }

    protected void btnAdd_New(object sender, EventArgs e)
    {
        Response.Redirect("ProductGroupMasterAddEdit.aspx?New=S");
    }

    protected void gvGroup_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (ddlsearch.SelectedIndex > 0)
            BindData(true, true, true);
        else
            BindData(true, true, false);
    }

    protected void gvGroup_Sorting(object sender, GridViewSortEventArgs e)
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

    protected void gvGroup_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //GridView grid = (sender as GridView);
        //switch (e.CommandName.Trim())
        //{
        //    case "Delete":
        //        {
        //            int Index = Int32.Parse(e.CommandArgument.ToString());
        //            GridViewRow row = gvGroup.Rows[Index];
        //            objGroup.prpPGId = Int32.Parse(row.Cells[1].Text.ToString());

        //            string strResult = objGroup.DeleteProductGroup();
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

    protected void gvGroup_PageIndexChanged(object sender, GridViewPageEventArgs e)
    {
        gvGroup.PageIndex = e.NewPageIndex;
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
    protected void gvGroup_RowDataBound(object sender, GridViewRowEventArgs e)
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
        int Index = Int32.Parse(hdnDummy.Value);
        GridViewRow row = gvGroup.Rows[Index];
        objGroup.prpPGId = Int32.Parse(row.Cells[1].Text.ToString());

        string strResult = objGroup.DeleteProductGroup();
        if (strResult == "")
        {
            lblMessage.Text = "Deleted Succesfully.";
            lblMessage.CssClass = "SuccessMessage";
        }
        else
        {
            lblMessage.Text = strResult.Trim();
            lblMessage.CssClass = "ErrorMessage";
        }
        BindData(true, true, false);
    }
}
