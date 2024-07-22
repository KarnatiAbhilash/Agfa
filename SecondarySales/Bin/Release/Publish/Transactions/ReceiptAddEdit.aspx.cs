/* ==============================================================================================
    Created By      : Subahani SM
    Created Date    : 25 Aug 2010
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

public partial class Transactions_ReceiptAddEdit : System.Web.UI.Page
{
    ReceiptMaster objReceipt = new ReceiptMaster();
    CommonFunction objComm = new CommonFunction();
    CustomerMasterClass objCust = new CustomerMasterClass();
    MonthEndClass objMonth = new MonthEndClass();
    DealerMasterClass objDlr = new DealerMasterClass();

    private DataSet dsItem;

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
                //CommonFunction.PopulateRecordsWithTwoParam("Common_Values", "Text", "Value", "FieldName", "POType", "Status", "1", "Text", ddlPOType);
                objMonth.prpDealerCode = Convert.ToInt32(Session["DealerCode"].ToString());
                DataSet ds = objMonth.GetOpenMonth();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtMonth.Text = ds.Tables[0].Rows[0]["Month"].ToString();
                    txtYear.Text = ds.Tables[0].Rows[0]["Year"].ToString();
                }
                txtInvoiceDate.Text = DateTime.Today.ToString(Session["DateFormat"].ToString());
                txtPONo.Text = Request.QueryString["PONo"].ToString();

                calInvoiceDate.Format = Session["DateFormat"].ToString();
                ViewState["InvoiceDate"] = txtInvoiceDate.Text;
                if (Request.QueryString["ReceiptNo"].ToString() != "" && Request.QueryString["ReceiptNo"].ToString() != "0")
                {
                    txtInvoiceNo.ReadOnly = true;
                }

                txtSalesTax.Attributes.Add("onBlur", "CalcGrossAmount()");
                txtLocallevi.Attributes.Add("onBlur", "CalcGrossAmount()");
                txtInsurence.Attributes.Add("onBlur", "CalcGrossAmount()");
                txtOthers.Attributes.Add("onBlur", "CalcGrossAmount()");

                string[] strMonthList ={"January","February","March","April","May","June","July", 
                "August",    "September",    "October","November","December" };
                ArrayList monthArrayList = new ArrayList(strMonthList);
                hdnIntMonth.Value = (monthArrayList.IndexOf(txtMonth.Text) + 1).ToString();
            }
            GenerateSearchModel(false);
            if (Request.Params["__EVENTTARGET"] == null)
                btnAdd_Click(btnAdd, null);

            if (!IsPostBack && Request.QueryString["ReceiptNo"] != null)
                BindData();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
            lblMessage.CssClass = "ErrorMessage";
        }

    }

    protected void BindData()
    {
        Table tbl = Page.FindControl("SearchTable") as Table;
        tbl.Controls.Clear();
        AddHeader();
        objReceipt.prpReceiptNo = Convert.ToInt32(Request.QueryString["ReceiptNo"].ToString());
        objReceipt.prpPONo = Request.QueryString["PONo"].ToString(); 
        dsItem = objReceipt.FetchReceiptPO();
        for (int bindparse = 0; bindparse < dsItem.Tables[0].Rows.Count; bindparse++)
            CreateRow(bindparse + 1);

        hidSearchCount.Value = Convert.ToString(dsItem.Tables[0].Rows.Count);

        for ( int bindparse = 0; bindparse < dsItem.Tables[0].Rows.Count; bindparse++)
        {
            int icount = bindparse + 1;
            //Button btnDelete = new Button();
            //btnDelete.CssClass = "btn deletebtn";
            //btnDelete.Width = Unit.Pixel(15);
            TextBox txtItemCode = Page.FindControl("txtItemCode_" + icount) as TextBox;
            HiddenField hdnItemId = Page.FindControl("hdnItemId_" + icount) as HiddenField;
            TextBox txtQty = Page.FindControl("txtQty_" + icount) as TextBox;
            HtmlInputText txtUnitPrice = Page.FindControl("txtUnitPrice_" + icount) as HtmlInputText;
            HtmlInputText txtInvoiceAmt = Page.FindControl("txtInvoiceAmt_" + icount) as HtmlInputText;
            TextBox txtDesc1 = Page.FindControl("txtDesc1_" + icount) as TextBox;
            TextBox txtDesc2 = Page.FindControl("txtDesc2_" + icount) as TextBox;
            //Button btnDelete = Page.FindControl("btnDelete_" + icount) as Button;
            HiddenField hdnQty = Page.FindControl("hdnQty_" + icount) as HiddenField;

            txtItemCode.Text = dsItem.Tables[0].Rows[bindparse]["ItemCode"].ToString();
            hdnItemId.Value = dsItem.Tables[0].Rows[bindparse]["ItemId"].ToString();
            txtDesc1.Text = dsItem.Tables[0].Rows[bindparse]["Description1"].ToString();
            txtDesc2.Text = dsItem.Tables[0].Rows[bindparse]["Description2"].ToString();
            txtQty.Text = dsItem.Tables[0].Rows[bindparse]["Qty"].ToString();
            hdnQty.Value = txtQty.Text;
            txtUnitPrice.Value = dsItem.Tables[0].Rows[bindparse]["UnitPrice"].ToString();
            txtInvoiceAmt.Value = dsItem.Tables[0].Rows[bindparse]["InvoiceAmt"].ToString();
            txtPONo.Text = dsItem.Tables[0].Rows[bindparse]["PONo"].ToString();
            //txtMonth.Text = dsItem.Tables[0].Rows[bindparse]["Month"].ToString();
            //txtYear.Text = dsItem.Tables[0].Rows[bindparse]["Year"].ToString();
            txtInvoiceNo.Text = dsItem.Tables[0].Rows[bindparse]["InvoiceNo"].ToString();
            if (dsItem.Tables[0].Rows[bindparse]["InvoiceDate"].ToString() != "")
            {
                txtInvoiceDate.Text = Convert.ToDateTime(dsItem.Tables[0].Rows[bindparse]["InvoiceDate"].ToString()).ToString(Session["DateFormat"].ToString());
                ViewState["InvoiceDate"] = txtInvoiceDate.Text;
            }

            txtSalesTax.Text = dsItem.Tables[0].Rows[bindparse]["SalesTax"].ToString();
            if (dsItem.Tables[0].Rows[bindparse]["LocalLevi"].ToString() != "" && dsItem.Tables[0].Rows[bindparse]["LocalLevi"].ToString() != "0.000")
                txtLocallevi.Text = dsItem.Tables[0].Rows[bindparse]["LocalLevi"].ToString();
            txtNetInvoiceAmt.Value = dsItem.Tables[0].Rows[bindparse]["NetInvoiceAmt"].ToString();
            if (dsItem.Tables[0].Rows[bindparse]["Insurance"].ToString() != "" && dsItem.Tables[0].Rows[bindparse]["Insurance"].ToString() != "0.000")
                txtInsurence.Text = dsItem.Tables[0].Rows[bindparse]["Insurance"].ToString();
            if (dsItem.Tables[0].Rows[bindparse]["Others"].ToString() != "" && dsItem.Tables[0].Rows[bindparse]["Others"].ToString() != "0.000")
                txtOthers.Text = dsItem.Tables[0].Rows[bindparse]["Others"].ToString();
            txtGrossInvoice.Value = dsItem.Tables[0].Rows[bindparse]["GrossInvoiceAmt"].ToString();
            txtRemarks.Text = dsItem.Tables[0].Rows[bindparse]["Remarks"].ToString();

            //btnDelete.Style.Add("display", "none");
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

        tc = new TableCell();
        tc.Text = "Qty";
        tc.HorizontalAlign = HorizontalAlign.Left;
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Text = "Unit Price";
        tc.HorizontalAlign = HorizontalAlign.Left;
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Text = "Invoice Amt.";
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
        txtItemCode.AutoPostBack = true;
        txtItemCode.TextChanged += new EventHandler(txtItemCode_TextChanged);
        txtItemCode.CssClass = "commonfont textdropwidthSmall";
        txtItemCode.ID = "txtItemCode_" + SearchCount.ToString();
        txtItemCode.Attributes.Add("onBlur", "CalcInvoiceAmt('" + SearchCount + "');CalcNetInvoiceAmt('" + SearchCount + "')");
        txtItemCode.Attributes.Add("OnChange", "$get('hdnCurrentItemId').value=this.id;fnCheckDuplicate(this.value);");

        HiddenField hdnItemId = new HiddenField();
        hdnItemId.ID = "hdnItemId_" + SearchCount;
        HtmlButton btnItem = new HtmlButton();
        btnItem.Attributes.Add("runat", "server");
        btnItem.InnerText = "...";
        btnItem.ID = "btnItem_" + SearchCount.ToString();
        btnItem.Attributes.Add("onclick", "popupItem('PO','" + SearchCount.ToString() + "','" + Session["DealerCode"].ToString() + "','" + Request.QueryString["strType"].ToString() + "')");
        btnItem.Attributes.Add("class", "popButton");
        tc.Controls.Add(txtItemCode);
        tc.Controls.Add(hdnItemId);
        tc.Controls.Add(btnItem);

        tc = new TableCell();
        tr.Cells.Add(tc);
        tc.HorizontalAlign = HorizontalAlign.Left;
        TextBox txtDesc1 = new TextBox();
        txtDesc1.ReadOnly = true;
        txtDesc1.CssClass = "commonfont textdropwidth";
        txtDesc1.ID = "txtDesc1_" + SearchCount.ToString();
        txtDesc1.TextMode = TextBoxMode.MultiLine;
        tc.Controls.Add(txtDesc1);

        tc = new TableCell();
        tr.Cells.Add(tc);
        tc.HorizontalAlign = HorizontalAlign.Left;
        TextBox txtDesc2 = new TextBox();
        txtDesc2.ReadOnly = true;
        txtDesc2.CssClass = "commonfont textdropwidth";
        txtDesc2.ID = "txtDesc2_" + SearchCount.ToString();
        txtDesc2.TextMode = TextBoxMode.MultiLine;
        tc.Controls.Add(txtDesc2);

        tc = new TableCell();
        tr.Cells.Add(tc);
        tc.HorizontalAlign = HorizontalAlign.Left;
        TextBox txtQty = new TextBox();
        txtQty.MaxLength = 6;
        txtQty.CssClass = "commonfont textdropwidthUOM right";
        txtQty.ID = "txtQty_" + SearchCount.ToString();
        HiddenField hdnQty = new HiddenField();
        hdnQty.ID = "hdnQty_" + SearchCount.ToString();
        txtQty.Attributes.Add("onBlur", "CalcInvoiceAmt('" + SearchCount + "');CalcNetInvoiceAmt('" + SearchCount + "')");
        tc.Controls.Add(txtQty);
        tc.Controls.Add(hdnQty);

        //tc = new TableCell();
        //tr.Cells.Add(tc);
        //tc.HorizontalAlign = HorizontalAlign.Left;
        //TextBox txtUnitPrice = new TextBox();
        //txtUnitPrice.CssClass = "commonfont textdropwidthUOM right";
        //txtUnitPrice.ID = "txtUnitPrice_" + SearchCount.ToString();     
        //tc.Controls.Add(txtUnitPrice);

        tc = new TableCell();
        tc.HorizontalAlign = HorizontalAlign.Left;
        tr.Cells.Add(tc);
        HtmlInputText txtUnitPrice = new HtmlInputText();
        txtUnitPrice.Attributes.Add("style", "commonfont textdropwidth right");
        txtUnitPrice.Attributes.Add("runat", "server");
        txtUnitPrice.Attributes.Add("readonly", "readonly");
        txtUnitPrice.Style.Add("width", "80px");
        txtUnitPrice.ID = "txtUnitPrice_" + SearchCount.ToString();
        tc.Controls.Add(txtUnitPrice);

        //tc = new TableCell();
        //tr.Cells.Add(tc);
        //tc.HorizontalAlign = HorizontalAlign.Left;
        //TextBox txtInvoiceAmt = new TextBox();
        //txtInvoiceAmt.CssClass = "commonfont textdropwidthSmall right";
        //txtInvoiceAmt.ID = "txtInvoiceAmt_" + SearchCount.ToString();
        //tc.Controls.Add(txtInvoiceAmt);

        tc = new TableCell();
        tc.HorizontalAlign = HorizontalAlign.Left;
        tr.Cells.Add(tc);
        HtmlInputText txtInvoiceAmt = new HtmlInputText();
        txtInvoiceAmt.Attributes.Add("style", "commonfont textdropwidth right");
        txtInvoiceAmt.Attributes.Add("runat", "server");
        txtInvoiceAmt.Attributes.Add("readonly", "readonly");
        txtInvoiceAmt.Style.Add("width", "120px");
        txtInvoiceAmt.ID = "txtInvoiceAmt_" + SearchCount.ToString();
        txtInvoiceAmt.Attributes.Add("onBlur", "CalcNetInvoiceAmt('" + SearchCount + "')");
        tc.Controls.Add(txtInvoiceAmt);


    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        ReceiptMaster objReceipt = new ReceiptMaster();
        Table tbl = Page.FindControl("SearchTable") as Table;
        int index = 0;
        Button btn = sender as Button;

        string strID = btn.ID.Replace("btnDelete_", "");
        index = int.Parse(strID);
        int SearchCount = int.Parse(hidSearchCount.Value);
        HiddenField hdnRow = Page.FindControl("hdnRow_" + strID) as HiddenField;
        hdnRow.Value = "S";
        TextBox txtItemCode = (TextBox)Page.FindControl("txtItemCode_" + strID);
        string strItemCode = txtItemCode.Text;
        TableRow tr = Page.FindControl("tr_" + strID) as TableRow;

        objReceipt.prpPONo = Request.QueryString["PONo"].ToString().Trim();
        objReceipt.prpUserId = Session["UserID"].ToString();
        string strMessage = objReceipt.DeletePartialPO(strItemCode);
        if (strMessage.StartsWith("D"))
        //string strMessage=objReceipt.dele
        //tr.Visible = false;
        {
            tr.Style.Add("display", "none");

            lblMessage.Text = "Deleted Successfully.";
            lblMessage.CssClass = "SuccessMessageBold";
        }
        else
        {
            lblMessage.Text = "Can Not be Deleted as Receipt Exists For this PO.";
            lblMessage.CssClass = "ErrorMessage";
        }
    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        lblMessage.CssClass = "";
        Int32 intCount = Convert.ToInt32(hidSearchCount.Value.Trim()) + 1;
        CreateRow(intCount);
        hidSearchCount.Value = intCount.ToString();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string strMsg = "";
        Int32 intReceiptNo = 0;
        Table tbl = Page.FindControl("SearchTable") as Table;
        objReceipt.prpMonth = txtMonth.Text.Trim();
        objReceipt.prpYear = Convert.ToInt32(txtYear.Text.Trim());
        try
        {
            if (IsValidate())
            {
                if (Request.QueryString["ReceiptNo"].ToString() == "" || Request.QueryString["ReceiptNo"].ToString() == "0")
                    intReceiptNo = 0;
                else
                    intReceiptNo = Convert.ToInt32(Request.QueryString["ReceiptNo"].ToString());

                objReceipt.prpReceiptNo = intReceiptNo;
                objReceipt.prpUserId = Session["UserID"].ToString();
                objReceipt.prpDealerCode = Convert.ToInt32(Session["DealerCode"].ToString());
                objReceipt.prpPONo = txtPONo.Text.Trim();
                objReceipt.prpInvoiceNo = txtInvoiceNo.Text.Trim();
                objReceipt.prpInvoiceDate = objComm.DateFormateConversion(txtInvoiceDate.Text.Trim(), Session["DateFormat"].ToString());
                objReceipt.prpSalesTax = Convert.ToDouble(txtSalesTax.Text.Trim());
                if (txtLocallevi.Text.Trim() != "")
                    objReceipt.prpLocalLevi = Convert.ToDouble(txtLocallevi.Text.Trim());
                else
                    objReceipt.prpLocalLevi = 0;
                if (txtInsurence.Text.Trim() != "")
                    objReceipt.prpInsurance = Convert.ToDouble(txtInsurence.Text.Trim());
                else
                    objReceipt.prpInsurance = 0;
                if (txtOthers.Text.Trim() != "")
                    objReceipt.prpOthers = Convert.ToDouble(txtOthers.Text.Trim());
                else
                    objReceipt.prpOthers = 0;
                objReceipt.prpNetInvoiceAmt = Convert.ToDouble(txtNetInvoiceAmt.Value.Trim());
                objReceipt.prpGrossInvoiceAmt = Convert.ToDouble(txtGrossInvoice.Value.Trim());
                objReceipt.prpRemarks = txtRemarks.Text.Trim();
                strMsg = objReceipt.SaveReceipt();

                if (strMsg == "")
                {
                    for (int icount = 1; icount < tbl.Rows.Count; icount++)
                    {
                        HiddenField hdnRow = Page.FindControl("hdnRow_" + icount) as HiddenField;
                        TextBox txtItemCode = Page.FindControl("txtItemCode_" + icount) as TextBox;
                        HiddenField hdnItemId = Page.FindControl("hdnItemId_" + icount) as HiddenField;
                        TextBox txtQty = Page.FindControl("txtQty_" + icount) as TextBox;
                        HtmlInputText txtUnitPrice = Page.FindControl("txtUnitPrice_" + icount) as HtmlInputText;
                        HiddenField hdnQty = Page.FindControl("hdnQty_" + icount) as HiddenField;
                        hdnQty.Value = txtQty.Text.Trim();
                        if (BindItem(txtItemCode))
                        {
                            if (hdnRow.Value == "S")
                                continue;

                            objReceipt.prpItemId = Convert.ToInt32(hdnItemId.Value.Trim());
                            objReceipt.prpQty = Convert.ToInt32(txtQty.Text.Trim());
                            if (hdnQty.Value.Trim() != "")
                                objReceipt.prpPrevQty = Convert.ToInt32(hdnQty.Value.Trim());
                            else
                                objReceipt.prpPrevQty = 0;
                            objReceipt.prpUnitPrice = Convert.ToDouble(txtUnitPrice.Value.Trim());
                            strMsg = objReceipt.SaveReceiptDetails();

                            if (strMsg != "")
                            {
                                lblMessage.Text = strMsg;
                                lblMessage.CssClass = "ErrorMessage";
                                return;
                            }
                        }
                    }
                    if (strMsg == "")
                    {
                        lblMessage.Text = "Updated Successfully.";
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
        Response.Redirect("ReceiptAddEdit.aspx?PONo=" + txtPONo.Text.Trim() + "&ReceiptNo=" + Request.QueryString["ReceiptNo"].ToString() + "&strType=" + Request.QueryString["strType"].ToString());
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Receipt.aspx");
    }

    protected Boolean IsValidate()
    {
        Table tbl = Page.FindControl("SearchTable") as Table;
        for (int icount = 1; icount < tbl.Rows.Count; icount++)
        {
            HiddenField hdnRow = Page.FindControl("hdnRow_" + icount) as HiddenField;
            TextBox txtItemCode = Page.FindControl("txtItemCode_" + icount) as TextBox;
            HiddenField hdnItemId = Page.FindControl("hdnItemId_" + icount) as HiddenField;
            TextBox txtQty = Page.FindControl("txtQty_" + icount) as TextBox;
            if (hdnRow.Value == "S")
                continue;

            int maxQty = 0;
            int openStock = 0;
            int recQty = 0;
            int issueQty = 0;
            int dlrStockRtnQty = 0;
            int custStockRtnQty = 0;
            int intAvailQty = 0;
            int intPOQty = 0;
            int intPOTotal = 0;
            int ReceiptQty = Convert.ToInt32(txtQty.Text.Trim());

            if (!BindItem(txtItemCode))
            {
                return false;
            }
            
            DataSet ds = new DataSet();
            ds = CommonFunction.FetchRecordsWithQuery("select isnull(MaxQty,0) from DealerPrice where DealerCode='" + Session["DealerCode"] + "' and Item_Id=" + hdnItemId.Value, "' '");
            if (ds.Tables[0].Rows.Count > 0)
                maxQty = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            ds = CommonFunction.FetchRecordsWithQuery("Select OpeningStock,DlrReturnStock,CustReturnStock from DealerStock where DealerCode='" + Session["DealerCode"] + "' and ItemId=" + hdnItemId.Value + " and Year=" + objReceipt.prpYear + " and Month='" + objReceipt.prpMonth + "'", "DealerCode");
            if (ds.Tables[0].Rows.Count > 0)
            {
                openStock = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                dlrStockRtnQty = Convert.ToInt32(ds.Tables[0].Rows[0][1].ToString());
                custStockRtnQty = Convert.ToInt32(ds.Tables[0].Rows[0][2].ToString());
            }   

            if (Request.QueryString["ReceiptNo"].ToString() == "" || Request.QueryString["ReceiptNo"].ToString() == "0")
            {
                ds = CommonFunction.FetchRecordsWithQuery("select r2.dealercode,isnull(sum(isnull(Qty,0)),0) from ReceiptDetails r1 inner join receipt r2 on r1.receiptno=r2.receiptno where r1.itemid=" + hdnItemId.Value + " and r2.DealerCode=" + Session["DealerCode"] + " and r2.Year=" + objReceipt.prpYear + " and r2.Month='" + objReceipt.prpMonth + "' group by r2.dealercode", "r2.DealerCode");
                if (ds.Tables[0].Rows.Count > 0)
                    recQty = Convert.ToInt32(ds.Tables[0].Rows[0][1].ToString());
            }
            else
            {
                ds = CommonFunction.FetchRecordsWithQuery("select r2.dealercode,isnull(sum(isnull(Qty,0)),0) from ReceiptDetails r1 inner join receipt r2 on r1.receiptno=r2.receiptno where r1.itemid=" + hdnItemId.Value + " and r2.DealerCode=" + Session["DealerCode"] + " and r1.ReceiptNo not in(Select ReceiptNo from Receipt where PONo='" + txtPONo.Text.Trim() + "') and r2.Year=" + objReceipt.prpYear + " and r2.Month='" + objReceipt.prpMonth + "' group by r2.dealercode", "r2.DealerCode");
                if (ds.Tables[0].Rows.Count > 0)
                    recQty = Convert.ToInt32(ds.Tables[0].Rows[0][1].ToString());
            }

            ds = CommonFunction.FetchRecordsWithQuery("select i2.dealercode,isnull(sum(isnull(Qty,0)),0) from IssueDetails i1 inner join Issue i2 on i1.issueno=i2.issueno where i1.itemid=" + hdnItemId.Value + " and i2.DealerCode=" + Session["DealerCode"] + " and i2.Year=" + objReceipt.prpYear + "and i2.Month='" + objReceipt.prpMonth + "' group by i2.dealercode", "i2.DealerCode");
            if (ds.Tables[0].Rows.Count > 0)
                issueQty = Convert.ToInt32(ds.Tables[0].Rows[0][1].ToString());

            intAvailQty = maxQty - ((openStock + recQty + custStockRtnQty) - (issueQty + dlrStockRtnQty));

            ds = CommonFunction.FetchRecordsWithQuery("select isnull(Qty,0),isnull(RemainingQty,IsNull(Qty,0)) from PurchaseOrderDetails where PONo='" + txtPONo.Text.Trim() + "' and ItemId=" + hdnItemId.Value, "Qty");
            if (ds.Tables[0].Rows.Count > 0)
            {
                intPOTotal = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                intPOQty = Convert.ToInt32(ds.Tables[0].Rows[0][1].ToString());
            }
            else
            {
                lblMessage.Text = "Item " + txtItemCode.Text.Trim() + " Is Not Mapped.Please Contact Agfa.";
                lblMessage.CssClass = "ErrorMessage";
                return false;
            }
            
            if (ReceiptQty > intPOTotal)
            {
                lblMessage.Text = "For Item " + txtItemCode.Text.Trim() + " Maximum Allowed Quantity Is " + intPOQty;
                lblMessage.CssClass = "ErrorMessage";
                return false;
            }
            else if (ReceiptQty > intPOQty && ReceiptQty != intPOTotal)
            {
                lblMessage.Text = "For Item " + txtItemCode.Text.Trim() + " Maximum Allowed Quantity Is " + intPOQty;
                lblMessage.CssClass = "ErrorMessage";
                return false;
            }
            else
                if (intAvailQty < ReceiptQty)
            {       lblMessage.Text = "For Item " + txtItemCode.Text.Trim() + " Available Quantity Is " + intAvailQty;
                    lblMessage.CssClass = "ErrorMessage";
                    return false;
            }
        }

        objReceipt.prpDealerCode = Convert.ToInt32(Session["DealerCode"].ToString());
        DataRow[] dr = objReceipt.FetchPO().Tables[0].Select("PONo='" + Request.QueryString["PONo"].ToString() + "'");
        //DateTime dt = DateTime.ParseExact(dr[0].ItemArray[3].ToString(), "dd/mm/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //DateTime dt = DateTime.ParseExact(string.Format("DD/MM/YYYY", dr[0].ItemArray[3].ToString()), "dd/mm/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //DateTime DT1 = DateTime.ParseExact(txtInvoiceDate.Text,"dd/MM/yyyy",null);
        DateTime DT1 = DateTime.ParseExact(txtInvoiceDate.Text, "dd/MM/yyyy", null);
        DateTime dt = DateTime.ParseExact(dr[0].ItemArray[3].ToString(), "dd/MM/yyyy",null);
        DateTime DtInvoice = DateTime.ParseExact(ViewState["InvoiceDate"].ToString().Trim(), "dd/MM/yyyy", null);
        //if (DT1.Month != Convert.ToInt32(hdnIntMonth.Value) && dr[0].ItemArray[4].ToString().ToLower()!="pending")
        //{
        //    lblMessage.Text = "Invoice Date Should be Within This Month";
        //    lblMessage.CssClass = "ErrorMessage";
        //    return false;
        //}
        if (DT1.Subtract(dt).Days < 0)
        {
            lblMessage.Text = "Invoice Date Can not be Less than PO Date";
            lblMessage.CssClass = "ErrorMessage";
            return false;
        }
        else if (DT1.Month != DtInvoice.Month && dr[0].ItemArray[4].ToString().ToLower() == "pending" && dr[0].ItemArray[0].ToString()!="0")
        {
            lblMessage.Text = "Invoice Date Should be Within PO Month For Partial Receipt";
            lblMessage.CssClass = "ErrorMessage";
            return false;
        }
        else if (DT1.Month != Convert.ToInt32(hdnIntMonth.Value))
        {
            lblMessage.Text = "Invoice Date Should be Within This Month";
            lblMessage.CssClass = "ErrorMessage";
            return false;
        }
        else
        return true;
    }

    protected bool BindItem(TextBox ItemCode)
    {
        string strSearchQuery = "";
        if (Request.QueryString["PONo"] != null)
        {
            if (Request.QueryString["strType"].ToString() == "Commercial")
                strSearchQuery = "inner join PurchaseOrder po on dp.dealercode=po.dealercode inner join PurchaseOrderDetails POD on DP.Item_Id=POD.ItemId and po.POno=POD.PONo where POD.pono ='" + Request.QueryString["PONo"] + "'";
            else
                strSearchQuery = "inner join PurchaseOrderDetails POD on CSS.ItemId=POD.ItemId where POD.pono ='" + Request.QueryString["PONo"] + "'";
        }

        if (strSearchQuery == "")
            strSearchQuery = " where IM.Status=1 and Itemcode='" + ItemCode.Text.Trim() + "'";
        else
            strSearchQuery += " and IM.Status=1 and Itemcode='" + ItemCode.Text.Trim() + "'";

        DataSet dsCust = objDlr.FetchDealerItems(strSearchQuery, Request.QueryString["strType"].ToString());

        TextBox Desc1 = Page.FindControl("txtDesc1_" + ItemCode.ID.Substring(ItemCode.ID.IndexOf('_') + 1, ItemCode.ID.Length - (ItemCode.ID.IndexOf('_') + 1))) as TextBox;
        TextBox Desc2 = Page.FindControl("txtDesc2_" + ItemCode.ID.Substring(ItemCode.ID.IndexOf('_') + 1, ItemCode.ID.Length - (ItemCode.ID.IndexOf('_') + 1))) as TextBox;
        HiddenField hdnItemId = Page.FindControl("hdnItemId_" + ItemCode.ID.Substring(ItemCode.ID.IndexOf('_') + 1, ItemCode.ID.Length - (ItemCode.ID.IndexOf('_') + 1))) as HiddenField;
        HtmlInputText UnitPrice = Page.FindControl("txtUnitPrice_" + ItemCode.ID.Substring(ItemCode.ID.IndexOf('_') + 1, ItemCode.ID.Length - (ItemCode.ID.IndexOf('_') + 1))) as HtmlInputText;
        TextBox Quantity = Page.FindControl("txtQty_" + ItemCode.ID.Substring(ItemCode.ID.IndexOf('_') + 1, ItemCode.ID.Length - (ItemCode.ID.IndexOf('_') + 1))) as TextBox;
        HiddenField hdnMaxQty = Page.FindControl("hdnQty_" + ItemCode.ID.Substring(ItemCode.ID.IndexOf('_') + 1, ItemCode.ID.Length - (ItemCode.ID.IndexOf('_') + 1))) as HiddenField;

        int Count = Convert.ToInt16(ItemCode.ID.Substring(ItemCode.ID.IndexOf('_') + 1, ItemCode.ID.Length - (ItemCode.ID.IndexOf('_') + 1)));
        if (dsCust.Tables[0].Rows.Count > 0)
        {
            Desc1.Text = dsCust.Tables[0].Rows[0]["Description1"].ToString();
            Desc2.Text = dsCust.Tables[0].Rows[0]["Description2"].ToString();
            UnitPrice.Value = dsCust.Tables[0].Rows[0]["DlrPrice"].ToString();
            hdnItemId.Value = dsCust.Tables[0].Rows[0]["Itemid"].ToString();
            hdnMaxQty.Value = dsCust.Tables[0].Rows[0]["maxqty"].ToString();
            //Quantity.Text = hdnMaxQty.Value;                 
            Quantity.Focus();
            lblMessage.Text = "";
            lblMessage.CssClass = "";
            return true;

        }
        else
        {
            Desc1.Text = "";
            Desc2.Text = "";
            UnitPrice.Value = "";
            hdnItemId.Value = "";
            Quantity.Text = "";
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

