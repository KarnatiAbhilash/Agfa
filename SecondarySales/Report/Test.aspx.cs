using BusinessClass;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;


public partial class Report_Test : System.Web.UI.Page
{
   
    private ClsReports objRep = new ClsReports();
    protected void Page_Load(object sender, EventArgs e)
    {
        string str1 = "0";
        Decimal num1 = 0M;
        Decimal num2 = 0M;
        this.Server.ScriptTimeout = 12000;
        DataSet dataSet = new DataSet();
        this.objRep.prpBCCode = this.Session["BCCode"].ToString();
        this.objRep.prpPFCode = this.Session["PFCode"].ToString();
        this.objRep.prpCustCode = this.Session["CustCode"].ToString();
        this.objRep.prpGroupCode = this.Session["GroupCode"].ToString();
        this.objRep.prpExecutiveCode = this.Session["ExeCode"].ToString();
        this.objRep.prpInvFromDate = this.Session["InvFromDate"].ToString();
        this.objRep.prpInvToDate = this.Session["InvToDate"].ToString();
        this.objRep.prpItemCode = this.Session["ItemCode"].ToString();
        this.objRep.prpMonth = this.Session["Month"].ToString();
        this.objRep.prpPFCode = this.Session["PFCode"].ToString();
        this.objRep.prpRegion = this.Session["Region"].ToString();
        this.objRep.prpState = this.Session["State"].ToString();
        this.objRep.prpDealerCode = this.Session["DealerCode"].ToString();
        this.objRep.prpYear = this.Session["Year"].ToString();
        SqlDataReader sqlDataReader = this.objRep.funDebitCreditNoteReportNew();
        string str2 = this.Session["BCCode"].ToString() != "0" ? this.Session["BCCode"].ToString() : "";
        string str3 = this.Session["CustCode"].ToString() != "0" ? this.Session["CustCode"].ToString() : "";
        string str4 = this.Session["CustCode"].ToString() != "0" ? this.Session["GroupCode"].ToString() : "";
        string str5 = this.Session["ExeCode"].ToString() != "0" ? this.Session["ExeCode"].ToString() : "";
        string str6 = this.Session["InvFromDate"].ToString() != "0" ? this.Session["InvFromDate"].ToString() : "";
        string str7 = this.Session["InvToDate"].ToString() != "0" ? this.Session["InvToDate"].ToString() : "";
        if (this.Session["ItemCode"].ToString() != "0")
            this.Session["ItemCode"].ToString();
        string str8 = this.Session["Month"].ToString() != "0" ? this.Session["Month"].ToString() : "";
        string str9 = this.Session["PFCode"].ToString() != "0" ? this.Session["PFCode"].ToString() : "";
        string str10 = this.Session["Region"].ToString() != "0" ? this.Session["Region"].ToString() : "";
        string str11 = this.Session["State"].ToString() != "0" ? this.Session["State"].ToString() : "";
        string str12 = this.Session["DealerCode"].ToString() != "0" ? this.Session["DealerCode"].ToString() : "";
        string str13 = this.Session["Year"].ToString() != "0" ? this.Session["Year"].ToString() : "";
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("<table border='1'>");
        stringBuilder.Append("<tr><td colspan='15' align='center'><b><h2>Debit Credit Note Report</h2></b></td></tr>");
        stringBuilder.Append("<tr><td><b>Budget Class</b></td><td colspan='2'>" + str2 + "</td><td><b>Product Family</b></td><td colspan='3'>" + str9 + "</td><td><b>Product Group</b></td><td colspan='3'>" + str4 + "</td><td><b>Item Code</b></td><td colspan='3'>" + str2 + "</td></tr>");
        stringBuilder.Append("<tr></td><td><b>Region</b></td><td colspan='2'>" + str10 + "</td><td><b>DealerCode</b></td><td colspan='3'>" + str12 + "</td><td><b>Executive Code</b></td><td colspan='3'>" + str5 + "<td><b>Customer Group</b></td><td colspan='3'>" + str3 + "</td></tr>");
        stringBuilder.Append("<tr><td><b>Month</b></td>" + str8 + "<td colspan='2'></td><td><b>Year</b></td><td colspan='3'>" + str13 + "</td><td><b>InvFromDate</b></td><td colspan='3'>" + str6 + "</td><td><b>InvToDate</b></td><td colspan='3'>" + str7 + "</td></tr>");
        stringBuilder.Append("<td><b>State</b></td><td colspan='14'>" + str11 + "</td></tr>");
        stringBuilder.Append("<tr><td colspan='15'></td></tr>");
        stringBuilder.Append("</table>");
        stringBuilder.Append("<table border='1'>");
        stringBuilder.Append("<tr><td><b>Dealer Name</b></td>");
        stringBuilder.Append("<td><b>Customer Name</b></td>");
        stringBuilder.Append("<td><b>Invoice No</b></td>");
        stringBuilder.Append("<td><b>Invoice Date</b></td>");
        stringBuilder.Append("<td><b>Item Code</b></td>");
        stringBuilder.Append("<td><b>Qty</b></td>");
        stringBuilder.Append("<td><b>Sqmt</b></td>");
        stringBuilder.Append("<td><b>EU Price</b></td>");
        stringBuilder.Append("<td><b>Agfa Price</b></td>");
        stringBuilder.Append("<td><b>Profit Earned</b></td>");
        stringBuilder.Append("<td><b>Profit Earned (%)</b></td>");
        stringBuilder.Append("<td><b>Profit Agreed (Rs)</b></td>");
        stringBuilder.Append("<td><b>Agreed Margin (%)</b></td>");
        stringBuilder.Append("<td><b>Claim</b></td>");
        stringBuilder.Append("<td><b>Remarks</b></td>");
        stringBuilder.Append("</tr>");
        if (sqlDataReader == null)
        {
            stringBuilder.Append("<tr><td colspan='9'>No records found</td></tr>");
        }
        else
        {
            while (sqlDataReader.Read())
            {
                str1 = (Convert.ToInt32(str1) + Convert.ToInt32(sqlDataReader.GetValue(12).ToString())).ToString();
                stringBuilder.Append("<tr>");
                stringBuilder.Append("<td>" + sqlDataReader.GetValue(9).ToString() + "</td>");
                stringBuilder.Append("<td>" + sqlDataReader.GetValue(10).ToString() + "</td>");
                stringBuilder.Append("<td>" + sqlDataReader.GetValue(0).ToString() + "</td>");
                stringBuilder.Append("<td>" + sqlDataReader.GetValue(11).ToString() + "</td>");
                stringBuilder.Append("<td>" + sqlDataReader.GetValue(4).ToString() + "</td>");
                stringBuilder.Append("<td>" + sqlDataReader.GetValue(12).ToString() + "</td>");
                stringBuilder.Append("<td>" + (object)(Convert.ToDecimal(sqlDataReader.GetValue(14).ToString()) * Convert.ToDecimal(sqlDataReader.GetValue(12).ToString())) + "</td>");
                stringBuilder.Append("<td>" + sqlDataReader.GetValue(13).ToString() + "</td>");
                stringBuilder.Append("<td>" + sqlDataReader.GetValue(20).ToString() + "</td>");
                Decimal num3 = (Convert.ToDecimal(sqlDataReader.GetValue(13).ToString()) - Convert.ToDecimal(sqlDataReader.GetValue(20).ToString())) * Convert.ToDecimal(sqlDataReader.GetValue(12).ToString());
                stringBuilder.Append("<td>" + num3.ToString() + "</td>");
                if (Convert.ToDecimal(sqlDataReader.GetValue(13).ToString()) == 0M)
                    stringBuilder.Append("<td>" + (object)0 + "</td>");
                else
                    stringBuilder.Append("<td>" + (object)(num3 / (Convert.ToDecimal(sqlDataReader.GetValue(13).ToString()) * Convert.ToDecimal(sqlDataReader.GetValue(12).ToString())) * 100M) + "</td>");
                Decimal num4 = Convert.ToDecimal(sqlDataReader.GetValue(13).ToString()) * Convert.ToDecimal(sqlDataReader.GetValue(12).ToString()) * Convert.ToDecimal(sqlDataReader.GetValue(15).ToString()) / 100M;
                stringBuilder.Append("<td>" + (object)num4 + "</td>");
                stringBuilder.Append("<td>" + sqlDataReader.GetValue(15).ToString() + "</td>");
                stringBuilder.Append("<td>" + (num4 - num3).ToString() + "</td>");
                num2 += num4 - num3;
                stringBuilder.Append("<td>" + sqlDataReader.GetValue(16).ToString() + "</td>");
                Decimal num5 = Convert.ToDecimal(sqlDataReader.GetValue(12).ToString()) * Convert.ToDecimal(sqlDataReader.GetValue(14).ToString());
                num1 += num5;
                stringBuilder.Append("</tr>");
            }
        }
        stringBuilder.Append("</table>");
        stringBuilder.Append("<table border='1'>");
        stringBuilder.Append("<tr><td colspan='15' align='center'><b></b></td></tr>");
        stringBuilder.Append("<tr><td><b>Total</b></td><td colspan='4'></td><td><b>" + str1 + "</b></td><td><b>" + num1.ToString() + "</b></td><td colspan='6'><b></b><td><b>" + num2.ToString() + "</b></td><td></td></tr>");
        stringBuilder.Append("<tr></td><td><b>Sales Tax</b></td><td colspan='13'></td><td><b></b></td></tr>");
        stringBuilder.Append("<tr><td><b>Other Levis</b></td><td colspan='13'></td><td><b></b></td></tr>");
        stringBuilder.Append("<tr><td><b>Grand Total</b></td><td colspan='13'></td><td><b></b></td></tr>");
        stringBuilder.Append("</table>");
        HttpResponse response = HttpContext.Current.Response;
        response.Clear();
        response.AddHeader("Content-Disposition", "attachment;filename=DebitCreditNote.xls");
        response.ContentType = "application/vnd.ms-excel";
        string empty = string.Empty;
        string s = stringBuilder.ToString();
        Encoding encoding = (Encoding)new UTF8Encoding();
        response.AddHeader("Content-Length", encoding.GetByteCount(s).ToString());
        response.BinaryWrite(encoding.GetBytes(s));
        response.Charset = "";
        response.End();
    }

    private string DebitCreditNoteHeader()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("<table border='2'><tr><td>");
        stringBuilder.Append("uuuuuuuu<td></tr></table>");
        return stringBuilder.ToString();
    }
}
