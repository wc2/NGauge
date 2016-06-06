using System.Linq;
using Gauge.Messages;
using Gauge.NCrunch.CodeContracts;
using Gauge.NCrunch.Specs.Reader.Models;

namespace Gauge.NCrunch.Specs.Reader.Factories
{
    internal sealed class SpecificationFactory : ISpecificationFactory
    {
        private readonly IStepFactory _stepFactory;
        private readonly IScenarioFactory _scenarioFactory;

        public SpecificationFactory(IStepFactory stepFactory, IScenarioFactory scenarioFactory)
        {
            Contract.RequiresNotNull(stepFactory, nameof(stepFactory));
            Contract.RequiresNotNull(scenarioFactory, nameof(scenarioFactory));

            _stepFactory = stepFactory;
            _scenarioFactory = scenarioFactory;
        }

        ISpecification ISpecificationFactory.Create(ProtoSpec protoSpec)
        {
            Contract.RequiresNotNull(protoSpec, nameof(protoSpec));

            var protoConcepts = protoSpec.ItemsList
                .Where(item => item.HasConcept)
                .Select(item => item.Concept);
            var protoScenarios = protoSpec.ItemsList
                .Where(item => item.HasScenario)
                .Select(item => item.Scenario);
            var tags = protoSpec.TagsList;

            return new Specification(
                protoSpec.SpecHeading,
                protoConcepts.SelectOrEmpty(_stepFactory.Create),
                protoScenarios.SelectOrEmpty(_scenarioFactory.Create),
                tags);
        }
    }
}