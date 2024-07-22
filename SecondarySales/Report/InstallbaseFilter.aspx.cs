
using Agfa.Common;
using BusinessClass;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Report_InstallbaseFilter : System.Web.UI.Page
{
  
    private DealerMasterClass objDealer = new DealerMasterClass();
    private ClsReports objRep = new ClsReports();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Session["UserID"] == null)
            this.Response.Redirect("../Logout.aspx");
        try
        {
            if (this.IsPostBack)
                return;
            CommonFunction.PopulateRecords("ExecutiveMaster", "ExecutiveCode", "ExecutiveCode", "emId", this.ddlExecutiveCode);
            CommonFunction.PopulateRecordsWithTwoParam("Common_Values", "Text", "Value", "FieldName", "Region", "Status", "1", "Id", this.ddlRegion);
            CommonFunction.PopulateRecords("stateMaster", "StateName", "StateCode", "StateCode", this.ddlState);
            CommonFunction.GetCustomerGroup(this.ddlCustGrp);
            if (!(this.Session["UserName"].ToString().ToLower() != "admin"))
                return;
            if (this.Session["UserType"].ToString().ToLower() == "dealer")
            {
                this.btnDealer.Disabled = true;
                this.txtDealerCode.Enabled = false;
                this.txtDealerCode.Text = this.Session["DealerCode"].ToString();
            }
            else
                this.btnDealer.Disabled = false;
        }
        catch (Exception ex)
        {
        }
    }
        protected void ddlBudgetClassCode_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void ddlProductFamily_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<ReportCriterion> reportCriterionList = new List<ReportCriterion>();
            ReportCriterion reportCriterion = new ReportCriterion();
            this.objRep.prpExecutiveCode = "0";
            this.objRep.prpCustCode = "0";
            this.objRep.prpInvFromDate = "0";
            this.objRep.prpInvToDate = "0";
            this.objRep.prpMonth = "0";
            this.objRep.prpRegion = "0";
            this.objRep.prpState = "0";
            this.objRep.prpYear = "0";
            if (this.txtCustCode.Value != "")
            {
                this.objRep.prpCustCode = this.txtCustCode.Value.Trim();
                reportCriterionList.Add(new ReportCriterion()
                {
                    Name = "Customer Code",
                    Values = new string[1]
                  {
                    this.txtCustCode.Value.Trim()
                  }
                });
            }
            if (this.ddlExecutiveCode.SelectedIndex != 0)
            {
                this.objRep.prpExecutiveCode = this.ddlExecutiveCode.Text.Trim();
                reportCriterionList.Add(new ReportCriterion()
                {
                    Name = "Executive Code",
                    Values = new string[1]
                  {
          this.ddlExecutiveCode.Text.Trim()
                  }
                });
            }
            if (this.ddlRegion.SelectedIndex != 0)
            {
                this.objRep.prpRegion = this.ddlRegion.Text.Trim();
                reportCriterionList.Add(new ReportCriterion()
                {
                    Name = "Region",
                    Values = new string[1] { this.ddlRegion.Text.Trim() }
                });
            }
            if (this.txtDealerCode.Text.ToString() != "")
            {
                this.objRep.prpDealerCode = this.txtDealerCode.Text.Trim();
                reportCriterionList.Add(new ReportCriterion()
                {
                    Name = "Dealer Code",
                    Values = new string[1]
                  {
                     this.txtDealerCode.Text.Trim()
                  }
                });
            }
            if (this.ddlState.SelectedIndex != 0)
            {
                this.objRep.prpState = this.ddlState.Text.Trim();
                reportCriterionList.Add(new ReportCriterion()
                {
                    Name = "State",
                    Values = new string[1] { this.ddlState.Text.Trim() }
                });
            }
            if (this.ddlCustGrp.SelectedIndex != 0)
            {
                this.objRep.prpInvToDate = this.ddlCustGrp.Text.Trim();
                reportCriterionList.Add(new ReportCriterion()
                {
                    Name = "Customer Group",
                    Values = new string[1]
                  {
                    this.ddlCustGrp.Text.Trim()
                  }
                });
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
            SqlDataReader reader = this.objRep.funInstallBaseFilter();
            ReportHeader reportHeader = new ReportHeader();
            reportHeader.HeaderText = "Install Base Filter";
            reportHeader.Alignment = Alignments.Left;
            ReportFooter reportFooter = new ReportFooter();
            reportFooter.FooterText = "Install Base Filter";
            reportFooter.Alignment = Alignments.Right;
            ExcelReportConfiguration ReportConfiguration = new ExcelReportConfiguration();
            ReportConfiguration.BorderWidth = 1;
            ReportConfiguration.ReportTitle = "Install Base Filter";
            ReportConfiguration.Footer = reportFooter;
            ReportConfiguration.Header = reportHeader;
            ReportConfiguration.ReportCriteria = reportCriterionList.ToArray();
            string HTTPHandlerAbsolutePath = this.ResolveUrl("~/Handlers/SendFileToBrowser.ashx");
            reader.CreateExcelReport(ReportConfiguration).SendFileToBrowser(HTTPHandlerAbsolutePath);
        }
    

        protected bool ValidateDealerCode()
        {
            if (this.objDealer.FetchDealerMaster(" where Status=1 and DealerCode='" + this.txtDealerCode.Text.Trim() + "'").Tables[0].Rows.Count > 0)
            {
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
            this.ValidateDealerCode();
        }
           
    
}