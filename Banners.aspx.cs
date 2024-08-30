using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

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
                string JsonInput = "{\r\n    \"UserId\" : " + "'" + Idn.Value + "'" + "\r\n}";

                DataTable dta = new DataTable();

                PostAPI apir = new PostAPI();

                dta = apir.get_datatable("getBannerListById", JsonInput, "POST");

                if (dta.Rows.Count > 0)
                {
                    ViewState["BannerTable"] = dta;

                    BannerGridView.DataSource = dta;
                    BannerGridView.DataBind();
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
                string JsonInput = "{\r\n    \"UserId\" : " + "'" + Idn.Value + "'" + "\r\n}";

                DataTable dta = new DataTable();

                PostAPI apir = new PostAPI();

                dta = apir.get_datatable("getCampaignById", JsonInput, "POST");

                if (dta.Rows.Count > 0)
                {
                    CampaignDDL.DataValueField = "Id";
                    CampaignDDL.DataTextField = "Name";
                    CampaignDDL.DataSource = dta;
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
                // Create a JSON object using a C# dictionary
                var jsonObject = new
                {
                    BannerId = websiteID,
                    Status = status,
                    UserId = Idn.Value
                };

                // Serialize the object to a JSON string
                string JsonInput = JsonConvert.SerializeObject(jsonObject);

                PostAPI apir = new PostAPI();

                string result = apir.get_string("updateBannerById", JsonInput, "POST");

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
            //else if (!fileBannerUpload.HasFile)
            //{
            //    ErrLbl.Text = "Upload the Media!";
            //}
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
                bool proceed;
                string fileExtension = System.IO.Path.GetExtension(fileBannerUpload.FileName).ToLower();
                string[] allowedExtensions = { ".html", ".htm", ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".webp", ".txt", ".mp4", ".avi", ".mkv", ".mov", ".wmv" };

                //if (Array.Exists(allowedExtensions, ext => ext == fileExtension))
                {
                    //if (fileBannerUpload.PostedFile.ContentLength <= 5242880) // 5MB in bytes
                    //{
                    //    string folderPath = Server.MapPath("~/Uploads/" + WebsiteDDL.SelectedValue.ToString() + "_" + ZonesDDL.SelectedValue.ToString() + "/");

                    //    if (!System.IO.Directory.Exists(folderPath))
                    //    {
                    //        System.IO.Directory.CreateDirectory(folderPath);
                    //    }
                    //    string savePath = folderPath + fileBannerUpload.FileName;

                    //    fileBannerUpload.SaveAs(savePath);
                    //    ErrLbl.Text = "File uploaded successfully!";
                    proceed = true;
                    //}
                    //else
                    //{
                    //    ErrLbl.Text = "File size exceeds the 5MB limit.";
                    //    proceed = false;
                    //}
                }
                //else
                //{
                //    ErrLbl.Text = "Invalid file type. Only HTML, Image, Text, and Video files are allowed.";
                //    proceed = false;
                //}

                if (proceed == true)
                {
                    PostAPI apir = new PostAPI();
                    string reslt = "";

                    reslt = InsertRecord(CampaignDDL.SelectedValue.ToString().Trim(), ZonesDDL.SelectedValue.ToString().Trim(), ddlBannerType.SelectedValue.ToString().Trim(),
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

        public string InsertRecord(string CampaignDDLVlu, string ZonesDDLVlu, string ddlBannerTypeVlu, string ddlTargetVlu, string txtBannerLinkVlu, string txtBannerNameVlu)
        {
            try
            {
                // Create a JSON object with the required fields
                var jsonObject = new
                {
                    CampaignId = CampaignDDLVlu,
                    ZoneId = ZonesDDLVlu,
                    BannerTypeId = ddlBannerTypeVlu,
                    Target = ddlTargetVlu,
                    BannerLink = txtBannerLinkVlu,
                    Name = txtBannerNameVlu,
                    UserId = Idn.Value
                };

                // Serialize the object to a JSON string
                string JsonInput = JsonConvert.SerializeObject(jsonObject);

                // Create instance of PostAPI and retrieve data
                PostAPI apir = new PostAPI();
                string result = apir.get_string("insertBanner", JsonInput, "POST");

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

        protected void WebsiteDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string JsonInput = "{\r\n    \"WebsiteId\" : " + "'" + WebsiteDDL.SelectedValue.ToString() + "'" + "\r\n}";

                DataTable dta = new DataTable();

                PostAPI apir = new PostAPI();

                dta = apir.get_datatable("getZonesById", JsonInput, "POST");

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

        protected void CampaignDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string JsonInput = "{\r\n    \"CampaignId\" : " + "'" + CampaignDDL.SelectedValue.ToString() + "'" + "\r\n}";

                DataTable dta = new DataTable();
                DataTable dtb = new DataTable();

                PostAPI apir = new PostAPI();

                dta = apir.get_datatable("getWebsiteByCampaignId", JsonInput, "POST");

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