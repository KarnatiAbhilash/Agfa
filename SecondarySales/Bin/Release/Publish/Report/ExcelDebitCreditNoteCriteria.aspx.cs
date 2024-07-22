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
using System.Text;
using System.IO;
using System.Data.SqlClient;

public partial class Report_Test : System.Web.UI.Page
{
    ClsReports objRep = new ClsReports();
    protected void Page_Load(object sender, EventArgs e)
    {
        decimal DblProfitAgreed;
        decimal DblProfitMargin;
        string strQty="0";
        decimal decSumSqmt = 0;
        decimal decSqmt = 0;
        decimal decSumClaim = 0;
        string strBCCode;
        string strCustCode;
        string strGroupCode;
        string strExecutiveCode;
        string strInvFromDate;
        string strInvToDate;
        string strItemCode;
        string strMonth;
        string strPFCode;
        string strRegion;
        string strState;
        string strDealerCode;
        string strYear;
        Server.ScriptTimeout = 12000;

        DataSet ObjDs = new DataSet();
        DataTable ObjTbl;
        objRep.prpBCCode = Session["BCCode"].ToString();
        objRep.prpPFCode = Session["PFCode"].ToString();
        objRep.prpCustCode = Session["CustCode"].ToString();
        objRep.prpGroupCode = Session["GroupCode"].ToString();
        objRep.prpExecutiveCode = Session["ExeCode"].ToString();
        objRep.prpInvFromDate = Session["InvFromDate"].ToString();
        objRep.prpInvToDate = Session["InvToDate"].ToString();
        objRep.prpItemCode = Session["ItemCode"].ToString();
        objRep.prpMonth = Session["Month"].ToString();
        objRep.prpPFCode = Session["PFCode"].ToString();
        objRep.prpRegion = Session["Region"].ToString();
        objRep.prpState = Session["State"].ToString();
        objRep.prpDealerCode = Session["DealerCode"].ToString();
        objRep.prpYear = Session["Year"].ToString();
        
        SqlDataReader dr = objRep.funDebitCreditNoteReportNew();
        
        //ObjDs = objRep.funDebitCreditNoteReport();

        //ObjTbl = new DataTable();
        //ObjTbl = ObjDs.Tables[0];
        //strQty = ObjTbl.Compute("sum(Qty)", "").ToString();

        strBCCode = Session["BCCode"].ToString() != "0" ? Session["BCCode"].ToString() : "";
        strCustCode = Session["CustCode"].ToString() != "0" ? Session["CustCode"].ToString() : "";
        strGroupCode = Session["CustCode"].ToString() != "0" ? Session["GroupCode"].ToString() : "";
        strExecutiveCode = Session["ExeCode"].ToString() != "0" ? Session["ExeCode"].ToString() : "";
        strInvFromDate = Session["InvFromDate"].ToString() != "0" ? Session["InvFromDate"].ToString() : "";
        strInvToDate = Session["InvToDate"].ToString() != "0" ? Session["InvToDate"].ToString() : "";
        strItemCode = Session["ItemCode"].ToString() != "0" ? Session["ItemCode"].ToString() : "";
        strMonth = Session["Month"].ToString() != "0" ? Session["Month"].ToString() : "";
        strPFCode = Session["PFCode"].ToString() != "0" ? Session["PFCode"].ToString() : "";
        strRegion = Session["Region"].ToString() != "0" ? Session["Region"].ToString() : "";
        strState = Session["State"].ToString() != "0" ? Session["State"].ToString() : "";
        strDealerCode = Session["DealerCode"].ToString() != "0" ? Session["DealerCode"].ToString() : "";
        strYear = Session["Year"].ToString() != "0" ? Session["Year"].ToString() : "";

        StringBuilder strBiuld = new StringBuilder();
        strBiuld.Append("<table border='1'>");
        strBiuld.Append("<tr><td colspan='15' align='center'><b><h2>Debit Credit Note Report</h2></b></td></tr>");
        strBiuld.Append("<tr><td><b>Budget Class</b></td><td colspan='2'>" + strBCCode + "</td><td><b>Product Family</b></td><td colspan='3'>" + strPFCode + "</td><td><b>Product Group</b></td><td colspan='3'>" + strGroupCode + "</td><td><b>Item Code</b></td><td colspan='3'>" + strBCCode + "</td></tr>");
        strBiuld.Append("<tr></td><td><b>Region</b></td><td colspan='2'>" + strRegion + "</td><td><b>DealerCode</b></td><td colspan='3'>" + strDealerCode + "</td><td><b>Executive Code</b></td><td colspan='3'>" + strExecutiveCode + "<td><b>Customer Group</b></td><td colspan='3'>" + strCustCode + "</td></tr>");
        strBiuld.Append("<tr><td><b>Month</b></td>" + strMonth + "<td colspan='2'></td><td><b>Year</b></td><td colspan='3'>" + strYear + "</td><td><b>InvFromDate</b></td><td colspan='3'>" + strInvFromDate + "</td><td><b>InvToDate</b></td><td colspan='3'>" + strInvToDate + "</td></tr>");
        strBiuld.Append("<td><b>State</b></td><td colspan='14'>" + strState + "</td></tr>");



        strBiuld.Append("<tr><td colspan='15'></td></tr>");
        strBiuld.Append("</table>");
        strBiuld.Append("<table border='1'>");
        strBiuld.Append("<tr><td><b>Dealer Name</b></td>");
        strBiuld.Append("<td><b>Customer Name</b></td>");
        strBiuld.Append("<td><b>Invoice No</b></td>");
        strBiuld.Append("<td><b>Invoice Date</b></td>");
        strBiuld.Append("<td><b>Item Code</b></td>");
        strBiuld.Append("<td><b>Qty</b></td>");
        strBiuld.Append("<td><b>Sqmt</b></td>");
        strBiuld.Append("<td><b>EU Price</b></td>");
        strBiuld.Append("<td><b>Agfa Price</b></td>");
        strBiuld.Append("<td><b>Profit Earned</b></td>");
        strBiuld.Append("<td><b>Profit Earned (%)</b></td>");
        strBiuld.Append("<td><b>Profit Agreed (Rs)</b></td>");
        strBiuld.Append("<td><b>Agreed Margin (%)</b></td>");
        strBiuld.Append("<td><b>Claim</b></td>");
        strBiuld.Append("<td><b>Remarks</b></td>");
        strBiuld.Append("</tr>");

        if (dr == null)
        {
            strBiuld.Append("<tr><td colspan='9'>No records found</td></tr>");

        }
        else
        {
            while (dr.Read())
            {
                strQty = (Convert.ToInt32(strQty) + Convert.ToInt32(dr.GetValue(12).ToString())).ToString();
                strBiuld.Append("<tr>");
                strBiuld.Append("<td>" + dr.GetValue(9).ToString() + "</td>");
                strBiuld.Append("<td>" + dr.GetValue(10).ToString() + "</td>");
                strBiuld.Append("<td>" + dr.GetValue(0).ToString() + "</td>");
                strBiuld.Append("<td>" + dr.GetValue(11).ToString() + "</td>");
                strBiuld.Append("<td>" + dr.GetValue(4).ToString() + "</td>");
                strBiuld.Append("<td>" + dr.GetValue(12).ToString() + "</td>");
                strBiuld.Append("<td>" + Convert.ToDecimal(dr.GetValue(14).ToString()) * Convert.ToDecimal(dr.GetValue(12).ToString()) + "</td>");
                strBiuld.Append("<td>" + dr.GetValue(13).ToString() + "</td>");
                strBiuld.Append("<td>" + dr.GetValue(20).ToString() + "</td>");
                DblProfitAgreed = ((Convert.ToDecimal(dr.GetValue(13).ToString()) - Convert.ToDecimal(dr.GetValue(20).ToString())) * Convert.ToDecimal(dr.GetValue(12).ToString()));
                strBiuld.Append("<td>" + DblProfitAgreed.ToString() + "</td>");
                if (Convert.ToDecimal(dr.GetValue(13).ToString()) == 0)
                    strBiuld.Append("<td>" + 0 + "</td>");
                else
                    strBiuld.Append("<td>" + (DblProfitAgreed / (Convert.ToDecimal(dr.GetValue(13).ToString()) * Convert.ToDecimal(dr.GetValue(12).ToString())) * 100) + "</td>");
                DblProfitMargin = (Convert.ToDecimal(dr.GetValue(13).ToString()) * Convert.ToDecimal(dr.GetValue(12).ToString()) * Convert.ToDecimal(dr.GetValue(15).ToString())) / 100;
                strBiuld.Append("<td>" + DblProfitMargin + "</td>");
                strBiuld.Append("<td>" + dr.GetValue(15).ToString() + "</td>");
                strBiuld.Append("<td>" + (DblProfitMargin - DblProfitAgreed).ToString() + "</td>");
                decSumClaim = decSumClaim + (DblProfitMargin - DblProfitAgreed);
                strBiuld.Append("<td>" + dr.GetValue(16).ToString() + "</td>");
                decSqmt = Convert.ToDecimal(dr.GetValue(12).ToString()) * Convert.ToDecimal(dr.GetValue(14).ToString());
                decSumSqmt = decSumSqmt + decSqmt;
                strBiuld.Append("</tr>");
            }
        }



        //for (int i = 0; i < ObjDs.Tables[0].Rows.Count; i++)
        //{
        //    strBiuld.Append("<tr>");
        //    strBiuld.Append("<td>" + ObjDs.Tables[0].Rows[i].ItemArray[9].ToString() + "</td>");
        //    strBiuld.Append("<td>" + ObjDs.Tables[0].Rows[i].ItemArray[10].ToString() + "</td>");
        //    strBiuld.Append("<td>" + ObjDs.Tables[0].Rows[i].ItemArray[0].ToString() + "</td>");
        //    strBiuld.Append("<td>" + ObjDs.Tables[0].Rows[i].ItemArray[11].ToString() + "</td>");
        //    strBiuld.Append("<td>" + ObjDs.Tables[0].Rows[i].ItemArray[4].ToString() + "</td>");
        //    strBiuld.Append("<td>" + ObjDs.Tables[0].Rows[i].ItemArray[12].ToString() + "</td>");
        //    strBiuld.Append("<td>" + Convert.ToDecimal(ObjDs.Tables[0].Rows[i].ItemArray[14].ToString()) * Convert.ToDecimal(ObjDs.Tables[0].Rows[i].ItemArray[12].ToString()) + "</td>");
        //    strBiuld.Append("<td>" + ObjDs.Tables[0].Rows[i].ItemArray[13].ToString() + "</td>");
        //    strBiuld.Append("<td>" + ObjDs.Tables[0].Rows[i].ItemArray[20].ToString() + "</td>");
        //    DblProfitAgreed = ((Convert.ToDecimal(ObjDs.Tables[0].Rows[i].ItemArray[13].ToString()) - Convert.ToDecimal(ObjDs.Tables[0].Rows[i].ItemArray[20].ToString())) * Convert.ToDecimal(ObjDs.Tables[0].Rows[i].ItemArray[12].ToString()));
        //    strBiuld.Append("<td>" + DblProfitAgreed.ToString() + "</td>");
        //    if (Convert.ToDecimal(ObjDs.Tables[0].Rows[i].ItemArray[13].ToString()) == 0)
        //        strBiuld.Append("<td>" + 0 + "</td>");
        //    else
        //        strBiuld.Append("<td>" + (DblProfitAgreed / (Convert.ToDecimal(ObjDs.Tables[0].Rows[i].ItemArray[13].ToString()) * Convert.ToDecimal(ObjDs.Tables[0].Rows[i].ItemArray[12].ToString())) * 100) + "</td>");
        //    DblProfitMargin = (Convert.ToDecimal(ObjDs.Tables[0].Rows[i].ItemArray[13].ToString()) * Convert.ToDecimal(ObjDs.Tables[0].Rows[i].ItemArray[12].ToString()) * Convert.ToDecimal(ObjDs.Tables[0].Rows[i].ItemArray[15].ToString())) / 100;
        //    strBiuld.Append("<td>" + DblProfitMargin + "</td>");
        //    strBiuld.Append("<td>" + ObjDs.Tables[0].Rows[i].ItemArray[15].ToString() + "</td>");
        //    strBiuld.Append("<td>" + (DblProfitMargin - DblProfitAgreed).ToString() + "</td>");
        //    decSumClaim = decSumClaim + (DblProfitMargin - DblProfitAgreed);
        //    strBiuld.Append("<td>" + ObjDs.Tables[0].Rows[i].ItemArray[16].ToString() + "</td>");
        //    decSqmt = Convert.ToDecimal(ObjDs.Tables[0].Rows[i].ItemArray[12].ToString()) * Convert.ToDecimal(ObjDs.Tables[0].Rows[i].ItemArray[14].ToString());
        //    decSumSqmt = decSumSqmt + decSqmt;
        //    strBiuld.Append("</tr>");
        //}
        strBiuld.Append("</table>");

        strBiuld.Append("<table border='1'>");
        strBiuld.Append("<tr><td colspan='15' align='center'><b></b></td></tr>");
        strBiuld.Append("<tr><td><b>Total</b></td><td colspan='4'></td><td><b>" + strQty + "</b></td><td><b>" + decSumSqmt.ToString() + "</b></td><td colspan='6'><b></b><td><b>" + decSumClaim.ToString() + "</b></td><td></td></tr>");
        strBiuld.Append("<tr></td><td><b>Sales Tax</b></td><td colspan='13'></td><td><b></b></td></tr>");
        strBiuld.Append("<tr><td><b>Other Levis</b></td><td colspan='13'></td><td><b></b></td></tr>");
        strBiuld.Append("<tr><td><b>Grand Total</b></td><td colspan='13'></td><td><b></b></td></tr>");
        strBiuld.Append("</table>");

        System.Web.HttpResponse response;
        response = System.Web.HttpContext.Current.Response;
        response.Clear();
        response.AddHeader("Content-Disposition", "attachment;filename=" + "DebitCreditNote.xls");
        response.ContentType = "application/vnd.ms-excel";
        String exportContent = String.Empty;
        exportContent = strBiuld.ToString();
        System.Text.Encoding Encoding = new System.Text.UTF8Encoding();
        response.AddHeader("Content-Length", Encoding.GetByteCount(exportContent).ToString());
        response.BinaryWrite(Encoding.GetBytes(exportContent));
        response.Charset = "";
        response.End();
    }

    private string DebitCreditNoteHeader()
    {
        StringBuilder strBiuld = new StringBuilder();

        strBiuld.Append("<table border='2'><tr><td>");
        strBiuld.Append("uuuuuuuu<td></tr></table>");
        return strBiuld.ToString();



    }
}
