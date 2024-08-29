using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AMS
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void RegBTN_Click(object sender, EventArgs e)
        {
            ErrTB.ForeColor = Color.Red;

            if (NameTB.Text.Trim() == "")
            {
                ErrTB.Text = "Enter your Display name!";
            }
            else if (SupportDDL.SelectedValue.ToString() == "0")
            {
                ErrTB.Text = "Enter Support Agency No!";
            }
            else if (EmailTB.Text.Trim() == "")
            {
                ErrTB.Text = "Enter your Email!";
            }
            else if (PhoneTB.Text.Trim() == "")
            {
                ErrTB.Text = "Enter your Phone!";
            }
            else if (AddressTB.Text.Trim() == "")
            {
                ErrTB.Text = "Enter Address!";
            }
            else if (PasswordTB.Text.Trim() == "")
            {
                ErrTB.Text = "Enter your Password!";
            }
            else if (RePasswordTB.Text.Trim() == "")
            {
                ErrTB.Text = "Re-type your Password!";
            }
            else if (RePasswordTB.Text.Trim() != PasswordTB.Text.Trim())
            {
                ErrTB.Text = "Passwords do not match!";
            }
            else if (PasswordTB.Text.Trim().Length <= 7)
            {
                ErrTB.Text = "Minimum length of the Password is 8 characters!";
            }
            else if (Regex.IsMatch(PasswordTB.Text.Trim(), "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?^[a-zA-Z0-9_@.-]).{8,}$") == false)
            {
                ErrTB.Text = "Uppser letter, Number, Special character and Lower letter combination is required!";
            }
            else if (TermsCheckBox.Checked != true)
            {
                ErrTB.Text = "Please confirm that you have read and agreed to the AMS Terms and Conditions!";
            }
            else
            {
                string reslt = "";
                reslt = Insertuser(EmailTB.Text.Trim(), PasswordTB.Text.Trim(), NameTB.Text.Trim(), SupportDDL.SelectedValue.ToString(), PhoneTB.Text.Trim(), AddressTB.Text.Trim());
                if (reslt.Contains(" successful"))
                {
                    ErrTB.ForeColor = Color.Green;
                    ScriptManager.RegisterStartupScript(this, GetType(), "Alert", "alert('" + reslt + "');", true);
                    ErrTB.Text = "Check your email inbox and please click on the link to activate your account.";
                }
                else
                {
                    ErrTB.ForeColor = Color.Red;
                    ErrTB.Text = reslt;
                }
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
        public string Insertuser(string EmailTB_, string PasswordTB_, string DName, string AId, string Phone, string Address)
        {
            try
            {
                string key = GenerateRandomKey(64);

                if (key.Length > 50)
                {
                    key = key.Substring(0, 50);
                }

                string pass = Kripta.Encrypt(PasswordTB_, "PPA4XCyfPMBrVASxNr/8A" + EmailTB_.ToLower().Trim()).ToString().Trim();
                string JsonInput = "{\r\n    \"Email\" : " + "'" + EmailTB_.ToLower().Trim() + "'" +
                    ",\r\n    \"Password\" : " + "'" + pass + "'" +
                    ",\r\n    \"DName\" : " + "'" + DName + "'" +
                    ",\r\n    \"AId\" : " + "'" + AId + "'" +
                    ",\r\n    \"Phone\" : " + "'" + Phone + "'" +
                    ",\r\n    \"KeyPara\" : " + "'" + key + "'" +
                    ",\r\n    \"Address\" : " + "'" + Address + "'" + "\r\n}";

                PostAPI apir = new PostAPI();
                string result = apir.get_string("insertUser", JsonInput, "post");

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