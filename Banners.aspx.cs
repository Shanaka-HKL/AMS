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
            int txtPriority_ = 0;
            try
            {
                txtPriority_ = Convert.ToInt16(txtPriority.Text);
            }
            catch
            {
                txtPriority_ = 0;
            }

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
            else if (CampaignDDL.SelectedValue.ToString() != "0" && txtPriority_ <= 0)
            {
                ErrLbl.Text = "Enter the Priority!";
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

                    if (BannerDDL.SelectedIndex != 0)
                    {
                        reslt = UpdateRecord(BannerDDL.SelectedValue.ToString().Trim(), txtPriority_, CampaignDDL.SelectedValue.ToString().Trim(), WebsiteDDL.SelectedValue.ToString().Trim(), ZonesDDL.SelectedValue.ToString().Trim(), ddlBannerType.SelectedValue.ToString().Trim(),
                            ddlTarget.SelectedValue.ToString().Trim(), txtBannerLink.Text.Trim(), txtBannerName.Text.Trim());
                        if (reslt.Contains(" successful"))
                        {
                            ErrLbl.ForeColor = Color.Green;
                            ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "alert('" + reslt + "');", true);
                            ErrLbl.Text = reslt;

                            BindBannerGridView();

                            CampaignDDL.SelectedIndex = 0;
                            BannerDDL.SelectedIndex = 0;
                            WebsiteDDL.SelectedIndex = 0;
                            ZonesDDL.SelectedIndex = 0;
                            ddlBannerType.SelectedIndex = 0;
                            ddlTarget.SelectedIndex = 0;
                            txtBannerLink.Text = "";
                            txtBannerName.Text = "";
                            txtPriority.Enabled = false;
                            txtPriority.Text = "1";
                        }
                        else
                        {
                            ErrLbl.ForeColor = Color.Red;
                            ErrLbl.Text = reslt;
                        }
                    }
                    else
                    {
                        reslt = InsertRecord(txtPriority_, CampaignDDL.SelectedValue.ToString().Trim(), WebsiteDDL.SelectedValue.ToString().Trim(), ZonesDDL.SelectedValue.ToString().Trim(), ddlBannerType.SelectedValue.ToString().Trim(),
                            ddlTarget.SelectedValue.ToString().Trim(), txtBannerLink.Text.Trim(), txtBannerName.Text.Trim());
                        if (reslt.Contains(" successful"))
                        {
                            ErrLbl.ForeColor = Color.Green;
                            ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "alert('" + reslt + "');", true);
                            ErrLbl.Text = reslt;

                            BindBannerGridView();

                            CampaignDDL.SelectedIndex = 0;
                            BannerDDL.SelectedIndex = 0;
                            WebsiteDDL.SelectedIndex = 0;
                            ZonesDDL.SelectedIndex = 0;
                            ddlBannerType.SelectedIndex = 0;
                            ddlTarget.SelectedIndex = 0;
                            txtBannerLink.Text = "";
                            txtBannerName.Text = "";
                            txtPriority.Enabled = false;
                            txtPriority.Text = "1";
                        }
                        else
                        {
                            ErrLbl.ForeColor = Color.Red;
                            ErrLbl.Text = reslt;
                        }
                    }
                }
            }
        }
        public string UpdateRecord(string BannerDDL_, int txtPriority_, string CampaignDDLVlu, string WebsiteDDLVlu, string ZonesDDLVlu, string ddlBannerTypeVlu, string ddlTargetVlu, string txtBannerLinkVlu, string txtBannerNameVlu)
        {
            try
            {
                // Create a JSON object with the required fields
                var jsonObject = new
                {
                    CampaignId = CampaignDDLVlu,
                    BId = BannerDDL_,
                    WebsiteId = WebsiteDDLVlu,
                    ZoneId = ZonesDDLVlu,
                    BannerTypeId = ddlBannerTypeVlu,
                    Target = ddlTargetVlu,
                    BannerLink = txtBannerLinkVlu,
                    Name = txtBannerNameVlu,
                    Priority = txtPriority_,
                    UserId = Idn.Value
                };

                // Serialize the object to a JSON string
                string JsonInput = JsonConvert.SerializeObject(jsonObject);

                // Create instance of PostAPI and retrieve data
                PostAPI apir = new PostAPI();
                string result = apir.get_string("updateBannerByBannerId", JsonInput, "POST");

                return result;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "alert('" + ex.Message + "');", true);
                return ex.Message;
            }
        }

        public string InsertRecord(int txtPriority_, string CampaignDDLVlu, string WebsiteDDLVlu, string ZonesDDLVlu, string ddlBannerTypeVlu, string ddlTargetVlu, string txtBannerLinkVlu, string txtBannerNameVlu)
        {
            try
            {
                // Create a JSON object with the required fields
                var jsonObject = new
                {
                    CampaignId = CampaignDDLVlu,
                    WebsiteId = WebsiteDDLVlu,
                    ZoneId = ZonesDDLVlu,
                    BannerTypeId = ddlBannerTypeVlu,
                    Target = ddlTargetVlu,
                    BannerLink = txtBannerLinkVlu,
                    Name = txtBannerNameVlu,
                    Priority = txtPriority_,
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

        protected void BannerDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BannerDDL.SelectedIndex != 0)
            {
                try
                {
                    string JsonInput = "{\r\n    \"BannerId\" : " + "'" + BannerDDL.SelectedValue.ToString() + "'" + "\r\n}";

                    DataTable dta = new DataTable();

                    PostAPI apir = new PostAPI();

                    dta = apir.get_datatable("getWebsiteByBannerId", JsonInput, "POST");

                    foreach (DataRow dr in dta.Rows)
                    {
                        CampaignDDL.SelectedValue = dr["CampaignId"].ToString().Trim();
                        WebsiteDDL.SelectedValue = dr["WebsiteId"].ToString().Trim();
                        ZonesDDL.SelectedValue = dr["ZoneId"].ToString().Trim();
                        txtBannerName.Text = dr["Name"].ToString().Trim();
                        txtBannerLink.Text = dr["BannerLink"].ToString().Trim();
                        ddlBannerType.SelectedValue = dr["BannerTypeId"].ToString().Trim();
                        ddlTarget.SelectedValue = dr["Target"].ToString().Trim();
                        txtPriority.Text = dr["Priority"].ToString().Trim();
                    }

                    CampaignDDL.Enabled = false;
                    WebsiteDDL.Enabled = false;
                    ZonesDDL.Enabled = false;
                    ddlBannerType.Enabled = false;
                    ddlTarget.Enabled = false;
                    txtBannerLink.Enabled = false;
                    txtBannerName.Enabled = false;
                    txtPriority.Enabled = true;

                    CampaignDDL.ForeColor = Color.Black;
                    WebsiteDDL.ForeColor = Color.Black;
                    ZonesDDL.ForeColor = Color.Black;
                    ddlBannerType.ForeColor = Color.Black;
                    ddlTarget.ForeColor = Color.Black;
                    txtBannerLink.ForeColor = Color.Black;
                    txtBannerName.ForeColor = Color.Black;
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "alert('" + ex.Message + "');", true);
                }
            }
            else
            {
                CampaignDDL.Enabled = true;
                WebsiteDDL.Enabled = true;
                ZonesDDL.Enabled = true;
                ddlBannerType.Enabled = true;
                ddlTarget.Enabled = true;
                txtBannerLink.Enabled = true;
                txtBannerName.Enabled = true;
                txtPriority.Enabled = false;
                txtPriority.Text = "1";
            }
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
                dtb = apir.get_datatable("getBannerByCampaignId", JsonInput, "POST");

                if (dta.Rows.Count > 0)
                {
                    WebsiteDDL.DataValueField = "Id";
                    WebsiteDDL.DataTextField = "Name";
                    WebsiteDDL.DataSource = dta;
                    WebsiteDDL.DataBind();
                    WebsiteDDL.SelectedIndex = 0;
                }

                if (dtb.Rows.Count > 0)
                {
                    BannerDDL.DataValueField = "Id";
                    BannerDDL.DataTextField = "Name";
                    BannerDDL.DataSource = dtb;
                    BannerDDL.DataBind();
                    BannerDDL.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "alert('" + ex.Message + "');", true);
            }
        }
    }
}