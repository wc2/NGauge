using System.Linq;
using System.Reflection;
using Gauge.NCrunch.CodeContracts;
using GaugeStep = Gauge.CSharp.Lib.Attribute.Step;

namespace Gauge.NCrunch.Runner.StepDefinitions
{
    internal sealed class StepDefinitionFactory : IStepDefinitionFactory
    {
        IStepDefinition[] IStepDefinitionFactory.Create(MethodInfo methodInfo)
        {
            Contract.RequiresNotNull(methodInfo, nameof(methodInfo));

            return methodInfo
                .GetCustomAttribute<GaugeStep>()
                ?.Names
                .Select(stepText => new StepDefinition(stepText, methodInfo))
                .Cast<IStepDefinition>()
                .ToArray() ?? new IStepDefinition[] { };
        }
    }
}