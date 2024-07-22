/* ==============================================================================================
    Created By      : Subahani SM
    Created Date    : 03 July 2010
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
    public class UserMasterClass
    {
        /* declare Class level Variables */
        private string strLoginId, strMessage;
        private string strUserId, strUserName,strPassword,strUserType,strEmail,strNewpwd;
        private Int32 intRowId,intDealerCode;
        private Boolean blnStatus;
        

        /* Define Properties */
        public string prpLoginId
        {
            get
            {
                return strLoginId;
            }
            set
            {
                strLoginId = value;
            }
        }

        public string prpNewpwd
        {
            get
            {
                return strNewpwd;
            }
            set
            {
                strNewpwd = value;
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

        public string prpUserName
        {
            get
            {
                return strUserName;
            }
            set
            {
                strUserName = value;
            }
        }

        public string prpPassword
        {
            get
            {
                return strPassword;
            }
            set
            {
                strPassword = value;
            }
        }

        public string prpUserType
        {
            get
            {
                return strUserType;
            }
            set
            {
                strUserType = value;
            }
        }

        public string prpEmail
        {
            get
            {
                return strEmail;
            }
            set
            {
                strEmail = value;
            }
        }

        public Int32 prpRowId
        {
            get
            {
                return intRowId;
            }
            set
            {
                intRowId = value;
            }
        }

        public Int32 prpDealerCode
        {
            get
            {
                return intDealerCode;
            }
            set
            {
                intDealerCode = value;
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

        public DataSet FetchUserMaster(string strCondition)
        {
            DataSet dsBc = new DataSet();
            SqlConnection SqlConn = DBConnection.OpenConnection();
            SqlDataAdapter sdaBC = new SqlDataAdapter("prcFetchUserMaster", SqlConn);
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

        public DataSet FetchUserMasterOnId()
        {
            DataSet dsBc = new DataSet();
            SqlConnection SqlConn = DBConnection.OpenConnection();
            SqlDataAdapter sdaBC = new SqlDataAdapter("prcFetchUserMasterOnId", SqlConn);
            try
            {
                sdaBC.SelectCommand.CommandType = CommandType.StoredProcedure;
                sdaBC.SelectCommand.Parameters.Add("@UserId", SqlDbType.VarChar,20).Value = strUserId;
                dsBc.Clear();
                sdaBC.Fill(dsBc);
            }
            catch (Exception)
            {
                if (SqlConn.State == ConnectionState.Open) SqlConn.Close();
            }
            return dsBc;
        }

        public string SaveUserMaster()
        {
            strMessage = "";
            SqlCommand cmd = new SqlCommand("prcInsertUpdateUserMaster", DBConnection.OpenConnection());
            try
            {
                cmd.Parameters.Add("@RowId", SqlDbType.BigInt).Value = intRowId;
                cmd.Parameters.Add("@UserId", SqlDbType.VarChar, 20).Value = strUserId;
                cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = strUserName;
                cmd.Parameters.Add("@Password", SqlDbType.VarChar, 50).Value = strPassword;
                cmd.Parameters.Add("@UserType", SqlDbType.VarChar, 10).Value = strUserType;
                cmd.Parameters.Add("@DealerCode", SqlDbType.BigInt).Value = intDealerCode;
                cmd.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = strEmail;
                cmd.Parameters.Add("@Status", SqlDbType.Bit).Value = blnStatus;
                cmd.Parameters.Add("@LoginId", SqlDbType.VarChar, 20).Value = strLoginId;
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

        public string SaveChangePassword()
        {
            strMessage = "";
            SqlCommand cmd = new SqlCommand("prcFetchPassword_username", DBConnection.OpenConnection());
             try
             {
                 
                     cmd.Parameters.AddWithValue("@Username", prpUserName);
                     cmd.CommandType = CommandType.StoredProcedure;
                     cmd.Connection.Open();
                     string password = Convert.ToString(cmd.ExecuteScalar());
                     cmd.Connection.Close();
                     if (strPassword == EncryptDecryptClass.Decrypt(password))
                     {
                         cmd = new SqlCommand("prcChangePassword", DBConnection.OpenConnection());
                         cmd.Parameters.AddWithValue("@Username", prpUserName);
                         cmd.Parameters.AddWithValue("@Newpassword", strNewpwd);
                         cmd.Parameters.Add("@Message", SqlDbType.VarChar, 200).Direction = ParameterDirection.Output;
                         cmd.CommandType = CommandType.StoredProcedure;
                         cmd.Connection.Open();
                         cmd.ExecuteNonQuery();
                         strMessage = cmd.Parameters["@Message"].Value.ToString();
                         cmd.Connection.Close();
                     }
                     else
                         strMessage = "Invalid Old Password"; 
                         
                                              
         
                 
            }
            catch (Exception ex)
            {
                if (cmd.Connection.State == ConnectionState.Open) cmd.Connection.Close();
                strMessage = ex.Message;
            }
            return strMessage;
        }

        public string DeleteUserMaster()
        {
            strMessage = "";
            SqlCommand cmd = new SqlCommand("prcDeleteUserMaster", DBConnection.OpenConnection());
            try
            {
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
