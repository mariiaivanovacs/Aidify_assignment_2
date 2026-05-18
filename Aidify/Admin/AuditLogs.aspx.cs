using System;
using Aidify;

namespace Aidify.Admin
{
    public partial class AuditLogs : BaseRolePage
    {
        protected override string RequiredRole => Constants.RoleAdmin;

        private readonly AdminRepository _repo = new AdminRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindLogs();
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            BindLogs();
        }

        private void BindLogs()
        {
            try
            {
                int hours = int.TryParse(ddlDateRange.SelectedValue, out int h) ? h : 24;
                var logs  = _repo.GetAuditLogs(
                    txtSearch.Text.Trim(),
                    ddlActionType.SelectedValue,
                    hours);

                rptLogs.DataSource = logs;
                rptLogs.DataBind();
            }
            catch
            {
                rptLogs.DataSource = null;
                rptLogs.DataBind();
            }
        }
    }
}
