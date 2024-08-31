using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AMS
{
    public partial class _DM : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["DownloadContent"] != null && Session["DownloadFileName"] != null)
            {
                string content = Session["DownloadContent"].ToString();
                string fileName = Session["DownloadFileName"].ToString();

                Response.Clear();
                Response.ContentType = "text/plain";
                Response.AddHeader("Content-Disposition", $"attachment;filename={fileName}");
                Response.Write(content);
                Response.End();
            }
        }
    }
}