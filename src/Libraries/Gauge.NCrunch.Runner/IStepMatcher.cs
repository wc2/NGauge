namespace Gauge.NCrunch.Runner
{
    public interface IStepMatcher
    {
        bool IsMatch(string stepDefinitionText, string stepText);
    }
}