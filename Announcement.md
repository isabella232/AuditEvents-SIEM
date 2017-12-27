# Monitoring Apprenda Security and Configuration Changes with SIEM Systems #

The Apprenda Cloud Platform offers IT operators many choices in monitoring the security and configuration events that occur in the platform. We audit events from user interactions with subscribers, developer and operator actions, and other system events using a flexible event-handling extension model. Our auditing pipeline was enhanced with the ability to transform event details into messages and forward them to off-platform monitoring systems, enabling the integration with a wide variety of Security Incident and Event Monitoring (SIEM) solutions. Today, we will talk about a transformation extension which supports the Syslog message protocol, including the Arcsight Common Event Format extension of RFC 5424. Integrating with Syslog capable systems offers IT operators the ability to correllate Apprenda events with other systems, including hardware and operating system monitoring, and pool all events together for advanced analytics and intelligence monitorings.

Publishing events using the Syslog standard (both RFC levels of Syslog are supported) or the Arcsight Common Event Format (CEF) message format allows a variety of systems, including Micro Focus (formerly HPE) ArcSight and RSA NetWitness as well as many standard log aggregation systems such as Splunk or Elastic Search to receive the event streams emitted by the Apprenda Cloud Platform.

## Types of Monitoring Events ##
Some of the more important Apprenda event categories which are supported in this extension include the following:
1. Custom Property Model configuration
1. Deployment Policy configuration
1. Operator Add-On configuration
1. External User Store plugin configuration
1. Kubernetes cluster configuration
1. Database partition configuration
1. External Authentication configuration
1. Cluster Node configuration
1. Tenant configuration
1. Bootstrap Policy configuration
1. Operator Role configuration
1. Operator Workload Management actions
1. Workload execution configuration
1. Log configuration and retention actions
1. Authentication failures
1. Password actions
1. Application Authorization failures 
1. User group role and tenant membership configuration

You can get started on using this integration by getting the latest source code or release package from [GitHub](https://github.com/apprenda/AuditEvents-SIEM/releases). The Platform Operator's installation and configuration walkthrough for this integration is available from the project's [documentation page](https://apprenda.github.io/AuditEvents-SIEM/). You are encouraged to submit pull requests and enhance or customize the extension to add specific information relevant to your SIEM taxonomy needs.

Some samples of Apprenda auditing events and their corresponding Arcsight Logger data points.

![Apprenda Audit Events](onplatform.png)
![Arcsight Logger Events](onarcsight.png)