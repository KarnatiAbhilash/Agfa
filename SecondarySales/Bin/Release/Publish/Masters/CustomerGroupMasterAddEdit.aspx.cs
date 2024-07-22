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

public partial class Masters_CustomerGroupMasterAddEdit : System.Web.UI.Page
{

    CommonFunction objComm = new CommonFunction();
    CustomerGroupMaster objBC = new CustomerGroupMaster();
  
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
                        lblHeader.Text = "Add - Customer Group Master";
                    else
                        lblHeader.Text = "Edit - Customer  Group Master";


                }
                if (Request.QueryString["strCode"] != null && Request.QueryString["strCode"].ToString() != "" && Request.QueryString["New"].ToString() != "S")
                {
                    hdnCustId.Value = Request.QueryString["strCode"].ToString();
                    BindData();
                    txtCustGroup.ReadOnly = true;
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    //this is for bindData
    protected void BindData()
    {
        objBC.prpCustId = Convert.ToInt32(hdnCustId.Value);
        DataSet ds = objBC.FetchCustomerGroupMasterClassOnId();
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtCustGroup.Text = ds.Tables[0].Rows[0]["CustomerGroup"].ToString().Trim();
            txtCustDescrip.Text = ds.Tables[0].Rows[0]["CustomerGroupDesc"].ToString().Trim();
            chkActive.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Status"].ToString().Trim());
        }
    }

    //this is for save customergroupmaster
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string stat;
        if (hdnCustId.Value.Trim() == "0")
            stat = "S";
        else
            stat = "U";

        try
        {
            objBC.prpUserId = Session["UserID"].ToString();
            objBC.prpCustId = Convert.ToInt32(hdnCustId.Value);
            objBC.prpCusGroup = txtCustGroup.Text.Trim();
            objBC.prpCustGroupDesc = txtCustDescrip.Text.Trim();
            objBC.prpStatus = chkActive.Checked;
            string strResult = objBC.SaveCustomerGroupMassterClass();
            if (strResult == "")
                Response.Redirect("CustomerGroupMaster.aspx?stat=" + stat, false);
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
        string strSearchQuery = " where Status=1 and CustomerGroup='" + txtCustGroup.Text.Trim() + "' ";
        DataSet dsGroup = objBC.FetchCustomerGroup(strSearchQuery);

        if (dsGroup.Tables[0].Rows.Count > 0)
        {
            lblMessage.Text = "";
            lblMessage.CssClass = "";
            return true;
        }
        else
        {
            lblMessage.Text = "CustomerGroupCode Not Found.";
            lblMessage.CssClass = "ErrorMessage";
            return false;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("CustomerGroupMasterAddEdit.aspx?strCode=" + hdnCustId.Value + "&&New=" + Request.QueryString["New"]);
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("CustomerGroupMaster.aspx");
    }

    protected void txtProdGroup_TextChanged(object sender, EventArgs e)
    {
        ValidateProductGroupCode();
    }

}



