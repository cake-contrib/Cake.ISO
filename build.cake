#tool "nuget:?package=xunit.runner.console&version=2.2.0"

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

// vars
var isLocalBuild = !AppVeyor.IsRunningOnAppVeyor;
var version = "0.1.1-alpha";
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
    NuGetRestore("./Cake.ISO.sln");
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
    if(IsRunningOnWindows())
    {
      // Use MSBuild
      MSBuild("./Cake.ISO.sln", settings =>
        settings.SetConfiguration(configuration));
    }
    else
    {
      // Use XBuild
      XBuild("./src/Example.sln", settings =>
        settings.SetConfiguration(configuration));
    }
});

Task("Run-Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
{
    XUnit2("./tests/**/bin/" + configuration + "/*.Tests.dll");
});

Task("Create-Nuget-Package")
    .IsDependentOn("Build")
    .IsDependentOn("Run-Unit-Tests")
    .Does(() =>
    {
        var packSettings = new NuGetPackSettings {
            Version = version,
            BasePath = "./src/Cake.ISO/bin/Release",
            OutputDirectory = "./nuget"
        };
        NuGetPack("./Cake.ISO.nuspec", packSettings);
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