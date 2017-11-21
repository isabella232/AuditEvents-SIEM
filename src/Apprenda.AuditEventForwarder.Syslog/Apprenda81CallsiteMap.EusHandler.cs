namespace Apprenda.AuditEventForwarder.Syslog
{
    public partial class Apprenda81CallsiteMap
    {
        private void ConfigureUserStorePluginHandler()
        {
            AddDefaultMap("EUS Plugin Addition");
            AddDefaultMap("EUS Plugin Modification");
        }
    }
}