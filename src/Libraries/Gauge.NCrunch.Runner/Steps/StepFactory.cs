using Gauge.NCrunch.Runner.StepDefinitions;

namespace Gauge.NCrunch.Runner.Steps
{
    internal sealed class StepFactory : IStepFactory
    {
        IStep IStepFactory.Create(IStepDefinition stepDefinition)
        {
            throw new System.NotImplementedException();
        }
    }
}