using System;
using Aidify;

namespace Aidify.Public
{
    public partial class Contact : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) { }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            string name    = Server.HtmlEncode(txtName.Text.Trim());
            string from    = txtEmail.Text.Trim();
            string subject = txtSubject.Text.Trim();
            string message = Server.HtmlEncode(txtMessage.Text.Trim());

            try
            {
                EmailService.Send(
                    System.Configuration.ConfigurationManager.AppSettings["SmtpUser"],
                    "Contact Form: " + subject,
                    $"<p><strong>From:</strong> {name} &lt;{from}&gt;</p>" +
                    $"<p><strong>Message:</strong><br/>{message.Replace("\n", "<br/>")}</p>");

                lblConfirm.Text     = "Your message has been sent. We'll get back to you shortly.";
                lblConfirm.CssClass = "d-block mt-3 text-success fw-semibold";
                lblConfirm.Visible  = true;

                txtName.Text = txtEmail.Text = txtSubject.Text = txtMessage.Text = string.Empty;
            }
            catch
            {
                lblConfirm.Text     = "Message could not be sent. Please try again later.";
                lblConfirm.CssClass = "d-block mt-3 text-danger fw-semibold";
                lblConfirm.Visible  = true;
            }
        }
    }
}
