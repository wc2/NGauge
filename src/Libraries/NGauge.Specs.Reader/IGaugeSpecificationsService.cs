using System.Collections.Generic;
using Gauge.Messages;

namespace NGauge.Specs.Reader
{
    public interface IGaugeSpecificationsService
    {
        IEnumerable<ProtoSpec> GetSpecs();
    }
}
