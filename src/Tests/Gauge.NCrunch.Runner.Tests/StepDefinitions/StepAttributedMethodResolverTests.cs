using System.Linq;
using FluentAssertions;
using Gauge.NCrunch.Runner.StepDefinitions;
using Gauge.NCrunch.Runner.Tests.StepDefinitions.TestCases;
using Xunit;

namespace Gauge.NCrunch.Runner.Tests.StepDefinitions
{
    public sealed class StepAttributedMethodResolverTests
    {
        [Fact]
        public void GetStepAttributedMethods_ReturnsExpectedMethodsFromTypeWithOneStepAttributedMethod()
        {
            var expectedMethods = TypeWithOneStepAttributedMethod.ExpectedSteps
                .Concat(TypeWithTwoStepAttributedMethods.ExpectedSteps);

            IStepAttributedMethodResolver resolver = new StepAttributedMethodResolver();

            var actualMethods = resolver.GetStepAttributedMethods()
                .Select(methodInfo => methodInfo.Name);

            actualMethods
                .Should()
                .BeEquivalentTo(expectedMethods);
        }
    }
}
