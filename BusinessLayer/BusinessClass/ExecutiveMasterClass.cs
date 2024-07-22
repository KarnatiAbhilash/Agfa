/* ==============================================================================================
    Created By      : Subahani SM
    Created Date    : 28 July 2010
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
    public class ExecutiveMasterClass
    {
        /* declare Class level Variables */
        private string strUserId, strMessage,strExecutiveUserId;
        private string strExecutiveCode, strExecutiveDesc,strRegion;
        private Int32 intEMId;      
        
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

        public string prpExecutiveUserId
        {
            get
            {
                return strExecutiveUserId;
            }
            set
            {
                strExecutiveUserId = value;
            }
        }

        public string prpExecutiveCode
        {
            get
            {
                return strExecutiveCode;
            }
            set
            {
                strExecutiveCode = value;
            }
        }

        public string prpExecutiveDesc
        {
            get
            {
                return strExecutiveDesc;
            }
            set
            {
                strExecutiveDesc = value;
            }
        }

        public string prpRegion
        {
            get
            {
                return strRegion;
            }
            set
            {
                strRegion = value;
            }
        }

        public Int32 prpEMId
        {
            get
            {
                return intEMId;
            }
            set
            {
                intEMId = value;
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

        public DataSet FetchExecutiveMaster(string strCondition)
        {
            DataSet dsExc = new DataSet();
            SqlConnection SqlConn = DBConnection.OpenConnection();
            SqlDataAdapter sdaExc = new SqlDataAdapter("prcFetchExecutiveMaster", SqlConn);
            try
            {
                sdaExc.SelectCommand.CommandType = CommandType.StoredProcedure;
                sdaExc.SelectCommand.Parameters.Add("@Condition", SqlDbType.VarChar, 200).Value = strCondition;
                dsExc.Clear();
                sdaExc.Fill(dsExc);
            }
            catch (Exception)
            {
                if (SqlConn.State == ConnectionState.Open) SqlConn.Close();
            }
            return dsExc;
        }

        public DataSet FetchExecutiveMasterOnId()
        {
            DataSet dsExc = new DataSet();
            SqlConnection SqlConn = DBConnection.OpenConnection();
            SqlDataAdapter sdaExc = new SqlDataAdapter("prcFetchExecutiveMasterOnId", SqlConn);
            try
            {
                sdaExc.SelectCommand.CommandType = CommandType.StoredProcedure;
                sdaExc.SelectCommand.Parameters.Add("@EMId", SqlDbType.BigInt).Value = intEMId;
                dsExc.Clear();
                sdaExc.Fill(dsExc);
            }
            catch (Exception)
            {
                if (SqlConn.State == ConnectionState.Open) SqlConn.Close();
            }
            return dsExc;
        }

        public string SaveExecutiveMaster()
        {
            strMessage = "";
            SqlCommand cmd = new SqlCommand("prcInsertUpdateExecutiveMaster", DBConnection.OpenConnection());
            try
            {
                cmd.Parameters.Add("@EMId", SqlDbType.BigInt).Value = intEMId;
                cmd.Parameters.Add("@ExecutiveCode", SqlDbType.VarChar, 10).Value = strExecutiveCode;
                cmd.Parameters.Add("@ExecutiveDesc", SqlDbType.VarChar, 50).Value = strExecutiveDesc;
                cmd.Parameters.Add("@Region", SqlDbType.VarChar, 10).Value = strRegion;
                cmd.Parameters.Add("@Status", SqlDbType.Bit).Value = blnStatus;
                cmd.Parameters.Add("@UserId", SqlDbType.VarChar, 200).Value = strUserId;
                cmd.Parameters.Add("@ExecutiveUserId", SqlDbType.VarChar, 20).Value = strExecutiveUserId;
                
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

        public string DeleteExecutiveMaster()
        {
            strMessage = "";
            SqlCommand cmd = new SqlCommand("prcDeleteExecutiveMaster", DBConnection.OpenConnection());
            try
            {
                cmd.Parameters.Add("@EMId", SqlDbType.BigInt).Value = intEMId;
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

