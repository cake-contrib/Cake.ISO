using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.ISO
{
    [CakeAliasCategory("ISO")]
    public static class IsoAliases
    {
        [CakeMethodAlias]
        public static void CreateIso(this ICakeContext context, string inputPath, string outputPath, string volumeIdentifier)
        {
            var isoBuilder = new IsoCreator(context.Environment, context.Log);
            isoBuilder.CreateIso(inputPath, outputPath, volumeIdentifier);
        }
    }
}
