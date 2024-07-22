using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Common
{
    public class DBConnection
    {
        public string strConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        public SqlConnection sqlCon;

        public void Connect()
        {
            sqlCon = new SqlConnection(strConnectionString);
            sqlCon.Open();
        }
        public static SqlConnection OpenConnection()
        {
            string strConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            SqlConnection objSqlConnection = new SqlConnection(strConnectionString);
            return objSqlConnection;
        }

        public static void CloseConnection()
        {
            string strConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            SqlConnection objSqlConnection = new SqlConnection(strConnectionString);
            objSqlConnection.Close();
        }
    }
}
