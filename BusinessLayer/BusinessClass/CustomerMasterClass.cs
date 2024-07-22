/* ==============================================================================================
    Created By      : Subahani SM
    Created Date    : 30 July 2010
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
    public class CustomerMasterClass
    {
        /* declare Class level Variables */
        private string strUserId, strMessage,strDMSCode;
        private string strRegion, strCustType, strCustName, strAddress1, strAddress2, strAddress3, strRemarks, strSalesEmpName;
        private string strQty, strItemCode,strCity, strState, strPincode, strContPerson, strContNo, strEmailId,strCSTNo, strGSTNo, strLSTNo, strCustGroup;
        private Int32 intCustcode,intDealerCode, intDMIId, intItemId, intCPMId,intQtyConsum,IntQtyEligible;
        private Boolean blnStatus,blIsSpecCust, blIsInstallbaseCheck, blnDirectCust, blnMou;
        private double dbEUPrice, dbEUPerSqrmt, dbProfitActual, dbProfitAgreed,dbSqrmtConsum, dbSqmtEligible,dbvalue,dbValueEligible;
        private DateTime dtValidUpto, dtInstDate, strDemostartDate;
        private string strStatus;
        private decimal strCmtSqm;
        private int strDescrp1;
        private int strDescrp2;

        /* Define Properties */

        public bool prpInstallbaseCheck
        {
            get => this.blIsInstallbaseCheck;
            set => this.blIsInstallbaseCheck = value;
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

        public string prpCustType
        {
            get
            {
                return strCustType;
            }
            set
            {
                strCustType = value;
            }
        }

        public string prpQty
        {
            get => this.strQty;
            set => this.strQty = value;
        }

        public string prpGSTNo
        {
            get => this.strGSTNo;
            set => this.strGSTNo = value;
        }

        public string prpCustName
        {
            get
            {
                return strCustName;
            }
            set
            {
                strCustName = value;
            }
        }

        public string prpSalesEmpName
        {
            get => this.strSalesEmpName;
            set => this.strSalesEmpName = value;
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

        public DateTime prpDemoStartDate
        {
            get => this.strDemostartDate;
            set => this.strDemostartDate = value;
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

        public Decimal prpComtSqm
        {
            get => this.strCmtSqm;
            set => this.strCmtSqm = value;
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

        public Int32 prpCustCode
        {
            get
            {
                return intCustcode;
            }
            set
            {
                intCustcode = value;
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

        public string prpItemCode
        {
            get => this.strItemCode;
            set => this.strItemCode = value;
        }

        public Int32 prpCPMId
        {
            get
            {
                return intCPMId;
            }
            set
            {
                intCPMId = value;
            }
        }

        public Int32 prpQtyConsum
        {
            get
            {
                return intQtyConsum;
            }
            set
            {
                intQtyConsum = value;
            }
        }
        public int prpDescrp1
        {
            get => this.strDescrp1;
            set => this.strDescrp1 = value;
        }

        public int prpDescrp2
        {
            get => this.strDescrp2;
            set => this.strDescrp2 = value;
        }
        public DateTime prpInstDate
        {
            get => this.dtInstDate;
            set => this.dtInstDate = value;
        }

        public Int32 prpQtyEligible
        {
            get
            {
                return IntQtyEligible;
            }
            set
            {
                IntQtyEligible = value;
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

        public bool prpDirectCust
        {
            get => this.blnDirectCust;
            set => this.blnDirectCust = value;
        }
        public string prpApproverStatus
        {
            get
            {
                return strStatus;
            }
            set
            {
                strStatus = value;
            }
        }

        public Boolean prpIsSpecCust
        {
            get
            {
                return blIsSpecCust;
            }
            set
            {
                blIsSpecCust = value;
            }
        }

        public bool prpMou
        {
            get => this.blnMou;
            set => this.blnMou = value;
        }

        public double prpSqrmtConsum
        {
            get
            {
                return dbSqrmtConsum;
            }
            set
            {
                dbSqrmtConsum = value;
            }
        }

        public double prpSrmtEligible
        {
            get
            {
                return dbSqmtEligible;
            }
            set
            {
                dbSqmtEligible = value;
            }
        }

        public double prpEuPrice
        {
            get
            {
                return dbEUPrice;
            }
            set
            {
                dbEUPrice = value;
            }
        }

        public double prpEUPerSqrmt
        {
            get
            {
                return dbEUPerSqrmt;
            }
            set
            {
                dbEUPerSqrmt = value;
            }
        }

        public double prpProfitActual
        {
            get
            {
                return dbProfitActual;
            }
            set
            {
                dbProfitActual = value;
            }
        }

        public double prpProfitAgreed
        {
            get
            {
                return dbProfitAgreed;
            }
            set
            {
                dbProfitAgreed = value;
            }
        }

        public double prpValue
        {
            get
            {
                return dbvalue;
            }
            set
            {
                dbvalue = value;
            }
        }

        public double prpValueEligible
        {
            get
            {
                return dbValueEligible;
            }
            set
            {
                dbValueEligible = value;
            }
        }

        public DateTime prpValidUpto
        {
            get
            {
                return dtValidUpto;
            }
            set
            {
                dtValidUpto = value;
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
        
        /* Define Methods & Functions */

        public DataSet FetchCustomerMaster(string strCondition)
        {
            DataSet dsCust = new DataSet();
            SqlConnection SqlConn = DBConnection.OpenConnection();
            SqlDataAdapter sdaCust = new SqlDataAdapter("prcFetchCustomerMaster", SqlConn);
            try
            {
                sdaCust.SelectCommand.CommandType = CommandType.StoredProcedure;
                sdaCust.SelectCommand.Parameters.Add("@Condition", SqlDbType.VarChar, 200).Value = strCondition;
                dsCust.Clear();
                sdaCust.Fill(dsCust);
            }
            catch (Exception)
            {
                if (SqlConn.State == ConnectionState.Open) SqlConn.Close();
            }
            return dsCust;
        }

        public DataSet FetchCustomerMasterOnId()
        {
            DataSet dsCust = new DataSet();
            SqlConnection SqlConn = DBConnection.OpenConnection();
            SqlDataAdapter sdaCust = new SqlDataAdapter("prcFetchCustomerMasterOnId", SqlConn);
            try
            {
                sdaCust.SelectCommand.CommandType = CommandType.StoredProcedure;
                sdaCust.SelectCommand.Parameters.Add("@CustCode", SqlDbType.BigInt).Value = intCustcode;
                dsCust.Clear();
                sdaCust.Fill(dsCust);
            }
            catch (Exception)
            {
                if (SqlConn.State == ConnectionState.Open) SqlConn.Close();
            }
            return dsCust;
        }

        public string SaveCustMaster()
        {
            strMessage = "";
            SqlCommand cmd = new SqlCommand("prcInsertUpdateCustomer", DBConnection.OpenConnection());
            try
            {
                cmd.Parameters.Add("@CustCode", SqlDbType.BigInt).Value = intCustcode; 
                cmd.Parameters.Add("@DealerCode", SqlDbType.BigInt).Value = intDealerCode;
                cmd.Parameters.Add("@CustName", SqlDbType.VarChar, 50).Value = strCustName;
                cmd.Parameters.Add("@Address1", SqlDbType.VarChar, 25).Value = strAddress1;
                cmd.Parameters.Add("@Address2", SqlDbType.VarChar, 25).Value = strAddress2;
                cmd.Parameters.Add("@Address3", SqlDbType.VarChar, 25).Value = strAddress3;
                cmd.Parameters.Add("@City", SqlDbType.VarChar, 25).Value = strCity;
                cmd.Parameters.Add("@State", SqlDbType.VarChar, 5).Value = strState;
                cmd.Parameters.Add("@Pincode", SqlDbType.VarChar, 10).Value = strPincode;
                cmd.Parameters.Add("@ContactPerson", SqlDbType.VarChar, 50).Value = strContPerson;
                cmd.Parameters.Add("@ContactNo", SqlDbType.VarChar, 15).Value = strContNo;
                cmd.Parameters.Add("@EmailId", SqlDbType.VarChar, 50).Value = strEmailId;
                cmd.Parameters.Add("@GSTNo", SqlDbType.VarChar, 15).Value = (object)this.strGSTNo;
                cmd.Parameters.Add("@IsSpecialCust", SqlDbType.Bit).Value = blIsSpecCust;
                cmd.Parameters.Add("@CustGroup", SqlDbType.VarChar, 10).Value = strCustGroup;
                cmd.Parameters.Add("@Region", SqlDbType.VarChar, 10).Value = strRegion;
                cmd.Parameters.Add("@CustType", SqlDbType.VarChar, 10).Value = strCustType;
                cmd.Parameters.Add("@Status", SqlDbType.Bit).Value = blnStatus;
                cmd.Parameters.Add("@DirectCustomer", SqlDbType.Bit).Value = (object)this.blnDirectCust;
                cmd.Parameters.Add("@SalesEmpName", SqlDbType.VarChar, 50).Value = (object)this.strSalesEmpName;
                cmd.Parameters.Add("@MOU", SqlDbType.Bit).Value = (object)this.blnMou;
                if (this.blnMou)
                {
                    cmd.Parameters.Add("@DEMOSTARTDATE", SqlDbType.DateTime).Value = (object)this.strDemostartDate;
                    cmd.Parameters.Add("@COMMITMENTINSQMPM", SqlDbType.Decimal).Value = (object)this.strCmtSqm;
                }
                cmd.Parameters.Add("@UserId", SqlDbType.VarChar, 20).Value = strUserId;
                cmd.Parameters.Add("@DMSCode", SqlDbType.VarChar, 20).Value = strDMSCode;
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

        public string DeleteCustMaster()
        {
            strMessage = "";
            SqlCommand cmd = new SqlCommand("prcDeleteCustomerMaster", DBConnection.OpenConnection());
            try
            {
                cmd.Parameters.Add("@CustCode", SqlDbType.BigInt).Value = intCustcode;
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

        public DataSet FetchCustItemList()
        {
            DataSet dsDealer = new DataSet();
            SqlConnection SqlConn = DBConnection.OpenConnection();
            SqlDataAdapter sdaDealer = new SqlDataAdapter("prcFetchCustItemPrice", SqlConn);
            try
            {
                sdaDealer.SelectCommand.CommandType = CommandType.StoredProcedure;
                sdaDealer.SelectCommand.Parameters.Add("@DealerCode", SqlDbType.BigInt).Value = intDealerCode;
                sdaDealer.SelectCommand.Parameters.Add("@CustCode", SqlDbType.BigInt).Value = intCustcode;
                dsDealer.Clear();
                sdaDealer.Fill(dsDealer);
            }
            catch (Exception)
            {
                if (SqlConn.State == ConnectionState.Open) SqlConn.Close();
            }
            return dsDealer;
        }
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
        public DataSet FetchEquipmentItemInsatallBaselist(string strCondition,int CustCode)
        {
            DataSet dsBc = new DataSet();
            SqlConnection SqlConn = DBConnection.OpenConnection();
            SqlDataAdapter sdaBC = new SqlDataAdapter("prcFetchCustInstallBaseList", SqlConn);
            try
            {
                sdaBC.SelectCommand.CommandType = CommandType.StoredProcedure;
                sdaBC.SelectCommand.Parameters.Add("@Condition", SqlDbType.VarChar, 200).Value = strCondition;
                sdaBC.SelectCommand.Parameters.Add("@CustCode", SqlDbType.BigInt).Value = CustCode;
                
                dsBc.Clear();
                sdaBC.Fill(dsBc);
            }
            catch (Exception)
            {
                if (SqlConn.State == ConnectionState.Open) SqlConn.Close();
            }
            return dsBc;
        }
        public DataSet FetchApproverPendingCustItemList()
        {
            DataSet dsDealer = new DataSet();
            SqlConnection SqlConn = DBConnection.OpenConnection();
            SqlDataAdapter sdaDealer = new SqlDataAdapter("prcFetchApproverPendingCustItemPrice", SqlConn);
            try
            {
                sdaDealer.SelectCommand.CommandType = CommandType.StoredProcedure;               
                sdaDealer.SelectCommand.Parameters.Add("@UserId", SqlDbType.VarChar).Value = strUserId;
                dsDealer.Clear();
                sdaDealer.Fill(dsDealer);
            }
            catch (Exception)
            {
                if (SqlConn.State == ConnectionState.Open) SqlConn.Close();
            }
            return dsDealer;
        }


        public void SetApproverCustItemPriceStatus(List<ApproverList> Ids)
        {
           
            SqlCommand cmd = new SqlCommand("prcSetApproverCustomerItemPriceStatus", DBConnection.OpenConnection());
            try
            {
                cmd.Parameters.Add("@ApproverList", SqlDbType.Structured).Value =Ids.ToDataTable();
                cmd.Parameters.Add("@Status", SqlDbType.VarChar, 20).Value = strStatus;
                cmd.Parameters.Add("@UserId", SqlDbType.VarChar, 20).Value = strUserId;
               // cmd.Parameters.Add("@Msg", SqlDbType.VarChar, 200).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                if (cmd.Connection.State == ConnectionState.Open) cmd.Connection.Close();
                strMessage = ex.Message;
            }
           
        }
        public string SaveCustItemList()
        {
            strMessage = "";
            SqlCommand cmd = new SqlCommand("prcInsertUpdateCustomerPrice", DBConnection.OpenConnection());
            try
            {
                cmd.Parameters.Add("@CPMId", SqlDbType.BigInt).Value = intCPMId;
                cmd.Parameters.Add("@CustCode", SqlDbType.BigInt).Value = intCustcode;
                cmd.Parameters.Add("@ItemId", SqlDbType.BigInt).Value = intItemId;
                cmd.Parameters.Add("@EuPrice", SqlDbType.Decimal).Value = dbEUPrice;
                cmd.Parameters.Add("@EUPerSqrMt", SqlDbType.Decimal).Value = dbEUPerSqrmt;
                cmd.Parameters.Add("@ProfitActual", SqlDbType.Decimal).Value = dbProfitActual;
                cmd.Parameters.Add("@ProfitAgreed", SqlDbType.Decimal).Value = dbProfitAgreed;
                cmd.Parameters.Add("@ValidUpto", SqlDbType.DateTime).Value = dtValidUpto;
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

        public string SaveInstallBasebckp()
        {
            this.strMessage = "";
            SqlCommand sqlCommand = new SqlCommand("prcInsertInstallBase", DBConnection.OpenConnection());
            try
            {
                sqlCommand.Parameters.Add("@CPMId", SqlDbType.BigInt).Value = (object)this.intCPMId;
                sqlCommand.Parameters.Add("@CustCode", SqlDbType.BigInt).Value = (object)this.intCustcode;
                sqlCommand.Parameters.Add("@ItemId", SqlDbType.VarChar, 20).Value = (object)this.intItemId;
                sqlCommand.Parameters.Add("@Qty", SqlDbType.VarChar, 50).Value = (object)this.strQty;
                sqlCommand.Parameters.Add("@ValidUpto", SqlDbType.DateTime).Value = (object)this.dtValidUpto;
                sqlCommand.Parameters.Add("@UserId", SqlDbType.VarChar, 20).Value = (object)this.strUserId;
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
        public string SaveInstallBase()
        {
            this.strMessage = "";
            SqlCommand sqlCommand = new SqlCommand("prcInsertInstallBase", DBConnection.OpenConnection());
            try
            {
                sqlCommand.Parameters.Add("@Id", SqlDbType.BigInt).Value = (object)this.intItemId;
                sqlCommand.Parameters.Add("@CustCode", SqlDbType.BigInt).Value = (object)this.intCustcode;
                sqlCommand.Parameters.Add("@ItemCode", SqlDbType.VarChar, 20).Value = (object)this.prpItemCode;              
                sqlCommand.Parameters.Add("@InstallDate", SqlDbType.DateTime).Value = (object)this.prpInstDate;
                sqlCommand.Parameters.Add("@UserId", SqlDbType.VarChar, 20).Value = (object)this.strUserId;
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


        public string DeleteItemPrice()
        {
            strMessage = "";
            SqlCommand cmd = new SqlCommand("prcDeleteCustItemPrice", DBConnection.OpenConnection());
            try
            {
                cmd.Parameters.Add("@CPMId", SqlDbType.BigInt).Value = intCPMId;
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

        public string DeleteInstallBase()
        {
            this.strMessage = "";
            SqlCommand sqlCommand = new SqlCommand("prcDeleteInstallBase", DBConnection.OpenConnection());
            try
            {
                sqlCommand.Parameters.Add("@Id", SqlDbType.BigInt).Value = (object)this.intCPMId;
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

        public DataSet FetchCustSpecialScheme()
        {
            DataSet dsCust = new DataSet();
            SqlConnection SqlConn = DBConnection.OpenConnection();
            SqlDataAdapter sdaCust = new SqlDataAdapter("prcFetchCustSpecialScheme", SqlConn);
            try
            {
                sdaCust.SelectCommand.CommandType = CommandType.StoredProcedure;                
                sdaCust.SelectCommand.Parameters.Add("@CustCode", SqlDbType.BigInt).Value = intCustcode;
                dsCust.Clear();
                sdaCust.Fill(dsCust);
            }
            catch (Exception)
            {
                if (SqlConn.State == ConnectionState.Open) SqlConn.Close();
            }
            return dsCust;
        }

        public string SaveCustSpecialScheme()
        {
            strMessage = "";
            SqlCommand cmd = new SqlCommand("prcCustSpecialSchemeInsertUpdate", DBConnection.OpenConnection());
            try
            {                
                cmd.Parameters.Add("@CustCode", SqlDbType.BigInt).Value = intCustcode;
                cmd.Parameters.Add("@ItemId", SqlDbType.BigInt).Value = intItemId;
                cmd.Parameters.Add("@QtyConsumption", SqlDbType.BigInt).Value = intQtyConsum;
                cmd.Parameters.Add("@QtyEligible", SqlDbType.BigInt).Value = IntQtyEligible;
                cmd.Parameters.Add("@SqmtConsumption ", SqlDbType.Decimal).Value = dbSqrmtConsum;
                cmd.Parameters.Add("@SqmtEligible", SqlDbType.Decimal).Value = dbSqmtEligible;
                cmd.Parameters.Add("@Value ", SqlDbType.Decimal).Value = dbvalue;
                cmd.Parameters.Add("@ValueEligible", SqlDbType.Decimal).Value = dbValueEligible;
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

        public string DeleteSpecialScheme()
        {
            strMessage = "";
            SqlCommand cmd = new SqlCommand("prcDeleteCustSpecialScheme", DBConnection.OpenConnection());
            try
            {
                cmd.Parameters.Add("@CustCode", SqlDbType.BigInt).Value = intCustcode;
                cmd.Parameters.Add("@ItemId", SqlDbType.BigInt).Value = intItemId;
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

        public DataSet FetchDealerCust()
        {
            DataSet dsCust = new DataSet();
            SqlConnection SqlConn = DBConnection.OpenConnection();
            SqlDataAdapter sdaCust = new SqlDataAdapter("prcFetchDlrCustLink", SqlConn);
            try
            {
                sdaCust.SelectCommand.CommandType = CommandType.StoredProcedure;
                sdaCust.SelectCommand.Parameters.Add("@DealerCode", SqlDbType.BigInt).Value = intDealerCode;
                dsCust.Clear();
                sdaCust.Fill(dsCust);
            }
            catch (Exception)
            {
                if (SqlConn.State == ConnectionState.Open) SqlConn.Close();
            }
            return dsCust;
        }

        public string SaveDlrCustlink()
        {
            strMessage = "";
            SqlCommand cmd = new SqlCommand("prcDlrCustLinkSave", DBConnection.OpenConnection());
            try
            {
                cmd.Parameters.Add("@DealerCode", SqlDbType.BigInt).Value = intDealerCode;  
                cmd.Parameters.Add("@CustCode", SqlDbType.BigInt).Value = intCustcode;                             
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

        public DataSet GetCustGroup()
        {
            DataSet dsCust = new DataSet();
            SqlConnection SqlConn = DBConnection.OpenConnection();
            SqlDataAdapter sdaCust = new SqlDataAdapter("prcGetCustGroup", SqlConn);
            try
            {
                sdaCust.SelectCommand.CommandType = CommandType.StoredProcedure;                
                dsCust.Clear();
                sdaCust.Fill(dsCust);
            }
            catch (Exception)
            {
                if (SqlConn.State == ConnectionState.Open) SqlConn.Close();
            }
            return dsCust;
        }

        public DataSet FetchCustItems(string strCondition, string strSalesType)
        {
            DataSet dsDealer = new DataSet();
            SqlConnection SqlConn = DBConnection.OpenConnection();
            SqlDataAdapter sdaDealer = new SqlDataAdapter("prcFetchCustItems", SqlConn);
            try
            {
                sdaDealer.SelectCommand.CommandType = CommandType.StoredProcedure;
                sdaDealer.SelectCommand.Parameters.Add("@Condition", SqlDbType.VarChar, 200).Value = strCondition;
                sdaDealer.SelectCommand.Parameters.Add("@SalesType", SqlDbType.VarChar, 20).Value = strSalesType;
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

