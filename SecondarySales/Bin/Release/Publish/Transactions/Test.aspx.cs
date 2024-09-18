using BusinessClass;
using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Transactions_Test : System.Web.UI.Page
{
    ItemMasterClass objBC = new ItemMasterClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        BindData(true, true, false);
    }
    protected void BindData(bool isFromDB, bool IsRebind, bool issearch)
    {
        DataTable dtBC;
        DataView dvBC;
        DataSet dsSetBC;
        dtBC = FetchEquipmentItemClass(" where Status=1").Tables[0];
        dvBC = dtBC.DefaultView;
        GridView1.DataSource = dvBC;       
            GridView1.DataBind();
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
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.DataItemIndex != -1)
        {
            if (e.Row.Cells[6].Text.ToLower() == "active" || e.Row.Cells[6].Text.ToLower() == "true")
                e.Row.Cells[6].Text = "Active";
            else
                e.Row.Cells[6].Text = "In-Active";
        }
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.EmptyDataRow)
        {
            e.Row.Cells[0].Attributes.Add("OnClick", "Del('" + e.Row.RowIndex + "')");
        }
    }
}