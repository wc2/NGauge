using System.Reflection;

namespace Gauge.NCrunch.Runner
{
    public interface IStepDefinitionFactory
    {
        IStepDefinition[] Create(MethodInfo methodInfo);
    }
}