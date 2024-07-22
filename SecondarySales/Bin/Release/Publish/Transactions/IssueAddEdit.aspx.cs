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
using BusinessClass;
using System.Net.Mail;
using System.IO;
using System.Text;
using System.Net;
using AjaxControlToolkit.HTMLEditor.ToolbarButton;

public partial class Transactions_IssueAddEdit : System.Web.UI.Page
{
    IssueMaster objIssue = new IssueMaster();
    CommonFunction objComm = new CommonFunction();
    MonthEndClass objMonth = new MonthEndClass();
    private DataSet dsItem;
    CustomerMasterClass objCust = new CustomerMasterClass();
    public bool specialcustomer = false;
    protected override void LoadViewState(object earlierState)
    {
        base.LoadViewState(earlierState);
        if (ViewState["dynamictable"] == null)

            GenerateSearchModel(false);

    }
    //protected void Page_Init(object sender, EventArgs e)
    //{
    //    GenerateSearchModel(false);
    //    btnAdd_Click(btnAdd, null);
    //}
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null) Response.Redirect("../Logout.aspx");
        try
        {
            hdnViewPath.Value = ConfigurationManager.AppSettings["issueViewDownloadpath"].ToString();
            //if (this.Session["FileUpload1"] == null && this.FileUpload1.HasFile)
            //    this.Session["FileUpload1"] = (object)this.FileUpload1;
            //else if (this.Session["FileUpload1"] != null && !this.FileUpload1.HasFile)
            //    this.FileUpload1 = (FileUpload)this.Session["FileUpload1"];
            //else if (this.FileUpload1.HasFile)
            //    this.Session["FileUpload1"] = (object)this.FileUpload1;
            if (!IsPostBack)
            { 
                if (Session["UserType"].ToString() == "Dealer")
                {
                    btnFileUpload.Visible = false;
                    FileUpload1.Visible = false;
                    btnView.Visible = false;
                    // lblUploadFileName.Visible = true;
                }
                else
                {
                    btnFileUpload.Visible = false;
                    FileUpload1.Visible = false;
                    btnView.Visible = false;
                    // lblUploadFileName.Visible = false;
                }
                lblMessage.Text = "";
                lblMessage.CssClass = "";
                hdnDlrCode.Value = Session["DealerCode"].ToString();
                objMonth.prpDealerCode = Convert.ToInt32(Session["DealerCode"].ToString());
                DataSet ds = objMonth.GetOpenMonth();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtMonth.Text =ds.Tables[0].Rows[0]["Month"].ToString();
                    txtYear.Text = ds.Tables[0].Rows[0]["Year"].ToString();
                }
                //txtMonth.Text= DateTime.Now.ToString("MMMM"); 
                //txtYear.Text = DateTime.Now.ToString("yyyy");
                txtInvoiceDate.Text = DateTime.Today.ToString(Session["DateFormat"].ToString());

                calInvoiceDate.Format = Session["DateFormat"].ToString();
                CommonFunction.PopulateRecordsWithTwoParam("Common_Values", "Text", "Value", "FieldName", "SalesType", "Status", "1", "Text", ddlSalesType);
                if (Request.QueryString["IssueNo"] != null && Request.QueryString["IssueNo"].ToString() != "")
                {
                    txtInvoiceNo.Text = Request.QueryString["InvoiceNo"].ToString();
                    ddlSalesType.SelectedValue = Request.QueryString["strType"].ToString();
                    txtInvoiceNo.ReadOnly = true;
                }

                txtSalesTax.Attributes.Add("onBlur", "CalcGrossAmount()");
                txtLocallevi.Attributes.Add("onBlur", "CalcGrossAmount()");
                txtInsurence.Attributes.Add("onBlur", "CalcGrossAmount()");
                txtOthers.Attributes.Add("onBlur", "CalcGrossAmount()");

                string[] strMonthList ={"January","February","March","April","May","June","July",
                "August",    "September", "October","November","December" };
                ArrayList monthArrayList = new ArrayList(strMonthList);
                hdnIntMonth.Value = (monthArrayList.IndexOf(txtMonth.Text) + 1).ToString();
            }
            GenerateSearchModel(false);

            if (Request.Params["__EVENTTARGET"] == null)
            {
                btnAdd_Click(btnAdd, null);
                ddlSalesType.SelectedIndex = 1;
            }

            if (!IsPostBack && Request.QueryString["IssueNo"] != null && Request.QueryString["IssueNo"].ToString() != "0")
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
        objIssue.prpIssueNo = Convert.ToInt32(Request.QueryString["IssueNo"].ToString());
        dsItem = objIssue.FetchIssue();
        for (int bindparse = 0; bindparse < dsItem.Tables[0].Rows.Count; bindparse++)
            CreateRow(bindparse + 1);

        hidSearchCount.Value = Convert.ToString(dsItem.Tables[0].Rows.Count);

        for (int bindparse = 0; bindparse < dsItem.Tables[0].Rows.Count; bindparse++)
        {
            int icount = bindparse + 1;
            TextBox txtItemCode = Page.FindControl("txtItemCode_" + icount) as TextBox;
            HiddenField hdnItemId = Page.FindControl("hdnItemId_" + icount) as HiddenField;
            TextBox txtQty = Page.FindControl("txtQty_" + icount) as TextBox;
            HtmlInputText txtUnitPrice = Page.FindControl("txtUnitPrice_" + icount) as HtmlInputText;
            HtmlInputText txtInvoiceAmt = Page.FindControl("txtInvoiceAmt_" + icount) as HtmlInputText;
            TextBox txtDesc1 = Page.FindControl("txtDesc1_" + icount) as TextBox;
            TextBox txtDesc2 = Page.FindControl("txtDesc2_" + icount) as TextBox;
            Button btnDelete = Page.FindControl("btnDelete_" + icount) as Button;
            HiddenField hdnQty = Page.FindControl("hdnQty_" + icount) as HiddenField;
            HiddenField hdnProfitAgreed = Page.FindControl("hdnProfitAgreed_" + icount) as HiddenField;

            txtItemCode.Text = dsItem.Tables[0].Rows[bindparse]["ItemCode"].ToString();
            hdnItemId.Value = dsItem.Tables[0].Rows[bindparse]["ItemId"].ToString();
            txtDesc1.Text = dsItem.Tables[0].Rows[bindparse]["Description1"].ToString();
            txtDesc2.Text = dsItem.Tables[0].Rows[bindparse]["Description2"].ToString();
            txtQty.Text = dsItem.Tables[0].Rows[bindparse]["Qty"].ToString();
            hdnQty.Value = txtQty.Text;
            txtUnitPrice.Value = dsItem.Tables[0].Rows[bindparse]["UnitPrice"].ToString();
            hdnProfitAgreed.Value = dsItem.Tables[0].Rows[bindparse]["ProfitAgreed"].ToString();
            txtInvoiceAmt.Value = dsItem.Tables[0].Rows[bindparse]["InvoiceAmt"].ToString();
            //txtPONo.Text = dsItem.Tables[0].Rows[bindparse]["PONo"].ToString();
            txtMonth.Text = dsItem.Tables[0].Rows[bindparse]["Month"].ToString();
            txtYear.Text = dsItem.Tables[0].Rows[bindparse]["Year"].ToString();
            hdnFileName.Value = dsItem.Tables[0].Rows[bindparse]["FileName"].ToString();
            hdnFileUploadName.Value = hdnFileName.Value;
            txtInvoiceNo.Text = dsItem.Tables[0].Rows[bindparse]["InvoiceNo"].ToString();
            if (dsItem.Tables[0].Rows[bindparse]["InvoiceDate"].ToString() != "")
                txtInvoiceDate.Text = Convert.ToDateTime(dsItem.Tables[0].Rows[bindparse]["InvoiceDate"].ToString()).ToString(Session["DateFormat"].ToString());
            txtCustCode.Text = dsItem.Tables[0].Rows[bindparse]["CustCode"].ToString();
            txtCustName.Value = dsItem.Tables[0].Rows[bindparse]["CustName"].ToString();
            ddlSalesType.SelectedValue = dsItem.Tables[0].Rows[bindparse]["SalesType"].ToString();

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
            if (dsItem.Tables[0].Rows[bindparse]["FileName"].ToString().Length > 0 || dsItem.Tables[0].Rows[bindparse]["FileName1"].ToString().Length > 0)
            {
                btnFileUpload.Visible = true;
                FileUpload1.Visible = true;
                btnView.Visible = true;
                

            }


            btnDelete.Style.Add("display", "none");
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
        txtItemCode.ID = "txtItemCode_" + SearchCount.ToString();
        txtItemCode.TextChanged += new EventHandler(txtItemCode_TextChanged);
        txtItemCode.Attributes.Add("OnChange", "$get('hdnCurrentItemId').value=this.id;fnCheckDuplicate(this.value);");
        txtItemCode.CssClass = "commonfont textdropwidthSmall";

        HiddenField hdnItemId = new HiddenField();
        hdnItemId.ID = "hdnItemId_" + SearchCount;
        HtmlButton btnItem = new HtmlButton();
        btnItem.Attributes.Add("runat", "server");
        btnItem.InnerText = "...";
        btnItem.ID = "btnItem_" + SearchCount.ToString();
        btnItem.Attributes.Add("onclick", "popupItem('Issue','" + SearchCount.ToString() + "','" + Session["DealerCode"].ToString() + "','" + ddlSalesType.SelectedValue + "','" + txtCustCode.Text + "')");
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
        txtQty.Attributes.Add("onBlur", "CalcInvoiceAmt('" + SearchCount + "')");
        tc.Controls.Add(txtQty);
        tc.Controls.Add(hdnQty);

        tc = new TableCell();
        tc.HorizontalAlign = HorizontalAlign.Left;
        tr.Cells.Add(tc);
        HtmlInputText txtUnitPrice = new HtmlInputText();
        txtUnitPrice.Attributes.Add("style", "commonfont textdropwidth right");
        txtUnitPrice.Attributes.Add("runat", "server");
        txtUnitPrice.Attributes.Add("readonly", "readonly");
        txtUnitPrice.Style.Add("width", "80px");
        txtUnitPrice.ID = "txtUnitPrice_" + SearchCount.ToString();
        HiddenField hdnProfitAgreed = new HiddenField();
        hdnProfitAgreed.ID = "hdnProfitAgreed_" + SearchCount.ToString();
        tc.Controls.Add(txtUnitPrice);
        tc.Controls.Add(hdnProfitAgreed);

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
        Table tbl = Page.FindControl("SearchTable") as Table;
        int index = 0;
        Button btn = sender as Button;

        string strID = btn.ID.Replace("btnDelete_", "");
        index = int.Parse(strID);
        int SearchCount = int.Parse(hidSearchCount.Value);
        HiddenField hdnRow = Page.FindControl("hdnRow_" + strID) as HiddenField;
        hdnRow.Value = "S";
        HiddenField hdnItemId = Page.FindControl("hdnItemId_" + strID) as HiddenField;

        TableRow tr = Page.FindControl("tr_" + strID) as TableRow;
        tr.Style.Add("display", "none");


        lblMessage.CssClass = "SuccessMessageBold";
        lblMessage.Text = "Deleted Sucessfully.";

        //string strError = "";
        //try
        //{
        //    objIssue.prpItemId = int.Parse(hdnItemId.Value);
        //    objIssue.prpIssueNo = int.Parse(Request.QueryString["IssueNo"].ToString());
        //    strError = objIssue.DeleteIssueDetails();
        //}
        //catch (Exception Ex)
        //{
        //    lblMessage.CssClass = "ErrorMessage";
        //    lblMessage.Text = Ex.Message;
        //}
        //if (strError != "")
        //{
        //    lblMessage.CssClass = "SuccessMessageBold";
        //    lblMessage.Text = "Deleted Sucessfully.";
        //}
        //else
        //{
        //    lblMessage.CssClass = "ErrorMessage";
        //    lblMessage.Text = strError;
        //}

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
        Int32 intIssueNo = 0;
        string str2 = "please select only pdf or jpg only ";
        Table tbl = Page.FindControl("SearchTable") as Table;
        try
        {
            objIssue.prpMonth = txtMonth.Text.Trim();
            objIssue.prpYear = Convert.ToInt32(txtYear.Text.Trim());
            objIssue.prpCustCode = Convert.ToInt32(txtCustCode.Text.Trim());
            objIssue.prpFileName = hdnFileUploadName.Value;
            if (IsValidate())
            {
                if (Request.QueryString["IssueNo"] == null || Request.QueryString["IssueNo"].ToString() == "")
                    intIssueNo = 0;
                else
                    intIssueNo = Convert.ToInt32(Request.QueryString["IssueNo"].ToString());

                objIssue.prpIssueNo = intIssueNo;
                objIssue.prpUserId = Session["UserID"].ToString();
                objIssue.prpDealerCode = Convert.ToInt32(Session["DealerCode"].ToString());                
                objIssue.prpSalesType = ddlSalesType.SelectedValue.Trim();
                objIssue.prpInvoiceNo = txtInvoiceNo.Text.Trim();
                objIssue.prpInvoiceDate = objComm.DateFormateConversion(txtInvoiceDate.Text.Trim(), Session["DateFormat"].ToString());
                objIssue.prpSalesTax = Convert.ToDouble(txtSalesTax.Text.Trim());
                if (txtLocallevi.Text.Trim() != "")
                    objIssue.prpLocalLevi = Convert.ToDouble(txtLocallevi.Text.Trim());
                else
                    objIssue.prpLocalLevi = 0;
                if (txtInsurence.Text.Trim() != "")
                    objIssue.prpInsurance = Convert.ToDouble(txtInsurence.Text.Trim());
                else
                    objIssue.prpInsurance = 0;
                if (txtOthers.Text.Trim() != "")
                    objIssue.prpOthers = Convert.ToDouble(txtOthers.Text.Trim());
                else
                    objIssue.prpOthers = 0;
                objIssue.prpNetInvoiceAmt = Convert.ToDouble(txtNetInvoiceAmt.Value.Trim());
                objIssue.prpGrossInvoiceAmt = Convert.ToDouble(txtGrossInvoice.Value.Trim());
                objIssue.prpRemarks = txtRemarks.Text.Trim();
                objIssue.prpFileName = hdnFileUploadName.Value;
                strMsg = objIssue.SaveIssue();
               

               
            


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
                        HiddenField hdnProfitAgreed = Page.FindControl("hdnProfitAgreed_" + icount) as HiddenField;

                        if (hdnRow.Value == "S")
                            continue;

                        objIssue.prpItemId = Convert.ToInt32(hdnItemId.Value.Trim());
                        objIssue.prpQty = Convert.ToInt32(txtQty.Text.Trim());
                        if (hdnQty.Value.Trim() != "")
                            objIssue.prpPrevQty = Convert.ToInt32(hdnQty.Value.Trim());
                        else
                            objIssue.prpPrevQty = 0;
                        objIssue.prpUnitPrice = Convert.ToDouble(txtUnitPrice.Value.Trim());
                        objIssue.prpProfitAgreed = Convert.ToDouble(hdnProfitAgreed.Value.Trim());
                        strMsg = objIssue.SaveIssueDetails();

                        if (strMsg != "")
                        {
                            lblMessage.Text = strMsg;
                            lblMessage.CssClass = "ErrorMessage";
                            return;
                        }
                    }
                    if (strMsg == "")
                    {
                        if (Request.QueryString["IssueNo"] != null && Request.QueryString["IssueNo"].ToString() != "")
                            lblMessage.Text = "Updated Sucessfully.";
                        else
                            lblMessage.Text = "Saved Sucessfully.";


                        lblMessage.CssClass = "SuccessMessageBold";
                        Response.Redirect("Issue.aspx");
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

    //protected void btnUpload_Click(object sender, EventArgs e)
    //{
    //    if (this.Session["FileUpload1"] == null && this.FileUpload1.HasFile)
    //        this.Session["FileUpload1"] = (object)this.FileUpload1;
    //    else if (this.Session["FileUpload1"] != null && !this.FileUpload1.HasFile)
    //    {
    //        this.FileUpload1 = (FileUpload)this.Session["FileUpload1"];
    //    }
    //    else
    //    {
    //        if (!this.FileUpload1.HasFile)
    //            return;
    //        byte[] numArray = (byte[])null;
    //        using (BinaryReader binaryReader = new BinaryReader(this.FileUpload1.PostedFile.InputStream))
    //            numArray = binaryReader.ReadBytes(this.FileUpload1.PostedFile.ContentLength);
    //        this.Session["FileArray"] = (object)numArray;
    //        this.Session["FileUpload1"] = (object)this.FileUpload1;
    //        this.FileUpload1 = (FileUpload)this.Session["FileUpload1"];
    //        this.lblUploadFileName.Text = this.FileUpload1.FileName;
    //    }
    //}
    //protected void BtnUpload_Click(object sender, EventArgs e)
    //{
    //    if (this.Session["FileUpload1"] == null && this.FileUpload1.HasFile)
    //        this.Session["FileUpload1"] = (object)this.FileUpload1;
    //    else if (this.Session["FileUpload1"] != null && !this.FileUpload1.HasFile)
    //    {
    //        this.FileUpload1 = (FileUpload)this.Session["FileUpload1"];
    //    }
    //    else
    //    {
    //        if (!this.FileUpload1.HasFile)
    //            return;
    //        byte[] numArray = (byte[])null;
    //        using (BinaryReader binaryReader = new BinaryReader(this.FileUpload1.PostedFile.InputStream))
    //            numArray = binaryReader.ReadBytes(this.FileUpload1.PostedFile.ContentLength);
    //        this.Session["FileArray"] = (object)numArray;
    //        this.Session["FileUpload1"] = (object)this.FileUpload1;
    //        this.FileUpload1 = (FileUpload)this.Session["FileUpload1"];
    //        this.lblUploadFileName.Text = this.FileUpload1.FileName;
    //    }
    //}

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["IssueNo"] == null || Request.QueryString["strType"] == null)
            Response.Redirect("IssueAddEdit.aspx");
        else
            Response.Redirect("IssueAddEdit.aspx?InvoiceNo=" + txtInvoiceNo.Text.Trim() + "&IssueNo=" + Request.QueryString["IssueNo"].ToString() + "&strType=" + Request.QueryString["strType"].ToString());
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Issue.aspx");
    }
    protected void ddlSalesType_SelectedIndexChanged(object sender, EventArgs e)
    {
        GenerateSearchModel(true);
    }
    protected void btnGetdata_Click(object sender, EventArgs e)
    {
        GenerateSearchModel(true);
        btnAdd_Click(btnAdd, null);
        
    }
    protected bool IsValidate()
    {

        Table tbl = Page.FindControl("SearchTable") as Table;
        for (int icount = 1; icount < tbl.Rows.Count; icount++)
        {
            HiddenField hdnRow = Page.FindControl("hdnRow_" + icount) as HiddenField;
            TextBox txtItemCode = Page.FindControl("txtItemCode_" + icount) as TextBox;
            HiddenField hdnItemId = Page.FindControl("hdnItemId_" + icount) as HiddenField;
            TextBox txtQty = Page.FindControl("txtQty_" + icount) as TextBox;
            HiddenField hdnQty = Page.FindControl("hdnQty_" + icount) as HiddenField;
            if (hdnRow.Value == "S")
                continue;

            int intPreQty = 0;
            int IssueQty = 0;
            if (hdnQty.Value.Trim() != "")
                intPreQty = Convert.ToInt32(hdnQty.Value.Trim());
            if (txtQty.Text.Trim() != "")
                IssueQty = Convert.ToInt32(txtQty.Text.Trim());
            if (!BindItem(txtItemCode))
            {
                return false;
            }

            decimal openStock, custStockRtnQty, dlrStockRtnQty, recQty, issueQty, intAvailQty;
            openStock = custStockRtnQty = dlrStockRtnQty = recQty = issueQty = intAvailQty = 0;



            DataSet ds = new DataSet();
            ds = CommonFunction.FetchRecordsWithQuery("Select OpeningStock,DlrReturnStock,CustReturnStock from DealerStock where DealerCode='" + Session["DealerCode"] + "' and ItemId=" + hdnItemId.Value + " and Year=" + objIssue.prpYear + " and Month='" + objIssue.prpMonth + "'", "DealerCode");
            if (ds.Tables[0].Rows.Count > 0)
            {
                openStock = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                dlrStockRtnQty = Convert.ToInt32(ds.Tables[0].Rows[0][1].ToString());
                custStockRtnQty = Convert.ToInt32(ds.Tables[0].Rows[0][2].ToString());
            }

            ds = CommonFunction.FetchRecordsWithQuery("select r2.dealercode,isnull(sum(isnull(Qty,0)),0) from ReceiptDetails r1 inner join receipt r2 on r1.receiptno=r2.receiptno where r1.itemid=" + hdnItemId.Value + " and r2.DealerCode=" + Session["DealerCode"] + " and r2.Year=" + objIssue.prpYear + " and r2.Month='" + objIssue.prpMonth + "' group by r2.dealercode", "r2.DealerCode");
            if (ds.Tables[0].Rows.Count > 0)
                recQty = Convert.ToInt32(ds.Tables[0].Rows[0][1].ToString());

            if (Request.QueryString["IssueNo"] != null && Request.QueryString["IssueNo"].ToString() != "")
            {
                ds = CommonFunction.FetchRecordsWithQuery("select i2.dealercode,isnull(sum(isnull(Qty,0)),0) from IssueDetails i1 inner join Issue i2 on i1.issueno=i2.issueno where i1.itemid=" + hdnItemId.Value + " and i2.DealerCode=" + Session["DealerCode"] + " and i2.Year=" + objIssue.prpYear + "and i2.Month='" + objIssue.prpMonth + "' and i1.issueno<>" + Request.QueryString["IssueNo"].ToString() + " group by i2.dealercode", "i2.DealerCode");
                if (ds.Tables[0].Rows.Count > 0)
                    issueQty = Convert.ToInt32(ds.Tables[0].Rows[0][1].ToString());
            }
            else
            {
                ds = CommonFunction.FetchRecordsWithQuery("select i2.dealercode,isnull(sum(isnull(Qty,0)),0) from IssueDetails i1 inner join Issue i2 on i1.issueno=i2.issueno where i1.itemid=" + hdnItemId.Value + " and i2.DealerCode=" + Session["DealerCode"] + " and i2.Year=" + objIssue.prpYear + "and i2.Month='" + objIssue.prpMonth + "' group by i2.dealercode", "i2.DealerCode");
                if (ds.Tables[0].Rows.Count > 0)
                    issueQty = Convert.ToInt32(ds.Tables[0].Rows[0][1].ToString());
            }

            intAvailQty = (openStock + recQty + custStockRtnQty) - (issueQty + dlrStockRtnQty);
            if (Session["UserType"].ToString() != "Agfa")
            {
                if (IssueQty > intAvailQty)
                {
                    lblMessage.Text = "For Item " + txtItemCode.Text.Trim() + " Maximum Available Quantity Is " + intAvailQty;
                    lblMessage.CssClass = "ErrorMessage";
                    return false;
                }
            }
        }

        //Check For Validation To Allow Only Current Month as Invoice Date
        int intMonth = Convert.ToInt16(hdnIntMonth.Value);
        if (intMonth != 0)
        {

            string[] sd = null;
            //if (int.Parse(this.txtInvoiceDate.Text.Split('/')[1]) != intMonth)
            if (txtInvoiceDate.Text.Contains('/'))
                sd = txtInvoiceDate.Text.Split('/');
            else
                sd = txtInvoiceDate.Text.Split('-');
            if (int.Parse(sd[1]) != intMonth)
            {
                lblMessage.Text = "Enter the Invoice Date With in This Month";
                lblMessage.CssClass = "ErrorMessage";
                return false;
            }
        }
        DataSet ds1 = new DataSet();
        ds1 = CommonFunction.FetchRecordsWithQuery("Select IsSpecialCust from CustomerMaster where CustCode=" + objIssue.prpCustCode , "CustCode");
        if (ds1.Tables[0].Rows.Count > 0)
        {
            if(Convert.ToBoolean(ds1.Tables[0].Rows[0][0].ToString()))
            {
                if (objIssue.prpFileName == null || objIssue.prpFileName == "" )
                {
                    lblMessage.Text = "Please Choose a File";
                    lblMessage.CssClass = "ErrorMessage";
                    return false;
                }
            }
        }
           
        
        return true;

    }

    protected bool BindItem(TextBox ItemCode)
    {
        string strSearchQuery;
        if (ddlSalesType.Text.Trim() == "Commercial")
            if(Session["DealerCode"].ToString() != "" && Session["DealerCode"].ToString() != "0")
            {
                strSearchQuery = " where IM.Status=1 and Itemcode='" + ItemCode.Text.Trim() + "' and DP.DealerCode='" + Session["DealerCode"].ToString() + "' and CM.CustCode='" + txtCustCode.Text.Trim() + "'";

            }
            else
            {
                strSearchQuery = " where IM.Status=1 and Itemcode='" + ItemCode.Text.Trim() + "' and CM.CustCode='" + txtCustCode.Text.Trim() + "'";

            }
        else
            strSearchQuery = " where IM.Status=1 and Itemcode='" + ItemCode.Text.Trim() + "' and CustCode='" + txtCustCode.Text.Trim() + "'";


        DataSet dsCust = objCust.FetchCustItems(strSearchQuery, ddlSalesType.Text);
        TextBox Desc1 = Page.FindControl("txtDesc1_" + ItemCode.ID.Substring(ItemCode.ID.IndexOf('_') + 1, ItemCode.ID.Length - (ItemCode.ID.IndexOf('_') + 1))) as TextBox;
        TextBox Desc2 = Page.FindControl("txtDesc2_" + ItemCode.ID.Substring(ItemCode.ID.IndexOf('_') + 1, ItemCode.ID.Length - (ItemCode.ID.IndexOf('_') + 1))) as TextBox;
        HiddenField hdnItemId = Page.FindControl("hdnItemId_" + ItemCode.ID.Substring(ItemCode.ID.IndexOf('_') + 1, ItemCode.ID.Length - (ItemCode.ID.IndexOf('_') + 1))) as HiddenField;
        HtmlInputText UnitPrice = Page.FindControl("txtUnitPrice_" + ItemCode.ID.Substring(ItemCode.ID.IndexOf('_') + 1, ItemCode.ID.Length - (ItemCode.ID.IndexOf('_') + 1))) as HtmlInputText;
        HiddenField hdnProfitAgreed = Page.FindControl("hdnProfitAgreed_" + ItemCode.ID.Substring(ItemCode.ID.IndexOf('_') + 1, ItemCode.ID.Length - (ItemCode.ID.IndexOf('_') + 1))) as HiddenField;

        if (dsCust.Tables.Count > 0)
        {
            if (dsCust.Tables[0].Rows.Count > 0)
            {

                Desc1.Text = dsCust.Tables[0].Rows[0][3].ToString();
                Desc2.Text = dsCust.Tables[0].Rows[0][4].ToString();
                UnitPrice.Value = dsCust.Tables[0].Rows[0]["EUPrice"].ToString();
                hdnItemId.Value = dsCust.Tables[0].Rows[0][1].ToString();
                hdnProfitAgreed.Value = dsCust.Tables[0].Rows[0]["ProfitAgreed"].ToString();
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
                lblMessage.Text = "ItemCode Not Found.";
                lblMessage.CssClass = "ErrorMessage";
                return false;
            }
        }
        else
        {
            Desc1.Text = "";
            Desc2.Text = "";
            UnitPrice.Value = "";
            hdnItemId.Value = "";
            lblMessage.Text = "ItemCode Not Found.";
            lblMessage.CssClass = "ErrorMessage";
            return false;
        }



    }


    protected void txtItemCode_TextChanged(object sender, EventArgs e)
    {
        BindItem((TextBox)sender);
    }

    protected bool ValidateCustomerCode(string custCode)
    {
        string strSearchQuery = "";
            if(hdnDlrCode.Value.Trim() == "0")
            {
                strSearchQuery = " where Status=1 and CustCode='" + custCode + "'";
            }
        if (hdnDlrCode.Value.Trim() != "0")
        {
            strSearchQuery = " where Status=1 and CustCode='" + custCode + "'and DealerCode='" + hdnDlrCode.Value.Trim() + "'";
        }
        DataSet dsDealer = objCust.FetchCustomerMaster(strSearchQuery);
        if (dsDealer.Tables[0].Rows.Count > 0)
        {
            txtCustName.Value = dsDealer.Tables[0].Rows[0]["CustName"].ToString();
            this.hndDlrDirect.Value = dsDealer.Tables[0].Rows[0]["SpecialCustomer"].ToString();
            lblMessage.Text = "";
            lblMessage.CssClass = "";
            if (dsDealer.Tables[0].Rows[0]["SpecialCustomer"].ToString() == "No")
            {
                btnFileUpload.Visible = false;
                FileUpload1.Visible = false;
                this.btnView.Visible = false;
                specialcustomer = false;
            }
            else
            {
                btnFileUpload.Visible = true;
                FileUpload1.Visible = true;
                specialcustomer = true;
                //this.btnView.Visible = true;
            }
            return true;
        }
        else
        {
            txtCustName.Value = "";
            lblMessage.Text = "Cust.Code Not Found.";
            lblMessage.CssClass = "ErrorMessage";
            return false;
        }

    }
    protected void txtCustCode_TextChanged(object sender, EventArgs e)
    {
        ValidateCustomerCode(txtCustCode.Text);
        btnGetdata_Click(btnGetdata, null);
        string str = this.hndDlrDirect.Value;
        lblMessege.Text = "";
        if (!string.IsNullOrEmpty(str) && str == "Yes")
        {
           // this.FileUpload1.Visible = true;
            //this.lblUplTxt.Visible = true;
            //this.btnView.Visible = true;
        }
        else
        {
           // this.FileUpload1.Visible = false;
           // this.lblUplTxt.Visible = false;
            //this.lblUploadFileName.Text = string.Empty;
            //this.btnView.Visible = false; //this line commented by abhilash
        }
    }




    protected void btnFileUpload_Click(object sender, EventArgs e)
    {
        hdnFileUploadName.Value = hdnFileName.Value;
        String FileName = DateTime.Now.ToString("yyyyMMddHHmmss");
        if (FileUpload1.HasFile)
        {
            string FileExtention = System.IO.Path.GetExtension(FileUpload1.FileName);
            FileName += "_" + FileUpload1.FileName;

            if (FileExtention.ToLower() != ".pdf" && FileExtention.ToLower() != ".jpj")
            {
                lblMessege.Text = "Only files with .pdf and .jpg extention are allowed";
                lblMessege.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                int filesize = FileUpload1.PostedFile.ContentLength;

                if (filesize > 2097152)
                {
                    lblMessege.Text = "Maximum files 2MB exceeded";
                    lblMessege.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    FileUpload1.SaveAs(Server.MapPath("~/Uploads/" + FileName));
                    lblMessege.Text = "File Uploaded";
                    lblMessege.ForeColor = System.Drawing.Color.Green;
                }
                hdnFileUploadName.Value = FileName;
            }

        }
        else
        {
            lblMessege.Text = "Please select file to upload";
            lblMessege.ForeColor = System.Drawing.Color.Red;
        }
    }




}


