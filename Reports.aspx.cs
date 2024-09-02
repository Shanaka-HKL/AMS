using Microsoft.Reporting.WebForms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web;

namespace AMS
{
    public partial class _Reports : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check user authentication
                HttpCookie userin4ck = Request.Cookies["SzxWNHuO4XCyfPMBrVASxNrPPA"];
                if (userin4ck == null)
                {
                    Response.Redirect("~/Login.aspx", false);
                }
                else
                {
                    Idn.Value = Kripta.Decrypt(userin4ck["id"].Trim(), "PPA4XCyfPMBrVASxNr/8A").ToString().Trim();
                }
            }
        }

        protected void GenerateReportButton_Click(object sender, EventArgs e)
        {
            // Validate input
            if (ddlReportType.SelectedValue == "0")
            {
                ErrLbl.ForeColor = System.Drawing.Color.Red;
                ErrLbl.Text = "Select Report Type!";
                return;
            }
            if (string.IsNullOrEmpty(txtFromDate.Text.Trim()))
            {
                ErrLbl.ForeColor = System.Drawing.Color.Red;
                ErrLbl.Text = "Select From Date!";
                return;
            }
            if (string.IsNullOrEmpty(txtToDate.Text.Trim()))
            {
                ErrLbl.ForeColor = System.Drawing.Color.Red;
                ErrLbl.Text = "Select To Date!";
                return;
            }

            // Fetch and load data based on selected report type
            DataTable dt = GetData();
            LoadReport(dt);
        }

        private void LoadReport(DataTable dt)
        {
            // Set the processing mode for the ReportViewer to Local
            ReportViewer1.ProcessingMode = ProcessingMode.Local;

            // Set the path to the RDLC file
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/PerformanceReport.rdlc");

            // Bind DataTable to the report
            ReportDataSource rds = new ReportDataSource("WebsiteDataSet", dt); // The name must match the DataSet in the RDLC
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rds);

            // Refresh the report
            ReportViewer1.LocalReport.Refresh();
        }

        private DataTable GetData()
        {
            DataTable dt = new DataTable();
            //int reportType = Convert.ToInt32(ddlReportType.SelectedValue);
            Serve apir = new Serve();

            //switch (reportType)
            //{
            //    case 1: // Example report type
                    dt = apir.getWebsiteListById("getWebsiteListById", Convert.ToInt16(Idn.Value));
                //    break;

                //case 2: // Another example report type
                //    dt = apir.getWebsiteListById("getWebsiteListById", Convert.ToInt16(Idn.Value));
                //    break;

                //// Add cases for other report types as needed

                //default:
                //    ErrLbl.ForeColor = System.Drawing.Color.Red;
                //    ErrLbl.Text = "Invalid Report Type!";
                //    break;
            //}

            ViewState["ReportData"] = dt;
            return dt;
        }
    }
}
