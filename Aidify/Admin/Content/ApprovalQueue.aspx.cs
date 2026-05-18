using System;
using System.Web.UI.WebControls;
using Aidify;

namespace Aidify.Admin.Content
{
    public partial class ApprovalQueue : BaseRolePage
    {
        protected override string RequiredRole => Constants.RoleAdmin;

        private readonly AdminRepository _repo = new AdminRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindQueue();
        }

        protected void rptPending_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int moduleId;
            if (!int.TryParse(e.CommandArgument.ToString(), out moduleId)) return;

            int adminId = AuthHelper.GetUserId();

            try
            {
                if (e.CommandName == "Approve")
                {
                    _repo.ApproveModule(moduleId, adminId);
                    lblQueueStatus.Text     = "Module approved and published.";
                    lblQueueStatus.CssClass = "d-block mb-3 text-success fw-semibold";
                }
                else if (e.CommandName == "Reject")
                {
                    _repo.RejectModule(moduleId, adminId);
                    lblQueueStatus.Text     = "Module returned to Draft status.";
                    lblQueueStatus.CssClass = "d-block mb-3 text-warning fw-semibold";
                }
                lblQueueStatus.Visible = true;
                BindQueue();
            }
            catch
            {
                lblQueueStatus.Text     = "An error occurred. Please try again.";
                lblQueueStatus.CssClass = "d-block mb-3 text-danger fw-semibold";
                lblQueueStatus.Visible  = true;
            }
        }

        private void BindQueue()
        {
            try
            {
                rptPending.DataSource = _repo.GetPendingModules();
                rptPending.DataBind();
            }
            catch
            {
                rptPending.DataSource = null;
                rptPending.DataBind();
            }
        }
    }
}
