using System;
using FluentAssertions;
using NGauge.Specs.Writer.Providers;
using NGauge.Specs.Writer.Services;
using NSubstitute;
using Ploeh.AutoFixture.Xunit2;
using SystemInterface.IO;
using Xunit;

namespace NGauge.Specs.Writer.Tests.Services
{
    public sealed class GeneratedCodeNamingServiceTests
    {
        [Fact]
        public void ctor_GeneratedCodeNamespaceProviderRequired()
        {
            Assert.Throws<ArgumentNullException>(
                "generatedCodeNamespaceProvider",
                () => new GeneratedCodeNamingService(
                    null,
                    Substitute.For<IPath>()));
        }

        [Fact]
        public void ctor_PathRequired()
        {
            Assert.Throws<ArgumentNullException>(
                "path",
                () => new GeneratedCodeNamingService(
                    Substitute.For<IGeneratedCodeNamespaceProvider>(),
                    null));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void GetGeneratedCodeFolder_ProjectPathRequired(string projectPath)
        {
            var generatedCodeNamingService = CreateGeneratedCodeNamingService();

            Assert.Throws<ArgumentNullException>(
                "projectPath",
                () => generatedCodeNamingService.GetGeneratedCodePath(projectPath));
        }

        [Theory, AutoData]
        public void GetGeneratedCodeFolder_ReturnsExpectedGeneratedCodePath(string bridgeNamespace, string projectPath,
            string expectedGeneratedCodeFolder)
        {
            var generatedCodeNamespaceProvider = Substitute.For<IGeneratedCodeNamespaceProvider>();
            generatedCodeNamespaceProvider
                .GetNamespace()
                .Returns(bridgeNamespace);

            var path = Substitute.For<IPath>();
            path
                .Combine(projectPath, bridgeNamespace)
                .Returns(expectedGeneratedCodeFolder);
            var generatedCodeNamingService = CreateGeneratedCodeNamingService(
                generatedCodeNamespaceProvider,
                path);

            var actualGeneratedCodeFolder = generatedCodeNamingService.GetGeneratedCodePath(projectPath);

            actualGeneratedCodeFolder
                .Should()
                .Be(expectedGeneratedCodeFolder);
        }

        private static IGeneratedCodeNamingService CreateGeneratedCodeNamingService(
            IGeneratedCodeNamespaceProvider generatedCodeNamespaceProvider = null, IPath path = null)
        {
            return new GeneratedCodeNamingService(
                generatedCodeNamespaceProvider ?? Substitute.For<IGeneratedCodeNamespaceProvider>(),
                path                           ?? Substitute.For<IPath>());
        }
    }
}
