namespace Gauge.NCrunch.Runner.Tests.StepDefinitions.TestCases
{
    internal sealed class TypeWithOneStepAttributedMethod
    {
        internal static readonly string[] ExpectedSteps = { "SomeStep" };
        [Gauge.CSharp.Lib.Attribute.Step("some step")]
        public void SomeStep() { }
        public void IgnoredMethod() { }
    }
}