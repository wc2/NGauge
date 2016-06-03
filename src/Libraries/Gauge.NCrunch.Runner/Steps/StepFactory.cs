using Gauge.NCrunch.Runner.CodeContracts;
using Gauge.NCrunch.Runner.StepDefinitions;

namespace Gauge.NCrunch.Runner.Steps
{
    internal sealed class StepFactory : IStepFactory
    {
        IStep IStepFactory.Create(IStepDefinition stepDefinition)
        {
            Contract.RequiresNotNull(stepDefinition, nameof(stepDefinition));
            
            return new Step(null, null);
        }
    }
}