using BusinessClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class HomeMenu : System.Web.UI.UserControl
{

   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Session["UserID"] == null)
            this.Response.Redirect("../Logout.aspx");
        try
        {
            this.GetHomeMenus();
        }
        catch (Exception ex)
        {
        }
    }
    protected void GetHomeMenus()
    {
        DataTable table = new UserMenuMapping().GetMenusForImage(this.Session["UserId"].ToString(), false, "0").Tables[0];
        HtmlTableRow htmlTableRow = new HtmlTableRow();
        HtmlTableCell cell = new HtmlTableCell();
        for (int index = 0; index < table.Rows.Count; ++index)
        {
            string newValue = table.Rows[index]["Code"].ToString();
            string str1 = table.Rows[index]["Name"].ToString();
            string str2 = table.Rows[index]["ImageURL"].ToString();
            string str3 = table.Rows[index]["NavigateURL"].ToString().Replace("{0}", newValue);
            if (index % 5 == 0)
            {
                HtmlTableRow row = new HtmlTableRow();
                this.tblHomeMenu.Rows.Add(row);
                cell = new HtmlTableCell();
                row.Cells.Add(cell);
            }
            cell.Align = "center";
            HtmlImage child1 = new HtmlImage();
            child1.Src = str2;
            child1.Alt = str1;
            HtmlAnchor child2 = new HtmlAnchor();
            child2.Target = "_self";
            child2.HRef = str3;
            child2.Controls.Add((Control)child1);
            Label child3 = new Label();
            child3.Text = "&nbsp;";
            child3.Width = Unit.Pixel(5);
            cell.Controls.Add((Control)child2);
            cell.Controls.Add((Control)child3);
        }
    }
}