using System;
using FluentAssertions;
using NGauge.Runner.StepDefinitions;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace NGauge.Runner.Tests.StepDefinitions
{
    public sealed class MultipleMatchingStepDefinitionsExceptionTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void ctor_StepNameRequired(string stepName)
        {
            Assert.Throws<ArgumentNullException>(
                "stepText",
                () => new MultipleMatchingStepDefinitionsException(stepName));
        }

        [Theory, AutoData]
        public void ctor_SetsExpectedMessage(string stepName)
        {
            var expectedMessage = string.Format(MultipleMatchingStepDefinitionsException.ErrorMessageFormat, stepName);
            var stepDefinitionNotFoundException = new MultipleMatchingStepDefinitionsException(stepName);

            stepDefinitionNotFoundException
                .Message
                .Should()
                .Be(expectedMessage);
        }
    }
}