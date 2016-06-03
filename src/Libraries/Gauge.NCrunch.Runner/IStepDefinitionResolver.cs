namespace Gauge.NCrunch.Runner
{
    public interface IStepDefinitionResolver
    {
        IStepDefinition GetStepDefinition(string stepText);
    }
}
