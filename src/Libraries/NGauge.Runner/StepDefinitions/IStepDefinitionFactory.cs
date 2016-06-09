using System.Reflection;

namespace NGauge.Runner.StepDefinitions
{
    public interface IStepDefinitionFactory
    {
        IStepDefinition[] Create(MethodInfo methodInfo);
    }
}