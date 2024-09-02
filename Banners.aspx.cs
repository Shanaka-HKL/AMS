using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.IO.Compression;
using System.Linq.Expressions;

namespace AMS
{
    public partial class _Banners : Page
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

                    BindBannerGridView();
                }
            }
        }

        protected void BannerGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BannerGridView.PageIndex = e.NewPageIndex;

            DataTable dt = ViewState["BannerTable"] as DataTable;

            if (dt != null)
            {
                BannerGridView.DataSource = dt;
                BannerGridView.DataBind();
            }
        }

        private void BindBannerGridView()
        {
            try
            {
                DataTable dt = new DataTable();
                Serve apir = new Serve();
                dt = apir.getBannerListById("getBannerListById", Convert.ToInt16(Idn.Value));

                if (dt.Rows.Count > 0)
                {
                    ViewState["BannerTable"] = dt;

                    BannerGridView.DataSource = dt;
                    BannerGridView.DataBind();
                }
                
                BindDropDowns();
                getWebsites();
                getZones();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "alert('" + ex.Message + "');", true);
            }
        }

        private void BindDropDowns()
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

        private void UpdateStatus(int websiteID, int status)
        {
            try
            {
                Serve apir = new Serve();
                string result = apir.updateBannerById("updateBannerById", websiteID, status, Convert.ToInt16(Idn.Value));

                if (result.Contains(" successful"))
                {
                    ErrLbl.ForeColor = Color.Green;
                    ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "alert('" + result + "');", true);
                    ErrLbl.Text = result;
                }
                else
                {
                    ErrLbl.ForeColor = Color.Red;
                    ErrLbl.Text = result;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "alert('" + ex.Message + "');", true);
            }
        }
        private static string GenerateRandomKey(int length)
        {
            byte[] buff = new byte[length];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(buff);
            }
            return BitConverter.ToString(buff).Replace("-", "");
        }

        protected void CreateBannerButton_Click(object sender, EventArgs e)
        {
            ErrLbl.ForeColor = Color.Red;

            if (CampaignDDL.SelectedValue.ToString() == "0")
            {
                ErrLbl.Text = "Select a Campaign!";
            }
            else if (WebsiteDDL.SelectedValue.ToString() == "0")
            {
                ErrLbl.Text = "Select a Website!";
            }
            else if (ZonesDDL.SelectedValue.ToString() == "0")
            {
                ErrLbl.Text = "Select a Zone!";
            }
            else if (txtBannerName.Text.Trim() == "")
            {
                ErrLbl.Text = "Enter Banner Name!";
            }
            else if (ddlBannerType.SelectedValue.ToString() == "0")
            {
                ErrLbl.Text = "Select a Banner Type!";
            }
            else if (!fileBannerUpload.HasFile)
            {
                ErrLbl.Text = "Upload the Media!";
            }
            else if (txtBannerLink.Text.Trim() == "")
            {
                ErrLbl.Text = "Enter the website link this banner points to!";
            }
            else if (ddlTarget.SelectedValue.ToString() == "0")
            {
                ErrLbl.Text = "Select the Target!";
            }
            else
            {
                bool proceed; string filenme = "";
                string fileExtension = System.IO.Path.GetExtension(fileBannerUpload.FileName).ToLower();
                string[] allowedExtensions = { ".html", ".htm", ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".webp", ".txt", ".mp4", ".avi", ".mkv", ".mov", ".wmv", ".zip" };

                if (Array.Exists(allowedExtensions, ext => ext == fileExtension))
                {
                    if (fileBannerUpload.PostedFile.ContentLength <= 5242880) // 5MB in bytes
                    {
                        string folderPath = Server.MapPath("~/Uploads/");
                        string savePath = "";

                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            System.IO.Directory.CreateDirectory(folderPath);
                        }

                        string key = GenerateRandomKey(50);

                        if (key.Length > 7)
                        {
                            key = key.Substring(0, 7);
                        }

                        if (fileExtension == ".zip")
                        {
                            string extractPath = folderPath + key;//filenme.Replace(".zip", "");
                            ZipFile.ExtractToDirectory(savePath, extractPath);

                            try
                            {
                                // Find the main HTML file
                                string[] htmlFiles = Directory.GetFiles(extractPath, "*.html");
                                if (htmlFiles.Length > 0)
                                {
                                    string mainHtmlFile = htmlFiles[0];

                                    string htmlContent = File.ReadAllText(mainHtmlFile);
                                    htmlContent = htmlContent.Replace("src=\"", "src=\"" + extractPath + "/");
                                    File.WriteAllText(mainHtmlFile, htmlContent);

                                    //filenme = Path.GetFileName(mainHtmlFile);
                                    filenme = CampaignDDL.SelectedValue.ToString() + "_" + WebsiteDDL.SelectedValue.ToString() + "_" + ZonesDDL.SelectedValue.ToString() + "_" + key + fileExtension;
                                    savePath = folderPath + filenme;

                                    fileBannerUpload.SaveAs(savePath);
                                    ErrLbl.Text = "File uploaded successfully!";
                                    proceed = true;
                                }
                                else
                                {
                                    ErrLbl.Text = "HTML file not found in this attachment!";
                                    proceed = false;
                                }
                            }
                            catch
                            {
                                ErrLbl.Text = "HTML file not found or currupted in this attachment!";
                                proceed = false;
                            }
                        }
                        else
                        {
                            filenme = CampaignDDL.SelectedValue.ToString() + "_" + WebsiteDDL.SelectedValue.ToString() + "_" + ZonesDDL.SelectedValue.ToString() + "_" + key + fileExtension;
                            savePath = folderPath + filenme;

                            fileBannerUpload.SaveAs(savePath);
                            ErrLbl.Text = "File uploaded successfully!";
                            proceed = true;
                        }
                    }
                    else
                    {
                        ErrLbl.Text = "File size exceeds the 5MB limit.";
                        proceed = false;
                    }
                }
                else
                {
                    ErrLbl.Text = "Invalid file type. Only HTML, Image, Text, and Video files are allowed.";
                    proceed = false;
                }

                if (proceed == true)
                {
                    PostAPI apir = new PostAPI();
                    string reslt = "";

                    reslt = InsertRecord(filenme, WebsiteDDL.SelectedValue.ToString().Trim(), ddlBannerSizeDDL.SelectedValue.ToString().Trim(), CampaignDDL.SelectedValue.ToString().Trim(), ZonesDDL.SelectedValue.ToString().Trim(), ddlBannerType.SelectedValue.ToString().Trim(),
                        ddlTarget.SelectedValue.ToString().Trim(), txtBannerLink.Text.Trim(), txtBannerName.Text.Trim());
                    if (reslt.Contains(" successful"))
                    {
                        ErrLbl.ForeColor = Color.Green;
                        ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "alert('" + reslt + "');", true);
                        ErrLbl.Text = reslt;

                        BindBannerGridView();

                        CampaignDDL.SelectedIndex = 0;
                        WebsiteDDL.SelectedIndex = 0;
                        ZonesDDL.SelectedIndex = 0;
                        ddlBannerSizeDDL.SelectedIndex = 0;
                        ddlBannerType.SelectedIndex = 0;
                        ddlTarget.SelectedIndex = 0;
                        txtBannerLink.Text = "";
                        txtBannerName.Text = "";
                    }
                    else
                    {
                        ErrLbl.ForeColor = Color.Red;
                        ErrLbl.Text = reslt;
                    }
                }
            }
        }

        public string InsertRecord(string filenme, string WebsiteId, string BannerSize, string CampaignDDLVlu, string ZonesDDLVlu, string ddlBannerTypeVlu, string ddlTargetVlu, string txtBannerLinkVlu, string txtBannerNameVlu)
        {
            try
            {
                Serve apir = new Serve();
                string result = apir.insertBanner("insertBanner", filenme, Convert.ToInt16(WebsiteId), BannerSize, Convert.ToInt16(CampaignDDLVlu), Convert.ToInt16(ZonesDDLVlu), ddlBannerTypeVlu, ddlTargetVlu, txtBannerLinkVlu, txtBannerNameVlu, Convert.ToInt16(Idn.Value));

                return result;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "alert('" + ex.Message + "');", true);
                return ex.Message;
            }
        }

        protected void ActivateButton_Click(object sender, EventArgs e)
        {
            LinkButton clickedButton = sender as LinkButton;
            if (clickedButton != null)
            {
                int websiteID = Convert.ToInt32(clickedButton.CommandArgument);
                UpdateStatus(websiteID, 1);
            }

            BindBannerGridView();
        }

        protected void DeactivateButton_Click(object sender, EventArgs e)
        {
            LinkButton clickedButton = sender as LinkButton;
            if (clickedButton != null)
            {
                int websiteID = Convert.ToInt32(clickedButton.CommandArgument);
                UpdateStatus(websiteID, 0);
            }

            BindBannerGridView();
        }

        private void getZones()
        {
            try
            {
                DataTable dta = new DataTable();
                Serve apir = new Serve();
                dta = apir.getZonesById("getZonesById", Convert.ToInt16(Idn.Value));

                if (dta.Rows.Count > 0)
                {
                    ZonesDDL.DataValueField = "Id";
                    ZonesDDL.DataTextField = "Name";
                    ZonesDDL.DataSource = dta;
                    ZonesDDL.DataBind();
                    ZonesDDL.SelectedIndex = 0;
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
    }
}