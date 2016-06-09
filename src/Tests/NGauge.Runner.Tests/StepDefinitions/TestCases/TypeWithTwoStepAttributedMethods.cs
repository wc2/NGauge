namespace NGauge.Runner.Tests.StepDefinitions.TestCases
{
    internal sealed class TypeWithTwoStepAttributedMethods
    {
        internal static readonly string[] ExpectedSteps = { "SomeOtherStep", "SomeSecondStep" };
        [Gauge.CSharp.Lib.Attribute.Step("hunky", "dory")]
        public void SomeOtherStep() { }
        [Gauge.CSharp.Lib.Attribute.Step("Hello, tootie!")]
        public void SomeSecondStep() { }
        public void AnotherIgnoredMethod() { }
    }
}