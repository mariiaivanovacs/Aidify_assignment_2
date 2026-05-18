// REQUIRES: BCrypt.Net-Next NuGet package (install via VS NuGet Manager)
using System;
using System.Configuration;
using Aidify;

namespace Aidify.Auth
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            string fullName = txtFullName.Text.Trim();
            string email    = txtEmail.Text.Trim().ToLower();
            string password = txtPassword.Text;

            var authService = new AuthService();

            try
            {
                int userId = authService.RegisterUser(fullName, email, password);

                string token  = authService.CreateEmailToken(userId, "Confirm", expiryHours: 24);
                string siteUrl = ConfigurationManager.AppSettings["SiteUrl"] ?? Request.Url.GetLeftPart(System.UriPartial.Authority);
                string link   = siteUrl + ResolveUrl("~/Auth/ConfirmEmail.aspx") + "?t=" + token;

                EmailService.Send(
                    email,
                    "Confirm your Aidify account",
                    $"<p>Hi {Server.HtmlEncode(fullName)},</p>" +
                    $"<p>Click the link below to confirm your email address. The link expires in 24 hours.</p>" +
                    $"<p><a href='{link}'>Confirm my account</a></p>" +
                    "<p>If you did not register, please ignore this email.</p>");

                pnlSuccess.Visible = true;
                lblError.Visible   = false;
            }
            catch (InvalidOperationException ex)
            {
                lblError.Text    = ex.Message;
                lblError.Visible = true;
            }
            catch
            {
                lblError.Text    = "An unexpected error occurred. Please try again.";
                lblError.Visible = true;
            }
        }
    }
}
