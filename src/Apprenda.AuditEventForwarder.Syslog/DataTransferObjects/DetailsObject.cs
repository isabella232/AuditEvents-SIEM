namespace Apprenda.AuditEventForwarder.Syslog
{
    class DetailsObject
    {
        // Properties are used for serialization
        // ReSharper disable UnusedAutoPropertyAccessor.Local
        // ReSharper disable MemberCanBePrivate.Local
        public string Details { get; set; }
        public string OriginalValue { get; set; }
        public string NewValue { get; set; }
        // ReSharper restore UnusedAutoPropertyAccessor.Local
        // ReSharper restore MemberCanBePrivate.Local
    }
}