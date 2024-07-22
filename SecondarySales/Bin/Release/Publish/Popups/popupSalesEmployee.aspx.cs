using BusinessClass;
using System;
using System.Data;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Popups_popupSalesEmployee : System.Web.UI.Page
{
    //protected HiddenField hidSelectType;
    //protected DropDownList ddlsearch;
    //protected TextBox txtValue;
    //protected Button btnSearch;
    //protected Button btnClear;
    //protected GridView gvBudget;
    //protected HtmlTable Table1;
    //protected Button btnSave;
    //protected Label lblMessage;
    //protected HtmlForm frmpopupBudgetClass;
    private CustomerGroupMaster objBudget = new CustomerGroupMaster();
    private SalesEmployee objBudget1 = new SalesEmployee();
    private DataSet dsBudget;
    private static string SelectType;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Session["UserID"] == null)
            this.Response.Redirect("../Logout.aspx");
        try
        {
            if (this.IsPostBack)
                return;
            this.bindBC(true, false);
            Popups_popupSalesEmployee.SelectType = Convert.ToString(this.Request.QueryString["SelectType"]);
            this.hidSelectType.Value = Popups_popupSalesEmployee.SelectType;
        }
        catch (Exception ex)
        {
        }
    }

    protected void bindBC(bool isrebind, bool issearch)
    {
        string strCondition;
        if (issearch)
            strCondition = " where Status=0 and    " + this.ddlsearch.SelectedValue + " like '%" + this.txtValue.Text.Trim() + "%' and  Region='" + this.Request.QueryString["SalesCls"].ToString() + "'";
        else
            strCondition = " where Status=0 and Region='" + this.Request.QueryString["SalesCls"].ToString() + "'";
        this.dsBudget = this.objBudget1.FetchSalesEmployeeClass(strCondition);
        this.Session["dsBudget"] = (object)this.dsBudget;
        if (!isrebind)
            return;
        this.gvBudget.DataSource = (object)this.dsBudget;
        this.gvBudget.DataBind();
        if (this.dsBudget.Tables[0].Rows.Count > 0)
        {
            this.lblMessage.Text = "";
            this.lblMessage.CssClass = "";
            this.btnSave.Visible = true;
        }
        else
        {
            this.lblMessage.Text = "No Record(s) Found.";
            this.lblMessage.CssClass = "ErrorMessage";
            this.btnSave.Visible = false;
        }
    }

    protected void gvBudget_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gvBudget.PageIndex = e.NewPageIndex;
        this.gvBudget.DataSource = this.Session["dsBudget"];
        this.gvBudget.DataBind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int num = 0;
        try
        {
            for (int index = 0; index < this.gvBudget.Rows.Count; ++index)
            {
                if ((this.gvBudget.Rows[index].FindControl("rbtnSelect") as RadioButton).Checked)
                {
                    ++num;
                    this.gvBudget.Rows[index].FindControl("lblGroupCode");
                    Label control = this.gvBudget.Rows[index].FindControl("lblName") as Label;
                    this.ClientScript.RegisterClientScriptBlock(this.GetType(), "CallEmp", "<script type=\"text/javascript\" language=\"javascript\">FillParentWindow(\"" + this.hidSelectType.Value + "\",\"" + control.Text + "\")</script>");
                }
            }
            if (num < 1)
            {
                this.lblMessage.Text = "Please Select Atleast One Record.";
                this.lblMessage.CssClass = "ErrorMessage";
            }
            else
                this.lblMessage.CssClass = "";
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        this.txtValue.Text = "";
        this.bindBC(true, false);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            this.bindBC(true, true);
        }
        catch (Exception ex)
        {
        }
    }
}