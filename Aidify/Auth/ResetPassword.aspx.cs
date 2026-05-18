// REQUIRES: BCrypt.Net-Next NuGet package (install via VS NuGet Manager)
using System;
using Aidify;

namespace Aidify.Auth
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            string token = Request.QueryString["t"];
            if (string.IsNullOrEmpty(token))
            {
                lblMessage.Text     = "Invalid or missing reset token.";
                lblMessage.Visible  = true;
                // Hide the form controls so the user can't submit
                txtNewPassword.Visible     = false;
                txtConfirmPassword.Visible = false;
                btnReset.Visible           = false;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            string token    = Request.QueryString["t"];
            string password = txtNewPassword.Text;

            var authService = new AuthService();
            bool ok = authService.ResetPassword(token, password);

            if (ok)
            {
                lblMessage.Text     = "Password reset successfully. You can now log in.";
                lblMessage.CssClass = "auth-success";
                lblMessage.Visible  = true;
                txtNewPassword.Visible     = false;
                txtConfirmPassword.Visible = false;
                btnReset.Visible           = false;
            }
            else
            {
                lblMessage.Text    = "This reset link is invalid or has expired. Please request a new one.";
                lblMessage.Visible = true;
            }
        }
    }
}
