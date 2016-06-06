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

            var concepts = protoSpec.ItemsList
                .Where(item => item.HasConcept);
            var scenarios = protoSpec.ItemsList
                .Where(item => item.HasScenario);
            var tags = protoSpec.TagsList;

            return new Specification(
                protoSpec.SpecHeading,
                _stepFactory.Create(concepts),
                _scenarioFactory.Create(scenarios),
                tags);
        }
    }
}