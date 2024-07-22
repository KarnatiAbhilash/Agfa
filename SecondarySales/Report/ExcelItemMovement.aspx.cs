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
using System.Web.SessionState;

public partial class Report_ExcelItemMovement : Page, IRequiresSessionState
{
    ClsReports objRep = new ClsReports();
    protected void Page_Load(object sender, EventArgs e)
    {
        string strInvFromDate;
        string strInvToDate;
        string strItemCode;
        string strSalesType;
        string strDealerCode;
        int OpenStock,CloseStock;

        DataSet ObjDs = new DataSet();

        objRep.prpDealerCode = Session["DealerCode"].ToString();
        objRep.prpInvFromDate = Session["InvFromDate"].ToString();
        objRep.prpInvToDate = Session["InvToDate"].ToString();
        objRep.prpItemCode = Session["ItemCode"].ToString();
        objRep.prpSalesType = Session["SalesType"].ToString();

        ObjDs = objRep.funItemMovmentReport();

        if (ObjDs.Tables[1] == null)
            OpenStock = 0;
        else
            OpenStock = Convert.ToInt32(ObjDs.Tables[1].Rows[0][0]);

        if (ObjDs.Tables[2].Rows.Count != 0 && ObjDs.Tables[4].Rows.Count != 0 && ObjDs.Tables[3].Rows.Count != 0 && ObjDs.Tables[5].Rows.Count != 0)
            CloseStock = OpenStock + Convert.ToInt32(ObjDs.Tables[2].Rows[0][0]) + Convert.ToInt32(ObjDs.Tables[4].Rows[0][0]) - Convert.ToInt32(ObjDs.Tables[3].Rows[0][0]) - Convert.ToInt32(ObjDs.Tables[5].Rows[0][0]);
        else
            CloseStock = 0;
        strInvFromDate = Session["InvFromDate"].ToString() != "0" ? Session["InvFromDate"].ToString() : "";
        strInvToDate = Session["InvToDate"].ToString() != "0" ? Session["InvToDate"].ToString() : "";
        strItemCode = Session["ItemCode"].ToString() != "0" ? Session["ItemCode"].ToString() : "";
        strDealerCode = Session["DealerCode"].ToString() != "0" ? Session["DealerCode"].ToString() : "";
        strSalesType = Session["SalesType"].ToString() != "0" ? Session["SalesType"].ToString() : "";
        StringBuilder strBiuld = new StringBuilder();
        strBiuld.Append("<table border='1'>");
        
        strBiuld.Append("<tr><td colspan='13' align='center'><b><h2>Item Movement Report</h2></b></td></tr>");
        strBiuld.Append("<tr></td><td><b>Dealer Code</b></td><td>" + strDealerCode + "</td><td><b>ItemCode</b></td><td>" + strItemCode + "</td><td colspan='2'><b>Sales Type</b></td><td>" + strSalesType + "");
        strBiuld.Append("<td colspan='2'><b>Invoice From Date</b></td><td>" + strInvFromDate + "</td><td colspan='2'><b>Invoice To Date</b></td><td>" + strInvToDate + "</td></tr>");

        strBiuld.Append("<tr><td colspan='13' align='center'></td></tr>");
        if (objRep.prpInvFromDate == "0" && objRep.prpInvToDate == "0")
            strBiuld.Append("<tr><td colspan='13'><b>Opening Stock: " + 0 + "</b></td></tr>");
        else
            strBiuld.Append("<tr><td colspan='13'><b>Opening Stock: "+CloseStock+"</b></td></tr>");
        strBiuld.Append("</table>");
        strBiuld.Append("<table border='1'>");
        strBiuld.Append("<tr><td><b>Sales Type</b></td>");
        strBiuld.Append("<td><b>AgfaRefNo</b></td>");
        strBiuld.Append("<td><b>DlrRefNo</b></td>");
        strBiuld.Append("<td><b>Invoice Date</b></td>");
        strBiuld.Append("<td><b>Customer Name</b></td>");
        strBiuld.Append("<td><b>Receipt Qty</b></td>");
        strBiuld.Append("<td><b>Issue Qty</b></td>");
        strBiuld.Append("<td><b>UserType</b></td>");
        strBiuld.Append("<td><b>DalerStockReturn Qty</b></td>");
        strBiuld.Append("<td><b>CustomerStockReturn Qty</b></td>");
        strBiuld.Append("<td><b>Value</b></td>");
        strBiuld.Append("<td><b>Stock Qty</b></td>");
        strBiuld.Append("<td><b>Agfa Price</b></td>");
        strBiuld.Append("</tr>");

        

        for (int i = 0; i < ObjDs.Tables[0].Rows.Count; i++)
        {
            strBiuld.Append("<tr>");
            strBiuld.Append("<td>" + ObjDs.Tables[0].Rows[i].ItemArray[0].ToString() + "</td>");
            strBiuld.Append("<td>" + ObjDs.Tables[0].Rows[i].ItemArray[13].ToString() + "</td>");
            strBiuld.Append("<td>" + ObjDs.Tables[0].Rows[i].ItemArray[14].ToString() + "</td>");
            strBiuld.Append("<td>" + ObjDs.Tables[0].Rows[i].ItemArray[12].ToString() + "</td>");
            strBiuld.Append("<td>" + ObjDs.Tables[0].Rows[i].ItemArray[11].ToString() + "</td>");
            strBiuld.Append("<td>" + ObjDs.Tables[0].Rows[i].ItemArray[6].ToString() + "</td>");
            strBiuld.Append("<td>" + ObjDs.Tables[0].Rows[i].ItemArray[7].ToString() + "</td>");
            strBiuld.Append("<td>"+ ObjDs.Tables[0].Rows[i].ItemArray[8].ToString() + "</td>");
            if (ObjDs.Tables[0].Rows[i].ItemArray[8].ToString() == "Dealer")
                strBiuld.Append("<td>"+ ObjDs.Tables[0].Rows[i].ItemArray[9].ToString() + "</td>");
            else
                strBiuld.Append("<td>"+0+"</td>");
            if (ObjDs.Tables[0].Rows[i].ItemArray[8].ToString() == "Customer")
                strBiuld.Append("<td>" + ObjDs.Tables[0].Rows[i].ItemArray[9].ToString() + "</td>");
            else
                strBiuld.Append("<td>"+0+"</td>");

            strBiuld.Append("<td>" + ObjDs.Tables[0].Rows[i].ItemArray[10].ToString() + "</td>");
            if (ObjDs.Tables[0].Rows[i].ItemArray[8].ToString() == "Customer")
                CloseStock = CloseStock + Convert.ToInt32(ObjDs.Tables[0].Rows[i].ItemArray[6]) + Convert.ToInt32(ObjDs.Tables[0].Rows[i].ItemArray[9]) - Convert.ToInt32(ObjDs.Tables[0].Rows[i].ItemArray[7]);
            else
                CloseStock = CloseStock + Convert.ToInt32(ObjDs.Tables[0].Rows[i].ItemArray[6]) - Convert.ToInt32(ObjDs.Tables[0].Rows[i].ItemArray[9]) - Convert.ToInt32(ObjDs.Tables[0].Rows[i].ItemArray[7]);

            strBiuld.Append("<td>" + CloseStock + "</td>");
            strBiuld.Append("<td>" + ObjDs.Tables[0].Rows[i].ItemArray[15].ToString() + "</td>");
            strBiuld.Append("</tr>");

            
        }
        strBiuld.Append("</table>");

        strBiuld.Append("<table border='1'>");
        strBiuld.Append("<tr><td colspan='10' align='center'><b></b></td></tr>");
        if (objRep.prpInvFromDate == "0" && objRep.prpInvToDate == "0")
            strBiuld.Append("<tr><td colspan='10' align='center'><b>Closing Balance:" + 0 + "</b></td></tr>");
        else
            strBiuld.Append("<tr><td colspan='10' align='center'><b>Closing Balance:" + CloseStock + "</b></td></tr>");
        strBiuld.Append("</table>");

        System.Web.HttpResponse response;
        response = System.Web.HttpContext.Current.Response;
        response.Clear();
        response.AddHeader("Content-Disposition", "attachment;filename=" + "ItemMovement.xls");
        response.ContentType = "application/vnd.ms-excel";
        String exportContent = String.Empty;
        exportContent = strBiuld.ToString();
        System.Text.Encoding Encoding = new System.Text.UTF8Encoding();
        response.AddHeader("Content-Length", Encoding.GetByteCount(exportContent).ToString());
        response.BinaryWrite(Encoding.GetBytes(exportContent));
        response.Charset = "";
        response.End();
    }
}
