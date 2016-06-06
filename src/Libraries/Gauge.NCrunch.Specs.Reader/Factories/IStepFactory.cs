using Gauge.Messages;

namespace Gauge.NCrunch.Specs.Reader.Factories
{
    public interface IStepFactory
    {
        IStep Create(ProtoConcept protoConcept);
        IStep Create(ProtoStep protoStep);
    }
}