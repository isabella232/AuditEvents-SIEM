namespace Apprenda.AuditEventForwarder.Syslog
{
    public partial class Apprenda81CallsiteMap
    {
        private void ConfigureApprendaCatalog()
        {
            AddActionMap("Custom Property Model Creation");
            AddActionMap("Custom Property Model Modification");
            AddActionMap("Custom Property Model Deletion");
            AddActionMap("Deployment Policy Creation");
            AddActionMap("Deployment Policy Modification");
            AddActionMap("Deployment Policy Deletion");
            AddMappedMap(new [] {"Add-on Update Completed", "Add-on Update Failed"}, "Add-on Update");
        }
    }
}