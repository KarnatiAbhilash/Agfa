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
using BusinessClass;
using System.Net.Mail;
using System.IO;
using System.Text;

public partial class Transactions_StockReturnDlr : System.Web.UI.Page
{
    MonthEndClass objMonth = new MonthEndClass();
    StockReturnClass objStock = new StockReturnClass();
    CommonFunction objComm = new CommonFunction();
    CustomerMasterClass objCust = new CustomerMasterClass();

    protected override void LoadViewState(object earlierState)
    {
        base.LoadViewState(earlierState);
        if (ViewState["dynamictable"] == null)
            GenerateSearchModel(false);

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null) Response.Redirect("../Logout.aspx");
        try
        {
            if (!IsPostBack)
            {
                lblMessage.Text = "";
                lblMessage.CssClass = "";
                btnAdd.Enabled = false;
                btnSave.Enabled = false;

                objMonth.prpDealerCode = Convert.ToInt32(Session["DealerCode"].ToString());
                DataSet ds = objMonth.GetOpenMonth();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtMonth.Text = ds.Tables[0].Rows[0]["Month"].ToString();
                    txtYear.Text = ds.Tables[0].Rows[0]["Year"].ToString();
                }

                txtReturnDate.Text = DateTime.Today.ToString(Session["DateFormat"].ToString());
                calReturnDate.Format = Session["DateFormat"].ToString();
                CommonFunction.PopulateRecordsWithTwoParam("Common_Values", "Text", "Value", "FieldName", "POType", "Status", "1", "Text", ddlSalesType);

                string[] strMonthList ={"January","February","March","April","May","June","July", 
                "August",    "September",    "October","November","December" };
                ArrayList monthArrayList = new ArrayList(strMonthList);
                hdnIntMonth.Value = (monthArrayList.IndexOf(txtMonth.Text) + 1).ToString();
            }
            GenerateSearchModel(false);
            if (Request.Params["__EVENTTARGET"] == null)
            {
                ddlSalesType.SelectedIndex = 1;
                ddlSalesType_SelectedIndexChanged(ddlSalesType, null);
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
            lblMessage.CssClass = "ErrorMessage";
        }
    }

    protected void GenerateSearchModel(bool clearall)
    {

        if (clearall == true)
        {
            hidSearchCount.Value = "0";
            Table tbl = Page.FindControl("SearchTable") as Table;
            tbl.Controls.Clear();
        }

        int SearchCount = int.Parse(hidSearchCount.Value);

        AddHeader();
        int searchrow = 1;
        for (searchrow = 1; searchrow <= SearchCount; searchrow++)
        {
            CreateRow(searchrow);
        }
        ViewState["dynamictable"] = true;
    }

    protected void AddHeader()
    {
        TableRow tr = new TableRow();
        TableCell tc;
        SearchTable.Rows.Add(tr);
        tr.ID = "tr_0";
        tr.CssClass = "TblHeader";

        tc = new TableCell();
        tc.Text = "&nbsp";
        tc.HorizontalAlign = HorizontalAlign.Left;
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Text = "Sl.No.";
        tc.HorizontalAlign = HorizontalAlign.Left;
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Text = "Item Code";
        tc.HorizontalAlign = HorizontalAlign.Left;
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Text = "Description 1";
        tc.HorizontalAlign = HorizontalAlign.Left;
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Text = "Description 2";
        tc.HorizontalAlign = HorizontalAlign.Left;
        tr.Cells.Add(tc);

        //tc = new TableCell();
        //tc.Text = "Available Qty";
        //tc.HorizontalAlign = HorizontalAlign.Left;
        //tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Text = "Return Qty";
        tc.HorizontalAlign = HorizontalAlign.Left;
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Text = "Reason";
        tc.HorizontalAlign = HorizontalAlign.Left;
        tr.Cells.Add(tc);
    }

    protected void CreateRow(int SearchCount)
    {
        TableRow tr = new TableRow();
        TableCell tc;
        SearchTable.Rows.Add(tr);
        tr.ID = "tr_" + SearchCount.ToString();
        tr.Attributes.Add("runat", "server");

        tc = new TableCell();
        tr.Cells.Add(tc);
        tc.HorizontalAlign = HorizontalAlign.Left;
        Button btnDelete = new Button();
        btnDelete.CssClass = "btn deletebtn";
        btnDelete.Width = Unit.Pixel(15);
        btnDelete.ID = "btnDelete_" + SearchCount.ToString();
        btnDelete.Attributes.Add("OnClick", "return confirm('Are You Sure You Want To Delete The Record?')");
        btnDelete.Click += new EventHandler(btnDelete_Click);
        HiddenField hdnRow = new HiddenField();
        hdnRow.ID = "hdnRow_" + SearchCount.ToString();
        tc.Controls.Add(btnDelete);
        tc.Controls.Add(hdnRow);

        tc = new TableCell();
        tr.Cells.Add(tc);
        tc.HorizontalAlign = HorizontalAlign.Left;
        tc.Text = SearchCount.ToString();

        tc = new TableCell();
        tr.Cells.Add(tc);
        tc.HorizontalAlign = HorizontalAlign.Left;
        TextBox txtItemCode = new TextBox();
        txtItemCode.CssClass = "commonfont textdropwidthSmall";
        txtItemCode.ID = "txtItemCode_" + SearchCount.ToString();
        txtItemCode.AutoPostBack = true;
        txtItemCode.TextChanged += new EventHandler(txtItemCode_TextChanged);
        HiddenField hdnItemId = new HiddenField();
        hdnItemId.ID = "hdnItemId_" + SearchCount;
        HtmlButton btnItem = new HtmlButton();
        btnItem.Attributes.Add("runat", "server");
        btnItem.InnerText = "...";
        btnItem.ID = "btnItem_" + SearchCount.ToString();
        btnItem.Attributes.Add("onclick", "popupItem('StockRetDlr','" + SearchCount.ToString() + "','" + Session["DealerCode"].ToString() + "','" + ddlSalesType.SelectedValue + "')");
        btnItem.Attributes.Add("class", "popButton");
        tc.Controls.Add(txtItemCode);
        tc.Controls.Add(hdnItemId);
        tc.Controls.Add(btnItem);

        tc = new TableCell();
        tr.Cells.Add(tc);
        tc.HorizontalAlign = HorizontalAlign.Left;
        TextBox txtDesc1 = new TextBox();
        txtDesc1.Enabled = false;
        txtDesc1.CssClass = "commonfont textdropwidth";
        txtDesc1.ID = "txtDesc1_" + SearchCount.ToString();
        txtDesc1.TextMode = TextBoxMode.MultiLine;
        tc.Controls.Add(txtDesc1);

        tc = new TableCell();
        tr.Cells.Add(tc);
        tc.HorizontalAlign = HorizontalAlign.Left;
        TextBox txtDesc2 = new TextBox();
        txtDesc2.Enabled = false;
        txtDesc2.CssClass = "commonfont textdropwidth";
        txtDesc2.ID = "txtDesc2_" + SearchCount.ToString();
        txtDesc2.TextMode = TextBoxMode.MultiLine;
        tc.Controls.Add(txtDesc2);

        //tc = new TableCell();
        //tr.Cells.Add(tc);
        //tc.HorizontalAlign = HorizontalAlign.Left;
        //TextBox txtAvailQty = new TextBox();
        //txtAvailQty.CssClass = "commonfont textdropwidthUOM right";
        //txtAvailQty.ID = "txtAvailQty_" + SearchCount.ToString();
        //tc.Controls.Add(txtAvailQty);

        tc = new TableCell();
        tr.Cells.Add(tc);
        tc.HorizontalAlign = HorizontalAlign.Left;
        TextBox txtReturnQty = new TextBox();
        txtReturnQty.MaxLength = 6;
        txtReturnQty.CssClass = "commonfont textdropwidthUOM right";
        txtReturnQty.ID = "txtReturnQty_" + SearchCount.ToString();
        HiddenField hdnMaxQty = new HiddenField();
        hdnMaxQty.ID = "hdnMaxQty_" + SearchCount.ToString();
        tc.Controls.Add(txtReturnQty);
        tc.Controls.Add(hdnMaxQty);

        tc = new TableCell();
        tr.Cells.Add(tc);
        tc.HorizontalAlign = HorizontalAlign.Left;
        TextBox txtReason = new TextBox();
        txtReason.CssClass = "commonfont textdropwidth";
        txtReason.ID = "txtReason_" + SearchCount.ToString();
        txtReason.TextMode = TextBoxMode.MultiLine;
        tc.Controls.Add(txtReason);

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Table tbl = Page.FindControl("SearchTable") as Table;
        int index = 0;
        Button btn = sender as Button;

        string strID = btn.ID.Replace("btnDelete_", "");
        index = int.Parse(strID);
        int SearchCount = int.Parse(hidSearchCount.Value);
        HiddenField hdnRow = Page.FindControl("hdnRow_" + strID) as HiddenField;
        hdnRow.Value = "S";

        TableRow tr = Page.FindControl("tr_" + strID) as TableRow;
        //tr.Visible = false;
        tr.Style.Add("display", "none");

        lblMessage.Text = "Deleted Successfully.";
        lblMessage.CssClass = "SuccessMessageBold";
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        lblMessage.CssClass = "";
        Int32 intCount = Convert.ToInt32(hidSearchCount.Value.Trim()) + 1;
        CreateRow(intCount);
        hidSearchCount.Value = intCount.ToString();
    }

    private bool IsValidate()
    {
        Table tbl = Page.FindControl("SearchTable") as Table;

        int intMonth = Convert.ToInt16(hdnIntMonth.Value);
        if (intMonth != 0)
        {
            string[] sd = null;
            if (txtReturnDate.Text.Contains('/'))
                sd = txtReturnDate.Text.Split('/');
            else
                sd = txtReturnDate.Text.Split('-');
            if (int.Parse(sd[1]) != intMonth)
            {
                lblMessage.Text = "Enter the Invoice Date With in This Month";
                lblMessage.CssClass = "ErrorMessage";
                return false;
            }
            //string[] sd = txtReturnDate.Text.Split('/');
            //if (int.Parse(sd[1]) != intMonth)
            //if (int.Parse(this.txtReturnDate.Text.Split('/')[0]) != intMonth)
            //    {
            //    lblMessage.Text = "Enter the Return Date With in This Month";
            //    lblMessage.CssClass = "ErrorMessage";
            //    return false;
            //}
        }
        for (int icount = 1; icount < tbl.Rows.Count; icount++)
        {
            HiddenField hdnRow = Page.FindControl("hdnRow_" + icount) as HiddenField;
            TextBox txtItemCode = Page.FindControl("txtItemCode_" + icount) as TextBox;
            HiddenField hdnItemId = Page.FindControl("hdnItemId_" + icount) as HiddenField;
            TextBox txtQty = Page.FindControl("txtReturnQty_" + icount) as TextBox;
            HiddenField hdnMaxQty = Page.FindControl("hdnMaxQty_" + icount) as HiddenField;
            if (hdnRow.Value == "S")
                continue;

            int openStock = 0;
            int dlrStockRtn = 0;
            int custStockRtn = 0;
            int receiptQty = 0;
            int issueQty = 0;
            int intAvailQty = 0;
            int intMaxQty = 0;
            int intReturnQty = 0;
            int intPendingQty = 0;

            if (hdnMaxQty.Value.Trim() != "")
                intMaxQty = Convert.ToInt32(hdnMaxQty.Value.Trim());

            if (txtQty.Text.Trim() != "")
                intReturnQty = Convert.ToInt32(txtQty.Text.Trim());

            if (!BindItem(txtItemCode))
            {
                return false;
            }

            DataSet ds = CommonFunction.FetchRecordsWithQuery("select isnull(OpeningStock,0),isnull(DlrReturnStock,0),isnull(CustReturnStock,0) from DealerStock where DealerCode=" + Session["DealerCode"].ToString() + " and Month='" + txtMonth.Text.Trim() + "' and Year=" + txtYear.Text.Trim() + " and ItemId=" + hdnItemId.Value, "DealerCode");
            if (ds.Tables[0].Rows.Count > 0)
            {
                openStock = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                dlrStockRtn = Convert.ToInt32(ds.Tables[0].Rows[0][1].ToString());
                custStockRtn = Convert.ToInt32(ds.Tables[0].Rows[0][2].ToString());
            }
            ds = CommonFunction.FetchRecordsWithQuery("Select r2.dealercode,r1.itemid,ISNULL(SUM(qty),0) from ReceiptDetails r1 inner join Receipt r2 on r1.ReceiptNo=r2.ReceiptNo where r2.DealerCode=" + Session["DealerCode"].ToString() + " and r2.Month='" + txtMonth.Text.Trim() + "' and r2.Year=" + txtYear.Text.Trim() + " and r1.ItemId=" + hdnItemId.Value + " group by r2.Dealercode,r1.ItemId", "r2.DealerCode");
            if (ds.Tables[0].Rows.Count > 0)
                receiptQty = Convert.ToInt32(ds.Tables[0].Rows[0][2].ToString());
            ds = CommonFunction.FetchRecordsWithQuery("Select i2.dealercode,i1.itemid,ISNULL(SUM(qty),0) from IssueDetails i1 inner join Issue i2 on i1.IssueNo=i2.IssueNo where i2.DealerCode=" + Session["DealerCode"].ToString() + " and i2.Month='" + txtMonth.Text.Trim() + "' and i2.Year=" + txtYear.Text.Trim() + " and i1.ItemId=" + hdnItemId.Value + " group by i2.Dealercode,i1.ItemId", "i2.DealerCode");
            if (ds.Tables[0].Rows.Count > 0)
                issueQty = Convert.ToInt32(ds.Tables[0].Rows[0][2].ToString());
            ds = CommonFunction.FetchRecordsWithQuery("Select s2.dealercode,s1.itemid,ISNULL(SUM(returnqty),0) from StockReturnDetails s1 inner join StockReturn s2 on s1.ReturnID=s2.ReturnID where s2.DealerCode=" + Session["DealerCode"].ToString() + " and s2.Month='" + txtMonth.Text.Trim() + "' and s2.Year=" + txtYear.Text.Trim() + " and s1.ItemId=" + hdnItemId.Value + " and (s2.status is null or s2.status='pending') group by s2.Dealercode,s1.ItemId", "s2.DealerCode");
            if (ds.Tables[0].Rows.Count > 0)
                intPendingQty = Convert.ToInt32(ds.Tables[0].Rows[0][2].ToString());
            //else
            //{
            //    lblMessage.Text = "Item " + txtItemCode.Text.Trim() + " Is Not Mapped.Please Contact Agfa.";
            //    lblMessage.CssClass = "ErrorMessage";
            //    return false;
            //}

            intAvailQty = (openStock + receiptQty + custStockRtn) - (dlrStockRtn + issueQty);
            intReturnQty = intReturnQty + intPendingQty;

            if (intReturnQty > intAvailQty)
            {
                lblMessage.Text = "For Item " + txtItemCode.Text.Trim() + "Return Exceeds maximum Limit.Stock At Dealer : " + intAvailQty;
                lblMessage.CssClass = "ErrorMessage";
                return false;
            }



        }
        
        return true;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string strMsg = "";
        Table tbl = Page.FindControl("SearchTable") as Table;
        try
        {
            if (IsValidate())
            {
                objStock.prpUserId = Session["UserID"].ToString();
                objStock.prpDealerCode = Convert.ToInt32(Session["DealerCode"].ToString());
                objStock.prpUserType = "Dealer";
                objStock.prpCustCode = 0;
                objStock.prpSalesType = ddlSalesType.SelectedValue;
                objStock.prpInvoiceNo = txtInvoiceNo.Text.Trim();
                objStock.prpMonth = txtMonth.Text.Trim();
                objStock.prpYear = Convert.ToInt32(txtYear.Text.Trim());
                objStock.prpReturnDate = objComm.DateFormateConversion(txtReturnDate.Text, Session["DateFormat"].ToString());

                for (int icount = 1; icount < tbl.Rows.Count; icount++)
                {
                    HiddenField hdnRow = Page.FindControl("hdnRow_" + icount) as HiddenField;
                    TextBox txtItemCode = Page.FindControl("txtItemCode_" + icount) as TextBox;
                    HiddenField hdnItemId = Page.FindControl("hdnItemId_" + icount) as HiddenField;
                    TextBox txtReturnQty = Page.FindControl("txtReturnQty_" + icount) as TextBox;

                    objStock.prpItemId = Convert.ToInt32(hdnItemId.Value.Trim());
                    objStock.prpReturnQty = Convert.ToInt32(txtReturnQty.Text.Trim());
                    strMsg = objStock.ValidateStockReturn();
                    if (strMsg != "")
                    {
                        lblMessage.Text = strMsg;
                        lblMessage.CssClass = "ErrorMessage";
                        return;
                    }
                }


                strMsg = objStock.SaveStockReturn();
                if (strMsg == "")
                {
                    for (int icount = 1; icount < tbl.Rows.Count; icount++)
                    {
                        HiddenField hdnRow = Page.FindControl("hdnRow_" + icount) as HiddenField;
                        TextBox txtItemCode = Page.FindControl("txtItemCode_" + icount) as TextBox;
                        HiddenField hdnItemId = Page.FindControl("hdnItemId_" + icount) as HiddenField;
                        TextBox txtReturnQty = Page.FindControl("txtReturnQty_" + icount) as TextBox;
                        TextBox txtReason = Page.FindControl("txtReason_" + icount) as TextBox;

                        if (hdnRow.Value == "S")
                            continue;
                        if (BindItem(txtItemCode))
                        {
                            objStock.prpItemId = Convert.ToInt32(hdnItemId.Value.Trim());
                            objStock.prpReturnQty = Convert.ToInt32(txtReturnQty.Text.Trim());
                            objStock.prpReason = txtReason.Text.Trim();
                            strMsg = objStock.SaveStockReturnDetails();
                        }
                        else
                        {
                            return;
                            break;
                        }
                        if (strMsg != "")
                        {
                            lblMessage.Text = strMsg;
                            lblMessage.CssClass = "ErrorMessage";
                            return;
                        }
                    }
                    if (strMsg == "")
                    {
                        lblMessage.Text = "Saved Successfully.";
                        lblMessage.CssClass = "SuccessMessageBold";
                    }
                }
                else
                {
                    lblMessage.Text = strMsg;
                    lblMessage.CssClass = "ErrorMessage";
                }
                btnSave.Enabled = false;
                btnAdd.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
            lblMessage.CssClass = "ErrorMessage";
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("StockReturnDlr.aspx");
    }
    protected void ddlSalesType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSalesType.SelectedValue != "0")
        {
            btnAdd.Enabled = true;
            btnSave.Enabled = true;
        }
        else
        {
            btnAdd.Enabled = false;
            btnSave.Enabled = false;
        }
        GenerateSearchModel(true);
        lblMessage.Text = "";
        lblMessage.CssClass = "";
        btnAdd_Click(btnAdd, null);
    }

    protected bool BindItem(TextBox ItemCode)
    {
        string strSearchQuery = " where IM.Status=1 and Itemcode='" + ItemCode.Text.Trim() + "'";
        DataSet dsCust = objCust.FetchCustItems(strSearchQuery, ddlSalesType.Text);

        if (dsCust.Tables[0].Rows.Count > 0)
        {
            TextBox Desc1 = Page.FindControl("txtDesc1_" + ItemCode.ID.Substring(ItemCode.ID.IndexOf('_') + 1, ItemCode.ID.Length - (ItemCode.ID.IndexOf('_') + 1))) as TextBox;
            TextBox Desc2 = Page.FindControl("txtDesc2_" + ItemCode.ID.Substring(ItemCode.ID.IndexOf('_') + 1, ItemCode.ID.Length - (ItemCode.ID.IndexOf('_') + 1))) as TextBox;
            HiddenField hdnItemId = Page.FindControl("hdnItemId_" + ItemCode.ID.Substring(ItemCode.ID.IndexOf('_') + 1, ItemCode.ID.Length - (ItemCode.ID.IndexOf('_') + 1))) as HiddenField;
            HiddenField hdnMaxQty = Page.FindControl("hdnMaxQty_" + ItemCode.ID.Substring(ItemCode.ID.IndexOf('_') + 1, ItemCode.ID.Length - (ItemCode.ID.IndexOf('_') + 1))) as HiddenField;
            
            Desc1.Text = dsCust.Tables[0].Rows[0][3].ToString();
            Desc2.Text = dsCust.Tables[0].Rows[0][4].ToString();
            hdnItemId.Value = dsCust.Tables[0].Rows[0][1].ToString();
            hdnMaxQty.Value = dsCust.Tables[0].Rows[0]["MaxQty"].ToString();
            lblMessage.Text = "";
            lblMessage.CssClass = "";
            return true;

        }
        else
        {
            lblMessage.Text = "ItemCode Not Found.";
            lblMessage.CssClass = "ErrorMessage";
            return false;
        }
    }

    protected void txtItemCode_TextChanged(object sender, EventArgs e)
    {
        BindItem((TextBox)sender);
    }
}
