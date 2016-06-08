using Gauge.Messages;

namespace Gauge.NCrunch.Specs.Reader.Factories
{
    public interface IStepFactory
    {
        IStep Create(ProtoStep protoStep);
    }
}