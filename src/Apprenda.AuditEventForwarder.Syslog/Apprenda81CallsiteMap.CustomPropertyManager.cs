namespace Apprenda.AuditEventForwarder.Syslog
{
    public partial class Apprenda81CallsiteMap
    {
        void ConfigureCustomPropertyService()
        {
            AddDefaultMap("Custom Property Modification");
            AddDefaultMap("Custom Property Addition");
            AddDefaultMap("Custom Property Removal");
        }
    }
}