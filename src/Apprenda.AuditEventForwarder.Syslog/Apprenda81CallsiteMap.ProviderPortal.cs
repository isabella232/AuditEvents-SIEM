namespace Apprenda.AuditEventForwarder.Syslog
{
    public partial class Apprenda81CallsiteMap
    {
        private void ConfigureProviderPortalService()
        {
            AddDefaultMap("Application Creation");
            AddMappedMap(new [] {"Application Deletion Started", "Application Deletion Completed"}, "Application Delete");
            AddDefaultMap("Version Creation");
            AddDefaultMap("Version Deletion");
            AddMappedMap("Updating Version Binaries", "Version Binaries Update");
            AddMappedMap(
                new[]
                {
                    "Persistence Partition Relocation Failed", "Persistence Partition Relocation Started",
                    "Persistence Partition Relocation Completed", "Persistence Partition Relocation"
                }, "Relocate Persistence Partition");
            AddDefaultMap("Version Demotion");
            AddDefaultMap("Cancel Promotion");
            AddMap("Removing Vanity URL Certificate", DefaultActionResultMapper);
            AddMap("Updating Vanity URL Certificate", DefaultActionResultMapper);
        }
    }
}