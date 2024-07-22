/* ==============================================================================================
    Created By      : Subahani SM
    Created Date    : 22 July 2010
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
   public class ProductFamilyClass
    {
        /* declare Class level Variables */
        private string strUserId, strMessage;
        private string strBCCode,strPFCode,strPFDesc;
        private Int32 intPFId;
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

        public string prpPFCode
        {
            get
            {
                return strPFCode;
            }
            set
            {
                strPFCode = value;
            }
        }

        public string prpPFDesc
        {
            get
            {
                return strPFDesc;
            }
            set
            {
                strPFDesc = value;
            }
        }

        public Int32 prpPFId
        {
            get
            {
                return intPFId;
            }
            set
            {
                intPFId = value;
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

        public DataSet FetchProductFamily(string strCondition)
        {
            DataSet dsBc = new DataSet();
            SqlConnection SqlConn = DBConnection.OpenConnection();
            SqlDataAdapter sdaBC = new SqlDataAdapter("prcFetchProductFamily", SqlConn);
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

        public DataSet FetchProductFamilyOnId()
        {
            DataSet dsBc = new DataSet();
            SqlConnection SqlConn = DBConnection.OpenConnection();
            SqlDataAdapter sdaBC = new SqlDataAdapter("prcFetchProductFamilyOnId", SqlConn);
            try
            {
                sdaBC.SelectCommand.CommandType = CommandType.StoredProcedure;
                sdaBC.SelectCommand.Parameters.Add("@PFId", SqlDbType.BigInt).Value = intPFId;
                dsBc.Clear();
                sdaBC.Fill(dsBc);
            }
            catch (Exception)
            {
                if (SqlConn.State == ConnectionState.Open) SqlConn.Close();
            }
            return dsBc;
        }

        public string SaveProductFamily()
        {
            strMessage = "";
            SqlCommand cmd = new SqlCommand("prcInsertUpdateProductFamily", DBConnection.OpenConnection());
            try
            {
                cmd.Parameters.Add("@PFId", SqlDbType.BigInt).Value = intPFId;
                cmd.Parameters.Add("@PFCode", SqlDbType.VarChar, 10).Value = strPFCode;
                cmd.Parameters.Add("@BCCode", SqlDbType.VarChar, 10).Value = strBCCode;
                cmd.Parameters.Add("@PFDesc", SqlDbType.VarChar, 50).Value = strPFDesc;
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

        public string DeleteProductFamily()
        {
            strMessage = "";
            SqlCommand cmd = new SqlCommand("prcDeleteProductFamily", DBConnection.OpenConnection());
            try
            {
                cmd.Parameters.Add("@PFId", SqlDbType.BigInt).Value = intPFId;
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
