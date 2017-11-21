namespace Apprenda.AuditEventForwarder.Syslog
{
    public partial class Apprenda81CallsiteMap
    {
        public void ConfigureTenantPortalService()
        {
            AddDefaultMap("User Addition");
            AddDefaultMap("Add Contact Section");
            AddDefaultMap("Remove Contact Section");
            AddDefaultMap("Update Contact Section");
            AddDefaultMap("Add Contact Detail");
            AddDefaultMap("Remove Contact Detail");
            AddDefaultMap("Save Company Profile");
            AddDefaultMap("Set Company Primary Location");
            AddDefaultMap("Assign Tenant Administrator");
            AddMappedMap(new[] {"Addition of a Role to Tenant Completed", "Addition of a Role to Tenant Failed"},
                "Tenant Role Addition");

            AddMappedMap("Remove Role from Tenant", "Tenant Role Removal");
            AddMappedMap(new[] {"Addition of a User to Role Completed", "Addition of a User to Role Failed"},
                "Role User Addition");
            AddMappedMap(new[] {"Addition of a Role to Role Completed", "Addition of a Role to Role Failed"},
                "Role Role Addition");
            AddMappedMap(new[] {"Removal of a User from a Role Completed", "Removal of a User from a Role Failed"},
                "Role User Removal");
            AddMappedMap(new[] {"Removal of Members from a Role Completed", "Removal of Members from a Role Failed"},
                "Role Multiple User Removal");
            AddMappedMap(new[] {"Addition of a Securable to Role Completed", "Addition of a Securable to Role Failed"},
                "Role Securable Addition");
            AddMappedMap(new[] {"Remove Securable from Role.", "Remove Securable from Role"}, "Role Securable Removal");
            AddMappedMap(
                new[]
                {
                    "Subscription Assignment to User Completed", "Subscription Assignment to User Failed",
                    "Subscription Assignment to User"
                }, "User Subscription Assignment");
            AddMappedMap(
                new[]
                {
                    "Subscription Removal from User Completed", "Subscription Removal from User Failed",
                    "Removal of a Subscription from User Failed"
                }, "User Subscription Removal");
            AddMappedMap(
                new[] {"Subscription Creation", "Subscription Creation Completed", "Subscription Creation Failed"},
                "Subscription Creation");
            AddMappedMap(new[] {"Subscription Cancellation Completed", "Subscription Cancellation Failed"},
                "Subscription Cancellation");
        }
    }
}