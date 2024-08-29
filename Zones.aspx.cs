using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace AMS
{
    public partial class _Zones : Page
    {
        String Id = "";
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
                    Id = Kripta.Decrypt(userin4ck["id"].Trim(), "PPA4XCyfPMBrVASxNr/8A").ToString().Trim();

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
                string JsonInput = "{\r\n    \"UserId\" : " + "'" + Id + "'" + "\r\n}";

                DataTable dta = new DataTable();

                PostAPI apir = new PostAPI();

                dta = apir.get_datatable("getZoneListById", JsonInput, "POST");

                if (dta.Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Id", typeof(int));
                    dt.Columns.Add("WebsiteName", typeof(string));
                    dt.Columns.Add("ZoneName", typeof(string));
                    dt.Columns.Add("ZoneType", typeof(string));
                    dt.Columns.Add("ZoneSize", typeof(string));
                    dt.Columns.Add("CreatedBy", typeof(string));
                    dt.Columns.Add("CreatedDate", typeof(DateTime));
                    dt.Columns.Add("UpdatedDate", typeof(DateTime));
                    dt.Columns.Add("Status", typeof(string));

                    var zoneTypeMapping = new Dictionary<string, string>
    {
        { "Banner", "Banner, Button or Rectangle" },
        { "Interstitial", "Interstitial or Floating DHTML" },
        { "TextAd", "Text ad" },
        { "EmailNewsletter", "Email/Newsletter zone" },
        { "InlineVideoAd", "Inline Video ad" },
        { "OverlayVideoAd", "Overlay Video ad" }
    };

                    var zoneSizeMapping = new Dictionary<string, string>
    {
        { "Banner", "Banner, Button or Rectangle" },
        { "468x60", "IAB Full Banner (468 x 60)" },
        { "120x600", "IAB Skyscraper (120 x 600)" },
        { "728x90", "IAB Leaderboard (728 x 90)" },
        { "120x90", "IAB Button 1 (120 x 90)" },
        { "120x60", "IAB Button 2 (120 x 60)" },
        { "234x60", "IAB Half Banner (234 x 60)" },
        { "88x31", "IAB Micro Bar (88 x 31)" },
        { "125x125", "IAB Square Button (125 x 125)" },
        { "120x240", "IAB Vertical Banner (120 x 240)" },
        { "180x150", "IAB Rectangle (180 x 150)" },
        { "300x250", "IAB Medium Rectangle (300 x 250)" },
        { "336x280", "IAB Large Rectangle (336 x 280)" },
        { "240x400", "IAB Vertical Rectangle (240 x 400)" },
        { "250x250", "IAB Square Pop-up (250 x 250)" },
        { "160x600", "IAB Wide Skyscraper (160 x 600)" },
        { "720x300", "IAB Pop-Under (720 x 300)" },
        { "-", "Custom" }
    };

                    foreach (DataRow dr in dta.Rows)
                    {
                        string zoneTypeText = zoneTypeMapping.ContainsKey(dr["ZoneType"].ToString())
    ? zoneTypeMapping[dr["ZoneType"].ToString()]
    : dr["ZoneType"].ToString();

                        string zoneSizeText = zoneSizeMapping.ContainsKey(dr["ZoneSize"].ToString())
                            ? zoneSizeMapping[dr["ZoneSize"].ToString()]
                            : dr["ZoneSize"].ToString();

                        dt.Rows.Add(dr["Id"].ToString(), dr["WebsiteName"].ToString(), dr["ZoneName"].ToString(),
            zoneTypeText,
            zoneSizeText,

                            dr["CreatedBy"].ToString(), dr["CreatedDate"].ToString(),
                            dr["UpdatedDate"].ToString(), dr["Status"].ToString());
                    }

                    ViewState["ZoneTable"] = dt;

                    ZoneGridView.DataSource = dt;
                    ZoneGridView.DataBind();

                    BindDropDowns();
                }
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
                string JsonInput = "{\r\n    \"AdvertiserId\" : " + "'" + Id + "'" + "\r\n}";

                DataTable dtc = new DataTable();

                PostAPI apir = new PostAPI();

                dtc = apir.get_datatable("getWebsiteByAdvertiserId", JsonInput, "POST");

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
                string JsonInput = "{\r\n   \"ZoneId\" : " + "'" + websiteID + "'" + "\r\n  \"Status\" : " + "'" + status + "'" + "\r\n  \"UserId\" : " + "'" + Id + "'" + "\r\n}";

                PostAPI apir = new PostAPI();

                string result = apir.get_string("updateZoneById", JsonInput, "POST");

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
                string JsonInput = "{\r\n    \"ZoneTypeId\" : " + "'" + ddlZoneTypeDDL + "'" +
                    ",\r\n    \"ZoneSizeId\" : " + "'" + ddlZoneSizeDDL + "'" +
                    ",\r\n    \"Description\" : " + "'" + txtZoneDescription + "'" +
                    ",\r\n    \"WebsiteId\" : " + "'" + WebSiteDDL_ + "'" +
                    ",\r\n    \"Name\" : " + "'" + txtZoneName + "'" +
                    ",\r\n    \"mWidth\" : " + "'" + txtWidth + "'" +
                    ",\r\n    \"mHeight\" : " + "'" + txtHeight + "'" +
                    ",\r\n    \"UserId\" : " + "'" + Id + "'" + "\r\n}";

                PostAPI apir = new PostAPI();
                string result = apir.get_string("insertZone", JsonInput, "post");

                return result;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "alert('" + ex.Message + "');", true);
                return ex.Message;
            }
        }
        protected void DownloadButton_Click(object sender, EventArgs e)
        {
            LinkButton clickedButton = sender as LinkButton;
            if (clickedButton != null)
            {
                int rowIndex = Convert.ToInt32(clickedButton.CommandArgument);
                GridViewRow row = ZoneGridView.Rows[rowIndex];

                string websiteName = row.Cells[1].Text;
                string location = "~/media/";
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