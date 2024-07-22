/* ==============================================================================================
    Created By      : Subahani SM
    Created Date    : 04 Aug 2010
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
using System.Drawing;
using BusinessClass;

public partial class Admin_UserMenuMapping : System.Web.UI.Page
{
    static DataSet dsMenu;
    static DataSet dsData;
    UserMenuMapping UMM = new UserMenuMapping();
    UserMasterClass objUser = new UserMasterClass();
    protected override void LoadViewState(object earlierState)
    {
        base.LoadViewState(earlierState);
        if (ViewState["dynamictable"] == null)
            GenerateUserMenu(false);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null) Response.Redirect("../Logout.aspx");
        try
        {
            if (!IsPostBack)
            {
                UMM.GetMenus("");
                dsMenu = UMM.prpDS;
                
                if (Request.QueryString["Message"] != null)
                {
                    lblMessage.Text = "Successfully Saved.";
                    lblMessage.CssClass = "SuccessMessageBold";
                }
            }

            GenerateUserMenu(false);
            if (!IsPostBack)
                HideMenu(true);

        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
            lblMessage.CssClass = "ErrorMessage";
        }
    }
    protected void GenerateUserMenu(bool clearall)
    {
        if (clearall == true)
            PlaceUserMenuMapping.Controls.Clear();

        string PrevMainCode = "";
        string MainCode, SubMainCode;
        string MainName, SubName;
        int countSubs = 0;
        int countMains = 0;

        Table tbl = new Table();
        PlaceUserMenuMapping.Controls.Add(tbl);

        tbl.CssClass = "alternatecolorborder";
        tbl.CellSpacing = 0;
        tbl.CellPadding = 0;
        //tbl.Width = Unit.Percentage(95);
        tbl.Width = Unit.Pixel(900);
        tbl.ID = "tblMenu";
        // Add the table to the placeholder control

        UMM.GetMenus("");
        dsMenu = UMM.prpDS;
        DataTable dtMenu = dsMenu.Tables[0];
        HiddenField hdCountSubs = new HiddenField();
        TableCell tc;
        TableRow tr;

        HiddenField hdMainCode;
        HiddenField hdSubCode;

        tr = new TableRow();
        tbl.Rows.Add(tr);
        //tr.CssClass = "pagetitle";
        tc = new TableCell();
        tr.Cells.Add(tc);
        tc.CssClass = "listHeadingSmall";
        tc.ColumnSpan = 3;
        tc.Width = Unit.Percentage(100);
        tc.HorizontalAlign = HorizontalAlign.Left;
        CheckBox chkall = new CheckBox();
        tc.Controls.Add(chkall);
        chkall.ID = "chkAll";
        chkall.Text = "Select/Deselect All";
        chkall.Attributes.Add("onclick", "CheckAll()");

        HiddenField hdCountMains = new HiddenField();
        tc.Controls.Add(hdCountMains);
        hdCountMains.ID = "maincount";
        hdCountMains.Value = "";

        for (int parsemenu = 0; parsemenu < dtMenu.Rows.Count; parsemenu++)
        {
            MainCode = Convert.ToString(dtMenu.Rows[parsemenu]["MM_Code"].ToString());
            SubMainCode = Convert.ToString(dtMenu.Rows[parsemenu]["SM_Code"].ToString());
            MainName = Convert.ToString(dtMenu.Rows[parsemenu]["MM_Name"]);
            SubName = Convert.ToString(dtMenu.Rows[parsemenu]["SM_Name"]);

            if (PrevMainCode != MainCode)
            {
                if (parsemenu != 0)
                {
                    hdCountSubs.Value = countSubs.ToString();
                    countSubs = 0;
                }

                countMains += 1;
                PrevMainCode = MainCode;
                tr = new TableRow();
                tbl.Rows.Add(tr);
                //tr.CssClass = "mandatoryField";
                tr.BackColor = Color.FromArgb(167, 175, 185);

                tc = new TableCell();
                tr.Cells.Add(tc);
                tc.CssClass = "listHeadingSmall";
                tc.Width = Unit.Percentage(20);
                tc.HorizontalAlign = HorizontalAlign.Left;

                hdMainCode = new HiddenField();
                tc.Controls.Add(hdMainCode);
                hdMainCode.ID = "hdMain_" + countMains;
                hdMainCode.Value = MainCode;

                CheckBox chk = new CheckBox();
                tc.Controls.Add(chk);
                chk.ID = "chk_" + MainCode;
                chk.Text = MainName;
                chk.Attributes.Add("onclick", "CheckParent('" + countMains + "')");

                hdCountSubs = new HiddenField();
                tc.Controls.Add(hdCountSubs);
                hdCountSubs.ID = "subcount_" + countMains;
                tc.Style.Add("white-space", "nowrap");
                hdCountSubs.Value = "";

                tc = new TableCell();
                tr.Cells.Add(tc);
                tc.Width = Unit.Percentage(21);
                tr.Style.Add("white-space", "nowrap");
                tc.Style.Add("white-space", "nowrap");
                tc.HorizontalAlign = HorizontalAlign.Left;
                tc.Text = "&nbsp;";

                tc = new TableCell();
                tr.Cells.Add(tc);
                tc.Width = Unit.Percentage(60);
                tc.HorizontalAlign = HorizontalAlign.Left;
                tc.Text = "&nbsp;";
            }

            if (SubName != "")
            {
                countSubs += 1;

                tr = new TableRow();
                tbl.Rows.Add(tr);
                //tr.CssClass = "mandatoryField";

                tc = new TableCell();
                tr.Cells.Add(tc);
                tc.Width = Unit.Percentage(20);
                tc.HorizontalAlign = HorizontalAlign.Left;
                tc.Text = "&nbsp;";

                tc = new TableCell();
                tr.Cells.Add(tc);
                tc.Width = Unit.Percentage(20);
                tc.HorizontalAlign = HorizontalAlign.Left;
                tc.Text = "&nbsp;";

                tc = new TableCell();
                tr.Cells.Add(tc);
                tc.CssClass = "commonfont";
                tc.Width = Unit.Percentage(60);
                tc.HorizontalAlign = HorizontalAlign.Left;

                hdSubCode = new HiddenField();
                tc.Controls.Add(hdSubCode);
                hdSubCode.ID = "hdSub_" + countMains + "_" + countSubs;
                hdSubCode.Value = SubMainCode;

                CheckBox chk = new CheckBox();
                tc.Controls.Add(chk);
                chk.ID = "chk_" + MainCode + "_" + SubMainCode;
                chk.Text = SubName;
                chk.Attributes.Add("onclick", "CheckChild('" + countMains + "')");

            }

        }
        if (hdCountSubs.Value == "")
            hdCountSubs.Value = countSubs.ToString();
        hdCountMains.Value = countMains.ToString();
        if (dtMenu.Rows.Count > 0)
        {
            tr = new TableRow();
            tbl.Rows.Add(tr);
            tr.CssClass = "datatableth";
            tc = new TableCell();
            tr.Cells.Add(tc);
            tc.ColumnSpan = 3;
            Button btnSave = new Button();
            btnSave.ID = "btnSave";
            btnSave.CssClass = "btn saveMenu";
            btnSave.Text = "Save User Menu Mapping";
            btnSave.Click += new EventHandler(btnSave_Click);
            tc.Controls.Add(btnSave);
        }
    }

    protected bool validateChecks()
    {
        Int32 MainCount;//, SubCount;
        HiddenField HdMainCount;
        HiddenField HdMain;

        HdMainCount = Page.FindControl("maincount") as HiddenField;
        MainCount = Convert.ToInt32(HdMainCount.Value);
        for (Int32 parseMain = 1; parseMain <= MainCount; parseMain++)
        {
            HdMain = Page.FindControl("hdMain_" + parseMain) as HiddenField;
            CheckBox chkMain = Page.FindControl("chk_" + HdMain.Value) as CheckBox;
            if (chkMain.Checked == true)
                return true;
        }

        return false;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //bool flag = false;
        HiddenField HdMainCount, HdSubCount;
        Int32 MainCount, SubCount;

        HiddenField HdMainCode, HdSubCode;
        string MainCode, SubCode;

        if (!validateChecks())
        {
            lblMessage.Text = "Please Select Atleast One Menu.";
            lblMessage.CssClass = "ErrorMessage";
            return;
        }

        HdMainCount = Page.FindControl("maincount") as HiddenField;

        MainCount = Convert.ToInt32(HdMainCount.Value);
        for (Int32 parseMain = 1; parseMain <= MainCount; parseMain++)
        {
            HdSubCount = Page.FindControl("subcount_" + parseMain) as HiddenField;
            HdMainCode = Page.FindControl("hdMain_" + parseMain) as HiddenField;
            CheckBox chkMain = Page.FindControl("chk_" + HdMainCode.Value) as CheckBox;

            MainCode = HdMainCode.Value;
            SubCount = Int32.Parse(HdSubCount.Value);

            if (SubCount == 0)
            {
                UMM.prpMM_Code = MainCode;
                UMM.prpSM_Code = "0";
                UMM.prpUserId = txtUserId.Text.Trim();
                if (chkMain.Checked == false)
                    UMM.prpType = "d";
                else
                    UMM.prpType = "i";
                UMM.Insert();

            }
            else
            {
                for (Int32 parseSub = 1; parseSub <= SubCount; parseSub++)
                {
                    HdSubCode = Page.FindControl("hdSub_" + parseMain + "_" + parseSub) as HiddenField;
                    CheckBox chkSub = Page.FindControl("chk_" + MainCode + "_" + HdSubCode.Value) as CheckBox;
                    SubCode = HdSubCode.Value;

                    UMM.prpMM_Code = MainCode;
                    UMM.prpSM_Code = SubCode;
                    UMM.prpUserId = txtUserId.Text.Trim();
                    if (chkSub.Checked == false)
                        UMM.prpType = "d";
                    else
                        UMM.prpType = "i";
                    UMM.prpLoginId = Session["UserId"].ToString();
                    UMM.Insert();
                }
            }
        }


        lblMessage.Text = "Successfully Saved.";
        lblMessage.CssClass = "SuccessMessageBold";
       
    }

    protected void HideMenu(bool hidetable)
    {
        Table tbl = Page.FindControl("tblMenu") as Table;
        if (hidetable == true)
            tbl.Style.Add("display", "none");
        else
            tbl.Style.Remove("display");
    }

    protected void MapRoles()
    {
        string MainCode, SubCode;
        CheckBox Main, Sub;
        string PrevMain = "";
        bool MainFlag = false;
        int MainCount = 0, SubCount = 0;
        try
        {
            UMM.GetMenus(txtUserId.Text.Trim());
            dsData = UMM.prpDS;
        }
        catch (Exception ex)
        {
            
        }

        for (int parseData = 0; parseData < dsData.Tables[0].Rows.Count; parseData++)
        {

            MainCode = dsData.Tables[0].Rows[parseData]["MM_Code"].ToString();
            SubCode = dsData.Tables[0].Rows[parseData]["SM_Code"].ToString();

            if (PrevMain != MainCode)
            {
                PrevMain = MainCode;
                MainCount += 1;
                SubCount = 0;
                MainFlag = false;
            }

            Main = Page.FindControl("chk_" + MainCode) as CheckBox;
            if (Convert.ToString(dsData.Tables[0].Rows[parseData]["SM_Code"]) != "")
            {
                SubCount += 1;
                Sub = Page.FindControl("chk_" + MainCode + "_" + SubCode) as CheckBox;
                if (Convert.ToString(dsData.Tables[0].Rows[parseData]["UM_MM_Code"]) != "")
                {
                 if(Sub!=null)  Sub.Checked = true;
                    MainFlag = true;
                }
                else
                    if (Sub != null) Sub.Checked = false;

            }
            else
            {
                if (Convert.ToString(dsData.Tables[1].Rows[parseData]["UM_MM_Code"]) != "")
                    MainFlag = true;
                else
                    MainFlag = false;
            }
            Main.Checked = MainFlag;
        }

    }

    protected void btnGetdata_Click(object sender, EventArgs e)
    {
        if (txtUserId.Text=="")
            HideMenu(true);
        else
        {
            HideMenu(false);
            MapRoles();
        }
        lblMessage.Text = "";
        lblMessage.CssClass = null;

    }
    protected bool ValidateUserName()
    {
        string strSearchQuery = " where Active=1 and UserId='" + txtUserId.Text.Trim() + "'";
        DataSet dsBudget = objUser.FetchUserMaster(strSearchQuery);
        if (dsBudget.Tables[0].Rows.Count > 0)
        {
            txtUserName.Text=dsBudget.Tables[0].Rows[0]["UserName"].ToString();
            lblMessage.Text = "";
            lblMessage.CssClass = "";            
            return true;

        }
        else
        {
            txtUserId.Focus();
            txtUserName.Text = "";
            lblMessage.Text = "User Not Found.";
            lblMessage.CssClass = "ErrorMessage";
            return false;
        }


    }

    protected void txtUserId_TextChanged(object sender, EventArgs e)
    {       
        if (txtUserId.Text == "" || !ValidateUserName())
        {
            HideMenu(true);
            LiteralControl Lc = new LiteralControl();
            Lc.Text = "<var class='ErrorMessage' style='font-style:normal;margin-left:23px;'>UserID Not Found.</var>";
            Page.Controls.Add(Lc);
        }
        else
        {
            HideMenu(false);
            MapRoles();
            //Page.Controls.Add("<var class='ErrorMessage'>User Not Found.</var>";

        }
        lblMessage.Text = "";
        lblMessage.CssClass = null;
    }
}
