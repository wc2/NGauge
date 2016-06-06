using System;
using System.Collections.Generic;
using FluentAssertions;
using NSubstitute;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace Gauge.NCrunch.Specs.Reader.Tests
{
    public sealed class ScenarioTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void ctor_NameRequired(string name)
        {
            Assert.Throws<ArgumentNullException>(
                "name",
                () => new Scenario(
                    name,
                    Substitute.For<IEnumerable<IStep>>(),
                    Substitute.For<IEnumerable<string>>()));
        }

        [Fact]
        public void ctor_StepsRequired()
        {
            Assert.Throws<ArgumentNullException>(
                "steps",
                () => new Scenario(
                    "some spec",
                    null,
                    Substitute.For<IEnumerable<string>>()));
        }

        [Fact]
        public void ctor_TagsRequired()
        {
            Assert.Throws<ArgumentNullException>(
                "tags",
                () => new Scenario(
                    "some spec",
                    Substitute.For<IEnumerable<IStep>>(),
                    null));
        }

        [Theory, AutoData]
        public void ctor_SetsName(string name)
        {
            var scenario = CreateScenario(name: name);

            scenario.Name
                .Should()
                .Be(name);
        }

        [Fact]
        public void ctor_SetsSteps()
        {
            var steps = Substitute.For<IEnumerable<IStep>>();
            var scenario = CreateScenario(steps: steps);

            scenario.Steps
                .Should()
                .BeSameAs(steps);
        }

        [Fact]
        public void ctor_SetsTags()
        {
            var tags = Substitute.For<IEnumerable<string>>();
            var scenario = CreateScenario(tags: tags);

            scenario.Tags
                .Should()
                .BeSameAs(tags);
        }

        private static IScenario CreateScenario(string name = null, IEnumerable<IStep> steps = null,
            IEnumerable<string> tags = null)
        {
            return new Scenario(
                name  ?? "spec",
                steps ?? Substitute.For<IEnumerable<IStep>>(),
                tags  ?? Substitute.For<IEnumerable<string>>());
        }
    }
}