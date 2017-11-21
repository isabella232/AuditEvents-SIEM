namespace Apprenda.AuditEventForwarder.Syslog
{
    public partial class Apprenda81CallsiteMap
    {
        void ConfigureRegistryService()
        {
            AddDefaultMap("Platform Registry Set Value");
            AddDefaultMap("Platform Registry Delete Value");
        }
    }
}