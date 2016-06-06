using System.Reflection;
using Gauge.NCrunch.Runner.CodeContracts;

namespace Gauge.NCrunch.Runner.StepDefinitions
{
    internal class StepDefinition : IStepDefinition
    {
        internal StepDefinition(string stepText, MethodInfo methodInfo)
        {
            Contract.RequiresNotNull(stepText, nameof(stepText));
            Contract.RequiresNotNull(methodInfo, nameof(methodInfo));

            StepText = stepText;
            MethodInfo = methodInfo;
        }

        public MethodInfo MethodInfo { get; }
        
        public string StepText { get; }
    }
}