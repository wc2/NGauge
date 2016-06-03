using Gauge.NCrunch.Runner.CodeContracts;
using Gauge.NCrunch.Runner.StepDefinitions;
using Gauge.NCrunch.Runner.Steps;

namespace Gauge.NCrunch.Runner
{
    internal sealed class ScenarioRunner : IScenarioRunner
    {
        private readonly IStepDefinitionResolver _stepDefinitionResolver;
        private readonly IStepFactory _stepFactory;

        internal ScenarioRunner(IStepDefinitionResolver stepDefinitionResolver, IStepFactory stepFactory)
        {
            Contract.RequiresNotNull(stepDefinitionResolver, nameof(stepDefinitionResolver));
            Contract.RequiresNotNull(stepFactory, nameof(stepFactory));

            _stepDefinitionResolver = stepDefinitionResolver;
            _stepFactory = stepFactory;
        }

        void IScenarioRunner.ExecuteStep(string stepText, params object[] parameters)
        {
            Contract.RequiresNotNull(stepText, nameof(stepText));

            var stepDefinition = _stepDefinitionResolver.GetStepDefinition(stepText);
            var step = _stepFactory.Create(stepDefinition);

            step.Invoke(parameters);
        }
    }
}