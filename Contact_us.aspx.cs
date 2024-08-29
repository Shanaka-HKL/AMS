using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading.Tasks;

namespace AMS
{
    public partial class Contactus : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SendEmailButton_Click(object sender, EventArgs e)
        {
            ErrLbl.ForeColor = Color.Red;

            if (txtName.Text.Trim() == "")
            {
                ErrLbl.Text = "Enter Your Name!";
            }
            else if (txtEmail.Text.Trim() == "")
            {
                ErrLbl.Text = "Enter Your Email!";
            }
            else if (txtSubject.Text.Trim() == "")
            {
                ErrLbl.Text = "Enter Subject!";
            }
            else if (txtMessage.Text.Trim() == "")
            {
                ErrLbl.Text = "Enter Message!";
            }
            else
            {
                _ = EmailAsync();
            }
        }
        public async Task EmailAsync()
        {
            bool x = await SendEmailAsync(txtSubject.Text.Trim(), txtMessage.Text.Trim(), txtName.Text.Trim(), txtEmail.Text.Trim());

            if (x == true)
            {
                ErrLbl.ForeColor = Color.Green;
                ErrLbl.Text = "Email sent. One of our agents will contact you soon!";

                txtName.Text = "";
                txtEmail.Text = "";
                txtSubject.Text = "";
                txtMessage.Text = "";
            }
            else
            {
                ErrLbl.ForeColor = Color.Red;
                ErrLbl.Text = "Email sending error, Please try again later!";
            }
        }
        public async Task<bool> SendEmailAsync(string subject, string message, string nme, string email_)
        {
            try
            {
                MailMessage mail = new MailMessage
                {
                    From = new MailAddress("alert@advertisementmanagementsystem.com")
                };
                mail.To.Add("shanaka@iq-global.com");
                mail.IsBodyHtml = true;
                mail.Subject = subject;

                string INNOVATION_QUOTIENT = "<a href='http://advertisementmanagementsystem.com/Terms_and_Conditions.aspx'>Terms</a>";
                mail.Body = "<div style='background-image: url(https://e0.pxfuel.com/wallpapers/277/595/desktop-wallpaper-mail-background-email-outlook.jpg); background-size: 100% 100%; color:Black; font-family:verdana;'><br /><b>&nbsp;"
                    + "Hi,<br />This is an alert email!</b><br /><br />"
                    + "&nbsp;This is from: " + nme.ToUpper() + "<br /><br />"
                    + "&nbsp;This is the sender's email: " + email_.ToLower() + "<br /><br />"
                    + "<br /><br />&nbsp;This is the original message: " + "<br /><br />&nbsp;" + message
                    + "<br /><br />&nbsp;Best Regards!<br />&nbsp;IT Team - " + INNOVATION_QUOTIENT + "<br /><br />"
                    + "&nbsp;<b>Note:</b> This is an Auto-Generated mail. Kindly do not reply to this. Please reply to info@advertisementmanagementsystem.com<br /></div>";

                using (var SmtpServer = new SmtpClient("webmail.advertisementmanagementsystem.com"))
                {
                    SmtpServer.Port = 25;
                    SmtpServer.EnableSsl = false;
                    SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                    SmtpServer.UseDefaultCredentials = true;
                    SmtpServer.Credentials = new NetworkCredential("alert@advertisementmanagementsystem.com", "^tVn62l16");
                    SmtpServer.Timeout = 30000;

                    await SmtpServer.SendMailAsync(mail);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }
    }
}