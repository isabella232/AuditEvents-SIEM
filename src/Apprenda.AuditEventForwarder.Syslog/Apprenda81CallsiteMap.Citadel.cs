using Apprenda.SaaSGrid.Extensions.DTO;
using Newtonsoft.Json;
using SyslogNet.Client;

namespace Apprenda.AuditEventForwarder.Syslog
{
    public partial class Apprenda81CallsiteMap
    {
        private void ConfigureCitadel()
        {
            AddDefaultMap("Tenant Administrator User Creation");
            AddActionMap("Tenant Creation");

            AddMap("Login Failure", LoginFailureMapper);
            AddDefaultMap("Reset User Password"); // todo
            AddDefaultMap("Platform User Addition");
            AddDefaultMap("Unauthorized Application Access");
            AddDefaultMap("User Password Reset Failed");
            AddMappedMap(new [] {"Platform User Addition to Tenant Completed"}, "Tenant Add Platform User");
            AddMappedMap("User Password Rest Completed", "User Password Reset");
            AddMappedMap(new[] {"Platform User Removal from Tenant Complete"}, "Tenant Remove Platform User");
        }
        SyslogMessage LoginFailureMapper(AuditedEventDTO evt)
        {
            var loginDetails = RehydrateLoginFailure(evt.Details);
            return FromEventDto(evt, Facility.SecurityOrAuthorizationMessages1, Severity.Notice, $"{evt.Operation} {loginDetails}");
        }

        private string RehydrateLoginFailure(string json)
        {
            var jsonDetails = JsonConvert.DeserializeObject<DetailsObject>(json);
            var d = JsonConvert.DeserializeObject<LoginFailureDto>(jsonDetails.Details);
            return
                $"User {d.identifier} reason {d.reason} Attempts: {d.failedLoginAttempts}. Was Locked out: {d.wasLockedOut} Locked out: {d.isLockedOut} ";
        }
    }

    public class LoginFailureDto
    {
        public string identifier { get; set; }
        public string emailAddress { get; set; }
        public string loginUsername { get; set; }
        public bool knownUser { get; set; }
        public string wasLockedOut { get; set; }
        public string isLockedOut { get; set; }
        public string failedLoginAttempts { get; set; }
        public string reason { get; set; }
    }
}