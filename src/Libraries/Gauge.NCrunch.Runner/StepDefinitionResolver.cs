using System.Collections.Generic;
using System.Linq;
using Gauge.NCrunch.Runner.CodeContracts;

namespace Gauge.NCrunch.Runner
{
    internal sealed class StepDefinitionResolver : IStepDefinitionResolver
    {
        private readonly IEnumerable<IStepDefinition> _stepDefinitions;
        private readonly IStepMatcher _stepMatcher;

        public StepDefinitionResolver(IStepAttributedMethodResolver stepAttributedMethodResolver, IStepDefinitionFactory stepDefinitionFactory, IStepMatcher stepMatcher)
        {
            Contract.RequiresNotNull(stepAttributedMethodResolver, nameof(stepAttributedMethodResolver));
            Contract.RequiresNotNull(stepDefinitionFactory, nameof(stepDefinitionFactory));
            Contract.RequiresNotNull(stepMatcher, nameof(stepMatcher));

            _stepDefinitions = stepAttributedMethodResolver
                .GetStepAttributeMethods()
                .SelectMany(stepDefinitionFactory.Create)
                .ToArray();

            _stepMatcher = stepMatcher;
        }

        IStepDefinition IStepDefinitionResolver.GetStepDefinition(string stepText)
        {
            Contract.RequiresNotNull(stepText, nameof(stepText));

            var matchingStepDefinitions = _stepDefinitions
                .Where(stepDefinition =>
                    _stepMatcher.IsMatch(stepDefinition.StepText, stepText))
                .ToArray();

            if (matchingStepDefinitions.Length == 0)
            {
                throw new StepDefinitionNotFoundException(stepText);
            }

            if (matchingStepDefinitions.Length > 1)
            {
                throw new MultipleMatchingStepDefinitionsException(stepText); 
            }

            return matchingStepDefinitions.Single();
        }
    }
}