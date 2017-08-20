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
    public class IsoTests
    {
        private readonly ITestOutputHelper output;
        public IsoTests(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public void ShouldCreateAnIsoWithAFile()
        {
            var testString = "0x0abad1d3a";
            var testFile = $"{Path.GetTempPath()}/testIso/test.txt";
            var testOutFile = $"{Path.GetTempPath()}/test.iso";
            if (!Directory.Exists($"{Path.GetTempPath()}/testIso"))
            {
                Directory.CreateDirectory($"{Path.GetTempPath()}/testIso");
            }
            File.WriteAllText(testFile, testString);
            var cakeMock = new Mock<ICakeEnvironment>();
            var logMock = new Mock<ICakeLog>();
            var builder = new IsoCreator(cakeMock.Object, logMock.Object);
            output.WriteLine(testOutFile);
            builder.CreateIso($"{Path.GetTempPath()}/testIso", testOutFile, "TEST_ISO");
            Assert.True(File.Exists(testOutFile));
        }
    }
}
