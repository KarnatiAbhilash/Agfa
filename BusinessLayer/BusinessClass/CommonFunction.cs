using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using Common;
using System.Drawing;
using System.Globalization;
using System.Web;
using System.Web.SessionState;
using System.Reflection;

namespace BusinessClass
{
    public class CommonFunction
    {
        public static void UnPopulateRecords(DropDownList ddlItems)
        {
            ddlItems.Items.Clear();
        }

        public static void PopulateRecords(string strTableName, string strDataTextField, string strDataValueField, string strOrderBy, DropDownList ddlItems)
        {
            SqlDataReader sdrPopulateRecords = null;
            SqlCommand cmdPopulateRecords = new SqlCommand("prcPopulateRecordsOrder", DBConnection.OpenConnection());
            int intIndex = 1;
            try
            {
                cmdPopulateRecords.CommandType = CommandType.StoredProcedure;
                cmdPopulateRecords.Parameters.Add("@TableName", SqlDbType.VarChar, 50).Value = strTableName;
                cmdPopulateRecords.Parameters.Add("@DataTextField", SqlDbType.VarChar, 50).Value = strDataTextField;
                cmdPopulateRecords.Parameters.Add("@DataValueField", SqlDbType.VarChar, 50).Value = strDataValueField;
                cmdPopulateRecords.Parameters.Add("@OrderBy", SqlDbType.VarChar, 50).Value = strOrderBy;
                cmdPopulateRecords.Connection.Open();
                sdrPopulateRecords = cmdPopulateRecords.ExecuteReader();

                ddlItems.Items.Clear();
                //ddlItems.DropDownWidth = 210;
                ddlItems.Items.Insert(0, new ListItem("<-- Select One -->", "0"));
                while (sdrPopulateRecords.Read())
                {
                    ListItem liItems = new ListItem();
                    liItems.Text = sdrPopulateRecords.GetValue(0).ToString();
                    liItems.Value = sdrPopulateRecords.GetValue(1).ToString();
                    ddlItems.Items.Insert(intIndex, liItems);
                    intIndex += 1;
                }
                cmdPopulateRecords.Connection.Close();
            }
            catch (Exception)
            {
                if (cmdPopulateRecords.Connection.State == ConnectionState.Open) cmdPopulateRecords.Connection.Close();
            }
            finally
            {
                if (sdrPopulateRecords != null) sdrPopulateRecords.Close();
                sdrPopulateRecords = null;
            }
        }

        public static void PopulateRecords(string strTableName, string strDataTextField, string strDataValueField, string strOrderBy, DropDownList ddlItems, string strSelectOne)
        {
            SqlDataReader sdrPopulateRecords = null;
            SqlCommand cmdPopulateRecords = new SqlCommand("prcPopulateRecordsOrder", DBConnection.OpenConnection());
            int intIndex = 1;
            try
            {
                cmdPopulateRecords.CommandType = CommandType.StoredProcedure;
                cmdPopulateRecords.Parameters.Add("@TableName", SqlDbType.VarChar, 50).Value = strTableName;
                cmdPopulateRecords.Parameters.Add("@DataTextField", SqlDbType.VarChar, 50).Value = strDataTextField;
                cmdPopulateRecords.Parameters.Add("@DataValueField", SqlDbType.VarChar, 50).Value = strDataValueField;
                cmdPopulateRecords.Parameters.Add("@OrderBy", SqlDbType.VarChar, 50).Value = strOrderBy;
                cmdPopulateRecords.Connection.Open();
                sdrPopulateRecords = cmdPopulateRecords.ExecuteReader();

                ddlItems.Items.Clear();
                //ddlItems.DropDownWidth = 210;
                ddlItems.Items.Insert(0, new ListItem(strSelectOne, "0"));
                while (sdrPopulateRecords.Read())
                {
                    ListItem liItems = new ListItem();
                    liItems.Text = sdrPopulateRecords.GetValue(0).ToString();
                    liItems.Value = sdrPopulateRecords.GetValue(1).ToString();
                    ddlItems.Items.Insert(intIndex, liItems);
                    intIndex += 1;
                }
                cmdPopulateRecords.Connection.Close();
            }
            catch (Exception)
            {
                if (cmdPopulateRecords.Connection.State == ConnectionState.Open) cmdPopulateRecords.Connection.Close();
            }
            finally
            {
                if (sdrPopulateRecords != null) sdrPopulateRecords.Close();
                sdrPopulateRecords = null;
            }
        }

