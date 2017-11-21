namespace Apprenda.AuditEventForwarder.Syslog
{
    public class TestExtensionConfiguration : IExtensionConfiguration
    {
        public string Host => "localhost";
        public int Port => 514;
        public SyslogFlavor Flavor => SyslogFlavor.Rfc5424;

        public SyslogProtocol Protocol => SyslogProtocol.Tcp;
        public IAuditCallsiteMap AuditEventMap { get; } = new Apprenda81CallsiteMap();
    }
}