# Cake.ISO

Cake Build addin for creating ISO files.

[![Build status](https://ci.appveyor.com/api/projects/status/smkq9ya8m7sa0mpg?svg=true)](https://ci.appveyor.com/project/austinlparker/cake-iso)

| NuGet Release | NuGet Prerelease |
|---------------|------------------|
|[![NugetRelease](https://img.shields.io/nuget/v/Cake.ISO.svg)](https://www.nuget.org/packages/Cake.ISO/) | [![NugetPrerelease](https://img.shields.io/nuget/vpre/Cake.ISO.svg)](https://www.nuget.org/packages/Cake.ISO/)

## Installation
Add the following reference to your Cake script:
```
#addin "Cake.ISO"
```

## Usage
```
var inputDir = "C:\path\to\files";
var outputDir = "C:\path\to\output.iso";
var volumeLabel = "MY_PROJECT";

CreateIso(inputDir, outputDir, volumeLabel);
```

## To-Do
* Enable support for bootable ISOs
