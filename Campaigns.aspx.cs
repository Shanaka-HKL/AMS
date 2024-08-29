using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;

namespace AMS
{
    public partial class _Campaigns : Page
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

                    BindCampaignGridView();
                }
            }
        }

        protected void CampaignGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CampaignGridView.PageIndex = e.NewPageIndex;

            DataTable dt = ViewState["CampaignTable"] as DataTable;

            if (dt != null)
            {
                CampaignGridView.DataSource = dt;
                CampaignGridView.DataBind();
            }
        }

        private void BindCampaignGridView()
        {
            try
            {
                string JsonInput = "{\r\n    \"UserId\" : " + "'" + Id + "'" + "\r\n}";

                DataTable dta = new DataTable();

                PostAPI apir = new PostAPI();

                dta = apir.get_datatable("getCampaignListById", JsonInput, "POST");

                if (dta.Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Id", typeof(int));
                    dt.Columns.Add("CampaignName", typeof(string));
                    dt.Columns.Add("Advertiser", typeof(string));
                    dt.Columns.Add("WebsiteName", typeof(string));
                    dt.Columns.Add("CreatedBy", typeof(string));
                    dt.Columns.Add("CreatedDate", typeof(DateTime));
                    dt.Columns.Add("StartDate", typeof(DateTime));
                    dt.Columns.Add("EndDate", typeof(DateTime));
                    dt.Columns.Add("Status", typeof(string));

                    foreach (DataRow dr in dta.Rows)
                    {
                        dt.Rows.Add(dr["Id"].ToString(), dr["CampaignName"].ToString(),
                            dr["Advertiser"].ToString(), dr["WebsiteName"].ToString(),
                            dr["CreatedBy"].ToString(), dr["CreatedDate"].ToString(),
                            dr["StartDate"].ToString(), dr["EndDate"].ToString(), dr["Status"].ToString());
                    }

                    ViewState["CampaignTable"] = dt;

                    CampaignGridView.DataSource = dt;
                    CampaignGridView.DataBind();

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
                string JsonInput = "{\r\n    \"UserId\" : " + "'" + Id + "'" + "\r\n}";

                DataTable dta = new DataTable();

                PostAPI apir = new PostAPI();

                dta = apir.get_datatable("getAdvertiserById", JsonInput, "POST");

                if (dta.Rows.Count > 0)
                {
                    AdvertiserDDL.DataValueField = "Id";
                    AdvertiserDDL.DataTextField = "Name";
                    AdvertiserDDL.DataSource = dta;
                    AdvertiserDDL.DataBind();
                    AdvertiserDDL.SelectedIndex = 1;
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
                string JsonInput = "{\r\n   \"CampaignId\" : " + "'" + websiteID + "'" + "\r\n  \"Status\" : " + "'" + status + "'" + "\r\n  \"UserId\" : " + "'" + Id + "'" + "\r\n}";

                PostAPI apir = new PostAPI();

                string result = apir.get_string("updateCampaignById", JsonInput, "POST");

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

        protected void CreateCampaignButton_Click(object sender, EventArgs e)
        {
            ErrLbl.ForeColor = Color.Red;

            if (txtCampaignName.Text.Trim() == "")
            {
                ErrLbl.Text = "Enter Campaign Name!";
            }
            else if (AdvertiserDDL.SelectedValue.ToString() == "0")
            {
                ErrLbl.Text = "Select an Advertiser!";
            }
            else if (txtStartDate.Text.Trim() == "")
            {
                ErrLbl.Text = "Select the Start Date!";
            }
            else if (txtEndDate.Text.Trim() == "")
            {
                ErrLbl.Text = "Select the End Date!";
            }
            else
            {
                PostAPI apir = new PostAPI();
                string reslt = "";

                reslt = InsertRecord(txtCampaignName.Text.Trim(), txtCampaignDescription.Text.Trim(), AdvertiserDDL.SelectedValue.ToString().Trim(), WebsiteDDL.SelectedValue.ToString().Trim(),
                     txtCampaignBudget.Text.Trim(), txtStartDate.Text.Trim(), txtEndDate.Text.Trim());
                if (reslt.Contains(" successful"))
                {
                    ErrLbl.ForeColor = Color.Green;
                    ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "alert('" + reslt + "');", true);
                    ErrLbl.Text = reslt;

                    BindCampaignGridView();

                AdvertiserDDL.SelectedIndex = 0;
                WebsiteDDL.SelectedIndex = 0;
                txtCampaignName.Text = "";
                txtCampaignDescription.Text = "";
                txtCampaignBudget.Text = "";
                txtStartDate.Text = "";
                txtEndDate.Text = "";
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

            BindCampaignGridView();
        }

        protected void DeactivateButton_Click(object sender, EventArgs e)
        {
            LinkButton clickedButton = sender as LinkButton;
            if (clickedButton != null)
            {
                int websiteID = Convert.ToInt32(clickedButton.CommandArgument);
                UpdateStatus(websiteID, 0);
            }

            BindCampaignGridView();
        }
        public string InsertRecord(string txtCampaignName_, string txtCampaignDescription_, string AdvertiserDDL_, string WebsiteDDL_, string txtCampaignBudget_, string txtStartDate_, string txtEndDate_)
        {
            try
            {
                string JsonInput = "{\r\n    \"Name\" : " + "'" + txtCampaignName_ + "'" +
                    ",\r\n    \"Description\" : " + "'" + txtCampaignDescription_ + "'" +
                    ",\r\n    \"AdvertiserId\" : " + "'" + AdvertiserDDL_ + "'" +
                    ",\r\n    \"WebsiteId\" : " + "'" + WebsiteDDL_ + "'" +
                    ",\r\n    \"Budget\" : " + "'" + txtCampaignBudget_ + "'" +
                    ",\r\n    \"StartDate\" : " + "'" + txtStartDate_ + "'" +
                    ",\r\n    \"EndDate\" : " + "'" + txtEndDate_ + "'" +
                    ",\r\n    \"UserId\" : " + "'" + Id + "'" + "\r\n}";

                PostAPI apir = new PostAPI();
                string result = apir.get_string("insertCampaign", JsonInput, "post");

                return result;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "alert('" + ex.Message + "');", true);
                return ex.Message;
            }
        }
        protected void AdvertiserDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string JsonInput = "{\r\n    \"AdvertiserId\" : " + "'" + AdvertiserDDL.SelectedValue.ToString() + "'" + "\r\n}";

                DataTable dta = new DataTable();

                PostAPI apir = new PostAPI();

                dta = apir.get_datatable("getWebsiteByAdvertiserId", JsonInput, "POST");

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