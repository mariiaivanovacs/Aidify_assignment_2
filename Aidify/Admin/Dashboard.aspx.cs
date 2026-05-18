using System;
using Aidify;

namespace Aidify.Admin
{
    public partial class Dashboard : BaseRolePage
    {
        protected override string RequiredRole => Constants.RoleAdmin;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            LoadStats();
        }

        private void LoadStats()
        {
            try
            {
                var stats = new AdminRepository().GetPlatformStats();
                litTotalUsers.Text    = stats.TotalUsers.ToString("N0");
                litPendingModules.Text = stats.PendingModules > 0
                    ? stats.PendingModules + " Pending"
                    : "None";
            }
            catch
            {
                litTotalUsers.Text     = "—";
                litPendingModules.Text = "—";
            }
        }
    }
}
