using System;
using System.Collections.Generic;
using FluentAssertions;
using NSubstitute;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace Gauge.NCrunch.Specs.Reader.Tests
{
    public sealed class SpecificationTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void ctor_NameRequired(string name)
        {
            Assert.Throws<ArgumentNullException>(
                "name",
                () => new Specification(
                    name,
                    Substitute.For<IEnumerable<IStep>>(),
                    Substitute.For<IEnumerable<IScenario>>(),
                    Substitute.For<IEnumerable<string>>()));
        }

        [Fact]
        public void ctor_ContextStepsRequired()
        {
            Assert.Throws<ArgumentNullException>(
                "contextSteps",
                () => new Specification(
                    "some spec",
                    null,
                    Substitute.For<IEnumerable<IScenario>>(),
                    Substitute.For<IEnumerable<string>>()));
        }

        [Fact]
        public void ctor_ScenariosRequired()
        {
            Assert.Throws<ArgumentNullException>(
                "scenarios",
                () => new Specification(
                    "some spec",
                    Substitute.For<IEnumerable<IStep>>(),
                    null,
                    Substitute.For<IEnumerable<string>>()));
        }

        [Fact]
        public void ctor_TagsRequired()
        {
            Assert.Throws<ArgumentNullException>(
                "tags",
                () => new Specification(
                    "some spec",
                    Substitute.For<IEnumerable<IStep>>(),
                    Substitute.For<IEnumerable<IScenario>>(),
                    null));
        }

        [Theory, AutoData]
        public void ctor_SetsName(string name)
        {
            var specification = CreateSpecification(name: name);

            specification.Name
                .Should()
                .Be(name);
        }

        [Fact]
        public void ctor_SetsContextSteps()
        {
            var contextSteps = Substitute.For<IEnumerable<IStep>>();
            var specification = CreateSpecification(contextSteps: contextSteps);

            specification.ContextSteps
                .Should()
                .BeSameAs(contextSteps);
        }

        [Fact]
        public void ctor_SetsScenarios()
        {
            var scenarios = Substitute.For<IEnumerable<IScenario>>();
            var specification = CreateSpecification(scenarios: scenarios);

            specification.Scenarios
                .Should()
                .BeSameAs(scenarios);
        }

        [Fact]
        public void ctor_SetsTags()
        {
            var tags = Substitute.For<IEnumerable<string>>();
            var specification = CreateSpecification(tags: tags);

            specification.Tags
                .Should()
                .BeSameAs(tags);
        }

        private static ISpecification CreateSpecification(string name = null, IEnumerable<IStep> contextSteps = null,
            IEnumerable<IScenario> scenarios = null, IEnumerable<string> tags = null)
        {
            return new Specification(
                name         ?? "spec",
                contextSteps ?? Substitute.For<IEnumerable<IStep>>(),
                scenarios    ?? Substitute.For<IEnumerable<IScenario>>(),
                tags         ?? Substitute.For<IEnumerable<string>>());
        }
    }
}
