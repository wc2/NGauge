using System;
using Gauge.NCrunch.CodeContracts;
using Gauge.NCrunch.Runner.StepDefinitions;

namespace Gauge.NCrunch.Runner.Steps
{
    internal sealed class StepFactory : IStepFactory
    {
        IStep IStepFactory.Create(IStepDefinition stepDefinition)
        {
            Contract.RequiresNotNull(stepDefinition, nameof(stepDefinition));

            var target = Activator.CreateInstance(stepDefinition.MethodInfo.DeclaringType);

            return new Step(target, stepDefinition.MethodInfo);
        }
    }
}