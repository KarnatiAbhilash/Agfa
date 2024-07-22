/* ==============================================================================================
    Created By      : Subahani SM
    Created Date    : 05 Aug 2010
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

public partial class Pricing_PriceListChange : System.Web.UI.Page
{
    PriceListClass objPrice = new PriceListClass();
    BudgetClassMaster objBudget = new BudgetClassMaster();
    ProductFamilyClass objProb = new ProductFamilyClass();
    ProductGroupClass objGroup = new ProductGroupClass();
    DealerMasterClass objDealer = new DealerMasterClass();
    CustomerMasterClass objCust = new CustomerMasterClass();
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
                txtEffectiveDate.Text = DateTime.Now.ToString(Session["DateFormat"].ToString());
                CommonFunction.PopulateRecordsWithTwoParam("Common_Values", "Text", "Value", "FieldName", "PriceType", "Status", "1", "Id", ddlPriceType,"ALL");

                if (Request.QueryString["Type"] != null && Request.QueryString["Type"].ToString() != "")
                {
                    lblMessage.Text = "Saved/Updated Successfully.";
                    lblMessage.CssClass = "SuccessMessageBold";
                }
            }
            GenerateSearchModel(false);
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
            lblMessage.CssClass = "ErrorMessage";
        }
    }
    protected void BindData()
    {
        TextBox txtDlrPrice = new TextBox();
        TextBox txtDlrPerSqrMt = new TextBox();
        TextBox txtEUPrice =new TextBox();
        TextBox txtEUPerSqMt = new TextBox();
        TextBox txtProfitActual = new TextBox();
        TextBox txtProfitAgreed = new TextBox();
        string strCondition = "";
        Table tbl = Page.FindControl("SearchTable") as Table;
        tbl.Controls.Clear();
        AddHeader();

        //if (txtDealer.Value.Trim() != "")
        //    strCondition = " Where DP.DealerCode=" + txtDealer.Value.Trim();

        //if (txtCustCode.Value.Trim() != "")
        //{
        //    if (strCondition == "")
        //        strCondition = " Where CP.CustCode=" + txtCustCode.Value.Trim();
        //    else
        //        strCondition = strCondition + " and CP.CustCode=" + txtCustCode.Value.Trim();
        //}

        objPrice.prpDealerCode = Convert.ToInt32(txtDealer.Text.Trim());

        if (txtCustCode.Text.Trim() != "")
            objPrice.prpCustCode = Convert.ToInt32(txtCustCode.Text.Trim());
        else
            objPrice.prpCustCode=0;

        if (txtBudgetCode.Text.Trim() != "")
        {
            if (strCondition == "")
                strCondition = " and IM.BCCode='" + txtBudgetCode.Text.Trim() + "'";
            else
                strCondition = strCondition + " and IM.BCCode='" + txtBudgetCode.Text.Trim() + "'";
        }

        if (txtProdFamily.Text.Trim() != "")
        {
            if (strCondition == "")
                strCondition = " and IM.PFCode='" + txtProdFamily.Text.Trim() + "'";
            else
                strCondition = strCondition + " and IM.PFCode='" + txtProdFamily.Text.Trim() + "'";
        }

        if (txtProdGroup.Text.Trim() != "")
        {
            if (strCondition == "")
                strCondition = " and IM.GroupCode='" + txtProdGroup.Text.Trim() + "'";
            else
                strCondition = strCondition + " and IM.GroupCode='" + txtProdGroup.Text.Trim() + "'";
        }        

        dsItem = objPrice.FetchPriceList(strCondition);

        for (int bindparse = 0; bindparse < dsItem.Tables[0].Rows.Count; bindparse++)
            CreateRow(bindparse + 1);

        hidSearchCount.Value = Convert.ToString(dsItem.Tables[0].Rows.Count);

        if (dsItem.Tables[0].Rows.Count > 0)
            btnSave.Enabled = true;
        else
            btnSave.Enabled = false;

        for (int bindparse = 0; bindparse < dsItem.Tables[0].Rows.Count; bindparse++)
        {
            int icount = bindparse + 1;
            HiddenField hdnItemId = Page.FindControl("hdnItemId_" + icount) as HiddenField;
            HiddenField hdnDIMId = Page.FindControl("hdnDIMId_" + icount) as HiddenField;
            HiddenField hdnCPMId = Page.FindControl("hdnCPMId_" + icount) as HiddenField;
            Label lblItemCode = Page.FindControl("lblItemCode_" + icount) as Label;
            Label lblDesc1 = Page.FindControl("lblDesc1_" + icount) as Label;
            Label lblDesc2 = Page.FindControl("lblDesc2_" + icount) as Label;
            Label lblConvFactor = Page.FindControl("lblConvFactor_" + icount) as Label;

            if (ddlPriceType.SelectedValue.ToLower() != "eu price")
            {
                 txtDlrPrice = Page.FindControl("txtDlrPrice_" + icount) as TextBox;
                 txtDlrPerSqrMt = Page.FindControl("txtDlrPerSqrMt_" + icount) as TextBox;

                 txtDlrPrice.Text = dsItem.Tables[0].Rows[bindparse]["DlrPrice"].ToString();
                 txtDlrPerSqrMt.Text = dsItem.Tables[0].Rows[bindparse]["PerSqrMt"].ToString();
            }
            if (txtCustCode.Text.Trim() != "")
            {
                if (ddlPriceType.SelectedValue.ToLower() != "dealer price")
                {
                    txtEUPrice = Page.FindControl("txtEUPrice_" + icount) as TextBox;
                    txtEUPerSqMt = Page.FindControl("txtEUPerSqMt_" + icount) as TextBox;
                    txtProfitActual = Page.FindControl("txtProfitActual_" + icount) as TextBox;
                    txtProfitAgreed = Page.FindControl("txtProfitAgreed_" + icount) as TextBox;

                    txtEUPrice.Text = dsItem.Tables[0].Rows[bindparse]["EUPrice"].ToString();
                    txtEUPerSqMt.Text = dsItem.Tables[0].Rows[bindparse]["EUPerSqrMt"].ToString();
                    txtProfitActual.Text = dsItem.Tables[0].Rows[bindparse]["ProfitActual"].ToString();
                    txtProfitAgreed.Text = dsItem.Tables[0].Rows[bindparse]["ProfitAgreed"].ToString();
                }
            }
            TextBox txtRemarks = Page.FindControl("txtRemarks_" + icount) as TextBox;

            hdnItemId.Value = dsItem.Tables[0].Rows[bindparse]["ItemId"].ToString();
            hdnDIMId.Value = dsItem.Tables[0].Rows[bindparse]["DIMId"].ToString();
            hdnCPMId.Value = dsItem.Tables[0].Rows[bindparse]["CPMId"].ToString();
            lblItemCode.Text = dsItem.Tables[0].Rows[bindparse]["ItemCode"].ToString();
            lblDesc1.Text = dsItem.Tables[0].Rows[bindparse]["Description1"].ToString();
            lblDesc2.Text = dsItem.Tables[0].Rows[bindparse]["Description2"].ToString();
            lblConvFactor.Text = dsItem.Tables[0].Rows[bindparse]["ConvFactor"].ToString();          
            txtRemarks.Text = dsItem.Tables[0].Rows[bindparse]["dlrRemarks"].ToString();
        }
    }
    protected void btnGetdata_Click(object sender, EventArgs e)
    {
        BindData();
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
        //Table-Cell is Made Hidden
        tc.Style.Add("display", "none");
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

        if (ddlPriceType.SelectedValue.ToLower() != "eu price")
        {
            tc = new TableCell();
            tc.Text = "DLR Price";
            tc.HorizontalAlign = HorizontalAlign.Left;
            tr.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = "DLR PerSqrMt";
            tc.HorizontalAlign = HorizontalAlign.Left;
            tr.Cells.Add(tc);

        }
        if (txtCustCode.Text.Trim() != "")
        {
            if (ddlPriceType.SelectedValue.ToLower() != "dealer price")
            {
                tc = new TableCell();
                tc.Text = "EU Price";
                tc.HorizontalAlign = HorizontalAlign.Left;
                tr.Cells.Add(tc);

                tc = new TableCell();
                tc.Text = "EU PerSqrMt";
                tc.HorizontalAlign = HorizontalAlign.Left;
                tr.Cells.Add(tc);

                tc = new TableCell();
                tc.Text = "Profit Margin Actual (%)";
                tc.HorizontalAlign = HorizontalAlign.Left;
                tr.Cells.Add(tc);

                tc = new TableCell();
                tc.Text = "Profit Margin Agreed (%)";
                tc.HorizontalAlign = HorizontalAlign.Left;
                tr.Cells.Add(tc);
            }
        }

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
        //Table-Cell is Made Hidden
        tc.Style.Add("display", "none");
        tc.HorizontalAlign = HorizontalAlign.Left;
        Button btnDelete = new Button();
        btnDelete.CssClass = "btn deletebtn";
        btnDelete.Width = Unit.Pixel(15);
        btnDelete.ID = "btnDelete_" + SearchCount.ToString();
        btnDelete.Attributes.Add("OnClick", "return confirm('Are You Sure You Want To Delete The Record?')");
        btnDelete.Click += new EventHandler(btnDelete_Click);
        HiddenField hdnItemId = new HiddenField();
        hdnItemId.ID = "hdnItemId_" + SearchCount.ToString();
        HiddenField hdnDIMId = new HiddenField();
        hdnDIMId.ID = "hdnDIMId_" + SearchCount.ToString();
        tc.Controls.Add(btnDelete);
        tc.Controls.Add(hdnItemId);
        tc.Controls.Add(hdnDIMId);

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

        if (ddlPriceType.SelectedValue.ToLower() != "eu price")
        {
            tc = new TableCell();
            tr.Cells.Add(tc);
            tc.HorizontalAlign = HorizontalAlign.Left;
            TextBox txtDlrPrice = new TextBox();
            txtDlrPrice.MaxLength = 12;
            txtDlrPrice.CssClass = "commonfont textdropwidthUOM right";
            txtDlrPrice.ID = "txtDlrPrice_" + SearchCount.ToString();
            txtDlrPrice.Attributes.Add("onBlur", "CalcDlrPerSqrMt('" + SearchCount + "')");
            tc.Controls.Add(txtDlrPrice);

            tc = new TableCell();
            tr.Cells.Add(tc);
            tc.HorizontalAlign = HorizontalAlign.Left;
            TextBox txtDlrPerSqrMt = new TextBox();
            txtDlrPerSqrMt.MaxLength = 12;
            txtDlrPerSqrMt.CssClass = "commonfont textdropwidthUOM right";
            txtDlrPerSqrMt.ID = "txtDlrPerSqrMt_" + SearchCount.ToString();
            txtDlrPerSqrMt.Attributes.Add("onBlur", "CalcDlrPrice('" + SearchCount + "')");
            tc.Controls.Add(txtDlrPerSqrMt);
        }

        if (txtCustCode.Text.Trim() != "")
        {
            if (ddlPriceType.SelectedValue.ToLower() != "dealer price")
            {
                tc = new TableCell();
                tr.Cells.Add(tc);
                tc.HorizontalAlign = HorizontalAlign.Left;
                TextBox txtEUPrice = new TextBox();
                txtEUPrice.MaxLength = 12;
                txtEUPrice.CssClass = "commonfont textdropwidthUOM right";
                txtEUPrice.ID = "txtEUPrice_" + SearchCount.ToString();
                txtEUPrice.Attributes.Add("onBlur", "CalcEUPerSqrMt('" + SearchCount + "')");
                tc.Controls.Add(txtEUPrice);

                tc = new TableCell();
                tr.Cells.Add(tc);
                tc.HorizontalAlign = HorizontalAlign.Left;
                TextBox txtEUPerSqMt = new TextBox();
                txtEUPerSqMt.MaxLength = 12;                
                txtEUPerSqMt.CssClass = "commonfont textdropwidthUOM right";
                txtEUPerSqMt.ID = "txtEUPerSqMt_" + SearchCount.ToString();
                txtEUPerSqMt.Attributes.Add("onBlur", "CalcEUPrice('" + SearchCount + "')");
                tc.Controls.Add(txtEUPerSqMt);

                tc = new TableCell();
                tr.Cells.Add(tc);
                tc.HorizontalAlign = HorizontalAlign.Left;
                TextBox txtProfitActual = new TextBox();
                txtProfitActual.MaxLength = 12;              
                txtProfitActual.CssClass = "commonfont textdropwidthUOM right";
                txtProfitActual.ID = "txtProfitActual_" + SearchCount.ToString();
                txtProfitActual.Attributes.Add("onBlur", "CalcProfitActual('" + SearchCount + "')");
                //txtProfitActual.ReadOnly = true;        
                tc.Controls.Add(txtProfitActual);

                tc = new TableCell();
                tr.Cells.Add(tc);
                tc.HorizontalAlign = HorizontalAlign.Left;
                TextBox txtProfitAgreed = new TextBox();
                txtProfitAgreed.MaxLength = 12;              
                txtProfitAgreed.CssClass = "commonfont textdropwidthUOM right";
                txtProfitAgreed.ID = "txtProfitAgreed_" + SearchCount.ToString();
                tc.Controls.Add(txtProfitAgreed);
            }
        }

        tc = new TableCell();
        tr.Cells.Add(tc);
        tc.HorizontalAlign = HorizontalAlign.Left;
        TextBox txtRemarks = new TextBox();
        txtRemarks.CssClass = "commonfont textdropwidth";
        txtRemarks.MaxLength = 100;
        txtRemarks.ID = "txtRemarks_" + SearchCount.ToString();
        txtRemarks.TextMode = TextBoxMode.MultiLine;
        txtRemarks.Rows = 3;
        txtRemarks.Width = Unit.Pixel(300);
        tc.Controls.Add(txtRemarks);
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            Table tbl = Page.FindControl("SearchTable") as Table;
            int index = 0;
            Button btn = sender as Button;

            string strID = btn.ID.Replace("btnDelete_", "");
            index = int.Parse(strID);
            int SearchCount = int.Parse(hidSearchCount.Value);

            HiddenField hdnCPMId = Page.FindControl("hdnCPMId_" + strID) as HiddenField;
            string strCPMId = hdnCPMId.Value;

            TableRow tr = Page.FindControl("tr_" + strID) as TableRow;
            tr.Visible = false;

            //hidSearchCount.Value = Convert.ToString(SearchCount - 1);

            lblMessage.Text = "Deleted Successfully.";
            lblMessage.CssClass = "SuccessMessageBold";
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message; ;
            lblMessage.CssClass = "ErrorMessage";
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string strMsg = "";
        Table tbl = Page.FindControl("SearchTable") as Table;

        TextBox txtDlrPrice = new TextBox();
        TextBox txtDlrPerSqrMt = new TextBox();
        TextBox txtEUPrice = new TextBox();
        TextBox txtEUPerSqMt = new TextBox();
        TextBox txtProfitActual = new TextBox();
        TextBox txtProfitAgreed = new TextBox();

        try
        {
            objPrice.prpUserId = Session["UserID"].ToString();
            if (ValidateDealerCode(false) && ValidateCustomerCode(txtCustCode.Text,false) && ValidateBudgetClassCode(false) && ValidateProductFamilyCode(false) && ValidateProductGroupCode(false))
            {
            for (int icount = 1; icount < tbl.Rows.Count; icount++)
            {
                TableRow tr = Page.FindControl("tr_" + icount) as TableRow;
                if (!tr.Visible)
                    continue;

                HiddenField hdnDIMId = Page.FindControl("hdnDIMId_" + icount) as HiddenField;
                HiddenField hdnCPMId = Page.FindControl("hdnCPMId_" + icount) as HiddenField;
                if (ddlPriceType.SelectedValue.ToLower() != "eu price")
                {
                    txtDlrPrice = Page.FindControl("txtDlrPrice_" + icount) as TextBox;
                    txtDlrPerSqrMt = Page.FindControl("txtDlrPerSqrMt_" + icount) as TextBox;

                    objPrice.prpDlrPrice = Convert.ToDouble(txtDlrPrice.Text.Trim());
                    objPrice.prpPerSqrmt = Convert.ToDouble(txtDlrPerSqrMt.Text.Trim());
                    objPrice.prpDMIId = Convert.ToInt32(hdnDIMId.Value.Trim());
                    objPrice.prpCPMId = 0;
                }

                if (txtCustCode.Text.Trim() != "")
                {
                    if (ddlPriceType.SelectedValue.ToLower() != "dealer price")
                    {
                        txtEUPrice = Page.FindControl("txtEUPrice_" + icount) as TextBox;
                        txtEUPerSqMt = Page.FindControl("txtEUPerSqMt_" + icount) as TextBox;
                        txtProfitActual = Page.FindControl("txtProfitActual_" + icount) as TextBox;
                        txtProfitAgreed = Page.FindControl("txtProfitAgreed_" + icount) as TextBox;

                        objPrice.prpEuPrice = Convert.ToDouble(txtEUPrice.Text.Trim());
                        objPrice.prpEUPerSqrmt = Convert.ToDouble(txtEUPerSqMt.Text.Trim());
                        objPrice.prpProfitActual = Convert.ToDouble(txtProfitActual.Text.Trim());
                        objPrice.prpProfitAgreed = Convert.ToDouble(txtProfitAgreed.Text.Trim());
                        objPrice.prpDMIId = 0;
                        if (hdnCPMId.Value != "")
                            objPrice.prpCPMId = Convert.ToInt32(hdnCPMId.Value.Trim());
                        else
                            objPrice.prpCPMId = 0;
                    }
                }
                TextBox txtRemarks = Page.FindControl("txtRemarks_" + icount) as TextBox;

                if (ddlPriceType.SelectedValue == "0")
                {
                    objPrice.prpDMIId = Convert.ToInt32(hdnDIMId.Value.Trim());
                    if (hdnCPMId.Value != "")
                        objPrice.prpCPMId = Convert.ToInt32(hdnCPMId.Value.Trim());
                    else
                        objPrice.prpCPMId = 0;
                }

                objPrice.prpRemarks = txtRemarks.Text.Trim();

                strMsg = objPrice.SavePriceChangeList();
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
                     
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
            lblMessage.CssClass = "ErrorMessage";
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("PriceListChange.aspx");
    }
    protected void ddlPriceType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        lblMessage.CssClass = "";

        if (txtDealer.Text.Trim() != "")
            BindData();
        
    }
    private bool ValidateBudgetClassCode(bool IsBind)
    {
        if (txtBudgetCode.Text == "")
            return true;

        string strSearchQuery = " where Status=1 and BCCode='" + txtBudgetCode.Text.Trim() + "'";
        DataSet dsBudget = objBudget.FetchBudgetClass(strSearchQuery);

        if (dsBudget.Tables[0].Rows.Count > 0)
        {
            if(IsBind)
            BindData();
            lblMessage.Text = "";
            lblMessage.CssClass = "";
            return true;
        }
        else
        {
            SearchTable.Controls.Clear();
            AddHeader();
            lblMessage.Text = "BudgetClassCode Not Found.";
            lblMessage.CssClass = "ErrorMessage";
            return false;
        }
    }

    private bool ValidateProductFamilyCode(bool IsBind)
    {
        if (txtProdFamily.Text.Trim() == "")
            return true;

        string strSearchQuery = " where Status=1 and PFCode='" + txtProdFamily.Text.Trim() + "' and BCCode='" + txtBudgetCode.Text.Trim() + "'";

        DataSet dsProd = objProb.FetchProductFamily(strSearchQuery);

        if (dsProd.Tables[0].Rows.Count > 0)
        {
            if(IsBind)
            BindData();
            lblMessage.Text = "";
            lblMessage.CssClass = "";
            return true;
        }
        else
        {

            SearchTable.Controls.Clear();
            AddHeader();
            lblMessage.Text = "ProductFamilyCode Not Found.";
            lblMessage.CssClass = "ErrorMessage";
            return false;
        }

    }

    protected bool ValidateProductGroupCode(bool IsBind)
    {
        if (txtProdGroup.Text.Trim() == "")
            return true;

        string strSearchQuery = " where Status=1 and GroupCode='" + txtProdGroup.Text.Trim() + "' and BCCode='" + txtBudgetCode.Text.Trim() + "' and PFCode='" + txtProdFamily.Text.Trim() + "'";
        DataSet dsGroup = objGroup.FetchProductGroup(strSearchQuery);

        if (dsGroup.Tables[0].Rows.Count > 0)
        {
            if(IsBind)
            BindData();
            lblMessage.Text = "";
            lblMessage.CssClass = "";
            return true;
        }
        else
        {

            SearchTable.Controls.Clear();
            AddHeader();
            lblMessage.Text = "ProductGroupCode Not Found.";
            lblMessage.CssClass = "ErrorMessage";
            return false;
        }
    }
    protected void txtBudgetCode_TextChanged(object sender, EventArgs e)
    {
        ValidateBudgetClassCode(true);
    }
    protected void txtProdFamily_TextChanged(object sender, EventArgs e)
    {
        ValidateProductFamilyCode(true);
    }
    protected void txtProdGroup_TextChanged(object sender, EventArgs e)
    {
        ValidateProductGroupCode(true);
    }
    protected void txtCustCode_TextChanged(object sender, EventArgs e)
    {
        ValidateCustomerCode(txtCustCode.Text.Trim(),true);
    }
    protected void txtDealer_TextChanged(object sender, EventArgs e)
    {
        ValidateDealerCode(true);
    }
    protected bool ValidateDealerCode(bool IsBind)
    {
        string strSearchQuery = " where Status=1 and DealerCode='" + txtDealer.Text.Trim() + "'";
        DataSet dsDealer = objDealer.FetchDealerMaster(strSearchQuery);
        if (dsDealer.Tables[0].Rows.Count > 0)
        {
            txtDealerName.Value = dsDealer.Tables[0].Rows[0][3].ToString();
            if(IsBind)
            BindData();
            lblMessage.Text = "";
            lblMessage.CssClass = "";
            return true;
        }
        else
        {

            SearchTable.Controls.Clear();
            AddHeader();
            lblMessage.Text = "DealerCode Not Found.";
            lblMessage.CssClass = "ErrorMessage";
            return false;
        }
    }

    protected bool ValidateCustomerCode(string custCode,bool IsBind)
    {
        if (custCode == "")
            return true;

        string strSearchQuery = " where Status=1 and CustCode='" + custCode + "'and DealerCode='"+ txtDealer.Text.Trim() +"'";
        DataSet dsDealer = objCust.FetchCustomerMaster(strSearchQuery);
        if (dsDealer.Tables[0].Rows.Count > 0)
        {
            txtCustName.Value = dsDealer.Tables[0].Rows[0]["CustName"].ToString();            
            if(IsBind)
            BindData();
            lblMessage.Text = "";
            lblMessage.CssClass = "";
            return true;
        }
        else
        {            
            SearchTable.Controls.Clear();
            AddHeader();
            lblMessage.Text = "Cust.Code Not Found.";
            lblMessage.CssClass = "ErrorMessage";
            txtCustName.Value = "";
            return false;
        }
    }

}
