using System;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using DiscUtils.Iso9660;
using Path = System.IO.Path;

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
            var builder = new CDBuilder();
            builder.UseJoliet = true;
            builder.VolumeIdentifier = volumeIdentifier ?? "CAKE_ISO";
            foreach (var files in System.IO.Directory.GetFiles(inputPath))
            {
                builder.AddFile(Path.GetFileName(files), files);
            }
            foreach (var dir in System.IO.Directory.GetDirectories(inputPath))
            {
                builder.AddDirectory(dir);
            }
            builder.Build(outputPath);
        }
    }
}