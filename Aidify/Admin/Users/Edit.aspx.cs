using System;
using Aidify;

namespace Aidify.Admin.Users
{
    public partial class Edit : BaseRolePage
    {
        protected override string RequiredRole => Constants.RoleAdmin;

        private readonly AdminRepository _repo = new AdminRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadUser();
        }

        private void LoadUser()
        {
            string raw = Request.QueryString["userId"];
            if (string.IsNullOrEmpty(raw)) return;

            int userId;
            if (!int.TryParse(raw, out userId)) return;

            var user = _repo.GetUserById(userId);
            if (user == null) return;

            hfUserId.Value   = userId.ToString();
            txtFirstName.Text = user.FullName.Contains(" ")
                ? user.FullName.Substring(0, user.FullName.IndexOf(' '))
                : user.FullName;
            txtLastName.Text  = user.FullName.Contains(" ")
                ? user.FullName.Substring(user.FullName.IndexOf(' ') + 1)
                : "";
            txtEmail.Text     = user.Email;
            ddlRole.SelectedValue = user.RoleName;

            btnDisable.Text     = user.IsActive ? "Disable User" : "Enable User";
            btnDisable.CssClass = user.IsActive
                ? "btn btn-outline-danger px-4"
                : "btn btn-outline-success px-4";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            int userId;
            if (!int.TryParse(hfUserId.Value, out userId)) return;

            string fullName = (txtFirstName.Text.Trim() + " " + txtLastName.Text.Trim()).Trim();

            try
            {
                using (var conn = DbHelper.GetConnection())
                {
                    conn.Open();
                    using (var tx = conn.BeginTransaction())
                    {
                        _repo.UpdateUser(userId, fullName, txtEmail.Text.Trim(),
                                         ddlRole.SelectedValue, true, conn, tx);
                        AuditService.Log(AuthHelper.GetUserId(), "UpdateUser", "Users", userId, conn, tx);
                        tx.Commit();
                    }
                }
                lblStatus.Text      = "User updated successfully.";
                lblStatus.CssClass  = "d-block mb-3 text-success fw-semibold";
                lblStatus.Visible   = true;
            }
            catch
            {
                lblStatus.Text     = "An error occurred. Please try again.";
                lblStatus.CssClass = "d-block mb-3 text-danger fw-semibold";
                lblStatus.Visible  = true;
            }
        }

        protected void btnDisable_Click(object sender, EventArgs e)
        {
            int userId;
            if (!int.TryParse(hfUserId.Value, out userId)) return;

            var user = _repo.GetUserById(userId);
            if (user == null) return;

            _repo.SetUserActive(userId, !user.IsActive);

            lblStatus.Text     = user.IsActive ? "User has been disabled." : "User has been re-enabled.";
            lblStatus.CssClass = "d-block mb-3 text-warning fw-semibold";
            lblStatus.Visible  = true;

            LoadUser();
        }
    }
}
