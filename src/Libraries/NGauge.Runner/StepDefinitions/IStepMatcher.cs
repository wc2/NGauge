namespace NGauge.Runner.StepDefinitions
{
    public interface IStepMatcher
    {
        bool IsMatch(string stepDefinitionText, string stepText);
    }
}