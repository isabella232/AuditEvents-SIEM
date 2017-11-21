public class BuildParameters{
    public string Target {get; set;}
    public string Configuration {get;set;}

    public string SourcePath {get; set;}
    public string OutputPath {get; set;}

    public string AddonName {get; set;}
    public string AddonProjectPath {get; set;} 
    public string AddonProjectFile {get; set;} 
    public IEnumerable<string> AddonBinaries {get; set;} 
    public string AddonPackagePath {get; set;} 
    public string AddonArchive {get; set;} 

    public string ExtensionName {get; set;}
    public string ExtensionProjectPath {get; set;}
    public string ExtensionProjectFile {get; set;} 
    public IEnumerable<string> ExtensionBinaries {get; set;} 
    public string ExtensionPackagePath {get; set;} 
    public string ExtensionArchive {get; set;}


    public static BuildParameters Load(ICakeContext context, BuildSystem buildSystem){
        if (context == null) throw new ArgumentNullException(nameof(context));
        if (buildSystem == null) throw new ArgumentNullException(nameof(buildSystem));
        var source = "./src";
        string buildConfiguration = context.Arguments.GetArgument("Configuration") ?? "Release";
        string buildTarget = context.Arguments.GetArgument("Target") ?? "Default";
        var addonName = "Apprenda.AddOns.Syslog";
        var addonProjectPath = $"{source}/{addonName}";
        var addonProjectFile = $"{addonProjectPath}/{addonName}.csproj";        
        var addonBinPath = $"{addonProjectPath}/bin/{buildConfiguration}";
        var addonBinaries = new[]{
            "Apprenda.AddOns.Syslog.dll",
            "Apprenda.Utils.dll",
            "Newtonsoft.Json.dll",
            "SaaSGrid.API.dll",
            "icon.png",
            "SyslogNet.Client.dll",
            "AddonManifest.xml"
        }.Select(fp => $"{addonBinPath}/{fp}");

        var extensionName = "Apprenda.AuditEventForwarder.Syslog";
        var extensionProjectPath = $"{source}/{extensionName}";
        var extensionProjectFile = $"{extensionProjectPath}/{extensionName}.csproj";
        var extensionBinPath = $"{extensionProjectPath}/bin/{buildConfiguration}";
        var extensionBinaries = new[]{ 
            "Apprenda.AuditEventForwarder.Syslog.dll",
            "Apprenda.AuditEventForwarder.Syslog.dll.config",
            "Apprenda.Utils.dll",
            "Newtonsoft.Json.dll",
            "SaasGrid.API.dll",
            "Syslognet.Client.dll"
        }.Select(fp => $"{extensionBinPath}/{fp}");

        var outPath = "./out";
        var addonArchive = $"{outPath}/Apprenda.SyslogAddon.zip";
        var extensionArchive  = $"{outPath}/Apprenda.AuditEventForwarder.Syslog.zip";

        return new BuildParameters{
            SourcePath =  source,
            OutputPath = outPath ,
            AddonName = addonName,
            AddonProjectPath = addonProjectPath,
            AddonProjectFile = addonProjectFile,
            AddonArchive = addonArchive,
            AddonBinaries = addonBinaries,
            AddonPackagePath = $"{addonProjectPath}/.package",
            ExtensionName = extensionName,
            ExtensionProjectPath = extensionProjectPath,
            ExtensionProjectFile = extensionProjectFile,
            ExtensionArchive = extensionArchive,
            ExtensionBinaries = extensionBinaries,
            ExtensionPackagePath = $"{extensionProjectPath}/.package",
            Target = buildTarget,
            Configuration = buildConfiguration
        };
    }
}

public class AddonInfo 
{

}

public class ApprendaArchiveInfo
{

}