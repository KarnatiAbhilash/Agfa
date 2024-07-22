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
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BusinessClass;

public partial class Masters_ItemMasterAddEdit : Page, IRequiresSessionState
{
    CommonFunction objComm = new CommonFunction();
    ItemMasterClass objItem = new ItemMasterClass();
    BudgetClassMaster objBudget = new BudgetClassMaster();
    ProductFamilyClass objProb = new ProductFamilyClass();
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
                    {
                        lblHeader.Text = "Add - Item Master";
                        txtBudgetClassCode.ReadOnly = false;
                        txtProdFamily.ReadOnly = false;
                        txtProdGroup.ReadOnly = false;
                        txtItemCode.ReadOnly = false;
                        txtConvFactor.ReadOnly = false;                     

                    }
                    else
                    {
                        lblHeader.Text = "Edit - Item Master";
                        btnBudget.Disabled = true;
                        btnProd.Disabled = true;
                        btnProbGrp.Disabled = true;
                        txtItemCode.ReadOnly = true;
                        txtBudgetClassCode.ReadOnly = true;
                        txtProdFamily.ReadOnly = true;
                        txtProdGroup.ReadOnly = true;
                        txtConvFactor.ReadOnly = true;                     

                    }


                }
                if (Request.QueryString["strCode"] != null && Request.QueryString["strCode"].ToString() != "" && Request.QueryString["New"].ToString() != "S")
                {
                    hdnId.Value = Request.QueryString["strCode"].ToString();
                    BindData();                   
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
            lblMessage.CssClass = "ErrorMessage";
        }
    }
    protected void BindData()
    {
        objItem.prpId = Convert.ToInt32(hdnId.Value);
        DataSet ds = objItem.FetchItemMasterOnId();
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtItemCode.Text = ds.Tables[0].Rows[0]["ItemCode"].ToString().Trim();
            txtProdFamily.Text = ds.Tables[0].Rows[0]["PFCode"].ToString().Trim();
            txtBudgetClassCode.Text = ds.Tables[0].Rows[0]["BCCode"].ToString().Trim();
            txtProdGroup.Text = ds.Tables[0].Rows[0]["GroupCode"].ToString().Trim();
            txtDescr1.Text = ds.Tables[0].Rows[0]["Description1"].ToString().Trim();
            txtDescr2.Text = ds.Tables[0].Rows[0]["Description2"].ToString().Trim();
            txtConvFactor.Text = ds.Tables[0].Rows[0]["ConvFactor"].ToString().Trim();
            chkActive.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Status"].ToString().Trim());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string stat;
        if (hdnId.Value.Trim() == "0")
            stat = "S";
        else
            stat = "U";

        try
        {
            if(ValidateBudgetClassCode() && ValidateProductFamilyCode() && ValidateProductGroupCode())
            {
            objItem.prpUserId = Session["UserID"].ToString();
            objItem.prpId = Convert.ToInt32(hdnId.Value);
            objItem.prpItemCode = txtItemCode.Text.Trim();
            objItem.prpPFCode = txtProdFamily.Text.Trim();
            objItem.prpBCCode = txtBudgetClassCode.Text.Trim();
            objItem.prpGroupCode = txtProdGroup.Text.Trim();
            objItem.prpDesc1 = txtDescr1.Text.Trim();
            objItem.prpDesc2 = txtDescr2.Text.Trim();
            objItem.prpConvFactor = Convert.ToDouble(txtConvFactor.Text.Trim());
            objItem.prpStatus = chkActive.Checked;
            string strResult = objItem.SaveItemMaster();
            if (strResult == "")
                Response.Redirect("ItemMaster.aspx?stat=" + stat, false);
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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ItemMasterAddEdit.aspx?strCode=" + hdnId.Value + "&&New=" + Request.QueryString["New"]);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ItemMaster.aspx");
    }
    private bool ValidateBudgetClassCode()
    {
        string strSearchQuery = " where Status=1 and BCCode='" + txtBudgetClassCode.Text.Trim() + "'";
        DataSet dsBudget = objBudget.FetchBudgetClass(strSearchQuery);

        if (dsBudget.Tables[0].Rows.Count > 0)
        {
            lblMessage.Text = "";
            lblMessage.CssClass = "";
            return true;
        }
        else
        {
            lblMessage.Text = "BudgetClassCode Not Found.";
            lblMessage.CssClass = "ErrorMessage";
            return false;
        }
    }

    private bool ValidateProductFamilyCode()
    {

        string strSearchQuery = " where Status=1 and PFCode='" + txtProdFamily.Text.Trim() + "' and BCCode='" + txtBudgetClassCode.Text.Trim() + "'";

        DataSet dsProd = objProb.FetchProductFamily(strSearchQuery);

        if (dsProd.Tables[0].Rows.Count > 0)
        {
            lblMessage.Text = "";
            lblMessage.CssClass = "";
            return true;
        }
        else
        {
            lblMessage.Text = "ProductFamilyCode Not Found.";
            lblMessage.CssClass = "ErrorMessage";
            return false;
        }

    }

    protected bool ValidateProductGroupCode()
    {
        string  strSearchQuery = " where Status=1 and GroupCode='" + txtProdGroup.Text.Trim() + "' and BCCode='" + txtBudgetClassCode.Text.Trim() + "' and PFCode='" + txtProdFamily.Text.Trim() + "'";
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


    protected void txtBudgetClassCode_TextChanged(object sender, EventArgs e)
    {
        ValidateBudgetClassCode();
    }
    protected void txtProdFamily_TextChanged(object sender, EventArgs e)
    {
        ValidateProductFamilyCode();
    }
    protected void txtProdGroup_TextChanged(object sender, EventArgs e)
    {
        ValidateProductGroupCode();
    }
}

