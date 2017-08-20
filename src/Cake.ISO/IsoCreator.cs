using System;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using DiscUtils.Iso9660;
using Path = System.IO.Path;
using System.IO;

namespace Cake.ISO
{
    public class IsoCreator
    {
        private readonly ICakeEnvironment _environment;
        private readonly ICakeLog _log;

        public IsoCreator(ICakeEnvironment environment, ICakeLog log)
        {
            _environment = environment;
            _log = log;
        }

        public void CreateIso(string inputPath, string outputPath, string volumeIdentifier)
        {
            _log.Verbose($"Creating ISO from directory {inputPath}");
            var builder = new CDBuilder
            {
                UseJoliet = true,
                VolumeIdentifier = volumeIdentifier ?? "CAKE_ISO"
            };
            foreach (var entry in Directory.GetFileSystemEntries(inputPath, "*", SearchOption.AllDirectories))
            {
                var fileInfo = new FileInfo(entry);
                if ((fileInfo.Attributes & FileAttributes.Directory) != 0)
                {
                    _log.Verbose($"Creating directory: {Path.GetFullPath(entry).Replace(inputPath, "")}");
                    builder.AddDirectory(Path.GetFullPath(entry).Replace(inputPath, ""));
                }
                else
                {
                    _log.Verbose($"Creating file: {Path.GetFullPath(entry).Replace(inputPath, "")}");
                    builder.AddFile(Path.GetFullPath(entry).Replace(inputPath, ""), entry);
                }
            }
            builder.Build(outputPath);
        }
    }
}