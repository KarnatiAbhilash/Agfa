using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Common;


namespace BusinessClass
{
    public class CustomerGroupMaster
    {
        /* declare Class level Variables */
        private string strUserId, strMessage;
        private string strCustGroup, strCustGroupDesc;
        private Int32 intCustId;
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

        public string prpCusGroup
        {
            get
            {
                return strCustGroup;
            }
            set
            {
                strCustGroup = value;
            }
        }


        public string prpCustGroupDesc
        {
            get
            {
                return strCustGroupDesc;
            }
            set
            {
                strCustGroupDesc = value;
            }
        }

        public Int32 prpCustId
        {
            get
            {
                return intCustId;
            }
            set
            {
                intCustId = value;
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
        public DataSet FetchCustomerGroup(string strCondition)
        {
            DataSet dsBc = new DataSet();
            SqlConnection SqlConn = DBConnection.OpenConnection();
            SqlDataAdapter sdaBC = new SqlDataAdapter("prcFetchCustomerGroupMaster", SqlConn);
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


        //this is for SaveCustomerGroupMassterClass
        public string SaveCustomerGroupMassterClass()
        {
            strMessage = "";
            SqlCommand cmd = new SqlCommand("prcInsertCustomerGroupMaster", DBConnection.OpenConnection());
            try
            {
                cmd.Parameters.Add("@CustId", SqlDbType.BigInt).Value = intCustId;
                cmd.Parameters.Add("@CustomerGroup", SqlDbType.VarChar, 10).Value = strCustGroup;
                cmd.Parameters.Add("@CustomerGroupDesc", SqlDbType.VarChar, 50).Value = strCustGroupDesc;
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


        //This is for fetch Customer GroupMaster
        public DataSet FetchCustomerGroupMasterClassOnId()
        {
            DataSet dsBc = new DataSet();
            SqlConnection SqlConn = DBConnection.OpenConnection();
            SqlDataAdapter sdaBC = new SqlDataAdapter("prcFetchCustomerGroupMasterClassOnId", SqlConn);
            try
            {
                sdaBC.SelectCommand.CommandType = CommandType.StoredProcedure;
                sdaBC.SelectCommand.Parameters.Add("@CustId", SqlDbType.BigInt).Value = intCustId;
                dsBc.Clear();
                sdaBC.Fill(dsBc);
            }
            catch (Exception)
            {
                if (SqlConn.State == ConnectionState.Open) SqlConn.Close();
            }
            return dsBc;
        }


        //this is for fetch records in CustomerGroupClass 
        public DataSet FetchCustomerGroupClass(string strCondition)
        {
            DataSet dsBc = new DataSet();
            SqlConnection SqlConn = DBConnection.OpenConnection();
            SqlDataAdapter sdaBC = new SqlDataAdapter("prcFetchCustomerGroupClassMaster", SqlConn);
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
        public string DeleteCustomerGroupMasterClass()
        {
            strMessage = "";
            SqlCommand cmd = new SqlCommand("prcDeleteCustomerGroupMasterClass", DBConnection.OpenConnection());
            try
            {
                cmd.Parameters.Add("@CustId", SqlDbType.BigInt).Value = intCustId;
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