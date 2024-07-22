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

public partial class ItemMovement : Page, IRequiresSessionState
{
    DealerMasterClass objDlr = new DealerMasterClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null) Response.Redirect("../Logout.aspx");
        try
        {
            if (!IsPostBack)
            {
                CommonFunction.PopulateRecordsWithTwoParam("Common_Values", "Text", "Value", "FieldName", "POType", "Status", "1", "Id", ddlSalesType);
                if (Session["UserName"].ToString().ToLower() != "admin")
                {
                    if (Session["UserType"].ToString() == "Dealer")
                    {
                        btnDealer.Disabled = true;
                        txtDealerCode.Enabled = false;
                        txtDealerCode.Text = Session["DealerCode"].ToString();
                    }
                    else
                    {
                        btnDealer.Disabled = false;
                        //txtDealerName.Value = Session["DealerName"].ToString();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            //lblMessage.Text = ex.Message;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (BindItem(txtItemCode))
        {
            if (txtDealerCode.Text != "")
            {
                Session["DealerCode"] = txtDealerCode.Text;


                if (txtItemCode.Text != "")
                {
                    Session["ItemCode"] = txtItemCode.Text.Trim();



                    if (txtInvFromDate.Text != "" && txtInvToDate.Text != "")
                    {
                        Session["InvFromDate"] = txtInvFromDate.Text;
                        Session["InvToDate"] = txtInvToDate.Text;

                    }
                    else
                    {
                        Session["InvFromDate"] = "0";
                        Session["InvToDate"] = "0";
                    }
                    if (ddlSalesType.SelectedIndex != 0)
                    {
                        Session["SalesType"] = ddlSalesType.Text.Trim();

                    }
                    else
                    {
                        Session["SalesType"] = "0";
                    }

                    Response.Redirect("ExcelItemMovement.aspx");
                }
                else
                {
                    lblMessage.Focus();
                    lblMessage.Text = "Invalid Item Code.Item Code Cannot Be Blank.";
                    lblMessage.CssClass = "ErrorMessage"; 
                }
            }
            else
            {
                lblMessage.Focus();
                lblMessage.Text = "Invalid Dealer Code.Dealer Code Cannot Be Blank.";
                lblMessage.CssClass = "ErrorMessage";
            }
        }
    }


    protected bool BindItem(TextBox ItemCode)
    {
        string strSearchQuery = "";
        if (Request.QueryString["PONo"] != null)
        {
            if (Request.QueryString["strType"].ToString() == "Commercial")
                strSearchQuery = "inner join PurchaseOrderDetails POD on DP.Item_Id=POD.ItemId where POD.pono ='" + Request.QueryString["PONo"] + "'";
            else
                strSearchQuery = "inner join PurchaseOrderDetails POD on CSS.ItemId=POD.ItemId where POD.pono ='" + Request.QueryString["PONo"] + "'";
        }

        if (strSearchQuery == "")
            strSearchQuery = " where IM.Status=1 and Itemcode='" + ItemCode.Text.Trim() + "'";
        else
            strSearchQuery += " and IM.Status=1 and Itemcode='" + ItemCode.Text.Trim() + "'";

        String SalesType = "";
        if (ddlSalesType.SelectedIndex != 0)
            SalesType = ddlSalesType.Text.Trim();
        else
            SalesType = "Commercial";


        DataSet dsCust = objDlr.FetchDealerItems(strSearchQuery, SalesType);

        //TextBox Desc1 = Page.FindControl("txtDesc1_" + ItemCode.ID.Substring(ItemCode.ID.IndexOf('_') + 1, ItemCode.ID.Length - (ItemCode.ID.IndexOf('_') + 1))) as TextBox;
        //TextBox Desc2 = Page.FindControl("txtDesc2_" + ItemCode.ID.Substring(ItemCode.ID.IndexOf('_') + 1, ItemCode.ID.Length - (ItemCode.ID.IndexOf('_') + 1))) as TextBox;
        //HiddenField hdnItemId = Page.FindControl("hdnItemId_" + ItemCode.ID.Substring(ItemCode.ID.IndexOf('_') + 1, ItemCode.ID.Length - (ItemCode.ID.IndexOf('_') + 1))) as HiddenField;
        //HtmlInputText UnitPrice = Page.FindControl("txtUnitPrice_" + ItemCode.ID.Substring(ItemCode.ID.IndexOf('_') + 1, ItemCode.ID.Length - (ItemCode.ID.IndexOf('_') + 1))) as HtmlInputText;
        //TextBox Quantity = Page.FindControl("txtQty_" + ItemCode.ID.Substring(ItemCode.ID.IndexOf('_') + 1, ItemCode.ID.Length - (ItemCode.ID.IndexOf('_') + 1))) as TextBox;
        //HiddenField hdnMaxQty = Page.FindControl("hdnQty_" + ItemCode.ID.Substring(ItemCode.ID.IndexOf('_') + 1, ItemCode.ID.Length - (ItemCode.ID.IndexOf('_') + 1))) as HiddenField;

        ////int Count = Convert.ToInt16(ItemCode.ID.Substring(ItemCode.ID.IndexOf('_') + 1, ItemCode.ID.Length - (ItemCode.ID.IndexOf('_') + 1)));
        if (dsCust.Tables[0].Rows.Count > 0 || txtItemCode.Text.ToString() == "")
        {
            //Desc1.Text = dsCust.Tables[0].Rows[0]["Description1"].ToString();
            //Desc2.Text = dsCust.Tables[0].Rows[0]["Description2"].ToString();
            //UnitPrice.Value = dsCust.Tables[0].Rows[0]["DlrPrice"].ToString();
            //hdnItemId.Value = dsCust.Tables[0].Rows[0]["Itemid"].ToString();
            //hdnMaxQty.Value = dsCust.Tables[0].Rows[0]["maxqty"].ToString();
            //Quantity.Text = hdnMaxQty.Value;                 
            //Quantity.Focus();
            lblMessage.Text = "";
            lblMessage.CssClass = "";
            return true;

        }
        else
        {
            //Desc1.Text = "";
            //Desc2.Text = "";
            //UnitPrice.Value = "";
            //hdnItemId.Value = "";
            //Quantity.Text = "";
            lblMessage.Text = "ItemCode Not Found.";
            lblMessage.CssClass = "ErrorMessage";
            return false;
        }
    }
    protected void DummyTxt_TextChanged(object sender, EventArgs e)
    {
        BindItem(sender as TextBox);
    }
    protected bool ValidateDealerCode()
    {
        string strSearchQuery = " where Status=1 and DealerCode='" + txtDealerCode.Text.Trim() + "'";
        DataSet dsDealer = objDlr.FetchDealerMaster(strSearchQuery);
        if (dsDealer.Tables[0].Rows.Count > 0)
        {
            //  txtDmsCode.Text = dsDealer.Tables[0].Rows[0]["DMSCode"].ToString();
            lblMessage.Text = "";
            lblMessage.CssClass = "";
            return true;
        }
        else
        {
            lblMessage.Focus();
            lblMessage.Text = "DealerCode Not Found.";
            lblMessage.CssClass = "ErrorMessage";
            return false;
        }
    }
    protected void txtDealerCode_TextChanged(object sender, EventArgs e)
    {
        ValidateDealerCode();
    }
}
