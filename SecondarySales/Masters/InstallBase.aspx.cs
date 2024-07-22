using AjaxControlToolkit;
using BusinessClass;
using System;
using System.Data;
using System.Reflection;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
//using static System.Net.Mime.MediaTypeNames;

public partial class Masters_InstallBase : System.Web.UI.Page
{

    private CustomerMasterClass objCust = new CustomerMasterClass();
    private CommonFunction objComm = new CommonFunction();
    ItemMasterClass objItem = new ItemMasterClass();
    private DataSet dsItem;
    //protected ToolkitScriptManager ScriptManager1;
    //protected HomeTopFrame TopFrame;
    //protected HiddenField hidSearchCount;
    //protected HiddenField hdnCurrentDate;
    //protected HiddenField hdnDateFormat;
    //protected Label lblCustCode;
    //protected Label lblCustName;
    //protected Label lblDealerCode;
    //protected HtmlTable TblParam;
    //protected Table SearchTable;
    //protected PlaceHolder phItem;
    //protected Panel PnlHeader;
    //protected Button btnBack;
    //protected Button btnCancel;
    //protected Button btnSave;
    //protected Button btnDelete;
    //protected Label lblMessage;
    //protected UpdatePanel upReceipt;
    //protected HtmlForm frmCustItemPrice;

