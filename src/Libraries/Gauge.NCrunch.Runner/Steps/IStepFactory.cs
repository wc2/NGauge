using Gauge.NCrunch.Runner.StepDefinitions;

namespace Gauge.NCrunch.Runner.Steps
{
    public interface IStepFactory
    {
        IStep Create(IStepDefinition stepDefinition);
    }
}