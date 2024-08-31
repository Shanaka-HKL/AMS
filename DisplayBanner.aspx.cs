using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AMS
{
    public partial class DisplayBanner : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string zoneId = ""; string BannerId = ""; string CampaignId = ""; string WebsiteId = ""; string Target = ""; string BannerLink = "";
            string BannerTypeId = ""; string BannerSizeId = ""; string FileName = ""; string TargetFrame = ""; string Priority = "";
            try
            {
                zoneId = Request.QueryString["zoneId"];
                if (zoneId != null)
                {
                    DataTable dt = new DataTable();
                    Serve apir = new Serve();
                    dt = apir.getDetailsByZoneId("getDetailsByZoneId", Convert.ToInt16(zoneId));

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            BannerId = row["Id"].ToString(); Target = row["Target"].ToString(); FileName = row["FileName"].ToString();
                            BannerTypeId = row["BannerTypeId"].ToString();
                            CampaignId = row["CampaignId"].ToString(); WebsiteId = row["WebsiteId"].ToString();
                            BannerLink = row["BannerLink"].ToString(); BannerSizeId = row["BannerSizeId"].ToString();
                            TargetFrame = row["TargetFrame"].ToString(); Priority = row["Priority"].ToString();
                        }
                        Response.Write($@"
        <html>
        <body style='margin:0;padding:0;'>
            <a href='HitAd.aspx?bannerId={BannerId}' target='{Target}'>
                <img src='{"https://advertisementmanagementsystem.azurewebsites.net/Uploads/" + FileName}' alt='{BannerTypeId}' style='width:100%; height:100%;' />
            </a>
        </body>
        </html>");
                    }
                    else
                    {
                        //Default ad
                    }
                }
                else
                {
                    //Default ad
                }
            }
            catch
            {
                //Default ad
            }
        }
    }
}