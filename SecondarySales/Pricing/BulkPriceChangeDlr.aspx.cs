/* ==============================================================================================
    Created By      : Subahani SM
    Created Date    : 10 Aug 2010
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

public partial class Pricing_BulkPriceChangeDlr : System.Web.UI.Page
{    
    ProductFamilyClass objPF = new ProductFamilyClass();
    ProductGroupClass objPG = new ProductGroupClass();
    ItemMasterClass objItem = new ItemMasterClass();
    PriceListClass objPrice = new PriceListClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null) Response.Redirect("../Logout.aspx");
        try
        {
            if (!IsPostBack)
            {
                lblMessage.Text = "";
                lblMessage.CssClass = "";
                BindAllData();                
            }        

            
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
            lblMessage.CssClass = "ErrorMessage";
        }
    }
    protected void BindAllData()
    {
        DataSet ds;
        ds = CommonFunction.FetchRecordsWithOneParam("DealerMaster", "Status", "1", "DealerName");
        chkLstDlr.DataSource = ds;
        chkLstDlr.DataTextField = "DealerCode";
        chkLstDlr.DataValueField = "DealerCode";
        chkLstDlr.DataBind();

        foreach (ListItem li in chkLstDlr.Items)
        {
            li.Selected = true;
        }

        ds.Clear();
        ds = CommonFunction.FetchRecordsWithOneParam("BudgetClassMaster", "Status", "1", "BCCode");
        chkLstBudget.DataSource = ds;
        chkLstBudget.DataTextField = "BCCode";
        chkLstBudget.DataValueField = "BCId";
        chkLstBudget.DataBind();

        foreach (ListItem li in chkLstBudget.Items)
        {
            li.Selected = true;
        }

        ds.Clear();
        ds = CommonFunction.FetchRecordsWithOneParam("ProductFamilyMaster", "Status", "1", "PFCode");
        chkLstProdFamily.DataSource = ds;
        chkLstProdFamily.DataTextField = "PFCode";
        chkLstProdFamily.DataValueField = "PFId";
        chkLstProdFamily.DataBind();

        foreach (ListItem li in chkLstProdFamily.Items)
        {
            li.Selected = true;
        }

        ds.Clear();
        ds = CommonFunction.FetchRecordsWithOneParam("ProductGroupMaster", "Status", "1", "GroupCode");
        chkLstProdGroup.DataSource = ds;
        chkLstProdGroup.DataTextField = "GroupCode";
        chkLstProdGroup.DataValueField = "PGId";
        chkLstProdGroup.DataBind();

        foreach (ListItem li in chkLstProdGroup.Items)
        {
            li.Selected = true;
        }

        ds.Clear();
        ds = CommonFunction.FetchRecordsWithOneParam("ItemMaster", "Status", "1", "ItemCode");
        chkLstItem.DataSource = ds;
        chkLstItem.DataTextField = "ItemCode";
        chkLstItem.DataValueField = "Id";
        chkLstItem.DataBind();

        foreach (ListItem li in chkLstItem.Items)
        {
            li.Selected = true;
        }

        hdnDealerCount.Value = chkLstDlr.Items.Count.ToString();
        hdnItemCount.Value = chkLstItem.Items.Count.ToString();

        CommonFunction.PopulateRecordsWithTwoParam("Common_Values", "Text", "Value", "FieldName", "PriceChangeType", "Status", "1", "Id", ddlPriceType);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string strDealerCode = "";
        string strResult = "";
        try
        {
            objPrice.prpUserId = Session["UserID"].ToString();
            foreach (ListItem li in chkLstDlr.Items)
            {
                if (li.Selected == true)
                {
                    if (strDealerCode == "")
                        strDealerCode = li.Value;
                    else
                        strDealerCode = strDealerCode + "," + li.Value;
                }
            }

            foreach (ListItem li in chkLstItem.Items)
            {
                if (li.Selected == true)
                {
                    objPrice.prpItemId = Convert.ToInt32(li.Value.Trim());
                    objPrice.prpPriceChangeType = ddlPriceType.SelectedValue;
                    objPrice.prpChangeValue = Convert.ToDouble(txtValue.Text.Trim());
                    objPrice.prpChangeType = ddlOperator.SelectedValue;
                    strResult=objPrice.SaveBulkPriceChangeDlr(strDealerCode);
                    if (strResult != "")
                    {
                        lblMessage.Text = strResult;
                        lblMessage.CssClass = "ErrorMessage";
                        return;
                    }
                }
            }
            if (strResult == "")
            {
                lblMessage.Text = "Prices Changed Successfully.";
                lblMessage.CssClass = "SuccessMessageBold";
            }
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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("BulkPriceChangeDlr.aspx");
    }
    protected void chkLstBudget_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindProductFamily();
    }
    protected void chkLstProdFamily_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindProductGroup();
    }
    protected void chkLstProdGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindItems();        
    }
    protected void BindProductFamily()
    {
        string strBudgetCode = "";
        DataSet dsPF=new DataSet();

        foreach (ListItem li in chkLstBudget.Items)
        {
            if (li.Selected == true)
            {
                if (strBudgetCode == "")
                    strBudgetCode = "'" + li.Text + "'";
                else
                    strBudgetCode = strBudgetCode + ",'" + li.Text + "'";
            }

        }

        if (strBudgetCode != "")  
        {   
            dsPF = objPF.FetchProductFamily(" where BCCode in(" + strBudgetCode + ")");       
        
            chkLstProdFamily.DataSource = dsPF;
            chkLstProdFamily.DataTextField = "PFCode";
            chkLstProdFamily.DataValueField = "PFId";
            chkLstProdFamily.DataBind();

            foreach (ListItem li in chkLstProdFamily.Items)
            {
                li.Selected = true;
            }
        }
        else
            chkLstProdFamily.Items.Clear();

        BindProductGroup();

    }

    protected void BindProductGroup()
    {
        string strPFCode = "";
        DataSet dsPG = new DataSet();

        foreach (ListItem li in chkLstProdFamily.Items)
        {
            if (li.Selected == true)
            {
                if (strPFCode == "")
                    strPFCode = "'" + li.Text + "'";
                else
                    strPFCode = strPFCode + ",'" + li.Text + "'";
            }

        }

        if (strPFCode != "")
        {
            dsPG = objPG.FetchProductGroup(" where PFCode in(" + strPFCode + ")");

            chkLstProdGroup.DataSource = dsPG;
            chkLstProdGroup.DataTextField = "GroupCode";
            chkLstProdGroup.DataValueField = "PGId";
            chkLstProdGroup.DataBind();

            foreach (ListItem li in chkLstProdGroup.Items)
            {
                li.Selected = true;
            }
        }
        else
            chkLstProdGroup.Items.Clear();

        BindItems();

    }

    protected void BindItems()
    {
        string strGroupCode = "";
        DataSet dsItem = new DataSet();
        int icount = 0;

        foreach (ListItem li in chkLstProdGroup.Items)
        {
            if (li.Selected == true)
            {
                if (strGroupCode == "")
                    strGroupCode = "'" + li.Text + "'";
                else
                    strGroupCode = strGroupCode + ",'" + li.Text + "'";
            }

        }

        if (strGroupCode != "")
        {
            dsItem = objItem.FetchItemMaster(" where GroupCode in(" + strGroupCode + ")");

            chkLstItem.DataSource = dsItem;
            chkLstItem.DataTextField = "ItemCode";
            chkLstItem.DataValueField = "Id";
            chkLstItem.DataBind();

            foreach (ListItem li in chkLstItem.Items)
            {
                li.Selected = true;
                icount += 1;
            }
        }
        else
            chkLstItem.Items.Clear();

        hdnItemCount.Value = icount.ToString();

    }

    protected void chkLstDlr_SelectedIndexChanged(object sender, EventArgs e)
    {
        int intCount = 0;
        foreach (ListItem li in chkLstDlr.Items)
        {
            if (li.Selected == true)
                intCount += 1;
        }
        hdnDealerCount.Value = intCount.ToString();
    }
    protected void chkLstItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        int intCount = 0;
        foreach (ListItem li in chkLstItem.Items)
        {
            if (li.Selected == true)
                intCount += 1;
        }
        hdnItemCount.Value = intCount.ToString();
    }
}
