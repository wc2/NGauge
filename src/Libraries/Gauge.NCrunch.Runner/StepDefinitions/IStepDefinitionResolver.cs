namespace Gauge.NCrunch.Runner.StepDefinitions
{
    public interface IStepDefinitionResolver
    {
        IStepDefinition GetStepDefinition(string stepText);
    }
}
