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

public partial class Report_ExcelInventoryReport : System.Web.UI.Page
{
    ClsReports objRep = new ClsReports();
    protected void Page_Load(object sender, EventArgs e)
    {
        string strDealerCode;
        string strGroupCode;
        DataSet ObjDs = new DataSet();

        objRep.prpDealerCode = Session["DealerCode"].ToString();
        objRep.prpGroupCode = Session["ProductGroup"].ToString();

        ObjDs = objRep.funInventoryReport();
        strDealerCode = Session["DealerCode"].ToString() != "0" ? Session["DealerCode"].ToString() : "";
        strGroupCode = Session["ProductGroup"].ToString() != "0" ? Session["ProductGroup"].ToString() : "";
        StringBuilder strBiuld = new StringBuilder();
        strBiuld.Append("<table border='1'>");
        strBiuld.Append("<tr><td colspan='8' align='center'><b><h2>As On Inventory Report</h2></b></td></tr>");
        strBiuld.Append("<tr></td><td><b>Dealer Code</b></td><td colspan='3'>" + strDealerCode + "</td><td><b>Product Group</b></td><td colspan='3'>" + strGroupCode + "</td></tr>");

        strBiuld.Append("<tr><td colspan='8'></td></tr>");
        strBiuld.Append("</table>");
        strBiuld.Append("<table border='1'>");
        strBiuld.Append("<tr><td><b>Month</b></td>");
        strBiuld.Append("<td><b>Item Code</b></td>");
        strBiuld.Append("<td><b>Group Code</b></td>");
        strBiuld.Append("<td><b>Opening Stock</b></td>");
        strBiuld.Append("<td><b>Recipt Stock</b></td>");
        strBiuld.Append("<td><b>Issue Stock</b></td>");
        strBiuld.Append("<td><b>Dlr Return Stock</b></td>");
        strBiuld.Append("<td><b>Closing Stock</b></td>");
        strBiuld.Append("</tr>");

        for (int i = 0; i < ObjDs.Tables[0].Rows.Count; i++)
        {
            strBiuld.Append("<tr>");
            strBiuld.Append("<td>" + ObjDs.Tables[0].Rows[i].ItemArray[4].ToString() + "</td>");
            strBiuld.Append("<td>" + ObjDs.Tables[0].Rows[i].ItemArray[2].ToString() + "</td>");
            strBiuld.Append("<td>" + ObjDs.Tables[0].Rows[i].ItemArray[3].ToString() + "</td>");
            strBiuld.Append("<td>" + ObjDs.Tables[0].Rows[i].ItemArray[6].ToString() + "</td>");
            strBiuld.Append("<td>" + ObjDs.Tables[0].Rows[i].ItemArray[7].ToString() + "</td>");
            strBiuld.Append("<td>" + ObjDs.Tables[0].Rows[i].ItemArray[8].ToString() + "</td>");
            strBiuld.Append("<td>" + ObjDs.Tables[0].Rows[i].ItemArray[9].ToString() + "</td>");
            strBiuld.Append("<td>" + ((Convert.ToDecimal(ObjDs.Tables[0].Rows[i].ItemArray[6].ToString()) + Convert.ToDecimal(ObjDs.Tables[0].Rows[i].ItemArray[7].ToString())) -(Convert.ToDecimal(ObjDs.Tables[0].Rows[i].ItemArray[8].ToString()) + Convert.ToDecimal(ObjDs.Tables[0].Rows[i].ItemArray[9].ToString()))).ToString() + "</td>");
            strBiuld.Append("</tr>");
        }
        strBiuld.Append("</table>");

        System.Web.HttpResponse response;
        response = System.Web.HttpContext.Current.Response;
        response.Clear();
        response.AddHeader("Content-Disposition", "attachment;filename=" + "InventoryOnDate.xls");
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