        public static void PopulateRecordsWithOneParam(string strTableName, string strDataTextField, string strDataValueField, string strParamName, string strParamValue, string strOrderBy, DropDownList ddlItems, string strSelectOne)
        {
            SqlDataReader sdrPopulateRecords = null;
            SqlCommand cmdPopulateRecords = new SqlCommand("prcPopulateRecordsWithOneParam", DBConnection.OpenConnection());
            int intIndex = 1;
            try
            {
                cmdPopulateRecords.CommandType = CommandType.StoredProcedure;
                cmdPopulateRecords.Parameters.Add("@TableName", SqlDbType.VarChar, 50).Value = strTableName;
                cmdPopulateRecords.Parameters.Add("@DataTextField", SqlDbType.VarChar, 50).Value = strDataTextField;
                cmdPopulateRecords.Parameters.Add("@DataValueField", SqlDbType.VarChar, 50).Value = strDataValueField;
                cmdPopulateRecords.Parameters.Add("@ParamName", SqlDbType.VarChar, 50).Value = strParamName;
                cmdPopulateRecords.Parameters.Add("@ParamValue", SqlDbType.VarChar, 200).Value = strParamValue;
                cmdPopulateRecords.Parameters.Add("@OrderBy", SqlDbType.VarChar, 50).Value = strOrderBy;
                cmdPopulateRecords.Connection.Open();
                sdrPopulateRecords = cmdPopulateRecords.ExecuteReader();

                ddlItems.Items.Clear();
                //ddlItems.DropDownWidth = 210;
                ddlItems.Items.Insert(0, new ListItem(strSelectOne, "0"));
                while (sdrPopulateRecords.Read())
                {
                    ListItem liItems = new ListItem();
                    liItems.Text = sdrPopulateRecords.GetValue(0).ToString();
                    liItems.Value = sdrPopulateRecords.GetValue(1).ToString();
                    ddlItems.Items.Insert(intIndex, liItems);
                    intIndex += 1;
                }
                cmdPopulateRecords.Connection.Close();
            }
            catch (Exception)
            {
                if (cmdPopulateRecords.Connection.State == ConnectionState.Open) cmdPopulateRecords.Connection.Close();
            }
            finally
            {
                if (sdrPopulateRecords != null) sdrPopulateRecords.Close();
                sdrPopulateRecords = null;
            }

        }

        public static void PopulateRecordsWithOneParam(string strTableName, string strDataTextField, string strDataValueField, string strParamName, string strParamValue, string strOrderBy, DropDownList ddlItems)
        {
            SqlDataReader sdrPopulateRecords = null;
            SqlCommand cmdPopulateRecords = new SqlCommand("prcPopulateRecordsWithOneParam", DBConnection.OpenConnection());
            int intIndex = 1;
            try
            {
                cmdPopulateRecords.CommandType = CommandType.StoredProcedure;
                cmdPopulateRecords.Parameters.Add("@TableName", SqlDbType.VarChar, 50).Value = strTableName;
                cmdPopulateRecords.Parameters.Add("@DataTextField", SqlDbType.VarChar, 50).Value = strDataTextField;
                cmdPopulateRecords.Parameters.Add("@DataValueField", SqlDbType.VarChar, 50).Value = strDataValueField;
                cmdPopulateRecords.Parameters.Add("@ParamName", SqlDbType.VarChar, 50).Value = strParamName;
                cmdPopulateRecords.Parameters.Add("@ParamValue", SqlDbType.VarChar, 200).Value = strParamValue;
                cmdPopulateRecords.Parameters.Add("@OrderBy", SqlDbType.VarChar, 50).Value = strOrderBy;
                cmdPopulateRecords.Connection.Open();
                sdrPopulateRecords = cmdPopulateRecords.ExecuteReader();

                ddlItems.Items.Clear();
                //ddlItems.DropDownWidth = 210;
                ddlItems.Items.Insert(0, new ListItem("<-- Select One -->", "0"));
                while (sdrPopulateRecords.Read())
                {
                    ListItem liItems = new ListItem();
                    liItems.Text = sdrPopulateRecords.GetValue(0).ToString();
                    liItems.Value = sdrPopulateRecords.GetValue(1).ToString();
                    ddlItems.Items.Insert(intIndex, liItems);
                    intIndex += 1;
                }
                cmdPopulateRecords.Connection.Close();
            }
            catch (Exception)
            {
                if (cmdPopulateRecords.Connection.State == ConnectionState.Open) cmdPopulateRecords.Connection.Close();
            }
            finally
            {
                if (sdrPopulateRecords != null) sdrPopulateRecords.Close();
                sdrPopulateRecords = null;
            }

        }

