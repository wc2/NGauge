using Gauge.Messages;

namespace NGauge.Specs.Reader.Factories
{
    public interface IScenarioFactory
    {
        IScenario Create(ProtoScenario protoScenario);
    }
}