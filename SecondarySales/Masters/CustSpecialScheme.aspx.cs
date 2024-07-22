    /* ==============================================================================================
    Created By      : Subahani SM
    Created Date    : 01 Aug 2010
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

public partial class Masters_CustSpecialScheme : System.Web.UI.Page
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
        objCust.prpCustCode = Convert.ToInt32(lblCustCode.Text.Trim());
        dsItem = objCust.FetchCustSpecialScheme();
        for (int bindparse = 0; bindparse < dsItem.Tables[0].Rows.Count; bindparse++)
            CreateRow(bindparse + 1);

        hidSearchCount.Value = Convert.ToString(dsItem.Tables[0].Rows.Count);

        for (int bindparse = 0; bindparse < dsItem.Tables[0].Rows.Count; bindparse++)
        {
            int icount = bindparse + 1;
            HiddenField hdnItemId = Page.FindControl("hdnItemId_" + icount) as HiddenField;
            Label lblItemCode = Page.FindControl("lblItemCode_" + icount) as Label;
            TextBox txtQtyConsum = Page.FindControl("txtQtyConsum_" + icount) as TextBox;
            TextBox txtQtyEligible= Page.FindControl("txtQtyEligible_" + icount) as TextBox;            
            TextBox txtSqmtConsum = Page.FindControl("txtSqmtConsum_" + icount) as TextBox;
            TextBox txtSqmtEligible = Page.FindControl("txtSqmtEligible_" + icount) as TextBox;
            TextBox txtValue = Page.FindControl("txtValue_" + icount) as TextBox;
            TextBox txtValueEligible = Page.FindControl("txtValueEligible_" + icount) as TextBox;

            hdnItemId.Value = dsItem.Tables[0].Rows[bindparse]["Id"].ToString();
            lblItemCode.Text = dsItem.Tables[0].Rows[bindparse]["ItemCode"].ToString();
            txtQtyConsum.Text = dsItem.Tables[0].Rows[bindparse]["QtyConsumption"].ToString();
            txtQtyEligible.Text = dsItem.Tables[0].Rows[bindparse]["QtyEligible"].ToString();
            txtSqmtConsum.Text = dsItem.Tables[0].Rows[bindparse]["SqmtConsumption"].ToString();
            txtSqmtEligible.Text = dsItem.Tables[0].Rows[bindparse]["SqmtEligible"].ToString();
            txtValue.Text = dsItem.Tables[0].Rows[bindparse]["Value"].ToString();
            txtValueEligible.Text = dsItem.Tables[0].Rows[bindparse]["ValueEligible"].ToString();
         
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
        tc.Text = "Qty Consumption";
        tc.HorizontalAlign = HorizontalAlign.Left;
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Text = "Free Qty Eligible";
        tc.HorizontalAlign = HorizontalAlign.Left;
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Text = "SqMt Consumption";
        tc.HorizontalAlign = HorizontalAlign.Left;
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Text = "SqMt Eligible";
        tc.HorizontalAlign = HorizontalAlign.Left;
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Text = "Value";
        tc.HorizontalAlign = HorizontalAlign.Left;
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Text = "Value Eligible";
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
        HiddenField hdnRow = new HiddenField();
        hdnRow.ID = "hdnRow_" + SearchCount.ToString();
        tc.Controls.Add(btnDelete);
        tc.Controls.Add(hdnItemId);
        tc.Controls.Add(hdnRow);

        tc = new TableCell();
        tr.Cells.Add(tc);
        tc.HorizontalAlign = HorizontalAlign.Left;
        Label lblItemCode = new Label();
        lblItemCode.CssClass = "commonfont textdropwidthUOM right";
        lblItemCode.ID = "lblItemCode_" + SearchCount.ToString();
        tc.Controls.Add(lblItemCode);

        tc = new TableCell();
        tr.Cells.Add(tc);
        tc.HorizontalAlign = HorizontalAlign.Left;
        TextBox txtQtyConsum = new TextBox();
        txtQtyConsum.MaxLength = 6;
        txtQtyConsum.CssClass = "commonfont textdropwidthUOM right";
        txtQtyConsum.ID = "txtQtyConsum_" + SearchCount.ToString();
        tc.Controls.Add(txtQtyConsum);

        tc = new TableCell();
        tr.Cells.Add(tc);
        tc.HorizontalAlign = HorizontalAlign.Left;
        TextBox txtQtyEligible = new TextBox();
        txtQtyEligible.MaxLength = 6;
        txtQtyEligible.CssClass = "commonfont textdropwidthUOM right";
        txtQtyEligible.ID = "txtQtyEligible_" + SearchCount.ToString();
        tc.Controls.Add(txtQtyEligible);

        tc = new TableCell();
        tr.Cells.Add(tc);
        tc.HorizontalAlign = HorizontalAlign.Left;
        TextBox txtSqmtConsum = new TextBox();
        txtSqmtConsum.MaxLength = 12;        
        txtSqmtConsum.CssClass = "commonfont textdropwidthUOM right";
        txtSqmtConsum.ID = "txtSqmtConsum_" + SearchCount.ToString();
        tc.Controls.Add(txtSqmtConsum);

        tc = new TableCell();
        tr.Cells.Add(tc);
        tc.HorizontalAlign = HorizontalAlign.Left;
        TextBox txtSqmtEligible = new TextBox();
        txtSqmtEligible.MaxLength = 12;        
        txtSqmtEligible.CssClass = "commonfont textdropwidthUOM right";
        txtSqmtEligible.ID = "txtSqmtEligible_" + SearchCount.ToString();
        tc.Controls.Add(txtSqmtEligible);

        tc = new TableCell();
        tr.Cells.Add(tc);
        tc.HorizontalAlign = HorizontalAlign.Left;
        TextBox txtValue = new TextBox();
        txtValue.MaxLength = 12;        
        txtValue.CssClass = "commonfont textdropwidthUOM right";
        txtValue.ID = "txtValue_" + SearchCount.ToString();
        tc.Controls.Add(txtValue);

        tc = new TableCell();
        tr.Cells.Add(tc);
        tc.HorizontalAlign = HorizontalAlign.Left;
        TextBox txtValueEligible = new TextBox();
        txtValueEligible.MaxLength = 12;        
        txtValueEligible.CssClass = "commonfont textdropwidthUOM right";
        txtValueEligible.ID = "txtValueEligible_" + SearchCount.ToString();
        tc.Controls.Add(txtValueEligible);
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
        string strItemId = hdnItemId.Value;
       
        TableRow tr = Page.FindControl("tr_" + strID) as TableRow;
        tr.Style.Add("display", "none");

        if (strItemId != "" && strItemId != "0")
        {
            objCust.prpItemId = Convert.ToInt32(strItemId);
            objCust.prpCustCode = Convert.ToInt32(lblCustCode.Text.Trim());
            string strResult = objCust.DeleteSpecialScheme();

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
                Label lblItemCode = Page.FindControl("lblItemCode_" + icount) as Label;
                TextBox txtQtyConsum = Page.FindControl("txtQtyConsum_" + icount) as TextBox;
                TextBox txtQtyEligible = Page.FindControl("txtQtyEligible_" + icount) as TextBox;
                TextBox txtSqmtConsum = Page.FindControl("txtSqmtConsum_" + icount) as TextBox;
                TextBox txtSqmtEligible = Page.FindControl("txtSqmtEligible_" + icount) as TextBox;
                TextBox txtValue = Page.FindControl("txtValue_" + icount) as TextBox;
                TextBox txtValueEligible = Page.FindControl("txtValueEligible_" + icount) as TextBox;

                if (hdnRow.Value == "S")
                    continue;

                objCust.prpItemId = Convert.ToInt32(hdnItemId.Value.Trim());
                if (txtQtyConsum.Text.Trim() != "")
                {
                    objCust.prpQtyConsum = Convert.ToInt32(txtQtyConsum.Text.Trim());
                    objCust.prpQtyEligible = Convert.ToInt32(txtQtyEligible.Text.Trim());
                }
                else
                {
                    objCust.prpQtyConsum = 0;
                    objCust.prpQtyEligible = 0;
                }

                if (txtSqmtConsum.Text.Trim() != "")
                {
                    objCust.prpSqrmtConsum = Convert.ToDouble(txtSqmtConsum.Text.Trim());
                    objCust.prpSrmtEligible = Convert.ToDouble(txtSqmtEligible.Text.Trim());
                }
                else
                {
                    objCust.prpSqrmtConsum = 0;
                    objCust.prpSrmtEligible = 0;
                }

                if (txtValue.Text.Trim() != "")
                {
                    objCust.prpValue = Convert.ToDouble(txtValue.Text.Trim());
                    objCust.prpValueEligible = Convert.ToDouble(txtValueEligible.Text.Trim());
                }
                else
                {
                    objCust.prpValue = 0;
                    objCust.prpValueEligible = 0;
                }

                if ((txtQtyConsum.Text.Trim() != "") && (txtSqmtConsum.Text.Trim() != "") && (txtValue.Text.Trim() != ""))
                {
                    strMsg = objCust.SaveCustSpecialScheme();
                }

                if (strMsg != "")
                {
                    lblMessage.Text = strMsg;
                    lblMessage.CssClass = "ErrorMessage";
                    return;
                }
            }
            if (strMsg == "")
                Response.Redirect("CustSpecialScheme.aspx?Type=s&DlrCode=" + lblDealerCode.Text + "&CustCode=" + lblCustCode.Text.Trim() + "&CustName=" + lblCustName.Text.Trim());
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
        Response.Redirect("CustSpecialScheme.aspx?DlrCode=" + lblDealerCode.Text + "&CustCode=" + lblCustCode.Text.Trim() + "&CustName=" + lblCustName.Text.Trim());
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("CustomerMaster.aspx");
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
                    HiddenField hdnItemId = Page.FindControl("hdnItemId_" + Index.ToString()) as HiddenField;
                    string strItemId = hdnItemId.Value;

                    TableRow tr = Page.FindControl("tr_" + Index.ToString()) as TableRow;
                    HiddenField hdnRow = Page.FindControl("hdnRow_" + Index.ToString()) as HiddenField;
                    hdnRow.Value = "S";
                    tr.Style.Add("display", "none");
                    lblMessage.Text = "Deleted Successfully.";
                    lblMessage.CssClass = "SuccessMessageBold";
                    if (strItemId != "" && strItemId != "0")
                    {
                        objCust.prpItemId = Convert.ToInt32(strItemId);
                        objCust.prpCustCode = Convert.ToInt32(lblCustCode.Text.Trim());
                        string strResult = objCust.DeleteSpecialScheme();

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
            lblMessage.Text = "Please Select Atleast One Item To Delete.";
            lblMessage.CssClass = "ErrorMessage";
        }
    }

}
