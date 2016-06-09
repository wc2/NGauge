using System.Linq;
using Gauge.Messages;
using NGauge.CodeContracts;
using NGauge.Specs.Reader.Models;

namespace NGauge.Specs.Reader.Factories
{
    public sealed class ScenarioFactory : IScenarioFactory
    {
        private readonly IStepFactory _stepFactory;

        public ScenarioFactory(IStepFactory stepFactory)
        {
            Contract.RequiresNotNull(stepFactory, nameof(stepFactory));

            _stepFactory = stepFactory;
        }

        IScenario IScenarioFactory.Create(ProtoScenario protoScenario)
        {
            Contract.RequiresNotNull(protoScenario, nameof(protoScenario));

            var protoSteps = protoScenario
                .ScenarioItemsList
                .Where(item => item.HasStep)
                .Select(item => item.Step);

            return new Scenario(
                protoScenario.ScenarioHeading,
                protoSteps.SelectOrEmpty(_stepFactory.Create),
                protoScenario.TagsList);
        }
    }
}