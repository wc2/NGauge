using System;
using System.Reflection;
using FluentAssertions;
using Gauge.NCrunch.Runner.StepDefinitions;
using NSubstitute;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace Gauge.NCrunch.Runner.Tests.StepDefinitions
{
    public sealed class StepDefinitionTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void ctor_StepTextRequired(string stepText)
        {
            Assert.Throws<ArgumentNullException>(
                "stepText",
                () => new StepDefinition(
                    stepText,
                    Substitute.For<MethodInfo>()));
        }

        [Fact]
        public void ctor_MethodInfoRequired()
        {
            Assert.Throws<ArgumentNullException>(
                "methodInfo",
                () => new StepDefinition(
                    "some step",
                    null));
        }

        [Theory, AutoData]
        public void ctor_SetsStepText(string stepText)
        {
            IStepDefinition stepDefinition = new StepDefinition(stepText, Substitute.For<MethodInfo>());

            stepDefinition
                .StepText
                .Should()
                .Be(stepText);
        }

        [Fact]
        public void ctor_SetsMethodInfo()
        {
            var methodInfo = Substitute.For<MethodInfo>();
            IStepDefinition stepDefinition = new StepDefinition("some step", methodInfo);

            stepDefinition
                .MethodInfo
                .Should()
                .BeSameAs(methodInfo);
        }
    }
}