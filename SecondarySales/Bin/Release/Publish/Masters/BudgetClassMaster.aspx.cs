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

public partial class Masters_BudgetClassMaster : System.Web.UI.Page
{
    BudgetClassMaster objBC = new BudgetClassMaster();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null) Response.Redirect("../Logout.aspx");
        try
        {
            if (!IsPostBack)
            {
                lblMessage.Text = "";
                lblMessage.CssClass = "";
                gvBudget.PageSize = Convert.ToInt32(Session["PageSize"].ToString());
                CommonFunction.PopulateRecordsWithTwoParam("Search_Values", "FieldName", "FieldValue", "SearchName", "BudgetClass", "Status", "1", "ID", ddlsearch);
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
            lblMessage.CssClass = "ErrorMessage";
        }
    }
    protected void BindData(bool isFromDB, bool IsRebind, bool issearch)
    {
        DataTable dtBC;
        DataView dvBC;
        DataSet dsSetBC;
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
                dsSetBC = CommonFunction.SearchRecordsWithOneParam("BudgetClassMaster", ddlsearch.SelectedValue.Trim(), strVal, "BCId");
                dtBC = dsSetBC.Tables[0];
            }
            else
                dtBC = objBC.FetchBudgetClass(" where Status=1").Tables[0];


            if (Cache.Get("Budget" + Convert.ToString(Session["UserId"])) != null)
                Cache.Remove("Budget" + Convert.ToString(Session["UserId"]));

            Cache.Insert("Budget" + Convert.ToString(Session["UserId"]), dtBC, null, DateTime.MaxValue, TimeSpan.FromMinutes(30), CacheItemPriority.Normal, null);

        }
        else
        {
            dtBC = (DataTable)Cache.Get("Budget" + Convert.ToString(Session["UserId"]));
        }

        dvBC = new DataView();
        if (ViewState["sortExpr"] != null)
        {
            dvBC = new DataView(dtBC);
            dvBC.Sort = (string)ViewState["sortExpr"];
        }
        else
            dvBC = dtBC.DefaultView;
        gvBudget.DataSource = dvBC;

        if (IsRebind)
            gvBudget.DataBind();
    }

    protected void btnAdd_New(object sender, EventArgs e)
    {
        Response.Redirect("BudgetClassMasterAddEdit.aspx?New=S");
    }

    protected void gvBudget_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (ddlsearch.SelectedIndex > 0)
            BindData(true, true, true);
        else
            BindData(true, true, false);
    }

    protected void gvBudget_Sorting(object sender, GridViewSortEventArgs e)
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

    protected void gvBudget_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //GridView grid = (sender as GridView);
        //switch (e.CommandName.Trim())
        //{
        //    case "Delete":
        //        {
        //            int Index = Int32.Parse(e.CommandArgument.ToString());
        //            GridViewRow row = gvBudget.Rows[Index];
        //            objBC.prpBCId = Int32.Parse(row.Cells[1].Text.ToString());

        //            string strResult = objBC.DeleteBudgetClass();
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

    protected void gvBudget_PageIndexChanged(object sender, GridViewPageEventArgs e)
    {
        gvBudget.PageIndex = e.NewPageIndex;
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
        BindData(true, true,false);
    }
    protected void gvBudget_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.DataItemIndex != -1)
        {
            if (e.Row.Cells[4].Text.ToLower() == "active" || e.Row.Cells[4].Text.ToLower() == "true")
                e.Row.Cells[4].Text = "Active";
            else
                e.Row.Cells[4].Text = "In-Active";
        }
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.EmptyDataRow)
        {
            e.Row.Cells[0].Attributes.Add("OnClick", "Del('" + e.Row.RowIndex + "')");
        }
    }
    protected void btnDummy_Click(object sender, EventArgs e)
    {
        int Index = Int32.Parse(hdnDummy.Value);
        GridViewRow row = gvBudget.Rows[Index];
        objBC.prpBCId = Int32.Parse(row.Cells[1].Text.ToString());

        string strResult = objBC.DeleteBudgetClass();
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
