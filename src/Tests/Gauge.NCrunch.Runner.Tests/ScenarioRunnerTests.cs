using System;
using NSubstitute;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace Gauge.NCrunch.Runner.Tests
{
    public sealed class ScenarioRunnerTests
    {
        [Fact]
        public void ctor_RequiresStepDefinitionResolver()
        {
            Assert.Throws<ArgumentNullException>(
                "stepDefinitionResolver",
                () => new ScenarioRunner(
                    null,
                    Substitute.For<IStepFactory>()));
        }

        [Fact]
        public void ctor_RequiresStepFactory()
        {
            Assert.Throws<ArgumentNullException>(
                "stepFactory",
                () => new ScenarioRunner(
                    Substitute.For<IStepDefinitionResolver>(),
                    null));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void ExecuteStep_StepTextIsRequired(string stepText)
        {
            var scenarioRunner = CreateScenarioRunner();

            Assert.Throws<ArgumentNullException>(
                "stepText",
                () => scenarioRunner.ExecuteStep(stepText));
        }

        [Theory, AutoData]
        public void ExecuteStep_ConstructsAndExecutesExpectedStep(string stepText)
        {
            var parameters = new object();

            var stepDefinition = Substitute.For<IStepDefinition>();
            var step = Substitute.For<IStep>();

            var scenarioRunner = CreateScenarioRunner(
                stepDefinitionResolver: GetMockStepDefinitionResolver(stepText, stepDefinition),
                stepFactory: GetMockStepFactory(stepDefinition, step));

            scenarioRunner.ExecuteStep(stepText, parameters);

            step
                .Received()
                .Invoke(parameters);
        }

        private static IScenarioRunner CreateScenarioRunner(IStepDefinitionResolver stepDefinitionResolver = null, IStepFactory stepFactory = null)
        {
            return new ScenarioRunner(
                stepDefinitionResolver ?? Substitute.For<IStepDefinitionResolver>(),
                stepFactory            ?? Substitute.For<IStepFactory>());
        }

        private static IStepDefinitionResolver GetMockStepDefinitionResolver(string stepText, IStepDefinition expectedStepDefinition)
        {
            var resolver = Substitute.For<IStepDefinitionResolver>();

            resolver
                .GetStepDefinition(stepText)
                .Returns(expectedStepDefinition);

            return resolver;
        }

        private static IStepFactory GetMockStepFactory(IStepDefinition stepDefinition, IStep expectedStep)
        {
            var factory = Substitute.For<IStepFactory>();

            factory
                .Create(stepDefinition)
                .Returns(expectedStep);

            return factory;
        }
    }
}
