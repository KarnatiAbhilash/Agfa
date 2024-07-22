using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Common;


namespace BusinessClass
{

    public class ClsReports
    {
        private string strUserId, strSalesType, strCustGroup;
        private string strBCCode,strPFCode,strGroupCode,strItemCode,strMonth,strYear,strCustCode;
        private string strExecutiveCode,strRegion,strDealerCode,strState,strInvFromDate,strInvToDate;
        private string strProductGroup, strPOType;

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

        public string prpProductGroup
        {
            get
            {
                return strProductGroup;
            }
            set
            {
                strProductGroup = value;
            }
        }
        public string prpInvToDate
        {
            get
            {
                return strInvToDate;
            }
            set
            {
                strInvToDate = value;
            }
        }

        public string prpInvFromDate
        {
            get
            {
                return strInvFromDate;
            }
            set
            {
                strInvFromDate = value;
            }
        }

        public string prpState
        {
            get
            {
                return strState;
            }
            set
            {
                strState = value;
            }
        }

        public string prpDealerCode
        {
            get
            {
                return strDealerCode;
            }
            set
            {
                strDealerCode = value;
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

        public string prpCustCode
        {
            get
            {
                return strCustCode;
            }
            set
            {
                strCustCode = value;
            }
        }

        public string prpYear
        {
            get
            {
                return strYear;
            }
            set
            {
                strYear = value;
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
        public string prpSalesType
        {
            get
            {
                return strSalesType;
            }
            set
            {
                strSalesType = value;
            }
        }
        public string prpCustGroup
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
        
        public SqlDataReader funDebitCreditNoteReportNew()
        {
            SqlDataReader sdrPopulateRecords = null;
            SqlConnection SqlConn = DBConnection.OpenConnection();
            SqlCommand cmdPopulateRecords = new SqlCommand("PrcDebitCreditNoteRpt", DBConnection.OpenConnection());
            try
            {
                cmdPopulateRecords.CommandType = CommandType.StoredProcedure;
                cmdPopulateRecords.CommandTimeout = 0;
                cmdPopulateRecords.Parameters.Add("@BCCode", SqlDbType.VarChar, 50).Value = strBCCode;
                cmdPopulateRecords.Parameters.Add("@PFCode", SqlDbType.VarChar, 50).Value = strPFCode;
                cmdPopulateRecords.Parameters.Add("@GroupCode", SqlDbType.VarChar, 50).Value = strGroupCode;
                cmdPopulateRecords.Parameters.Add("@ItemCode", SqlDbType.VarChar, 50).Value = strItemCode;
                cmdPopulateRecords.Parameters.Add("@Month", SqlDbType.VarChar, 50).Value = strMonth;
                cmdPopulateRecords.Parameters.Add("@Year", SqlDbType.VarChar, 50).Value = strYear;
                cmdPopulateRecords.Parameters.Add("@CustCode", SqlDbType.VarChar, 50).Value = strCustCode;
                cmdPopulateRecords.Parameters.Add("@ExecutiveCode", SqlDbType.VarChar, 50).Value = strExecutiveCode;
                cmdPopulateRecords.Parameters.Add("@Region", SqlDbType.VarChar, 50).Value = strRegion;
                cmdPopulateRecords.Parameters.Add("@DealerCode", SqlDbType.VarChar, 50).Value = strDealerCode;
                cmdPopulateRecords.Parameters.Add("@State", SqlDbType.VarChar, 50).Value = strState;
                cmdPopulateRecords.Parameters.Add("@InvFromDate", SqlDbType.VarChar, 50).Value = strInvFromDate;
                cmdPopulateRecords.Parameters.Add("@InvToDate", SqlDbType.VarChar, 50).Value = strInvToDate;
                cmdPopulateRecords.Connection.Open();
                sdrPopulateRecords = cmdPopulateRecords.ExecuteReader();
            }
            catch (Exception)
            {
                if (SqlConn.State == ConnectionState.Open) SqlConn.Close();
            }
            return sdrPopulateRecords;
        }
        public DataSet funDebitCreditNoteReport()
        {
            DataSet dsRpt = new DataSet();
            SqlConnection SqlConn = DBConnection.OpenConnection();
            SqlDataAdapter sdaRpt = new SqlDataAdapter("PrcDebitCreditNoteRpt", SqlConn);
            try
            {
                sdaRpt.SelectCommand.CommandType = CommandType.StoredProcedure;
                sdaRpt.SelectCommand.Parameters.Add("@BCCode", SqlDbType.VarChar, 50).Value = strBCCode;
                sdaRpt.SelectCommand.Parameters.Add("@PFCode", SqlDbType.VarChar, 50).Value = strPFCode;
                sdaRpt.SelectCommand.Parameters.Add("@GroupCode", SqlDbType.VarChar, 50).Value = strGroupCode;
                sdaRpt.SelectCommand.Parameters.Add("@ItemCode", SqlDbType.VarChar, 50).Value = strItemCode;
                sdaRpt.SelectCommand.Parameters.Add("@Month", SqlDbType.VarChar, 50).Value = strMonth;
                sdaRpt.SelectCommand.Parameters.Add("@Year", SqlDbType.VarChar, 50).Value = strYear;
                sdaRpt.SelectCommand.Parameters.Add("@CustCode", SqlDbType.VarChar, 50).Value = strCustCode;
                sdaRpt.SelectCommand.Parameters.Add("@ExecutiveCode", SqlDbType.VarChar, 50).Value = strExecutiveCode;
                sdaRpt.SelectCommand.Parameters.Add("@Region", SqlDbType.VarChar, 50).Value = strRegion;
                sdaRpt.SelectCommand.Parameters.Add("@DealerCode", SqlDbType.VarChar, 50).Value = strDealerCode;
                sdaRpt.SelectCommand.Parameters.Add("@State", SqlDbType.VarChar, 50).Value = strState;
                sdaRpt.SelectCommand.Parameters.Add("@InvFromDate", SqlDbType.VarChar, 50).Value = strInvFromDate;
                sdaRpt.SelectCommand.Parameters.Add("@InvToDate", SqlDbType.VarChar, 50).Value = strInvToDate;
                dsRpt.Clear();
                sdaRpt.Fill(dsRpt);
            }
            catch (Exception)
            {
                if (SqlConn.State == ConnectionState.Open) SqlConn.Close();
            }
            return dsRpt;
        }
        public DataSet funItemMovmentReport()
        {
            DataSet dsRpt = new DataSet();
            SqlConnection SqlConn = DBConnection.OpenConnection();
            SqlDataAdapter sdaRpt = new SqlDataAdapter("PrcGetItemMovementRpt", SqlConn);
            try
            {   sdaRpt.SelectCommand.CommandType = CommandType.StoredProcedure;
                sdaRpt.SelectCommand.Parameters.Add("@DealerName", SqlDbType.VarChar, 50).Value = strDealerCode;
                sdaRpt.SelectCommand.Parameters.Add("@ItemCode", SqlDbType.VarChar, 50).Value = strItemCode;
                sdaRpt.SelectCommand.Parameters.Add("@InvFromDate", SqlDbType.VarChar, 50).Value = strInvFromDate;
                sdaRpt.SelectCommand.Parameters.Add("@InvToDate", SqlDbType.VarChar, 50).Value = strInvToDate;
                sdaRpt.SelectCommand.Parameters.Add("@SalesType", SqlDbType.VarChar, 50).Value = strSalesType;
                dsRpt.Clear();
                sdaRpt.Fill(dsRpt);
            }
            catch (Exception)
            {
                if (SqlConn.State == ConnectionState.Open) SqlConn.Close();
            }
            return dsRpt;
        }

        public DataSet funSchemeAchivementReport()
        {
            DataSet dsRpt = new DataSet();
            SqlConnection SqlConn = DBConnection.OpenConnection();
            SqlDataAdapter sdaRpt = new SqlDataAdapter("PrcGetSchemeAchivement", SqlConn);
            try
            {
                sdaRpt.SelectCommand.CommandType = CommandType.StoredProcedure;
                sdaRpt.SelectCommand.Parameters.Add("@DealerCode", SqlDbType.VarChar, 50).Value = strDealerCode;
                sdaRpt.SelectCommand.Parameters.Add("@CustCode", SqlDbType.VarChar, 50).Value = strCustCode;
                sdaRpt.SelectCommand.Parameters.Add("@ItemCode", SqlDbType.VarChar, 50).Value = strItemCode;
                sdaRpt.SelectCommand.Parameters.Add("@InvFromDate", SqlDbType.VarChar, 50).Value = strInvFromDate;
                sdaRpt.SelectCommand.Parameters.Add("@InvToDate", SqlDbType.VarChar, 50).Value = strInvToDate;
                dsRpt.Clear();
                sdaRpt.Fill(dsRpt);
            }
            catch (Exception)
            {
                if (SqlConn.State == ConnectionState.Open) SqlConn.Close();
            }
            return dsRpt;
        }
        public DataSet funInventoryReport()
        {
            DataSet dsRpt = new DataSet();
            SqlConnection SqlConn = DBConnection.OpenConnection();
            SqlDataAdapter sdaRpt = new SqlDataAdapter("PrcGetInventoryRpt", SqlConn);
            try
            {
                sdaRpt.SelectCommand.CommandType = CommandType.StoredProcedure;
                sdaRpt.SelectCommand.Parameters.Add("@DealerCode", SqlDbType.VarChar, 50).Value = strDealerCode;
                sdaRpt.SelectCommand.Parameters.Add("@ProductGroup", SqlDbType.VarChar, 50).Value = strGroupCode;
                dsRpt.Clear();
                sdaRpt.Fill(dsRpt);
            }
            catch (Exception)
            {
                if (SqlConn.State == ConnectionState.Open) SqlConn.Close();
            }
            return dsRpt;
        }
        public DataSet GetItemPriceList()
        {
            DataSet dsRpt = new DataSet();
            SqlConnection SqlConn = DBConnection.OpenConnection();
            SqlDataAdapter sdaRpt = new SqlDataAdapter("[prcPriceListRpt]", SqlConn);
            try
            {
                sdaRpt.SelectCommand.CommandType = CommandType.StoredProcedure;
                sdaRpt.SelectCommand.Parameters.Add("@DealerCode", SqlDbType.VarChar, 20).Value = strDealerCode;
                sdaRpt.SelectCommand.Parameters.Add("@ItemCode", SqlDbType.VarChar, 50).Value = strItemCode;
                sdaRpt.SelectCommand.Parameters.Add("@CustCode", SqlDbType.VarChar, 20).Value = strCustCode;
                sdaRpt.SelectCommand.Parameters.Add("@ExecCode", SqlDbType.VarChar, 10).Value = strExecutiveCode;
                sdaRpt.SelectCommand.Parameters.Add("@State", SqlDbType.VarChar, 5).Value = strState;
                sdaRpt.SelectCommand.Parameters.Add("@Region", SqlDbType.VarChar, 10).Value = strRegion;
                sdaRpt.SelectCommand.Parameters.Add("@CustGroup", SqlDbType.VarChar, 10).Value = strCustGroup;
                dsRpt.Clear();
                sdaRpt.Fill(dsRpt);
            }
            catch (Exception)
            {
                if (SqlConn.State == ConnectionState.Open) SqlConn.Close();
            }
            return dsRpt;
        }


        public SqlDataReader GetDealerReceipt()
        {
            SqlConnection connection = DBConnection.OpenConnection();
            SqlCommand sqlCommand = new SqlCommand("PrcGetDealerReceipt", connection);
            try
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("@DealerName", SqlDbType.NVarChar, 50).Value = (string)this.strDealerCode;
                sqlCommand.Parameters.Add("@ProductGroup", SqlDbType.NVarChar, 50).Value = (string)this.strProductGroup;
                sqlCommand.Parameters.Add("@InvFromDate", SqlDbType.NVarChar, 50).Value = (string)this.strInvFromDate;
                sqlCommand.Parameters.Add("@InvToDate", SqlDbType.NVarChar, 50).Value = (string)this.strInvToDate;
                sqlCommand.Parameters.Add("@POType", SqlDbType.NVarChar, 50).Value = (string)this.strPOType;
                connection.Open();
                return sqlCommand.ExecuteReader();
            }
            catch (Exception ex)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                throw ex;
            }
        }
        public SqlDataReader GetDealerMonthEnd()
        {
            SqlConnection connection = DBConnection.OpenConnection();
            SqlCommand sqlCommand = new SqlCommand("PrcGetDealerMonthEnd", connection);
            try
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("@DealerCode", SqlDbType.BigInt).Value = (object)Convert.ToInt32(this.strDealerCode);
                connection.Open();
                return sqlCommand.ExecuteReader();
            }
            catch (Exception ex)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                throw ex;
            }
        }

