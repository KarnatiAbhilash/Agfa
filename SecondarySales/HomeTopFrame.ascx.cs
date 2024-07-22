using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using BusinessClass;

public partial class HomeTopFrame : System.Web.UI.UserControl
{
    DataSet dsMenu;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null) Response.Redirect("../Logout.aspx");
       
        if (!IsPostBack)
        {
            try
            {
                if (Request.UserAgent.IndexOf("AppleWebKit") > 0 || Request.UserAgent.IndexOf("Unknown") > 0 || Request.UserAgent.IndexOf("Chrome") > 0)
                    Request.Browser.Adapters.Clear();

                lblWelcome.InnerText = "Welcome " + Session["UserName"].ToString();
                lblDateTime.InnerText = DateTime.Today.ToString("ddd") + ", " + DateTime.Today.ToString("MMM dd,yyyy");//DateTime.Today.DayOfWeek.ToString() + ", " + DateTime.Today.ToString("MMM dd,yyyy");
                GenerateDynamicMenu();
            }
            catch (Exception ex)
            {
             
            }
        }
    }
    protected void GenerateDynamicMenu()
    {
        int menucount = 0;
        //mnr = new MenuRow("Menu",Session["UserID"].ToString());
        //dsMenu = mnr.prpDS;
        dsMenu = Session["dsMenu"] as DataSet;

        string PrevMainCode = "0";
        string MainCode = "0", SubMainCode = "0";
        string MainName, SubName, MainImageURL, MainNavigateURL, SubImageURL, SubNavigateURL;

        //MenuItem TopItem = MenuAgfa.Items.FindItemByValue("Top");
        DataTable dtadmin=new DataTable();
        DataTable dtMenu = dsMenu.Tables[0];
        MenuItem radmn;
        //MenuItem sepmenu;
        if (Session["UserName"].ToString() == "admin")
        {
            for (int i = 0; i<dtMenu.Rows.Count; i++)
             {
                 if (dtMenu.Rows[i]["MM_Code"].ToString() == "Changepassword")
                  {
                      dtMenu.Rows.RemoveAt(i);
                     
                      //DataColumn MMcode = new DataColumn("MM_Code", typeof(string));
                      //DataColumn MM_Name = new DataColumn("MM_Name", typeof(string));
                      //DataColumn MM_NavigateURL = new DataColumn("MM_NavigateURL", typeof(string));
                      //DataColumn SM_Code = new DataColumn("SM_Code", typeof(string));
                      //DataColumn SM_Name = new DataColumn("SM_Name", typeof(string));
                      //DataColumn SM_NavigateURL = new DataColumn("SM_NavigateURL", typeof(string));
                      //dtadmin.Columns.Add(MMcode);
                      //dtadmin.Columns.Add(MM_Name);
                      //dtadmin.Columns.Add(MM_NavigateURL);
                      //dtadmin.Columns.Add(SM_Code);
                      //dtadmin.Columns.Add(SM_Name);
                      //dtadmin.Columns.Add(SM_NavigateURL);

                      //DataRow dr ;
                      //dr = dtadmin.NewRow(); 
                      //dr["MM_Code"]="Admin";
                      //dr["MM_Name"] = "Admin Master";
                      //dr["MM_NavigateURL"] = "";
                      //dr["SM_Code"] = "Change Password";
                      //dr["SM_Name"] = "Change Password";
                      //dr["SM_NavigateURL"] = "~/Admin/ChangePassword.aspx";
                      //dtadmin.Rows.Add(dr);
                    
                       ////radmn = new MenuItem();
                       ////radmn.NavigateUrl = dtMenu.Rows[i]["SM_NavigateURL"].ToString();
                       ////radmn.Text = dtMenu.Rows[i]["SM_Name"].ToString();
                       ////MenuAgfa.FindItem("Admin Master").ChildItems.Add(radmn);
                       ////radmn = null;
                      ////dtMenu.Rows.RemoveAt(i);     DataTable dt = new DataTable();
                      
                      //DataColumn Name = new DataColumn("Name", typeof(string));
                      //dt.Columns.Add(Name);
                      //DataRow dr;
                      //dr = dt.NewRow();
                      //dr["Name"] = strRetVal;
                      //dt.Rows.Add(dr);
                     
                      
                  }
             }
        }

        for (int parsemenu = 0; parsemenu < dtMenu.Rows.Count; parsemenu++)
        {
            MainCode = Convert.ToString(dtMenu.Rows[parsemenu]["MM_Code"].ToString());
            SubMainCode = Convert.ToString(dtMenu.Rows[parsemenu]["SM_Code"].ToString());
            MainName = Convert.ToString(dtMenu.Rows[parsemenu]["MM_Name"]);
            SubName = Convert.ToString(dtMenu.Rows[parsemenu]["SM_Name"]);
            MainImageURL = Convert.ToString(dtMenu.Rows[parsemenu]["MM_ImageURL"]);
            SubImageURL = Convert.ToString(dtMenu.Rows[parsemenu]["SM_ImageURL"]);
            MainNavigateURL = Convert.ToString(dtMenu.Rows[parsemenu]["MM_NavigateURL"]).Replace("{0}", MainCode);
            SubNavigateURL = Convert.ToString(dtMenu.Rows[parsemenu]["SM_NavigateURL"]);

            if (PrevMainCode != MainCode)
            {
                PrevMainCode = MainCode;
                menucount = menucount + 1;

                radmn = new MenuItem();
                if (MainNavigateURL != "")
                    radmn.NavigateUrl = MainNavigateURL;
                radmn.Value = MainName;
                radmn.Text = MainName;
                MenuAgfa.Items.Add(radmn);

                //radmn.Text += "<img src=\"../Common/Images/SortDesc.gif\" alt=\"\"  style=\"cursor:inherit;\" />";
                radmn = null;
            }

            if (SubName != "")
            {
                radmn = new MenuItem();
                radmn.NavigateUrl = SubNavigateURL;
                radmn.Text = SubName;
                MenuAgfa.FindItem(MainName).ChildItems.Add(radmn);
                radmn = null;
               
                
            }
        }
        
        if (MenuAgfa.Items.Count != 5)
        {
            MenuAgfa.StaticMenuItemStyle.CssClass = "HeaderMenuStaticAdmin";
            MenuAgfa.DynamicHoverStyle.CssClass = "HeaderMenuHoverAdmin";
            MenuAgfa.DynamicMenuItemStyle.CssClass = "HeaderMenuStaticAdmin IE8Fix";
            MenuAgfa.DynamicMenuItemStyle.Width = Unit.Pixel(185);
            MenuAgfa.Width = Unit.Percentage(100);
            MenuAgfa.StaticHoverStyle.CssClass = "HeaderMenuHoverAdmin";
            MenuAgfa.StaticMenuStyle.CssClass = "MenuStyle";
        }

    }
}
