using Common;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BusinessClass
{
    public class SalesEmployee
    {
        private string strUserId;
        private string strMessage;
        private string strName;
        private string strRegion;
        private string strEmailId;
        private string strContactNo;
        private int intId;
        private bool blnStatus;
        private double dbConvFact;

        public string prpUserId
        {
            get => this.strUserId;
            set => this.strUserId = value;
        }

        public string prpName
        {
            get => this.strName;
            set => this.strName = value;
        }

        public string prpRegion
        {
            get => this.strRegion;
            set => this.strRegion = value;
        }

        public string prpEmailId
        {
            get => this.strEmailId;
            set => this.strEmailId = value;
        }

        public string prpContactNo
        {
            get => this.strContactNo;
            set => this.strContactNo = value;
        }

        public int prpId
        {
            get => this.intId;
            set => this.intId = value;
        }

        public bool prpStatus
        {
            get => this.blnStatus;
            set => this.blnStatus = value;
        }

        public double prpConvFactor
        {
            get => this.dbConvFact;
            set => this.dbConvFact = value;
        }

        public DataSet FetchItemMaster(string strCondition)
        {
            DataSet dataSet = new DataSet();
            SqlConnection selectConnection = DBConnection.OpenConnection();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("prcFetchItemMaster", selectConnection);
            try
            {
                sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDataAdapter.SelectCommand.Parameters.Add("@Condition", SqlDbType.VarChar, 200).Value = (object)strCondition;
                dataSet.Clear();
                sqlDataAdapter.Fill(dataSet);
            }
            catch (Exception ex)
            {
                if (selectConnection.State == ConnectionState.Open)
                    selectConnection.Close();
            }
            return dataSet;
        }

        public string DeleteSalesEmployee()
        {
            this.strMessage = "";
            SqlCommand sqlCommand = new SqlCommand("prcDeleteSalesEmployeeClass", DBConnection.OpenConnection());
            try
            {
                sqlCommand.Parameters.Add("@Id", SqlDbType.BigInt).Value = (object)this.intId;
                sqlCommand.Parameters.Add("@Msg", SqlDbType.VarChar, 200).Direction = ParameterDirection.Output;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection.Open();
                sqlCommand.ExecuteNonQuery();
                this.strMessage = sqlCommand.Parameters["@Msg"].Value.ToString();
                sqlCommand.Connection.Close();
            }
            catch (Exception ex)
            {
                if (sqlCommand.Connection.State == ConnectionState.Open)
                    sqlCommand.Connection.Close();
                this.strMessage = ex.Message;
            }
            return this.strMessage;
        }

        public DataSet FetchSalesEmployeeClass(string strCondition)
        {
            DataSet dataSet = new DataSet();
            SqlConnection selectConnection = DBConnection.OpenConnection();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("prcFetchSalesEmployeeClassMaster", selectConnection);
            try
            {
                sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDataAdapter.SelectCommand.Parameters.Add("@Condition", SqlDbType.VarChar, 200).Value = (object)strCondition;
                dataSet.Clear();
                sqlDataAdapter.Fill(dataSet);
            }
            catch (Exception ex)
            {
                if (selectConnection.State == ConnectionState.Open)
                    selectConnection.Close();
            }
            return dataSet;
        }

        public string SaveSalesEmployee()
        {
            this.strMessage = "";
            SqlCommand sqlCommand = new SqlCommand("prcInsertSalesEmployee", DBConnection.OpenConnection());
            try
            {
                sqlCommand.Parameters.Add("@Id", SqlDbType.BigInt).Value = (object)this.intId;
                sqlCommand.Parameters.Add("@Name", SqlDbType.VarChar, 100).Value = (object)this.strName;
                sqlCommand.Parameters.Add("@Region", SqlDbType.VarChar, 100).Value = (object)this.strRegion;
                sqlCommand.Parameters.Add("@EmailId", SqlDbType.VarChar, 100).Value = (object)this.strEmailId;
                sqlCommand.Parameters.Add("@ContactNo", SqlDbType.VarChar, 100).Value = (object)this.strContactNo;
                sqlCommand.Parameters.Add("@Status", SqlDbType.Bit).Value = (object)this.blnStatus;
                sqlCommand.Parameters.Add("@UserId", SqlDbType.VarChar, 200).Value = (object)this.strUserId;
                sqlCommand.Parameters.Add("@Msg", SqlDbType.VarChar, 200).Direction = ParameterDirection.Output;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection.Open();
                sqlCommand.ExecuteNonQuery();
                this.strMessage = sqlCommand.Parameters["@Msg"].Value.ToString();
                sqlCommand.Connection.Close();
            }
            catch (Exception ex)
            {
                if (sqlCommand.Connection.State == ConnectionState.Open)
                    sqlCommand.Connection.Close();
                this.strMessage = ex.Message;
            }
            return this.strMessage;
        }

        public DataSet FetchSalesEmployeeClassOnId()
        {
            DataSet dataSet = new DataSet();
            SqlConnection selectConnection = DBConnection.OpenConnection();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("prcFetchSalesEmployeeClassOnId", selectConnection);
            try
            {
                sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDataAdapter.SelectCommand.Parameters.Add("@Id", SqlDbType.BigInt).Value = (object)this.intId;
                dataSet.Clear();
                sqlDataAdapter.Fill(dataSet);
            }
            catch (Exception ex)
            {
                if (selectConnection.State == ConnectionState.Open)
                    selectConnection.Close();
            }
            return dataSet;
        }
    }
}
