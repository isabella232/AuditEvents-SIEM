namespace Apprenda.AuditEventForwarder.Syslog
{
    public partial class Apprenda81CallsiteMap
    {
        public void ConfigureUserManager()
        {
            AddMap("User Removal", DefaultActionResultMapper);
        }
    }
}