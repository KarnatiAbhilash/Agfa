/* ==============================================================================================
    Created By      : Subahani SM
    Created Date    : 04 Sep 2010
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
    public class StockReturnClass
    {
        /* declare Class level Variables */
        private string strUserId, strMessage;
        private string strUserType, strSalesType, strInvoiceNo, strMonth, strReason,strStatus;
        private Int32 intDealerCode, intCustCode, intYear, intReturnId, intItemId, intReturnQty, intIssueNo;
        private DateTime dtReturnDate;

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

        public string prpUserType
        {
            get
            {
                return strUserType;
            }
            set
            {
                strUserType = value;
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

        public string prpReason
        {
            get
            {
                return strReason;
            }
            set
            {
                strReason = value;
            }
        }

        public string prpStatus
        {
            get
            {
                return strStatus;
            }
            set
            {
                strStatus = value;
            }
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

        public Int32 prpReturnId
        {
            get
            {
                return intReturnId;
            }
            set
            {
                intReturnId = value;
            }
        }

        public int prpIssueNo
        {
            get => this.intIssueNo;
            set => this.intIssueNo = value;
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

        public Int32 prpReturnQty
        {
            get
            {
                return intReturnQty;
            }
            set
            {
                intReturnQty = value;
            }
        }

        public DateTime prpReturnDate
        {
            get
            {
                return dtReturnDate;
            }
            set
            {
                dtReturnDate = value;
            }
        }

        /* Define Methods & Functions */

        public string SaveStockReturn()
        {
            strMessage = "";
            SqlCommand cmd = new SqlCommand("prcSaveStockReturn", DBConnection.OpenConnection());
            try
            {
                cmd.Parameters.Add("@UserType", SqlDbType.VarChar, 20).Value = strUserType;
                cmd.Parameters.Add("@DealerCode", SqlDbType.BigInt).Value = intDealerCode;
                cmd.Parameters.Add("@CustCode", SqlDbType.BigInt).Value = intCustCode;
                cmd.Parameters.Add("@SalesType", SqlDbType.VarChar, 20).Value = strSalesType;
                cmd.Parameters.Add("@InvoiceNo", SqlDbType.VarChar, 20).Value = strInvoiceNo;
                cmd.Parameters.Add("@Month", SqlDbType.VarChar, 20).Value = strMonth;
                cmd.Parameters.Add("@Year", SqlDbType.Int).Value = intYear;
                cmd.Parameters.Add("@ReturnDate", SqlDbType.DateTime).Value = dtReturnDate;
                cmd.Parameters.Add("@UserId", SqlDbType.VarChar, 20).Value = strUserId;
                cmd.Parameters.Add("@Msg", SqlDbType.VarChar, 20).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@RetId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                strMessage = cmd.Parameters["@Msg"].Value.ToString();
                intReturnId = Convert.ToInt32(cmd.Parameters["@RetId"].Value.ToString());
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                if (cmd.Connection.State == ConnectionState.Open) cmd.Connection.Close();
                strMessage = ex.Message;
            }
            return strMessage;
        }

        public string SaveStockReturnDetails()
        {
            strMessage = "";
            SqlCommand cmd = new SqlCommand("prcSaveStockRetunDetails", DBConnection.OpenConnection());
            try
            {
                cmd.Parameters.Add("@ReturnId", SqlDbType.Int).Value = intReturnId;
                cmd.Parameters.Add("@ItemId", SqlDbType.Int).Value = intItemId;
                cmd.Parameters.Add("@ReturnQty", SqlDbType.Int).Value = intReturnQty;
                cmd.Parameters.Add("@Reason", SqlDbType.VarChar, 200).Value = strReason;
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

        public DataSet FetchStockReturnList()
        {
            DataSet dsStock = new DataSet();
            SqlConnection SqlConn = DBConnection.OpenConnection();
            SqlDataAdapter sdaStock = new SqlDataAdapter("prcFetchStockReturnList", SqlConn);
            try
            {
                sdaStock.SelectCommand.CommandType = CommandType.StoredProcedure;                
                dsStock.Clear();
                sdaStock.Fill(dsStock);
            }
            catch (Exception)
            {
                if (SqlConn.State == ConnectionState.Open) SqlConn.Close();
            }
            return dsStock;
        }

        public string SaveStockReturnStatus()
        {
            strMessage = "";
            SqlCommand cmd = new SqlCommand("prcSaveStockReturnStatus", DBConnection.OpenConnection());
            try
            {
                cmd.Parameters.Add("@ReturnId", SqlDbType.Int).Value = intReturnId;
                cmd.Parameters.Add("@ItemId", SqlDbType.Int).Value = intItemId;
                cmd.Parameters.Add("@Status", SqlDbType.VarChar, 20).Value = strStatus;
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

        public string ValidateStockReturn()
        {
            strMessage = "";
            SqlCommand cmd = new SqlCommand("[prcFetchStockReturnWithItem]", DBConnection.OpenConnection());
            try
            {
                cmd.Parameters.Add("@DealerCode", SqlDbType.BigInt).Value = intDealerCode;
                cmd.Parameters.Add("@Itme_Id", SqlDbType.BigInt).Value = intItemId;
                cmd.Parameters.Add("@ReturnQty", SqlDbType.Int).Value = intReturnQty;
                cmd.Parameters.Add("@Month", SqlDbType.VarChar, 20).Value = strMonth;
                cmd.Parameters.Add("@Year", SqlDbType.Int).Value = intYear;
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

    }
}
