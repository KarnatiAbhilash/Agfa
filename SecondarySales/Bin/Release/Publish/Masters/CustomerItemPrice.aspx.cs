/* ==============================================================================================
    Created By      : Subahani SM
    Created Date    : 30 July 2010
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
using AjaxControlToolkit;

public partial class Masters_CustomerItemPrice : System.Web.UI.Page
{
    CustomerMasterClass objCust = new CustomerMasterClass();
    CommonFunction objComm = new CommonFunction();
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
                hdnCurrentDate.Value = DateTime.Now.ToString(Session["DateFormat"].ToString());
                hdnDateFormat.Value = Session["DateFormat"].ToString();
                hdnDateFormat.Value = hdnDateFormat.Value.ToUpper();

                lblDealerCode.Text = Request.QueryString["DlrCode"].ToString();
                lblCustCode.Text = Request.QueryString["CustCode"].ToString();
                lblCustName.Text = Request.QueryString["CustName"].ToString();

                if (Request.QueryString["Type"] != null && Request.QueryString["Type"].ToString() != "")
                {
                    lblMessage.Text = "Saved/Updated Successfully.";
                    lblMessage.CssClass = "SuccessMessageBold";
                }
            }

            GenerateSearchModel(false);
            if (!IsPostBack && Request.QueryString["CustCode"] != null)
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
        objCust.prpDealerCode = Convert.ToInt32(lblDealerCode.Text.Trim());
        objCust.prpCustCode = Convert.ToInt32(lblCustCode.Text.Trim());
        dsItem = objCust.FetchCustItemList();
        for (int bindparse = 0; bindparse < dsItem.Tables[0].Rows.Count; bindparse++)
            CreateRow(bindparse + 1);

        hidSearchCount.Value = Convert.ToString(dsItem.Tables[0].Rows.Count);

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
            Label lblDlrPrice = Page.FindControl("lblDlrPrice_" + icount) as Label;
            Label lblDlrPerSqrMt = Page.FindControl("lblDlrPerSqrMt_" + icount) as Label;
            TextBox txtEUPrice = Page.FindControl("txtEUPrice_" + icount) as TextBox;
            TextBox txtEUPerSqMt = Page.FindControl("txtEUPerSqMt_" + icount) as TextBox;
            TextBox txtProfitActual = Page.FindControl("txtProfitActual_" + icount) as TextBox;
            TextBox txtProfitAgreed = Page.FindControl("txtProfitAgreed_" + icount) as TextBox;
            TextBox txtValidUpto = Page.FindControl("txtValidUpto_" + icount) as TextBox;
            Label lblStatus = Page.FindControl("lblStatus_" + icount) as Label;
            TextBox txtRemarks = Page.FindControl("txtRemarks_" + icount) as TextBox;
           

            hdnItemId.Value = dsItem.Tables[0].Rows[bindparse]["ItemId"].ToString();
            hdnDIMId.Value = dsItem.Tables[0].Rows[bindparse]["DIMId"].ToString();
            hdnCPMId.Value = dsItem.Tables[0].Rows[bindparse]["CPMId"].ToString();
            lblItemCode.Text = dsItem.Tables[0].Rows[bindparse]["ItemCode"].ToString();
            lblDesc1.Text = dsItem.Tables[0].Rows[bindparse]["Description1"].ToString();
            lblDesc2.Text = dsItem.Tables[0].Rows[bindparse]["Description2"].ToString();
            lblConvFactor.Text = dsItem.Tables[0].Rows[bindparse]["ConvFactor"].ToString();
            lblDlrPrice.Text = dsItem.Tables[0].Rows[bindparse]["DlrPrice"].ToString();
            lblDlrPerSqrMt.Text = dsItem.Tables[0].Rows[bindparse]["PerSqrMt"].ToString();
            txtEUPrice.Text = dsItem.Tables[0].Rows[bindparse]["EUPrice"].ToString();
            txtEUPerSqMt.Text = dsItem.Tables[0].Rows[bindparse]["EUPerSqrMt"].ToString();
            txtProfitActual.Text = dsItem.Tables[0].Rows[bindparse]["ProfitActual"].ToString();
            txtProfitAgreed.Text = dsItem.Tables[0].Rows[bindparse]["ProfitAgreed"].ToString();
            if (dsItem.Tables[0].Rows[bindparse]["ValidUpto"] != null && dsItem.Tables[0].Rows[bindparse]["ValidUpto"].ToString() != "")
                txtValidUpto.Text = Convert.ToDateTime(dsItem.Tables[0].Rows[bindparse]["ValidUpto"].ToString()).ToString(Session["DateFormat"].ToString());
            lblStatus.Text = dsItem.Tables[0].Rows[bindparse]["Status"].ToString();
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

        tc = new TableCell();
        tc.Text = "Delete";
        //Hidding the Cell
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

        tc = new TableCell();
        tc.Text = "Valid Upto";
        tc.HorizontalAlign = HorizontalAlign.Left;
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Text = "Status";
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
        CheckBox Chkdelete = new CheckBox();
        Chkdelete.ID = "Chkdelete_" + SearchCount.ToString();
        tc.Controls.Add(Chkdelete);

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
        btnDelete.Click += new EventHandler(btnDelete_Click);
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
        TextBox txtEUPrice = new TextBox();
        txtEUPrice.MaxLength = 12;
        txtEUPrice.CssClass = "commonfont textdropwidthUOM right";
        txtEUPrice.ID = "txtEUPrice_" + SearchCount.ToString();
        txtEUPrice.Attributes.Add("onBlur", "CalcPerSqrMt('" + SearchCount + "')");
        tc.Controls.Add(txtEUPrice);

        tc = new TableCell();
        tr.Cells.Add(tc);
        tc.HorizontalAlign = HorizontalAlign.Left;
        TextBox txtEUPerSqMt = new TextBox();
        txtEUPerSqMt.MaxLength = 12;
        txtEUPerSqMt.CssClass = "commonfont textdropwidthUOM right";
        txtEUPerSqMt.ID = "txtEUPerSqMt_" + SearchCount.ToString();
        txtEUPerSqMt.Attributes.Add("onBlur", "CalcDlrPrice('" + SearchCount + "')");
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
        imgValidUpto.Style.Add("vertical-align","bottom");
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
        Label lblStatus = new Label();
        lblStatus.CssClass = "commonfont textdropwidth";
        lblStatus.ID = "lblStatus_" + SearchCount.ToString();
        tc.Controls.Add(lblStatus);

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

    protected void btnDelete_Click(object sender, EventArgs e)
    {
       

        Table tbl = Page.FindControl("SearchTable") as Table;
        int index = 0;
        Button btn = sender as Button;

        string strID = btn.ID.Replace("btnDelete_", "");
        index = int.Parse(strID);
        int SearchCount = int.Parse(hidSearchCount.Value);

        HiddenField hdnCPMId = Page.FindControl("hdnCPMId_" + strID) as HiddenField;
        string strCPMId = hdnCPMId.Value;
        HiddenField hdnRow = Page.FindControl("hdnRow_" + strID) as HiddenField;
        hdnRow.Value = "S";

        
        TableRow tr = Page.FindControl("tr_" + strID) as TableRow;
        tr.Style.Add("display", "none");

        if (strCPMId != "" && strCPMId != "0")
        {
            objCust.prpCPMId = Convert.ToInt32(strCPMId);
            string strResult = objCust.DeleteItemPrice();

            if (strResult == "")
            {
                lblMessage.Text = "Deleted Successfully.";
                lblMessage.CssClass = "SuccessMessageBold";
            }
            else
            {
                lblMessage.Text = strResult;
                lblMessage.CssClass = "ErrorMessage";
            }

        }
        else 
        {
            lblMessage.Text = "Deleted Successfully.";
            lblMessage.CssClass = "SuccessMessageBold";
        }

        //GenerateSearchModel(true);
        //BindData();

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {        
        string strMsg = "";
        Table tbl = Page.FindControl("SearchTable") as Table;

        try
        {
            objCust.prpUserId = Session["UserID"].ToString();
            objCust.prpCustCode = Convert.ToInt32(lblCustCode.Text.Trim());

            for (int icount = 1; icount < tbl.Rows.Count; icount++)
            {
                HiddenField hdnRow = Page.FindControl("hdnRow_" + icount) as HiddenField;
                HiddenField hdnItemId = Page.FindControl("hdnItemId_" + icount) as HiddenField;
                HiddenField hdnDIMId = Page.FindControl("hdnDIMId_" + icount) as HiddenField;
                HiddenField hdnCPMId = Page.FindControl("hdnCPMId_" + icount) as HiddenField;
                Label lblItemCode = Page.FindControl("lblItemCode_" + icount) as Label;
                Label lblDesc1 = Page.FindControl("lblDesc1_" + icount) as Label;
                Label lblDesc2 = Page.FindControl("lblDesc2_" + icount) as Label;
                Label lblConvFactor = Page.FindControl("lblConvFactor_" + icount) as Label;
                Label lblDlrPrice = Page.FindControl("lblDlrPrice_" + icount) as Label;
                Label lblDlrPerSqrMt = Page.FindControl("lblDlrPerSqrMt_" + icount) as Label;
                TextBox txtEUPrice = Page.FindControl("txtEUPrice_" + icount) as TextBox;
                TextBox txtEUPerSqMt = Page.FindControl("txtEUPerSqMt_" + icount) as TextBox;
                TextBox txtProfitActual = Page.FindControl("txtProfitActual_" + icount) as TextBox;
                TextBox txtProfitAgreed = Page.FindControl("txtProfitAgreed_" + icount) as TextBox;
                TextBox txtValidUpto = Page.FindControl("txtValidUpto_" + icount) as TextBox;
                Label lblStatus = Page.FindControl("lblStatus_" + icount) as Label;
                TextBox txtRemarks = Page.FindControl("txtRemarks_" + icount) as TextBox;

                if (hdnRow.Value == "S")
                    continue;

                objCust.prpItemId = Convert.ToInt32(hdnItemId.Value.Trim());                
                if (hdnCPMId.Value != "")
                    objCust.prpCPMId = Convert.ToInt32(hdnCPMId.Value.Trim());
                else
                    objCust.prpCPMId = 0;
                if (txtEUPrice.Text != "" && txtEUPerSqMt.Text.Trim() != "" && txtProfitActual.Text.Trim() != "" && txtProfitAgreed.Text.Trim() != "" && txtValidUpto.Text.Trim() != "")
                {
                    objCust.prpEuPrice = Convert.ToDouble(txtEUPrice.Text.Trim());
                    objCust.prpEUPerSqrmt = Convert.ToDouble(txtEUPerSqMt.Text.Trim());
                    objCust.prpProfitActual = Convert.ToDouble(txtProfitActual.Text.Trim());
                    objCust.prpProfitAgreed = Convert.ToDouble(txtProfitAgreed.Text.Trim());
                    objCust.prpValidUpto = objComm.DateFormateConversion(txtValidUpto.Text.Trim(), Session["DateFormat"].ToString());
                   
                    objCust.prpRemarks = txtRemarks.Text.Trim();

                    strMsg = objCust.SaveCustItemList();
                    if (strMsg != "")
                    {
                        lblMessage.Text = strMsg;
                        lblMessage.CssClass = "ErrorMessage";
                        return;
                    }
                }


            }
            if (strMsg == "")
                Response.Redirect("CustomerItemPrice.aspx?Type=s&DlrCode=" + lblDealerCode.Text + "&CustCode=" + lblCustCode.Text.Trim() + "&CustName=" + lblCustName.Text.Trim());
            else
            {
                lblMessage.Text = strMsg;
                lblMessage.CssClass = "ErrorMessage";
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
        Response.Redirect("CustomerItemPrice.aspx?DlrCode=" + lblDealerCode.Text + "&CustCode=" + lblCustCode.Text.Trim() + "&CustName=" + lblCustName.Text.Trim());
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("CustomerMasterAddEdit.aspx?New=N&strCode=" + lblCustCode.Text);
    }

    protected void btnDelete_Click1(object sender, EventArgs e)
    {
        int Index = 0, RefIndex = 0, UnChecked = 0;
        foreach (TableRow TR in SearchTable.Rows)
        {
            CheckBox Chkdelete = TR.FindControl("Chkdelete_" + Index.ToString()) as CheckBox;
            if (Chkdelete != null)
            {
                if (Chkdelete.Checked == true)
                {
                    HiddenField hdnCPMId = Page.FindControl("hdnCPMId_" + Index.ToString()) as HiddenField;
                    string strCPMId = hdnCPMId.Value;

                    TableRow tr = Page.FindControl("tr_" + Index.ToString()) as TableRow;
                    HiddenField hdnRow = Page.FindControl("hdnRow_" + Index.ToString()) as HiddenField;
                    hdnRow.Value = "S";
                    tr.Style.Add("display", "none");
                    lblMessage.Text = "Deleted Successfully.";
                    lblMessage.CssClass = "SuccessMessageBold";
                    if (strCPMId != "" && strCPMId != "0")
                    {
                        objCust.prpCPMId = Convert.ToInt32(strCPMId);
                        string strResult = objCust.DeleteItemPrice();

                        if (strResult == "")
                        {
                            lblMessage.Text = "Deleted Successfully.";
                            lblMessage.CssClass = "SuccessMessageBold";                            
                        }
                        else
                        {
                            lblMessage.Text = strResult;
                            lblMessage.CssClass = "ErrorMessage";
                        }
                    }

                }
                else if (Chkdelete.Checked == false)
                {
                    UnChecked++;
                }
                RefIndex++;
            }
            Index++;
        }

        if (RefIndex == UnChecked)
        {
            lblMessage.Text = "Please Select Atleast one Item to Delete";
            lblMessage.CssClass = "ErrorMessage";
        }
    }
    

}
