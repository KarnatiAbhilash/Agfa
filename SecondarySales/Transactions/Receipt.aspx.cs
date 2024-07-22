/* ==============================================================================================
    Created By      : Subahani SM
    Created Date    : 25 July 2010
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

public partial class Transactions_Receipt : System.Web.UI.Page
{
    ReceiptMaster objReceipt = new ReceiptMaster();
    MonthEndClass objMonth = new MonthEndClass();
    PurchaseOrder objPurchaseOrder = new PurchaseOrder();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null) Response.Redirect("../Logout.aspx");
        try
        {
            if (!IsPostBack)
            {
                btnAdd.Style.Add("display", "none");
                objMonth.prpDealerCode = Convert.ToInt32(Session["DealerCode"].ToString());
                DataSet ds = objMonth.GetOpenMonth();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtMonth.Text = ds.Tables[0].Rows[0]["Month"].ToString();
                    txtYear.Text = ds.Tables[0].Rows[0]["Year"].ToString();
                }
                gvReceipt.PageSize = Convert.ToInt32(Session["PageSize"].ToString());
                BindData(true, true);
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
    protected void btnAdd_New(object sender, EventArgs e)
    {
        Response.Redirect("ReceiptAddEdit.aspx");
    }
    protected void BindData(bool isFromDB, bool IsRebind)
    {
        DataTable dtReceipt;
        DataView dvReceipt;

        if (isFromDB)
        {
            objReceipt.prpDealerCode = Convert.ToInt32(Session["DealerCode"].ToString());
            dtReceipt = objReceipt.FetchPO().Tables[0];

            if (Cache.Get("Receipt" + Convert.ToString(Session["UserId"])) != null)
                Cache.Remove("Receipt" + Convert.ToString(Session["UserId"]));

            Cache.Insert("Receipt" + Convert.ToString(Session["UserId"]), dtReceipt, null, DateTime.MaxValue, TimeSpan.FromMinutes(30), CacheItemPriority.Normal, null);

        }
        else
        {
            dtReceipt = (DataTable)Cache.Get("Receipt" + Convert.ToString(Session["UserId"]));
        }

        dvReceipt = new DataView();
        if (ViewState["sortExpr"] != null)
        {
            dvReceipt = new DataView(dtReceipt);
            dvReceipt.Sort = (string)ViewState["sortExpr"];
        }
        else
            dvReceipt = dtReceipt.DefaultView;
        gvReceipt.DataSource = dvReceipt;

        if (IsRebind)
            gvReceipt.DataBind();
    }

    protected void gvReceipt_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        BindData(true, true);
    }

    protected void gvReceipt_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (ViewState["sortExpr"] == null || ViewState["sortExpr"].ToString().Contains("ASC"))
        {
            ViewState["sortExpr"] = e.SortExpression + " " + "DESC";
        }
        else
        {
            ViewState["sortExpr"] = e.SortExpression + " " + "ASC";
        }

        BindData(false, true);
    }

    protected void gvReceipt_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //GridView grid = (sender as GridView);
        //try
        //{
        //    switch (e.CommandName.Trim())
        //    {
        //        case "Delete":
        //            {
        //                int Index = Int32.Parse(e.CommandArgument.ToString());
        //                GridViewRow row = gvReceipt.Rows[Index];
        //                objPurchaseOrder.prpPONo = row.Cells[6].Text;
        //                string strResult = "";
        //                if (row.Cells[5].Text.ToString() == "Pending")
        //                {
        //                    strResult = objPurchaseOrder.DeletePurchaseOrder();
        //                    if (strResult == "")
        //                    {
        //                        lblMessage.Text = "Deleted Succesfully.";
        //                        lblMessage.CssClass = "SuccessMessage";
        //                    }
        //                    else
        //                    {
        //                        lblMessage.Text = strResult.Trim();
        //                        lblMessage.CssClass = "ErrorMessage";
        //                    }
        //                }
        //                else
        //                {
        //                    lblMessage.Text = "You Can Delete Pending PO-Status For Which Receipt is Not Generated.";
        //                    lblMessage.CssClass = "ErrorMessage";
        //                }
        //            }
        //            break;
        //    }
        //}
        //catch (Exception Ex)
        //{
        //    lblMessage.Text = Ex.Message;
        //    lblMessage.CssClass = "ErrorMessage";
        //}
    }

    protected void gvReceipt_PageIndexChanged(object sender, GridViewPageEventArgs e)
    {
        gvReceipt.PageIndex = e.NewPageIndex;
        BindData(false, true);
    }

    protected void gvReceipt_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {         
            e.Row.Cells[0].Attributes.Add("onclick", "DelReceipt('" + e.Row.RowIndex + "');");
           
        }
        //disbling the Delete Option for Other User Except Adim User
        if (Session["UserId"].ToString().ToLower() != "admin")
        {
            gvReceipt.Columns[0].Visible = false;              
        }
    }

    protected void btnDummy_Click(object sender, EventArgs e)
    {
        try
        {
            int Index = Int32.Parse(hdnDummy.Value);
            GridViewRow row = gvReceipt.Rows[Index];            
            objPurchaseOrder.prpPONo = row.Cells[6].Text;
            string strResult = "";
            if (row.Cells[5].Text.ToString() == "Pending")
            {
                strResult = objPurchaseOrder.DeletePurchaseOrder();
                if (strResult == "")
                {
                    row.Style.Add("Display", "None");
                    lblMessage.Text = "Deleted Succesfully.";
                    lblMessage.CssClass = "SuccessMessage";
                }
                else
                {
                    lblMessage.Text = strResult.Trim();
                    lblMessage.CssClass = "ErrorMessage";
                }
            }
            else
            {
                lblMessage.Text = "You Can Delete Pending PO-Status For Which Receipt is Not Generated.";
                lblMessage.CssClass = "ErrorMessage";
            }         

        }
        catch (Exception Ex)
        {
            lblMessage.Text = Ex.Message;
            lblMessage.CssClass = "ErrorMessage";
        }

    }
}
