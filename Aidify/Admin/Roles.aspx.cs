using System;
using Aidify;

namespace Aidify.Admin
{
    // Roles page is read-only — the permission matrix is a designed UI mockup.
    // Role assignment is done via Admin/Users/Edit.aspx (ddlRole).
    public partial class Roles : BaseRolePage
    {
        protected override string RequiredRole => Constants.RoleAdmin;

        protected void Page_Load(object sender, EventArgs e) { }
    }
}
