namespace Gauge.NCrunch.Runner.StepDefinitions
{
    public interface IStepMatcher
    {
        bool IsMatch(string stepDefinitionText, string stepText);
    }
}