using Gauge.Messages;

namespace NGauge.Specs.Reader.Factories
{
    public interface ISpecificationFactory
    {
        ISpecification Create(ProtoSpec protoSpec);
    }
}
