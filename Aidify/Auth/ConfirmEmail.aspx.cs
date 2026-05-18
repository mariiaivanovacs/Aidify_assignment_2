using System;
using Aidify;

namespace Aidify.Auth
{
    public partial class ConfirmEmail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            string token = Request.QueryString["t"];

            if (string.IsNullOrEmpty(token))
            {
                lblMessage.Text      = "Invalid confirmation link.";
                lblMessage.CssClass  = "auth-error d-block";
                return;
            }

            var authService = new AuthService();
            bool ok = authService.ConfirmEmail(token);

            if (ok)
            {
                lblMessage.Text     = "Your email has been confirmed. You can now log in.";
                lblMessage.CssClass = "auth-success d-block";
            }
            else
            {
                lblMessage.Text     = "This confirmation link is invalid or has already been used. Please register again or contact support.";
                lblMessage.CssClass = "auth-error d-block";
            }
        }
    }
}
