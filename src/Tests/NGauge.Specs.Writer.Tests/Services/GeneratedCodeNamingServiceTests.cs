using System;
using FluentAssertions;
using NSubstitute;
using Ploeh.AutoFixture.Xunit2;
using SystemWrapper.IO;
using Xunit;

namespace NGauge.Specs.Writer.Tests
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
                    Substitute.For<IPathWrap>()));
        }

        [Fact]
        public void ctor_PathWrapRequired()
        {
            Assert.Throws<ArgumentNullException>(
                "pathWrap",
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

            var pathWrap = Substitute.For<IPathWrap>();
            pathWrap
                .Combine(projectPath, bridgeNamespace)
                .Returns(expectedGeneratedCodeFolder);
            var generatedCodeNamingService = CreateGeneratedCodeNamingService(
                generatedCodeNamespaceProvider,
                pathWrap);

            var actualGeneratedCodeFolder = generatedCodeNamingService.GetGeneratedCodePath(projectPath);

            actualGeneratedCodeFolder
                .Should()
                .Be(expectedGeneratedCodeFolder);
        }

        private static IGeneratedCodeNamingService CreateGeneratedCodeNamingService(
            IGeneratedCodeNamespaceProvider generatedCodeNamespaceProvider = null, IPathWrap pathWrap = null)
        {
            return new GeneratedCodeNamingService(
                generatedCodeNamespaceProvider ?? Substitute.For<IGeneratedCodeNamespaceProvider>(),
                pathWrap                       ?? Substitute.For<IPathWrap>());
        }
    }
}