        public static void PopulateRecordsWithTwoParam(string strTableName, string strDataTextField, string strDataValueField, string strParamName, string strParamValue, string strParam1Name, string strParam1Value, string strOrderBy, DropDownList ddlItems)
        {
            SqlDataReader sdrPopulateRecords = null;
            SqlCommand cmdPopulateRecords = new SqlCommand("prcPopulateRecordsWithTwoParam", DBConnection.OpenConnection());
            int intIndex = 1;
            try
            {
                cmdPopulateRecords.CommandType = CommandType.StoredProcedure;
                cmdPopulateRecords.Parameters.Add("@TableName", SqlDbType.VarChar, 50).Value = strTableName;
                cmdPopulateRecords.Parameters.Add("@DataTextField", SqlDbType.VarChar, 50).Value = strDataTextField;
                cmdPopulateRecords.Parameters.Add("@DataValueField", SqlDbType.VarChar, 50).Value = strDataValueField;
                cmdPopulateRecords.Parameters.Add("@ParamName", SqlDbType.VarChar, 50).Value = strParamName;
                cmdPopulateRecords.Parameters.Add("@ParamValue", SqlDbType.VarChar, 200).Value = strParamValue;
                cmdPopulateRecords.Parameters.Add("@Param1Name", SqlDbType.VarChar, 50).Value = strParam1Name;
                cmdPopulateRecords.Parameters.Add("@Param1Value", SqlDbType.VarChar, 200).Value = strParam1Value;
                cmdPopulateRecords.Parameters.Add("@OrderBy", SqlDbType.VarChar, 50).Value = strOrderBy;
                cmdPopulateRecords.Connection.Open();
                sdrPopulateRecords = cmdPopulateRecords.ExecuteReader();

                ddlItems.Items.Clear();
                //ddlItems.DropDownWidth = 210;
                ddlItems.Items.Insert(0, new ListItem("<-- Select One -->", "0"));
                while (sdrPopulateRecords.Read())
                {
                    ListItem liItems = new ListItem();
                    liItems.Text = sdrPopulateRecords.GetValue(0).ToString();
                    liItems.Value = sdrPopulateRecords.GetValue(1).ToString();
                    ddlItems.Items.Insert(intIndex, liItems);
                    intIndex += 1;
                }
                cmdPopulateRecords.Connection.Close();
            }
            catch (Exception)
            {
                if (cmdPopulateRecords.Connection.State == ConnectionState.Open) cmdPopulateRecords.Connection.Close();
            }
            finally
            {
                if (sdrPopulateRecords != null) sdrPopulateRecords.Close();
                sdrPopulateRecords = null;
            }

        }

        public static void PopulateRecordsWithTwoParam(string strTableName, string strDataTextField, string strDataValueField, string strParamName, string strParamValue, string strParam1Name, string strParam1Value, string strOrderBy, DropDownList ddlItems, string strSelectOne)
        {
            SqlDataReader sdrPopulateRecords = null;
            SqlCommand cmdPopulateRecords = new SqlCommand("prcPopulateRecordsWithTwoParam", DBConnection.OpenConnection());
            int intIndex = 1;
            try
            {
                cmdPopulateRecords.CommandType = CommandType.StoredProcedure;
                cmdPopulateRecords.Parameters.Add("@TableName", SqlDbType.VarChar, 50).Value = strTableName;
                cmdPopulateRecords.Parameters.Add("@DataTextField", SqlDbType.VarChar, 50).Value = strDataTextField;
                cmdPopulateRecords.Parameters.Add("@DataValueField", SqlDbType.VarChar, 50).Value = strDataValueField;
                cmdPopulateRecords.Parameters.Add("@ParamName", SqlDbType.VarChar, 50).Value = strParamName;
                cmdPopulateRecords.Parameters.Add("@ParamValue", SqlDbType.VarChar, 200).Value = strParamValue;
                cmdPopulateRecords.Parameters.Add("@Param1Name", SqlDbType.VarChar, 50).Value = strParam1Name;
                cmdPopulateRecords.Parameters.Add("@Param1Value", SqlDbType.VarChar, 200).Value = strParam1Value;
                cmdPopulateRecords.Parameters.Add("@OrderBy", SqlDbType.VarChar, 50).Value = strOrderBy;
                cmdPopulateRecords.Connection.Open();
                sdrPopulateRecords = cmdPopulateRecords.ExecuteReader();

                ddlItems.Items.Clear();
                //ddlItems.DropDownWidth = 210;
                ddlItems.Items.Insert(0, new ListItem(strSelectOne, "0"));
                while (sdrPopulateRecords.Read())
                {
                    ListItem liItems = new ListItem();
                    liItems.Text = sdrPopulateRecords.GetValue(0).ToString();
                    liItems.Value = sdrPopulateRecords.GetValue(1).ToString();
                    ddlItems.Items.Insert(intIndex, liItems);
                    intIndex += 1;
                }
                cmdPopulateRecords.Connection.Close();
            }
            catch (Exception)
            {
                if (cmdPopulateRecords.Connection.State == ConnectionState.Open) cmdPopulateRecords.Connection.Close();
            }
            finally
            {
                if (sdrPopulateRecords != null) sdrPopulateRecords.Close();
                sdrPopulateRecords = null;
            }

        }

