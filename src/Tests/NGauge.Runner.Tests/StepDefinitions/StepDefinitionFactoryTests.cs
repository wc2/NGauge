using System;
using FluentAssertions;
using NGauge.Runner.StepDefinitions;
using NGauge.Runner.Tests.StepDefinitions.TestCases;
using Xunit;

namespace NGauge.Runner.Tests.StepDefinitions
{
    public sealed class StepDefinitionFactoryTests
    {
        [Fact]
        public void Create_MethodInfoRequired()
        {
            IStepDefinitionFactory factory = new StepDefinitionFactory();

            Assert.Throws<ArgumentNullException>(
                "methodInfo",
                () => factory.Create(null));
        }

        [Fact]
        public void Create_ReturnsInstanceOfStepDefinitionArray()
        {
            var methodInfo = typeof(TypeWithOneStepAttributedMethod)
                .GetMethod(nameof(TypeWithOneStepAttributedMethod.SomeStep));
            IStepDefinitionFactory factory = new StepDefinitionFactory();

            var stepDefinition = factory.Create(methodInfo);

            stepDefinition[0]
                .Should()
                .BeOfType<StepDefinition>();
        }

        [Fact]
        public void Create_ReturnsOneStepDefinitionPerStepTextOnMethodInfo()
        {
            CreateAndAssertExpectedNumberOfStepDefinitionsForMethodInfo(
                type: typeof(TypeWithOneStepAttributedMethod),
                methodName: nameof(TypeWithOneStepAttributedMethod.SomeStep),
                expectedStepDefinitions: 1,
                explanation: "method has one step text");

            CreateAndAssertExpectedNumberOfStepDefinitionsForMethodInfo(
                type: typeof(TypeWithTwoStepAttributedMethods),
                methodName: nameof(TypeWithTwoStepAttributedMethods.SomeOtherStep),
                expectedStepDefinitions: 2,
                explanation: "method has two step texts");

            CreateAndAssertExpectedNumberOfStepDefinitionsForMethodInfo(
                type: typeof(TypeWithTwoStepAttributedMethods),
                methodName: nameof(TypeWithTwoStepAttributedMethods.SomeSecondStep),
                expectedStepDefinitions: 1,
                explanation: "method has one step text");
        }

        [Fact]
        public void Create_ReturnsNoStepDefinitionsWhenMethodInfoHasNoStepAttribute()
        {
            CreateAndAssertExpectedNumberOfStepDefinitionsForMethodInfo(
                type: typeof(TypeWithOneStepAttributedMethod),
                methodName: nameof(TypeWithOneStepAttributedMethod.IgnoredMethod),
                expectedStepDefinitions: 0,
                explanation: "method has no Step attribute");

            CreateAndAssertExpectedNumberOfStepDefinitionsForMethodInfo(
                type: typeof(TypeWithTwoStepAttributedMethods),
                methodName: nameof(TypeWithTwoStepAttributedMethods.AnotherIgnoredMethod),
                expectedStepDefinitions: 0,
                explanation: "method has no Step attribute");
        }

        private static void CreateAndAssertExpectedNumberOfStepDefinitionsForMethodInfo(Type type, string methodName, int expectedStepDefinitions, string explanation)
        {
            var methodInfo = type.GetMethod(methodName);
            IStepDefinitionFactory factory = new StepDefinitionFactory();

            var stepDefinitions = factory.Create(methodInfo);

            stepDefinitions
                .Length
                .Should()
                .Be(expectedStepDefinitions, explanation);
        }
    }
}