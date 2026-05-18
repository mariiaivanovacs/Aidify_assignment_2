using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Aidify
{
    // CSV export uses only built-in .NET StringBuilder — no NuGet required.
    // PDF export requires Rotativa NuGet (install via VS NuGet Manager).
    public static class ReportService
    {
        // Download a CSV of all users directly to the HTTP response
        public static void DownloadUsersCsv(HttpResponse response, List<UserListDto> users)
        {
            response.Clear();
            response.ContentType = "text/csv";
            response.AddHeader("Content-Disposition", "attachment; filename=users_" +
                DateTime.UtcNow.ToString("yyyyMMdd") + ".csv");

            var sb = new StringBuilder();
            sb.AppendLine("UserId,FullName,Email,Role,Status");

            foreach (var u in users)
            {
                sb.AppendLine(string.Join(",",
                    u.UserId,
                    CsvEscape(u.FullName),
                    CsvEscape(u.Email),
                    CsvEscape(u.RoleName),
                    CsvEscape(u.StatusLabel)));
            }

            response.Write(sb.ToString());
            response.End();
        }

        // Download a CSV of audit logs directly to the HTTP response
        public static void DownloadAuditLogsCsv(HttpResponse response, List<AuditLogDto> logs)
        {
            response.Clear();
            response.ContentType = "text/csv";
            response.AddHeader("Content-Disposition", "attachment; filename=auditlogs_" +
                DateTime.UtcNow.ToString("yyyyMMdd") + ".csv");

            var sb = new StringBuilder();
            sb.AppendLine("AuditId,Timestamp,Actor,Action,TargetEntity,IPAddress");

            foreach (var l in logs)
            {
                sb.AppendLine(string.Join(",",
                    l.AuditId,
                    l.Timestamp.ToString("yyyy-MM-dd HH:mm:ss"),
                    CsvEscape(l.ActorName),
                    CsvEscape(l.Action),
                    CsvEscape(l.TargetEntity),
                    CsvEscape(l.IPAddress)));
            }

            response.Write(sb.ToString());
            response.End();
        }

        // REQUIRES: Rotativa NuGet package (install via VS NuGet Manager)
        // Uncomment and use once Rotativa is installed:
        //
        // public static ActionResult ExportAnalyticsPdf(Controller controller)
        // {
        //     return new ViewAsPdf("Analytics") { FileName = "analytics.pdf" };
        // }

        private static string CsvEscape(string value)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;
            if (value.Contains(",") || value.Contains("\"") || value.Contains("\n"))
                return "\"" + value.Replace("\"", "\"\"") + "\"";
            return value;
        }
    }
}