        public static void PopulateRecordsWithThreeParam(string strTableName, string strDataTextField, string strDataValueField, string strParamName, string strParamValue, string strParam1Name, string strParam1Value, string strParam2Name, string strParam2Value, string strOrderBy, DropDownList ddlItems)
        {
            SqlDataReader sdrPopulateRecords = null;
            SqlCommand cmdPopulateRecords = new SqlCommand("prcPopulateRecordsWithThreeParam", DBConnection.OpenConnection());
            int intIndex = 1;
            try
            {
                cmdPopulateRecords.CommandType = CommandType.StoredProcedure;
                cmdPopulateRecords.Parameters.Add("@TableName", SqlDbType.VarChar, 50).Value = strTableName;
                cmdPopulateRecords.Parameters.Add("@DataTextField", SqlDbType.VarChar, 50).Value = strDataTextField;
                cmdPopulateRecords.Parameters.Add("@DataValueField", SqlDbType.VarChar, 50).Value = strDataValueField;
                cmdPopulateRecords.Parameters.Add("@ParamName", SqlDbType.VarChar, 50).Value = strParamName;
                cmdPopulateRecords.Parameters.Add("@ParamValue", SqlDbType.VarChar, 200).Value = strParamValue;
                cmdPopulateRecords.Parameters.Add("@Param1Name", SqlDbType.VarChar, 50).Value = strParam1Name;
                cmdPopulateRecords.Parameters.Add("@Param1Value", SqlDbType.VarChar, 200).Value = strParam1Value;
                cmdPopulateRecords.Parameters.Add("@Param2Name", SqlDbType.VarChar, 50).Value = strParam2Name;
                cmdPopulateRecords.Parameters.Add("@Param2Value", SqlDbType.VarChar, 200).Value = strParam2Value;
                cmdPopulateRecords.Parameters.Add("@OrderBy", SqlDbType.VarChar, 50).Value = strOrderBy;
                cmdPopulateRecords.Connection.Open();
                sdrPopulateRecords = cmdPopulateRecords.ExecuteReader();

                ddlItems.Items.Clear();
                //ddlItems.DropDownWidth = 210;
                ddlItems.Items.Insert(0, new ListItem("<-- Select One -->", "0"));
                while (sdrPopulateRecords.Read())
                {
                    ListItem liItems = new ListItem();
                    liItems.Text = sdrPopulateRecords.GetValue(0).ToString();
                    liItems.Value = sdrPopulateRecords.GetValue(1).ToString();
                    ddlItems.Items.Insert(intIndex, liItems);
                    intIndex += 1;
                }
                cmdPopulateRecords.Connection.Close();
            }
            catch (Exception)
            {
                if (cmdPopulateRecords.Connection.State == ConnectionState.Open) cmdPopulateRecords.Connection.Close();
            }
            finally
            {
                if (sdrPopulateRecords != null) sdrPopulateRecords.Close();
                sdrPopulateRecords = null;
            }

        }

        public static void PopulateRecordsWithFourParam(string strTableName, string strDataTextField, string strDataValueField, string strParamName, string strParamValue, string strParam1Name, string strParam1Value, string strParam2Name, string strParam2Value, string strParam3Name, string strParam3Value, string strOrderBy, DropDownList ddlItems)
        {
            SqlDataReader sdrPopulateRecords = null;
            SqlCommand cmdPopulateRecords = new SqlCommand("prcPopulateRecordsWithFourParam", DBConnection.OpenConnection());
            int intIndex = 1;
            try
            {
                cmdPopulateRecords.CommandType = CommandType.StoredProcedure;
                cmdPopulateRecords.Parameters.Add("@TableName", SqlDbType.VarChar, 50).Value = strTableName;
                cmdPopulateRecords.Parameters.Add("@DataTextField", SqlDbType.VarChar, 50).Value = strDataTextField;
                cmdPopulateRecords.Parameters.Add("@DataValueField", SqlDbType.VarChar, 50).Value = strDataValueField;
                cmdPopulateRecords.Parameters.Add("@ParamName", SqlDbType.VarChar, 50).Value = strParamName;
                cmdPopulateRecords.Parameters.Add("@ParamValue", SqlDbType.VarChar, 200).Value = strParamValue;
                cmdPopulateRecords.Parameters.Add("@Param1Name", SqlDbType.VarChar, 50).Value = strParam1Name;
                cmdPopulateRecords.Parameters.Add("@Param1Value", SqlDbType.VarChar, 200).Value = strParam1Value;
                cmdPopulateRecords.Parameters.Add("@Param2Name", SqlDbType.VarChar, 50).Value = strParam2Name;
                cmdPopulateRecords.Parameters.Add("@Param2Value", SqlDbType.VarChar, 200).Value = strParam2Value;
                cmdPopulateRecords.Parameters.Add("@Param3Name", SqlDbType.VarChar, 50).Value = strParam3Name;
                cmdPopulateRecords.Parameters.Add("@Param3Value", SqlDbType.VarChar, 200).Value = strParam3Value;
                cmdPopulateRecords.Parameters.Add("@OrderBy", SqlDbType.VarChar, 50).Value = strOrderBy;
                cmdPopulateRecords.Connection.Open();
                sdrPopulateRecords = cmdPopulateRecords.ExecuteReader();

                ddlItems.Items.Clear();
                //ddlItems.DropDownWidth = 210;
                ddlItems.Items.Insert(0, new ListItem("<-- Select One -->", "0"));
                while (sdrPopulateRecords.Read())
                {
                    ListItem liItems = new ListItem();
                    liItems.Text = sdrPopulateRecords.GetValue(0).ToString();
                    liItems.Value = sdrPopulateRecords.GetValue(1).ToString();
                    ddlItems.Items.Insert(intIndex, liItems);
                    intIndex += 1;
                }
                cmdPopulateRecords.Connection.Close();
            }
            catch (Exception)
            {
                if (cmdPopulateRecords.Connection.State == ConnectionState.Open) cmdPopulateRecords.Connection.Close();
            }
            finally
            {
                if (sdrPopulateRecords != null) sdrPopulateRecords.Close();
                sdrPopulateRecords = null;
            }

        }

