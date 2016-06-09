using Gauge.Messages;

namespace NGauge.Specs.Reader.Factories
{
    public interface IStepFactory
    {
        IStep Create(ProtoStep protoStep);
    }
}