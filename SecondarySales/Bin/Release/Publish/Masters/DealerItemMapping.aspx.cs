/* ==============================================================================================
    Created By      : Subahani SM
    Created Date    : 29 July 2010
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

public partial class Masters_DealerItemMapping : System.Web.UI.Page
{
    DealerMasterClass objdlr = new DealerMasterClass();
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
                lblDealerCode.Text = Request.QueryString["DlrCode"].ToString();
                lblDealerName.Text = Request.QueryString["DlrName"].ToString();

                if (Request.QueryString["Type"] != null && Request.QueryString["Type"].ToString() != "")
                {
                    lblMessage.Text = "Saved/Updated Successfully.";
                    lblMessage.CssClass = "SuccessMessageBold";
                }
            }

            GenerateSearchModel(false);
            if (!IsPostBack && Request.QueryString["DlrCode"] != null)
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
        objdlr.prpDealerCode = Convert.ToInt32(lblDealerCode.Text.Trim());
        dsItem = objdlr.FetchDealerItemList();
        for (int bindparse = 0; bindparse < dsItem.Tables[0].Rows.Count; bindparse++)
            CreateRow(bindparse + 1);

        hidSearchCount.Value = Convert.ToString(dsItem.Tables[0].Rows.Count);

        for (int bindparse = 0; bindparse < dsItem.Tables[0].Rows.Count; bindparse++)
        {
            int icount = bindparse + 1;
            HiddenField hdnItemId = Page.FindControl("hdnItemId_" + icount) as HiddenField;
            HiddenField hdnDIMId = Page.FindControl("hdnDIMId_" + icount) as HiddenField;
            Label lblBudgetClass = Page.FindControl("lblBudgetClass_" + icount) as Label;
            Label lblProdFamily = Page.FindControl("lblProdFamily_" + icount) as Label;
            Label lblProdGroup = Page.FindControl("lblProdGroup_" + icount) as Label;
            Label lblItemCode = Page.FindControl("lblItemCode_" + icount) as Label;
            Label lblDesc1 = Page.FindControl("lblDesc1_" + icount) as Label;
            Label lblDesc2 = Page.FindControl("lblDesc2_" + icount) as Label;
            Label lblConvFactor = Page.FindControl("lblConvFactor_" + icount) as Label;
            TextBox txtDLRPrice = Page.FindControl("txtDLRPrice_" + icount) as TextBox;
            TextBox txtPerSqMt = Page.FindControl("txtPerSqMt_" + icount) as TextBox;
            TextBox txtMaxQty = Page.FindControl("txtMaxQty_" + icount) as TextBox;
            TextBox txtRemarks = Page.FindControl("txtRemarks_" + icount) as TextBox;

            hdnItemId.Value = dsItem.Tables[0].Rows[bindparse]["ItemId"].ToString();
            hdnDIMId.Value = dsItem.Tables[0].Rows[bindparse]["DIMId"].ToString();
            lblBudgetClass.Text = dsItem.Tables[0].Rows[bindparse]["BCCode"].ToString();
            lblProdFamily.Text = dsItem.Tables[0].Rows[bindparse]["PFCode"].ToString();
            lblProdGroup.Text = dsItem.Tables[0].Rows[bindparse]["GroupCode"].ToString();
            lblItemCode.Text = dsItem.Tables[0].Rows[bindparse]["ItemCode"].ToString();
            lblDesc1.Text = dsItem.Tables[0].Rows[bindparse]["Description1"].ToString();
            lblDesc2.Text = dsItem.Tables[0].Rows[bindparse]["Description2"].ToString();
            lblConvFactor.Text = dsItem.Tables[0].Rows[bindparse]["ConvFactor"].ToString();
            txtDLRPrice.Text = dsItem.Tables[0].Rows[bindparse]["DlrPrice"].ToString();
            txtPerSqMt.Text = dsItem.Tables[0].Rows[bindparse]["PerSqrMt"].ToString();
            txtMaxQty.Text = dsItem.Tables[0].Rows[bindparse]["MaxQty"].ToString();
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
        //hidding the Cell
        tc.Style.Add("display", "none");
        tc.HorizontalAlign = HorizontalAlign.Left;
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Text = "Budget Class";
        tc.HorizontalAlign = HorizontalAlign.Left;
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Text = "Product Family";
        tc.HorizontalAlign = HorizontalAlign.Left;
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Text = "Product Group";
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
        tc.Text = "PerSqrMt";
        tc.HorizontalAlign = HorizontalAlign.Left;
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Text = "Max. Qty";
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
        //hidding the Cell 
        tc.Style.Add("Display", "none");
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
        Label lblBudgetClass = new Label();
        lblBudgetClass.CssClass = "commonfont textdropwidth";
        lblBudgetClass.ID = "lblBudgetClass_" + SearchCount.ToString();
        tc.Controls.Add(lblBudgetClass);

        tc = new TableCell();
        tr.Cells.Add(tc);
        tc.HorizontalAlign = HorizontalAlign.Left;
        Label lblProdFamily = new Label();
        lblProdFamily.CssClass = "commonfont textdropwidth";
        lblProdFamily.ID = "lblProdFamily_" + SearchCount.ToString();
        tc.Controls.Add(lblProdFamily);

        tc = new TableCell();
        tr.Cells.Add(tc);
        tc.HorizontalAlign = HorizontalAlign.Left;
        Label lblProdGroup = new Label();
        lblProdGroup.CssClass = "commonfont textdropwidth";
        lblProdGroup.ID = "lblProdGroup_" + SearchCount.ToString();
        tc.Controls.Add(lblProdGroup);

        tc = new TableCell();
        tr.Cells.Add(tc);
        tc.HorizontalAlign = HorizontalAlign.Left;
        Label lblItemCode = new Label();
        lblItemCode.CssClass = "commonfont textdropwidth";
        lblItemCode.ID = "lblItemCode_" + SearchCount.ToString();
        tc.Controls.Add(lblItemCode);

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
        TextBox txtDLRPrice = new TextBox();
        txtDLRPrice.CssClass = "commonfont textdropwidthUOM right";
        txtDLRPrice.ID = "txtDLRPrice_" + SearchCount.ToString();
        txtDLRPrice.Attributes.Add("onBlur", "CalcPerSqrMt('" + SearchCount + "')");
        txtDLRPrice.MaxLength = 12;
        tc.Controls.Add(txtDLRPrice);

        tc = new TableCell();
        tr.Cells.Add(tc);
        tc.HorizontalAlign = HorizontalAlign.Left;
        TextBox txtPerSqMt = new TextBox();
        txtPerSqMt.CssClass = "commonfont textdropwidthUOM right";
        txtPerSqMt.ID = "txtPerSqMt_" + SearchCount.ToString();
        txtPerSqMt.Attributes.Add("onBlur", "CalcDlrPrice('" + SearchCount + "')");
        txtPerSqMt.MaxLength = 12;
        tc.Controls.Add(txtPerSqMt);

        tc = new TableCell();
        tr.Cells.Add(tc);
        tc.HorizontalAlign = HorizontalAlign.Left;
        TextBox txtMaxQty = new TextBox();
        txtMaxQty.CssClass = "commonfont textdropwidthUOM right";
        txtMaxQty.ID = "txtMaxQty_" + SearchCount.ToString();
        txtMaxQty.MaxLength = 6;
        tc.Controls.Add(txtMaxQty);

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

        //HiddenField hdnDIMId, hdnDIMIdNext;
        //HiddenField hdnItemId, hdnItemIdNext;
        //Label lblBudgetClass, lblBudgetClassNext;
        //Label lblProdFamily, lblProdFamilyNext;
        //Label lblProdGroup, lblProdGroupNext;
        //Label lblItemCode, lblItemCodeNext;
        //Label lblDesc1, lblDesc1Next;
        //Label lblDesc2, lblDesc2Next;
        //Label lblConvFactor, lblConvFactorNext;
        //TextBox txtDLRPrice, txtDLRPriceNext;
        //TextBox txtPerSqMt, txtPerSqMtNext;
        //TextBox txtMaxQty, txtMaxQtyNext; 
        //TextBox txtRemarks, txtRemarksNext;

        HiddenField hdnDIMId = Page.FindControl("hdnDIMId_" + strID) as HiddenField;
        string strDIMId = hdnDIMId.Value;

        //for (int icount = index; icount < SearchCount - 1; icount++)
        //{
        //    hdnDIMId = Page.FindControl("hdnDIMId_" + icount) as HiddenField;
        //    hdnItemId = Page.FindControl("hdnItemId_" + icount) as HiddenField;
        //    lblBudgetClass = Page.FindControl("lblBudgetClass_" + icount) as Label;
        //    lblProdFamily = Page.FindControl("lblProdFamily_" + icount) as Label;
        //    lblProdGroup = Page.FindControl("lblProdGroup_" + icount) as Label;
        //    lblItemCode = Page.FindControl("lblItemCode_" + icount) as Label;
        //    lblDesc1 = Page.FindControl("lblDesc1_" + icount) as Label;
        //    lblDesc2 = Page.FindControl("lblDesc2_" + icount) as Label;
        //    lblConvFactor = Page.FindControl("lblConvFactor_" + icount) as Label;
        //    txtDLRPrice = Page.FindControl("txtDLRPrice_" + icount) as TextBox;
        //    txtPerSqMt = Page.FindControl("txtPerSqMt_" + icount) as TextBox;
        //    txtMaxQty = Page.FindControl("txtMaxQty_" + icount) as TextBox;
        //    txtRemarks = Page.FindControl("txtRemarks_" + icount) as TextBox;

        //    hdnDIMIdNext = Page.FindControl("hdnDIMId_" + Convert.ToString(icount + 1)) as HiddenField;
        //    hdnItemIdNext = Page.FindControl("hdnItemId_" + Convert.ToString(icount + 1)) as HiddenField;
        //    lblBudgetClassNext = Page.FindControl("lblBudgetClass_" + Convert.ToString(icount + 1)) as Label;
        //    lblProdFamilyNext = Page.FindControl("lblProdFamily_" + Convert.ToString(icount + 1)) as Label;
        //    lblProdGroupNext = Page.FindControl("lblProdGroup_" + Convert.ToString(icount + 1)) as Label;
        //    lblItemCodeNext = Page.FindControl("lblItemCode_" + Convert.ToString(icount + 1)) as Label;
        //    lblDesc1Next = Page.FindControl("lblDesc1_" + Convert.ToString(icount + 1)) as Label;
        //    lblDesc2Next = Page.FindControl("lblDesc2_" + Convert.ToString(icount + 1)) as Label;
        //    lblConvFactorNext = Page.FindControl("lblConvFactor_" + Convert.ToString(icount + 1)) as Label;
        //    txtDLRPriceNext = Page.FindControl("txtDLRPrice_" + Convert.ToString(icount + 1)) as TextBox;
        //    txtPerSqMtNext = Page.FindControl("txtPerSqMt_" + Convert.ToString(icount + 1)) as TextBox;
        //    txtMaxQtyNext = Page.FindControl("txtMaxQty_" + Convert.ToString(icount + 1)) as TextBox;
        //    txtRemarksNext = Page.FindControl("txtRemarks_" + Convert.ToString(icount + 1)) as TextBox;

        //    hdnDIMId.Value = hdnDIMIdNext.Value;
        //    hdnItemId.Value = hdnItemIdNext.Value;
        //    lblBudgetClass.Text = lblBudgetClassNext.Text;
        //    lblProdFamily.Text = lblProdFamilyNext.Text;
        //    lblProdGroup.Text = lblProdGroupNext.Text;
        //    lblItemCode.Text = lblItemCodeNext.Text;
        //    lblDesc1.Text = lblDesc1Next.Text;
        //    lblDesc2.Text = lblDesc2Next.Text;
        //    lblConvFactor.Text = lblConvFactorNext.Text;
        //    txtDLRPrice.Text = txtDLRPriceNext.Text;
        //    txtPerSqMt.Text = txtPerSqMtNext.Text;
        //    txtMaxQty.Text = txtMaxQtyNext.Text;
        //    txtRemarks.Text = txtRemarksNext.Text;
        //}

        TableRow tr = Page.FindControl("tr_" + strID) as TableRow;
        HiddenField hdnRow = Page.FindControl("hdnRow_" + strID) as HiddenField;
        hdnRow.Value = "S";
        //tbl.Rows.Remove(tr);
        //hidSearchCount.Value = Convert.ToString(SearchCount - 1);
        // tr.Visible = false;
        tr.Style.Add("display", "none");

        if (strDIMId != "" && strDIMId != "0")
        {
            objdlr.prpDMIId = Convert.ToInt32(strDIMId);
            string strResult = objdlr.DeleteItemPrice();

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

        //GenerateSearchModel(true);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string strMsg = "";
        Table tbl = Page.FindControl("SearchTable") as Table;

        try
        {
            objdlr.prpUserId = Session["UserID"].ToString();
            objdlr.prpDealerCode = Convert.ToInt32(lblDealerCode.Text.Trim());

            for (int icount = 1; icount < tbl.Rows.Count; icount++)
            {
                HiddenField hdnRow = Page.FindControl("hdnRow_" + icount) as HiddenField;
                HiddenField hdnItemId = Page.FindControl("hdnItemId_" + icount) as HiddenField;
                HiddenField hdnDIMId = Page.FindControl("hdnDIMId_" + icount) as HiddenField;
                Label lblBudgetClass = Page.FindControl("lblBudgetClass_" + icount) as Label;
                Label lblProdFamily = Page.FindControl("lblProdFamily_" + icount) as Label;
                Label lblProdGroup = Page.FindControl("lblProdGroup_" + icount) as Label;
                Label lblItemCode = Page.FindControl("lblItemCode_" + icount) as Label;
                Label lblDesc1 = Page.FindControl("lblDesc1_" + icount) as Label;
                Label lblDesc2 = Page.FindControl("lblDesc2_" + icount) as Label;
                Label lblConvFactor = Page.FindControl("lblConvFactor_" + icount) as Label;
                TextBox txtDLRPrice = Page.FindControl("txtDLRPrice_" + icount) as TextBox;
                TextBox txtPerSqMt = Page.FindControl("txtPerSqMt_" + icount) as TextBox;
                TextBox txtMaxQty = Page.FindControl("txtMaxQty_" + icount) as TextBox;
                TextBox txtRemarks = Page.FindControl("txtRemarks_" + icount) as TextBox;

                if (hdnRow.Value == "S")
                    continue;

                objdlr.prpItemId = Convert.ToInt32(hdnItemId.Value.Trim());
                if (hdnDIMId.Value != "")
                    objdlr.prpDMIId = Convert.ToInt32(hdnDIMId.Value.Trim());
                else
                    objdlr.prpDMIId = 0;
                if (txtDLRPrice.Text != "" && txtPerSqMt.Text != "" && txtMaxQty.Text != "")
                {
                    objdlr.prpDlrPrice = Convert.ToDouble(txtDLRPrice.Text.Trim());
                    objdlr.prpPerSqrmt = Convert.ToDouble(txtPerSqMt.Text.Trim());
                    objdlr.prpQty = Convert.ToInt32(txtMaxQty.Text.Trim());
                    objdlr.prpRemarks = txtRemarks.Text.Trim();

                    strMsg = objdlr.SaveDealerItemList();


                    if (strMsg != "")
                    {
                        lblMessage.Text = strMsg;
                        lblMessage.CssClass = "ErrorMessage";
                        return;
                    }
                }
            }
            if (strMsg == "")
                Response.Redirect("DealerItemMapping.aspx?Type=s&DlrCode=" + lblDealerCode.Text + "&DlrName=" + lblDealerName.Text.Trim());
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
        Response.Redirect("DealerItemMapping.aspx?DlrCode=" + lblDealerCode.Text + "&DlrName=" + lblDealerName.Text.Trim());
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("DealerMasterAddEdit.aspx?New=N&strCode=" + lblDealerCode.Text);
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
                    HiddenField hdnDIMId = Page.FindControl("hdnDIMId_" + Index.ToString()) as HiddenField;
                    string strDIMId = hdnDIMId.Value;

                    TableRow tr = Page.FindControl("tr_" + Index.ToString()) as TableRow;
                    HiddenField hdnRow = Page.FindControl("hdnRow_" + Index.ToString()) as HiddenField;
                    hdnRow.Value = "S";
                    tr.Style.Add("display", "none");
                    lblMessage.Text = "Deleted Successfully.";
                    lblMessage.CssClass = "SuccessMessageBold";

                    if (strDIMId != "" && strDIMId != "0")
                    {
                        objdlr.prpDMIId = Convert.ToInt32(strDIMId);
                        string strResult = objdlr.DeleteItemPrice();

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