        public static void PopulateRecordsWithQuery(string strQuery, string strOrderBy, DropDownList ddlItems)
        {
            SqlDataReader sdrPopulateRecords = null;
            SqlCommand cmdPopulateRecords = new SqlCommand("prcPopulateRecordsWithQuery", DBConnection.OpenConnection());
            int intIndex = 1;
            try
            {
                cmdPopulateRecords.CommandType = CommandType.StoredProcedure;
                cmdPopulateRecords.Parameters.Add("@Query", SqlDbType.VarChar, 7500).Value = strQuery;
                cmdPopulateRecords.Parameters.Add("@OrderBy", SqlDbType.VarChar, 50).Value = strOrderBy;
                cmdPopulateRecords.Connection.Open();
                sdrPopulateRecords = cmdPopulateRecords.ExecuteReader();

                ddlItems.Items.Clear();
                //ddlItems.DropDownWidth = 210;
                ddlItems.Items.Insert(0, new ListItem("<-- Select One -->", "0"));
                while (sdrPopulateRecords.Read())
                {
                    ListItem liItems = new ListItem();
                    liItems.Text = sdrPopulateRecords.GetValue(0).ToString();
                    liItems.Value = sdrPopulateRecords.GetValue(1).ToString();
                    ddlItems.Items.Insert(intIndex, liItems);
                    intIndex += 1;
                }
                cmdPopulateRecords.Connection.Close();
            }
            catch (Exception)
            {
                if (cmdPopulateRecords.Connection.State == ConnectionState.Open) cmdPopulateRecords.Connection.Close();
            }
            finally
            {
                if (sdrPopulateRecords != null) sdrPopulateRecords.Close();
                sdrPopulateRecords = null;
            }

        }

        public static DataSet FetchRecords(string strTableName, string strOrderBy)
        {
            DataSet dsFetch = new DataSet();
            SqlCommand cmdFetchRecords = new SqlCommand("prcFetchRecords", DBConnection.OpenConnection());
            cmdFetchRecords.CommandType = CommandType.StoredProcedure;
            cmdFetchRecords.Parameters.Add("@TableName", SqlDbType.VarChar, 50).Value = strTableName;
            cmdFetchRecords.Parameters.Add("@OrderBy", SqlDbType.VarChar, 200).Value = strOrderBy;
            SqlDataAdapter sdc = new SqlDataAdapter(cmdFetchRecords);
            sdc.Fill(dsFetch);
            return dsFetch;
        }

        public static DataSet FetchRecordsWithQuery(string strQuery, string strOrderBy)
        {
            DataSet dsFetch = new DataSet();
            SqlCommand cmdFetchRecords = new SqlCommand("prcPopulateRecordsWithQuery", DBConnection.OpenConnection());
            cmdFetchRecords.CommandType = CommandType.StoredProcedure;
            cmdFetchRecords.Parameters.Add("@Query", SqlDbType.VarChar, 7500).Value = strQuery;
            cmdFetchRecords.Parameters.Add("@OrderBy", SqlDbType.VarChar, 50).Value = strOrderBy;
            SqlDataAdapter sdc = new SqlDataAdapter(cmdFetchRecords);
            sdc.Fill(dsFetch);
            return dsFetch;
        }

        public static DataSet FetchRecordsWithOneParam(string strTableName, string strParamName, string strParamValue, string strOrderBy)
        {
            DataSet dsFetch = new DataSet();
            SqlCommand cmdFetchRecords = new SqlCommand("prcFetchRecordsWithOneParam", DBConnection.OpenConnection());
            cmdFetchRecords.CommandType = CommandType.StoredProcedure;
            cmdFetchRecords.Parameters.Add("@TableName", SqlDbType.VarChar, 50).Value = strTableName;
            cmdFetchRecords.Parameters.Add("@ParamName", SqlDbType.VarChar, 50).Value = strParamName;
            cmdFetchRecords.Parameters.Add("@ParamValue", SqlDbType.VarChar, 200).Value = strParamValue;
            cmdFetchRecords.Parameters.Add("@OrderBy", SqlDbType.VarChar, 200).Value = strOrderBy;
            SqlDataAdapter sdc = new SqlDataAdapter(cmdFetchRecords);
            sdc.Fill(dsFetch);
            return dsFetch;
        }

        public static DataSet FetchRecordsWithTwoParam(string strTableName, string strParamName, string strParamValue, string strParam1Name, string strParam1Value, string strOrderBy)
        {
            DataSet dsFetch = new DataSet();
            SqlCommand cmdFetchRecords = new SqlCommand("prcFetchRecordsWithTwoParam", DBConnection.OpenConnection());
            cmdFetchRecords.CommandType = CommandType.StoredProcedure;
            cmdFetchRecords.Parameters.Add("@TableName", SqlDbType.VarChar, 50).Value = strTableName;
            cmdFetchRecords.Parameters.Add("@ParamName", SqlDbType.VarChar, 50).Value = strParamName;
            cmdFetchRecords.Parameters.Add("@ParamValue", SqlDbType.VarChar, 200).Value = strParamValue;
            cmdFetchRecords.Parameters.Add("@Param1Name", SqlDbType.VarChar, 50).Value = strParam1Name;
            cmdFetchRecords.Parameters.Add("@Param1Value", SqlDbType.VarChar, 200).Value = strParam1Value;
            cmdFetchRecords.Parameters.Add("@OrderBy", SqlDbType.VarChar, 200).Value = strOrderBy;
            SqlDataAdapter sdc = new SqlDataAdapter(cmdFetchRecords);
            sdc.Fill(dsFetch);
            return dsFetch;
        }

