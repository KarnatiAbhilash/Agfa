/* ==============================================================================================
    Created By      : Subahani SM
    Created Date    : 27 July 2010
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
    public class ItemMasterClass
    {
        /* declare Class level Variables */
        private string strUserId, strMessage;
        private string strItemCode,strBCCode, strPFCode, strGroupCode, strDesc1, strDesc2;
        private Int32 intId;
        private Boolean blnStatus;
        private double dbConvFact;

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

        public string prpDesc1
        {
            get
            {
                return strDesc1;
            }
            set
            {
                strDesc1 = value;
            }
        }

        public string prpDesc2
        {
            get
            {
                return strDesc2;
            }
            set
            {
                strDesc2 = value;
            }
        }

        public Int32 prpId
        {
            get
            {
                return intId;
            }
            set
            {
                intId = value;
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

        public double prpConvFactor
        {
            get
            {
                return dbConvFact;
            }
            set
            {
                dbConvFact = value;
            }
        }

        /* Define Methods & Functions */

        public DataSet FetchItemMaster(string strCondition)
        {
            DataSet dsBc = new DataSet();
            SqlConnection SqlConn = DBConnection.OpenConnection();
            SqlDataAdapter sdaBC = new SqlDataAdapter("prcFetchItemMaster", SqlConn);
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

        public DataSet FetchItemMasterOnId()
        {
            DataSet dsBc = new DataSet();
            SqlConnection SqlConn = DBConnection.OpenConnection();
            SqlDataAdapter sdaBC = new SqlDataAdapter("prcFetchItemMasterOnId", SqlConn);
            try
            {
                sdaBC.SelectCommand.CommandType = CommandType.StoredProcedure;
                sdaBC.SelectCommand.Parameters.Add("@Id", SqlDbType.BigInt).Value = intId;
                dsBc.Clear();
                sdaBC.Fill(dsBc);
            }
            catch (Exception)
            {
                if (SqlConn.State == ConnectionState.Open) SqlConn.Close();
            }
            return dsBc;
        }

        public string SaveItemMaster()
        {
            strMessage = "";
            SqlCommand cmd = new SqlCommand("prcInsertUpdateItemMaster", DBConnection.OpenConnection());
            try
            {
                cmd.Parameters.Add("@Id", SqlDbType.BigInt).Value = intId;
                cmd.Parameters.Add("@ItemCode", SqlDbType.VarChar, 10).Value = strItemCode;
                cmd.Parameters.Add("@BCCode", SqlDbType.VarChar, 10).Value = strBCCode;
                cmd.Parameters.Add("@PFCode", SqlDbType.VarChar, 10).Value = strPFCode;
                cmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 10).Value = strGroupCode;
                cmd.Parameters.Add("@Desc1", SqlDbType.VarChar, 50).Value = strDesc1;
                cmd.Parameters.Add("@Desc2", SqlDbType.VarChar, 50).Value = strDesc2;
                cmd.Parameters.Add("@ConvFactor", SqlDbType.Decimal).Value = dbConvFact;
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

        public string DeleteItemMaster()
        {
            strMessage = "";
            SqlCommand cmd = new SqlCommand("prcDeleteItemMaster", DBConnection.OpenConnection());
            try
            {
                cmd.Parameters.Add("@Id", SqlDbType.BigInt).Value = intId;
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
        //This is for fetch itemgroup
        public DataSet FetchEquipmentItemClassOnId()
        {
            DataSet dsBc = new DataSet();
            SqlConnection SqlConn = DBConnection.OpenConnection();
            SqlDataAdapter sdaBC = new SqlDataAdapter("prcFetchItemClassOnId", SqlConn);
            try
            {
                sdaBC.SelectCommand.CommandType = CommandType.StoredProcedure;
                sdaBC.SelectCommand.Parameters.Add("@Id", SqlDbType.BigInt).Value = intId;
                dsBc.Clear();
                sdaBC.Fill(dsBc);
            }
            catch (Exception)
            {
                if (SqlConn.State == ConnectionState.Open) SqlConn.Close();
            }
            return dsBc;
        }

        //this is for save equipment item master
        public string SaveEquipmentItemMaster()
        {
            strMessage = "";
            SqlCommand cmd = new SqlCommand("prcInsertEquipmentItemMaster", DBConnection.OpenConnection());
            try
            {
                cmd.Parameters.Add("@Id", SqlDbType.BigInt).Value = intId;
                cmd.Parameters.Add("@ItemCode", SqlDbType.VarChar, 10).Value = strItemCode;
                cmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 10).Value = strGroupCode;
                cmd.Parameters.Add("@Desc1", SqlDbType.VarChar, 50).Value = strDesc1;
                cmd.Parameters.Add("@Desc2", SqlDbType.VarChar, 50).Value = strDesc2;
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
        //this is for fetch equipment itemgroup all records in grid
        public DataSet FetchEquipmentItemClass(string strCondition)
        {
            DataSet dsBc = new DataSet();
            SqlConnection SqlConn = DBConnection.OpenConnection();
            SqlDataAdapter sdaBC = new SqlDataAdapter("prcFetchEquipmentItemClassMaster", SqlConn);
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
        //this is for delete equipment item group
        public string DeleteEquipmentItemGroupClass()
        {
            strMessage = "";
            SqlCommand cmd = new SqlCommand("prcDeleteEquipmentItemClass", DBConnection.OpenConnection());
            try
            {
                cmd.Parameters.Add("@Id", SqlDbType.BigInt).Value = intId;
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