        public SqlDataReader GetMonthEnd()
        {
            SqlConnection connection = DBConnection.OpenConnection();
            SqlCommand sqlCommand = new SqlCommand("PrcGetMonthEnd", connection);
            try
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("@DealerCode", SqlDbType.BigInt).Value = (object)Convert.ToInt32(this.strDealerCode);
                connection.Open();
                return sqlCommand.ExecuteReader();
            }
            catch (Exception ex)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                throw ex;
            }
        }


        //procedure for Installbaseconsumption
        public SqlDataReader funInstallbaseconsumption()
        {
            SqlDataReader sdrPopulateRecords = null;
            SqlConnection SqlConn = DBConnection.OpenConnection();
            SqlCommand cmdPopulateRecords = new SqlCommand("PrcInstallBaseFilmConsumptionRpt", DBConnection.OpenConnection());
            try
            {
                cmdPopulateRecords.CommandType = CommandType.StoredProcedure;
                cmdPopulateRecords.CommandTimeout = 0;
                cmdPopulateRecords.Parameters.Add("@ExecutiveCode", SqlDbType.VarChar, 50).Value = strExecutiveCode;
                cmdPopulateRecords.Parameters.Add("@Region", SqlDbType.VarChar, 50).Value = strRegion;
                cmdPopulateRecords.Parameters.Add("@DealerCode", SqlDbType.VarChar, 50).Value = strDealerCode;
                cmdPopulateRecords.Parameters.Add("@CustCode", SqlDbType.VarChar, 50).Value = strCustCode;
                cmdPopulateRecords.Parameters.Add("@Month", SqlDbType.VarChar, 50).Value = strMonth;
                cmdPopulateRecords.Parameters.Add("@Year", SqlDbType.VarChar, 50).Value = strYear;
                cmdPopulateRecords.Parameters.Add("@State", SqlDbType.VarChar, 50).Value = strState;
                cmdPopulateRecords.Parameters.Add("@CustomerGroup", SqlDbType.VarChar, 50).Value = strCustGroup;
                cmdPopulateRecords.Parameters.Add("@InvFromDate", SqlDbType.VarChar, 50).Value = strInvFromDate;
                cmdPopulateRecords.Parameters.Add("@InvToDate", SqlDbType.VarChar, 50).Value = strInvToDate;
                cmdPopulateRecords.Connection.Open();
                sdrPopulateRecords = cmdPopulateRecords.ExecuteReader();
            }
            catch (Exception)
            {
                if (SqlConn.State == ConnectionState.Open) SqlConn.Close();
            }
            return sdrPopulateRecords;
        }

        //DemoConsumption
        public SqlDataReader funDemoConsumption()
        {
            SqlDataReader sdrPopulateRecords = null;
            SqlConnection SqlConn = DBConnection.OpenConnection();
            SqlCommand cmdPopulateRecords = new SqlCommand("PrcDemofillconsuptionRpt_old", DBConnection.OpenConnection());
            try
            {
                cmdPopulateRecords.CommandType = CommandType.StoredProcedure;
                cmdPopulateRecords.CommandTimeout = 0;
                cmdPopulateRecords.Parameters.Add("@ExecutiveCode", SqlDbType.VarChar, 50).Value = strExecutiveCode;
                cmdPopulateRecords.Parameters.Add("@Region", SqlDbType.VarChar, 50).Value = strRegion;
                cmdPopulateRecords.Parameters.Add("@DealerCode", SqlDbType.VarChar, 50).Value = strDealerCode;
                cmdPopulateRecords.Parameters.Add("@CustCode", SqlDbType.VarChar, 50).Value = strCustCode;
                cmdPopulateRecords.Parameters.Add("@Month", SqlDbType.VarChar, 50).Value = strMonth;
                cmdPopulateRecords.Parameters.Add("@Year", SqlDbType.VarChar, 50).Value = strYear;
                cmdPopulateRecords.Parameters.Add("@State", SqlDbType.VarChar, 50).Value = strState;
                cmdPopulateRecords.Parameters.Add("@CustomerGroup", SqlDbType.VarChar, 50).Value = strCustGroup;
                cmdPopulateRecords.Parameters.Add("@InvFromDate", SqlDbType.VarChar, 50).Value = strInvFromDate;
                cmdPopulateRecords.Parameters.Add("@InvToDate", SqlDbType.VarChar, 50).Value = strInvToDate;
                cmdPopulateRecords.Connection.Open();
                sdrPopulateRecords = cmdPopulateRecords.ExecuteReader();
            }
            catch (Exception)
            {
                if (SqlConn.State == ConnectionState.Open) SqlConn.Close();
            }
            return sdrPopulateRecords;
        }

        // InstallBaseFilter
        public SqlDataReader funInstallBaseFilter()
        {
            SqlDataReader sdrPopulateRecords = null;
            SqlConnection SqlConn = DBConnection.OpenConnection();
            SqlCommand cmdPopulateRecords = new SqlCommand("PrcInstallBaseFilterRpt", DBConnection.OpenConnection());
            try
            {
                cmdPopulateRecords.CommandType = CommandType.StoredProcedure;
                cmdPopulateRecords.CommandTimeout = 0;
                cmdPopulateRecords.Parameters.Add("@ExecutiveCode", SqlDbType.VarChar, 50).Value = strExecutiveCode;
                cmdPopulateRecords.Parameters.Add("@Region", SqlDbType.VarChar, 50).Value = strRegion;
                cmdPopulateRecords.Parameters.Add("@DealerCode", SqlDbType.VarChar, 50).Value = strDealerCode;
                cmdPopulateRecords.Parameters.Add("@CustCode", SqlDbType.VarChar, 50).Value = strCustCode;
                cmdPopulateRecords.Parameters.Add("@State", SqlDbType.VarChar, 50).Value = strState;
                cmdPopulateRecords.Parameters.Add("@CustomerGroup", SqlDbType.VarChar, 50).Value = strCustGroup;
                cmdPopulateRecords.Connection.Open();
                sdrPopulateRecords = cmdPopulateRecords.ExecuteReader();
            }
            catch (Exception)
            {
                if (SqlConn.State == ConnectionState.Open) SqlConn.Close();
            }
            return sdrPopulateRecords;
        }

        public SqlDataReader funPrivillage()
        {
            SqlDataReader sqlDataReader = (SqlDataReader)null;
            SqlConnection sqlConnection = DBConnection.OpenConnection();
            SqlCommand sqlCommand = new SqlCommand("PrcPrivilageCustomer_id", DBConnection.OpenConnection());
            try
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 0;
                sqlCommand.Parameters.Add("@ExecutiveCode", SqlDbType.VarChar, 50).Value = (object)this.strExecutiveCode;
                sqlCommand.Parameters.Add("@Region", SqlDbType.VarChar, 50).Value = (object)this.strRegion;
                sqlCommand.Parameters.Add("@DealerCode", SqlDbType.VarChar, 50).Value = (object)this.strDealerCode;
                sqlCommand.Parameters.Add("@CustCode", SqlDbType.VarChar, 50).Value = (object)this.strCustCode;
                sqlCommand.Parameters.Add("@State", SqlDbType.VarChar, 50).Value = (object)this.strState;
                sqlCommand.Parameters.Add("@CustomerGroup", SqlDbType.VarChar, 50).Value = (object)this.strCustGroup;
                sqlCommand.Connection.Open();
                sqlDataReader = sqlCommand.ExecuteReader();
            }
            catch (Exception ex)
            {
                if (sqlConnection.State == ConnectionState.Open)
                    sqlConnection.Close();
            }
            return sqlDataReader;
        }

        public SqlDataReader funZeroConsumption()
        {
            SqlDataReader sqlDataReader = (SqlDataReader)null;
            SqlConnection sqlConnection = DBConnection.OpenConnection();
            SqlCommand sqlCommand = new SqlCommand("PrcZeroConsumption", DBConnection.OpenConnection());
            try
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 0;
                sqlCommand.Parameters.Add("@ExecutiveCode", SqlDbType.VarChar, 50).Value = (object)this.strExecutiveCode;
                sqlCommand.Parameters.Add("@Region", SqlDbType.VarChar, 50).Value = (object)this.strRegion;
                sqlCommand.Parameters.Add("@DealerCode", SqlDbType.VarChar, 50).Value = (object)this.strDealerCode;
                sqlCommand.Parameters.Add("@CustCode", SqlDbType.VarChar, 50).Value = (object)this.strCustCode;
                sqlCommand.Parameters.Add("@State", SqlDbType.VarChar, 50).Value = (object)this.strState;
                sqlCommand.Parameters.Add("@CustomerGroup", SqlDbType.VarChar, 50).Value = (object)this.strCustGroup;
                sqlCommand.Connection.Open();
                sqlDataReader = sqlCommand.ExecuteReader();
            }
            catch (Exception ex)
            {
                if (sqlConnection.State == ConnectionState.Open)
                    sqlConnection.Close();
            }
            return sqlDataReader;
        }

        public SqlDataReader funPrivilageCustomerClassMaster()
        {
            SqlDataReader sqlDataReader = (SqlDataReader)null;
            SqlConnection sqlConnection = DBConnection.OpenConnection();
            SqlCommand sqlCommand = new SqlCommand("PrcPrivilageCustomerClassMaster", DBConnection.OpenConnection());
            try
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 0;
                sqlCommand.Parameters.Add("@ExecutiveCode", SqlDbType.VarChar, 50).Value = (object)this.strExecutiveCode;
                sqlCommand.Parameters.Add("@Region", SqlDbType.VarChar, 50).Value = (object)this.strRegion;
                sqlCommand.Parameters.Add("@DealerCode", SqlDbType.VarChar, 50).Value = (object)this.strDealerCode;
                sqlCommand.Parameters.Add("@CustCode", SqlDbType.VarChar, 50).Value = (object)this.strCustCode;
                sqlCommand.Parameters.Add("@State", SqlDbType.VarChar, 50).Value = (object)this.strState;
                sqlCommand.Parameters.Add("@CustomerGroup", SqlDbType.VarChar, 50).Value = (object)this.strCustGroup;
                sqlCommand.Connection.Open();
                sqlDataReader = sqlCommand.ExecuteReader();
            }
            catch (Exception ex)
            {
                if (sqlConnection.State == ConnectionState.Open)
                    sqlConnection.Close();
            }
            return sqlDataReader;
        }

        public SqlDataReader funPriceAndMarginChange()
        {
            SqlDataReader sqlDataReader = (SqlDataReader)null;
            SqlConnection sqlConnection = DBConnection.OpenConnection();
            SqlCommand sqlCommand = new SqlCommand("PrcPriceAndMarginChange", DBConnection.OpenConnection());
            try
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 0;
                sqlCommand.Parameters.Add("@ExecutiveCode", SqlDbType.VarChar, 50).Value = (object)this.strExecutiveCode;
                sqlCommand.Parameters.Add("@Region", SqlDbType.VarChar, 50).Value = (object)this.strRegion;
                sqlCommand.Parameters.Add("@DealerCode", SqlDbType.VarChar, 50).Value = (object)this.strDealerCode;
                sqlCommand.Parameters.Add("@CustCode", SqlDbType.VarChar, 50).Value = (object)this.strCustCode;
                sqlCommand.Parameters.Add("@State", SqlDbType.VarChar, 50).Value = (object)this.strState;
                sqlCommand.Parameters.Add("@CustomerGroup", SqlDbType.VarChar, 50).Value = (object)this.strCustGroup;
                sqlCommand.Connection.Open();
                sqlDataReader = sqlCommand.ExecuteReader();
            }
            catch (Exception ex)
            {
                if (sqlConnection.State == ConnectionState.Open)
                    sqlConnection.Close();
            }
            return sqlDataReader;
        }

    }
}
