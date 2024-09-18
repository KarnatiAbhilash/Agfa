using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BusinessClass;
using AjaxControlToolkit;
using System.Data;
using System;
using System.Collections.Generic;
using System.Activities.Expressions;

public partial class Masters_ApproverCustItemPrice : System.Web.UI.Page
{

    CustomerMasterClass objCust = new CustomerMasterClass();
    CommonFunction objComm = new CommonFunction();
    private DataSet dsItem;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null) Response.Redirect("../Logout.aspx");
        try
        {
            if (!IsPostBack)
            {
                CommonFunction.PopulateRecordsWithTwoParam("Common_Values", "Text", "Value", "FieldName", "Region", "Status", "1", "Text", this.ddlRegion, "ALL");
                lblMessage.Text = "";
                lblMessage.CssClass = "";
                hdnCurrentDate.Value = DateTime.Now.ToString(Session["DateFormat"].ToString());
                hdnDateFormat.Value = Session["DateFormat"].ToString();
                hdnDateFormat.Value = hdnDateFormat.Value.ToUpper();
               

                if (Request.QueryString["Type"] != null && Request.QueryString["Type"].ToString() != "")
                {
                    lblMessage.Text = "Saved/Updated Successfully.";
                    lblMessage.CssClass = "SuccessMessageBold";
                }
            }

            GenerateSearchModel(false);
            if (!IsPostBack && Session["UserID"] != null)
                BindData();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
            lblMessage.CssClass = "ErrorMessage";
        }
    }

    protected override void LoadViewState(object earlierState)
    {
        base.LoadViewState(earlierState);
        if (ViewState["dynamictable"] == null)
            GenerateSearchModel(false);
    }

    protected void BindData()
    {
        Table tbl = Page.FindControl("SearchTable") as Table;
        tbl.Controls.Clear();
        AddHeader();
        this.objCust.prpRegion = this.ddlRegion.SelectedItem.Value;
        objCust.prpUserId = Convert.ToString(Session["UserID"]);
        objCust.prpRegion = this.ddlRegion.SelectedItem.Value;
        dsItem = objCust.FetchApproverPendingCustItemList();
        for (int bindparse = 0; bindparse < dsItem.Tables[0].Rows.Count; bindparse++)
            CreateRow(bindparse + 1);

        hidSearchCount.Value = Convert.ToString(dsItem.Tables[0].Rows.Count);

        for (int bindparse = 0; bindparse < dsItem.Tables[0].Rows.Count; bindparse++)
        {
            int icount = bindparse + 1;
            HiddenField hdnItemId = Page.FindControl("hdnItemId_" + icount) as HiddenField;
            HiddenField hdnDIMId = Page.FindControl("hdnDIMId_" + icount) as HiddenField;
            HiddenField hdnCPMId = Page.FindControl("hdnCPMId_" + icount) as HiddenField;
            Label lblDealerName = Page.FindControl("lblDealerName_" + icount) as Label;
            Label lblCustomerName = Page.FindControl("lblCustomerName_" + icount) as Label;
            Label lblItemCode = Page.FindControl("lblItemCode_" + icount) as Label;
            Label lblDesc1 = Page.FindControl("lblDesc1_" + icount) as Label;
            Label lblDesc2 = Page.FindControl("lblDesc2_" + icount) as Label;
            Label lblConvFactor = Page.FindControl("lblConvFactor_" + icount) as Label;
            Label lblDlrPrice = Page.FindControl("lblDlrPrice_" + icount) as Label;
            Label lblDlrPerSqrMt = Page.FindControl("lblDlrPerSqrMt_" + icount) as Label;
            Label lblEUPrice = Page.FindControl("lblEUPrice_" + icount) as Label;
            Label lblEUPerSqrMt = Page.FindControl("lblEUPerSqrMt_" + icount) as Label;
            Label lblProfitActual = Page.FindControl("lblProfitActual_" + icount) as Label;
            Label lblProfitAgreed = Page.FindControl("lblProfitAgreed_" + icount) as Label;
            Label lblNewEUPrice = Page.FindControl("lblNewEUPrice_" + icount) as Label;
            Label lblNewEUPerSqrMt = Page.FindControl("lblNewEUPerSqrMt_" + icount) as Label;
            Label lblNewProfitActual = Page.FindControl("lblNewProfitActual_" + icount) as Label;
            Label lblNewProfitAgreed = Page.FindControl("lblNewProfitAgreed_" + icount) as Label;
            TextBox txtValidUpto = Page.FindControl("txtValidUpto_" + icount) as TextBox;          
            TextBox txtRemarks = Page.FindControl("txtRemarks_" + icount) as TextBox;


            hdnItemId.Value = dsItem.Tables[0].Rows[bindparse]["ItemId"].ToString();
            hdnDIMId.Value = dsItem.Tables[0].Rows[bindparse]["DIMId"].ToString();
            hdnCPMId.Value = dsItem.Tables[0].Rows[bindparse]["CPMId"].ToString();
            lblDealerName.Text = dsItem.Tables[0].Rows[bindparse]["DealerName"].ToString();
            lblCustomerName.Text = dsItem.Tables[0].Rows[bindparse]["CustName"].ToString();
            lblItemCode.Text = dsItem.Tables[0].Rows[bindparse]["ItemCode"].ToString();
            lblDesc1.Text = dsItem.Tables[0].Rows[bindparse]["Description1"].ToString();
            lblDesc2.Text = dsItem.Tables[0].Rows[bindparse]["Description2"].ToString();
            lblConvFactor.Text = dsItem.Tables[0].Rows[bindparse]["ConvFactor"].ToString();
            lblDlrPrice.Text = dsItem.Tables[0].Rows[bindparse]["DlrPrice"].ToString();
            lblDlrPerSqrMt.Text = dsItem.Tables[0].Rows[bindparse]["PerSqrMt"].ToString();
            lblEUPrice.Text = dsItem.Tables[0].Rows[bindparse]["EUPrice"].ToString();
            lblNewEUPrice.Text = dsItem.Tables[0].Rows[bindparse]["NewEUPrice"].ToString();
            lblEUPerSqrMt.Text = dsItem.Tables[0].Rows[bindparse]["EUPerSqrMt"].ToString();
            lblNewEUPerSqrMt.Text = dsItem.Tables[0].Rows[bindparse]["NewEUPerSqrMt"].ToString();
            lblProfitActual.Text = dsItem.Tables[0].Rows[bindparse]["ProfitActual"].ToString();
            lblNewProfitActual.Text = dsItem.Tables[0].Rows[bindparse]["NewProfitActual"].ToString();
            lblProfitAgreed.Text = dsItem.Tables[0].Rows[bindparse]["ProfitAgreed"].ToString();
            lblNewProfitAgreed.Text = dsItem.Tables[0].Rows[bindparse]["NewProfitAgreed"].ToString();

            if (dsItem.Tables[0].Rows[bindparse]["ValidUpto"] != null && dsItem.Tables[0].Rows[bindparse]["ValidUpto"].ToString() != "")
                txtValidUpto.Text = Convert.ToDateTime(dsItem.Tables[0].Rows[bindparse]["ValidUpto"].ToString()).ToString(Session["DateFormat"].ToString());
           
            txtRemarks.Text = dsItem.Tables[0].Rows[bindparse]["Remarks"].ToString();

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
        tc.Text = "Select";
        tc.HorizontalAlign = HorizontalAlign.Left;        
        tr.Cells.Add(tc);
        CheckBox chkall = new CheckBox();
        chkall.ID = "ChkAll";
        chkall.Attributes.Add("onclick", "CheckAll()");
        tc.Controls.Add(chkall);

        tc = new TableCell();
        tc.Text = "Delete";
        //Hidding the Cell
        tc.Style.Add("display", "none");
        tc.HorizontalAlign = HorizontalAlign.Left;
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Text = "Dealer Name";
        tc.HorizontalAlign = HorizontalAlign.Left;
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Text = "Customer Name";
        tc.HorizontalAlign = HorizontalAlign.Left;
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Text = "Item Code";
        tc.HorizontalAlign = HorizontalAlign.Left;
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Text = "Description1";
        tc.HorizontalAlign = HorizontalAlign.Left;
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Text = "Description2";
        tc.HorizontalAlign = HorizontalAlign.Left;
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Text = "Conv. Factor";
        tc.HorizontalAlign = HorizontalAlign.Left;
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Text = "DLR Price";
        tc.HorizontalAlign = HorizontalAlign.Left;
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Text = "DLR PerSqrMt";
        tc.HorizontalAlign = HorizontalAlign.Left;
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Text = "EU Price";
        tc.HorizontalAlign = HorizontalAlign.Left;
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Text = "New EU Price";
        tc.HorizontalAlign = HorizontalAlign.Left;
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Text = "EU PerSqrMt";
        tc.HorizontalAlign = HorizontalAlign.Left;
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Text = "New EU PerSqrMt";
        tc.HorizontalAlign = HorizontalAlign.Left;
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Text = "Profit Margin Actual (%)";
        tc.HorizontalAlign = HorizontalAlign.Left;
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Text = "New Profit Margin Actual (%)";
        tc.HorizontalAlign = HorizontalAlign.Left;
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Text = "Profit Margin Agreed (%)";
        tc.HorizontalAlign = HorizontalAlign.Left;
        tr.Cells.Add(tc);


        tc = new TableCell();
        tc.Text = "New Profit Margin Agreed (%)";
        tc.HorizontalAlign = HorizontalAlign.Left;
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Text = "Valid Upto";
        tc.HorizontalAlign = HorizontalAlign.Left;
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Text = "Remarks";
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
        CheckBox ChkItem = new CheckBox();
        ChkItem.ID = "ChkItem_" + SearchCount.ToString();
        ChkItem.Attributes.Add("onclick", "CheckChild()");
        tc.Controls.Add(ChkItem);

        tc = new TableCell();
        tr.Cells.Add(tc);
        //Hidding the Cell
        tc.Style.Add("display", "none");
        tc.HorizontalAlign = HorizontalAlign.Left;
        Button btnDelete = new Button();
        btnDelete.CssClass = "btn deletebtn";
        btnDelete.Width = Unit.Pixel(15);
        btnDelete.ID = "btnDelete_" + SearchCount.ToString();
        btnDelete.Attributes.Add("OnClick", "return confirm('Are You Sure You Want To Delete The Record?')");
      //  btnDelete.Click += new EventHandler(btnDelete_Click);
        HiddenField hdnItemId = new HiddenField();
        hdnItemId.ID = "hdnItemId_" + SearchCount.ToString();
        HiddenField hdnDIMId = new HiddenField();
        hdnDIMId.ID = "hdnDIMId_" + SearchCount.ToString();
        HiddenField hdnRow = new HiddenField();
        hdnRow.ID = "hdnRow_" + SearchCount.ToString();
        tc.Controls.Add(btnDelete);
        tc.Controls.Add(hdnItemId);
        tc.Controls.Add(hdnDIMId);
        tc.Controls.Add(hdnRow);

        tc = new TableCell();
        tr.Cells.Add(tc);
        tc.HorizontalAlign = HorizontalAlign.Left;
        Label lblDealerName = new Label();
        lblDealerName.CssClass = "commonfont textdropwidth";
        lblDealerName.ID = "lblDealerName_" + SearchCount.ToString();
        tc.Controls.Add(lblDealerName);

        tc = new TableCell();
        tr.Cells.Add(tc);
        tc.HorizontalAlign = HorizontalAlign.Left;
        Label lblCustomerName = new Label();
        lblCustomerName.CssClass = "commonfont textdropwidth";
        lblCustomerName.ID = "lblCustomerName_" + SearchCount.ToString();
        tc.Controls.Add(lblCustomerName);

        tc = new TableCell();
        tr.Cells.Add(tc);
        tc.HorizontalAlign = HorizontalAlign.Left;
        Label lblItemCode = new Label();
        lblItemCode.CssClass = "commonfont textdropwidth";
        lblItemCode.ID = "lblItemCode_" + SearchCount.ToString();
        HiddenField hdnCPMId = new HiddenField();
        hdnCPMId.ID = "hdnCPMId_" + SearchCount.ToString();
        tc.Controls.Add(lblItemCode);
        tc.Controls.Add(hdnCPMId);

        tc = new TableCell();
        tr.Cells.Add(tc);
        tc.HorizontalAlign = HorizontalAlign.Left;
        Label lblDesc1 = new Label();
        lblDesc1.CssClass = "commonfont textdropwidth";
        lblDesc1.ID = "lblDesc1_" + SearchCount.ToString();
        tc.Controls.Add(lblDesc1);

        tc = new TableCell();
        tr.Cells.Add(tc);
        tc.HorizontalAlign = HorizontalAlign.Left;
        Label lblDesc2 = new Label();
        lblDesc2.CssClass = "commonfont textdropwidth";
        lblDesc2.ID = "lblDesc2_" + SearchCount.ToString();
        tc.Controls.Add(lblDesc2);

        tc = new TableCell();
        tr.Cells.Add(tc);
        tc.HorizontalAlign = HorizontalAlign.Left;
        Label lblConvFactor = new Label();
        lblConvFactor.CssClass = "commonfont textdropwidth";
        lblConvFactor.ID = "lblConvFactor_" + SearchCount.ToString();
        tc.Controls.Add(lblConvFactor);

        tc = new TableCell();
        tr.Cells.Add(tc);
        tc.HorizontalAlign = HorizontalAlign.Left;
        Label lblDlrPrice = new Label();
        lblDlrPrice.CssClass = "commonfont textdropwidth";
        lblDlrPrice.ID = "lblDlrPrice_" + SearchCount.ToString();
        tc.Controls.Add(lblDlrPrice);

        tc = new TableCell();
        tr.Cells.Add(tc);
        tc.HorizontalAlign = HorizontalAlign.Left;
        Label lblDlrPerSqrMt = new Label();
        lblDlrPerSqrMt.CssClass = "commonfont textdropwidth";
        lblDlrPerSqrMt.ID = "lblDlrPerSqrMt_" + SearchCount.ToString();
        tc.Controls.Add(lblDlrPerSqrMt);

        tc = new TableCell();
        tr.Cells.Add(tc);
        tc.HorizontalAlign = HorizontalAlign.Left;
        Label lblEUPrice = new Label();
        lblEUPrice.CssClass = "commonfont textdropwidth";
        lblEUPrice.ID = "lblEUPrice_" + SearchCount.ToString();
        tc.Controls.Add(lblEUPrice);


        tc = new TableCell();
        tr.Cells.Add(tc);
        tc.HorizontalAlign = HorizontalAlign.Left;
        Label lblNewEUPrice = new Label();
        lblNewEUPrice.CssClass = "commonfont textdropwidth";
        lblNewEUPrice.BackColor = System.Drawing.Color.Yellow;
        lblNewEUPrice.ID = "lblNewEUPrice_" + SearchCount.ToString();
        tc.Controls.Add(lblNewEUPrice);


        tc = new TableCell();
        tr.Cells.Add(tc);
        tc.HorizontalAlign = HorizontalAlign.Left;
        Label lblEUPerSqMt = new Label();
        lblEUPerSqMt.CssClass = "commonfont textdropwidth";
        lblEUPerSqMt.ID = "lblEUPerSqrMt_" + SearchCount.ToString();
        tc.Controls.Add(lblEUPerSqMt);


        tc = new TableCell();
        tr.Cells.Add(tc);
        tc.HorizontalAlign = HorizontalAlign.Left;
        Label lblNewEUPerSqMt = new Label();
        lblNewEUPerSqMt.CssClass = "commonfont textdropwidth";
        lblNewEUPerSqMt.BackColor = System.Drawing.Color.Yellow;
        lblNewEUPerSqMt.ID = "lblNewEUPerSqrMt_" + SearchCount.ToString();
        tc.Controls.Add(lblNewEUPerSqMt);



        tc = new TableCell();
        tr.Cells.Add(tc);
        tc.HorizontalAlign = HorizontalAlign.Left;
        Label lblProfitActual = new Label();
        lblProfitActual.CssClass = "commonfont textdropwidth";
       
        lblProfitActual.ID = "lblProfitActual_" + SearchCount.ToString();
        tc.Controls.Add(lblProfitActual);

        tc = new TableCell();
        tr.Cells.Add(tc);
        tc.HorizontalAlign = HorizontalAlign.Left;
        Label lblNewProfitActual = new Label();
        lblNewProfitActual.CssClass = "commonfont textdropwidth";
        lblNewProfitActual.BackColor = System.Drawing.Color.Yellow;
        lblNewProfitActual.ID = "lblNewProfitActual_" + SearchCount.ToString();
        tc.Controls.Add(lblNewProfitActual);


        tc = new TableCell();
        tr.Cells.Add(tc);
        tc.HorizontalAlign = HorizontalAlign.Left;
        Label lblProfitAgreed = new Label();
        lblProfitAgreed.CssClass = "commonfont textdropwidth";
        lblProfitAgreed.ID = "lblProfitAgreed_" + SearchCount.ToString();
        tc.Controls.Add(lblProfitAgreed);


        tc = new TableCell();
        tr.Cells.Add(tc);
        tc.HorizontalAlign = HorizontalAlign.Left;
        Label lblNewProfitAgreed = new Label();
        lblNewProfitAgreed.CssClass = "commonfont textdropwidth";
        lblNewProfitAgreed.BackColor = System.Drawing.Color.Yellow;
        lblNewProfitAgreed.ID = "lblNewProfitAgreed_" + SearchCount.ToString();
        tc.Controls.Add(lblNewProfitAgreed);

        tc = new TableCell();
        tr.Cells.Add(tc);
        tc.HorizontalAlign = HorizontalAlign.Left;
        TextBox txtValidUpto = new TextBox();
        txtValidUpto.MaxLength = 12;
        txtValidUpto.CssClass = "commonfont textdropwidthFinesmall";
        txtValidUpto.ID = "txtValidUpto_" + SearchCount.ToString();
        ImageButton imgValidUpto = new ImageButton();
        imgValidUpto.ImageUrl = "~/Common/Images/datePickerPopup.gif";
        imgValidUpto.ID = "imgValidUpto_" + SearchCount.ToString();
        imgValidUpto.Style.Add("vertical-align", "bottom");
        CalendarExtender ceValid = new CalendarExtender();
        ceValid.ID = "ceValid_+" + SearchCount.ToString();
        ceValid.TargetControlID = txtValidUpto.ID;
        ceValid.PopupButtonID = imgValidUpto.ID;
        ceValid.CssClass = "CalendarDatePicker";
        ceValid.Format = Session["DateFormat"].ToString();
        tc.Controls.Add(txtValidUpto);
        tc.Controls.Add(imgValidUpto);
        tc.Controls.Add(ceValid);


        tc = new TableCell();
        tr.Cells.Add(tc);
        tc.HorizontalAlign = HorizontalAlign.Left;
        TextBox txtRemarks = new TextBox();
        txtRemarks.CssClass = "commonfont textdropwidth";
        txtRemarks.ID = "txtRemarks_" + SearchCount.ToString();
        txtRemarks.TextMode = TextBoxMode.MultiLine;
        txtRemarks.Rows = 3;
        txtRemarks.Width = Unit.Pixel(300);
        tc.Controls.Add(txtRemarks);
    }

    protected void btnRejected_Click(object sender, EventArgs e)
    {

        SetApproverStatus("Rejected");
    }
    protected void btnApproved_Click(object sender, EventArgs e)
    {
        SetApproverStatus("Approved");
    }


    private void SetApproverStatus(string strAStatus)
    {
        List<ApproverList> Ids = new List<ApproverList>();
        //string strMsg = "";
        Table tbl = Page.FindControl("SearchTable") as Table;

        try
        {
            objCust.prpUserId = Session["UserID"].ToString();

            for (int icount = 1; icount < tbl.Rows.Count; icount++)
            {
                CheckBox chkMain = Page.FindControl("ChkItem_" + icount) as CheckBox;
                if (chkMain.Checked)
                {

                    HiddenField hdnRow = Page.FindControl("hdnCPMId_" + icount) as HiddenField;
                    TextBox txtRemarks = Page.FindControl("txtRemarks_" + icount) as TextBox;
                    Ids.Add(new ApproverList()
                    {
                        Id = Convert.ToInt32(hdnRow.Value)
                    ,
                        Remarks = txtRemarks.Text
                    });
                }
            }

            if (Ids.Count == 0)
            {
                lblMessage.Text = "Please select atleast one item";
                lblMessage.CssClass = "ErrorMessage";
            }
            else
            {
                objCust.prpApproverStatus = strAStatus;
                objCust.SetApproverCustItemPriceStatus(Ids);
                Response.Redirect("ApproverCustItemPrice.aspx");
            }


        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
            lblMessage.CssClass = "ErrorMessage";
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        this.lblMessage.Text = "";
        this.lblMessage.CssClass = "";
        this.ddlRegion.SelectedIndex = 0;
        this.BindData();

    }
    protected void search_click(object sender, EventArgs e)
    {
        BindData();
    }
}