/* ==============================================================================================
    Created By      : Subahani SM
    Created Date    : 22 Aug 2010
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
    public class PurchaseOrder
    {
        /* declare Class level Variables */
        private string strUserId, strMessage;
        private string strPONo, strPOType, strMonth;
        private Int32 intDealerCode, intYear, intItemId, intQty;
        private double dbSalesTax, dbLocalLevi, dbInsurance, dbOthers, dbNetInvoiceAmt, dbGrossInvoiceAmt, dbUnitPrice;

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

        public string prpPOType
        {
            get
            {
                return strPOType;
            }
            set
            {
                strPOType = value;
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

        /* Define Methods & Functions */

        public string FetchPOMaxNo(string strSubCode)
        {
            strMessage = "";
            SqlCommand cmd = new SqlCommand("prcFetchPOMaxNo", DBConnection.OpenConnection());
            try
            {
                cmd.Parameters.Add("@DealerCode", SqlDbType.BigInt).Value = intDealerCode;
                cmd.Parameters.Add("@SubCode", SqlDbType.VarChar, 10).Value = strSubCode;
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

            }
            return strMessage;
        }

        public string SavePurchaseOrder()
        {
            strMessage = "";
            SqlCommand cmd = new SqlCommand("prcSavePurchaseOrder", DBConnection.OpenConnection());
            try
            {
                cmd.Parameters.Add("@PONo", SqlDbType.VarChar, 20).Value = strPONo;
                cmd.Parameters.Add("@DealerCode", SqlDbType.BigInt).Value = intDealerCode;
                cmd.Parameters.Add("@POType", SqlDbType.VarChar, 20).Value = strPOType;
                cmd.Parameters.Add("@Month", SqlDbType.VarChar, 15).Value = strMonth;
                cmd.Parameters.Add("@Year", SqlDbType.Int).Value = intYear;
                cmd.Parameters.Add("@SalesTax", SqlDbType.Decimal).Value = dbSalesTax;
                cmd.Parameters.Add("@LocalLevi", SqlDbType.Decimal).Value = dbLocalLevi;
                cmd.Parameters.Add("@Insurance", SqlDbType.Decimal).Value = dbInsurance;
                cmd.Parameters.Add("@Others", SqlDbType.Decimal).Value = dbOthers;
                cmd.Parameters.Add("@NetInvoiceAmt", SqlDbType.Decimal).Value = dbNetInvoiceAmt;
                cmd.Parameters.Add("@GrossInvoiceAmt", SqlDbType.Decimal).Value = dbGrossInvoiceAmt;
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

        public string SavePurchaseOrderDetails()
        {
            strMessage = "";
            SqlCommand cmd = new SqlCommand("prcSavePurchaseOrderDetails", DBConnection.OpenConnection());
            try
            {
                cmd.Parameters.Add("@PONo", SqlDbType.VarChar, 20).Value = strPONo;
                cmd.Parameters.Add("@ItemId", SqlDbType.Int).Value = intItemId;
                cmd.Parameters.Add("@Qty", SqlDbType.Int).Value = intQty;
                cmd.Parameters.Add("@UnitPrice", SqlDbType.Decimal).Value = dbUnitPrice;
                cmd.Parameters.Add("@UserId", SqlDbType.VarChar, 20).Value = strUserId;
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
        public string DeletePurchaseOrder()
        {
            strMessage = "";
            SqlCommand cmd = new SqlCommand("prcDeletePurchaseOrder", DBConnection.OpenConnection());
            try
            {
                cmd.Parameters.Add("@PONo", SqlDbType.VarChar, 20).Value = strPONo;
                cmd.Parameters.Add("@Msg", SqlDbType.VarChar, 2000).Direction = ParameterDirection.Output;
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
