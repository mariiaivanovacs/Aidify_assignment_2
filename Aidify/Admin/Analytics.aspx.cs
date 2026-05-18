using System;
using Aidify;

namespace Aidify.Admin
{
    public partial class Analytics : BaseRolePage
    {
        protected override string RequiredRole => Constants.RoleAdmin;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadStats();
        }

        private void LoadStats()
        {
            try
            {
                var stats = new AdminRepository().GetPlatformStats();
                litTotalAttempts.Text  = stats.TotalAttempts.ToString("N0");
                litActiveLearners.Text = stats.ActiveLearners.ToString("N0");
            }
            catch
            {
                litTotalAttempts.Text = litActiveLearners.Text = "—";
            }
        }
    }
}
