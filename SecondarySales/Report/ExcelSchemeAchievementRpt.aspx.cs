using BusinessClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_ExcelSchemeAchievementRpt : System.Web.UI.Page
{
    private ClsReports objRep = new ClsReports();
    protected void Page_Load(object sender, EventArgs e)
    {

        Decimal num1 = 0M;
        Decimal num2 = 0M;
        Decimal num3 = 0M;
        Decimal num4 = 0M;
        DataSet dataSet1 = new DataSet();
        this.objRep.prpDealerCode = this.Session["DealerCode"].ToString();
        this.objRep.prpInvFromDate = this.Session["InvFromDate"].ToString();
        this.objRep.prpInvToDate = this.Session["InvToDate"].ToString();
        this.objRep.prpItemCode = this.Session["ItemCode"].ToString();
        this.objRep.prpCustCode = this.Session["CustCode"].ToString();
        DataTable dataTable = new DataTable();
        DataSet dataSet2 = this.objRep.funSchemeAchivementReport();
        DataTable table = dataSet2.Tables[0];
        string str1 = this.Session["InvFromDate"].ToString() != "0" ? this.Session["InvFromDate"].ToString() : "";
        string str2 = this.Session["InvToDate"].ToString() != "0" ? this.Session["InvToDate"].ToString() : "";
        string str3 = this.Session["ItemCode"].ToString() != "0" ? this.Session["ItemCode"].ToString() : "";
        string str4 = this.Session["DealerCode"].ToString() != "0" ? this.Session["DealerCode"].ToString() : "";
        string str5 = this.Session["CustCode"].ToString() != "0" ? this.Session["CustCode"].ToString() : "";
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("<table border='1'>");
        stringBuilder.Append("<tr><td colspan='10' align='center'><b><h2>Scheme Achivement Report</h2></b></td></tr>");
        stringBuilder.Append("<tr></td><td><b>Dealer Code</b></td><td>" + str4 + "</td><td><b>ItemCode</b></td><td>" + str3 + "</td><td><b>Customer Code</b></td><td>" + str5);
        stringBuilder.Append("<td><b>Invoice From Date</b></td><td>" + str1 + "</td><td><b>Invoice To Date</b></td><td>" + str2 + "</td></tr>");
        stringBuilder.Append("<tr><td colspan='10'></td></tr>");
        stringBuilder.Append("</table>");
        stringBuilder.Append("<table border='1'>");
        stringBuilder.Append("<tr><td><b>Customer</b></td>");
        stringBuilder.Append("<td><b>InvoiceNo</b></td>");
        stringBuilder.Append("<td><b>InvoiceDate</b></td>");
        stringBuilder.Append("<td><b>ItemCode</b></td>");
        stringBuilder.Append("<td><b>Qty Consumption</b></td>");
        stringBuilder.Append("<td><b>Qty Eligible</b></td>");
        stringBuilder.Append("<td><b>Sqmt Consumption</b></td>");
        stringBuilder.Append("<td><b>Sqmt Eligible</b></td>");
        stringBuilder.Append("<td><b>Dealer Price</b></td>");
        stringBuilder.Append("<td><b>EU Price</b></td>");
        stringBuilder.Append("</tr>");
        for (int index = 0; index < dataSet2.Tables[0].Rows.Count; ++index)
        {
            stringBuilder.Append("<tr>");
            stringBuilder.Append("<td>" + dataSet2.Tables[0].Rows[index].ItemArray[1].ToString() + "</td>");
            stringBuilder.Append("<td>" + dataSet2.Tables[0].Rows[index].ItemArray[2].ToString() + "</td>");
            stringBuilder.Append("<td>" + dataSet2.Tables[0].Rows[index].ItemArray[3].ToString() + "</td>");
            stringBuilder.Append("<td>" + dataSet2.Tables[0].Rows[index].ItemArray[4].ToString() + "</td>");
            stringBuilder.Append("<td>" + dataSet2.Tables[0].Rows[index].ItemArray[7].ToString() + "</td>");
            stringBuilder.Append("<td>" + (Convert.ToDecimal(dataSet2.Tables[0].Rows[index].ItemArray[7].ToString()) / (Convert.ToDecimal(dataSet2.Tables[0].Rows[index].ItemArray[5].ToString()) / Convert.ToDecimal(dataSet2.Tables[0].Rows[index].ItemArray[6].ToString()))).ToString("#.##", (IFormatProvider)CultureInfo.InvariantCulture) + "</td>");
            stringBuilder.Append("<td>" + (Convert.ToDecimal(dataSet2.Tables[0].Rows[index].ItemArray[7].ToString()) * Convert.ToDecimal(dataSet2.Tables[0].Rows[index].ItemArray[10].ToString())).ToString("#.##", (IFormatProvider)CultureInfo.InvariantCulture) + "</td>");
            stringBuilder.Append("<td>" + (Convert.ToDecimal(dataSet2.Tables[0].Rows[index].ItemArray[7].ToString()) * Convert.ToDecimal(dataSet2.Tables[0].Rows[index].ItemArray[10].ToString()) / (Convert.ToDecimal(dataSet2.Tables[0].Rows[index].ItemArray[8].ToString()) / Convert.ToDecimal(dataSet2.Tables[0].Rows[index].ItemArray[9].ToString()))).ToString("#.##", (IFormatProvider)CultureInfo.InvariantCulture) + "</td>");
            stringBuilder.Append("<td>" + dataSet2.Tables[0].Rows[index].ItemArray[11].ToString() + "</td>");
            stringBuilder.Append("<td>" + dataSet2.Tables[0].Rows[index].ItemArray[12].ToString() + "</td>");
            stringBuilder.Append("</tr>");
            num2 += Convert.ToDecimal(dataSet2.Tables[0].Rows[index].ItemArray[7].ToString());
            num1 += Convert.ToDecimal(dataSet2.Tables[0].Rows[index].ItemArray[7].ToString()) / (Convert.ToDecimal(dataSet2.Tables[0].Rows[index].ItemArray[5].ToString()) / Convert.ToDecimal(dataSet2.Tables[0].Rows[index].ItemArray[6].ToString()));
            num3 += Convert.ToDecimal(dataSet2.Tables[0].Rows[index].ItemArray[7].ToString()) * Convert.ToDecimal(dataSet2.Tables[0].Rows[index].ItemArray[10].ToString());
            num4 += Convert.ToDecimal(dataSet2.Tables[0].Rows[index].ItemArray[7].ToString()) * Convert.ToDecimal(dataSet2.Tables[0].Rows[index].ItemArray[10].ToString()) / (Convert.ToDecimal(dataSet2.Tables[0].Rows[index].ItemArray[8].ToString()) / Convert.ToDecimal(dataSet2.Tables[0].Rows[index].ItemArray[9].ToString()));
        }
        stringBuilder.Append("</table>");
        stringBuilder.Append("<table border='1'>");
        stringBuilder.Append("<tr><td colspan='10' align='center'><b></b></td></tr>");
        stringBuilder.Append("<tr><td colspan='4'><b>Total</b></td><td><b>" + num2.ToString("#.##", (IFormatProvider)CultureInfo.InvariantCulture) + "</b></td><td><b>" + num1.ToString("#.##", (IFormatProvider)CultureInfo.InvariantCulture) + "</b></td><td><b>" + num3.ToString("#.##", (IFormatProvider)CultureInfo.InvariantCulture) + "</b><td><b>" + num4.ToString("#.##", (IFormatProvider)CultureInfo.InvariantCulture) + "</b></td><td colspan='2'></td></tr>");
        stringBuilder.Append("</table>");
        HttpResponse response = HttpContext.Current.Response;
        response.Clear();
        response.AddHeader("Content-Disposition", "attachment;filename=ScemeAcheivement.xls");
        response.ContentType = "application/vnd.ms-excel";
        string empty = string.Empty;
        string s = stringBuilder.ToString();
        Encoding encoding = (Encoding)new UTF8Encoding();
        response.AddHeader("Content-Length", encoding.GetByteCount(s).ToString());
        response.BinaryWrite(encoding.GetBytes(s));
        response.Charset = "";
        response.End();

    }
}