using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.ISO
{
    /// <summary>
    /// Contains functionality related to creating ISO files.
    /// </summary>
    [CakeAliasCategory("ISO")]
    public static class IsoAliases
    {
        /// <summary>
        /// Generates an ISO file from a given path.
        /// </summary>
        /// <param name="context">The Cake context.</param>
        /// <param name="inputPath">The input directory.</param>
        /// <param name="outputPath">The output file name.</param>
        /// <param name="volumeIdentifier">The volume label.</param>
        /// <example>
        /// <code>
        /// var inputDir = "C:\path\to\files";
        /// var outputDir = "C:\path\to\output.iso";
        /// var volumeLabel = "MY_PROJECT";
        /// CreateIso(inputDir, outputDir, volumeLabel);
        /// </code>
        /// </example>
        [CakeMethodAlias]
        public static void CreateIso(this ICakeContext context, string inputPath, string outputPath, string volumeIdentifier)
        {
            var isoBuilder = new IsoCreator(context.Environment, context.Log);
            isoBuilder.CreateIso(inputPath, outputPath, volumeIdentifier);
        }
    }
}
