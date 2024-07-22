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

public partial class Masters_EquipmentProductGroupAddEdit : System.Web.UI.Page
{

    CommonFunction objComm = new CommonFunction();
    ProductGroupClass objBC = new ProductGroupClass();
    ProductGroupClass objGroup = new ProductGroupClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null) Response.Redirect("../Logout.aspx");
        try
        {
            if (!IsPostBack)
            {
                btnSave.Attributes.Add("onclick", "return fnValidateSave()");
                if (Request.QueryString["New"] != null)
                {
                    if (Request.QueryString["New"].ToString() == "S")
                        lblHeader.Text = "Add - Equipment Product Group";
                    else
                        lblHeader.Text = "Edit - Equipment Product Group";


                }
                if (Request.QueryString["strCode"] != null && Request.QueryString["strCode"].ToString() != "" && Request.QueryString["New"].ToString() != "S")
                {
                    hdnBCId.Value = Request.QueryString["strCode"].ToString();
                    BindData();
                    txtProdGroup.ReadOnly = true;
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }
    protected void BindData()
    {
        objBC.prpPGId = Convert.ToInt32(hdnBCId.Value);
        DataSet ds = objBC.FetchEquipmentProductClassOnId();
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtProdGroup.Text = ds.Tables[0].Rows[0]["GroupCode"].ToString().Trim();
            txtDescrip.Text = ds.Tables[0].Rows[0]["GroupDesc"].ToString().Trim();
            chkActive.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Status"].ToString().Trim());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string stat;
        if (hdnBCId.Value.Trim() == "0")
            stat = "S";
        else
            stat = "U";

        try
        {
            objBC.prpUserId = Session["UserID"].ToString();
            objBC.prpPGId = Convert.ToInt32(hdnBCId.Value);
            objBC.prpGroupCode = txtProdGroup.Text.Trim();
            objBC.prpGroupDesc = txtDescrip.Text.Trim();
            objBC.prpStatus = chkActive.Checked;
            string strResult = objBC.SaveEquipmentProductClass();
            if (strResult == "")
                Response.Redirect("EquipmentProductGroup.aspx?stat=" + stat, false);
            else
            {
                lblMessage.Text = strResult;
                lblMessage.CssClass = "ErrorMessage";
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
            lblMessage.CssClass = "ErrorMessage";
        }
    }
    //this is for  select product code
    protected bool ValidateProductGroupCode()
    {
        string strSearchQuery = " where Status=1 and GroupCode='" + txtProdGroup.Text.Trim() + "' ";
        DataSet dsGroup = objGroup.FetchProductGroup(strSearchQuery);

        if (dsGroup.Tables[0].Rows.Count > 0)
        {
            lblMessage.Text = "";
            lblMessage.CssClass = "";
            return true;
        }
        else
        {
            lblMessage.Text = "ProductGroupCode Not Found.";
            lblMessage.CssClass = "ErrorMessage";
            return false;
        }
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("EquipmentProductGroupAddEdit.aspx?strCode=" + hdnBCId.Value + "&&New=" + Request.QueryString["New"]);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("EquipmentProductGroup.aspx");
    }
}
