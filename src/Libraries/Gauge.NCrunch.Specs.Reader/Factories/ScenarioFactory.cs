using System.Linq;
using Gauge.Messages;
using Gauge.NCrunch.CodeContracts;
using Gauge.NCrunch.Specs.Reader.Models;

namespace Gauge.NCrunch.Specs.Reader.Factories
{
    internal sealed class ScenarioFactory : IScenarioFactory
    {
        private readonly IStepFactory _stepFactory;

        public ScenarioFactory(IStepFactory stepFactory)
        {
            Contract.RequiresNotNull(stepFactory, nameof(stepFactory));

            _stepFactory = stepFactory;
        }

        IScenario IScenarioFactory.Create(ProtoScenario protoScenario)
        {
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