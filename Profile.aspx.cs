using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;

namespace AMS
{
    public partial class _Profile : Page
    {
         String Email = ""; String DName = ""; String AId = ""; String Address = ""; String Type = "";
        String Phone = ""; String Pic = ""; String Description = "";
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
                    Email = Kripta.Decrypt(userin4ck["email"].Trim(), "PPA4XCyfPMBrVASxNr/8A").ToString().Trim();
                    profileEmail.Text = Email;
                    DName = Kripta.Decrypt(userin4ck["dname"].Trim(), "PPA4XCyfPMBrVASxNr/8A").ToString().Trim();
                    profileName.Text = DName;
                    AId = Kripta.Decrypt(userin4ck["aId"].Trim(), "PPA4XCyfPMBrVASxNr/8A").ToString().Trim();
                    profileSupport.Text = AId;
                    Address = Kripta.Decrypt(userin4ck["addr"].Trim(), "PPA4XCyfPMBrVASxNr/8A").ToString().Trim();
                    profileAddress.Text = Address;
                    Type = Kripta.Decrypt(userin4ck["type"].Trim(), "PPA4XCyfPMBrVASxNr/8A").ToString().Trim();
                    Phone = Kripta.Decrypt(userin4ck["phone"].Trim(), "PPA4XCyfPMBrVASxNr/8A").ToString().Trim();
                    profilePhone.Text = Phone;
                    Pic = userin4ck["pic"].Trim();
                    if (Pic == "")
                    {
                        profileImageDisplay.ImageUrl = "~/Images/Default-profile.png";
                    }
                    else
                    {
                        profileImageDisplay.ImageUrl = "data:image/png;base64," + Pic.Trim();
                    }
                    Description = Kripta.Decrypt(userin4ck["description"].Trim(), "PPA4XCyfPMBrVASxNr/8A").ToString().Trim();
                    profileDescription.Text = Description;
                }
            }
        }

        protected void UpdateProfileButton_Click(object sender, EventArgs e)
        {
            ErrTB.ForeColor = Color.Red;

            if ((profilePassword.Text.Trim().Length >= 1) && (newPassword.Text.Trim() == "" || profileRePassword.Text.Trim() == ""))
            {
                ErrTB.Text = "Passwords do not match!";
            }
            else if (newPassword.Text.Trim().Length <= 7)
            {
                ErrTB.Text = "Minimum length of the Password is 8 characters!";
            }
            else if (newPassword.Text.Trim() != profileRePassword.Text.Trim())
            {
                ErrTB.Text = "Passwords do not match!";
            }
            else if (Regex.IsMatch(newPassword.Text.Trim(), "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?^[a-zA-Z0-9_@.-]).{8,}$") == false)
            {
                ErrTB.Text = "Uppser letter, Number, Special character and Lower letter combination is required!";
            }
            else
            {
                string base64Stringa = "";

                if (profileImage.HasFile)
                {
                    Stream fs = profileImage.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    base64Stringa = Convert.ToBase64String(bytes, 0, bytes.Length);
                }

                ErrTB.ForeColor = Color.Green;
                ErrTB.Text = "Profile has been updated successfully!";

                profilePassword.Text = "";
                newPassword.Text = "";
                profileRePassword.Text = "";
            }
        }

        protected void DeleteAccountButton_Click(object sender, EventArgs e)
        {
            try
            {
                string JsonInput = "{\r\n   \"Email\" : " + "'" + Email + "'" + "\r\n  \"Status\" : " + "'" + 0 + "'" + "\r\n  \"UserId\" : " + "'" + Idn.Value + "'" + "\r\n}";

                PostAPI apir = new PostAPI();

                string result = apir.get_string("deleteUserById", JsonInput, "POST");

                if (result.Contains(" successful"))
                {
                    ErrTB.ForeColor = Color.Green;
                    ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "alert('" + result + "');", true);
                    ErrTB.Text = result;
                }
                else
                {
                    ErrTB.ForeColor = Color.Red;
                    ErrTB.Text = result;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "alert('" + ex.Message + "');", true);
            }
        }
    }
}