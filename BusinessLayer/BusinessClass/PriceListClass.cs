/* ==============================================================================================
    Created By      : Subahani SM
    Created Date    : 09 Aug 2010
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
    public class PriceListClass
    {
        /* declare Class level Variables */
        private string strUserId, strMessage;
        private string strRemarks, strChangeType, strPriceType, strItemCode;
        private Int32 intDMIId, intCPMId, intItemId,intDealercode,intCustCode,maxQty;
        private double dbDlrPrice, dbPerSqrmt, dbEUPrice, dbEUPerSqrmt, dbProfitActual, dbProfitAgreed,dbValue;
        DateTime dtValidUpto;

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
        public string prpItemCode
        {
            get
            {
                return strItemCode;
            }
            set
            {
                strItemCode = value;
            }
        }

        public string prpChangeType
        {
            get
            {
                return strChangeType;
            }
            set
            {
                strChangeType = value;
            }
        }

        public string prpPriceChangeType
        {
            get
            {
                return strPriceType;
            }
            set
            {
                strPriceType = value;
            }
        }

        public Int32 prpMaxQty
        {
            get
            {
                return maxQty;
            }
            set
            {
                maxQty = value;
            }
        }
        public Int32 prpDMIId
        {
            get
            {
                return intDMIId;
            }
            set
            {
                intDMIId = value;
            }
        }

        public Int32 prpCPMId
        {
            get
            {
                return intCPMId;
            }
            set
            {
                intCPMId = value;
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

        public Int32 prpDealerCode
        {
            get
            {
                return intDealercode;
            }
            set
            {
                intDealercode = value;
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

        public double prpDlrPrice
        {
            get
            {
                return dbDlrPrice;
            }
            set
            {
                dbDlrPrice = value;
            }
        }

        public double prpPerSqrmt
        {
            get
            {
                return dbPerSqrmt;
            }
            set
            {
                dbPerSqrmt = value;
            }
        }

        public double prpEuPrice
        {
            get
            {
                return dbEUPrice;
            }
            set
            {
                dbEUPrice = value;
            }
        }

        public double prpEUPerSqrmt
        {
            get
            {
                return dbEUPerSqrmt;
            }
            set
            {
                dbEUPerSqrmt = value;
            }
        }

        public double prpProfitActual
        {
            get
            {
                return dbProfitActual;
            }
            set
            {
                dbProfitActual = value;
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

        public double prpChangeValue
        {
            get
            {
                return dbValue;
            }
            set
            {
                dbValue = value;
            }
        }
        public DateTime prpValidUpto
        {
            get
            {
                return dtValidUpto;
            }
            set
            {
                dtValidUpto = value;
            }
        }

        /* Define Methods & Functions */

        public DataSet FetchPriceList(string strCondition)
        {
            DataSet dsCust = new DataSet();
            SqlConnection SqlConn = DBConnection.OpenConnection();
            SqlDataAdapter sdaCust = new SqlDataAdapter("prcFetchPriceList", SqlConn);
            try
            {
                sdaCust.SelectCommand.CommandType = CommandType.StoredProcedure;
                sdaCust.SelectCommand.Parameters.Add("@DealerCode", SqlDbType.BigInt).Value = intDealercode;
                sdaCust.SelectCommand.Parameters.Add("@CustCode", SqlDbType.BigInt).Value = intCustCode;
                sdaCust.SelectCommand.Parameters.Add("@Condition", SqlDbType.VarChar, 8000).Value = strCondition;
                dsCust.Clear();
                sdaCust.Fill(dsCust);
            }
            catch (Exception)
            {
                if (SqlConn.State == ConnectionState.Open) SqlConn.Close();
            }
            return dsCust;
        }

        public string SavePriceChangeList()
        {
            strMessage = "";
            SqlCommand cmd = new SqlCommand("prcSavePriceChangeList", DBConnection.OpenConnection());
            try
            {
                cmd.Parameters.Add("@CPMId", SqlDbType.BigInt).Value = intCPMId;                
                cmd.Parameters.Add("@DIMId", SqlDbType.BigInt).Value = intDMIId;
                cmd.Parameters.Add("@DlrPrice", SqlDbType.Decimal).Value = dbDlrPrice;
                cmd.Parameters.Add("@PerSqrMt", SqlDbType.Decimal).Value = dbPerSqrmt;
                cmd.Parameters.Add("@EuPrice", SqlDbType.Decimal).Value = dbEUPrice;
                cmd.Parameters.Add("@EUPerSqrMt", SqlDbType.Decimal).Value = dbEUPerSqrmt;
                cmd.Parameters.Add("@ProfitActual", SqlDbType.Decimal).Value = dbProfitActual;
                cmd.Parameters.Add("@ProfitAgreed", SqlDbType.Decimal).Value = dbProfitAgreed;                
                cmd.Parameters.Add("@Remarks", SqlDbType.VarChar, 200).Value = strRemarks;
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

        public string SaveBulkPriceChangeDlr(string strDealerCodes)
        {
            strMessage = "";
            SqlCommand cmd = new SqlCommand("prcSaveBulkPriceChangeDlr", DBConnection.OpenConnection());
            try
            {
                cmd.Parameters.Add("@DealerCodes", SqlDbType.VarChar, 5000).Value = strDealerCodes;
                cmd.Parameters.Add("@ItemId", SqlDbType.BigInt).Value = intItemId;
                cmd.Parameters.Add("@Value", SqlDbType.Decimal).Value = dbValue;
                cmd.Parameters.Add("@PriceType", SqlDbType.VarChar, 10).Value = strPriceType;
                cmd.Parameters.Add("@ChangeType", SqlDbType.VarChar, 1).Value = strChangeType;
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
        public string SaveLoadPriceData(string strTableName)
        {
            strMessage = "";
            SqlCommand cmd;
            try
            {
                if (strTableName.ToLower() == "dealerprice")
                {
                    cmd = new SqlCommand("prcSaveLoadDlrPrice", DBConnection.OpenConnection());
                    cmd.Parameters.Add("@DealerCode", SqlDbType.Int).Value = intDealercode;
                    cmd.Parameters.Add("@ItemId", SqlDbType.Int).Value = intItemId;
                    cmd.Parameters.Add("@Price", SqlDbType.Decimal).Value = dbDlrPrice;
                    cmd.Parameters.Add("@PerSqMtr", SqlDbType.Decimal).Value = dbPerSqrmt;
                    cmd.Parameters.Add("@MaxQty", SqlDbType.Int).Value = maxQty;
                    cmd.Parameters.Add("@Remarks", SqlDbType.VarChar, 200).Value = strRemarks;
                    cmd.Parameters.Add("@UserId", SqlDbType.VarChar, 20).Value = strUserId;
                    cmd.Parameters.Add("@Msg", SqlDbType.VarChar, 200).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    strMessage = cmd.Parameters["@Msg"].Value.ToString();
                    cmd.Connection.Close();
                }
                else if (strTableName.ToLower() == "customeritemprice")
                {
                    cmd = new SqlCommand("prcSaveLoadCustPrice", DBConnection.OpenConnection());
                    cmd.Parameters.Add("@CustomerCode", SqlDbType.Int).Value = intCustCode;
                    cmd.Parameters.Add("@ItemId", SqlDbType.Int).Value = intItemId;
                    cmd.Parameters.Add("@EUPrice", SqlDbType.Decimal).Value = dbEUPrice;
                    cmd.Parameters.Add("@EUPerSqMtr", SqlDbType.Decimal).Value = dbEUPerSqrmt;
                    cmd.Parameters.Add("@ProfitActual", SqlDbType.Decimal).Value = dbProfitActual;
                    cmd.Parameters.Add("@ProfitAgreed", SqlDbType.Decimal).Value = dbProfitAgreed;
                    cmd.Parameters.Add("@ValidUpto", SqlDbType.DateTime).Value = dtValidUpto;
                    cmd.Parameters.Add("@Remarks", SqlDbType.VarChar, 200).Value = strRemarks;
                    cmd.Parameters.Add("@UserId", SqlDbType.VarChar, 20).Value = strUserId;
                    cmd.Parameters.Add("@Msg", SqlDbType.VarChar, 200).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    strMessage = cmd.Parameters["@Msg"].Value.ToString();
                    cmd.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                strMessage = ex.Message;
            }
            return strMessage;
        }

        public string SaveBulkPriceChangeCust(string strCustCodes)
        {
            strMessage = "";
            SqlCommand cmd = new SqlCommand("prcSaveBulkPriceChangeCust", DBConnection.OpenConnection());
            try
            {
                cmd.Parameters.Add("@CustCodes", SqlDbType.VarChar, 5000).Value = strCustCodes;
                cmd.Parameters.Add("@ItemId", SqlDbType.BigInt).Value = intItemId;
                cmd.Parameters.Add("@Value", SqlDbType.Decimal).Value = dbValue;
                cmd.Parameters.Add("@PriceType", SqlDbType.VarChar, 10).Value = strPriceType;
                cmd.Parameters.Add("@ChangeType", SqlDbType.VarChar, 1).Value = strChangeType;
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
    }
}
