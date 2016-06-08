using Gauge.Messages;

namespace Gauge.NCrunch.Specs.Reader.Factories
{
    public interface IScenarioFactory
    {
        IScenario Create(ProtoScenario protoScenario);
    }
}