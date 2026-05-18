using System;
using System.Collections.Generic;
using Aidify;

namespace Aidify.Admin.Users
{
    public partial class List : BaseRolePage
    {
        protected override string RequiredRole => Constants.RoleAdmin;

        private readonly AdminRepository _repo = new AdminRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadStats();
                BindUsers();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindUsers();
        }

        private void LoadStats()
        {
            try
            {
                var stats = _repo.GetPlatformStats();
                litTotalUsers.Text       = stats.TotalUsers.ToString("N0");
                litActiveLearners.Text   = stats.ActiveLearners.ToString("N0");
                litPendingApprovals.Text = stats.PendingModules.ToString("N0");
            }
            catch
            {
                litTotalUsers.Text = litActiveLearners.Text = litPendingApprovals.Text = "—";
            }
        }

        private void BindUsers()
        {
            try
            {
                var users = _repo.GetAllUsers(
                    txtSearch.Text.Trim(),
                    ddlRoleFilter.SelectedValue);

                rptUsers.DataSource = users.Count > 0 ? users : null;
                rptUsers.DataBind();

                var empty = new[] { new { } };
                rptEmpty.DataSource = users.Count == 0 ? empty : null;
                rptEmpty.DataBind();
            }
            catch
            {
                rptUsers.DataSource = null;
                rptUsers.DataBind();
            }
        }
    }
}
