using System;
using System.Collections.Generic;
using System.Reflection;
using FluentAssertions;
using NGauge.Runner.StepDefinitions;
using NGauge.Runner.Steps;
using NSubstitute;
using Xunit;

namespace NGauge.Runner.Tests.Steps
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
        public void Create_ReturnsInstanceOfStepWithInstanceOfMethodInfoDeclaringType()
        {
            CreateStepAndAssertTargetIsInstanceOfType<object>();
            CreateStepAndAssertTargetIsInstanceOfType<List<string>>();
        }

        [Fact]
        public void Create_ReturnsInstanceOfStepWithExpectedMethodInfo()
        {
            var stepDefinition = Substitute.For<IStepDefinition>();
            var methodInfo = GetMockMethodInfoWithDeclaringType<object>();
            stepDefinition
                .MethodInfo
                .Returns(methodInfo);

            IStepFactory stepFactory = new StepFactory();

            var step = stepFactory.Create(stepDefinition);

            step
                .Should()
                .BeOfType<Step>();
        }

        private static void CreateStepAndAssertTargetIsInstanceOfType<T>()
        {
            var methodInfo = GetMockMethodInfoWithDeclaringType<T>();

            var stepDefinition = Substitute.For<IStepDefinition>();
            stepDefinition
                .MethodInfo
                .Returns(methodInfo);

            IStepFactory stepFactory = new StepFactory();

            var step = stepFactory.Create(stepDefinition);

            step.Invoke();

            methodInfo
                .Received()
                .Invoke(
                    Arg.Any<T>(),
                    Arg.Any<object[]>());
        }

        private static MethodInfo GetMockMethodInfoWithDeclaringType<T>()
        {
            var methodInfo = Substitute.For<MethodInfo>();

            methodInfo
                .DeclaringType
                .Returns(typeof(T));

            return methodInfo;
        }
    }
}
