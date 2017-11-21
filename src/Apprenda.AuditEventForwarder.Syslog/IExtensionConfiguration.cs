namespace Apprenda.AuditEventForwarder.Syslog
{
    public interface IExtensionConfiguration
    {
        string Host { get; }
        int Port { get; }

        SyslogFlavor Flavor { get; }
        SyslogProtocol Protocol { get; }

        IAuditCallsiteMap AuditEventMap { get; }
    }

    public enum SyslogFlavor
    {
        Rfc3164,
        Rfc5424,
        Cef
    }

    public enum SyslogProtocol
    {
        Tcp,
        EncryptedTcp,
        Udp
    }
}