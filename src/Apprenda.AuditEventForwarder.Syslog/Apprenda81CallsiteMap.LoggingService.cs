namespace Apprenda.AuditEventForwarder.Syslog
{
    public partial class Apprenda81CallsiteMap
    {
        private void ConfgureLoggingService()
        {
            AddDefaultMap("Set Global Log Level");
            AddDefaultMap("Global Log Email Recipient Addition");
            AddDefaultMap("Global Log Email Recipient Removal");
            AddDefaultMap("Log Override Added");
            AddDefaultMap("Log Override Updated");
            AddDefaultMap("Log Override Version Migrated");
            AddDefaultMap("Log Override Removed");
            AddDefaultMap("Log Filter Addition");
        }
    }
}