#tool nuget:?package=NUnit.ConsoleRunner&version=3.6.1
// #tool nuget:?package=NUnit.Extension.NUnitV2Driver
// #tool nuget:?package=NUnit.Extension.NUnitV2ResultWriter
#tool "nuget:?package=GitVersion.CommandLine&version=3.6.5"
#addin "Cake.Compression"


//////////////////////////////////////////////////////////////////////
// ARGUMENTS
/////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var solution = Argument("solution", "ChristmasMothers.sln");
var configuration = Argument("configuration", "Release");
var PackageOutDir = "./publish";
//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////
Task("Change-Assembly-Info")
    .Does(() =>
{
    var versionInfo = GitVersion(new GitVersionSettings {
        UpdateAssemblyInfo = false,
        NoFetch = true,
        RepositoryPath = "."
    });
    
    var assemblyVersion = versionInfo.MajorMinorPatch + '.' + versionInfo.CommitsSinceVersionSource;
    var repoVersion = versionInfo.SemVer + '-' + versionInfo.CommitsSinceVersionSource;
    var versionFileName = Argument("versionFile", @"version.txt");
    System.IO.File.WriteAllText(versionFileName, repoVersion);
    Information("Version: {0}", repoVersion);
    Information("Assembly Version: {0}", assemblyVersion);

    Func<IFileSystemInfo, bool> exclude_packages =
        fileSystemInfo => !fileSystemInfo.Path.FullPath.Contains(
            "/packages/");

    var files = GetFiles("./**/AssemblyInfo.cs", exclude_packages);
    var company = Argument("company", "... Inc.");
    var copyright = Argument("copyright", "Copyright (c) - " + DateTime.Now.Year);
    var description = Argument("description", "ChristmasMothers.");
    var appVersion = Argument("appVersion", assemblyVersion);

    foreach (var file in files) {
        var assemblyInfo = ParseAssemblyInfo(file.FullPath);
        
        CreateAssemblyInfo(file, new AssemblyInfoSettings {
            CLSCompliant = assemblyInfo.ClsCompliant,
            Company = company,
            ComVisible = assemblyInfo.ComVisible,
            Configuration = configuration,
            Copyright = copyright,
            Description = description,
            FileVersion = appVersion,
            Guid = assemblyInfo.Guid,
            InformationalVersion = appVersion,
            InternalsVisibleTo = assemblyInfo.InternalsVisibleTo,
            Product = assemblyInfo.Product,
            Title = assemblyInfo.Title,
            Trademark = assemblyInfo.Trademark,
            Version = appVersion
        });

        Information(assemblyInfo.Title + " updated to " + appVersion + ".");
		
    }
});

Task("Clean")
    .Does(() =>
    {
    var cleanSetting =new DotNetCoreCleanSettings
     {
        Framework = "netcoreapp2.0",
        Configuration = "Release",
        Verbosity =  DotNetCoreVerbosity.Minimal
     }; 
    DotNetCoreClean(".",cleanSetting);

    });
 
Task("Restore")
    .Does(() => {
        var restoreSetting = new DotNetCoreRestoreSettings
        {
            Verbosity = DotNetCoreVerbosity.Minimal, 
            NoCache = true,
            DiagnosticOutput = true
        };
        DotNetCoreRestore(".",restoreSetting);
        //DotNetCoreRestore(restoreSetting);
    });
 
Task("Build")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .Does(() => {
        var buildSettings = new DotNetCoreBuildSettings
     {
        Framework = "netcoreapp2.0",
        Configuration = "Release",    
        Verbosity = DotNetCoreVerbosity.Minimal,
        NoIncremental = true ,
        DiagnosticOutput = true,
        MSBuildSettings = new DotNetCoreMSBuildSettings
        {
            DiagnosticOutput = true,
            Verbosity = DotNetCoreVerbosity.Minimal
        }
     };
	 DotNetCoreBuild(".",buildSettings);
     });

Task("Tests")
     .Does(() =>
    {
     var settings = new DotNetCoreTestSettings
     {
        Framework = "netcoreapp2.0",
        Configuration = "Release",
        Verbosity =  DotNetCoreVerbosity.Minimal
     };
    var testProjectFiles = GetFiles("./*.Test*/*.csproj");
    foreach(var file in testProjectFiles)
    {
        Information("Tests project file find : ");
        Information(file.FullPath);
        DotNetCoreTest(file.FullPath, settings);

    }
    });

Task("Publish")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .Does(() => {

        if (DirectoryExists(PackageOutDir))
        {
            DeleteDirectory(PackageOutDir, new DeleteDirectorySettings {
                Recursive = true,
                Force = true
            });
        }
        var settings = new DotNetCorePublishSettings
        {
            Framework = "netcoreapp2.0",
            Configuration = "Release",
            OutputDirectory  = PackageOutDir,
        };
                
        DotNetCorePublish("./ChristmasMothers.Web.Application/", settings);
        
    });

Task("Package-Publish")
    .IsDependentOn("Publish")
    .Does(() => {
        var package = Argument("package","publish.zip");
        Zip(PackageOutDir, package);
 
    });

Task("Package-Oracle-Scripts")
    .Does(() =>
    {
        var package = Argument("package", "ChristmasMothers-oracle-scripts.zip"); 

        Zip("database-scripts/oracle/integrated", package);
    });

Task("Package-SqlServer-Scripts")
    .Does(() =>
    {
        var package = Argument("package", "ChristmasMothers-sql-server-scripts.zip"); 

        Zip("database-scripts/sql-server/integrated", package);
    });

Task("Generate-Artifacts")
    .IsDependentOn("Publish")
    .IsDependentOn("Package-Oracle-Scripts")
    .IsDependentOn("Package-SqlServer-Scripts");


Task("Default")
    .IsDependentOn("Build")
    .IsDependentOn("Publish")
    .IsDependentOn("Tests");
 
//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////
 
RunTarget(target);