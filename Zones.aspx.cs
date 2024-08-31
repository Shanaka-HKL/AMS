using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace AMS
{
    public partial class _Zones : Page
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

                    BindZoneGridView();
                }
            }
        }

        protected void ZoneGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ZoneGridView.PageIndex = e.NewPageIndex;

            DataTable dt = ViewState["ZoneTable"] as DataTable;

            if (dt != null)
            {
                ZoneGridView.DataSource = dt;
                ZoneGridView.DataBind();
            }
        }

        private void BindZoneGridView()
        {
            try
            {
                DataTable dta = new DataTable();
                Serve apir = new Serve();
                dta = apir.getZoneListById("getZoneListById", Convert.ToInt16(Idn.Value));

                if (dta.Rows.Count > 0)
                {
                    ViewState["ZoneTable"] = dta;

                    ZoneGridView.DataSource = dta;
                    ZoneGridView.DataBind();
                }
                
                BindDropDowns();
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
                dtc = apir.getWebsiteByAdvertiserId("getWebsiteByAdvertiserId", Convert.ToInt16(Idn.Value));

                if (dtc.Rows.Count > 0)
                {
                    WebsiteDDL.DataValueField = "Id";
                    WebsiteDDL.DataTextField = "Name";
                    WebsiteDDL.DataSource = dtc;
                    WebsiteDDL.DataBind();
                    WebsiteDDL.SelectedIndex = 0;
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
                string result = apir.updateZoneById("updateZoneById", websiteID, status, Convert.ToInt16(Idn.Value));

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

        protected void CreateZoneButton_Click(object sender, EventArgs e)
        {
            ErrLbl.ForeColor = Color.Red;

            if (WebsiteDDL.SelectedValue.ToString() == "0")
            {
                ErrLbl.Text = "Select a Website!";
            }
            else if (txtZoneName.Text.Trim() == "")
            {
                ErrLbl.Text = "Enter Zone Name!";
            }
            else if (ddlZoneTypeDDL.SelectedValue.ToString() == "0")
            {
                ErrLbl.Text = "Select Zone Type!";
            }
            else if (ddlZoneSizeDDL.SelectedValue.ToString() == "0")
            {
                ErrLbl.Text = "Select Zone Size!";
            }
            else if ((ddlZoneSizeDDL.SelectedValue.ToString() == "0") && (txtWidth.Text.Trim() == "" || txtHeight.Text.Trim() == ""))
            {
                ErrLbl.Text = "Enter Zone Sizes!";
            }
            else
            {
                PostAPI apir = new PostAPI();
                string reslt = "";

                reslt = InsertRecord(WebsiteDDL.SelectedValue.ToString().Trim(), ddlZoneTypeDDL.SelectedValue.ToString().Trim(), ddlZoneSizeDDL.SelectedValue.ToString().Trim(), txtZoneDescription.Text.Trim(),
                    txtZoneName.Text.Trim(), txtWidth.Text.Trim(), txtHeight.Text.Trim());
                if (reslt.Contains(" successful"))
                {
                    ErrLbl.ForeColor = Color.Green;
                    ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "alert('" + reslt + "');", true);
                    ErrLbl.Text = reslt;

                    BindZoneGridView();

                    WebsiteDDL.SelectedIndex = 0;
                    ddlZoneTypeDDL.SelectedIndex = 0;
                    ddlZoneSizeDDL.SelectedIndex = 0;
                    txtZoneDescription.Text = "";
                    txtZoneName.Text = "";
                    txtWidth.Text = "";
                    txtHeight.Text = "";
                }
                else
                {
                    ErrLbl.ForeColor = Color.Red;
                    ErrLbl.Text = reslt;
                }
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

            BindZoneGridView();
        }

        protected void DeactivateButton_Click(object sender, EventArgs e)
        {
            LinkButton clickedButton = sender as LinkButton;
            if (clickedButton != null)
            {
                int websiteID = Convert.ToInt32(clickedButton.CommandArgument);
                UpdateStatus(websiteID, 0);
            }

            BindZoneGridView();
        }
        public string InsertRecord(string WebSiteDDL_, string ddlZoneTypeDDL, string ddlZoneSizeDDL, string txtZoneDescription, string txtZoneName, string txtWidth, string txtHeight)
        {
            try
            {
                // Parse numeric values
                int width = 0;
                int height = 0;

                // Try parsing the width and height values
                int.TryParse(txtWidth, out width);
                int.TryParse(txtHeight, out height);

                Serve apir = new Serve();
                string result = apir.insertZone("insertZone", txtZoneName,
                    txtZoneDescription,
                    WebSiteDDL_,
                    ddlZoneTypeDDL,
                    ddlZoneSizeDDL,
                    width,
                    height, Convert.ToInt16(Idn.Value));

                return result;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "alert('" + ex.Message + "');", true);
                return ex.Message;
            }
        }
        public static string GenerateRandomKey(int size = 32)
        {
            // Size is in bytes; a 32-byte key will generate a 256-bit key
            byte[] randomBytes = new byte[size];

            using (var rng = new RNGCryptoServiceProvider())
            {
                // Fill the array with cryptographically strong random bytes
                rng.GetBytes(randomBytes);
            }

            // Convert the byte array to a base64 string
            return Convert.ToBase64String(randomBytes);
        }

        protected void DownloadButton_Click(object sender, EventArgs e)
        {
            LinkButton clickedButton = sender as LinkButton;
            if (clickedButton != null)
            {
                int rowIndex = Convert.ToInt32(clickedButton.CommandArgument);
                GridViewRow row = ZoneGridView.Rows[rowIndex];
                string websiteName = GenerateRandomKey();

                string location = "~/Uploads/";
                string div_width = "800";
                string div_height = "640";
                string target = "_blank";

                // Fetch files from the directory
                string mediaFolder = Server.MapPath(location);
                var mediaFiles = Directory.GetFiles(mediaFolder)
                    .Select(filePath => new
                    {
                        Url = location + Path.GetFileName(filePath),
                        Priority = int.Parse(Path.GetFileName(filePath)[0].ToString()), // First letter of filename as priority
                        Extension = Path.GetExtension(filePath)
                    })
                    .OrderBy(file => file.Priority) // Sort by priority
                    .ToList();

                string mediaItemsScriptArray = string.Join(",\n", mediaFiles.Select(file => $@"{{ url: '{file.Url}', type: '{file.Extension.Replace(".", "")}', priority: {file.Priority} }}"));

                string script = $@"<!-- -----Paid Advertisement AMS IQ------ -->
                            <div id=""ad-container"" style=""width: {div_width}px; height: {div_height}px;""></div>
                            <script>
                              // Array of media URLs with their priorities and types
                              const mediaItems = [
                                {mediaItemsScriptArray}
                              ];

                              let currentIndex = 0;
                              const displayTime = 5 * 60 * 1000; // 5 minutes in milliseconds

                              function loadAdContent() {{
                                const adContainer = document.getElementById('ad-container');
                                const currentMedia = mediaItems[currentIndex];

                                // Clear previous content
                                adContainer.innerHTML = '';

                                if (currentMedia.type === 'txt') {{
                                  fetch(currentMedia.url)
                                    .then(response => response.text())
                                    .then(data => {{
                                      const textElement = document.createElement('p');
                                      textElement.textContent = data;
                                      adContainer.appendChild(textElement);
                                    }});
                                }} else if (currentMedia.type === 'html') {{
                                  fetch(currentMedia.url)
                                    .then(response => response.text())
                                    .then(data => {{
                                      adContainer.innerHTML = data;
                                    }});
                                }} else if (currentMedia.type === 'jpg' || currentMedia.type === 'png' || currentMedia.type === 'gif') {{
                                  const imgElement = document.createElement('img');
                                  imgElement.src = currentMedia.url;
                                  imgElement.style.width = '100%';
                                  adContainer.appendChild(imgElement);
                                }} else if (currentMedia.type === 'mp4' || currentMedia.type === 'webm') {{
                                  const videoElement = document.createElement('video');
                                  videoElement.src = currentMedia.url;
                                  videoElement.controls = true;
                                  videoElement.style.width = '100%';
                                  adContainer.appendChild(videoElement);
                                }} else {{
                                  const defaultMessage = document.createElement('p');
                                  defaultMessage.textContent = 'Advertisement content could not be loaded.';
                                  adContainer.appendChild(defaultMessage);
                                }}

                                adContainer.onclick = function () {{
                                  window.location.href = 'HitAd.aspx';
                                  window.location.target = '{target}';
                                }};

                                currentIndex = (currentIndex + 1) % mediaItems.length;
                              }}

                              // Initial display of the first media item
                              loadAdContent();

                              // Set an interval to change the media item every 5 minutes
                              setInterval(loadAdContent, displayTime);
                            </script>
                            <!-- -----Paid Advertisement AMS IQ------ -->";

                Session["DownloadContent"] = script;
                Session["DownloadFileName"] = $"{websiteName}.txt";

                ScriptManager.RegisterStartupScript(this, GetType(), "DownloadFile", "window.open('DM.aspx', '_blank');", true);
            }

            BindZoneGridView();
        }
    }
}