using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Cake.Core;
using Cake.Core.Diagnostics;
using Xunit.Abstractions;

namespace Cake.ISO.Tests
{
    public class IsoTestsFixture
    {
        public IsoTestsFixture()
        {
            CakeEnvironmentMock = new Mock<ICakeEnvironment>();
            CakeLogMock = new Mock<ICakeLog>();
        }
        public Mock<ICakeEnvironment> CakeEnvironmentMock { get; set; }
        public Mock<ICakeLog> CakeLogMock { get; set; } 
    }
    public class IsoTests : IClassFixture<IsoTestsFixture>
    {
        private readonly ITestOutputHelper output;
        private IsoTestsFixture _fixture;
        public IsoTests(ITestOutputHelper output, IsoTestsFixture fixture)
        {
            this.output = output;
            _fixture = fixture;
        }
        [Fact]
        public void ShouldCreateAnIsoWithAFile()
        {
            var testFileName = Path.GetRandomFileName();
            var testDir = Path.GetRandomFileName();
            var testFile = $"{Path.GetTempPath()}{Path.DirectorySeparatorChar}{testDir}{Path.DirectorySeparatorChar}{testFileName}";
            var testOutFile = $"{Path.GetTempPath()}{Path.DirectorySeparatorChar}{testDir}{Path.DirectorySeparatorChar}{testFileName}.iso";

            if (!Directory.Exists($"{Path.GetTempPath()}{Path.DirectorySeparatorChar}{testDir}"))
            {
                Directory.CreateDirectory($"{Path.GetTempPath()}{Path.DirectorySeparatorChar}{testDir}");
            }
            File.WriteAllText(testFile, "0x0abad1d3a");

            var builder = new IsoCreator(_fixture.CakeEnvironmentMock.Object, _fixture.CakeLogMock.Object);

            builder.CreateIso($"{Path.GetTempPath()}{testDir}", testOutFile, "TEST_ISO");
            Assert.True(File.Exists(testOutFile));
        }

        [Fact]
        public void ShouldCreateNestedFilesAndDirectories()
        {
            var testDir = Path.GetRandomFileName();
            var inputPath = $"{Path.GetTempPath()}{testDir}";
            var isoDirs = $"{Path.GetTempPath()}{testDir}{Path.DirectorySeparatorChar}nestedOne{Path.DirectorySeparatorChar}nestedTwo{Path.DirectorySeparatorChar}";
            if (!Directory.Exists(isoDirs))
            {
                Directory.CreateDirectory(isoDirs);
            }
            var testFile = $"{isoDirs}{Path.DirectorySeparatorChar}test.txt";
            File.WriteAllText(testFile, "I'm a little file!");

            var builder = new IsoCreator(_fixture.CakeEnvironmentMock.Object, _fixture.CakeLogMock.Object);
            builder.CreateIso(inputPath, $"{inputPath}{Path.DirectorySeparatorChar}dirTest.iso", "DIR_TEST_ISO");
            Assert.True(File.Exists($"{inputPath}{Path.DirectorySeparatorChar}dirTest.iso"));
        }
    }
}
