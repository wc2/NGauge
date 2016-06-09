namespace NGauge.Runner.StepDefinitions
{
    public interface IStepDefinitionResolver
    {
        IStepDefinition GetStepDefinition(string stepText);
    }
}
