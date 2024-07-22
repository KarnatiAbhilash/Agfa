using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Handlers_Download : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpResponse response = HttpContext.Current.Response;
        response.Clear();
        response.Clear();
        response.ContentType = "application/pdf";
        response.AppendHeader("content-disposition", "filename=" + this.Session["FileName"].ToString());
        response.TransmitFile(this.Session["FileName"].ToString());
        response.End();
    }
}