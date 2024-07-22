/* ==============================================================================================
    Created By      : Subahani SM
    Created Date    : 23 July 2010
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
   public class ProductGroupClass
    {
        /* declare Class level Variables */
        private string strUserId, strMessage;
        private string strGroupCode,strBCCode, strPFCode, strGroupDesc;
        private Int32 intPGId;
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

        public string prpGroupCode
        {
            get
            {
                return strGroupCode;
            }
            set
            {
                strGroupCode = value;
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

        public string prpGroupDesc
        {
            get
            {
                return strGroupDesc;
            }
            set
            {
                strGroupDesc = value;
            }
        }

        public Int32 prpPGId
        {
            get
            {
                return intPGId;
            }
            set
            {
                intPGId = value;
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

        public DataSet FetchProductGroup(string strCondition)
        {
            DataSet dsBc = new DataSet();
            SqlConnection SqlConn = DBConnection.OpenConnection();
            SqlDataAdapter sdaBC = new SqlDataAdapter("prcFetchProductGroup", SqlConn);
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

        public DataSet FetchProductGroupOnId()
        {
            DataSet dsBc = new DataSet();
            SqlConnection SqlConn = DBConnection.OpenConnection();
            SqlDataAdapter sdaBC = new SqlDataAdapter("prcFetchProductGroupOnId", SqlConn);
            try
            {
                sdaBC.SelectCommand.CommandType = CommandType.StoredProcedure;
                sdaBC.SelectCommand.Parameters.Add("@PGId", SqlDbType.BigInt).Value = intPGId;
                dsBc.Clear();
                sdaBC.Fill(dsBc);
            }
            catch (Exception)
            {
                if (SqlConn.State == ConnectionState.Open) SqlConn.Close();
            }
            return dsBc;
        }

        public string SaveProductGroup()
        {
            strMessage = "";
            SqlCommand cmd = new SqlCommand("prcInsertUpdateProductGroup", DBConnection.OpenConnection());
            try
            {
                cmd.Parameters.Add("@PGId", SqlDbType.BigInt).Value = intPGId;
                cmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 10).Value = strGroupCode;
                cmd.Parameters.Add("@BCCode", SqlDbType.VarChar, 10).Value = strBCCode;
                cmd.Parameters.Add("@PFCode", SqlDbType.VarChar, 10).Value = strPFCode;
                cmd.Parameters.Add("@GroupDesc", SqlDbType.VarChar, 50).Value = strGroupDesc;
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

        public string DeleteProductGroup()
        {
            strMessage = "";
            SqlCommand cmd = new SqlCommand("prcDeleteProductGroup", DBConnection.OpenConnection());
            try
            {
                cmd.Parameters.Add("@PGId", SqlDbType.BigInt).Value = intPGId;
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

        //this is for fetch records in equipmentproduct feild in popup
        public DataSet FetchEquipmentProductClass(string strCondition)
        {
            DataSet dsBc = new DataSet();
            SqlConnection SqlConn = DBConnection.OpenConnection();
            SqlDataAdapter sdaBC = new SqlDataAdapter("prcFetchEquipmentProductClassMaster", SqlConn);
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

        //this is for delete equipment product class
        public string DeleteEquipmentProductClass()
        {
            strMessage = "";
            SqlCommand cmd = new SqlCommand("prcDeleteEquipmentProductClass", DBConnection.OpenConnection());
            try
            {
                cmd.Parameters.Add("@PGId", SqlDbType.BigInt).Value = intPGId;
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

        //This is for fetch equipment productgroup
        public DataSet FetchEquipmentProductClassOnId()
        {
            DataSet dsBc = new DataSet();
            SqlConnection SqlConn = DBConnection.OpenConnection();
            SqlDataAdapter sdaBC = new SqlDataAdapter("prcFetchProductClassOnId", SqlConn);
            try
            {
                sdaBC.SelectCommand.CommandType = CommandType.StoredProcedure;
                sdaBC.SelectCommand.Parameters.Add("@PGId", SqlDbType.BigInt).Value = intPGId;
                dsBc.Clear();
                sdaBC.Fill(dsBc);
            }
            catch (Exception)
            {
                if (SqlConn.State == ConnectionState.Open) SqlConn.Close();
            }
            return dsBc;
        }

        //this is for Equipmentproductsaving
        public string SaveEquipmentProductClass()
        {
            strMessage = "";
            SqlCommand cmd = new SqlCommand("prcInsertEquipmentProductGroup", DBConnection.OpenConnection());
            try
            {
                cmd.Parameters.Add("@PGId", SqlDbType.BigInt).Value = intPGId;
                cmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 10).Value = strGroupCode;
                cmd.Parameters.Add("@GroupDesc", SqlDbType.VarChar, 50).Value = strGroupDesc;
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

    }
}

