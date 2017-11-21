namespace Apprenda.AuditEventForwarder.Syslog
{
    public partial class Apprenda81CallsiteMap
    {
        private void ConfigureDeveloperPortalControllers()
        {
            AddActionMap("Provision Add-on");
            AddActionMap("De-Provision Add-on");
            AddActionMap("Add Groups to Application", "Application Add Groups");
            AddActionMap("Remove Groups from Application", "Application Remove Groups");
            AddActionMap("Start Version");
            AddActionMap("Stop Version");
            AddActionMap("Add Users to Application", "Application Add Users");
            AddActionMap("Remove Users from Application", "Application Remove Users");
            AddActionMap("Delete Logs");
        }
    }
}