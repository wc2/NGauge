using System.Reflection;

namespace NGauge.Runner.StepDefinitions
{
    public interface IStepDefinition
    {
        MethodInfo MethodInfo { get; }
        string StepText { get; }
    }
}