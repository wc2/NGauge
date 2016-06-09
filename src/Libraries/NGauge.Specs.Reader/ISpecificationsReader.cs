using System.Collections.Generic;

namespace NGauge.Specs.Reader
{
    public interface ISpecificationsReader
    {
        IEnumerable<ISpecification> ReadSpecifications();
    }
}