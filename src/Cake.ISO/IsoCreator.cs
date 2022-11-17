using System;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using DiscUtils.Iso9660;
using Path = System.IO.Path;
using System.IO;

namespace Cake.ISO
{
    /// <summary>
    /// Contains functionality related to creating ISO files.
    /// </summary>
    public class IsoCreator
    {
        private readonly ICakeEnvironment _environment;
        private readonly ICakeLog _log;

        /// <summary>
        /// Generates an ISO file from a given path.
        /// </summary>
        /// <param name="environment">The Cake environment.</param>
        /// <param name="log">The Cake log.</param>
        public IsoCreator(ICakeEnvironment environment, ICakeLog log)
        {
            _environment = environment;
            _log = log;
        }

        /// <summary>
        /// Generates an ISO file from a given path.
        /// </summary>
        /// <param name="inputDirectoryPath">The input directory.</param>
        /// <param name="outputFilePath">The output file name.</param>
        /// <param name="volumeIdentifier">The volume label.</param>
        public void CreateIso(DirectoryPath inputDirectoryPath, FilePath outputFilePath, string volumeIdentifier)
        {
            var inputPath = inputDirectoryPath.FullPath;
            var outputPath = outputFilePath.FullPath;

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