        public static DataSet FetchRecordsWithTwoParam(string strTableName, string strParamName, string strOper, string strParamValue, string strParam1Name, string strOper1, string strParam1Value, string strOrderBy)
        {
            DataSet dsFetch = new DataSet();
            SqlCommand cmdFetchRecords = new SqlCommand("prcFetchRecordsWithTwoParamOperator", DBConnection.OpenConnection());
            cmdFetchRecords.CommandType = CommandType.StoredProcedure;
            cmdFetchRecords.Parameters.Add("@TableName", SqlDbType.VarChar, 50).Value = strTableName;
            cmdFetchRecords.Parameters.Add("@ParamName", SqlDbType.VarChar, 50).Value = strParamName;
            cmdFetchRecords.Parameters.Add("@Oper", SqlDbType.VarChar, 50).Value = strOper;
            cmdFetchRecords.Parameters.Add("@ParamValue", SqlDbType.VarChar, 200).Value = strParamValue;
            cmdFetchRecords.Parameters.Add("@Param1Name", SqlDbType.VarChar, 50).Value = strParam1Name;
            cmdFetchRecords.Parameters.Add("@Oper1", SqlDbType.VarChar, 50).Value = strOper1;
            cmdFetchRecords.Parameters.Add("@Param1Value", SqlDbType.VarChar, 200).Value = strParam1Value;
            cmdFetchRecords.Parameters.Add("@OrderBy", SqlDbType.VarChar, 200).Value = strOrderBy;
            SqlDataAdapter sdc = new SqlDataAdapter(cmdFetchRecords);
            sdc.Fill(dsFetch);
            return dsFetch;
        }

        public static DataSet FetchRecordsWithThreeParam(string strTableName, string strParamName, string strParamValue, string strParam1Name, string strParam1Value, string strParam2Name, string strParam2Value, string strOrderBy)
        {
            DataSet dsFetch = new DataSet();
            SqlCommand cmdFetchRecords = new SqlCommand("prcFetchRecordsWithThreeParam", DBConnection.OpenConnection());
            cmdFetchRecords.CommandType = CommandType.StoredProcedure;
            cmdFetchRecords.Parameters.Add("@TableName", SqlDbType.VarChar, 50).Value = strTableName;
            cmdFetchRecords.Parameters.Add("@ParamName", SqlDbType.VarChar, 50).Value = strParamName;
            cmdFetchRecords.Parameters.Add("@ParamValue", SqlDbType.VarChar, 200).Value = strParamValue;
            cmdFetchRecords.Parameters.Add("@Param1Name", SqlDbType.VarChar, 50).Value = strParam1Name;
            cmdFetchRecords.Parameters.Add("@Param1Value", SqlDbType.VarChar, 200).Value = strParam1Value;
            cmdFetchRecords.Parameters.Add("@Param2Name", SqlDbType.VarChar, 50).Value = strParam2Name;
            cmdFetchRecords.Parameters.Add("@Param2Value", SqlDbType.VarChar, 200).Value = strParam2Value;
            cmdFetchRecords.Parameters.Add("@OrderBy", SqlDbType.VarChar, 200).Value = strOrderBy;
            SqlDataAdapter sdc = new SqlDataAdapter(cmdFetchRecords);
            sdc.Fill(dsFetch);
            return dsFetch;
        }

        public static DataSet FetchRecordsWithThreeParam(string strTableName, string strParamName, string strOper, string strParamValue, string strParam1Name, string strOper1, string strParam1Value, string strParam2Name, string strOper2, string strParam2Value, string strOrderBy)
        {
            DataSet dsFetch = new DataSet();
            SqlCommand cmdFetchRecords = new SqlCommand("prcFetchRecordsWithThreeParamOperator", DBConnection.OpenConnection());
            cmdFetchRecords.CommandType = CommandType.StoredProcedure;
            cmdFetchRecords.Parameters.Add("@TableName", SqlDbType.VarChar, 50).Value = strTableName;
            cmdFetchRecords.Parameters.Add("@ParamName", SqlDbType.VarChar, 50).Value = strParamName;
            cmdFetchRecords.Parameters.Add("@Oper", SqlDbType.VarChar, 50).Value = strOper;
            cmdFetchRecords.Parameters.Add("@ParamValue", SqlDbType.VarChar, 200).Value = strParamValue;
            cmdFetchRecords.Parameters.Add("@Param1Name", SqlDbType.VarChar, 50).Value = strParam1Name;
            cmdFetchRecords.Parameters.Add("@Oper1", SqlDbType.VarChar, 50).Value = strOper1;
            cmdFetchRecords.Parameters.Add("@Param1Value", SqlDbType.VarChar, 200).Value = strParam1Value;
            cmdFetchRecords.Parameters.Add("@Param2Name", SqlDbType.VarChar, 50).Value = strParam2Name;
            cmdFetchRecords.Parameters.Add("@Oper2", SqlDbType.VarChar, 50).Value = strOper2;
            cmdFetchRecords.Parameters.Add("@Param2Value", SqlDbType.VarChar, 200).Value = strParam2Value;
            cmdFetchRecords.Parameters.Add("@OrderBy", SqlDbType.VarChar, 200).Value = strOrderBy;
            SqlDataAdapter sdc = new SqlDataAdapter(cmdFetchRecords);
            sdc.Fill(dsFetch);
            return dsFetch;
        }

