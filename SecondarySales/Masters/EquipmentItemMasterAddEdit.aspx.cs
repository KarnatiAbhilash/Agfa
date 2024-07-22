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

public partial class Masters_EquipmentItemMasterAddEdit : System.Web.UI.Page
{


    CommonFunction objComm = new CommonFunction();
    ItemMasterClass objItem = new ItemMasterClass();
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
                        lblHeader.Text = "Add - Equipment Item  Master";
                    else
                        lblHeader.Text = "Edit - Equipment Item  Master";


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

    //This is for bindata
    protected void BindData()
    {
        objItem.prpId = Convert.ToInt32(hdnBCId.Value);
        DataSet ds = objItem.FetchEquipmentItemClassOnId();
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtItemCode.Text = ds.Tables[0].Rows[0]["ItemCode"].ToString().Trim();
            txtProdGroup.Text = ds.Tables[0].Rows[0]["GroupCode"].ToString().Trim();
            txtDescr1.Text = ds.Tables[0].Rows[0]["Description1"].ToString().Trim();
            txtDescr2.Text = ds.Tables[0].Rows[0]["Description2"].ToString().Trim();
            chkActive.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Status"].ToString().Trim());
        }
    }
    //this is for save 
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string stat;
        if (hdnBCId.Value.Trim() == "0")
            stat = "S";
        else
            stat = "U";

        try
        {
            if (true)
            {
                objItem.prpUserId = Session["UserID"].ToString();
                objItem.prpId = Convert.ToInt32(hdnBCId.Value);
                objItem.prpItemCode = txtItemCode.Text.Trim();
                objItem.prpGroupCode = txtProdGroup.Text.Trim();
                objItem.prpDesc1 = txtDescr1.Text.Trim();
                objItem.prpDesc2 = txtDescr2.Text.Trim();
                objItem.prpStatus = chkActive.Checked;
                string strResult = objItem.SaveEquipmentItemMaster();
                if (strResult == "")
                    Response.Redirect("EquipmentItemMaster.aspx?stat=" + stat, false);
                else
                {
                    lblMessage.Text = strResult;
                    lblMessage.CssClass = "ErrorMessage";
                }
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
        Response.Redirect("EquipmentItemMasterAddEdit.aspx?strCode=" + hdnBCId.Value + "&&New=" + Request.QueryString["New"]);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("EquipmentItemMaster.aspx");
    }
    protected void txtProdGroup_TextChanged(object sender, EventArgs e)
    {
        ValidateProductGroupCode();
    }

}




