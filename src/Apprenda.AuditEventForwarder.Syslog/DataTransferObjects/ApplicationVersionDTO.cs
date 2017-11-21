using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apprenda.AuditEventForwarder.Syslog
{
    public class ApplicationVersionDTO 
    {
        public string Alias { get; set; }
        public Guid ApplicationId { get; set; }
        public string Description { get; set; }
        public Guid Id { get; set; }
        public bool IsStopped { get; set; }
        public string Name { get; set; }
        public string PreviousVersionAlias { get; set; }
        public ApplicationVersionStage Stage { get; set; }
        public ApplicationVersionLifecycleStrategy Strategy { get; set; }
        public VersionTransitionType Transition { get; set; }
        public PresentationDeploymentStrategy? UrlStrategy { get; set; }
        public Guid VersionTenantId { get; set; }
    }
    public enum ApplicationVersionStage : byte
    {
        Definition = 1,
        Sandbox = 2,
        Published = 3,
        Archived = 4
    }
    public enum VersionTransitionType
    {
        NoTransition = 0,
        Promoting = 1,
        Demoting = 2
    }

    public enum ApplicationVersionLifecycleStrategy
    {
        Patch = 0,
        NewArchive = 1
    }
    public enum PresentationDeploymentStrategy : byte
    {
        CommingledRootApp = 3,
        CommingledAppRoot = 4
    }
}
