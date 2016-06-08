using Gauge.Messages;

namespace Gauge.NCrunch.Specs.Reader.Factories
{
    public interface ISpecificationFactory
    {
        ISpecification Create(ProtoSpec protoSpec);
    }
}
