using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web;
using System.Security.Policy;
using System.Web.Services.Description;
using System.Runtime.InteropServices.ComTypes;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using System.Text;
using iTextSharp.tool.xml;

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
                    Idn.Value = Kripta.Decrypt(userin4ck["id"].Trim(), "PPA4XCyfPMBrVASxNr/8A").ToString().Trim();
                    AdDDL.Text = Kripta.Decrypt(userin4ck["dname"].Trim(), "PPA4XCyfPMBrVASxNr/8A").ToString().Trim();

                    getWebsites();
                    BindCams();
                }
            }
        }

        protected void GenerateReportButton_Click(object sender, EventArgs e)
        {
            if (ddlReportType.SelectedValue == "0")
            {
                ErrLbl.ForeColor = System.Drawing.Color.Red;
                ErrLbl.Text = "Select Report Type!";
            }
            else if (string.IsNullOrEmpty(txtFromDate.Text.Trim()))
            {
                ErrLbl.ForeColor = System.Drawing.Color.Red;
                ErrLbl.Text = "Select From Date!";
            }
            else if (string.IsNullOrEmpty(txtToDate.Text.Trim()))
            {
                ErrLbl.ForeColor = System.Drawing.Color.Red;
                ErrLbl.Text = "Select To Date!";
            }
            else
            {
                if (ddlReportType.SelectedValue == "CampaignPerformance")
                {
                    campaignPerformanceReport();
                    ReportNameLabel.Text = "Campaign Performance Report | From: " + txtFromDate.Text.Trim() + " To: " + txtToDate.Text.Trim();
                    ReportDateLabel.Text = "Generated on: " + DateTime.Now.ToString();
                }
                else if (ddlReportType.SelectedValue == "BannerPerformance")
                {
                    bannerPerformanceReport();
                    ReportNameLabel.Text = "Banner Performance Report | From: " + txtFromDate.Text.Trim() + " To: " + txtToDate.Text.Trim();
                    ReportDateLabel.Text = "Generated on: " + DateTime.Now.ToString();
                }
                else if (ddlReportType.SelectedValue == "WebsitePerformance")
                {
                    websitePerformanceReport();
                    ReportNameLabel.Text = "Website Performance Report | From: " + txtFromDate.Text.Trim() + " To: " + txtToDate.Text.Trim();
                    ReportDateLabel.Text = "Generated on: " + DateTime.Now.ToString();
                }
                else if (ddlReportType.SelectedValue == "ZonePerformance")
                {
                    zonePerformanceReport();
                    ReportNameLabel.Text = "Zone Performance Report | From: " + txtFromDate.Text.Trim() + " To: " + txtToDate.Text.Trim();
                    ReportDateLabel.Text = "Generated on: " + DateTime.Now.ToString();
                }
                else if (ddlReportType.SelectedValue == "CustomReport")
                {
                    customReport();
                    ReportNameLabel.Text = "Custom Report | From: " + txtFromDate.Text.Trim() + " To: " + txtToDate.Text.Trim();
                    ReportDateLabel.Text = "Generated on: " + DateTime.Now.ToString();
                }
            }
        }

        private void campaignPerformanceReport()
        {
            try
            {
                DataTable dta = new DataTable();
                Serve apir = new Serve();
                dta = apir.campaignPerformanceReport("campaignPerformanceReport", txtFromDate.Text.Trim(), txtToDate.Text.Trim(), Convert.ToInt16(Idn.Value));

                if (dta.Rows.Count > 0)
                {
                    ViewState["CampaignTable"] = dta;

                    DynamicReportGridView.DataSource = dta;
                    DynamicReportGridView.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "alert('" + ex.Message + "');", true);
            }
        }

        private void bannerPerformanceReport()
        {
            try
            {
                DataTable dta = new DataTable();
                Serve apir = new Serve();
                dta = apir.bannerPerformanceReport("bannerPerformanceReport", txtFromDate.Text.Trim(), txtToDate.Text.Trim(), Convert.ToInt16(Idn.Value));

                if (dta.Rows.Count > 0)
                {
                    ViewState["CampaignTable"] = dta;

                    DynamicReportGridView.DataSource = dta;
                    DynamicReportGridView.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "alert('" + ex.Message + "');", true);
            }
        }

        private void websitePerformanceReport()
        {
            try
            {
                DataTable dta = new DataTable();
                Serve apir = new Serve();
                dta = apir.websitePerformanceReport("websitePerformanceReport", txtFromDate.Text.Trim(), txtToDate.Text.Trim(), Convert.ToInt16(Idn.Value));

                if (dta.Rows.Count > 0)
                {
                    ViewState["CampaignTable"] = dta;

                    DynamicReportGridView.DataSource = dta;
                    DynamicReportGridView.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "alert('" + ex.Message + "');", true);
            }
        }

        private void zonePerformanceReport()
        {
            try
            {
                DataTable dta = new DataTable();
                Serve apir = new Serve();
                dta = apir.zonePerformanceReport("zonePerformanceReport", txtFromDate.Text.Trim(), txtToDate.Text.Trim(), Convert.ToInt16(Idn.Value));

                if (dta.Rows.Count > 0)
                {
                    ViewState["CampaignTable"] = dta;

                    DynamicReportGridView.DataSource = dta;
                    DynamicReportGridView.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "alert('" + ex.Message + "');", true);
            }
        }
        private void BindCams()
        {
            try
            {
                DataTable dtc = new DataTable();
                Serve apir = new Serve();
                dtc = apir.getCampaignById("getCampaignById", Convert.ToInt16(Idn.Value));

                if (dtc.Rows.Count > 0)
                {
                    CampaignDDL.DataValueField = "Id";
                    CampaignDDL.DataTextField = "Name";
                    CampaignDDL.DataSource = dtc;
                    CampaignDDL.DataBind();
                    CampaignDDL.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "alert('" + ex.Message + "');", true);
            }
        }
        private void getWebsites()
        {
            try
            {
                DataTable dta = new DataTable();
                Serve apir = new Serve();
                dta = apir.getWebsiteByCampaignId("getWebsiteByCampaignId", Convert.ToInt16(Idn.Value));

                if (dta.Rows.Count > 0)
                {
                    WebsiteDDL.DataValueField = "Id";
                    WebsiteDDL.DataTextField = "Name";
                    WebsiteDDL.DataSource = dta;
                    WebsiteDDL.DataBind();
                    WebsiteDDL.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "alert('" + ex.Message + "');", true);
            }
        }

        private void customReport()
        {
            try
            {
                DataTable dta = new DataTable();
                Serve apir = new Serve();
                dta = apir.customReport("customReport", Convert.ToInt16(Idn.Value), Convert.ToInt16(CampaignDDL.SelectedValue.ToString()), Convert.ToInt16(WebsiteDDL.SelectedValue.ToString()), txtFromDate.Text.Trim(), txtToDate.Text.Trim(), Convert.ToInt16(Idn.Value));

                if (dta.Rows.Count > 0)
                {
                    ViewState["CampaignTable"] = dta;

                    DynamicReportGridView.DataSource = dta;
                    DynamicReportGridView.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "alert('" + ex.Message + "');", true);
            }
        }

        protected void btnDownloadPdf_Click(object sender, EventArgs e)
        {
            string htmlContent = CaptureDivHtml("divToExport");
            string nme = "";
            if(ReportNameLabel.Text.Contains("Campaign"))
            {
                nme = "Campaign Performance Report";
            }
            if (ReportNameLabel.Text.Contains("Banner"))
            {
                nme = "Banner Performance Report";
            }
            if (ReportNameLabel.Text.Contains("Website"))
            {
                nme = "Website Performance Report";
            }
            if (ReportNameLabel.Text.Contains("Zone"))
            {
                nme = "Zone Performance Report";
            }
            if (ReportNameLabel.Text.Contains("Custom"))
            {
                nme = "Custom Report";
            }
            GeneratePdf(htmlContent, nme + ".pdf");
        }
        private string CaptureDivHtml(string divId)
        {
            StringBuilder sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            {
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                var div = FindControl(divId) as Control;
                if (div != null)
                {
                    div.RenderControl(htw);
                }
            }
            string htmlContent = sb.ToString();

            // Log or debug the HTML content
            System.Diagnostics.Debug.WriteLine(htmlContent);

            return htmlContent;
        }

        private void GeneratePdf(string htmlContent, string fileName)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Document document = new Document(PageSize.A4);
                PdfWriter writer = PdfWriter.GetInstance(document, ms);
                document.Open();

                // Convert HTML to PDF
                using (StringReader sr = new StringReader(htmlContent))
                {
                    XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, sr);
                }

                //document.Close();

                // Return PDF as a file download
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
                Response.BinaryWrite(ms.ToArray());
                Response.End();
            }
        }
    }
}
