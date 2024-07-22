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


public partial class MonthEndRpt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       // reportdocument objectreport = new reportdocument();
        //string s = configurationsettings.appsettings["reportpath"] + "\\monthend.rpt";
       // objectreport.load(s);
        //objectreport.refresh();
        //objectreport = setparameter(objectreport);
        //crystalreportviewer1.reportsource = objectreport;
    }

    //protected ReportDocument SetParameter(ReportDocument _ReportObj)
    //{
    //    //Applying DataBase Settings
    //    TableLogOnInfo _Login;
    //    ReportDocument subreport;

    //    string[] connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString().Split(';');
    //    foreach (CrystalDecisions.CrystalReports.Engine.Table _Table in _ReportObj.Database.Tables)
    //    {
    //        _Login = _Table.LogOnInfo;
    //        _Login.ConnectionInfo.DatabaseName = connectionString[1].Replace("database=", "");
    //        _Login.ConnectionInfo.ServerName = connectionString[0].Replace("server=", "");
    //        _Login.ConnectionInfo.UserID = connectionString[2].Replace("uid=", "");
    //        _Login.ConnectionInfo.Password = connectionString[3].Replace("pwd=", "");
    //        _Table.ApplyLogOnInfo(_Login);
    //        _Table.Location = _Login.ConnectionInfo.DatabaseName + ".dbo." + _Table.Name;
    //    }

    //    ParameterDiscreteValue _ParameterDiscreteValue;
    //    ParameterFieldDefinitions _ParameterFieldDefinitions;
    //    ParameterFieldDefinition _ParameterFieldLocation;
    //    ParameterValues _ParameterValues;

    //    _ParameterFieldDefinitions = _ReportObj.DataDefinition.ParameterFields;

    //    string Dealercode = "0";
    //    //if (Session["DealerCode"].ToString() != "")
    //    //    Dealercode = Session["DealerCode"].ToString();



    //    _ParameterFieldLocation = _ParameterFieldDefinitions["@DealerCode"];
    //    _ParameterValues = _ParameterFieldLocation.CurrentValues;
    //    _ParameterDiscreteValue = new CrystalDecisions.Shared.ParameterDiscreteValue();
    //    _ParameterDiscreteValue.Value = Dealercode;
    //    _ParameterValues.Add(_ParameterDiscreteValue);
    //    _ParameterFieldLocation.ApplyCurrentValues(_ParameterValues);

    //    return _ReportObj;

    //}
}
