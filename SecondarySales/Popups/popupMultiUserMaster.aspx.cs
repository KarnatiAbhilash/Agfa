/* ==============================================================================================
    Created By      : Subahani SM
    Created Date    : 28 July 2010
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
using System.Web.Caching;
using BusinessClass;
using System.Text;

public partial class popupMultiUserMaster : System.Web.UI.Page
{
    UserMasterClass objUser = new UserMasterClass();
    DataSet dsBudget;
    static string SelectType;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null) Response.Redirect("../Logout.aspx");
        try
        {
            if (!IsPostBack)
            {
                BindUser(true, false);
                SelectType = Convert.ToString(Request.QueryString["SelectType"]);
                hidSelectType.Value = SelectType;
            }
        }
        catch (Exception ex)
        {
            
        }
    }
    protected void BindUser(bool isrebind, bool issearch)
    {
        string strSearchQuery = "";

        if (issearch)
            strSearchQuery = " where Active=1 and " + ddlsearch.SelectedValue + " like '%" + txtValue.Text.Trim() + "%'";
        else
            strSearchQuery = " where Active=1 ";

        dsBudget = objUser.FetchUserMaster(strSearchQuery);
        Session["dsUser"] = dsBudget;
        if (isrebind == true)
        {
            gvUser.DataSource = dsBudget;
            gvUser.DataBind();
            if (dsBudget.Tables[0].Rows.Count > 0)
            {
                lblMessage.Text = "";
                lblMessage.CssClass = "";
                btnSave.Visible = true;
            }
            else
            {
                lblMessage.Text = "No Record(s) Found.";
                lblMessage.CssClass = "ErrorMessage";
                btnSave.Visible = false;
            }
        }


    }
    protected void gvUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvUser.PageIndex = e.NewPageIndex;
        gvUser.DataSource = Session["dsUser"];
        gvUser.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {        
        int intcnt = 0;
        try
        {
            StringBuilder resPossibleUser = new StringBuilder();
            for (int i = 0; i < gvUser.Rows.Count; i++)
            {
                CheckBox chkSelect = gvUser.Rows[i].FindControl("ChkSelect") as CheckBox;

                if (chkSelect.Checked)
                {
                    intcnt = intcnt + 1;
                    Label lblUserid = gvUser.Rows[i].FindControl("lblUserid") as Label;
                    Label lblUserName = gvUser.Rows[i].FindControl("lblName") as Label;
                    if(intcnt==1)
                    resPossibleUser.Append(lblUserid.Text);
                    else
                    resPossibleUser.Append("," + lblUserid.Text);
                    resPossibleUser.ToString().Trim(',').Trim(',');
                }
            }
            if(intcnt>0)
            ClientScript.RegisterClientScriptBlock(GetType(), "CallUser", "<script type=\"text/javascript\" language=\"javascript\">FillParentWindow(\"" + hidSelectType.Value + "\",\"" + resPossibleUser.ToString() + "\",\"" + "" + "\")</script>");

            if (intcnt <= 0)
            {
                lblMessage.Text = "Please Select Atleast One Record.";
                lblMessage.CssClass = "ErrorMessage";
            }
            else
            {
                lblMessage.CssClass = "";
            }
        }
        catch (Exception ex)
        {
            
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtValue.Text = "";
        BindUser(true, false);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {       
        try
        {
            BindUser(true, true);
        }
        catch (Exception ex)
        {
            
        }
    }
}