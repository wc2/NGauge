using System;
using FluentAssertions;
using NGauge.Specs.Reader.Models;
using NSubstitute;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace NGauge.Specs.Reader.Tests.Models
{
    public sealed class StepTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void ctor_StepTextRequired(string stepText)
        {
            Assert.Throws<ArgumentNullException>(
                "stepText",
                () => new Step(
                    stepText));
        }

        [Theory, AutoData]
        public void ctor_SetsStepText(string stepText)
        {
            var scenario = CreateStep(stepText: stepText);

            scenario.StepText
                .Should()
                .Be(stepText);
        }

        [Fact]
        public void ctor_SetsParameters()
        {
            var parameters = new[] {Substitute.For<object>()};
            var scenario = CreateStep(parameters: parameters);

            scenario.Parameters
                .Should()
                .BeSameAs(parameters);
        }

        private static IStep CreateStep(string stepText = null, params object[] parameters)
        {
            return new Step(
                stepText ?? "spec",
                parameters);
        }
    }
}