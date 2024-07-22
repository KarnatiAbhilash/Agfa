/* ==============================================================================================
    Created By      : Subahani SM
    Created Date    : 22 July 2010
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

public partial class Masters_ProductFamilyMasterAddEdit : System.Web.UI.Page
{
    CommonFunction objComm = new CommonFunction();
    ProductFamilyClass objPF = new ProductFamilyClass();
    BudgetClassMaster objBudget = new BudgetClassMaster();

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
                        lblHeader.Text = "Add - Product Family Master";
                        txtBudgetClassCode.ReadOnly = false;
                    }
                    else
                    {
                        lblHeader.Text = "Edit - Product Family Master";
                        txtBudgetClassCode.ReadOnly = true;
                    }


                }
                if (Request.QueryString["strCode"] != null && Request.QueryString["strCode"].ToString() != "" && Request.QueryString["New"].ToString() != "S")
                {
                    hdnPFId.Value = Request.QueryString["strCode"].ToString();
                    BindData();
                    txtProdFamily.ReadOnly = true;
                    btnBudget.Disabled = true;
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
        objPF.prpPFId = Convert.ToInt32(hdnPFId.Value);
        DataSet ds = objPF.FetchProductFamilyOnId();
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtProdFamily.Text = ds.Tables[0].Rows[0]["PFCode"].ToString().Trim();
            txtBudgetClassCode.Text = ds.Tables[0].Rows[0]["BCCode"].ToString().Trim();
            txtDescrip.Text = ds.Tables[0].Rows[0]["PFDesc"].ToString().Trim();
            chkActive.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Status"].ToString().Trim());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string stat;
        if (hdnPFId.Value.Trim() == "0")
            stat = "S";
        else
            stat = "U";

        try
        {
            if (ValidateBudgetClassCode())
            {
                objPF.prpUserId = Session["UserID"].ToString();
                objPF.prpPFId = Convert.ToInt32(hdnPFId.Value);
                objPF.prpPFCode = txtProdFamily.Text.Trim();
                objPF.prpBCCode = txtBudgetClassCode.Text.Trim();
                objPF.prpPFDesc = txtDescrip.Text.Trim();
                objPF.prpStatus = chkActive.Checked;
                string strResult = objPF.SaveProductFamily();
                if (strResult == "")
                    Response.Redirect("ProductFamilyMaster.aspx?stat=" + stat, false);
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
        Response.Redirect("ProductFamilyMasterAddEdit.aspx?strCode=" + hdnPFId.Value + "&&New=" + Request.QueryString["New"]);
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProductFamilyMaster.aspx");
    }

    protected void txtBudgetClassCode_TextChanged(object sender, EventArgs e)
    {
        ValidateBudgetClassCode();
    }

    private bool ValidateBudgetClassCode()
    {
            string  strSearchQuery = " where Status=1 and BCCode='" + txtBudgetClassCode.Text.Trim() + "'";
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

}
