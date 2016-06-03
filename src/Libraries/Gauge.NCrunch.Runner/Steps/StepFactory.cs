using Gauge.NCrunch.Runner.StepDefinitions;
using Gauge.NCrunch.Runner.Steps;

namespace Gauge.NCrunch.Runner
{
    internal sealed class StepFactory : IStepFactory
    {
        IStep IStepFactory.Create(IStepDefinition stepDefinition)
        {
            throw new System.NotImplementedException();
        }
    }
}