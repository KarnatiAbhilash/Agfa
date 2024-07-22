using BusinessClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Masters_EquipmentProductGroupMasterAddEdit : System.Web.UI.Page
{
    private EquipmentProductGroupMasterClass objEquipment = new EquipmentProductGroupMasterClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Session["UserId"] == null)
            this.Response.Redirect("../Logout.aspx");
        try
        {
            if (this.IsPostBack)
                return;
            if (this.Request.QueryString["New"] != null)
                this.lblHeader.Text = !(this.Request.QueryString["New"].ToString() == "S") ? "Edit - Equipment Master" : "Add - Equipment Master";
            if (this.Request.QueryString["strCode"] == null || !(this.Request.QueryString["strCode"].ToString() != "") || !(this.Request.QueryString["New"].ToString() != "S"))
                return;
            this.hdnId.Value = this.Request.QueryString["strCode"].ToString();
            this.BindData();
            this.txtEquipmentCode.ReadOnly = true;
        }
        catch (Exception ex)
        {
            this.lblMessage.Text = ex.Message;
        }
    }

    private void BindData()
    {
        this.objEquipment.PrpId = Convert.ToInt32(this.hdnId.Value);
        DataSet dataSet = this.objEquipment.FetchEquipmentProductGroupMasterOnId();
        if (dataSet.Tables[0].Rows.Count <= 0)
            return;
        this.txtEquipmentCode.Text = dataSet.Tables[0].Rows[0]["ProductGroup"].ToString().Trim();
        this.txtDescrip.Text = dataSet.Tables[0].Rows[0]["ProductDesc"].ToString().Trim();
        this.chkActive.Checked = Convert.ToBoolean(dataSet.Tables[0].Rows[0]["Status"].ToString().Trim());
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("EquipmentProductGroupMasterAddEdit.aspx?strCode=" + this.hdnId.Value + "&&New=" + this.Request.QueryString["New"]);
    }

    protected void btnBack_Click(object sender, EventArgs e) {
        Response.Redirect("EquipmentProductGroupMaster.aspx");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string str1 = !(this.hdnId.Value.Trim() == "0") ? "U" : "S";
        try
        {
            this.objEquipment.PrpId = Convert.ToInt32(this.hdnId.Value);
            this.objEquipment.prpProductGroup = this.txtEquipmentCode.Text.Trim();
            this.objEquipment.prpProductDesc = this.txtDescrip.Text.Trim();
            this.objEquipment.prpStatus = chkActive.Checked;
            string str2 = this.objEquipment.SaveEquipmentMaster();
            if (str2 == "")
            {
                this.Response.Redirect("EquipmentMaster.aspx?stat=" + str1, false);
            }
            else
            {
                this.lblMessage.Text = str2;
                this.lblMessage.CssClass = "ErrorMessage";
            }
        }
        catch (Exception ex)
        {
            this.lblMessage.Text = ex.Message;
            this.lblMessage.CssClass = "ErrorMessage";
        }
    }
}