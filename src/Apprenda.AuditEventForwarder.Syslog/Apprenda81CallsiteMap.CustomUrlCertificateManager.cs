namespace Apprenda.AuditEventForwarder.Syslog
{
    public partial class Apprenda81CallsiteMap
    {
        public void ConfigureCustomUrlCertificateManager()
        {
            AddMap("Updating Vanity Url Certificate", DefaultActionResultMapper);
            AddMap("Removing Vanity Url Certificate", DefaultActionResultMapper);
        }
    }
}