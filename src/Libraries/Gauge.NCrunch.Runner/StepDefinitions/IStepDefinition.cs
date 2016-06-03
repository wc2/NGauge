using System.Reflection;

namespace Gauge.NCrunch.Runner.StepDefinitions
{
    public interface IStepDefinition
    {
        MethodInfo MethodInfo { get; }
        string StepText { get; }
    }
}