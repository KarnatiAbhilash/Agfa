/* ==============================================================================================
    Created By      : Subahani SM
    Created Date    : 20 July 2010
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
    public class BudgetClassMaster
    {
        /* declare Class level Variables */
        private string strUserId,strMessage;
        private string strBCCode, strBCDesc;
        private Int32 intBCId;
        private Boolean blnStatus;

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

        public string prpBCCode
        {
            get
            {
                return strBCCode;
            }
            set
            {
                strBCCode = value;
            }
        }

        public string prpBCDesc
        {
            get
            {
                return strBCDesc;
            }
            set
            {
                strBCDesc = value;
            }
        }

        public Int32 prpBCId
        {
            get
            {
                return intBCId;
            }
            set
            {
                intBCId = value;
            }
        }

        public Boolean prpStatus
            {
                get
                {
                    return blnStatus;
                }
                set
                {
                    blnStatus = value;
                }
            }

        /* Define Methods & Functions */

        public DataSet FetchBudgetClass(string strCondition)
        {
            DataSet dsBc = new DataSet();
            SqlConnection SqlConn = DBConnection.OpenConnection();
            SqlDataAdapter sdaBC = new SqlDataAdapter("prcFetchBudgetClassMaster", SqlConn);
            try
            {
                sdaBC.SelectCommand.CommandType = CommandType.StoredProcedure;
                sdaBC.SelectCommand.Parameters.Add("@Condition", SqlDbType.VarChar, 200).Value = strCondition;                
                dsBc.Clear();
                sdaBC.Fill(dsBc);
            }
            catch (Exception)
            {
                if (SqlConn.State == ConnectionState.Open) SqlConn.Close();
            }
            return dsBc;
        }

        public DataSet FetchBudgetClassOnId()
        {
            DataSet dsBc = new DataSet();
            SqlConnection SqlConn = DBConnection.OpenConnection();
            SqlDataAdapter sdaBC = new SqlDataAdapter("prcFetchBudgetClassOnId", SqlConn);
            try
            {
                sdaBC.SelectCommand.CommandType = CommandType.StoredProcedure;
                sdaBC.SelectCommand.Parameters.Add("@BCId", SqlDbType.BigInt).Value = intBCId;
                dsBc.Clear();
                sdaBC.Fill(dsBc);
            }
            catch (Exception)
            {
                if (SqlConn.State == ConnectionState.Open) SqlConn.Close();
            }
            return dsBc;
        }

        public string SaveBudgetClass()
        {
            strMessage = "";
            SqlCommand cmd = new SqlCommand("prcInsertUpdateBudgetClass", DBConnection.OpenConnection());
            try
            {
                cmd.Parameters.Add("@BCId", SqlDbType.BigInt).Value = intBCId;
                cmd.Parameters.Add("@BCCode", SqlDbType.VarChar, 10).Value = strBCCode;
                cmd.Parameters.Add("@BCDesc", SqlDbType.VarChar, 50).Value = strBCDesc;                
                cmd.Parameters.Add("@Status", SqlDbType.Bit).Value = blnStatus;
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

        public string DeleteBudgetClass()
        {
            strMessage = "";
            SqlCommand cmd = new SqlCommand("prcDeleteBudgetClass", DBConnection.OpenConnection());
            try
            {
                cmd.Parameters.Add("@BCId", SqlDbType.BigInt).Value = intBCId;
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
