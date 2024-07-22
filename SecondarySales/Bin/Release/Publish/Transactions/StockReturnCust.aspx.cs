/* ==============================================================================================
    Created By      : Subahani SM
    Created Date    : 06 Sep 2010
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

public partial class Transactions_StockReturnCust : System.Web.UI.Page
{
    MonthEndClass objMonth = new MonthEndClass();
    StockReturnClass objStock = new StockReturnClass();
    CommonFunction objComm = new CommonFunction();
    CustomerMasterClass objCust = new CustomerMasterClass();
    IssueMaster objIssue = new IssueMaster();
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
                hdnDlrCode.Value = Session["DealerCode"].ToString();
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
        btnItem.Attributes.Add("onclick", "popupItem('StockRetCust','" + SearchCount.ToString() + "','" + Session["DealerCode"].ToString() + "','" + ddlSalesType.SelectedValue + "','" + hdnCustCode.Value + "')");
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
        txtReturnQty.CssClass = "commonfont textdropwidthUOM right";
        txtReturnQty.ID = "txtReturnQty_" + SearchCount.ToString();
        tc.Controls.Add(txtReturnQty);

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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string strMsg = "";
        Table tbl = Page.FindControl("SearchTable") as Table;
        //objIssue.fet
        try
        {
            if (Validate())
            {
                objStock.prpUserId = Session["UserID"].ToString();
                objStock.prpDealerCode = Convert.ToInt32(Session["DealerCode"].ToString());
                objStock.prpUserType = "Customer";
                objStock.prpCustCode = Convert.ToInt32(hdnCustCode.Value.Trim());
                objStock.prpSalesType = ddlSalesType.SelectedValue;
                objStock.prpInvoiceNo = ddlInvoceNo.SelectedValue;
                objStock.prpMonth = txtMonth.Text.Trim();
                objStock.prpYear = Convert.ToInt32(txtYear.Text.Trim());
                objStock.prpReturnDate = objComm.DateFormateConversion(txtReturnDate.Text, Session["DateFormat"].ToString());
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
                        if (BindItem(txtItemCode))
                        {

                            if (hdnRow.Value == "S")
                                continue;

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
        Response.Redirect("StockReturnCust.aspx");
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
    protected void btnGetdata_Click(object sender, EventArgs e)
    {
        BindInvoice();
        GenerateSearchModel(true);
        CreateRow(1);
        hidSearchCount.Value = (1).ToString();
    }
    protected void BindInvoice()
    {
        ddlInvoceNo.Items.Clear();
        if (hdnCustCode.Value != "" && hdnDlrCode.Value != "")
        {
            DataSet ds = CommonFunction.FetchRecordsWithQuery("select InvoiceNo from Issue where DealerCode=" + hdnDlrCode.Value.Trim() + " and CustCode=" + hdnCustCode.Value.Trim(), "InvoiceNo");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlInvoceNo.DataSource = ds;
                ddlInvoceNo.DataTextField = "InvoiceNo";
                ddlInvoceNo.DataValueField = "InvoiceNo";
                ddlInvoceNo.DataBind();
            }
            ddlInvoceNo.Items.Insert(0, new ListItem("<-- Select One -->", "0"));
        }
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
            Desc1.Text = dsCust.Tables[0].Rows[0][3].ToString();
            Desc2.Text = dsCust.Tables[0].Rows[0][4].ToString();
            hdnItemId.Value = dsCust.Tables[0].Rows[0][1].ToString();
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

    protected bool ValidateCustomerCode(string custCode)
    {
        string strSearchQuery = " where Status=1 and CustCode='" + custCode + "'and DealerCode='" + hdnDlrCode.Value.Trim() + "'";
        DataSet dsDealer = objCust.FetchCustomerMaster(strSearchQuery);
        if (dsDealer.Tables[0].Rows.Count > 0)
        {
            hdnCustCode.Value = txtCustCode.Text;
            lblMessage.Text = "";
            lblMessage.CssClass = "";
            return true;
        }
        else
        {
            ddlInvoceNo.Items.Clear();
            lblMessage.Text = "Cust.Code Not Found.";
            lblMessage.CssClass = "ErrorMessage";
            return false;
        }
    }
    protected void txtCustCode_TextChanged(object sender, EventArgs e)
    {
        ValidateCustomerCode(txtCustCode.Text);
        btnGetdata_Click(btnGetdata, null);
    }

    public bool Validate()
    {

        Hashtable IssueHash = new Hashtable();
        try
        {
            DataSet ds = CommonFunction.FetchRecordsWithQuery("select ItemID,(select distinct Itemcode from itemMAster where id=IssDet.ItemID)Itemcode,Qty from issuedetails IssDet inner join  issue on IssDet.IssueNo=issue.IssueNo  where DealerCode=" + hdnDlrCode.Value.Trim() + " and CustCode=" + hdnCustCode.Value.Trim()+" And Issue.InvoiceNo="+ddlInvoceNo.SelectedItem.Text.Trim(), "InvoiceNo");
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                 if(!IssueHash.Contains(ds.Tables[0].Rows[i]["Itemcode"].ToString().ToLower()))
                    IssueHash.Add(ds.Tables[0].Rows[i]["Itemcode"].ToString().ToLower(), ds.Tables[0].Rows[i]["Qty"]);
                }
            }

            int intMonth = Convert.ToInt16(hdnIntMonth.Value);
            if (intMonth != 0)
            {
                string[] sd = txtReturnDate.Text.Split('/');
                if (int.Parse(sd[1]) != intMonth)
                {
                    lblMessage.Text = "Enter the Return Date With in This Month";
                    lblMessage.CssClass = "ErrorMessage";
                    return false;
                }
            }
            int issueQty, returnQty;
            int hdnCount = int.Parse(hidSearchCount.Value);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                TextBox txtItemCode = (TextBox)Page.FindControl("txtItemCode_" + (i + 1));
                HiddenField hdnItemCode = (HiddenField)Page.FindControl("hdnItemId_" + (i + 1));

                if (IssueHash[txtItemCode.Text.Trim().ToLower()] != null)
                {
                    returnQty = int.Parse(((TextBox)Page.FindControl("txtReturnQty_" + (i + 1))).Text);
                    issueQty = int.Parse(IssueHash[txtItemCode.Text.Trim().ToLower()].ToString());
                }
                else
                {
                    lblMessage.Text = "ItemCode Not Found In Issued Items.";
                    lblMessage.CssClass = "ErrorMessage";
                    return false;
                }

                int PrevReturnQty = 0;
                //get the Sum of Already Existing Stock REturn Qty for the InvoiceNo
                ds = CommonFunction.FetchRecordsWithQuery("select sum(ReturnQty) as TotQty ,InvoiceNo  from StockReturn Stk inner join StockReturnDetails  StkDet on StkDet.returnid=Stk.returnid  where DealerCode=" + hdnDlrCode.Value.Trim() + " and CustCode=" + hdnCustCode.Value.Trim() + " and InvoiceNo='" + ddlInvoceNo.Text.Trim() + "' and ItemID='" + hdnItemCode.Value.Trim() + "' group by InvoiceNo", "InvoiceNo");
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        PrevReturnQty = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                    }
                }

                if (issueQty <= PrevReturnQty)
                {
                    lblMessage.Text = "Stock Return For the Item:" + txtItemCode.Text + " For InvoicNo:" + ddlInvoceNo.Text.Trim() + " is Already Closed..";
                    lblMessage.CssClass = "ErrorMessage";
                    return false;
                }

                if (IssueHash[txtItemCode.Text.Trim().ToLower()] != null)
                {
                    if ((returnQty + PrevReturnQty) > issueQty)
                    {
                        lblMessage.Text = "Check if you have aleardy made the StockReturn For the Item:" + txtItemCode.Text + " For InvoicNo:" + ddlInvoceNo.Text.Trim() + " /  ReturnQty Should Not be Greater than IssueQty: " + issueQty;
                        lblMessage.CssClass = "ErrorMessage";
                        txtItemCode.Focus();
                        return false;
                    }
                }
                else
                {
                    lblMessage.Text = "ItemCode Not Found In Issued Items.";
                    lblMessage.CssClass = "ErrorMessage";
                    return false;
                }


            }



            ds = CommonFunction.FetchRecordsWithQuery("select top 1 convert(Varchar(10),InvoiceDate,101)InvoiceDate from Issue where DealerCode=" + hdnDlrCode.Value.Trim() + " and CustCode=" + hdnCustCode.Value.Trim() + " and InvoiceNo='" + ddlInvoceNo.Text.Trim() + "'", "InvoiceNo");
            DateTime issueDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["InvoiceDate"]);
            DateTime stockRetDate = DateTime.ParseExact(txtReturnDate.Text.Split('/')[1].Trim() + "/" + txtReturnDate.Text.Split('/')[0].Trim() + "/" + txtReturnDate.Text.Split('/')[2].Trim(), "d", null);
            if ((stockRetDate.Subtract(issueDate)).Days < 0)
            {
                lblMessage.Text = "Stock-ReturnDate Must Be Greater than Or Equal To IssueDate.";
                lblMessage.CssClass = "ErrorMessage";
                return false;
            }

        }
        catch (Exception Ex)
        {
            lblMessage.Text = Ex.Message;
            lblMessage.CssClass = "ErrorMessage";
            return false;
        }
        return true;
    }
}

