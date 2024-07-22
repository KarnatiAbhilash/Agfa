/* ==============================================================================================
    Created By      : Subahani SM
    Created Date    : 02 Sep 2010
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
    public class MonthEndClass
    {
        /* declare Class level Variables */
        private string strUserId, strMessage, strDebitCreditNote;
        private Int32 intDealerCode,intMonthNo,intYear;
        private Boolean BoolIsAgfaUser=false;
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

        public Int32 prpMonthNo
        {
            get
            {
                return intMonthNo;
            }
            set
            {
                intMonthNo = value;
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

        public Boolean IsAgfaUser
        {
            get
            {
                return BoolIsAgfaUser;
            }
            set
            {
                BoolIsAgfaUser = value;
            }
        }

        public string prpDebitCreditNote
        {
            get
            {
                return strDebitCreditNote;
            }
            set
            {
                strDebitCreditNote = value;
            }
        }
        

        /* Define Methods & Functions */
        public DataSet GetOpenMonth()
        {
            DataSet dsMonth = new DataSet();
            SqlConnection SqlConn = DBConnection.OpenConnection();
            SqlDataAdapter sdaMonth = new SqlDataAdapter("prcGetOpenMonth", SqlConn);
            try
            {
                sdaMonth.SelectCommand.CommandType = CommandType.StoredProcedure;
                sdaMonth.SelectCommand.Parameters.Add("@DealerCode", SqlDbType.BigInt).Value = intDealerCode;
                sdaMonth.SelectCommand.Parameters.Add("@IsAgfaUser", SqlDbType.Bit).Value = BoolIsAgfaUser;
                dsMonth.Clear();
                sdaMonth.Fill(dsMonth);
            }
            catch (Exception)
            {
                if (SqlConn.State == ConnectionState.Open) SqlConn.Close();
            }
            return dsMonth;
        }

        public string CloseMonthEnd()
        {
            strMessage = "";
            SqlCommand cmd = new SqlCommand("prcCloseMonthEnd", DBConnection.OpenConnection());
            try
            {
                cmd.Parameters.Add("@DealerCode", SqlDbType.BigInt).Value = intDealerCode;
                cmd.Parameters.Add("@MonthNo", SqlDbType.Int).Value = intMonthNo;
                cmd.Parameters.Add("@Year", SqlDbType.Int).Value = intYear;
                cmd.Parameters.Add("@UserId", SqlDbType.VarChar, 20).Value = strUserId;
                cmd.Parameters.Add("@IsAgfaUser", SqlDbType.Bit).Value = BoolIsAgfaUser;
                cmd.Parameters.Add("@DebitCreditNote", SqlDbType.VarChar, 50).Value = strDebitCreditNote;                                             
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

        public string MonthEndInitialize()
        {
            strMessage = "";
            SqlCommand cmd = new SqlCommand("prcMonthEndInitialize", DBConnection.OpenConnection());
            try
            {
                cmd.Parameters.Add("@DealerCode", SqlDbType.BigInt).Value = intDealerCode;           
                cmd.Parameters.Add("@UserId", SqlDbType.VarChar, 20).Value = strUserId;
                cmd.Parameters.Add("@IsAgfaUser", SqlDbType.Bit).Value = BoolIsAgfaUser;
                cmd.Parameters.Add("@DebitCreditNote", SqlDbType.VarChar,50).Value = strDebitCreditNote;                
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
    }
}
