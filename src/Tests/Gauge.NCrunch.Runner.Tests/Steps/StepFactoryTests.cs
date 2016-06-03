using System;
using FluentAssertions;
using Gauge.NCrunch.Runner.StepDefinitions;
using Gauge.NCrunch.Runner.Steps;
using NSubstitute;
using Xunit;

namespace Gauge.NCrunch.Runner.Tests.Steps
{
    public sealed class StepFactoryTests
    {
        [Fact]
        public void Create_StepDefinitionRequired()
        {
            IStepFactory stepFactory = new StepFactory();

            Assert.Throws<ArgumentNullException>(
                "stepDefinition",
                () => stepFactory.Create(null));
        }

        [Fact]
        public void Create_ReturnsInstanceOfStep()
        {
            var stepDefinition = Substitute.For<IStepDefinition>();
            IStepFactory stepFactory = new StepFactory();

            var step = stepFactory.Create(stepDefinition);

            step
                .Should()
                .BeOfType<Step>();
        }
    }
}
