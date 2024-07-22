/* ==============================================================================================
    Created By      : Subahani SM
    Created Date    : 04 Sep 2010
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

public partial class Transactions_StockReturnApproval : System.Web.UI.Page
{
   // StockReturnClass objStock = new StockReturnClass();

    private StockReturnClass objStock = new StockReturnClass();
  

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null) Response.Redirect("../Logout.aspx");
        try
        {
            if (!IsPostBack)
            {
                gvStockRet.PageSize = Convert.ToInt32(Session["PageSize"].ToString());
                BindData(true, true);
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
            lblMessage.CssClass = "ErrorMessage";
        }
    }
    protected void BindData(bool isFromDB, bool IsRebind)
    {
        DataTable dtStock;
        DataView dvStock;

        if (isFromDB)
        {           
            dtStock = objStock.FetchStockReturnList().Tables[0];

            if (Cache.Get("Stock" + Convert.ToString(Session["UserId"])) != null)
                Cache.Remove("Stock" + Convert.ToString(Session["UserId"]));

            Cache.Insert("Stock" + Convert.ToString(Session["UserId"]), dtStock, null, DateTime.MaxValue, TimeSpan.FromMinutes(30), CacheItemPriority.Normal, null);

        }
        else
        {
            dtStock = (DataTable)Cache.Get("Stock" + Convert.ToString(Session["UserId"]));
        }

        dvStock = new DataView();
        if (ViewState["sortExpr"] != null)
        {
            dvStock = new DataView(dtStock);
            dvStock.Sort = (string)ViewState["sortExpr"];
        }
        else
            dvStock = dtStock.DefaultView;
        gvStockRet.DataSource = dvStock;

        if (IsRebind)
            gvStockRet.DataBind();

        if (gvStockRet.Rows.Count > 0)
            tblBtn.Style.Add("display", "inline");
        else
            tblBtn.Style.Add("display", "none");
    }
    protected void gvStockRet_Sorting(object sender, GridViewSortEventArgs e)
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
    protected void gvStockRet_PageIndexChanged(object sender, GridViewPageEventArgs e)
    {
        gvStockRet.PageIndex = e.NewPageIndex;
        BindData(false, true);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {        
        SaveStatus("Approved");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {        
        SaveStatus("Rejected");
    }

    protected void SaveStatus(string strStatus)
    {
        string strResult = "";
        int intCnt = 0;
        try
        {
            objStock.prpStatus = strStatus;
            for (int i = 0; i < gvStockRet.Rows.Count; i++)
            {
                CheckBox chkStockRet = gvStockRet.Rows[i].FindControl("chkStockRet") as CheckBox;
                if (chkStockRet.Checked == true)
                {
                    intCnt += 1;
                    HiddenField hdnReturnId = gvStockRet.Rows[i].FindControl("hdnReturnId") as HiddenField;
                    HiddenField hdnItemId = gvStockRet.Rows[i].FindControl("hdnItemId") as HiddenField;

                    objStock.prpReturnId = Convert.ToInt32(hdnReturnId.Value.Trim());
                    objStock.prpItemId = Convert.ToInt32(hdnItemId.Value.Trim());
                    strResult = objStock.SaveStockReturnStatus();
                    if (strResult != "")
                    {
                        lblMessage.Text = strResult;
                        lblMessage.CssClass = "ErrorMessage";
                        return;
                    }
                }
            }

            if (intCnt < 1)
            {
                lblMessage.Text = "Please Select Atleast One Record.";
                lblMessage.CssClass = "ErrorMessage";
            }
            else
            {
                if (strResult == "")
                {
                    BindData(true, true);
                    lblMessage.Text = strStatus + " Successfully.";
                    lblMessage.CssClass = "SuccessMessageBold";
                }
                else
                {
                    lblMessage.Text = strResult;
                    lblMessage.CssClass = "ErrorMessage";
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
            lblMessage.CssClass = "ErrorMessage";
        }
    }
}
