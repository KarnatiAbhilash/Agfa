/* ==============================================================================================
    Created By      : Subahani SM
    Created Date    : 23 July 2010
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

public partial class Masters_ProductGroupMasterAddEdit : System.Web.UI.Page
{
    CommonFunction objComm = new CommonFunction();
    ProductGroupClass objGP = new ProductGroupClass();
    BudgetClassMaster objBudget = new BudgetClassMaster();
    ProductFamilyClass objProb = new ProductFamilyClass();

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
                        lblHeader.Text = "Add - Product Group Master";
                        txtBudgetClassCode.ReadOnly = false;
                        txtProdFamily.ReadOnly = false;
                        txtProdGroup.ReadOnly = false;

                    }
                    else
                    {
                        lblHeader.Text = "Edit - Product Group Master";
                        txtBudgetClassCode.ReadOnly = true;
                        txtProdGroup.ReadOnly = true;
                        txtProdFamily.ReadOnly = true;

                    }


                }
                if (Request.QueryString["strCode"] != null && Request.QueryString["strCode"].ToString() != "" && Request.QueryString["New"].ToString() != "S")
                {
                    hdnPGId.Value = Request.QueryString["strCode"].ToString();
                    BindData();                    
                    btnBudget.Disabled = true;
                    btnProd.Disabled = true;
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
        objGP.prpPGId = Convert.ToInt32(hdnPGId.Value);
        DataSet ds = objGP.FetchProductGroupOnId();
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtProdGroup.Text = ds.Tables[0].Rows[0]["GroupCode"].ToString().Trim();
            txtProdFamily.Text = ds.Tables[0].Rows[0]["PFCode"].ToString().Trim();
            txtBudgetClassCode.Text = ds.Tables[0].Rows[0]["BCCode"].ToString().Trim();
            txtDescrip.Text = ds.Tables[0].Rows[0]["GroupDesc"].ToString().Trim();
            chkActive.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Status"].ToString().Trim());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string stat;
        if (hdnPGId.Value.Trim() == "0")
            stat = "S";
        else
            stat = "U";

        try
        {
            if (ValidateProductFamilyCode())
            {
                objGP.prpUserId = Session["UserID"].ToString();
                objGP.prpPGId = Convert.ToInt32(hdnPGId.Value);
                objGP.prpGroupCode = txtProdGroup.Text.Trim();
                objGP.prpPFCode = txtProdFamily.Text.Trim();
                objGP.prpBCCode = txtBudgetClassCode.Text.Trim();
                objGP.prpGroupDesc = txtDescrip.Text.Trim();
                objGP.prpStatus = chkActive.Checked;
                string strResult = objGP.SaveProductGroup();
                if (strResult == "")
                    Response.Redirect("ProductGroupMaster.aspx?stat=" + stat, false);
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
        Response.Redirect("ProductGroupMasterAddEdit.aspx?strCode=" + hdnPGId.Value + "&&New=" + Request.QueryString["New"]);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProductGroupMaster.aspx");
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
     
        string   strSearchQuery = " where Status=1 and PFCode='" + txtProdFamily.Text.Trim() + "' and BCCode='" + txtBudgetClassCode.Text.Trim() + "'";
      
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

    protected void txtBudgetClassCode_TextChanged(object sender, EventArgs e)
    {
        ValidateBudgetClassCode();
    }
    protected void txtProdFamily_TextChanged(object sender, EventArgs e)
    {
        ValidateProductFamilyCode();

    }
}

