# Cake.ISO

Cake Build addin for creating ISO files.

[![Build status](https://ci.appveyor.com/api/projects/status/smkq9ya8m7sa0mpg?svg=true)](https://ci.appveyor.com/project/cakecontrib/cake-iso)

| NuGet Release | NuGet Prerelease |
|---------------|------------------|
|[![NugetRelease](https://img.shields.io/nuget/v/Cake.ISO.svg)](https://www.nuget.org/packages/Cake.ISO/) | [![NugetPrerelease](https://img.shields.io/nuget/vpre/Cake.ISO.svg)](https://www.nuget.org/packages/Cake.ISO/)

## Give a Star! :star:

If you like or are using this project please give it a star. Thanks!

## Installation

Add the following reference to your Cake script:

```csharp
#addin "Cake.ISO"
```

## Usage

```csharp
var inputDir = "C:\path\to\files";
var outputDir = "C:\path\to\output.iso";
var volumeLabel = "MY_PROJECT";

CreateIso(inputDir, outputDir, volumeLabel);
```

## To-Do

* Enable support for bootable ISOs

## Discussion

For questions and to discuss ideas & feature requests, use the [GitHub discussions on the Cake GitHub repository](https://github.com/cake-build/cake/discussions), under the [Extension Q&A](https://github.com/cake-build/cake/discussions/categories/extension-q-a) category.

[![Join in the discussion on the Cake repository](https://img.shields.io/badge/GitHub-Discussions-green?logo=github)](https://github.com/cake-build/cake/discussions)

## Release History

Click on the [Releases](https://github.com/cake-contrib/Cake.ISO/releases) tab on GitHub.

---

_Copyright &copy; 2017-2021 Cake Contributors - Provided under the [MIT License](LICENSE)._
