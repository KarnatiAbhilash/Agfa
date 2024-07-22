/* ==============================================================================================
    Created By      : Subahani SM
    Created Date    : 02 Aug 2010
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

public partial class Masters_DealerCustLink : System.Web.UI.Page
{
    CustomerMasterClass objCust = new CustomerMasterClass();
    DealerMasterClass objDealer = new DealerMasterClass();  
    //static DataTable dtDlrCustLink;
    //static DataTable dtDlrCust;
    private DataSet dsItem;
    bool IsValidCustCode = false;
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

                if (Request.QueryString["Type"] != null && Request.QueryString["Type"].ToString() != "")
                {
                    txtDealer.Text = Request.QueryString["DlrCode"].ToString();                    
                    lblMessage.Text = "Saved/Updated Successfully.";
                    lblMessage.CssClass = "SuccessMessageBold";
                    txtDealerName.Value = Session["DlrName"].ToString(); 
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
        tc.Text = "Cust. Code";
        tc.HorizontalAlign = HorizontalAlign.Left;
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Text = "Cust. Name";
        tc.HorizontalAlign = HorizontalAlign.Left;
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Text = "City";
        tc.HorizontalAlign = HorizontalAlign.Left;
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Text = "Dealer Name";
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
        btnDelete.CssClass = "btn hide";
        btnDelete.Attributes.Add("Title", "Hide the Row");
        btnDelete.Width = Unit.Pixel(15);
        btnDelete.ID = "btnDelete_" + SearchCount.ToString();
        btnDelete.Attributes.Add("OnClick", "return confirm('Are You Sure You Want To Hide The Record?')");
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
        TextBox txtCustCode = new TextBox();
        txtCustCode.AutoPostBack = true;
        txtCustCode.TextChanged += new EventHandler(txtCustCode_TextChanged);
        txtCustCode.CssClass = "commonfont textdropwidth";
        txtCustCode.ID = "txtCustCode_" + SearchCount.ToString();
        HiddenField hdnCustCode = new HiddenField();
        hdnCustCode.ID = "hdnCustCode_" + SearchCount;
        HtmlButton btnCust = new HtmlButton();
        btnCust.Attributes.Add("runat", "server");
        btnCust.InnerText = "...";
        btnCust.ID = "btnCust_" + SearchCount.ToString();
        btnCust.Attributes.Add("onclick", "popCustomer('DlrCustLink','" + SearchCount.ToString() + "')");
        btnCust.Attributes.Add("class", "popButton");
        tc.Controls.Add(txtCustCode);
        tc.Controls.Add(hdnCustCode);
        tc.Controls.Add(btnCust);

        tc = new TableCell();
        tr.Cells.Add(tc);
        tc.HorizontalAlign = HorizontalAlign.Left;
        Label lblCustName= new Label();
        lblCustName.CssClass = "commonfont textdropwidth";
        lblCustName.ID = "lblCustName_" + SearchCount.ToString();
        tc.Controls.Add(lblCustName);

        tc = new TableCell();
        tr.Cells.Add(tc);
        tc.HorizontalAlign = HorizontalAlign.Left;
        Label lblCity = new Label();
        lblCity.CssClass = "commonfont textdropwidth";
        lblCity.ID = "lblCity_" + SearchCount.ToString();
        tc.Controls.Add(lblCity);

        tc = new TableCell();
        tr.Cells.Add(tc);
        tc.HorizontalAlign = HorizontalAlign.Left;
        DropDownList ddlDealer = new DropDownList();
        ddlDealer.ID = "ddlDealer_" + SearchCount.ToString();
        ddlDealer.CssClass = "commonfont textdropwidth";
        CommonFunction.PopulateRecordsWithOneParam("DealerMaster", "DealerName", "DealerCode", "Status", "1", "DealerName", ddlDealer);
        tc.Controls.Add(ddlDealer);
    }

    protected void BindData()
    {
        Table tbl = Page.FindControl("SearchTable") as Table;
        tbl.Controls.Clear();
        AddHeader();

        objCust.prpDealerCode = Convert.ToInt32(txtDealer.Text.Trim());
        dsItem = objCust.FetchDealerCust();

        for (int bindparse = 0; bindparse < dsItem.Tables[0].Rows.Count; bindparse++)
            CreateRow(bindparse + 1);

        hidSearchCount.Value = Convert.ToString(dsItem.Tables[0].Rows.Count);

        if (dsItem.Tables[0].Rows.Count > 0)
        {
            btnSave.Enabled = true;
            btnAdd.Enabled = true;
        }

        for (int bindparse = 0; bindparse < dsItem.Tables[0].Rows.Count; bindparse++)
        {
            int icount = bindparse + 1;
            HiddenField hdnCustCode = Page.FindControl("hdnCustCode_" + icount) as HiddenField;
            TextBox txtCustCode = Page.FindControl("txtCustCode_" + icount) as TextBox;
            Label lblCustName = Page.FindControl("lblCustName_" + icount) as Label;
            Label lblCity = Page.FindControl("lblCity_" + icount) as Label;
            DropDownList ddlDealer = Page.FindControl("ddlDealer_" + icount) as DropDownList;

            txtCustCode.Text = dsItem.Tables[0].Rows[bindparse]["CustCode"].ToString();
            hdnCustCode.Value = dsItem.Tables[0].Rows[bindparse]["CustCode"].ToString();
            lblCustName.Text = dsItem.Tables[0].Rows[bindparse]["CustName"].ToString();
            lblCity.Text = dsItem.Tables[0].Rows[bindparse]["City"].ToString();
            ddlDealer.SelectedValue = dsItem.Tables[0].Rows[bindparse]["DealerCode"].ToString();
        }
        
        //dtDlrCustLink = ds.Tables[0].Clone();
        //dtDlrCust = ds.Tables[0].Clone();
        //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //{
        //    if (ds.Tables[0].Rows[i]["DealerCode"].ToString() == txtDealer.Value.Trim())
        //        dtDlrCustLink.Rows.Add(ds.Tables[0].Rows[i].ItemArray);
        //    else
        //        dtDlrCust.Rows.Add(ds.Tables[0].Rows[i].ItemArray);
        //}

        //gvDlrCustLink.DataSource = dtDlrCustLink;
        //gvDlrCustLink.DataBind();

        //gvDlrCust.DataSource = dtDlrCust;
        //gvDlrCust.DataBind();

      
    }
    protected void btnGetdata_Click(object sender, EventArgs e)
    {
        BindData();
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
        tr.Style.Add("display", "none");
        tr.CssClass = "delete";
        lblMessage.Text = "Hidden Successfully.";
        lblMessage.CssClass = "SuccessMessageBold";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string strMsg = "";
        Table tbl = Page.FindControl("SearchTable") as Table;
        try
        {
            objCust.prpUserId = Session["UserID"].ToString();
            //objCust.prpDealerCode = Convert.ToInt32(txtDealer.Value.Trim());
            
            if (lblMessage.Text != "DealerCode Not Found.")
            {
                for (int icount = 1; icount < tbl.Rows.Count; icount++)
                {
                    HiddenField hdnRow = Page.FindControl("hdnRow_" + icount) as HiddenField;
                    HiddenField hdnCustCode = Page.FindControl("hdnCustCode_" + icount) as HiddenField;
                    DropDownList ddlDealer = Page.FindControl("ddlDealer_" + icount) as DropDownList;
                    TextBox txtCustCode = Page.FindControl("txtCustCode_" + icount) as TextBox;
                    TableRow tr = Page.FindControl("tr_" + icount) as TableRow;
                    string dlrCode = ddlDealer.SelectedValue.Trim();
                    if(tr.CssClass != "delete")
                    {
                    if (ValidateCustomerCode(txtCustCode))
                    {
                        if (hdnRow.Value == "S")
                            continue;

                        objCust.prpDealerCode = Convert.ToInt32(dlrCode);
                        objCust.prpCustCode = Convert.ToInt32(hdnCustCode.Value.Trim());
                        strMsg = objCust.SaveDlrCustlink();
                        Session["DlrName"] = txtDealerName.Value;
                    }
                    else
                    {                        
                        //TextBox txtCustCode = Page.FindControl("txtCustCode_" + icount) as TextBox;
                        txtCustCode.Focus();
                        break;
                    }
                }
                }
            }

            if (strMsg == "" && IsValidCustCode==true)
             Response.Redirect("DealerCustLink.aspx?Type=s&DlrCode=" + txtDealer.Text );
            else if (IsValidCustCode == true)
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
        Response.Redirect("DealerCustLink.aspx");
    }

    //protected void btnAdd_Click(object sender, EventArgs e)
    //{
    //    int intcnt = 0;
    //    try
    //    {
    //        for (int i = 0; i < gvDlrCust.Rows.Count; i++)
    //        {                
    //            CheckBox chkDlrCust = gvDlrCust.Rows[i].FindControl("chkDlrCust") as CheckBox;
    //            if (chkDlrCust.Checked)
    //            {
    //                intcnt = intcnt + 1;

    //                Label lblCustCode = gvDlrCust.Rows[i].FindControl("lblCustCode") as Label;
    //                DataRow[] dr = dtDlrCust.Select("CustCode=" + Convert.ToInt32(lblCustCode.Text));
    //                dtDlrCustLink.Rows.Add(dr[0].ItemArray);
    //                dtDlrCust.Rows.Remove(dr[0]);
    //            }                
    //        }
    //        if (intcnt > 0)
    //        {
    //            gvDlrCustLink.DataSource = dtDlrCustLink;
    //            gvDlrCustLink.DataBind();

    //            gvDlrCust.DataSource = dtDlrCust;
    //            gvDlrCust.DataBind();
    //        }
    //        else
    //        {
    //            lblMessage.Text = "Please Select Atleast One Record.";
    //            lblMessage.CssClass = "ErrorMessage";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMessage.Text = ex.Message;
    //        lblMessage.CssClass = "ErrorMessage";
    //    }
    //}
    //protected void btnRemove_Click(object sender, EventArgs e)
    //{
    //    int intcnt = 0;
    //    try
    //    {
    //        for (int i = 0; i < gvDlrCustLink.Rows.Count; i++)
    //        {
    //            CheckBox chkDlrCustLink = gvDlrCustLink.Rows[i].FindControl("chkDlrCustLink") as CheckBox;
    //            if (chkDlrCustLink.Checked)
    //            {
    //                intcnt = intcnt + 1;

    //                Label lblCustCode = gvDlrCustLink.Rows[i].FindControl("lblCustCode") as Label;
    //                DataRow[] dr = dtDlrCustLink.Select("CustCode=" + Convert.ToInt32(lblCustCode.Text));
    //                dtDlrCust.Rows.Add(dr[0].ItemArray);
    //                dtDlrCustLink.Rows.Remove(dr[0]);
    //            }
    //        }
    //        if (intcnt > 0)
    //        {
    //            gvDlrCustLink.DataSource = dtDlrCustLink;
    //            gvDlrCustLink.DataBind();

    //            gvDlrCust.DataSource = dtDlrCust;
    //            gvDlrCust.DataBind();
    //        }
    //        else
    //        {
    //            lblMessage.Text = "Please Select Atleast One Record.";
    //            lblMessage.CssClass = "ErrorMessage";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMessage.Text = ex.Message;
    //        lblMessage.CssClass = "ErrorMessage";
    //    }
    //}
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Int32 intCount = Convert.ToInt32(hidSearchCount.Value.Trim()) + 1;
        CreateRow(intCount);        
        hidSearchCount.Value = intCount.ToString();
    }

    protected bool ValidateDealerCode()
    {
        string strSearchQuery = " where Status=1 and DealerCode='" + txtDealer.Text.Trim() + "'";
        DataSet dsDealer = objDealer.FetchDealerMaster(strSearchQuery);
        if (dsDealer.Tables[0].Rows.Count > 0)
        {
            BindData();
            txtDealerName.Value = dsDealer.Tables[0].Rows[0]["DealerName"].ToString();
            lblMessage.Text = "";
            lblMessage.CssClass = "";
            btnAdd.Enabled = true;
            btnSave.Enabled = true;
            return true;
        }
        else
        {
            txtDealerName.Value = "";
            btnAdd.Enabled = false;
            btnSave.Enabled = false;
            lblMessage.Text = "DealerCode Not Found.";
            lblMessage.CssClass = "ErrorMessage";
            SearchTable.Controls.Clear();
            AddHeader();            
            return false;
        }
    }

    protected bool ValidateCustomerCode(TextBox custCode)
    {
        string strSearchQuery = " where Status=1 and CustCode='" + custCode.Text.Trim() + "'";
        string ID = custCode.ID.Replace("txtCustCode_", "").ToString();
        DataSet dsDealer = objCust.FetchCustomerMaster(strSearchQuery);
                
        if (dsDealer.Tables[0].Rows.Count > 0)
        {
            Label lblCustName = Page.FindControl("lblCustName_" + ID) as Label;
            Label lblCity = Page.FindControl("lblCity_" + ID) as Label;
            DropDownList ddlDealer = Page.FindControl("ddlDealer_" + ID) as DropDownList;

            lblCustName.Text = dsDealer.Tables[0].Rows[0]["CustName"].ToString();
            lblCity.Text = dsDealer.Tables[0].Rows[0]["City"].ToString();
            ddlDealer.SelectedValue = dsDealer.Tables[0].Rows[0]["DealerCode"].ToString();

            lblMessage.Text = "";
            lblMessage.CssClass = "";
            IsValidCustCode = true;
            return true;
        }
        else
        {
            lblMessage.Text = "Cust.Code Not Found.";
            lblMessage.CssClass = "ErrorMessage";
            IsValidCustCode = false;
            return false;
        }
    }

    protected void txtDealer_TextChanged(object sender, EventArgs e)
    {
        ValidateDealerCode();
    }

    protected void txtCustCode_TextChanged(object sender, EventArgs e)
    {
        TextBox txtCustCode = (TextBox)sender;
        string[] hndNum = txtCustCode.ID.Split('_');
        HiddenField hdnCustCode = (HiddenField)Page.FindControl("hdnCustCode_" + Convert.ToInt16(hndNum[1].ToString()));
        hdnCustCode.Value = txtCustCode.Text;
        if(!ValidateCustomerCode(txtCustCode))
        txtCustCode.Focus();
    }
    
}
