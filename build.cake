#tool "nuget:?package=xunit.runner.console&version=2.2.0"

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

// vars
var isLocalBuild = !AppVeyor.IsRunningOnAppVeyor;
var version = "0.1.4-alpha";
var semVersion = isLocalBuild ? version : string.Concat(version, "-build-", AppVeyor.Environment.Build.Number);

// Definitions
var buildDir = Directory("./src/Cake.ISO/bin") + Directory(configuration);

Task("Clean")
    .Does(() =>
{
    CleanDirectory(buildDir);
});

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    DotNetCoreRestore("./Cake.ISO.sln");
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
    {
      DotNetCoreBuild("./Cake.ISO.sln", new DotNetCoreBuildSettings { Configuration = configuration });
});

Task("Run-Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
{
    DotNetCoreTest("./tests/Cake.ISO.Tests/Cake.ISO.Tests.csproj");
});

Task("Create-Nuget-Package")
    .IsDependentOn("Build")
    .IsDependentOn("Run-Unit-Tests")
    .Does(() =>
    {
        // make sure our working dir is empty
        CleanDirectory("./nuget/tmp");
        
        // two-parts -- publish to get _all_ the files, then an explicit pack.
        DotNetCorePublish("./src/Cake.ISO/Cake.ISO.csproj", new DotNetCorePublishSettings
        {          
            Framework = "netstandard2.0",
            Configuration = "Release",
            OutputDirectory = "./nuget/tmp"
        });

        NuGetPack("./nuget/Cake.ISO.nuspec", new NuGetPackSettings {          
            IncludeReferencedProjects = false,
            Version = version,
            BasePath = "./nuget",
            OutputDirectory = "./nuget"
        });
    });

Task("Publish-Nuget-Package")
    .IsDependentOn("Create-Nuget-Package")
    .Does(() =>
    {
        var packages = GetFiles("./nuget/*.nupkg");
        NuGetPush(packages, new NuGetPushSettings {
            Source = "https://www.nuget.org/api/v2/package",
            ApiKey = EnvironmentVariable("NugetApiKey")
        });
    });

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Run-Unit-Tests");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);