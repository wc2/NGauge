using System.Collections.Generic;
using Gauge.Messages;

namespace Gauge.NCrunch.Specs.Reader
{
    public interface IGaugeSpecificationsService
    {
        IEnumerable<ProtoSpec> GetSpecs();
    }
}
