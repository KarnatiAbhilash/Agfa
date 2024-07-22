/* ==============================================================================================
    Created By      : Subahani SM
    Created Date    : 02 Sep 2010
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
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;

public partial class Transactions_Issue : System.Web.UI.Page
{
    IssueMaster objIssue = new IssueMaster();
    MonthEndClass objMonth = new MonthEndClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null) Response.Redirect("../Logout.aspx");
        try
        {
            //this.hdnViewPath.Value = ConfigurationManager.AppSettings["ViewDownloadpath"].ToString();
            this.hdnViewPath.Value = ConfigurationManager.AppSettings["issueViewDownloadpath"].ToString();
            if (!IsPostBack)
            {
                //btnAdd.Style.Add("display", "none");
                CommonFunction.PopulateRecordsWithTwoParam("Common_Values", "Text", "Value", "FieldName", "Region", "Status", "1", "Text", this.ddlRegion, "ALL");
                objMonth.prpDealerCode = Convert.ToInt32(Session["DealerCode"].ToString());
                DataSet ds = objMonth.GetOpenMonth();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtMonth.Text = ds.Tables[0].Rows[0]["Month"].ToString();
                    txtYear.Text = ds.Tables[0].Rows[0]["Year"].ToString();
                }
                //txtMonth.Text = DateTime.Now.ToString("MMMM");
                //txtYear.Text = DateTime.Now.ToString("yyyy");
                gvIssue.PageSize = Convert.ToInt32(Session["PageSize"].ToString());
                BindData(true, true,true);
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
    protected void cmdView_Click(object sender, EventArgs e)
    {
        this.Response.Write("<h2> File name selected = " + ((TableRow)((Control)sender).Parent.Parent).Cells[2].Text + "</h2>");
    }
    protected void btnAdd_New(object sender, EventArgs e)
    {
        Response.Redirect("IssueAddEdit.aspx");
    }
    protected void BindData(bool isFromDB, bool IsRebind, bool issearch)
    {
        DataTable dtIssue;
        DataView dvIssue;

        if (isFromDB)
        {
            string[] files = Directory.GetFiles(this.Server.MapPath("~/IssueFiles/"));
            List<ListItem> listItemList = new List<ListItem>();
            foreach (string path in files)
            listItemList.Add(new ListItem(Path.GetFileName(path), path));
            this.gvIssue.DataSource = (object)listItemList;
            objIssue.prpDealerCode = Convert.ToInt32(Session["DealerCode"].ToString());
            //this.objIssue.prpRegion = this.ddlRegion.SelectedItem.Value;
            if (!string.IsNullOrEmpty(this.hdnCustcode.Value))
                this.objIssue.prpCustCode = Convert.ToInt32(this.hdnCustcode.Value.Trim());
            if (Session["UserType"].ToString() == "Approver")
            {
                dtIssue = objIssue.FetchApproverIssueList().Tables[0];
            }
            else
            {
                dtIssue = objIssue.FetchIssueList().Tables[0];

            }


            if (Cache.Get("Issue" + Convert.ToString(Session["UserId"])) != null)
                Cache.Remove("Issue" + Convert.ToString(Session["UserId"]));

            Cache.Insert("Issue" + Convert.ToString(Session["UserId"]), dtIssue, null, DateTime.MaxValue, TimeSpan.FromMinutes(30), CacheItemPriority.Normal, null);

        }
        else
        {
            dtIssue = (DataTable)Cache.Get("Issue" + Convert.ToString(Session["UserId"]));
        }
        

        dvIssue = new DataView();
        if (ViewState["sortExpr"] != null)
        {
            dvIssue = new DataView(dtIssue);
            dvIssue.Sort = (string)ViewState["sortExpr"];
        }
        else
            dvIssue = dtIssue.DefaultView;
        gvIssue.DataSource = dvIssue;
        if (!IsRebind)
            return;
         gvIssue.DataBind();
    }

    protected void gvIssue_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        BindData(true, true,true);
    }

    protected void gvIssue_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (ViewState["sortExpr"] == null || ViewState["sortExpr"].ToString().Contains("ASC"))
        {
            ViewState["sortExpr"] = e.SortExpression + " " + "DESC";
        }
        else
        {
            ViewState["sortExpr"] = e.SortExpression + " " + "ASC";
        }

        BindData(false, true,true);
    }

    protected void gvIssue_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string str;
        if ((str = e.CommandName.Trim()) == null)
            return;
        int num = str == "Delete" ? 1 : 0;
    }


    protected void gvIssue_PageIndexChanged(object sender, GridViewPageEventArgs e)
    {
        gvIssue.PageIndex = e.NewPageIndex;
        BindData(false, true,true);
    }

    //protected void gvIssue_DataBound(object sender, GridViewi  e)
    //{
    //    if ((e.Item.ItemType == ListItemType.Item) ||
    //         (e.Item.ItemType == ListItemType.AlternatingItem))
    //    {

    //    }
    //}
    protected void gvIssue_RowDataBound1(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Attributes.Add("onClick", "DelIssue('" + e.Row.RowIndex + "')");

            Button btnVerify = e.Row.FindControl("btnverify") as Button;
            string status = e.Row.Cells[6].Text;
            Button Row_btnVerify = e.Row.FindControl("btnverify") as Button;
            Row_btnVerify.Visible = false;
            btnApproved.Visible = false;
            if (Session["UserType"].ToString() == "Approver")
            {
                btnApproved.Visible = true;
                if (status != "Verified" && status != "Approved")
                {
                    Row_btnVerify.Visible = true;
                    e.Row.Cells[8].Attributes.Add("onClick", "VerifyIssue('" + e.Row.RowIndex + "')");
                }
            }

        }
    }



    protected void btnDummy_Click(object sender, EventArgs e)
    {
        //string EventArg = this.Request["__EVENTARGUMENT"].ToString();
        //string ctrlArg = Page.Request.Params.Get("__EVENTARGUMENT");

        int Index = Int32.Parse(hdnDummy.Value);
        GridViewRow row = gvIssue.Rows[Index];
        objIssue.prpIssueNo = Int32.Parse(row.Cells[1].Text.ToString());
        string strError = "";
        try
        {
            DataSet ds = objIssue.FetchIssue();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Int32 ItemId = Int32.Parse(ds.Tables[0].Rows[i]["ItemId"].ToString());
                objIssue.prpItemId = ItemId;
                objIssue.prpIssueNo = Int32.Parse(row.Cells[1].Text.ToString());
                strError = objIssue.DeleteIssueDetails();
            }
            objIssue.prpIssueNo = Int32.Parse(row.Cells[1].Text.ToString());
            strError = objIssue.DeleteIssue();
            if (strError == "")
            {
                row.Style.Add("Display", "None");
                lblMessage.CssClass = "SuccessMessageBold";
                lblMessage.Text = "Deleted Sucessfully.";
            }
            else
            {
                lblMessage.CssClass = "ErrorMessage";
                lblMessage.Text = strError;
            }
            BindData(true, true, true);
        }
        catch (Exception Ex)
        {
            lblMessage.CssClass = "ErrorMessage";
            lblMessage.Text = strError;
        }
    }
    protected void DownloadFile(object sender, EventArgs e)
    {
        string text = this.gvIssue.Rows[((GridViewRow)(sender as Control).NamingContainer).RowIndex].Cells[7].Text;
        byte[] buffer = new WebClient().DownloadData(this.Server.MapPath(ConfigurationManager.AppSettings["IssueFilesFolder"].ToString() + text));
        if (buffer == null)
            return;
        this.Response.ContentType = "application/pdf";
        this.Response.AddHeader("content-length", buffer.Length.ToString());
        this.Response.BinaryWrite(buffer);
        this.Response.Flush();
        this.Response.End();
    }
   
        
    protected void SaveStatus(string strStatus,int IssueNo )
    {
        string str = "";
        int num = 0;
        try
        {
            this.objIssue.prpStatus = strStatus;
            ++num;
            this.objIssue.prpIssueNo = IssueNo;
            str = this.objIssue.SaveIssueVerify();
            if (str != "")
            {
                this.lblMessage.Text = str;
                this.lblMessage.CssClass = "ErrorMessage";
                return;
            }
            
            //for (int index = 0; index < this.gvIssue.Rows.Count; ++index)
            //{
            //    ++num;
            //    this.objIssue.prpIssueNo = Convert.ToInt32((this.gvIssue.Rows[index].FindControl("hdnIssueNo") as HiddenField).Value.Trim());
            //    str = this.objIssue.SaveIssueVerify();
            //    if (str != "")
            //    {
            //        this.lblMessage.Text = str;
            //        this.lblMessage.CssClass = "ErrorMessage";
            //        return;
            //    }
            //    this.BindData(true, true, true);
            //}
            if (num < 1)
            {
                this.lblMessage.Text = "Please Select Atleast One Record.";
                this.lblMessage.CssClass = "ErrorMessage";
            }
            else if (str == "")
            {
                this.BindData(true, true, true);
                this.lblMessage.Text = strStatus + " Successfully.";
                this.lblMessage.CssClass = "SuccessMessageBold";
            }
            else
            {
                this.lblMessage.Text = str;
                this.lblMessage.CssClass = "ErrorMessage";
            }
        }
        catch (Exception ex)
        {
            this.lblMessage.Text = ex.Message;
            this.lblMessage.CssClass = "ErrorMessage";
        }
    }

    protected void btnApproved_Click(object sender, EventArgs e)
    {
        int Index = Int32.Parse(hdnDummy.Value);
        GridViewRow row = gvIssue.Rows[Index];       
        int issueNo = Int32.Parse(row.Cells[1].Text.ToString());
        this.SaveStatus("Verified", issueNo);
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        this.lblMessage.Text = "";
        this.lblMessage.CssClass = "";
        this.ddlRegion.SelectedIndex = 0;
        this.txtCustCode.Text = "";
        this.hdnCustcode.Value = "";
        this.BindData(true, true, true);
    }

    protected void search_click(object sender, EventArgs e)
    {
        this.BindData(true, true, true);
    }
    
}
