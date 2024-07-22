using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Agfa.Common;
using BusinessClass;

public partial class Report_DealerReceipt : System.Web.UI.Page
{
    DealerMasterClass objDealer = new DealerMasterClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null) Response.Redirect("../Logout.aspx");
        try
        {
            if (!IsPostBack)
            {
                CommonFunction.PopulateRecords("ProductGroupMaster", "Groupcode", "Groupcode", "pgId", ddlProductGroup);
                CommonFunction.PopulateRecordsWithTwoParam("Common_Values", "Text", "Value", "FieldName", "POType", "Status", "1", "Id", ddlPOType);
                if (Session["UserName"].ToString().ToLower() != "admin")
                {
                    if (Session["UserType"].ToString().ToLower() == "dealer")
                    {
                        btnDealer.Disabled = true;
                        txtDealerCode.Enabled = false;
                        txtDealerCode.Text = Session["DealerCode"].ToString();
                    }
                    else
                    {
                        btnDealer.Disabled = false;
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
        ClsReports clsReports = new ClsReports();
        DataSet dataSet = new DataSet();
        List<ReportCriterion> reportCriterionList = new List<ReportCriterion>();
        ReportCriterion reportCriterion = new ReportCriterion();
        if (this.txtDealerCode.Text != "")
        {
            reportCriterion.Name = "DealerCode";
            reportCriterion.Values = new string[1]
            {
        this.txtDealerCode.Text
            };
            reportCriterionList.Add(reportCriterion);
            this.Session["DealerCode"] = (object)this.txtDealerCode.Text;
            if (this.ddlProductGroup.SelectedIndex != 0)
            {
                reportCriterionList.Add(new ReportCriterion()
                {
                    Name = "ProductGroup",
                    Values = new string[1]
                  {
            this.ddlProductGroup.Text
                  }
                });
                this.Session["ProductGroup"] = (object)this.ddlProductGroup.Text;
            }
            else
                this.Session["ProductGroup"] = (object)"0";
            if (this.txtInvFromDate.Text.Trim() != "" && this.txtInvToDate.Text.Trim() != "")
            {
                reportCriterionList.Add(new ReportCriterion()
                {
                    Name = "InvFromDate",
                    Values = new string[1] { this.txtInvFromDate.Text }
                });
                reportCriterionList.Add(new ReportCriterion()
                {
                    Name = "InvToDate",
                    Values = new string[1] { this.txtInvToDate.Text }
                });
                this.Session["InvFromDate"] = (object)this.txtInvFromDate.Text;
                this.Session["InvToDate"] = (object)this.txtInvToDate.Text;
            }
            else
            {
                this.Session["InvFromDate"] = (object)"0";
                this.Session["InvToDate"] = (object)"0";
            }
            if (this.ddlPOType.SelectedIndex != 0)
            {
                reportCriterionList.Add(new ReportCriterion()
                {
                    Name = "POType",
                    Values = new string[1] { this.ddlPOType.Text }
                });
                this.Session["POType"] = (object)this.ddlPOType.Text;
            }
            else
                this.Session["POType"] = (object)"0";
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
            clsReports.prpPOType = this.Session["POType"].ToString();
            clsReports.prpInvToDate = this.Session["InvToDate"].ToString();
            clsReports.prpInvFromDate = this.Session["InvFromDate"].ToString();
            clsReports.prpProductGroup = this.Session["ProductGroup"].ToString();
            clsReports.prpDealerCode = this.Session["DealerCode"].ToString();
            SqlDataReader dealerReceipt = clsReports.GetDealerReceipt();
            ReportHeader reportHeader = new ReportHeader();
            reportHeader.HeaderText = "Dealer Receipt";
            reportHeader.Alignment = Alignments.Left;
            ReportFooter reportFooter = new ReportFooter();
            reportFooter.FooterText = "Dealer Receipt";
            reportFooter.Alignment = Alignments.Right;
            ExcelReportConfiguration ReportConfiguration = new ExcelReportConfiguration();
            ReportConfiguration.BorderWidth = 1;
            ReportConfiguration.ReportTitle = "Dealer Receipt";
            ReportConfiguration.Footer = reportFooter;
            ReportConfiguration.Header = reportHeader;
            ReportConfiguration.ReportCriteria = reportCriterionList.ToArray();
            string HTTPHandlerAbsolutePath = this.ResolveUrl("~/Handlers/SendFileToBrowser.ashx");
            dealerReceipt.CreateExcelReport(ReportConfiguration).SendFileToBrowser(HTTPHandlerAbsolutePath);
        }
        else
        {
            this.lblMessage.Focus();
            this.lblMessage.Text = "Invalid Dealer Code.Dealer Code Cannot Be Blank.";
            this.lblMessage.CssClass = "ErrorMessage";
        }

    }
    protected bool ValidateDealerCode()
    {
        string strSearchQuery = " where Status=1 and DealerCode='" + txtDealerCode.Text.Trim() + "'";
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
