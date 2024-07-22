/* ==============================================================================================
    Created By      : Subahani SM
    Created Date    : 21 July 2010
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
    public class UserMenuMapping
    {
        private string strType;        
        private string MM_Code;
        private string MM_Name;
        private string MM_ImageURL;
        private string MM_NavigateURL;
        private string SM_Code;
        private string SM_Name;
        private string SM_ImageURL;
        private string SM_NavigateURL;
        private string strLoginId;
        private string strUserId;
        private DataSet DS;

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

        public string prpType
        {
            get
            {
                return strType;
            }
            set
            {
                strType = value;
            }
        }

        public DataSet prpDS
        {
            get
            {
                return DS;
            }
            set
            {
                DS = value;
            }
        }

        public string prpMM_NavigateURL
        {
            get
            {
                return MM_NavigateURL;
            }
            set
            {
                MM_NavigateURL = value;
            }
        }

        public string prpSM_NavigateURL
        {
            get
            {
                return SM_NavigateURL;
            }
            set
            {
                SM_NavigateURL = value;
            }
        }

        public string prpMM_ImageURL
        {
            get
            {
                return MM_ImageURL;
            }
            set
            {
                MM_ImageURL = value;
            }
        }

        public string prpSM_ImageURL
        {
            get
            {
                return SM_ImageURL;
            }
            set
            {
                SM_ImageURL = value;
            }
        }

        public string prpMM_Name
        {
            get
            {
                return MM_Name;
            }
            set
            {
                MM_Name = value;
            }
        }

        public string prpSM_Name
        {
            get
            {
                return SM_Name;
            }
            set
            {
                SM_Name = value;
            }
        }

        public string prpMM_Code
        {
            get
            {
                return MM_Code;
            }
            set
            {
                MM_Code = value;
            }
        }

        public string prpSM_Code
        {
            get
            {
                return SM_Code;
            }
            set
            {
                SM_Code = value;
            }
        }

        public void Insert()
        {
            SqlCommand cmdInsertGroup = null;
            cmdInsertGroup = new SqlCommand("prcInsertUserMenuMapping", DBConnection.OpenConnection());
            cmdInsertGroup.CommandType = CommandType.StoredProcedure;
            cmdInsertGroup.Parameters.Clear();
            cmdInsertGroup.Parameters.Add("@Type", SqlDbType.VarChar, 10).Value = strType;
            cmdInsertGroup.Parameters.Add("@UM_MM_Code", SqlDbType.VarChar, 200).Value = MM_Code;
            cmdInsertGroup.Parameters.Add("@UM_SM_Code", SqlDbType.VarChar, 200).Value = SM_Code;
            cmdInsertGroup.Parameters.Add("@LoginId", SqlDbType.VarChar, 20).Value = strLoginId;
            cmdInsertGroup.Parameters.Add("@UserId", SqlDbType.VarChar,20).Value = strUserId;
            cmdInsertGroup.Connection.Open();
            cmdInsertGroup.ExecuteNonQuery();
            cmdInsertGroup.Connection.Close();
        }

        public void GetMenus(string UserIdstr)
        {
            DataSet dsFetch = new DataSet();
            SqlCommand cmdFetchRecords = new SqlCommand("prcFetchMenus", DBConnection.OpenConnection());
            cmdFetchRecords.CommandType = CommandType.StoredProcedure;
            cmdFetchRecords.Parameters.Add("@UserId", SqlDbType.VarChar, 20).Value = UserIdstr;
            SqlDataAdapter sdc = new SqlDataAdapter(cmdFetchRecords);
            sdc.Fill(dsFetch);
            DS = dsFetch;
        }

        public DataSet GetMenusForImage(string UserIdstr, bool blnIsMenu, string strMMCode)
        {
            DataSet dsFetch = new DataSet();
            SqlCommand cmdFetchRecords = new SqlCommand("prcFetchMenusForImage", DBConnection.OpenConnection());
            cmdFetchRecords.CommandType = CommandType.StoredProcedure;
            cmdFetchRecords.Parameters.Add("@UserId", SqlDbType.VarChar, 20).Value = UserIdstr;
            cmdFetchRecords.Parameters.Add("@IsSubMenu", SqlDbType.Bit).Value = blnIsMenu;
            cmdFetchRecords.Parameters.Add("@MMCode", SqlDbType.VarChar, 200).Value = strMMCode;
            SqlDataAdapter sdc = new SqlDataAdapter(cmdFetchRecords);
            sdc.Fill(dsFetch);
            return dsFetch;
        }

        public void PopulateMenus(string UserIdstr)
        {
            DataSet dsFetch = new DataSet();
            SqlCommand cmdFetchRecords = new SqlCommand("prcPopulateMenus", DBConnection.OpenConnection());
            cmdFetchRecords.CommandType = CommandType.StoredProcedure;
            cmdFetchRecords.Parameters.Add("@UserId", SqlDbType.VarChar, 20).Value = UserIdstr;
            SqlDataAdapter sdc = new SqlDataAdapter(cmdFetchRecords);
            sdc.Fill(dsFetch);
            DS = dsFetch;
        }

    }
}