        public static DataSet SearchRecordsWithOneParam(string strTableName, string strParamName, string strParamValue, string strOrderBy)
        {
            DataSet dsFetch = new DataSet();
            SqlCommand cmdFetchRecords = new SqlCommand("prcSearchRecordsWithOneParam", DBConnection.OpenConnection());
            cmdFetchRecords.CommandType = CommandType.StoredProcedure;
            cmdFetchRecords.Parameters.Add("@TableName", SqlDbType.VarChar, 50).Value = strTableName;
            cmdFetchRecords.Parameters.Add("@ParamName", SqlDbType.VarChar, 50).Value = strParamName;
            cmdFetchRecords.Parameters.Add("@ParamValue", SqlDbType.VarChar, 200).Value = strParamValue;
            cmdFetchRecords.Parameters.Add("@OrderBy", SqlDbType.VarChar, 200).Value = strOrderBy;
            SqlDataAdapter sdc = new SqlDataAdapter(cmdFetchRecords);
            sdc.Fill(dsFetch);
            return dsFetch;
        }

        public static DataSet SearchRecordsWithTwoParam(string strTableName, string strParamName, string strParamValue, string strParam1Name, string strParam1Value, string strOrderBy)
        {
            DataSet dsFetch = new DataSet();
            SqlCommand cmdFetchRecords = new SqlCommand("prcSearchRecordsWithTwoParam", DBConnection.OpenConnection());
            cmdFetchRecords.CommandType = CommandType.StoredProcedure;
            cmdFetchRecords.Parameters.Add("@TableName", SqlDbType.VarChar, 50).Value = strTableName;
            cmdFetchRecords.Parameters.Add("@ParamName", SqlDbType.VarChar, 50).Value = strParamName;
            cmdFetchRecords.Parameters.Add("@ParamValue", SqlDbType.VarChar, 200).Value = strParamValue;
            cmdFetchRecords.Parameters.Add("@Param1Name", SqlDbType.VarChar, 50).Value = strParam1Name;
            cmdFetchRecords.Parameters.Add("@Param1Value", SqlDbType.VarChar, 200).Value = strParam1Value;
            cmdFetchRecords.Parameters.Add("@OrderBy", SqlDbType.VarChar, 200).Value = strOrderBy;
            SqlDataAdapter sdc = new SqlDataAdapter(cmdFetchRecords);
            sdc.Fill(dsFetch);
            return dsFetch;
        }

        public static DataSet SearchRecordsWithThreeParam(string strTableName, string strParamName, string strParamValue, string strParam1Name, string strParam1Value, string strParam2Name, string strParam2Value, string strOrderBy)
        {
            DataSet dsFetch = new DataSet();
            SqlCommand cmdFetchRecords = new SqlCommand("prcSearchRecordsWithThreeParam", DBConnection.OpenConnection());
            cmdFetchRecords.CommandType = CommandType.StoredProcedure;
            cmdFetchRecords.Parameters.Add("@TableName", SqlDbType.VarChar, 50).Value = strTableName;
            cmdFetchRecords.Parameters.Add("@ParamName", SqlDbType.VarChar, 50).Value = strParamName;
            cmdFetchRecords.Parameters.Add("@ParamValue", SqlDbType.VarChar, 200).Value = strParamValue;
            cmdFetchRecords.Parameters.Add("@Param1Name", SqlDbType.VarChar, 50).Value = strParam1Name;
            cmdFetchRecords.Parameters.Add("@Param1Value", SqlDbType.VarChar, 200).Value = strParam1Value;
            cmdFetchRecords.Parameters.Add("@Param2Name", SqlDbType.VarChar, 50).Value = strParam2Name;
            cmdFetchRecords.Parameters.Add("@Param2Value", SqlDbType.VarChar, 200).Value = strParam2Value;
            cmdFetchRecords.Parameters.Add("@OrderBy", SqlDbType.VarChar, 200).Value = strOrderBy;
            SqlDataAdapter sdc = new SqlDataAdapter(cmdFetchRecords);
            sdc.Fill(dsFetch);
            return dsFetch;
        }

