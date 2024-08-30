using Newtonsoft.Json;
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
                DataTable dta = new DataTable();
                Serve apir = new Serve();
                dta = apir.getCampaignListById("getCampaignListById", Convert.ToInt16(Idn.Value));

                if (dta.Rows.Count > 0)
                {
                    ViewState["CampaignTable"] = dta;

                    CampaignGridView.DataSource = dta;
                    CampaignGridView.DataBind();
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

        protected void CampaignDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CampaignDDL.SelectedIndex > 0)
            {
                CreateCampaignButton.Text = "Update Campaign";

                try
                {
                    DataTable dta = new DataTable();
                    Serve apir = new Serve();
                    dta = apir.getCampaignByCampaignId("getCampaignByCampaignId", Convert.ToInt16(Idn.Value));

                    if (dta.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dta.Rows)
                        {
                            CampaignDDL.SelectedValue = dr["Id"].ToString().Trim();
                            txtCampaignName.Text = dr["Name"].ToString().Trim();
                            txtCampaignDescription.Text = dr["Description"].ToString().Trim();
                            txtCampaignBudget.Text = dr["Budget"].ToString().Trim();
                            txtPriority.Text = dr["Priority"].ToString().Trim();
                            txtStartDate.Text = Convert.ToDateTime(dr["StartDate"].ToString().Trim()).ToString("MM/dd/yyyy");
                            txtEndDate.Text = Convert.ToDateTime(dr["EndDate"].ToString().Trim()).ToString("MM/dd/yyyy");
                        }

                        txtCampaignName.Enabled = false;
                        txtCampaignDescription.Enabled = false;
                        txtCampaignBudget.Enabled = false;
                        txtPriority.Enabled = true;
                        txtStartDate.Enabled = false;
                        txtEndDate.Enabled = false;

                        txtCampaignName.ForeColor = Color.Black;
                        txtCampaignDescription.ForeColor = Color.Black;
                        txtCampaignBudget.ForeColor = Color.Black;
                        txtPriority.ForeColor = Color.White;
                        txtStartDate.ForeColor = Color.Black;
                        txtEndDate.ForeColor = Color.Black;
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "alert('" + ex.Message + "');", true);
                }
            }
            else if (CampaignDDL.SelectedIndex == 0)
            {
                CreateCampaignButton.Text = "Create Campaign";

                txtCampaignName.Enabled = false;
                txtCampaignDescription.Enabled = false;
                txtCampaignBudget.Enabled = false;
                txtPriority.Enabled = true;
                txtStartDate.Enabled = false;
                txtEndDate.Enabled = false;

                txtCampaignName.ForeColor = Color.Black;
                txtCampaignDescription.ForeColor = Color.Black;
                txtCampaignBudget.ForeColor = Color.Black;
                txtPriority.ForeColor = Color.White;
                txtStartDate.ForeColor = Color.Black;
                txtEndDate.ForeColor = Color.Black;

                txtPriority.Text = "1";
            }
        }

        private void UpdateStatus(int websiteID, int status)
        {
            try
            {
                Serve apir = new Serve();
                string result = apir.updateCampaignById("updateCampaignById", websiteID, status, Convert.ToInt16(Idn.Value));

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

            int txtPriority_ = 0;
            try
            {
                txtPriority_ = Convert.ToInt16(txtPriority.Text);
            }
            catch
            {
                txtPriority_ = 0;
            }

            if (txtCampaignName.Text.Trim() == "")
            {
                ErrLbl.Text = "Enter Campaign Name!";
            }
            else if (txtStartDate.Text.Trim() == "")
            {
                ErrLbl.Text = "Select the Start Date!";
            }
            else if (txtEndDate.Text.Trim() == "")
            {
                ErrLbl.Text = "Select the End Date!";
            }
            else if (CampaignDDL.SelectedValue.ToString() != "0" && txtPriority_ <= 0)
            {
                ErrLbl.Text = "Enter the Priority!";
            }
            else
            {
                PostAPI apir = new PostAPI();
                string reslt = "";

                if (CampaignDDL.SelectedIndex > 0)
                {
                    reslt = UpdateRecord(Convert.ToInt16(CampaignDDL.SelectedValue.ToString().Trim()), Convert.ToInt16(txtPriority.Text.Trim()), Convert.ToInt16(Idn.Value));
                    if (reslt.Contains(" successful"))
                    {
                        ErrLbl.ForeColor = Color.Green;
                        ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "alert('" + reslt + "');", true);
                        ErrLbl.Text = reslt;

                        BindCampaignGridView();

                        txtCampaignName.Text = "";
                        txtCampaignDescription.Text = "";
                        txtCampaignBudget.Text = "";
                        txtStartDate.Text = "";
                        txtEndDate.Text = "";
                        txtPriority.Enabled = false;
                        txtPriority.Text = "1";
                        CampaignDDL.SelectedIndex = 0;
                    }
                    else
                    {
                        ErrLbl.ForeColor = Color.Red;
                        ErrLbl.Text = reslt;
                    }
                }
                else
                {
                    reslt = InsertRecord(txtCampaignName.Text.Trim(), txtPriority_, txtCampaignDescription.Text.Trim(), Convert.ToInt16(Idn.Value), txtCampaignBudget.Text.Trim(), txtStartDate.Text.Trim(), txtEndDate.Text.Trim());
                    if (reslt.Contains(" successful"))
                    {
                        ErrLbl.ForeColor = Color.Green;
                        ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "alert('" + reslt + "');", true);
                        ErrLbl.Text = reslt;

                        BindCampaignGridView();

                        txtCampaignName.Text = "";
                        txtCampaignDescription.Text = "";
                        txtCampaignBudget.Text = "";
                        txtStartDate.Text = "";
                        txtEndDate.Text = "";
                        txtPriority.Enabled = false;
                        txtPriority.Text = "1";
                        DataTable dtx = new DataTable();
                        ViewState["CampaignTable"] = dtx;
                        if (dtx.Rows.Count > 0)
                        {
                            CampaignDDL.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        ErrLbl.ForeColor = Color.Red;
                        ErrLbl.Text = reslt;
                    }
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
        public string UpdateRecord(int campid, int prio, int usr)
        {
            try
            {
                Serve apir = new Serve();
                string result = apir.updateCampaign("updateCampaign", campid, prio, usr);

                return result;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "alert('" + ex.Message + "');", true);
                return ex.Message;
            }
        }
        public string InsertRecord(string txtCampaignName_, int prio, string txtCampaignDescription_, int AdvertiserDDL_, string txtCampaignBudget_, string txtStartDate_, string txtEndDate_)
        {
            try
            {
                // Parse numeric value for budget
                decimal budget = 0;
                decimal.TryParse(txtCampaignBudget_, out budget);

                // Convert date strings to DateTime objects
                DateTime startDate;
                DateTime endDate;
                DateTime.TryParse(txtStartDate_, out startDate);
                DateTime.TryParse(txtEndDate_, out endDate);

                Serve apir = new Serve();
                string result = apir.insertCampaign("insertCampaign", txtCampaignName_, txtCampaignDescription_, prio, AdvertiserDDL_, budget, startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"), Convert.ToInt16(Idn.Value));

                return result;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "alert('" + ex.Message + "');", true);
                return ex.Message;
            }
        }
    }
}