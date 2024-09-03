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
using System.IO;
using System.Text;
using PdfSharp.Pdf;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using PdfSharp.Drawing;
using iTextSharp.text.pdf;
using iTextSharp.text;

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
                }
                else if (ddlReportType.SelectedValue == "BannerPerformance")
                {
                    bannerPerformanceReport();
                }
                else if (ddlReportType.SelectedValue == "WebsitePerformance")
                {
                    websitePerformanceReport();
                }
                else if (ddlReportType.SelectedValue == "ZonePerformance")
                {
                    zonePerformanceReport();
                }
                else if (ddlReportType.SelectedValue == "CustomReport")
                {
                    customReport();
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

                    ReportNameLabel.Text = "Campaign Performance Report | From: " + txtFromDate.Text.Trim() + " To: " + txtToDate.Text.Trim();
                    ReportDateLabel.Text = "Generated on: " + DateTime.Now.ToString();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "alert('" + "No Record(s) found!" + "');", true);
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

                    ReportNameLabel.Text = "Banner Performance Report | From: " + txtFromDate.Text.Trim() + " To: " + txtToDate.Text.Trim();
                    ReportDateLabel.Text = "Generated on: " + DateTime.Now.ToString();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "alert('" + "No Record(s) found!" + "');", true);
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

                    ReportNameLabel.Text = "Website Performance Report | From: " + txtFromDate.Text.Trim() + " To: " + txtToDate.Text.Trim();
                    ReportDateLabel.Text = "Generated on: " + DateTime.Now.ToString();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "alert('" + "No Record(s) found!" + "');", true);
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

                    ReportNameLabel.Text = "Zone Performance Report | From: " + txtFromDate.Text.Trim() + " To: " + txtToDate.Text.Trim();
                    ReportDateLabel.Text = "Generated on: " + DateTime.Now.ToString();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "alert('" + "No Record(s) found!" + "');", true);
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

                    ReportNameLabel.Text = "Custom Report | From: " + txtFromDate.Text.Trim() + " To: " + txtToDate.Text.Trim();
                    ReportDateLabel.Text = "Generated on: " + DateTime.Now.ToString();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "alert('" + "No Record(s) found!" + "');", true);
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
            if (DynamicReportGridView.Rows.Count > 0)
            {
                string nme = "";

                if (ReportNameLabel.Text.Contains("Campaign"))
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

                try
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        // Initialize the PDF document using iTextSharp
                        using (Document document = new Document())
                        {
                            PdfWriter.GetInstance(document, memoryStream);
                            document.Open();

                            // Create a table in the PDF with the same number of columns as the GridView
                            PdfPTable pdfTable = new PdfPTable(DynamicReportGridView.HeaderRow.Cells.Count);

                            // Add the headers from the GridView to the PDF table
                            foreach (TableCell headerCell in DynamicReportGridView.HeaderRow.Cells)
                            {
                                PdfPCell pdfCell = new PdfPCell(new Phrase(headerCell.Text));
                                pdfTable.AddCell(pdfCell);
                            }

                            // Add the data rows to the PDF table
                            foreach (GridViewRow gridViewRow in DynamicReportGridView.Rows)
                            {
                                foreach (TableCell gridViewCell in gridViewRow.Cells)
                                {
                                    PdfPCell pdfCell = new PdfPCell(new Phrase(gridViewCell.Text));
                                    pdfTable.AddCell(pdfCell);
                                }
                            }

                            // Add the table to the PDF document
                            document.Add(pdfTable);
                            document.Close();
                        }

                        // Prepare the PDF file for download
                        Response.Clear();
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-disposition", "attachment;filename=" + nme + ".pdf");
                        Response.Buffer = true;
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);

                        // Write the PDF to the response
                        Response.BinaryWrite(memoryStream.ToArray());
                        Response.End();
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    ScriptManager.RegisterStartupScript(this, GetType(), "Alert", $"alert('An error occurred: {ex.Message}');", true);
                }
            }
            else
            {
                // Handle the case when there is no data to export
                ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "alert('No data available to export.');", true);
            }
        }

        private string CaptureDivHtml(string divId)
        {
            Control ctrl = Page.FindControl(divId);
            if (ctrl != null)
            {
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                ctrl.RenderControl(htw);
                return sw.ToString();
            }
            return string.Empty;
        }

        private void GeneratePdf(string htmlContent, string fileName)
        {
            PdfSharp.Pdf.PdfDocument pdf = PdfGenerator.GeneratePdf(htmlContent, PdfSharp.PageSize.A4);
            using (MemoryStream ms = new MemoryStream())
            {
                pdf.Save(ms, false);

                // Return PDF as a file download
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
                Response.BinaryWrite(ms.ToArray());
                Response.End();
            }
        }
    }
}
