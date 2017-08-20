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
            var testString = "0x0abad1d3a";
            var testFile = $"{Path.GetTempPath()}\\testIso\\test.txt";
            var testOutFile = $"{Path.GetTempPath()}\\testIso\\test.iso";
            if (!Directory.Exists($"{Path.GetTempPath()}\\testIso"))
            {
                Directory.CreateDirectory($"{Path.GetTempPath()}/testIso");
            }
            File.WriteAllText(testFile, testString);

            var builder = new IsoCreator(_fixture.CakeEnvironmentMock.Object, _fixture.CakeLogMock.Object);

            builder.CreateIso($"{Path.GetTempPath()}/testIso", testOutFile, "TEST_ISO");
            Assert.True(File.Exists(testOutFile));
        }

        [Fact]
        public void ShouldCreateNestedFilesAndDirectories()
        {
            var inputPath = $"{Path.GetTempPath()}dirTest";
            var isoDirs = $"{Path.GetTempPath()}dirTest\\nestedOne\\nestedTwo\\";
            if (!Directory.Exists(isoDirs))
            {
                Directory.CreateDirectory(isoDirs);
            }
            var testFile = $"{isoDirs}/test.txt";
            File.WriteAllText(testFile, "I'm a little file!");

            var builder = new IsoCreator(_fixture.CakeEnvironmentMock.Object, _fixture.CakeLogMock.Object);
            builder.CreateIso(inputPath, $"{inputPath}\\dirTest.iso", "DIR_TEST_ISO");
            Assert.True(File.Exists($"{inputPath}\\dirTest.iso"));
        }
    }
}
