/* ==============================================================================================
    Created By      : Subahani SM
    Created Date    : 13 Aug 2010
    Purpose         :     
=================================================================================================
Change History :
=================================================================================================
S.No    Modified By        Modified Date        Description
1       
2
3 
================================================================================================= */
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
using System.Data.OleDb;
using System.IO;
using Controls;

public partial class Admin_UploadData : System.Web.UI.Page
{
    ExecutiveMasterClass objExecu = new ExecutiveMasterClass();
    BulkInsertClass objBulk = new BulkInsertClass();
    PriceListClass objPrice = new PriceListClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null) Response.Redirect("../Logout.aspx");
        try
        {
            if (!IsPostBack)
            {
                lblMessage.Text = "";
                lblMessage.CssClass = "";
                CommonFunction.PopulateRecordsWithTwoParam("Common_Values", "Text", "Value", "FieldName", "UploadType", "Status", "1", "Id", ddlUploadType);
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
            lblMessage.CssClass = "ErrorMessage";
        }
    }
    protected void btnDownload_Click(object sender, EventArgs e)
    {
            
        if (ddlUploadType.SelectedValue.ToLower() == "dealer")
        {
            ExpotToExcel exToExcel = new ExpotToExcel();
            DataSet dsExcel = new DataSet();
            DataRow drExcel;

            dsExcel.Tables.Add("Excel");
            dsExcel.Tables[0].Columns.Add("ExecutiveCode");
            dsExcel.Tables[0].Columns.Add("Region");
            dsExcel.Tables[0].Columns.Add("DMSCode");
            dsExcel.Tables[0].Columns.Add("DealerName");
            dsExcel.Tables[0].Columns.Add("Address1");
            dsExcel.Tables[0].Columns.Add("Address2");
            dsExcel.Tables[0].Columns.Add("Address3");
            dsExcel.Tables[0].Columns.Add("City");
            dsExcel.Tables[0].Columns.Add("State");
            dsExcel.Tables[0].Columns.Add("PinCode");
            dsExcel.Tables[0].Columns.Add("ContactPerson");
            dsExcel.Tables[0].Columns.Add("ContactNo");
            dsExcel.Tables[0].Columns.Add("EmailId");
            dsExcel.Tables[0].Columns.Add("FaxNo");
            dsExcel.Tables[0].Columns.Add("TINNo");
            dsExcel.Tables[0].Columns.Add("CSTNo");
            dsExcel.Tables[0].Columns.Add("LSTNo");
            dsExcel.Tables[0].Columns.Add("RespUser");

            drExcel = dsExcel.Tables[0].NewRow();
            foreach (DataColumn dtColumn in dsExcel.Tables[0].Columns)
            {
                drExcel[dtColumn.ColumnName] = "";
            }
            dsExcel.Tables[0].Rows.Add(drExcel);

            exToExcel.Dataview = dsExcel.Tables[0].DefaultView;

           
            Random objRam = new Random();
            exToExcel.FileNameToExport = "DealerLoadData" + objRam.Next() + ".xls";
            exToExcel.CustomExport();
            //myData = new OleDbDataAdapter(@"SELECT ExecutiveCode,Region,DMSCode,DealerName,Address1,Address2,Address3,City,State,Pincode,ContactPerson,ContactNo,EmailId,FaxNo,TINNo,CSTNo,LSTNo,RespUser FROM [" + DTable.Rows[0]["TABLE_NAME"].ToString() + "]", conn);
        }
        else if (ddlUploadType.SelectedValue.ToLower() == "customer")
        {
            ExpotToExcel exToExcel = new ExpotToExcel();
            DataSet dsExcel = new DataSet();
            DataRow drExcel;

            dsExcel.Tables.Add("Excel");
            dsExcel.Tables[0].Columns.Add("DealerCode");
            dsExcel.Tables[0].Columns.Add("CustName");
            dsExcel.Tables[0].Columns.Add("Address1");
            dsExcel.Tables[0].Columns.Add("Address2");
            dsExcel.Tables[0].Columns.Add("Address3");
            dsExcel.Tables[0].Columns.Add("City");
            dsExcel.Tables[0].Columns.Add("State");
            dsExcel.Tables[0].Columns.Add("PinCode");
            dsExcel.Tables[0].Columns.Add("ContactPerson");
            dsExcel.Tables[0].Columns.Add("ContactNo");
            dsExcel.Tables[0].Columns.Add("EmailId");
            dsExcel.Tables[0].Columns.Add("CSTNo");
            dsExcel.Tables[0].Columns.Add("LSTNo");
            dsExcel.Tables[0].Columns.Add("IsSpecialCust");
            dsExcel.Tables[0].Columns.Add("CustGroup");
            dsExcel.Tables[0].Columns.Add("Region");
            dsExcel.Tables[0].Columns.Add("CustType");

            drExcel = dsExcel.Tables[0].NewRow();
            foreach (DataColumn dtColumn in dsExcel.Tables[0].Columns)
            {
                drExcel[dtColumn.ColumnName] = "";
            }
            dsExcel.Tables[0].Rows.Add(drExcel);

            exToExcel.Dataview = dsExcel.Tables[0].DefaultView;


            Random objRam = new Random();
            exToExcel.FileNameToExport = "CustomerLoadData" + objRam.Next() + ".xls";
            exToExcel.CustomExport();
            //myData = new OleDbDataAdapter(@"SELECT DealerCode,CustName,Address1,Address2,Address3,City,State,Pincode,ContactPerson,ContactNo,EmailId,CSTNo,LSTNo,IsSpecialCust,CustGroup,Region,CustType FROM [" + DTable.Rows[0]["TABLE_NAME"].ToString() + "]", conn);
        }
        else if (ddlUploadType.SelectedValue.ToLower() == "dlrprice")
        {
            ExpotToExcel exToExcel = new ExpotToExcel();
            DataSet dsExcel = new DataSet();
            DataRow drExcel;

            dsExcel.Tables.Add("Excel");
            dsExcel.Tables[0].Columns.Add("DealerCode");
            dsExcel.Tables[0].Columns.Add("ItemCode");
            dsExcel.Tables[0].Columns.Add("DealerPrice");
            dsExcel.Tables[0].Columns.Add("PerSqrMt");
            dsExcel.Tables[0].Columns.Add("MaxQty");
            dsExcel.Tables[0].Columns.Add("Remarks");

            drExcel = dsExcel.Tables[0].NewRow();
            foreach (DataColumn dtColumn in dsExcel.Tables[0].Columns)
            {
                drExcel[dtColumn.ColumnName] = "";
            }
            dsExcel.Tables[0].Rows.Add(drExcel);

            exToExcel.Dataview = dsExcel.Tables[0].DefaultView;


            Random objRam = new Random();
            exToExcel.FileNameToExport = "DealerPriceLoadData" + objRam.Next() + ".xls";
            exToExcel.CustomExport();

            //myData = new OleDbDataAdapter(@"SELECT DealerCode,ItemCode as Item_Id,DealerPrice as DlrPrice,PerSqrMt,MaxQty,Remarks FROM [" + DTable.Rows[0]["TABLE_NAME"].ToString() + "]", conn);
        }
        else if (ddlUploadType.SelectedValue.ToLower() == "custprice")
        {
            ExpotToExcel exToExcel = new ExpotToExcel();
            DataSet dsExcel = new DataSet();
            DataRow drExcel;

            dsExcel.Tables.Add("Excel");
            dsExcel.Tables[0].Columns.Add("CustCode");
            dsExcel.Tables[0].Columns.Add("ItemCode");
            dsExcel.Tables[0].Columns.Add("EUPrice");
            dsExcel.Tables[0].Columns.Add("EUPerSqrMt");
            dsExcel.Tables[0].Columns.Add("ProfitActual");
            dsExcel.Tables[0].Columns.Add("ProfitAgreed");
            dsExcel.Tables[0].Columns.Add("ValidUpto");
            dsExcel.Tables[0].Columns.Add("Remarks");

            drExcel = dsExcel.Tables[0].NewRow();
            foreach (DataColumn dtColumn in dsExcel.Tables[0].Columns)
            {
                drExcel[dtColumn.ColumnName] = "";
            }
            dsExcel.Tables[0].Rows.Add(drExcel);

            exToExcel.Dataview = dsExcel.Tables[0].DefaultView;


            Random objRam = new Random();
            exToExcel.FileNameToExport = "CustomerPriceLoadData" + objRam.Next() + ".xls";
            exToExcel.CustomExport();
            //myData = new OleDbDataAdapter(@"SELECT CustCode,ItemCode as ItemId,EUPrice,EUPerSqrMt,ProfitActual,ProfitAgreed,ValidUpto,Remarks FROM [" + DTable.Rows[0]["TABLE_NAME"].ToString() + "]", conn);
        }
        else
        {
            lblMessage.Text = "Please select one table option from the Dropdownlist.";
            lblMessage.CssClass = "ErrorMessage";
        }


    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        string strFilepath = "";
        string strError = "Record(s) Could Not Be Uploaded.<br/>";
        int intUploaded, IntFailed;
        intUploaded = IntFailed = 0;
        double trialdb = 0;
        DateTime datetime;
        try
        {
            strFilepath = Server.MapPath(ConfigurationManager.AppSettings["FilePath"].ToString());
            strFilepath = strFilepath + fuUploadFile.FileName;

            //FileInfo objFileInfo = new FileInfo(strFilepath);
            //if (objFileInfo.Exists)
            //    File.Delete(strFilepath);

            fuUploadFile.SaveAs(strFilepath);

            DataSet ds = new DataSet();
            string strConn = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strFilepath + @";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1""";
            OleDbConnection conn = new OleDbConnection(strConn);
            OleDbDataAdapter myData = new OleDbDataAdapter();
            conn.Open();
            DataTable DTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            if (ddlUploadType.SelectedValue.ToLower() == "dealer")
                myData = new OleDbDataAdapter(@"SELECT ExecutiveCode,Region,DMSCode,DealerName,Address1,Address2,Address3,City,State,Pincode,ContactPerson,ContactNo,EmailId,FaxNo,TINNo,CSTNo,LSTNo,RespUser FROM [" + DTable.Rows[0]["TABLE_NAME"].ToString() + "]", conn);
            else if (ddlUploadType.SelectedValue.ToLower() == "customer")
                myData = new OleDbDataAdapter(@"SELECT DealerCode,CustName,Address1,Address2,Address3,City,State,Pincode,ContactPerson,ContactNo,EmailId,CSTNo,LSTNo,IsSpecialCust,CustGroup,Region,CustType FROM [" + DTable.Rows[0]["TABLE_NAME"].ToString() + "]", conn);
            else if (ddlUploadType.SelectedValue.ToLower() == "dlrprice")
                myData = new OleDbDataAdapter(@"SELECT DealerCode,ItemCode as Item_Id,DealerPrice as DlrPrice,PerSqrMt,MaxQty,Remarks FROM [" + DTable.Rows[0]["TABLE_NAME"].ToString() + "]", conn);
            else if (ddlUploadType.SelectedValue.ToLower() == "custprice")
                myData = new OleDbDataAdapter(@"SELECT CustCode,ItemCode as ItemId,EUPrice,EUPerSqrMt,ProfitActual,ProfitAgreed,ValidUpto,Remarks FROM [" + DTable.Rows[0]["TABLE_NAME"].ToString() + "]", conn);

            myData.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dtData = new DataTable();
                dtData = ds.Tables[0].Clone();

                for (int icount = 0; icount < ds.Tables[0].Rows.Count; icount++)
                {
                    //Dealer Validation
                    if (ddlUploadType.SelectedValue.ToLower() == "dealer")
                    {
                        DataSet dsExcu = objExecu.FetchExecutiveMaster(" where ExecutiveCode='" + ds.Tables[0].Rows[icount]["ExecutiveCode"].ToString() + "'");
                        if (dsExcu == null || dsExcu.Tables[0].Rows.Count <= 0)
                        {
                            strError = strError + "<br/>Row " + (icount + 1).ToString() + ".Invalid Executive Code:" + ds.Tables[0].Rows[icount]["ExecutiveCode"].ToString();
                            IntFailed += 1;
                            continue;
                        }
                        //Region validation
                        DataSet dsRegion = CommonFunction.FetchRecordsWithThreeParam("Common_Values", "FieldName", "Region", "Status", "1", "Value", ds.Tables[0].Rows[icount]["Region"].ToString().Trim(), "Text");
                        if (dsRegion == null || dsRegion.Tables[0].Rows.Count <= 0)
                        {
                            strError = strError + "<br/>Row " + (icount + 1).ToString() + ".Invalid Region:" + ds.Tables[0].Rows[icount]["Region"].ToString().Trim();
                            IntFailed += 1;
                            continue;
                        }
                        //DMSCode Validation
                        if (ds.Tables[0].Rows[icount]["DMSCode"].ToString() == "")
                        {
                            strError = strError + "<br/>Row " + (icount + 1).ToString() + ".DMS Code Cannot Be Empty.";
                            IntFailed += 1;
                            continue;
                        }
                        if (ds.Tables[0].Rows[icount]["DMSCode"].ToString().Length > 20)
                        {
                            strError = strError + "<br/>Row " + (icount + 1).ToString() + ".DMS Code Cannot Be Greater Than 20 Characters.";
                            IntFailed += 1;
                            continue;
                        }
                        //DealerName Validation
                        if (ds.Tables[0].Rows[icount]["DealerName"].ToString() == "")
                        {
                            strError = strError + "<br/>Row " + (icount + 1).ToString() + ".Dealer Name Cannot Be Empty.";
                            IntFailed += 1;
                            continue;
                        }
                        if (ds.Tables[0].Rows[icount]["DealerName"].ToString().Length > 50)
                        {
                            strError = strError + "<br/>Row " + (icount + 1).ToString() + ".Dealer Name Cannot Be Greater Than 50 Characters.";
                            IntFailed += 1;
                            continue;
                        }
                        //EmailId Validation
                        if (ds.Tables[0].Rows[icount]["EmailId"].ToString() == "")
                        {
                            strError = strError + "<br/>Row " + (icount + 1).ToString() + ".Email Id Cannot Be Empty.";
                            IntFailed += 1;
                            continue;
                        }
                        //User Validation
                        DataSet dsUser = CommonFunction.FetchRecordsWithThreeParam("UserMaster", "UserType", "Agfa", "Active", "1", "UserId", ds.Tables[0].Rows[icount]["RespUser"].ToString().Trim(), "UserId");
                        if (dsUser == null || dsUser.Tables[0].Rows.Count <= 0)
                        {
                            strError = strError + "<br/>Row " + (icount + 1).ToString() + ".Invalid Resp.User:" + ds.Tables[0].Rows[icount]["RespUser"].ToString();
                            IntFailed += 1;
                            continue;
                        }

                    }
                    //Customer Validation
                    else if (ddlUploadType.SelectedValue.ToLower() == "customer")
                    {
                        //Dealer Validation
                        DataSet dsDlr = CommonFunction.FetchRecordsWithTwoParam("DealerMaster", "Status", "1", "DealerCode", ds.Tables[0].Rows[icount]["DealerCode"].ToString().Trim(), "DealerCode");
                        if (dsDlr == null || dsDlr.Tables[0].Rows.Count <= 0)
                        {
                            strError = strError + "<br/>Row " + (icount + 1).ToString() + ".Invalid Dealer Code:" + ds.Tables[0].Rows[icount]["DealerCode"].ToString();
                            IntFailed += 1;
                            continue;
                        }
                        // Customer Name
                        if (ds.Tables[0].Rows[icount]["CustName"].ToString() == "")
                        {
                            strError = strError + "<br/>Row " + (icount + 1).ToString() + ".CustName Cannot Be Empty.";
                            IntFailed += 1;
                            continue;
                        }
                        if (ds.Tables[0].Rows[icount]["CustName"].ToString().Length > 50)
                        {
                            strError = strError + "<br/>Row " + (icount + 1).ToString() + ".CustName Cannot Be Greater Than 50 Characters.";
                            IntFailed += 1;
                            continue;
                        }
                        //Contact No
                        if (ds.Tables[0].Rows[icount]["ContactNo"].ToString() == "")
                        {
                            strError = strError + "<br/>Row " + (icount + 1).ToString() + ".ContactNo Cannot Be Empty.";
                            IntFailed += 1;
                            continue;
                        }
                        if (ds.Tables[0].Rows[icount]["ContactNo"].ToString().Length > 15)
                        {
                            strError = strError + "<br/>Row " + (icount + 1).ToString() + ".ContactNo Cannot Be Greater Than 15 Characters.";
                            IntFailed += 1;
                            continue;
                        }
                        //Cust Group
                        if (ds.Tables[0].Rows[icount]["CustGroup"].ToString().Length > 20)
                        {
                            strError = strError + "<br/>Row " + (icount + 1).ToString() + ".CustGroup Cannot Be Greater Than 20 Characters.";
                            IntFailed += 1;
                            continue;
                        }
                        //Region validation
                        DataSet dsRegion = CommonFunction.FetchRecordsWithThreeParam("Common_Values", "FieldName", "Region", "Status", "1", "Value", ds.Tables[0].Rows[icount]["Region"].ToString().Trim(), "Text");
                        if (dsRegion == null || dsRegion.Tables[0].Rows.Count <= 0)
                        {
                            strError = strError + "<br/>Row " + (icount + 1).ToString() + ".Invalid Region:" + ds.Tables[0].Rows[icount]["Region"].ToString().Trim();
                            IntFailed += 1;
                            continue;
                        }
                        //Customer Type
                        DataSet dsCustType = CommonFunction.FetchRecordsWithThreeParam("Common_Values", "FieldName", "CustType", "Status", "1", "Value", ds.Tables[0].Rows[icount]["CustType"].ToString().Trim(), "Text");
                        if (dsCustType == null || dsCustType.Tables[0].Rows.Count <= 0)
                        {
                            strError = strError + "<br/>Row " + (icount + 1).ToString() + ".Invalid CustGroup:" + ds.Tables[0].Rows[icount]["CustType"].ToString().Trim();
                            IntFailed += 1;
                            continue;
                        }
                    }
                    else if (ddlUploadType.SelectedValue.ToLower() == "dlrprice")
                    {
                        //Dealer Validation
                        DataSet dsDlr = CommonFunction.FetchRecordsWithTwoParam("DealerMaster", "Status", "1", "DealerCode", ds.Tables[0].Rows[icount]["DealerCode"].ToString().Trim(), "DealerCode");
                        if (dsDlr == null || dsDlr.Tables[0].Rows.Count <= 0)
                        {
                            strError = strError + "<br/>Row " + (icount + 1).ToString() + ".Invalid Dealer Code:" + ds.Tables[0].Rows[icount]["DealerCode"].ToString();
                            IntFailed += 1;
                            continue;
                        }
                        //Item Code Validation
                        DataSet dsItem = CommonFunction.FetchRecordsWithTwoParam("ItemMaster", "Status", "1", "ItemCode", ds.Tables[0].Rows[icount]["Item_Id"].ToString().Trim(), "ItemCode");
                        if (dsDlr == null || dsDlr.Tables[0].Rows.Count <= 0)
                        {
                            strError = strError + "<br/>Row " + (icount + 1).ToString() + ".Invalid Item Code:" + ds.Tables[0].Rows[icount]["Item_Id"].ToString();
                            IntFailed += 1;
                            continue;
                        }
                        else
                            ds.Tables[0].Rows[icount]["Item_Id"] = dsItem.Tables[0].Rows[0]["Id"];
                        //DealerPrice
                        if (ds.Tables[0].Rows[icount]["DlrPrice"].ToString() == "")
                        {
                            strError = strError + "<br/>Row " + (icount + 1).ToString() + ".DealerPrice Cannot Be Empty.";
                            IntFailed += 1;
                            continue;
                        }
                        if (!double.TryParse(ds.Tables[0].Rows[icount]["DlrPrice"].ToString(), out trialdb))
                        {
                            strError = strError + "<br/>Row " + (icount + 1).ToString() + ".Invalid DealerPrice.DealerPrice Accepts Only Numeric Data.";
                            IntFailed += 1;
                            continue;
                        }
                        //PerSqrMt
                        if (ds.Tables[0].Rows[icount]["PerSqrMt"].ToString() == "")
                        {
                            strError = strError + "<br/>Row " + (icount + 1).ToString() + ".PerSqrMt Cannot Be Empty.";
                            IntFailed += 1;
                            continue;
                        }
                        if (!double.TryParse(ds.Tables[0].Rows[icount]["PerSqrMt"].ToString(), out trialdb))
                        {
                            strError = strError + "<br/>Row " + (icount + 1).ToString() + ".Invalid PerSqrMt.PerSqrMt Accepts Only Numeric Data.";
                            IntFailed += 1;
                            continue;
                        }
                        //MaxQty
                        if (ds.Tables[0].Rows[icount]["MaxQty"].ToString() == "")
                        {
                            strError = strError + "<br/>Row " + (icount + 1).ToString() + ".MaxQty Cannot Be Empty.";
                            IntFailed += 1;
                            continue;
                        }
                        if (!double.TryParse(ds.Tables[0].Rows[icount]["MaxQty"].ToString(), out trialdb))
                        {
                            strError = strError + "<br/>Row " + (icount + 1).ToString() + ".Invalid MaxQty.MaxQty Accepts Only Numbers.";
                            IntFailed += 1;
                            continue;
                        }
                        //Remarks
                        if (ds.Tables[0].Rows[icount]["Remarks"].ToString().Length > 200)
                        {
                            strError = strError + "<br/>Row " + (icount + 1).ToString() + ".Remarks Cannot Be Greater Than 200 Characters.";
                            IntFailed += 1;
                            continue;
                        }
                        
                    }
                    else if (ddlUploadType.SelectedValue.ToLower() == "custprice")
                    {
                        //CustCode Validation
                        DataSet dsDlr = CommonFunction.FetchRecordsWithTwoParam("CustomerMaster", "Status", "1", "CustCode", ds.Tables[0].Rows[icount]["CustCode"].ToString().Trim(), "DealerCode");
                        if (dsDlr == null || dsDlr.Tables[0].Rows.Count <= 0)
                        {
                            strError = strError + "<br/>Row " + (icount + 1).ToString() + ".Invalid Cust Code:" + ds.Tables[0].Rows[icount]["CustCode"].ToString();
                            IntFailed += 1;
                            continue;
                        }
                        //Item Code Validation
                        DataSet dsItem = CommonFunction.FetchRecordsWithTwoParam("ItemMaster", "Status", "1", "ItemCode", ds.Tables[0].Rows[icount]["ItemId"].ToString().Trim(), "ItemCode");
                        if (dsDlr == null || dsDlr.Tables[0].Rows.Count <= 0)
                        {
                            strError = strError + "<br/>Row " + (icount + 1).ToString() + ".Invalid Dealer Code:" + ds.Tables[0].Rows[icount]["ItemId"].ToString();
                            IntFailed += 1;
                            continue;
                        }
                        else
                            ds.Tables[0].Rows[icount]["ItemId"] = dsItem.Tables[0].Rows[0]["Id"];
                        //EUPrice
                        if (ds.Tables[0].Rows[icount]["EUPrice"].ToString() == "")
                        {
                            strError = strError + "<br/>Row " + (icount + 1).ToString() + ".EUPrice Cannot Be Empty.";
                            IntFailed += 1;
                            continue;
                        }
                        if (!double.TryParse(ds.Tables[0].Rows[icount]["EUPrice"].ToString(), out trialdb))
                        {
                            strError = strError + "<br/>Row " + (icount + 1).ToString() + ".Invalid EUPrice.EUPrice Accepts Only Numeric Data.";
                            IntFailed += 1;
                            continue;
                        }
                        //EUPerSqrMt
                        if (ds.Tables[0].Rows[icount]["EUPerSqrMt"].ToString() == "")
                        {
                            strError = strError + "<br/>Row " + (icount + 1).ToString() + ".EUPerSqrMt Cannot Be Empty.";
                            IntFailed += 1;
                            continue;
                        }
                        if (!double.TryParse(ds.Tables[0].Rows[icount]["EUPerSqrMt"].ToString(), out trialdb))
                        {
                            strError = strError + "<br/>Row " + (icount + 1).ToString() + ".Invalid EUPerSqrMt.EUPerSqrMt Accepts Only Numeric Data.";
                            IntFailed += 1;
                            continue;
                        }
                        //ProfitActual
                        if (ds.Tables[0].Rows[icount]["ProfitActual"].ToString() == "")
                        {
                            strError = strError + "<br/>Row " + (icount + 1).ToString() + ".ProfitActual Cannot Be Empty.";
                            IntFailed += 1;
                            continue;
                        }
                        if (!double.TryParse(ds.Tables[0].Rows[icount]["ProfitActual"].ToString(), out trialdb))
                        {
                            strError = strError + "<br/>Row " + (icount + 1).ToString() + ".Invalid ProfitActual.ProfitActual Accepts Only Numeric Data.";
                            IntFailed += 1;
                            continue;
                        }
                        //ProfitAgreed
                        if (ds.Tables[0].Rows[icount]["ProfitAgreed"].ToString() == "")
                        {
                            strError = strError + "<br/>Row " + (icount + 1).ToString() + ".ProfitAgreed Cannot Be Empty.";
                            IntFailed += 1;
                            continue;
                        }
                        if (!double.TryParse(ds.Tables[0].Rows[icount]["ProfitAgreed"].ToString(), out trialdb))
                        {
                            strError = strError + "<br/>Row " + (icount + 1).ToString() + ".Invalid ProfitAgreed.ProfitAgreed Accepts Only Numeric Data.";
                            IntFailed += 1;
                            continue;
                        }
                        //Valid Upto
                        
                        if (ds.Tables[0].Rows[icount]["ValidUpto"].ToString() == "")
                        {
                            strError = strError + "<br/>Row " + (icount + 1).ToString() + ".ValidUpto Cannot be Empty.";
                            IntFailed += 1;
                            continue;
                        }
                        if (!DateTime.TryParse(ds.Tables[0].Rows[icount]["ValidUpto"].ToString(), out datetime))
                        {
                            strError = strError + "<br/>Row " + (icount + 1).ToString() + ".Invalid ValidUpto.Please Check The Date Format.";
                            IntFailed += 1;
                            continue;
                        }
                        //Remarks
                        if (ds.Tables[0].Rows[icount]["Remarks"].ToString().Length > 200)
                        {
                            strError = strError + "<br/>Row " + (icount + 1).ToString() + ".Remarks Cannot Be Greater Than 200 Characters.";
                            IntFailed += 1;
                            continue;
                        }
                    }

                    if (ddlUploadType.SelectedValue.ToLower() == "customer" || ddlUploadType.SelectedValue.ToLower() == "dealer")
                    {
                        //Address Validation
                        if (ds.Tables[0].Rows[icount]["Address1"].ToString() == "")
                        {
                            strError = strError + "<br/>Row " + (icount + 1).ToString() + ".Address1 Cannot Be Empty.";
                            IntFailed += 1;
                            continue;
                        }
                        if (ds.Tables[0].Rows[icount]["Address1"].ToString().Length > 25)
                        {
                            strError = strError + "<br/>Row " + (icount + 1).ToString() + ".Address1 Cannot Be Greater Than 25 Characters.";
                            IntFailed += 1;
                            continue;
                        }
                        if (ds.Tables[0].Rows[icount]["Address2"].ToString().Length > 25)
                        {
                            strError = strError + "<br/>Row " + (icount + 1).ToString() + ".Address2 Cannot Be Greater Than 25 Characters.";
                            IntFailed += 1;
                            continue;
                        }
                        if (ds.Tables[0].Rows[icount]["Address3"].ToString().Length > 25)
                        {
                            strError = strError + "<br/>Row " + (icount + 1).ToString() + ".Address3 Cannot Be Greater Than 25 Characters.";
                            IntFailed += 1;
                            continue;
                        }
                        //City
                        if (ds.Tables[0].Rows[icount]["City"].ToString().Length > 25)
                        {
                            strError = strError + "<br/>Row " + (icount + 1).ToString() + ".City Cannot Be Greater Than 25 Characters.";
                            IntFailed += 1;
                            continue;
                        }
                        //State Validation
                        DataSet dsState = CommonFunction.FetchRecordsWithTwoParam("StateMaster", "Active", "1", "StateCode", ds.Tables[0].Rows[icount]["State"].ToString().Trim(), "StateCode");
                        if (dsState == null || dsState.Tables[0].Rows.Count <= 0)
                        {
                            strError = strError + "<br/>Row " + (icount + 1).ToString() + ".Invalid State Code:" + ds.Tables[0].Rows[icount]["State"].ToString();
                            IntFailed += 1;
                            continue;
                        }
                    }

                    dtData.Rows.Add(ds.Tables[0].Rows[icount].ItemArray);
                    intUploaded += 1;

                }

                if (intUploaded > 0 && IntFailed<=0)
                {
                    //objBulk.prpUserId = Session["UserID"].ToString();
                    string strResult = "";

                    if (ddlUploadType.SelectedValue.ToLower() == "dealer")
                        strResult = objBulk.Insert("DealerMaster", dtData);
                    else if (ddlUploadType.SelectedValue.ToLower() == "customer")
                        strResult = objBulk.Insert("CustomerMaster", dtData);
                    else
                    {
                        if (ddlUploadType.SelectedValue.ToLower() == "dlrprice")
                        {
                            for (int i = 0; i < dtData.Rows.Count; i++)
                            {

                                objPrice.prpDealerCode = Convert.ToInt32(dtData.Rows[i]["DealerCode"]);
                                objPrice.prpItemId = Convert.ToInt32(dtData.Rows[i]["Item_Id"]);
                                objPrice.prpDlrPrice = Convert.ToDouble(dtData.Rows[i]["DlrPrice"]);
                                objPrice.prpPerSqrmt = Convert.ToDouble(dtData.Rows[i]["PerSqrMt"]);
                                objPrice.prpMaxQty = Convert.ToInt32(dtData.Rows[i]["MaxQty"]);
                                objPrice.prpRemarks = dtData.Rows[i]["Remarks"].ToString();
                                objPrice.prpUserId = Session["UserID"].ToString();

                                strResult = strResult + objPrice.SaveLoadPriceData("DealerPrice");
                            }
                        }
                            if (ddlUploadType.SelectedValue.ToLower() == "custprice")
                            {
                                for (int i = 0; i < dtData.Rows.Count; i++)
                                {

                                    objPrice.prpCustCode = Convert.ToInt32(dtData.Rows[i]["CustCode"]);
                                    objPrice.prpItemId = Convert.ToInt32(dtData.Rows[i]["ItemId"]);
                                    objPrice.prpEuPrice = Convert.ToDouble(dtData.Rows[i]["EuPrice"]);
                                    objPrice.prpEUPerSqrmt = Convert.ToDouble(dtData.Rows[i]["EUPerSqrMt"]);
                                    objPrice.prpProfitActual=Convert.ToDouble(dtData.Rows[i]["ProfitActual"]);
                                    objPrice.prpProfitAgreed=Convert.ToDouble(dtData.Rows[i]["ProfitAgreed"]);
                                    if (dtData.Rows[i]["ValidUpto"] != null && dtData.Rows[i]["ValidUpto"].ToString()!="")
                                        objPrice.prpValidUpto = Convert.ToDateTime(dtData.Rows[i]["ValidUpto"]);
                                    else
                                        objPrice.prpValidUpto = Convert.ToDateTime("01/01/1900");
                                    objPrice.prpRemarks = dtData.Rows[i]["Remarks"].ToString();
                                    objPrice.prpUserId = Session["UserID"].ToString();

                                    strResult = strResult + objPrice.SaveLoadPriceData("CustomerItemPrice");
                                }
                            }
                    
                    }

                    //else if (ddlUploadType.SelectedValue.ToLower() == "dlrprice")
                    //    strResult = objBulk.Insert("DealerPrice", dtData);
                    //else if (ddlUploadType.SelectedValue.ToLower() == "custprice")
                    //    strResult = objBulk.Insert("CustomerItemPrice", dtData);

                    if (strResult == "")
                    {
                        //lblMessage.Text = intUploaded.ToString() + " Record(s) Have Been Successfully Uploaded.";
                        lblMessage.Text = " Record(s) Have Been Successfully Uploaded.";
                        lblMessage.CssClass = "SuccessMessageBold";
                    }
                    else
                    {
                        lblMessage.Text = strResult;
                        lblMessage.CssClass = "ErrorMessage";
                    }
                }

                if (IntFailed > 0)
                {
                    lblFailed.Text = strError;
                    lblFailed.CssClass = "ErrorMessage";
                }

            }
            else
            {
                lblMessage.Text = "Please Enter Data In The Selected Excel File.";
                lblMessage.CssClass = "ErrorMessage";
            }

            //Delete the Uploaded File
            //FileInfo objFileInfo = new FileInfo(strFilepath);
            //if (objFileInfo.Exists)
            //    File.Delete(strFilepath);

        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
            lblMessage.CssClass = "ErrorMessage";
        }
    }
}
