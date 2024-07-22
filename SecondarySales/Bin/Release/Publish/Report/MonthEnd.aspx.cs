using Agfa.Common;
using BusinessClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_MonthEnd : System.Web.UI.Page
{
    private DealerMasterClass objDlr = new DealerMasterClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Session["UserID"] == null)
            this.Response.Redirect("../Logout.aspx");
        try
        {
            if (this.IsPostBack)
                return;
            CommonFunction.PopulateRecords("ProductGroupMaster", "GroupCode", "GroupCode", "pgId", this.ddlProductGroup);
            if (!(this.Session["UserName"].ToString().ToLower() != "admin"))
                return;
            if (this.Session["UserType"].ToString().ToLower() == "dealer")
            {
                this.btnDealer.Disabled = true;
                this.txtDealerCode.Enabled = false;
                this.txtDealerCode.Text = this.Session["DealerCode"].ToString();
                this.txtDealerName.Value = this.Session["DealerName"].ToString();
            }
            else
                this.btnDealer.Disabled = false;
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        ClsReports clsReports = new ClsReports();
        DataSet dataSet = new DataSet();
        clsReports.prpDealerCode = "0";
        List<ReportCriterion> reportCriterionList = new List<ReportCriterion>();
        ReportCriterion reportCriterion = new ReportCriterion();
        if (this.txtDealerCode.Text != "" && this.txtDealerName.Value != "")
        {
            this.Session["DealerName"] = (object)this.txtDealerName.Value.Trim();
            this.Session["DealerCode"] = (object)this.txtDealerCode.Text.Trim();
            clsReports.prpDealerCode = this.txtDealerCode.Text.Trim();
            reportCriterion.Name = "DealerCode";
            reportCriterion.Values = new string[1]
            {
        this.txtDealerCode.Text
            };
            reportCriterionList.Add(reportCriterion);
        }
        else
        {
            clsReports.prpDealerCode = this.txtDealerCode.Text.Trim();
            reportCriterion.Name = "DealerCode";
            reportCriterion.Values = new string[1] { "ALL" };
            reportCriterionList.Add(reportCriterion);
            this.Session["DealerName"] = (object)"0";
            this.Session["DealerCode"] = (object)"0";
        }
        reportCriterionList.Add(new ReportCriterion()
        {
            Name = "Run By",
            Values = new string[1]
          {
        this.Session["UserName"].ToString()
          }
        });
        reportCriterionList.Add(new ReportCriterion()
        {
            Name = "Run Date",
            Values = new string[1]
          {
        DateTime.Now.ToString("MM/dd/yyyy")
          }
        });
        clsReports.prpDealerCode = this.Session["DealerCode"].ToString();
        SqlDataReader monthEnd = clsReports.GetMonthEnd();
        ReportHeader reportHeader = new ReportHeader();
        reportHeader.HeaderText = "Month End";
        reportHeader.Alignment = Alignments.Left;
        ReportFooter reportFooter = new ReportFooter();
        reportFooter.FooterText = "Month End";
        reportFooter.Alignment = Alignments.Right;
        ExcelReportConfiguration ReportConfiguration = new ExcelReportConfiguration();
        ReportConfiguration.BorderWidth = 1;
        ReportConfiguration.ReportTitle = "Month End";
        ReportConfiguration.Footer = reportFooter;
        ReportConfiguration.Header = reportHeader;
        ReportConfiguration.ReportCriteria = reportCriterionList.ToArray();
        string HTTPHandlerAbsolutePath = this.ResolveUrl("~/Handlers/SendFileToBrowser.ashx");
        monthEnd.CreateExcelReport(ReportConfiguration).SendFileToBrowser(HTTPHandlerAbsolutePath);
    }
    protected bool ValidateDealerCode()
    {
        DataSet dataSet = this.objDlr.FetchDealerMaster(" where Status=1 and DealerCode='" + this.txtDealerCode.Text.Trim() + "'");
        if (dataSet.Tables[0].Rows.Count > 0)
        {
            this.txtDealerName.Value = dataSet.Tables[0].Rows[0][4].ToString();
            this.lblMessage.Text = "";
            this.lblMessage.CssClass = "";
            return true;
        }
        this.lblMessage.Focus();
        this.lblMessage.Text = "DealerCode Not Found.";
        this.lblMessage.CssClass = "ErrorMessage";
        return false;
    }
    protected void txtDealerCode_TextChanged(object sender, EventArgs e)
    {
        ValidateDealerCode();
    }
}