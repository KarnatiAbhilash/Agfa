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

public partial class Report_ExcelPriceList : System.Web.UI.Page
{
    ClsReports objRep = new ClsReports();
    protected void Page_Load(object sender, EventArgs e)
    {
        string strCustCode="";
        string strGroupCode="0";
        string strExecutiveCode="0";
        string strItemCode="";
        string strRegion="0";
        string strState="0";
        string strDealerCode="";
        Server.ScriptTimeout = 12000;

        strCustCode = Request.QueryString["CustCode"].Trim();
        strItemCode = Request.QueryString["ItemCode"].Trim();
        strDealerCode = Request.QueryString["DealerCode"].Trim();
        if (Request.QueryString["ExcutiveCode"] != null && Request.QueryString["ExcutiveCode"] != "0")
            strExecutiveCode = Request.QueryString["ExcutiveCode"].Trim();
        if (Request.QueryString["Region"] != null && Request.QueryString["Region"] != "0")
            strRegion = Request.QueryString["Region"].Trim();
        if (Request.QueryString["CustGroup"] != null && Request.QueryString["CustGroup"] != "0")
            strGroupCode = Request.QueryString["CustGroup"].Trim();
        if (Request.QueryString["State"] != null && Request.QueryString["State"] != "0")
            strState = Request.QueryString["State"].Trim();
        
        objRep.prpDealerCode = strDealerCode;
        objRep.prpCustCode = strCustCode;
        objRep.prpItemCode = strItemCode;
        objRep.prpExecutiveCode = strExecutiveCode;
        objRep.prpRegion = strRegion;
        objRep.prpState = strState;
        objRep.prpCustGroup = strGroupCode;

        DataTable ObjTbl;
        ObjTbl = objRep.GetItemPriceList().Tables[0];
        StringBuilder strBiuld = new StringBuilder();
        strBiuld.Append("<table border='1'>");
        strBiuld.Append("<tr><td colspan='10' align='center'><b><h2>Item Price List Report</h2></b></td></tr>");
        strBiuld.Append("<tr></td><td><b>Costomer Code</b></td><td align='left'>" + strCustCode + "</td><td><b>ItemCode</b></td><td align='left'>" + strItemCode + "</td><td align='left'><b>Executive Code</b></td><td align='left'>" + strExecutiveCode + "</td><td><b>Region</b></td><td align='left'>" + strRegion + "</td><td><b>Dealer Code</b></td><td align='left'>" + strDealerCode + "</td></tr>");
        strBiuld.Append("<tr><td><b>Costomer Group</b></td><td align='left'>" + strGroupCode + "</td><td><b>State</b></td><td colspan='7' align='left'>" + strState + "</td></tr>");
        strBiuld.Append("<tr><td colspan='10' align='center'></td></tr></table>");

        strBiuld.Append("<table border='1'>");
        strBiuld.Append("<tr><td><b>Dealer Name</b></td>");
        strBiuld.Append("<td><b>Region</b></td>");
        strBiuld.Append("<td><b>Customer Code</b></td>");
        strBiuld.Append("<td><b>Customer Name</b></td>");
        strBiuld.Append("<td><b>City</b></td>");
        strBiuld.Append("<td><b>Item Code</b></td>");
        strBiuld.Append("<td><b>End User Price</b></td>");
        strBiuld.Append("<td><b>Agfa Price</b></td>");
        strBiuld.Append("<td><b>Profit Earned(%)</b></td>");
        strBiuld.Append("<td><b>Agreed Margin(%)</b></td>");
        strBiuld.Append("<td><b>Remarks</b></td>");
        strBiuld.Append("<td><b>Modified By</b></td>");
        strBiuld.Append("<td><b>Modified On</b></td>");
        strBiuld.Append("</tr>");

        for (int i = 0; i < ObjTbl.Rows.Count; i++)
        {
            strBiuld.Append("<tr>");
            strBiuld.Append("<td>" + ObjTbl.Rows[i]["DealerName"].ToString() + "</td>");
            strBiuld.Append("<td>" + ObjTbl.Rows[i]["Region"].ToString() + "</td>");
            strBiuld.Append("<td>" + ObjTbl.Rows[i]["CustCode"].ToString() + "</td>");
            strBiuld.Append("<td>" + ObjTbl.Rows[i]["CustName"].ToString() + "</td>");
            strBiuld.Append("<td>" + ObjTbl.Rows[i]["City"].ToString() + "</td>");
            strBiuld.Append("<td>" + ObjTbl.Rows[i]["ItemCode"].ToString() + "</td>");
            strBiuld.Append("<td>" + ObjTbl.Rows[i]["EUPrice"].ToString() + "</td>");
            strBiuld.Append("<td>" + ObjTbl.Rows[i]["AgfaPrice"].ToString() + "</td>");
            strBiuld.Append("<td>" + ObjTbl.Rows[i]["ProfitEarned"].ToString() + "</td>");
            strBiuld.Append("<td>" + ObjTbl.Rows[i]["AgreedMargin"].ToString() + "</td>");
            strBiuld.Append("<td>" + ObjTbl.Rows[i]["Remarks"].ToString() + "</td>");
            strBiuld.Append("<td>" + ObjTbl.Rows[i]["ModifiedBy"].ToString() + "</td>");
            strBiuld.Append("<td>" + ObjTbl.Rows[i]["ModifiedOn"].ToString() + "</td>");
            strBiuld.Append("</tr>");


        }
        strBiuld.Append("</table>");

        //Response.Write(strBiuld.ToString());

        System.Web.HttpResponse response;
        response = System.Web.HttpContext.Current.Response;
        response.Clear();
        response.AddHeader("Content-Disposition", "attachment;filename=" + "ItemPriceList.xls");
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
