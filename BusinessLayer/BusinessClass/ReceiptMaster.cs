/* ==============================================================================================
    Created By      : Subahani SM
    Created Date    : 25 Aug 2010
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
    public class ReceiptMaster
    {
        /* declare Class level Variables */
        private string strUserId, strMessage;
        private string strPONo, strInvoiceNo, strMonth,strRemarks;
        private Int32 intDealerCode, intYear, intItemId, intQty, intReceiptNo, intPrevQty;
        private double dbSalesTax, dbLocalLevi, dbInsurance, dbOthers, dbNetInvoiceAmt, dbGrossInvoiceAmt, dbUnitPrice;
        private DateTime dtInvoicedate;

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

        public string prpPONo
        {
            get
            {
                return strPONo;
            }
            set
            {
                strPONo = value;
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

        public Int32 prpReceiptNo
        {
            get
            {
                return intReceiptNo;
            }
            set
            {
                intReceiptNo = value;
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
        public DataSet FetchPO()
        {

            DataSet dsPO = new DataSet();
            SqlConnection SqlConn = DBConnection.OpenConnection();
            SqlDataAdapter sdaPO = new SqlDataAdapter("prcFetchPOForOpenMonth", SqlConn);
            try
            {
                sdaPO.SelectCommand.CommandType = CommandType.StoredProcedure;
                sdaPO.SelectCommand.Parameters.Add("@DealerCode", SqlDbType.BigInt).Value = intDealerCode;
                dsPO.Clear();
                sdaPO.Fill(dsPO);
            }
            catch (Exception)
            {
                if (SqlConn.State == ConnectionState.Open) SqlConn.Close();
            }
            return dsPO;
        }
        public DataSet FetchReceipt()
        {

            DataSet dsReceipt = new DataSet();
            SqlConnection SqlConn = DBConnection.OpenConnection();
            SqlDataAdapter sdaReceipt = new SqlDataAdapter("prcFetchReceipt", SqlConn);
            try
            {
                sdaReceipt.SelectCommand.CommandType = CommandType.StoredProcedure;
                sdaReceipt.SelectCommand.Parameters.Add("@ReceiptNo", SqlDbType.BigInt).Value = intReceiptNo;
                dsReceipt.Clear();
                sdaReceipt.Fill(dsReceipt);
            }
            catch (Exception)
            {
                if (SqlConn.State == ConnectionState.Open) SqlConn.Close();
            }
            return dsReceipt;

        }

        public string DeletePartialPO(string strItemCode)
        {
            strMessage = "";
            SqlCommand cmd = new SqlCommand("prcDeletePartialPO", DBConnection.OpenConnection());
            try
            {
                cmd.Parameters.Add("@PONo", SqlDbType.VarChar, 20).Value = strPONo;
                cmd.Parameters.Add("@ItemCode", SqlDbType.VarChar, 20).Value = strItemCode;
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
        public string SaveReceipt()
        {
            strMessage = "";
            SqlCommand cmd = new SqlCommand("prcInsertUpdateReceipt", DBConnection.OpenConnection());
            try
            {
                cmd.Parameters.Add("@ReceiptNo", SqlDbType.BigInt).Value = intReceiptNo;
                cmd.Parameters.Add("@PONo", SqlDbType.VarChar, 20).Value = strPONo;
                cmd.Parameters.Add("@DealerCode", SqlDbType.BigInt).Value = intDealerCode;
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
                cmd.Parameters.Add("@UserId", SqlDbType.VarChar, 20).Value = strUserId;
                cmd.Parameters.Add("@Receipt", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@Msg", SqlDbType.VarChar, 200).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                strMessage = cmd.Parameters["@Msg"].Value.ToString();
                if (strMessage == "")
                    intReceiptNo = Convert.ToInt32(cmd.Parameters["@Receipt"].Value.ToString());
                else
                    intReceiptNo = 0;
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                if (cmd.Connection.State == ConnectionState.Open) cmd.Connection.Close();
                strMessage = ex.Message;

            }
            return strMessage;
        }

        public string SaveReceiptDetails()
        {
            strMessage = "";
            SqlCommand cmd = new SqlCommand("prcInsertUpdateReceiptDetails", DBConnection.OpenConnection());
            try
            {
                cmd.Parameters.Add("@ReceiptNo", SqlDbType.BigInt).Value = intReceiptNo;
                cmd.Parameters.Add("@ItemId", SqlDbType.Int).Value = intItemId;
                cmd.Parameters.Add("@Qty", SqlDbType.Int).Value = intQty;
                cmd.Parameters.Add("@PrevQty", SqlDbType.Int).Value = intPrevQty;
                cmd.Parameters.Add("@UnitPrice", SqlDbType.Decimal).Value = dbUnitPrice;
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

        public DataSet FetchReceiptPO()
        {

            DataSet dsReceipt = new DataSet();
            SqlConnection SqlConn = DBConnection.OpenConnection();
            SqlDataAdapter sdaReceipt = new SqlDataAdapter("prcFetchReceipt", SqlConn);
            try
            {
                sdaReceipt.SelectCommand.CommandType = CommandType.StoredProcedure;
                sdaReceipt.SelectCommand.Parameters.Add("@ReceiptNo", SqlDbType.BigInt).Value = intReceiptNo;
                sdaReceipt.SelectCommand.Parameters.Add("@PONo", SqlDbType.VarChar, 20).Value = strPONo;                
                dsReceipt.Clear();
                sdaReceipt.Fill(dsReceipt);
            }
            catch (Exception)
            {
                if (SqlConn.State == ConnectionState.Open) SqlConn.Close();
            }
            return dsReceipt;

        }

    }
}
