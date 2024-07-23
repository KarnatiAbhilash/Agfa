/* ==============================================================================================
    Created By      : Subahani SM
    Created Date    : 30 July 2010
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

public partial class Masters_CustomerMasterAddEdit : System.Web.UI.Page
{
    CustomerMasterClass objCust = new CustomerMasterClass();
    DealerMasterClass objDealer = new DealerMasterClass();
    MonthEndClass objMonth = new MonthEndClass();
    StockReturnClass objStock = new StockReturnClass();
    CommonFunction objComm = new CommonFunction();
    //CustomerMasterClass objCust = new CustomerMasterClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null) Response.Redirect("../Logout.aspx");
        try
        {
            if (!IsPostBack)
            {
                CommonFunction.PopulateRecordsWithTwoParam("Common_Values", "Text", "Value", "FieldName", "Region", "Status", "1", "Id", ddlRegion);
                CommonFunction.PopulateRecordsWithOneParam("StateMaster", "StateName", "StateCode", "Active", "1", "StateName", ddlState);
                CommonFunction.PopulateRecordsWithTwoParam("Common_Values", "Text", "Value", "FieldName", "CustType", "Status", "1", "Id", ddlCustType);
                btnSave.Attributes.Add("onclick", "return fnValidateSave()");
                if (Request.QueryString["New"] != null)
                {
                    if (Request.QueryString["New"].ToString() == "S")
                        lblHeader.Text = "Add - Customer Master";
                    else
                        lblHeader.Text = "Edit - Customer Master";
                }
                if (Request.QueryString["strCode"] != null && Request.QueryString["strCode"].ToString() != "" && Request.QueryString["New"].ToString() != "S")
                {
                    hdnCustCode.Value = Request.QueryString["strCode"].ToString();
                    BindData();
                    btnAddNew.Style.Add("display", "table-row");

                    if (chkIsSpecialCust.Checked == true)
                        btnSpecial.Style.Add("display", "table-row");
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
        objCust.prpCustCode = Convert.ToInt32(hdnCustCode.Value);
        DataSet ds = objCust.FetchCustomerMasterOnId();
        if (ds.Tables[0].Rows.Count > 0)
        {
            btnAddNew.Visible = true;
            InstBase.Visible = true;
            txtDealer.Text = ds.Tables[0].Rows[0]["DealerCode"].ToString().Trim();
            txtCustCode.Text = ds.Tables[0].Rows[0]["CustCode"].ToString().Trim();
            txtCustName.Text = ds.Tables[0].Rows[0]["CustName"].ToString().Trim();
            txtAddress1.Text = ds.Tables[0].Rows[0]["Address1"].ToString().Trim();
            txtAddress2.Text = ds.Tables[0].Rows[0]["Address2"].ToString().Trim();
            txtAddress3.Text = ds.Tables[0].Rows[0]["Address3"].ToString().Trim();
            txtCity.Text = ds.Tables[0].Rows[0]["City"].ToString().Trim();
            ddlState.SelectedValue = ds.Tables[0].Rows[0]["State"].ToString().Trim();
            txtPincode.Text = ds.Tables[0].Rows[0]["Pincode"].ToString().Trim();
            txtContPerson.Text = ds.Tables[0].Rows[0]["ContactPerson"].ToString().Trim();
            txtContNo.Text = ds.Tables[0].Rows[0]["ContactNo"].ToString().Trim();
            txtEmailId.Text = ds.Tables[0].Rows[0]["EmailId"].ToString().Trim();
            //txtCSTNo.Text = ds.Tables[0].Rows[0]["CSTNo"].ToString().Trim();
            // txtLSTNo.Text = ds.Tables[0].Rows[0]["LSTNo"].ToString().Trim();
            txtGSTNo.Text = ds.Tables[0].Rows[0]["GSTNo"].ToString().Trim();
            chkIsSpecialCust.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsSpecialCust"].ToString().Trim());
            txtCustGroup.Text = ds.Tables[0].Rows[0]["CustGroup"].ToString().Trim();
            ddlRegion.SelectedValue = ds.Tables[0].Rows[0]["Region"].ToString().Trim();
            ddlCustType.SelectedValue = ds.Tables[0].Rows[0]["CustType"].ToString().Trim();
            chkActive.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["Status"].ToString().Trim());
            drtCust.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["DirectCustomer"].ToString().Trim());
            txtSalesEmp.Text = ds.Tables[0].Rows[0]["SalesEmpName"].ToString().Trim();
            txtDmsCode.Text = ds.Tables[0].Rows[0]["DMSCode"].ToString().Trim();
            chkIsSpecialMOU.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["MOU"].ToString().Trim());
            if (chkIsSpecialMOU.Checked != false)
            {
                trReg.Disabled = true;
                trReg.Style.Add("display", "table-row");
                txtDemoStartDate.Text = ds.Tables[0].Rows[0]["DemoStartDate"].ToString().Trim();
                // txtDemoStartDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["DemoStartDate"].ToString()).ToString(Session["DateFormat"].ToString());
                txtCmtSqm.Text = ds.Tables[0].Rows[0]["CommitmentinSqmPM"].ToString().Trim();
            }
            //txtDemoStartDate.Text = ds.Tables[0].Rows[0]["DemoStartDate"].ToString().Trim();
            //txtCmtSqm.Text = ds.Tables[0].Rows[0]["CommitmentinSqmPM"].ToString().Trim();
        }

    }
    
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string stat;
        if (hdnCustCode.Value.Trim() == "0")
            stat = "S";
        else
            stat = "U";

        try
        {
            if (ValidateDealerCode())
            {
                objCust.prpUserId = Session["UserID"].ToString();
                objCust.prpCustCode = Convert.ToInt32(hdnCustCode.Value);
                objCust.prpDealerCode = Convert.ToInt32(txtDealer.Text.Trim());
                objCust.prpCustName = txtCustName.Text.Trim();
                objCust.prpAddress1 = txtAddress1.Text.Trim();
                objCust.prpAddress2 = txtAddress2.Text.Trim();
                objCust.prpAddress3 = txtAddress3.Text.Trim();
                objCust.prpCity = txtCity.Text.Trim();
                objCust.prpState = ddlState.SelectedValue;
                objCust.prpPincode = txtPincode.Text.Trim();
                objCust.prpContPerson = txtContPerson.Text.Trim();
                objCust.prpContNo = txtContNo.Text.Trim();
                objCust.prpEmailId = txtEmailId.Text.Trim();
                //objCust.prpCSTNo = txtCSTNo.Text.Trim();
                //objCust.prpLSTNo = txtLSTNo.Text.Trim();
                objCust.prpGSTNo = txtGSTNo.Text.Trim();
                objCust.prpIsSpecCust = chkIsSpecialCust.Checked;
                objCust.prpCustGroup = txtCustGroup.Text;
                objCust.prpRegion = ddlRegion.SelectedValue;
                objCust.prpCustType = ddlCustType.SelectedValue;
                objCust.prpStatus = chkActive.Checked;
                objCust.prpDirectCust = drtCust.Checked;
                objCust.prpSalesEmpName = txtSalesEmp.Text.Trim();
                objCust.prpMou = chkIsSpecialMOU.Checked;
                objCust.prpDMSCode = txtDmsCode.Text.Trim();
                if (chkIsSpecialMOU.Checked)
                {
                    objCust.prpDemoStartDate = Convert.ToDateTime(txtDemoStartDate.Text);
                    objCust.prpComtSqm = Convert.ToDecimal(txtCmtSqm.Text);
                }
                
                string strResult = objCust.SaveCustMaster();
                if (strResult == "")
                    Response.Redirect("CustomerMaster.aspx?stat=" + stat, false);
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
        Response.Redirect("CustomerMasterAddEdit.aspx?strCode=" + hdnCustCode.Value + "&&New=" + Request.QueryString["New"]);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("CustomerMaster.aspx");
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("CustomerItemPrice.aspx?DlrCode=" + txtDealer.Text + "&CustCode=" + txtCustCode.Text.Trim() + "&CustName=" + txtCustName.Text.Trim());
    }
    protected void btnSpecial_Click(object sender, EventArgs e)
    {
        if (txtDealer.Text != "" && txtCustCode.Text != "")
            Response.Redirect("CustSpecialScheme.aspx?DlrCode=" + txtDealer.Text + "&CustCode=" + txtCustCode.Text.Trim() + "&CustName=" + txtCustName.Text.Trim());
        else
        {
            lblMessage.Text = "Dealer Code And Customer Code Not Found.";
            lblMessage.CssClass = "ErrorMessage";
            if (chkIsSpecialCust.Checked == true)
                btnSpecial.Style.Add("display", "table-row");
        }
    }

  

    protected bool ValidateDealerCode()
    {
        string strSearchQuery = " where Status=1 and DealerCode='" + txtDealer.Text.Trim() + "'";
        DataSet dsDealer = objDealer.FetchDealerMaster(strSearchQuery);
        if (dsDealer.Tables[0].Rows.Count > 0)
        {
            //  txtDmsCode.Text = dsDealer.Tables[0].Rows[0]["DMSCode"].ToString();
            lblMessage.Text = "";
            lblMessage.CssClass = "";
            return true;
        }
        else
        {
            txtDealer.Focus();
            txtDmsCode.Text = "";
            lblMessage.Text = "DealerCode Not Found.";
            lblMessage.CssClass = "ErrorMessage";
            return false;
        }
    }

    protected void txtDealer_TextChanged(object sender, EventArgs e)
    {
        ValidateDealerCode();
    }

    protected void btnDemo_Details(object sender, EventArgs e)
    {

    }
    protected void btnInstall_Base(object sender, EventArgs e)
    {
        if (txtDealer.Text != "" || txtCustCode.Text != "")
            Response.Redirect("InstallBase.aspx?DlrCode=" + txtDealer.Text + "&CustCode=" + txtCustCode.Text.Trim() + "&CustName=" + txtCustName.Text.Trim());
        else
        {
            lblMessage.Text = "Dealer Code And Customer Code Not Found.";
            lblMessage.CssClass = "ErrorMessage";
            if (chkIsSpecialCust.Checked == true)
                btnSpecial.Style.Add("display", "table-row");
        }
    }
}