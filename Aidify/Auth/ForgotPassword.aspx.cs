// REQUIRES: BCrypt.Net-Next NuGet package (install via VS NuGet Manager)
using System;
using System.Configuration;
using Aidify;

namespace Aidify.Auth
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            string email = txtEmail.Text.Trim().ToLower();
            var userRepo = new UserRepository();
            var user     = userRepo.GetByEmail(email);

            // Always show the same message to prevent email enumeration
            lblMessage.CssClass = "auth-success d-block";
            lblMessage.Text     = "If an account exists for that email, a reset link has been sent.";
            lblMessage.Visible  = true;

            if (user == null) return;

            try
            {
                var authService = new AuthService();
                string token    = authService.CreateEmailToken(user.UserId, "Reset", expiryHours: 1);
                string siteUrl  = ConfigurationManager.AppSettings["SiteUrl"] ?? Request.Url.GetLeftPart(System.UriPartial.Authority);
                string link     = siteUrl + ResolveUrl("~/Auth/ResetPassword.aspx") + "?t=" + token;

                EmailService.Send(
                    email,
                    "Reset your Aidify password",
                    $"<p>Hi {Server.HtmlEncode(user.FullName)},</p>" +
                    $"<p>Click the link below to reset your password. The link expires in 1 hour.</p>" +
                    $"<p><a href='{link}'>Reset my password</a></p>" +
                    "<p>If you did not request this, please ignore this email.</p>");
            }
            catch
            {
                // Email send failure should not reveal to the user that the address exists
            }
        }
    }
}
