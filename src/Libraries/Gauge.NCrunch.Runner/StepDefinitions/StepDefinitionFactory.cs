using System.Reflection;

namespace Gauge.NCrunch.Runner.StepDefinitions
{
    internal sealed class StepDefinitionFactory : IStepDefinitionFactory
    {
        IStepDefinition[] IStepDefinitionFactory.Create(MethodInfo methodInfo)
        {
            throw new System.NotImplementedException();
        }
    }
}