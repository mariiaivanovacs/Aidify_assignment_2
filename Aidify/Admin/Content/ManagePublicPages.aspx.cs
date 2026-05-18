using System;
using Aidify;

namespace Aidify.Admin.Content
{
    // Manage Public Pages — visibility toggles are UI-only.
    // Extend with a PublicPages DB table in a future sprint if persistence is needed.
    public partial class ManagePublicPages : BaseRolePage
    {
        protected override string RequiredRole => Constants.RoleAdmin;

        protected void Page_Load(object sender, EventArgs e) { }
    }
}
