using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using NGauge.Bridge.Tests.Helpers;
using NGauge.Specs;
using NGauge.Specs.Reader;
using NGauge.Specs.Writer;
using NSubstitute;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace NGauge.Bridge.Tests
{
    public sealed class GeneratorTests
    {
        [Fact]
        public void ctor_SpecificationReaderRequired()
        {
            Assert.Throws<ArgumentNullException>(
                "specificationsReader",
                () => new Generator(
                    null,
                    Substitute.For<ISpecificationsWriter>()));
        }

        [Fact]
        public void ctor_SpecificationWriterRequired()
        {
            Assert.Throws<ArgumentNullException>(
                "specificationsWriter",
                () => new Generator(
                    Substitute.For<ISpecificationsReader>(),
                    null));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public async Task CreateOrUpdateAsync_ProjectPathRequired(string projectPath)
        {
            var generator = CreateGenerator();

            await Assert.ThrowsAsync<ArgumentNullException>(
                "projectPath",
                () => generator.CreateOrUpdateAsync(projectPath));
        }

        [Fact]
        public async Task CreateOrUpdateAsync_ReadsSpecifications()
        {
            var reader = Substitute.For<ISpecificationsReader>();
            var generator = CreateGenerator(reader);

            await generator.CreateOrUpdateAsync("some path");

            reader
                .Received()
                .ReadSpecifications();
        }

        [Theory, AutoData]
        public async Task CreateOrUpdateAsync_WritesSpecificationsWithExpectedProjectPath(string projectPath)
        {
            var writer = Substitute.For<ISpecificationsWriter>();

            var generator = CreateGenerator(specificationsWriter: writer);

            await generator.CreateOrUpdateAsync(projectPath);

            writer
                .Received()
                .WriteSpecificationsAsync(
                    Arg.Any<IEnumerable<ISpecification>>(),
                    projectPath)
                .IgnoreAwaitForNSubstituteAssertion();
        }

        [Fact]
        public async Task CreateOrUpdateAsync_WritesReadSpecifications()
        {
            var specifications = new[] { Substitute.For<ISpecification>() };
            var reader = Substitute.For<ISpecificationsReader>();
            reader
                .ReadSpecifications()
                .Returns(specifications);

            var writer = Substitute.For<ISpecificationsWriter>();

            var generator = CreateGenerator(reader, writer);

            await generator.CreateOrUpdateAsync("some path");

            writer
                .Received()
                .WriteSpecificationsAsync(
                    specifications,
                    Arg.Any<string>())
                .IgnoreAwaitForNSubstituteAssertion();
        }

        [Theory, AutoData]
        public async Task CreateOrUpdateAsync_ReturnsGeneratedCodeFileName(string expectedGeneratedCodeFile)
        {
            var writer = Substitute.For<ISpecificationsWriter>();
            writer
                .WriteSpecificationsAsync(Arg.Any<IEnumerable<ISpecification>>(), Arg.Any<string>())
                .Returns(Task.FromResult(expectedGeneratedCodeFile));

            var generator = CreateGenerator(specificationsWriter: writer);

            var actualGeneratedCodeFileName = await generator.CreateOrUpdateAsync("some path");

            actualGeneratedCodeFileName
                .Should()
                .Be(expectedGeneratedCodeFile);
        }

        private static IGenerator CreateGenerator(ISpecificationsReader specificationsReader = null,
            ISpecificationsWriter specificationsWriter = null)
        {
            return new Generator(
                specificationsReader ?? Substitute.For<ISpecificationsReader>(),
                specificationsWriter ?? Substitute.For<ISpecificationsWriter>());
        }
    }
}
