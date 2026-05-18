using System;

namespace Aidify.Public
{
    public partial class PreviewQuiz : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) { }

        protected void btnSubmitPreviewQuiz_Click(object sender, EventArgs e)
        {
            // Preview quiz — answers are hardcoded HTML radio buttons, not server controls.
            // We just acknowledge the submission without scoring or saving to DB.
            lblQuizResult.Text =
                "<strong>Thanks for trying the preview quiz!</strong> " +
                "Register for a free account to access full quizzes with instant scoring, " +
                "progress tracking, and certificates.";
            lblQuizResult.CssClass = "d-block mt-3 p-3 rounded text-success border border-success-subtle bg-success-subtle";
            lblQuizResult.Visible  = true;
        }
    }
}
