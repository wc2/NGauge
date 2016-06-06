using System.Linq;
using FluentAssertions;
using Gauge.NCrunch.Runner.StepDefinitions;
using Xunit;

namespace Gauge.NCrunch.Runner.Tests.StepDefinitions
{
    public sealed class StepAttributedMethodResolverTests
    {
        #region Sample Types

        private class TypeWithOneStepAttributedMethod
        {
            internal static readonly string[] ExpectedSteps = {"SomeStep"};
            [Gauge.CSharp.Lib.Attribute.Step("some step")]
            public void SomeStep() { }
            public void IgnoredMethod() { }
        }

        private class TypeWithTwoStepAttributedMethods
        {
            internal static readonly string[] ExpectedSteps = { "SomeOtherStep", "SomeSecondStep" };
            [Gauge.CSharp.Lib.Attribute.Step("hunky")]
            public void SomeOtherStep() { }
            [Gauge.CSharp.Lib.Attribute.Step("dory")]
            public void SomeSecondStep() { }
            public void AnotherIgnoredMethod() { }
        }
        #endregion

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
