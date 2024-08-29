using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace AMS
{
    public partial class _Reports : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HttpCookie userin4ck = Request.Cookies["SzxWNHuO4XCyfPMBrVASxNrPPA"];

                if (userin4ck == null)
                {
                    Response.Redirect("~/Login.aspx", false);
                }
                else
                {
                    //Do nothing
                }
            }
        }

        protected void GenerateReportButton_Click(object sender, EventArgs e)
        {
            ErrLbl.ForeColor = Color.Red;
            
            if (ddlReportType.SelectedValue.ToString() == "0")
            {
                ErrLbl.Text = "Select Report Type!";
            }
            else if (txtFromDate.Text.Trim() == "")
            {
                ErrLbl.Text = "Select From Date!";
            }
            else if (txtToDate.Text.Trim() == "")
            {
                ErrLbl.Text = "Select To Date!";
            }
            else
            {
                ErrLbl.ForeColor = Color.Green;
                ErrLbl.Text = "Email sent. One of our agents will contact you soon!";

                ddlReportType.SelectedIndex = 0;
                AdDDL.SelectedIndex = 0;
                CampaignDDL.SelectedIndex = 0;
                WebsiteDDL.SelectedIndex = 0;
                txtFilter.Text = "";
                txtFromDate.Text = "";
                txtToDate.Text = "";
            }
        }
    }
}