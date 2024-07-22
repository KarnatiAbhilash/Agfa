/* ==============================================================================================
    Created By      : Subahani SM
    Created Date    : 28 July 2010
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
    public class DealerMasterClass
    {
        /* declare Class level Variables */
        private string strUserId, strMessage;
        private string strExecutiveCode, strRegion, strDMSCode, strDealerName, strAddress1, strAddress2, strAddress3;
        private string strCity, strState, strPincode, strContPerson, strContNo, strEmailId, strFaxNo, strTINNo, strCSTNo, strLSTNo, strRespUser;
        private string strRemarks;
        private Int32 intDealerCode,intDMIId,intItemId,intQty;
        private Boolean blnStatus;
        private double dbDlrPrice, dbPerSqrmt;

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

        public string prpDMSCode
        {
            get
            {
                return strDMSCode;
            }
            set
            {
                strDMSCode = value;
            }
        }

        public string prpDealerName
        {
            get
            {
                return strDealerName;
            }
            set
            {
                strDealerName = value;
            }
        }

        public string prpAddress1
        {
            get
            {
                return strAddress1;
            }
            set
            {
                strAddress1 = value;
            }
        }

        public string prpAddress2
        {
            get
            {
                return strAddress2;
            }
            set
            {
                strAddress2 = value;
            }
        }

        public string prpAddress3
        {
            get
            {
                return strAddress3;
            }
            set
            {
                strAddress3 = value;
            }
        }

        public string prpCity
        {
            get
            {
                return strCity;
            }
            set
            {
                strCity = value;
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

        public string prpPincode
        {
            get
            {
                return strPincode;
            }
            set
            {
                strPincode = value;
            }
        }

        public string prpContPerson
        {
            get
            {
                return strContPerson;
            }
            set
            {
                strContPerson = value;
            }
        }

        public string prpContNo
        {
            get
            {
                return strContNo;
            }
            set
            {
                strContNo = value;
            }
        }

        public string prpEmailId
        {
            get
            {
                return strEmailId;
            }
            set
            {
                strEmailId = value;
            }
        }

        public string prpFaxNo
        {
            get
            {
                return strFaxNo;
            }
            set
            {
                strFaxNo = value;
            }
        }

        public string prpTINNo
        {
            get
            {
                return strTINNo;
            }
            set
            {
                strTINNo = value;
            }
        }

        public string prpCSTNo
        {
            get
            {
                return strCSTNo;
            }
            set
            {
                strCSTNo = value;
            }
        }

        public string prpLSTNo
        {
            get
            {
                return strLSTNo;
            }
            set
            {
                strLSTNo = value;
            }
        }

        public string prpRespUser
        {
            get
            {
                return strRespUser;
            }
            set
            {
                strRespUser = value;
            }
        }

        public string prpRemarks
        {
            get
            {
                return strRemarks;
            }
            set
            {
                strRemarks = value;
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

        public Int32 prpDMIId
        {
            get
            {
                return intDMIId;
            }
            set
            {
                intDMIId = value;
            }
        }

        public Int32 prpItemId
        {
            get
            {
                return intItemId;
            }
            set
            {
                intItemId = value;
            }
        }

        public Int32 prpQty
        {
            get
            {
                return intQty;
            }
            set
            {
                intQty = value;
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

        public double prpDlrPrice
        {
            get
            {
                return dbDlrPrice;
            }
            set
            {
                dbDlrPrice = value;
            }
        }

        public double prpPerSqrmt
        {
            get
            {
                return dbPerSqrmt;
            }
            set
            {
                dbPerSqrmt = value;
            }
        }

         /* Define Methods & Functions */

        public DataSet FetchDealerMaster(string strCondition)
        {
            DataSet dsDealer = new DataSet();
            SqlConnection SqlConn = DBConnection.OpenConnection();
            SqlDataAdapter sdaDealer = new SqlDataAdapter("prcFetchDealerMaster", SqlConn);
            try
            {
                sdaDealer.SelectCommand.CommandType = CommandType.StoredProcedure;
                sdaDealer.SelectCommand.Parameters.Add("@Condition", SqlDbType.VarChar, 200).Value = strCondition;                
                dsDealer.Clear();
                sdaDealer.Fill(dsDealer);
            }
            catch (Exception)
            {
                if (SqlConn.State == ConnectionState.Open) SqlConn.Close();
            }
            return dsDealer;
        }

        public DataSet FetchDealerMasterOnId()
        {
            DataSet dsDealer = new DataSet();
            SqlConnection SqlConn = DBConnection.OpenConnection();
            SqlDataAdapter sdaDealer = new SqlDataAdapter("prcFetchDealerMasterOnId", SqlConn);
            try
            {
                sdaDealer.SelectCommand.CommandType = CommandType.StoredProcedure;
                sdaDealer.SelectCommand.Parameters.Add("@DealerCode", SqlDbType.BigInt).Value = intDealerCode;
                dsDealer.Clear();
                sdaDealer.Fill(dsDealer);
            }
            catch (Exception)
            {
                if (SqlConn.State == ConnectionState.Open) SqlConn.Close();
            }
            return dsDealer;
        }

        public string SaveDealerMaster()
        {
            strMessage = "";
            SqlCommand cmd = new SqlCommand("prcInsertUpdateDealer", DBConnection.OpenConnection());
            try
            {
                cmd.Parameters.Add("@DealerCode", SqlDbType.BigInt).Value = intDealerCode;
                cmd.Parameters.Add("@ExecutiveCode", SqlDbType.VarChar, 10).Value = strExecutiveCode;
                cmd.Parameters.Add("@Region", SqlDbType.VarChar, 10).Value = strRegion;
                cmd.Parameters.Add("@DMSCode", SqlDbType.VarChar,20).Value = strDMSCode;
                cmd.Parameters.Add("@DealerName", SqlDbType.VarChar, 50).Value = strDealerName;
                cmd.Parameters.Add("@Address1", SqlDbType.VarChar, 50).Value = strAddress1;
                cmd.Parameters.Add("@Address2", SqlDbType.VarChar, 50).Value = strAddress2;
                cmd.Parameters.Add("@Address3", SqlDbType.VarChar, 50).Value = strAddress3;
                cmd.Parameters.Add("@City", SqlDbType.VarChar, 25).Value = strCity;
                cmd.Parameters.Add("@State", SqlDbType.VarChar,5).Value = strState;
                cmd.Parameters.Add("@Pincode", SqlDbType.VarChar, 10).Value = strPincode;
                cmd.Parameters.Add("@ContactPerson", SqlDbType.VarChar, 50).Value = strContPerson;
                cmd.Parameters.Add("@ContactNo", SqlDbType.VarChar, 15).Value = strContNo;
                cmd.Parameters.Add("@EmailId", SqlDbType.VarChar, 50).Value = strEmailId;
                cmd.Parameters.Add("@FaxNo", SqlDbType.VarChar,15).Value = strFaxNo;
                cmd.Parameters.Add("@TINNo", SqlDbType.VarChar, 50).Value = strTINNo;
                cmd.Parameters.Add("@CSTNo", SqlDbType.VarChar, 50).Value = strCSTNo;
                cmd.Parameters.Add("@LSTNo", SqlDbType.VarChar, 50).Value = strLSTNo;
                cmd.Parameters.Add("@RespUser", SqlDbType.VarChar, 1000).Value = strRespUser;                
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

        public string DeleteDealerMaster()
        {
            strMessage = "";
            SqlCommand cmd = new SqlCommand("prcDeleteDealerMaster", DBConnection.OpenConnection());
            try
            {
                cmd.Parameters.Add("@DealerCode", SqlDbType.BigInt).Value = intDealerCode;
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

        public DataSet FetchDealerItemList()
        {
            DataSet dsDealer = new DataSet();
            SqlConnection SqlConn = DBConnection.OpenConnection();
            SqlDataAdapter sdaDealer = new SqlDataAdapter("prcFetchDealerItemList", SqlConn);
            try
            {
                sdaDealer.SelectCommand.CommandType = CommandType.StoredProcedure;
                sdaDealer.SelectCommand.Parameters.Add("@DealerCode", SqlDbType.BigInt).Value = intDealerCode;
                dsDealer.Clear();
                sdaDealer.Fill(dsDealer);
            }
            catch (Exception)
            {
                if (SqlConn.State == ConnectionState.Open) SqlConn.Close();
            }
            return dsDealer;
        }
        public string SaveDealerItemList()
        {
            strMessage = "";
            SqlCommand cmd = new SqlCommand("prcInsertUpdateDealerPrice", DBConnection.OpenConnection());
            try
            {
                cmd.Parameters.Add("@DIMId", SqlDbType.BigInt).Value = intDMIId;
                cmd.Parameters.Add("@DealerCode", SqlDbType.BigInt).Value = intDealerCode;
                cmd.Parameters.Add("@Item_Id", SqlDbType.BigInt).Value = intItemId;
                cmd.Parameters.Add("@DlrPrice", SqlDbType.Decimal).Value = dbDlrPrice;
                cmd.Parameters.Add("@PerSqrMt", SqlDbType.Decimal).Value = dbPerSqrmt;
                cmd.Parameters.Add("@MaxQty", SqlDbType.BigInt).Value = intQty;
                cmd.Parameters.Add("@Remarks", SqlDbType.VarChar, 200).Value = strRemarks;
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

        public string DeleteItemPrice()
        {
            strMessage = "";
            SqlCommand cmd = new SqlCommand("prcDeleteDealerPrice", DBConnection.OpenConnection());
            try
            {
                cmd.Parameters.Add("@DIMId", SqlDbType.BigInt).Value = intDMIId;
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

        public DataSet FetchDealerItems(string strCondition,string strPOType)
        {
            DataSet dsDealer = new DataSet();
            SqlConnection SqlConn = DBConnection.OpenConnection();
            SqlDataAdapter sdaDealer = new SqlDataAdapter("prcFetchDealerItems", SqlConn);
            try
            {
                sdaDealer.SelectCommand.CommandType = CommandType.StoredProcedure;
                sdaDealer.SelectCommand.Parameters.Add("@Condition", SqlDbType.VarChar, 500).Value = strCondition;
                sdaDealer.SelectCommand.Parameters.Add("@POType", SqlDbType.VarChar, 20).Value = strPOType;
                dsDealer.Clear();
                sdaDealer.Fill(dsDealer);
            }
            catch (Exception)
            {
                if (SqlConn.State == ConnectionState.Open) SqlConn.Close();
            }
            return dsDealer;
        }

    }
}

 
