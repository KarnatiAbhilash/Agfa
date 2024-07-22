/* ==============================================================================================
    Created By      : Subahani SM
    Created Date    : 18 Aug 2010
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
    public class BulkInsertClass
    {
        /* declare Class level Variables */
        private string strUserId, strMessage;

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

        /* Define Methods & Functions */
        public string Insert(string strDestinationTableName, DataTable dtDestinationTable)
        {
            strMessage = "";
            SqlConnection SqlCn = DBConnection.OpenConnection();
            try
            {
                SqlBulkCopy objSqlBulk = new SqlBulkCopy(SqlCn);
                objSqlBulk.DestinationTableName = strDestinationTableName;

                foreach(DataColumn col in dtDestinationTable.Columns)                
                    objSqlBulk.ColumnMappings.Add(col.ColumnName, col.ColumnName);              
                
                SqlCn.Open();
                objSqlBulk.WriteToServer(dtDestinationTable);
                SqlCn.Close();
            }
            catch (Exception ex)
            {
                if (SqlCn.State == ConnectionState.Open) SqlCn.Close();
                strMessage = ex.Message;
            }
            return strMessage;
        }
    }
}
