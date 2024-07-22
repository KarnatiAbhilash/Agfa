/* ==============================================================================================
    Created By      : Subahani SM
    Created Date    : 19 July 2010
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
    public class LoginClass
    {
        /* declare Class level Variables */
        private string strUserId,strPassword;

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

        /* Define Methods & Functions */
        public DataSet AuthenticateUser()
        {
            DataSet dsUser = new DataSet();
            SqlConnection SqlConn = DBConnection.OpenConnection();
            SqlDataAdapter sdaAuth = new SqlDataAdapter("prcAuthenticateUser", SqlConn);
            try
            {
                sdaAuth.SelectCommand.CommandType = CommandType.StoredProcedure;
                sdaAuth.SelectCommand.Parameters.Add("@UserId", SqlDbType.VarChar, 20).Value = strUserId;
                sdaAuth.SelectCommand.Parameters.Add("@Password", SqlDbType.VarChar, 50).Value = strPassword;
                dsUser.Clear();
                sdaAuth.Fill(dsUser);
            }
            catch (Exception)
            {
                if (SqlConn.State == ConnectionState.Open) SqlConn.Close();
            }
            return dsUser;
        }
    }
}
