// REQUIRES: BCrypt.Net-Next NuGet package (install via VS NuGet Manager)
using System;
using System.Web.Security;
using Aidify;

namespace Aidify.Auth
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && AuthHelper.IsLoggedIn())
                RedirectByRole(AuthHelper.GetRole());
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            string email = txtEmail.Text.Trim().ToLower();
            string ip    = Request.UserHostAddress;

            var authService = new AuthService();

            if (authService.IsAccountLocked(email))
            {
                lblError.Text    = "Account is temporarily locked after too many failed attempts. Please try again in 15 minutes.";
                lblError.Visible = true;
                return;
            }

            var user = authService.Authenticate(email, txtPassword.Text, ip);

            if (user == null)
            {
                lblError.Text    = "Invalid email or password.";
                lblError.Visible = true;
                return;
            }

            if (!user.IsEmailConfirmed)
            {
                lblError.Text    = "Please confirm your email address before logging in. Check your inbox.";
                lblError.Visible = true;
                return;
            }

            Session[Constants.SessionUserId] = user.UserId;
            Session[Constants.SessionRole]   = user.RoleName;
            Session[Constants.SessionName]   = user.FullName;

            if (chkRememberMe.Checked)
                FormsAuthentication.SetAuthCookie(user.UserId.ToString(), true);

            RedirectByRole(user.RoleName);
        }

        private void RedirectByRole(string role)
        {
            switch (role)
            {
                case Constants.RoleAdmin:
                    Response.Redirect("~/Admin/Dashboard.aspx", false);
                    break;
                case Constants.RoleInstructor:
                    Response.Redirect("~/Instructor/Dashboard.aspx", false);
                    break;
                default:
                    Response.Redirect("~/Learner/Dashboard.aspx", false);
                    break;
            }
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}
