using Gauge.NCrunch.CodeContracts;
using Gauge.NCrunch.Core;

namespace Gauge.NCrunch.Runner.StepDefinitions
{
    internal sealed class StepMatcher : IStepMatcher
    {
        private readonly IStepTextParameterExtractor _stepTextParameterExtractor;

        public StepMatcher(IStepTextParameterExtractor stepTextParameterExtractor)
        {
            Contract.RequiresNotNull(stepTextParameterExtractor, nameof(stepTextParameterExtractor));

            _stepTextParameterExtractor = stepTextParameterExtractor;
        }

        bool IStepMatcher.IsMatch(string stepDefinitionText, string stepText)
        {
            return string.Equals(
                _stepTextParameterExtractor.ExtractParameters(stepDefinitionText),
                stepText);
        }
    }
}