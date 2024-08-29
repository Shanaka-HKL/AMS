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
    public partial class _Websites : Page
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
                    BindWebsiteGridView();
                }
            }
        }

        protected void WebsiteGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            WebsiteGridView.PageIndex = e.NewPageIndex;

            DataTable dt = ViewState["WebsiteTable"] as DataTable;

            if (dt != null)
            {
                WebsiteGridView.DataSource = dt;
                WebsiteGridView.DataBind();
            }
        }

        private void BindWebsiteGridView()
        {
            try
            {
                string JsonInput = "{\r\n    \"UserId\" : " + "'" + Id + "'" + "\r\n}";

                DataTable dta = new DataTable();

                PostAPI apir = new PostAPI();

                dta = apir.get_datatable("getWebsiteListById", JsonInput, "POST");

                if (dta.Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Id", typeof(int));
                    dt.Columns.Add("WebsiteName", typeof(string));
                    dt.Columns.Add("CreatedBy", typeof(string));
                    dt.Columns.Add("CreatedDate", typeof(DateTime));
                    dt.Columns.Add("UpdatedDate", typeof(DateTime));
                    dt.Columns.Add("Status", typeof(string));

                    foreach (DataRow dr in dta.Rows)
                    {
                        dt.Rows.Add(dr["Id"].ToString(), dr["WebsiteName"].ToString(),
                            dr["CreatedBy"].ToString(), dr["CreatedDate"].ToString(),
                            dr["UpdatedDate"].ToString(), dr["Status"].ToString());
                    }

                    ViewState["WebsiteTable"] = dt;

                    WebsiteGridView.DataSource = dt;
                    WebsiteGridView.DataBind();
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
                string JsonInput = "{\r\n   \"WebsiteId\" : " + "'" + websiteID + "'" + "\r\n  \"Status\" : " + "'" + status + "'" + "\r\n  \"UserId\" : " + "'" + Id + "'" + "\r\n}";

                PostAPI apir = new PostAPI();

                string result = apir.get_string("updateWebsiteById", JsonInput, "POST");

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

        protected void CreateWebsiteButton_Click(object sender, EventArgs e)
        {
            ErrLbl.ForeColor = Color.Red;

            if (NameTextBox.Text.Trim() == "")
            {
                ErrLbl.Text = "Enter Website Name!";
            }
            else if (WebsiteUrlTextBox.Text.Trim() == "")
            {
                ErrLbl.Text = "Enter Website URL!";
            }
            else if (TargetFrameDropDownList.Text.Trim() == "")
            {
                ErrLbl.Text = "Enter Target Frame!";
            }
            else
            {
                PostAPI apir = new PostAPI();
                string reslt = "";

                reslt = InsertRecord(NameTextBox.Text.Trim(), WebsiteUrlTextBox.Text.Trim(), TargetFrameDropDownList.SelectedValue.ToString().Trim());
                if (reslt.Contains(" successful"))
                {
                    ErrLbl.ForeColor = Color.Green;
                    ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "alert('" + reslt + "');", true);
                    ErrLbl.Text = reslt;

                    BindWebsiteGridView();

                    TargetFrameDropDownList.SelectedIndex = 0;
                    NameTextBox.Text = "";
                    WebsiteUrlTextBox.Text = "";
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
            
            BindWebsiteGridView();
        }

        protected void DeactivateButton_Click(object sender, EventArgs e)
        {
            LinkButton clickedButton = sender as LinkButton;
            if (clickedButton != null)
            {
                int websiteID = Convert.ToInt32(clickedButton.CommandArgument);
                UpdateStatus(websiteID, 0);
            }

            BindWebsiteGridView();
        }
        public string InsertRecord(string NameTextBox_, string WebsiteUrlTextBox_, string TargetFrameDropDownList_)
        {
            try
            {
                string JsonInput = "{\r\n    \"Name\" : " + "'" + NameTextBox_ + "'" +
                    ",\r\n    \"WebsiteUrl\" : " + "'" + WebsiteUrlTextBox_ + "'" +
                    ",\r\n    \"TargetFrame\" : " + "'" + TargetFrameDropDownList_ + "'" +
                    ",\r\n    \"UserId\" : " + "'" + Id + "'" + "\r\n}";

                PostAPI apir = new PostAPI();
                string result = apir.get_string("insertWebsite", JsonInput, "post");

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