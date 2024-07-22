using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.IO;
using BusinessClass;
using System.Data;

public partial class HomePage_HomeMenuSection : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null) Response.Redirect("../Logout.aspx");
        try
        {
            GetHomeMenus();
        }
        catch (Exception ex)
        {
            
        }

    }

    protected void GetHomeMenus()
    {
        UserMenuMapping objUMM = new UserMenuMapping();
        DataTable dtMenus = objUMM.GetMenusForImage(Session["UserId"].ToString(), false, "0").Tables[0];
        HtmlTableRow tr = new HtmlTableRow();
        HtmlTableCell tc = new HtmlTableCell();
        for (int iMenu = 0; iMenu < dtMenus.Rows.Count; iMenu++)
        {
            string strCode = dtMenus.Rows[iMenu]["Code"].ToString();
            string strName = dtMenus.Rows[iMenu]["Name"].ToString();
            string strImageURL = dtMenus.Rows[iMenu]["ImageURL"].ToString();
            string strNavigateURL = dtMenus.Rows[iMenu]["NavigateURL"].ToString().Replace("{0}", strCode);

            if (iMenu % 5 == 0)
            {
                tr = new HtmlTableRow();
                tblHomeMenu.Rows.Add(tr);

                tc = new HtmlTableCell();
                tr.Cells.Add(tc);
            }


            //tc.Width = "20%";
            //tc.Align = "left";
            tc.Align = "center";
            //if (iMenu == dtMenus.Rows.Count - 1 && iMenu % 5 != 0)
            //{
                //tc.ColSpan = 5 - (iMenu % 5);
                //tc.Width = Convert.ToString(tc.ColSpan * 20) + "%";
            //}

            HtmlImage img = new HtmlImage();
            img.Src = strImageURL;
            img.Alt = strName;

            HtmlAnchor ha = new HtmlAnchor();
            ha.Target = "_self";
            ha.HRef = strNavigateURL;
            ha.Controls.Add(img);

            Label hgc = new Label();
            hgc.Text = "&nbsp;";
            hgc.Width = Unit.Pixel(5);

            tc.Controls.Add(ha);
            tc.Controls.Add(hgc);
        }
    }
}
