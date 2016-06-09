using System.Linq;
using Gauge.Messages;
using NGauge.CodeContracts;
using NGauge.Specs.Reader.Models;

namespace NGauge.Specs.Reader.Factories
{
    public sealed class SpecificationFactory : ISpecificationFactory
    {
        private readonly IScenarioFactory _scenarioFactory;

        public SpecificationFactory(IScenarioFactory scenarioFactory)
        {
            Contract.RequiresNotNull(scenarioFactory, nameof(scenarioFactory));

            _scenarioFactory = scenarioFactory;
        }

        ISpecification ISpecificationFactory.Create(ProtoSpec protoSpec)
        {
            Contract.RequiresNotNull(protoSpec, nameof(protoSpec));

            var protoScenarios = protoSpec.ItemsList
                .Where(item => item.HasScenario)
                .Select(item => item.Scenario);
            var tags = protoSpec.TagsList;

            return new Specification(
                protoSpec.SpecHeading,
                protoScenarios.SelectOrEmpty(_scenarioFactory.Create),
                tags);
        }
    }
}