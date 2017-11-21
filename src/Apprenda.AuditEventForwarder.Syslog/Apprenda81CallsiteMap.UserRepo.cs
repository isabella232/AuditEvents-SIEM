namespace Apprenda.AuditEventForwarder.Syslog
{
    public partial class Apprenda81CallsiteMap
    {
        private void ConfigureUserRepository()
        {
            AddDefaultMap("Account Lockout Reset");
            AddDefaultMap("Account Lockout");
        }
    }
}