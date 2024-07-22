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

public partial class Report_InstallbaseFilmConsumption : System.Web.UI.Page
{
    DealerMasterClass objDealer = new DealerMasterClass();
    ClsReports objRep = new ClsReports();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null) Response.Redirect("../Logout.aspx");
        try
        {
            if (!IsPostBack)
            {
             
                CommonFunction.PopulateRecords("ExecutiveMaster", "ExecutiveCode", "ExecutiveCode", "emId", ddlExecutiveCode);
                CommonFunction.PopulateRecordsWithTwoParam("Common_Values", "Text", "Value", "FieldName", "Region", "Status", "1", "Id", ddlRegion);
                CommonFunction.PopulateRecords("stateMaster", "StateName", "StateCode", "StateCode", ddlState);
                CommonFunction.FetchGetMonthYear(1, ddlMonth);
                CommonFunction.FetchGetMonthYear(0, ddlYear);
                CommonFunction.GetCustomerGroup(ddlCustGrp);
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
    protected void ddlBudgetClassCode_SelectedIndexChanged(object sender, EventArgs e)
    {
       // ddlProductFamily.Items.Clear();
        //ddlProductGroupCode.Items.Clear();
       // CommonFunction.PopulateRecordsWithOneParam("ProductFamilyMaster", "pfcode", "pfcode", "Bccode", ddlBudgetClassCode.Text, "pfId", ddlProductFamily);
    }
    protected void ddlProductFamily_SelectedIndexChanged(object sender, EventArgs e)
    {
      //  CommonFunction.PopulateRecordsWithTwoParam("ProductGroupMaster", "GroupCode", "GroupCode", "Bccode", ddlBudgetClassCode.Text, "pfcode", ddlProductFamily.Text, "pgId", ddlProductGroupCode);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        List<ReportCriterion> reportCriterionList = new List<ReportCriterion>();
        ReportCriterion reportCriterion = new ReportCriterion();


        objRep.prpCustCode = "0";
        objRep.prpExecutiveCode = "0";
        objRep.prpInvFromDate = "0";
        objRep.prpInvToDate = "0";
        objRep.prpMonth = "0";
        objRep.prpRegion = "0";
        objRep.prpState = "0";
        objRep.prpYear = "0";
        objRep.prpDealerCode = "0";
        objRep.prpItemCode = "0";

        //2
        if (txtDealerCode.Text.ToString() != "")
        {
            objRep.prpDealerCode = txtDealerCode.Text.Trim();

            reportCriterion = new ReportCriterion();
            reportCriterion.Name = "Dealer Code";
            reportCriterion.Values = new string[1]
            {
                txtDealerCode.Text.Trim()
            };
            reportCriterionList.Add(reportCriterion);
        }
        //3
        if (ddlRegion.SelectedIndex != 0)
        {
            objRep.prpRegion = ddlRegion.Text.Trim();

            reportCriterion = new ReportCriterion();
            reportCriterion.Name = "Region";
            reportCriterion.Values = new string[1]
            {
                ddlRegion.Text.Trim()
            };
            reportCriterionList.Add(reportCriterion);
        }
        //4
        if (txtCustCode.Value != "")
        {
            objRep.prpCustCode = txtCustCode.Value.Trim();
            reportCriterion = new ReportCriterion();
            reportCriterion.Name = "Customer Code";
            reportCriterion.Values = new string[1]
            {
                txtCustCode.Value.Trim()
            };
            reportCriterionList.Add(reportCriterion);
        }

        //5
        if (txtInvFromDate.Text != "" && txtInvToDate.Text != "")
        {

            objRep.prpInvFromDate = txtInvFromDate.Text.Trim();

            reportCriterion = new ReportCriterion();
            reportCriterion.Name = "Invoice From Date";
            reportCriterion.Values = new string[1]
            {
                txtInvFromDate.Text.Trim()
            };
            reportCriterionList.Add(reportCriterion);

            objRep.prpInvToDate = txtInvToDate.Text.Trim();

            reportCriterion = new ReportCriterion();
            reportCriterion.Name = "Invoice To Date";
            reportCriterion.Values = new string[1]
            {
                txtInvToDate.Text.Trim()
            };
            reportCriterionList.Add(reportCriterion);


        }


        if (ddlMonth.SelectedIndex != 0)
        {
            objRep.prpMonth = ddlMonth.Text.Trim();
            reportCriterion.Name = "Month";
            reportCriterion.Values = new string[1]
            {
                ddlMonth.Text.Trim()
            };
            reportCriterionList.Add(reportCriterion);
        }

        if (ddlYear.SelectedIndex != 0)
        {
            objRep.prpYear = ddlYear.Text.Trim();
            reportCriterion = new ReportCriterion();
            reportCriterion.Name = "Year";
            reportCriterion.Values = new string[1]
            {
                ddlYear.Text.Trim()
            };
            reportCriterionList.Add(reportCriterion);
        }





        if (ddlExecutiveCode.SelectedIndex != 0)
        {

            objRep.prpExecutiveCode = ddlExecutiveCode.Text.Trim();

            reportCriterion = new ReportCriterion();
            reportCriterion.Name = "Executive Code";
            reportCriterion.Values = new string[1]
            {
                ddlExecutiveCode.Text.Trim()
            };
            reportCriterionList.Add(reportCriterion);
        }

        if (ddlState.SelectedIndex != 0)
        {
            objRep.prpState = ddlState.Text.Trim();

            reportCriterion = new ReportCriterion();
            reportCriterion.Name = "State";
            reportCriterion.Values = new string[1]
            {
                ddlState.Text.Trim()
            };
            reportCriterionList.Add(reportCriterion);
        }


        if (ddlCustGrp.SelectedIndex != 0)
        {
            objRep.prpInvToDate = ddlCustGrp.Text.Trim();

            reportCriterion = new ReportCriterion();
            reportCriterion.Name = "Customer Group";
            reportCriterion.Values = new string[1]
            {
                 ddlCustGrp.Text.Trim()
        };
            reportCriterionList.Add(reportCriterion);
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
        SqlDataReader dr = objRep.funInstallbaseconsumption();
        ReportHeader reportHeader = new ReportHeader();
        reportHeader.HeaderText = "InstallBase Film Consumption";
        reportHeader.Alignment = Alignments.Left;
        ReportFooter reportFooter = new ReportFooter();
        reportFooter.FooterText = "InstallBase Film Consumption";
        reportFooter.Alignment = Alignments.Right;
        ExcelReportConfiguration ReportConfiguration = new ExcelReportConfiguration();
        ReportConfiguration.BorderWidth = 1;
        ReportConfiguration.ReportTitle = "InstallBase Film Consumption";
        ReportConfiguration.Footer = reportFooter;
        ReportConfiguration.Header = reportHeader;
        ReportConfiguration.ReportCriteria = reportCriterionList.ToArray();
        string HTTPHandlerAbsolutePath = this.ResolveUrl("~/Handlers/SendFileToBrowser.ashx");
        dr.CreateExcelReport(ReportConfiguration).SendFileToBrowser(HTTPHandlerAbsolutePath);

    }
    protected bool ValidateDealerCode()
    {
        string strSearchQuery = " where Status=1 and DealerCode='" + txtDealerCode.Text.Trim() + "'";
        DataSet dsDealer = objDealer.FetchDealerMaster(strSearchQuery);
        if (dsDealer.Tables[0].Rows.Count > 0)
        {
           
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