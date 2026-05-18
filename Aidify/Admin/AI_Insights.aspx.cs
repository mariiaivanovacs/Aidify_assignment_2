using System;
using Aidify;

namespace Aidify.Admin
{
    // Phase C — AI Insights requires GeminiClient.cs + AIInsightsService.cs.
    // Current UI shows static insight cards as a design preview.
    // Wire litAIStatus and the chat widget once Phase C is implemented.
    public partial class AI_Insights : BaseRolePage
    {
        protected override string RequiredRole => Constants.RoleAdmin;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                litAIStatus.Text = "AI features will be available after Phase C (GeminiClient) is implemented.";
        }

        protected void btnAskAI_Click(object sender, EventArgs e)
        {
            litAIResponse.Text =
                "<em class='text-muted'>AI integration is not yet configured. " +
                "Implement GeminiClient.cs and AIInsightsService.cs to enable this feature.</em>";
            litAIResponse.Visible = true;
        }
    }
}