        public static DataSet SearchRecordsWithFourParam(string strTableName, string strParamName, string strParamValue, string strParam1Name, string strParam1Value, string strParam2Name, string strParam2Value, string strParam3Name, string strParam3Value, string strOrderBy)
        {
            DataSet dsFetch = new DataSet();
            SqlCommand cmdFetchRecords = new SqlCommand("prcSearchRecordsWithFourParam", DBConnection.OpenConnection());
            cmdFetchRecords.CommandType = CommandType.StoredProcedure;
            cmdFetchRecords.Parameters.Add("@TableName", SqlDbType.VarChar, 50).Value = strTableName;
            cmdFetchRecords.Parameters.Add("@ParamName", SqlDbType.VarChar, 50).Value = strParamName;
            cmdFetchRecords.Parameters.Add("@ParamValue", SqlDbType.VarChar, 200).Value = strParamValue;
            cmdFetchRecords.Parameters.Add("@Param1Name", SqlDbType.VarChar, 50).Value = strParam1Name;
            cmdFetchRecords.Parameters.Add("@Param1Value", SqlDbType.VarChar, 200).Value = strParam1Value;
            cmdFetchRecords.Parameters.Add("@Param2Name", SqlDbType.VarChar, 50).Value = strParam2Name;
            cmdFetchRecords.Parameters.Add("@Param2Value", SqlDbType.VarChar, 200).Value = strParam2Value;
            cmdFetchRecords.Parameters.Add("@Param3Name", SqlDbType.VarChar, 50).Value = strParam3Name;
            cmdFetchRecords.Parameters.Add("@Param3Value", SqlDbType.VarChar, 200).Value = strParam3Value;
            cmdFetchRecords.Parameters.Add("@OrderBy", SqlDbType.VarChar, 200).Value = strOrderBy;
            SqlDataAdapter sdc = new SqlDataAdapter(cmdFetchRecords);
            sdc.Fill(dsFetch);
            return dsFetch;
        }


        public bool TryDateFormateConversion(string InputDate, string strDateFormat)
        {
            try
            {
                DateTime OutputDate;
                if (strDateFormat.ToUpper() == "MM/DD/YYYY")
                    OutputDate = DateTime.Parse(InputDate);
                else
                {
                    DateTimeFormatInfo DateFormate = new CultureInfo("en-GB", false).DateTimeFormat;
                    OutputDate = Convert.ToDateTime(InputDate, DateFormate);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public DateTime DateFormateConversion(string InputDate, string strDateFormat)
        {
            if (strDateFormat.ToUpper() == "MM/DD/YYYY")
                return DateTime.Parse(InputDate);

            DateTime OutputDate;
            DateTimeFormatInfo DateFormate = new CultureInfo("en-GB", false).DateTimeFormat;
            OutputDate = Convert.ToDateTime(InputDate, DateFormate);
            return OutputDate;
        }

        public static void FetchGetMonthYear(int IsMonth, DropDownList ddl)
        {
            DataSet dsFetch = new DataSet();
            SqlCommand cmdFetchRecords = new SqlCommand("PrcGetMonthYear", DBConnection.OpenConnection());
            cmdFetchRecords.CommandType = CommandType.StoredProcedure;
            cmdFetchRecords.Parameters.Add("@IsMonth", SqlDbType.Int).Value = IsMonth;
            SqlDataAdapter sdc = new SqlDataAdapter(cmdFetchRecords);
            sdc.Fill(dsFetch);
            if (dsFetch != null)
            {
                ddl.DataSource = dsFetch;
                if (IsMonth == 1)
                    ddl.DataTextField = "Month";
                else
                    ddl.DataTextField = "year";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("<-- Select One -->", "0"));

            }
        }

        public static void GetCustomerGroup(DropDownList ddlItems)
        {
            SqlDataReader sdrPopulateRecords = null;
            SqlCommand cmdPopulateRecords = new SqlCommand("prcGetCustGrp", DBConnection.OpenConnection());
            int intIndex = 1;
            try
            {
                cmdPopulateRecords.CommandType = CommandType.StoredProcedure;
                cmdPopulateRecords.Connection.Open();
                sdrPopulateRecords = cmdPopulateRecords.ExecuteReader();

                ddlItems.Items.Clear();
                //ddlItems.DropDownWidth = 210;
                ddlItems.Items.Insert(0, new ListItem("<-- Select One -->", "0"));
                while (sdrPopulateRecords.Read())
                {
                    ListItem liItems = new ListItem();
                    liItems.Text = sdrPopulateRecords.GetValue(0).ToString();
                    liItems.Value = sdrPopulateRecords.GetValue(0).ToString();
                    ddlItems.Items.Insert(intIndex, liItems);
                    intIndex += 1;
                }
                cmdPopulateRecords.Connection.Close();
            }
            catch (Exception)
            {
                if (cmdPopulateRecords.Connection.State == ConnectionState.Open) cmdPopulateRecords.Connection.Close();
            }
            finally
            {
                if (sdrPopulateRecords != null) sdrPopulateRecords.Close();
                sdrPopulateRecords = null;
            }
        }

    }


    public static class Extensions
    {
        public static DataTable ToDataTable<T>(this List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
    }


}
