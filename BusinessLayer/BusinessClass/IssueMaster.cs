/* ==============================================================================================
    Created By      : Subahani SM
    Created Date    : 01 Sep 2010
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Common;

namespace BusinessClass
{
    public class IssueMaster
    {
        /* declare Class level Variables */
        private string strUserId, strMessage;
        private string strStatus,strSalesType, strInvoiceNo, strMonth, strRemarks, strFileName, strRegion;
        private Int32 intDealerCode, intCustCode, intYear, intItemId, intQty, intIssueNo, intPrevQty, intPGId;
        private double dbSalesTax, dbLocalLevi, dbInsurance, dbOthers, dbNetInvoiceAmt, dbGrossInvoiceAmt, dbUnitPrice,dbProfitAgreed;
        private DateTime dtInvoicedate;
        private bool blnDirectCust;
        /* Define Properties */
        public string prpUserId
        {
            get
            {
                return strUserId;
            }
            set
            {
                strUserId = value;
            }
        }

        public string prpRegion
        {
            get => this.strRegion;
            set => this.strRegion = value;
        }
        public bool prpdirectcust
        {
            get => this.blnDirectCust;
            set => this.blnDirectCust = value;
        }

        public string prpSalesType
        {
            get
            {
                return strSalesType;
            }
            set
            {
                strSalesType = value;
            }
        }

        public string prpInvoiceNo
        {
            get
            {
                return strInvoiceNo;
            }
            set
            {
                strInvoiceNo = value;
            }
        }

        public string prpMonth
        {
            get
            {
                return strMonth;
            }
            set
            {
                strMonth = value;
            }
        }

        public string prpRemarks
        {
            get
            {
                return strRemarks;
            }
            set
            {
                strRemarks = value;
            }
        }

        public string prpFileName
        {
            get => this.strFileName;
            set => this.strFileName = value;
        }

        public string prpStatus
        {
            get => this.strStatus;
            set => this.strStatus = value;
        }

        public Int32 prpDealerCode
        {
            get
            {
                return intDealerCode;
            }
            set
            {
                intDealerCode = value;
            }
        }

        public int prpid
        {
            get => this.intPGId;
            set => this.intPGId = value;
        }

        public Int32 prpCustCode
        {
            get
            {
                return intCustCode;
            }
            set
            {
                intCustCode = value;
            }
        }

        public Int32 prpYear
        {
            get
            {
                return intYear;
            }
            set
            {
                intYear = value;
            }
        }

        public Int32 prpItemId
        {
            get
            {
                return intItemId;
            }
            set
            {
                intItemId = value;
            }
        }

        public Int32 prpQty
        {
            get
            {
                return intQty;
            }
            set
            {
                intQty = value;
            }
        }

        public Int32 prpPrevQty
        {
            get
            {
                return intPrevQty;
            }
            set
            {
                intPrevQty = value;
            }
        }

        public Int32 prpIssueNo
        {
            get
            {
                return intIssueNo;
            }
            set
            {
                intIssueNo = value;
            }
        }

        public double prpSalesTax
        {
            get
            {
                return dbSalesTax;
            }
            set
            {
                dbSalesTax = value;
            }
        }

        public double prpLocalLevi
        {
            get
            {
                return dbLocalLevi;
            }
            set
            {
                dbLocalLevi = value;
            }
        }

        public double prpInsurance
        {
            get
            {
                return dbInsurance;
            }
            set
            {
                dbInsurance = value;
            }
        }

        public double prpOthers
        {
            get
            {
                return dbOthers;
            }
            set
            {
                dbOthers = value;
            }
        }

        public double prpNetInvoiceAmt
        {
            get
            {
                return dbNetInvoiceAmt;
            }
            set
            {
                dbNetInvoiceAmt = value;
            }
        }

        public double prpGrossInvoiceAmt
        {
            get
            {
                return dbGrossInvoiceAmt;
            }
            set
            {
                dbGrossInvoiceAmt = value;
            }
        }

        public double prpUnitPrice
        {
            get
            {
                return dbUnitPrice;
            }
            set
            {
                dbUnitPrice = value;
            }
        }
        public double prpProfitAgreed
        {
            get
            {
                return dbProfitAgreed;
            }
            set
            {
                dbProfitAgreed = value;
            }
        }

        public DateTime prpInvoiceDate
        {
            get
            {
                return dtInvoicedate;
            }
            set
            {
                dtInvoicedate = value;
            }
        }

        /* Define Methods & Functions */
        public DataSet FetchIssueList()
        {

            DataSet dsIssue = new DataSet();
            SqlConnection SqlConn = DBConnection.OpenConnection();
            SqlDataAdapter sdaIssue = new SqlDataAdapter("prcFetchIssueList", SqlConn);
            try
            {
                sdaIssue.SelectCommand.CommandType = CommandType.StoredProcedure;
                sdaIssue.SelectCommand.Parameters.Add("@DealerCode", SqlDbType.BigInt).Value = intDealerCode;
                sdaIssue.SelectCommand.Parameters.Add("@Region", SqlDbType.VarChar, 10).Value = (object)this.strRegion;
                sdaIssue.SelectCommand.Parameters.Add("@CustCode", SqlDbType.Int).Value = (object)this.intCustCode;
                dsIssue.Clear();
                sdaIssue.Fill(dsIssue);
            }
            catch (Exception)
            {
                if (SqlConn.State == ConnectionState.Open) SqlConn.Close();
            }
            return dsIssue;

        }
        public DataSet FetchApproverIssueList()
        {

            DataSet dsIssue = new DataSet();
            SqlConnection SqlConn = DBConnection.OpenConnection();
            SqlDataAdapter sdaIssue = new SqlDataAdapter("prcFetchApproverIssueList", SqlConn);
            try
            {
                sdaIssue.SelectCommand.CommandType = CommandType.StoredProcedure;
                //sdaIssue.SelectCommand.Parameters.Add("@DealerCode", SqlDbType.BigInt).Value = intDealerCode;
                sdaIssue.SelectCommand.Parameters.Add("@Region", SqlDbType.VarChar, 10).Value = (object)this.strRegion;
                sdaIssue.SelectCommand.Parameters.Add("@CustCode", SqlDbType.Int).Value = (object)this.intCustCode;
                dsIssue.Clear();
                sdaIssue.Fill(dsIssue);
            }
            catch (Exception)
            {
                if (SqlConn.State == ConnectionState.Open) SqlConn.Close();
            }
            return dsIssue;

        }
        public DataSet FetchIssue()
        {

            DataSet dsIssue = new DataSet();
            SqlConnection SqlConn = DBConnection.OpenConnection();
            SqlDataAdapter sdaIssue = new SqlDataAdapter("prcFetchIssue", SqlConn);
            try
            {
                sdaIssue.SelectCommand.CommandType = CommandType.StoredProcedure;
                sdaIssue.SelectCommand.Parameters.Add("@IssueNo", SqlDbType.BigInt).Value = intIssueNo;
                dsIssue.Clear();
                sdaIssue.Fill(dsIssue);
            }
            catch (Exception)
            {
                if (SqlConn.State == ConnectionState.Open) SqlConn.Close();
            }
            return dsIssue;

        }
        public string SaveIssue()
        {
            strMessage = "";
            SqlCommand cmd = new SqlCommand("prcInsertUpdateIssue", DBConnection.OpenConnection());
            try
            {
                cmd.Parameters.Add("@IssueNo", SqlDbType.BigInt).Value = intIssueNo;
                cmd.Parameters.Add("@DealerCode", SqlDbType.BigInt).Value = intDealerCode;
                cmd.Parameters.Add("@CustCode", SqlDbType.BigInt).Value = intCustCode;
                cmd.Parameters.Add("@SalesType", SqlDbType.VarChar, 20).Value = strSalesType;
                cmd.Parameters.Add("@InvoiceNo", SqlDbType.VarChar, 20).Value = strInvoiceNo;
                cmd.Parameters.Add("@InvoiceDate", SqlDbType.DateTime).Value = dtInvoicedate;
                cmd.Parameters.Add("@Month", SqlDbType.VarChar, 15).Value = strMonth;
                cmd.Parameters.Add("@Year", SqlDbType.Int).Value = intYear;
                cmd.Parameters.Add("@SalesTax", SqlDbType.Decimal).Value = dbSalesTax;
                cmd.Parameters.Add("@LocalLevi", SqlDbType.Decimal).Value = dbLocalLevi;
                cmd.Parameters.Add("@Insurance", SqlDbType.Decimal).Value = dbInsurance;
                cmd.Parameters.Add("@Others", SqlDbType.Decimal).Value = dbOthers;
                cmd.Parameters.Add("@NetInvoiceAmt", SqlDbType.Decimal).Value = dbNetInvoiceAmt;
                cmd.Parameters.Add("@GrossInvoiceAmt", SqlDbType.Decimal).Value = dbGrossInvoiceAmt;
                cmd.Parameters.Add("@Remarks", SqlDbType.VarChar, 200).Value = strRemarks;
                cmd.Parameters.Add("@FileName", SqlDbType.VarChar, 200).Value = (object)this.strFileName;
                if(this.strFileName != "" && this.strFileName.Length > 0)
                {
                    cmd.Parameters.Add("@Stattus", SqlDbType.VarChar, 200).Value = (object)"Unverified";

                }
                else
                {
                    cmd.Parameters.Add("@Stattus", SqlDbType.VarChar, 200).Value = (object)"Verified";
                }
                cmd.Parameters.Add("@UserId", SqlDbType.VarChar, 20).Value = strUserId;
                cmd.Parameters.Add("@Issue", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@Msg", SqlDbType.VarChar, 20).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                strMessage = cmd.Parameters["@Msg"].Value.ToString();
                if (strMessage == "")
                    intIssueNo = Convert.ToInt32(cmd.Parameters["@Issue"].Value.ToString());
                else
                    intIssueNo = 0;
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                if (cmd.Connection.State == ConnectionState.Open) cmd.Connection.Close();
                strMessage = ex.Message;

            }
            return strMessage;

        }

        public string SaveIssueDetails()
        {
            strMessage = "";
            SqlCommand cmd = new SqlCommand("prcInsertUpdateIssueDetails", DBConnection.OpenConnection());
            try
            {
                cmd.Parameters.Add("@IssueNo", SqlDbType.BigInt).Value = intIssueNo;
                cmd.Parameters.Add("@ItemId", SqlDbType.Int).Value = intItemId;
                cmd.Parameters.Add("@Qty", SqlDbType.Int).Value = intQty;
                cmd.Parameters.Add("@PrevQty", SqlDbType.Int).Value = intPrevQty;
                cmd.Parameters.Add("@UnitPrice", SqlDbType.Decimal).Value = dbUnitPrice;
                cmd.Parameters.Add("@ProfitAgreed", SqlDbType.Decimal).Value = dbProfitAgreed;
                cmd.Parameters.Add("@UserId", SqlDbType.VarChar, 20).Value = strUserId;
                cmd.Parameters.Add("@Msg", SqlDbType.VarChar, 200).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                strMessage = cmd.Parameters["@Msg"].Value.ToString();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                if (cmd.Connection.State == ConnectionState.Open) cmd.Connection.Close();
                strMessage = ex.Message;

            }
            return strMessage;

        }

        public string DeleteIssue()
        {
            strMessage = "";
            SqlCommand cmd = new SqlCommand("prcDeleteIssue", DBConnection.OpenConnection());
            try
            {
                cmd.Parameters.Add("@IssueNo", SqlDbType.BigInt).Value = intIssueNo;
                cmd.Parameters.Add("@Msg", SqlDbType.VarChar, 20).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                strMessage = cmd.Parameters["@Msg"].Value.ToString();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                if (cmd.Connection.State == ConnectionState.Open) cmd.Connection.Close();
                strMessage = ex.Message;
            }
            return strMessage;
        }


        public string DeleteIssueDetails()
        {
            strMessage = "";
            SqlCommand cmd = new SqlCommand("prcDeleteIssueDetails", DBConnection.OpenConnection());

            try
            {
                cmd.Parameters.Add("@IssueNo", SqlDbType.BigInt).Value = intIssueNo;
                cmd.Parameters.Add("@ItemId", SqlDbType.BigInt).Value = intItemId;
                cmd.Parameters.Add("@Msg", SqlDbType.VarChar, 20).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                strMessage = cmd.Parameters["@Msg"].Value.ToString();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                if (cmd.Connection.State == ConnectionState.Open) cmd.Connection.Close();
                strMessage = ex.Message;
            }
            return strMessage;
        }

        public string IssueFileValidate1()
        {
            this.strMessage = "";
            SqlCommand sqlCommand = new SqlCommand("prcInsertUpdateIssueDetails", DBConnection.OpenConnection());
            try
            {
                sqlCommand.Parameters.Add("@IssueNo", SqlDbType.BigInt).Value = (object)this.intIssueNo;
                sqlCommand.Parameters.Add("@ItemId", SqlDbType.Int).Value = (object)this.intItemId;
                sqlCommand.Parameters.Add("@Qty", SqlDbType.Int).Value = (object)this.intQty;
                sqlCommand.Parameters.Add("@PrevQty", SqlDbType.Int).Value = (object)this.intPrevQty;
                sqlCommand.Parameters.Add("@UnitPrice", SqlDbType.Decimal).Value = (object)this.dbUnitPrice;
                sqlCommand.Parameters.Add("@ProfitAgreed", SqlDbType.Decimal).Value = (object)this.dbProfitAgreed;
                sqlCommand.Parameters.Add("@UserId", SqlDbType.VarChar, 20).Value = (object)this.strUserId;
                sqlCommand.Parameters.Add("@Msg", SqlDbType.VarChar, 200).Direction = ParameterDirection.Output;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection.Open();
                sqlCommand.ExecuteNonQuery();
                this.strMessage = sqlCommand.Parameters["@Msg"].Value.ToString();
                sqlCommand.Connection.Close();
            }
            catch (Exception ex)
            {
                if (sqlCommand.Connection.State == ConnectionState.Open)
                    sqlCommand.Connection.Close();
                this.strMessage = ex.Message;
            }
            return this.strMessage;
        }

        public DataSet IssueFileValidate()
        {
            DataSet dataSet = new DataSet();
            SqlConnection selectConnection = DBConnection.OpenConnection();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("getcustdetailsnew", selectConnection);
            try
            {
                sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDataAdapter.SelectCommand.Parameters.Add("@DirectCustomer", SqlDbType.Bit).Value = (object)this.blnDirectCust;
                sqlDataAdapter.SelectCommand.Parameters.Add("@CustCode", SqlDbType.BigInt).Value = (object)this.intCustCode;
                dataSet.Clear();
                sqlDataAdapter.Fill(dataSet);
            }
            catch (Exception ex)
            {
                if (selectConnection.State == ConnectionState.Open)
                    selectConnection.Close();
            }
            return dataSet;
        }

        public string SaveIssueVerify()
        {
            this.strMessage = "";
            SqlCommand sqlCommand = new SqlCommand("prcSaveIssueStatus", DBConnection.OpenConnection());
            try
            {
                sqlCommand.Parameters.Add("@IssueNo", SqlDbType.Int).Value = (object)this.intIssueNo;
                sqlCommand.Parameters.Add("@Status", SqlDbType.VarChar, 20).Value = (object)this.strStatus;
                sqlCommand.Parameters.Add("@Msg", SqlDbType.VarChar, 20).Direction = ParameterDirection.Output;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection.Open();
                sqlCommand.ExecuteNonQuery();
                this.strMessage = sqlCommand.Parameters["@Msg"].Value.ToString();
                sqlCommand.Connection.Close();
            }
            catch (Exception ex)
            {
                if (sqlCommand.Connection.State == ConnectionState.Open)
                    sqlCommand.Connection.Close();
                this.strMessage = ex.Message;
            }
            return this.strMessage;
        }

    }
}