    protected override void LoadViewState(object earlierState)
    {
        base.LoadViewState(earlierState);
        if (this.ViewState["dynamictable"] != null)
            return;
        this.GenerateSearchModel(false);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Session["UserID"] == null)
            this.Response.Redirect("../Logout.aspx");
        try
        {
            if (!this.IsPostBack)
            {
                this.lblMessage.Text = "";
                this.lblMessage.CssClass = "";
                this.hdnCurrentDate.Value = DateTime.Now.ToString(this.Session["DateFormat"].ToString());
                this.hdnDateFormat.Value = this.Session["DateFormat"].ToString();
                this.hdnDateFormat.Value = this.hdnDateFormat.Value.ToUpper();
                this.lblDealerCode.Text = this.Request.QueryString["DlrCode"].ToString();
                this.lblCustCode.Text = this.Request.QueryString["CustCode"].ToString();
                this.lblCustName.Text = this.Request.QueryString["CustName"].ToString();
                if (this.Request.QueryString["Type"] != null && this.Request.QueryString["Type"].ToString() != "")
                {
                    this.lblMessage.Text = "Saved/Updated Successfully.";
                    this.lblMessage.CssClass = "SuccessMessageBold";
                }
            }
            this.GenerateSearchModel(false);
            if (this.IsPostBack || this.Request.QueryString["CustCode"] == null)
                return;
            this.BindData();
        }
        catch (Exception ex)
        {
            this.lblMessage.Text = ex.Message;
            this.lblMessage.CssClass = "ErrorMessage";
        }
    }
    protected void BindData()
    {
        (this.Page.FindControl("SearchTable") as Table).Controls.Clear();
        this.AddHeader();
        this.objCust.prpDealerCode = Convert.ToInt32(this.lblDealerCode.Text.Trim());
        this.objCust.prpCustCode = Convert.ToInt32(this.lblCustCode.Text.Trim());
       // this.dsItem = this.objCust.FetchCustItemList();
        this.dsItem = this.objCust.FetchEquipmentItemInsatallBaselist(" where Status=1", Convert.ToInt32(this.lblCustCode.Text));
        for (int index = 0; index < this.dsItem.Tables[0].Rows.Count; ++index)
            this.CreateRow(index + 1);
        this.hidSearchCount.Value = Convert.ToString(this.dsItem.Tables[0].Rows.Count);
        //for (int index = 0; index < this.dsItem.Tables[0].Rows.Count; ++index)
        //{
        //    int num = index + 1;
        //    HiddenField control1 = this.Page.FindControl("hdnItemId_" + (object)num) as HiddenField;
        //    HiddenField control2 = this.Page.FindControl("hdnDIMId_" + (object)num) as HiddenField;
        //    HiddenField control3 = this.Page.FindControl("hdnCPMId_" + (object)num) as HiddenField;
        //    Label control4 = this.Page.FindControl("lblItemCode_" + (object)num) as Label;
        //    Label control5 = this.Page.FindControl("lblDesc1_" + (object)num) as Label;
        //    Label control6 = this.Page.FindControl("lblDesc2_" + (object)num) as Label;
        //    this.Page.FindControl("lblQty_" + (object)num);
        //    TextBox control7 = this.Page.FindControl("txtValidUpto_" + (object)num) as TextBox;
        //    this.Page.FindControl("txtRemarks_" + (object)num);
        //    control1.Value = this.dsItem.Tables[0].Rows[index]["ItemId"].ToString();
        //    control2.Value = this.dsItem.Tables[0].Rows[index]["DIMId"].ToString();
        //    control3.Value = this.dsItem.Tables[0].Rows[index]["CPMId"].ToString();
        //    control4.Text = this.dsItem.Tables[0].Rows[index]["ItemCode"].ToString();
        //    control5.Text = this.dsItem.Tables[0].Rows[index]["Description1"].ToString();
        //    control6.Text = this.dsItem.Tables[0].Rows[index]["Description2"].ToString();
        //    if (this.dsItem.Tables[0].Rows[index]["ValidUpto"] != null && this.dsItem.Tables[0].Rows[index]["ValidUpto"].ToString() != "")
        //        control7.Text = Convert.ToDateTime(this.dsItem.Tables[0].Rows[index]["ValidUpto"].ToString()).ToString(this.Session["DateFormat"].ToString());
        //}
        for (int index = 0; index < this.dsItem.Tables[0].Rows.Count; ++index)
        {
            int num = index + 1;
            HiddenField control1 = this.Page.FindControl("hdnItemId_" + (object)num) as HiddenField;
            HiddenField control2 = this.Page.FindControl("hdnDIMId_" + (object)num) as HiddenField;
            HiddenField control3 = this.Page.FindControl("hdnCPMId_" + (object)num) as HiddenField;
            Label control4 = this.Page.FindControl("lblItemCode_" + (object)num) as Label;
            Label control5 = this.Page.FindControl("lblDesc1_" + (object)num) as Label;
            Label control6 = this.Page.FindControl("lblDesc2_" + (object)num) as Label;
            //this.Page.FindControl("lblQty_" + (object)num);
            TextBox control7 = this.Page.FindControl("txtValidUpto_" + (object)num) as TextBox;
            this.Page.FindControl("txtRemarks_" + (object)num);
            control1.Value = this.dsItem.Tables[0].Rows[index]["Id"].ToString();
            control2.Value = this.dsItem.Tables[0].Rows[index]["GroupCode"].ToString();
            control3.Value = this.dsItem.Tables[0].Rows[index]["ItemCode"].ToString();
            control4.Text = this.dsItem.Tables[0].Rows[index]["ItemCode"].ToString();
            control5.Text = this.dsItem.Tables[0].Rows[index]["Description1"].ToString();
            control6.Text = this.dsItem.Tables[0].Rows[index]["Description2"].ToString();       
            if(this.dsItem.Tables[0].Rows[index]["InstallDate"].ToString() != "")
            control7.Text = Convert.ToDateTime(this.dsItem.Tables[0].Rows[index]["InstallDate"].ToString()).ToString(this.Session["DateFormat"].ToString());
        }
    }

    protected void GenerateSearchModel(bool clearall)
    {
        if (clearall)
        {
            this.hidSearchCount.Value = "0";
            (this.Page.FindControl("SearchTable") as Table).Controls.Clear();
        }
        int num = int.Parse(this.hidSearchCount.Value);
        this.AddHeader();
        for (int SearchCount = 1; SearchCount <= num; ++SearchCount)
            this.CreateRow(SearchCount);
        this.ViewState["dynamictable"] = (object)true;
    }

    protected void AddHeader()
    {
        TableRow row = new TableRow();
        this.SearchTable.Rows.Add(row);
        row.ID = "tr_0";
        row.CssClass = "TblHeader";
        row.Cells.Add(new TableCell()
        {
            Text = "Select",
            HorizontalAlign = HorizontalAlign.Left
        });
        TableCell cell = new TableCell();
        cell.Text = "Delete";
        cell.Style.Add("display", "none");
        cell.HorizontalAlign = HorizontalAlign.Left;
        row.Cells.Add(cell);
        row.Cells.Add(new TableCell()
        {
            Text = "Item Code",
            HorizontalAlign = HorizontalAlign.Left
        });
        row.Cells.Add(new TableCell()
        {
            Text = "Description1",
            HorizontalAlign = HorizontalAlign.Left
        });
        row.Cells.Add(new TableCell()
        {
            Text = "Description2",
            HorizontalAlign = HorizontalAlign.Left
        });
        //row.Cells.Add(new TableCell()
        //{
        //    Text = "Qty",
        //    HorizontalAlign = HorizontalAlign.Left
        //});
        row.Cells.Add(new TableCell()
        {
            Text = "Inst Date",
            HorizontalAlign = HorizontalAlign.Left
        });
    }

    protected void CreateRow(int SearchCount)
    {
        TableRow row = new TableRow();
        this.SearchTable.Rows.Add(row);
        row.ID = "tr_" + SearchCount.ToString();
        row.Attributes.Add("runat", "server");
        TableCell cell1 = new TableCell();
        row.Cells.Add(cell1);
        cell1.HorizontalAlign = HorizontalAlign.Left;
        CheckBox child1 = new CheckBox();
        child1.ID = "Chkdelete_" + SearchCount.ToString();
        cell1.Controls.Add((Control)child1);
        TableCell cell2 = new TableCell();
        row.Cells.Add(cell2);
        cell2.Style.Add("display", "none");
        cell2.HorizontalAlign = HorizontalAlign.Left;
        Button child2 = new Button();
        child2.CssClass = "btn deletebtn";
        child2.Width = Unit.Pixel(15);
        child2.ID = "btnDelete_" + SearchCount.ToString();
        child2.Attributes.Add("OnClick", "return confirm('Are You Sure You Want To Delete The Record?')");
        child2.Click += new EventHandler(this.btnDelete_Click);
        HiddenField child3 = new HiddenField();
        child3.ID = "hdnItemId_" + SearchCount.ToString();
        HiddenField child4 = new HiddenField();
        child4.ID = "hdnDIMId_" + SearchCount.ToString();
        HiddenField child5 = new HiddenField();
        child5.ID = "hdnRow_" + SearchCount.ToString();
        cell2.Controls.Add((Control)child2);
        cell2.Controls.Add((Control)child3);
        cell2.Controls.Add((Control)child4);
        cell2.Controls.Add((Control)child5);
        TableCell cell3 = new TableCell();
        row.Cells.Add(cell3);
        cell3.HorizontalAlign = HorizontalAlign.Left;
        Label child6 = new Label();
        child6.CssClass = "commonfont textdropwidth";
        child6.ID = "lblItemCode_" + SearchCount.ToString();
        HiddenField child7 = new HiddenField();
        child7.ID = "hdnCPMId_" + SearchCount.ToString();
        cell3.Controls.Add((Control)child6);
        cell3.Controls.Add((Control)child7);
        TableCell cell4 = new TableCell();
        row.Cells.Add(cell4);
        cell4.HorizontalAlign = HorizontalAlign.Left;
        Label child8 = new Label();
        child8.CssClass = "commonfont textdropwidth";
        child8.ID = "lblDesc1_" + SearchCount.ToString();
        cell4.Controls.Add((Control)child8);
        TableCell cell5 = new TableCell();
        row.Cells.Add(cell5);
        cell5.HorizontalAlign = HorizontalAlign.Left;
        Label child9 = new Label();
        child9.CssClass = "commonfont textdropwidth";
        child9.ID = "lblDesc2_" + SearchCount.ToString();
        cell5.Controls.Add((Control)child9);
        //TableCell cell6 = new TableCell();
        //row.Cells.Add(cell6);
        //cell6.HorizontalAlign = HorizontalAlign.Left;
        //TextBox child10 = new TextBox();
        //child10.CssClass = "commonfont textdropwidth";
        //child10.ID = "lblQty_" + SearchCount.ToString();
        //cell6.Controls.Add((Control)child10);
        //child10.Text = "1";
        TableCell cell7 = new TableCell();
        row.Cells.Add(cell7);
        cell7.HorizontalAlign = HorizontalAlign.Left;
        TextBox child11 = new TextBox();
        child11.MaxLength = 12;
        child11.CssClass = "commonfont textdropwidthFinesmall";
        child11.ID = "txtValidUpto_" + SearchCount.ToString();
        ImageButton child12 = new ImageButton();
        child12.ImageUrl = "~/Common/Images/datePickerPopup.gif";
        child12.ID = "imgValidUpto_" + SearchCount.ToString();
        child12.Style.Add("vertical-align", "bottom");
        CalendarExtender child13 = new CalendarExtender();
        child13.ID = "ceValid_+" + SearchCount.ToString();
        child13.TargetControlID = child11.ID;
        child13.PopupButtonID = child12.ID;
        child13.CssClass = "CalendarDatePicker";
        child13.Format = this.Session["DateFormat"].ToString();
        cell7.Controls.Add((Control)child11);
        cell7.Controls.Add((Control)child12);
        cell7.Controls.Add((Control)child13);
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        this.Page.FindControl("SearchTable");
        string s = (sender as Button).ID.Replace("btnDelete_", "");
        int.Parse(s);
        int.Parse(this.hidSearchCount.Value);
        string str1 = (this.Page.FindControl("hdnCPMId_" + s) as HiddenField).Value;
        (this.Page.FindControl("hdnRow_" + s) as HiddenField).Value = "S";
        (this.Page.FindControl("tr_" + s) as TableRow).Style.Add("display", "none");
        if (str1 != "" && str1 != "0")
        {
            this.objCust.prpCPMId = Convert.ToInt32(str1);
            string str2 = this.objCust.DeleteInstallBase();
            if (str2 == "")
            {
                this.lblMessage.Text = "Deleted Successfully.";
                this.lblMessage.CssClass = "SuccessMessageBold";
            }
            else
            {
                this.lblMessage.Text = str2;
                this.lblMessage.CssClass = "ErrorMessage";
            }
        }
        else
        {
            this.lblMessage.Text = "Deleted Successfully.";
            this.lblMessage.CssClass = "SuccessMessageBold";
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string str = "";
        Table control1 = this.Page.FindControl("SearchTable") as Table;
        try
        {
            this.objCust.prpUserId = this.Session["UserID"].ToString();
            this.objCust.prpCustCode = Convert.ToInt32(this.lblCustCode.Text.Trim());
            for (int index = 1; index < control1.Rows.Count; ++index)
            {
                CheckBox control2 = this.Page.FindControl("Chkdelete_" + (object)index) as CheckBox;
                HiddenField control3 = this.Page.FindControl("hdnRow_" + (object)index) as HiddenField;
                HiddenField control4 = this.Page.FindControl("hdnItemId_" + (object)index) as HiddenField;
                this.Page.FindControl("hdnDIMId_" + (object)index);
                HiddenField control5 = this.Page.FindControl("hdnCPMId_" + (object)index) as HiddenField;
                this.Page.FindControl("lblItemCode_" + (object)index);
                TextBox control6 = this.Page.FindControl("lblItemCode_" + (object)index) as TextBox;
                TextBox control7 = this.Page.FindControl("txtValidUpto_" + (object)index) as TextBox;
                string ItemCode = ((System.Web.UI.WebControls.Label)this.Page.FindControl("lblItemCode_" + (object)index)).Text;
                if (!(control3.Value == "S"))
                {
                    if(control7.Text != null && control7.Text != "")
                    {
                        this.objCust.prpItemId = Convert.ToInt32(control4.Value.Trim());
                        this.objCust.prpCustCode = Convert.ToInt32(this.lblCustCode.Text);
                        this.objCust.prpItemCode = ItemCode;
                        this.objCust.prpInstDate = this.objComm.DateFormateConversion(control7.Text.Trim(), this.Session["DateFormat"].ToString());
                        str = this.objCust.SaveInstallBase();
                    }
                    
                    //if (control7.Text.Trim() != "")
                    //{
                    //    if (control2.Checked)
                    //    {
                    //        this.objCust.prpItemId = (int)Convert.ToInt16(control4.Value);
                    //        this.objCust.prpQty = control6.Text.Trim();
                    //        this.objCust.prpInstallbaseCheck = control2.Checked;
                    //        this.objCust.prpValidUpto = this.objComm.DateFormateConversion(control7.Text.Trim(), this.Session["DateFormat"].ToString());
                    //        str = this.objCust.SaveInstallBase();
                    //    }
                    //    if (str != "")
                    //    {
                    //        this.lblMessage.Text = str;
                    //        this.lblMessage.CssClass = "ErrorMessage";
                    //        return;
                    //    }
                    //}
                }
            }
            if (str == "")
            {
                this.Response.Redirect("InstallBase.aspx?Type=s&DlrCode=" + this.lblDealerCode.Text + "&CustCode=" + this.lblCustCode.Text.Trim() + "&CustName=" + this.lblCustName.Text.Trim());
            }
            else
            {
                this.lblMessage.Text = str;
                this.lblMessage.CssClass = "ErrorMessage";
            }
        }
        catch (Exception ex)
        {
            this.lblMessage.Text = ex.Message;
            this.lblMessage.CssClass = "ErrorMessage";
        }
    }  

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("InstallBase.aspx?DlrCode=" + this.lblDealerCode.Text + "&CustCode=" + this.lblCustCode.Text.Trim() + "&CustName=" + this.lblCustName.Text.Trim());
    }
    protected void btnBack_Click(object sender, EventArgs e) 
    {
        this.Response.Redirect("CustomerMasterAddEdit.aspx?New=N&strCode=" + this.lblCustCode.Text);
    }

    protected void btnDelete_Click1(object sender, EventArgs e)
    {
        int num1 = 0;
        int num2 = 0;
        int num3 = 0;
        foreach (Control row in this.SearchTable.Rows)
        {
            CheckBox control1 = (CheckBox)row.FindControl("Chkdelete_" + num1.ToString());
            if(control1!=null)
            {
                if (control1.Checked)
                {
                    string str1 = (this.Page.FindControl("hdnCPMId_" + num1.ToString()) as HiddenField).Value;
                    string Id = (this.Page.FindControl("hdnItemId_" + num1.ToString()) as HiddenField).Value;
                    //this.Page.FindControl("hdnItemId_" + (object)index)
                    TableRow control = this.Page.FindControl("tr_" + num1.ToString()) as TableRow;
                    (this.Page.FindControl("hdnRow_" + num1.ToString()) as HiddenField).Value = "S";
                    control.Style.Add("display", "none");
                    this.lblMessage.Text = "Deleted Successfully.";
                    this.lblMessage.CssClass = "SuccessMessageBold";
                    if (str1 != "" && str1 != "0")
                    {
                        this.objCust.prpCPMId = Convert.ToInt32(Id);
                        string str2 = this.objCust.DeleteInstallBase();
                        if (str2 == "")
                        {
                            this.lblMessage.Text = "Deleted Successfully.";
                            this.lblMessage.CssClass = "SuccessMessageBold";
                        }
                        else
                        {
                            this.lblMessage.Text = str2;
                            this.lblMessage.CssClass = "ErrorMessage";
                        }
                    }
                }
                else if (!control1.Checked)
                    ++num3;
                ++num2;
            }
            ++num1;
        }
        this.BindData();

        if (num2 != num3)
            return;
        this.lblMessage.Text = "Please Select Atleast one Item to Delete";
        this.lblMessage.CssClass = "ErrorMessage";
    }
}