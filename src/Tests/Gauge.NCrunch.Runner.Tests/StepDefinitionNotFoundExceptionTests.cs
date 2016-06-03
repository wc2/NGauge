using System;
using FluentAssertions;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace Gauge.NCrunch.Runner.Tests
{
    public sealed class StepDefinitionNotFoundExceptionTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void ctor_StepNameRequired(string stepName)
        {
            Assert.Throws<ArgumentNullException>(
                "stepText",
                () => new StepDefinitionNotFoundException(stepName));
        }

        [Theory, AutoData]
        public void ctor_SetsExpectedMessage(string stepName)
        {
            var expectedMessage = string.Format(StepDefinitionNotFoundException.ErrorMessageFormat, stepName);
            var stepDefinitionNotFoundException = new StepDefinitionNotFoundException(stepName);

            stepDefinitionNotFoundException
                .Message
                .Should()
                .Be(expectedMessage);
        }
    }
}