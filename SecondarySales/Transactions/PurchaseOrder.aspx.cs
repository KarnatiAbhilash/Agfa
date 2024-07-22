/* ==============================================================================================
    Created By      : Subahani SM
    Created Date    : 20 Aug 2010
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

public partial class Transactions_PurchaseOrder : System.Web.UI.Page
{
    PurchaseOrder objPO = new PurchaseOrder();
    MonthEndClass objMonth = new MonthEndClass();
    CustomerMasterClass objCust = new CustomerMasterClass();
    DealerMasterClass objDlr = new DealerMasterClass();

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
                txtPODate.Text = DateTime.Today.ToString(Session["DateFormat"].ToString());

                txtSalesTax.Attributes.Add("onBlur", "CalcGrossAmount()");
                txtLocallevi.Attributes.Add("onBlur", "CalcGrossAmount()");
                txtInsurence.Attributes.Add("onBlur", "CalcGrossAmount()");
                txtOthers.Attributes.Add("onBlur", "CalcGrossAmount()");

                CommonFunction.PopulateRecordsWithTwoParam("Common_Values", "Text", "Value", "FieldName", "POType", "Status", "1", "Text", ddlPOType);
            }
            GenerateSearchModel(false);
            if (Request.Params["__EVENTTARGET"] == null)
            {
                ddlPOType.SelectedIndex = 1;
                ddlPOType_SelectedIndexChanged(ddlPOType, null);
            }

        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
            lblMessage.CssClass = "ErrorMessage";
        }

    }

    protected void BindData()
    {
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
        txtItemCode.Attributes.Add("OnChange", "$get('hdnCurrentItemId').value=this.id;fnCheckDuplicate(this.value);");

        HiddenField hdnItemId = new HiddenField();
        hdnItemId.ID = "hdnItemId_" + SearchCount;
        HtmlButton btnItem = new HtmlButton();
        btnItem.Attributes.Add("runat", "server");
        btnItem.InnerText = "...";
        btnItem.ID = "btnItem_" + SearchCount.ToString();
        btnItem.Attributes.Add("onclick", "popupItem('PO','" + SearchCount.ToString() + "','" + Session["DealerCode"].ToString() + "','" + ddlPOType.SelectedValue + "')");
        btnItem.Attributes.Add("class", "popButton");
        tc.Controls.Add(txtItemCode);
        tc.Controls.Add(hdnItemId);
        tc.Controls.Add(btnItem);

        tc = new TableCell();
        tr.Cells.Add(tc);
        tc.HorizontalAlign = HorizontalAlign.Left;
        TextBox txtDesc1 = new TextBox();
        txtDesc1.CssClass = "commonfont textdropwidth";
        txtDesc1.ID = "txtDesc1_" + SearchCount.ToString();
        txtDesc1.TextMode = TextBoxMode.MultiLine;
        txtDesc1.Enabled = false;
        tc.Controls.Add(txtDesc1);

        tc = new TableCell();
        tr.Cells.Add(tc);
        tc.HorizontalAlign = HorizontalAlign.Left;
        TextBox txtDesc2 = new TextBox();
        txtDesc2.CssClass = "commonfont textdropwidth";
        txtDesc2.ID = "txtDesc2_" + SearchCount.ToString();
        txtDesc2.TextMode = TextBoxMode.MultiLine;
        txtDesc2.Enabled = false;
        tc.Controls.Add(txtDesc2);

        tc = new TableCell();
        tr.Cells.Add(tc);
        tc.HorizontalAlign = HorizontalAlign.Left;
        TextBox txtQty = new TextBox();
        txtQty.MaxLength = 6;
        txtQty.CssClass = "commonfont textdropwidthUOM right";
        txtQty.ID = "txtQty_" + SearchCount.ToString();
        HiddenField hdnMaxQty = new HiddenField();
        hdnMaxQty.ID = "hdnMaxQty_" + SearchCount.ToString();
        txtQty.Attributes.Add("onBlur", "CalcInvoiceAmt('" + SearchCount + "')");
        tc.Controls.Add(txtQty);
        tc.Controls.Add(hdnMaxQty);

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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string strMsg = "";
        Table tbl = Page.FindControl("SearchTable") as Table;
        try
        {
            if (IsValidate())
            {
                objPO.prpUserId = Session["UserID"].ToString();
                objPO.prpDealerCode = Convert.ToInt32(Session["DealerCode"].ToString());
                objPO.prpPONo = txtPONo.Text.Trim();
                objPO.prpPOType = ddlPOType.SelectedValue;
                objPO.prpMonth = txtMonth.Text.Trim();
                objPO.prpYear = Convert.ToInt32(txtYear.Text.Trim());
                objPO.prpSalesTax = Convert.ToDouble(txtSalesTax.Text.Trim());
                if (txtLocallevi.Text.Trim() != "")
                    objPO.prpLocalLevi = Convert.ToDouble(txtLocallevi.Text.Trim());
                else
                    objPO.prpLocalLevi = 0;
                if (txtInsurence.Text.Trim() != "")
                    objPO.prpInsurance = Convert.ToDouble(txtInsurence.Text.Trim());
                else
                    objPO.prpInsurance = 0;
                if (txtOthers.Text.Trim() != "")
                    objPO.prpOthers = Convert.ToDouble(txtOthers.Text.Trim());
                else
                    objPO.prpOthers = 0;
                objPO.prpNetInvoiceAmt = Convert.ToDouble(txtNetInvoiceAmt.Value.Trim());
                objPO.prpGrossInvoiceAmt = Convert.ToDouble(txtGrossInvoice.Value.Trim());
                strMsg = objPO.SavePurchaseOrder();
                if (strMsg == "")
                {
                    for (int icount = 1; icount < tbl.Rows.Count; icount++)
                    {
                        HiddenField hdnRow = Page.FindControl("hdnRow_" + icount) as HiddenField;
                        TextBox txtItemCode = Page.FindControl("txtItemCode_" + icount) as TextBox;
                        HiddenField hdnItemId = Page.FindControl("hdnItemId_" + icount) as HiddenField;
                        TextBox txtQty = Page.FindControl("txtQty_" + icount) as TextBox;
                        HtmlInputText txtUnitPrice = Page.FindControl("txtUnitPrice_" + icount) as HtmlInputText;
                        if (BindItem(txtItemCode))
                        {
                            if (hdnRow.Value == "S")
                                continue;

                            objPO.prpItemId = Convert.ToInt32(hdnItemId.Value.Trim());
                            objPO.prpQty = Convert.ToInt32(txtQty.Text.Trim());
                            objPO.prpUnitPrice = Convert.ToDouble(txtUnitPrice.Value.Trim());
                            strMsg = objPO.SavePurchaseOrderDetails();

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
                        lblMessage.Text = "PO Generated Successfully.";
                        lblMessage.CssClass = "SuccessMessageBold";
                        SendMailToResponsible();
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
        Response.Redirect("PurchaseOrder.aspx");
    }

    protected void ddlPOType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPOType.SelectedValue != "0")
        {

            objPO.prpDealerCode = Convert.ToInt32(Session["DealerCode"].ToString());
            string strDealerName = "  ";
            DataSet ds = CommonFunction.FetchRecordsWithQuery("select DealerName from DealerMaster where DealerCode=" + Session["DealerCode"].ToString(), "DealerName");
            if (ds.Tables[0].Rows.Count > 0)
            {
                // strDealerName = ds.Tables[0].Rows[0]["DealerName"].ToString();
                hdnDlrName.Value = strDealerName;

                if (ddlPOType.SelectedValue == "Commercial")
                    strDealerName = "PO-" + Session["DealerCode"].ToString() + "-";
                else
                    strDealerName = "SM-" + Session["DealerCode"].ToString() + "-";

                txtPONo.Text = strDealerName + objPO.FetchPOMaxNo(strDealerName);

                btnAdd.Enabled = true;
                btnSave.Enabled = true;
            }
        }
        else
        {
            txtPONo.Text = "";
            btnAdd.Enabled = false;
            btnSave.Enabled = false;
        }
        txtSalesTax.Text = "";
        txtLocallevi.Text = "";
        txtNetInvoiceAmt.Value = "";
        txtInsurence.Text = "";
        txtOthers.Text = "";
        txtGrossInvoice.Value = "";
        GenerateSearchModel(true);
        btnAdd_Click(btnAdd, null);
    }

    protected void SendMailToResponsible()
    {
        try
        {
            MailMessage sendMsg = new MailMessage();
            StringBuilder contents = new StringBuilder();
            string strHeader = "";
            string strContent = "";
            string strFooter = "";
            string RespName, RespId, RespEmail;

            //DataSet ds = CommonFunction.FetchRecordsWithQuery("select Username,UserId,Email from UserMaster UM inner join DealerMaster DM on UM.UserId=DM.Respuser where UM.DealerCode=" + Session["DealerCode"].ToString(), "UserId");
            DataSet ds;
            int i = 0;

            string RespUser = "";
            ds = CommonFunction.FetchRecordsWithQuery("select RespUser from dealermaster where DealerCode=" + Session["DealerCode"].ToString(), "DealerCode");
            if (ds.Tables[0].Rows.Count > 0)
            {
                RespUser = ds.Tables[0].Rows[0]["RespUser"].ToString();
            }

            ds = CommonFunction.FetchRecordsWithQuery("select Username,UserId,Email from UserMaster UM where UM.DealerCode=" + Session["DealerCode"].ToString(), "UserId");
            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    RespName = ds.Tables[0].Rows[i]["Username"].ToString();
                    RespId = ds.Tables[0].Rows[i]["UserId"].ToString();
                    RespEmail = ds.Tables[0].Rows[i]["Email"].ToString();
                }
                else
                {
                    lblMessage.Text = "Mail Send Failed.Email Id is not exists for Resp. User.Please Contact AGFA.";
                    lblMessage.CssClass = "ErrorMessage";
                    return;
                }

                if (RespUser.Contains(RespId))
                {
                    sendMsg = new MailMessage();
                    sendMsg.Subject = "PO";
                    strContent = "Dear " + RespName + " ,<br/><br/>" + "Dealer " + Session["DealerName"].ToString() + " has been generated the PO.<br/>Please find the PO details below:<br/>";

                    strHeader = "<table border=\"1\" cellpadding=\"0\" cellspacing = \"0\" style=\"color:#852052;\">"
                             + "<tr>"
                             + "<td align=\"left\">Supplier:" + txtSupplier.Text.Trim() + "</td><td align=\"left\">Month:" + txtMonth.Text.Trim() + "</td><td align=\"left\">Year:" + txtYear.Text.Trim() + "</td>"
                             + "</tr>"
                             + "<tr>"
                             + "<td align=\"left\">PO Type:" + ddlPOType.SelectedValue + "</td><td align=\"left\">PO No:" + txtPONo.Text.Trim() + "</td><td align=\"left\">PO Date:" + txtPODate.Text.Trim() + "</td>"
                             + "</tr>";
                    contents.Append("<tr>");
                    contents.Append("<td align=\"left\"><b>Item Code</b></td><td align=\"left\"><b>Description 1</b></td><td align=\"left\"><b>Description 2</b></td><td align=\"left\"><b>Qty</b></td><td align=\"left\"><b>Unit Price</b></td><td align=\"left\"><b>Invoice Amt</b></td>");
                    contents.Append("</tr>");

                    Table tbl = Page.FindControl("SearchTable") as Table;

                    for (int icount = 1; icount < tbl.Rows.Count; icount++)
                    {
                        HiddenField hdnRow = Page.FindControl("hdnRow_" + icount) as HiddenField;
                        TextBox txtItemCode = Page.FindControl("txtItemCode_" + icount) as TextBox;
                        TextBox txtDesc1 = Page.FindControl("txtDesc1_" + icount) as TextBox;
                        TextBox txtDesc2 = Page.FindControl("txtDesc2_" + icount) as TextBox;
                        HiddenField hdnItemId = Page.FindControl("hdnItemId_" + icount) as HiddenField;
                        TextBox txtQty = Page.FindControl("txtQty_" + icount) as TextBox;
                        HtmlInputText txtUnitPrice = Page.FindControl("txtUnitPrice_" + icount) as HtmlInputText;
                        HtmlInputText txtInvoiceAmt = Page.FindControl("txtInvoiceAmt_" + icount) as HtmlInputText;
                        if (hdnRow.Value == "S")
                            continue;

                        contents.Append("<tr>");
                        contents.Append("<td align=\"left\">" + txtItemCode.Text.Trim() + "</td>");
                        contents.Append("<td align=\"left\">" + txtDesc1.Text.Trim() + "</td>");
                        contents.Append("<td align=\"left\">" + txtDesc2.Text.Trim() + "</td>");
                        contents.Append("<td align=\"left\">" + txtQty.Text.Trim() + "</td>");
                        contents.Append("<td align=\"left\">" + txtUnitPrice.Value.Trim() + "</td>");
                        contents.Append("<td align=\"left\">" + txtInvoiceAmt.Value.Trim() + "</td>");
                        contents.Append("</tr>");

                    }

                    strFooter = "<tr>"
                             + "<td align=\"left\">Sales Tax (CST/VAT %):" + txtSalesTax.Text.Trim() + "</td><td align=\"left\">Local Levi (%):" + txtLocallevi.Text.Trim() + "</td><td align=\"left\">Net Invoice Amt:" + txtNetInvoiceAmt.Value.Trim() + "</td>"
                             + "</tr>"
                             + "<tr>"
                             + "<td align=\"left\">Insurance (Rs.):" + txtInsurence.Text.Trim() + "</td><td align=\"left\">Others (Rs.):" + txtOthers.Text.Trim() + "</td><td align=\"left\">Gross Invoice Amt:" + txtGrossInvoice.Value.Trim() + "</td>"
                             + "</tr>"
                             + "<tr>"
                             + "<td align=\"left\" colspan=\"3\">Remarks:" + txtRemarks.Text.Trim() + "</td>"
                             + "</tr>"
                             + "</table>"
                             + "<br/><br/>"
                             + "Thanks & Regards,<br/>"
                             + Session["UserName"].ToString();
                             //+ "AGFA Portal."; changed by prasani on 29/03/2011

                    sendMsg.From = new MailAddress(ConfigurationManager.AppSettings["EmailID"].ToString().Trim());
                    //sendMsg.From = new MailAddress("8Dportal@wipro.com");
                    Session["FailedEmail"] = RespEmail;
                    sendMsg.To.Add(new MailAddress(RespEmail));
                    sendMsg.Body = strContent + "<br/>" + strHeader + contents.ToString() + strFooter;
                    sendMsg.IsBodyHtml = true;
                    SmtpClient client = new SmtpClient();
                    client.Send(sendMsg);
                }
            }

            //if (sendMsg.To.Count > 0 && RespUser.ToString() != "")
            //{

            //}
        }
        catch (Exception ex)
        {
            lblMessage.CssClass = "ErrorMessage";
            if (ex.GetType().FullName == "System.Net.Mail.SmtpFailedRecipientException" && Session["FailedEmail"].ToString() != "")
                lblMessage.Text = "Mail Failed for Recipient - '" + Session["FailedEmail"].ToString() + "'";
            else
                lblMessage.CssClass = "ErrorMessage";
        }
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
            int intPOQty = 0;
            int intPendingPO = 0;
            
            if (hdnMaxQty.Value.Trim() != "")
                intMaxQty = Convert.ToInt32(hdnMaxQty.Value.Trim());

            if (txtQty.Text.Trim() != "")
                intPOQty = Convert.ToInt32(txtQty.Text.Trim());

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
            ds = CommonFunction.FetchRecordsWithQuery("Select r2.dealercode,r1.itemid,ISNULL(SUM(qty),0) from ReceiptDetails r1 inner join Receipt r2 on r1.ReceiptNo=r2.ReceiptNo where r2.DealerCode=" + Session["DealerCode"].ToString() + " and r2.Month='" + txtMonth.Text.Trim() + "' and r2.Year=" + txtYear.Text.Trim() + " and r1.ItemId=" + hdnItemId.Value+" group by r2.Dealercode,r1.ItemId", "r2.DealerCode");
            if (ds.Tables[0].Rows.Count > 0)
                receiptQty = Convert.ToInt32(ds.Tables[0].Rows[0][2].ToString());
            ds = CommonFunction.FetchRecordsWithQuery("Select i2.dealercode,i1.itemid,ISNULL(SUM(qty),0) from IssueDetails i1 inner join Issue i2 on i1.IssueNo=i2.IssueNo where i2.DealerCode=" + Session["DealerCode"].ToString() + " and i2.Month='" + txtMonth.Text.Trim() + "' and i2.Year=" + txtYear.Text.Trim() + " and i1.ItemId=" + hdnItemId.Value + " group by i2.Dealercode,i1.ItemId", "i2.DealerCode");
            if (ds.Tables[0].Rows.Count > 0)
                issueQty = Convert.ToInt32(ds.Tables[0].Rows[0][2].ToString());
            ds = CommonFunction.FetchRecordsWithQuery("Select p2.dealercode,p1.itemid,ISNULL(SUM(qty),0) from PurchaseOrderDetails p1 inner join PurchaseOrder p2 on p1.PONo=p2.PONo where p2.DealerCode=" + Session["DealerCode"].ToString() + " and p2.Month='" + txtMonth.Text.Trim() + "' and p2.Year=" + txtYear.Text.Trim() + " and p1.ItemId=" + hdnItemId.Value + " and (p2.postatus is null or p2.postatus='pending') group by p2.Dealercode,p1.ItemId", "p2.DealerCode");
            if (ds.Tables[0].Rows.Count > 0)
                intPendingPO = Convert.ToInt32(ds.Tables[0].Rows[0][2].ToString());
            //else
            //{
            //    lblMessage.Text = "Item " + txtItemCode.Text.Trim() + " Is Not Mapped.Please Contact Agfa.";
            //    lblMessage.CssClass = "ErrorMessage";
            //    return false;
            //}

            intAvailQty = (openStock + receiptQty + custStockRtn) - (dlrStockRtn + issueQty);
            intPOQty = intPOQty + intAvailQty + intPendingPO;

            if (intPOQty > intMaxQty)
            {
                lblMessage.Text = "For Item " + txtItemCode.Text.Trim() + " Maximum Allowed Quantity Is " + intMaxQty + ".\nStock At Dealer: " + intAvailQty;
                lblMessage.CssClass = "ErrorMessage";
                return false;
            }



        }
        return true;
    }

    protected bool BindItem(TextBox ItemCode)
    {
        string strSearchQuery = " where IM.Status=1 and Itemcode='" + ItemCode.Text.Trim() + "' and Dealercode='" + Session["DealerCode"].ToString() + "'";
        DataSet dsCust = objDlr.FetchDealerItems(strSearchQuery, ddlPOType.Text.Trim());
        TextBox Desc1 = Page.FindControl("txtDesc1_" + ItemCode.ID.Substring(ItemCode.ID.IndexOf('_') + 1, ItemCode.ID.Length - (ItemCode.ID.IndexOf('_') + 1))) as TextBox;
        TextBox Desc2 = Page.FindControl("txtDesc2_" + ItemCode.ID.Substring(ItemCode.ID.IndexOf('_') + 1, ItemCode.ID.Length - (ItemCode.ID.IndexOf('_') + 1))) as TextBox;
        HiddenField hdnItemId = Page.FindControl("hdnItemId_" + ItemCode.ID.Substring(ItemCode.ID.IndexOf('_') + 1, ItemCode.ID.Length - (ItemCode.ID.IndexOf('_') + 1))) as HiddenField;
        HtmlInputText UnitPrice = Page.FindControl("txtUnitPrice_" + ItemCode.ID.Substring(ItemCode.ID.IndexOf('_') + 1, ItemCode.ID.Length - (ItemCode.ID.IndexOf('_') + 1))) as HtmlInputText;
        HiddenField hdnMaxQty = Page.FindControl("hdnMaxQty_" + ItemCode.ID.Substring(ItemCode.ID.IndexOf('_') + 1, ItemCode.ID.Length - (ItemCode.ID.IndexOf('_') + 1))) as HiddenField;

        if (dsCust.Tables[0].Rows.Count > 0)
        {

            Desc1.Text = dsCust.Tables[0].Rows[0]["Description1"].ToString();
            Desc2.Text = dsCust.Tables[0].Rows[0]["Description2"].ToString();
            UnitPrice.Value = dsCust.Tables[0].Rows[0]["DlrPrice"].ToString();
            hdnItemId.Value = dsCust.Tables[0].Rows[0]["Itemid"].ToString();
            hdnMaxQty.Value = dsCust.Tables[0].Rows[0]["maxqty"].ToString();

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



    protected void txtItemCode_TextChanged(object sender, EventArgs e)
    {
        BindItem((TextBox)sender);
        TextBox ItemCode = (TextBox)sender;
        TextBox Quantity = Page.FindControl("txtQty_" + ItemCode.ID.Substring(ItemCode.ID.IndexOf('_') + 1, ItemCode.ID.Length - (ItemCode.ID.IndexOf('_') + 1))) as TextBox;
        HiddenField hdnMaxQty = Page.FindControl("hdnMaxQty_" + ItemCode.ID.Substring(ItemCode.ID.IndexOf('_') + 1, ItemCode.ID.Length - (ItemCode.ID.IndexOf('_') + 1))) as HiddenField;
        //Quantity.Text = hdnMaxQty.Value;     
    }
}
