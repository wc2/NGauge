using System.Reflection;

namespace Gauge.NCrunch.Runner.StepDefinitions
{
    public interface IStepDefinitionFactory
    {
        IStepDefinition[] Create(MethodInfo methodInfo);
    }
}