using System.Reflection;
using NGauge.CodeContracts;

namespace NGauge.Runner.StepDefinitions
{
    internal sealed class StepDefinition : IStepDefinition
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