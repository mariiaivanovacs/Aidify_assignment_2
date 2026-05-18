using System.Data.SqlClient;

namespace Aidify
{
    // STUB — full implementation is Mariia's task in Phase B1.
    // Other developers can call this now; it is a safe no-op until implemented.
    public static class AuditService
    {
        public static void Log(int userId, string action, string targetEntity = null,
                               int? targetId = null,
                               SqlConnection conn = null, SqlTransaction tx = null)
        {
            // TODO (Mariia): replace this stub with the real INSERT into AuditLogs
        }
    }
}
