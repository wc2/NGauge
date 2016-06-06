using System.Collections.Generic;
using Gauge.Messages;

namespace Gauge.NCrunch.Specs.Reader.Factories
{
    public interface IScenarioFactory
    {
        IEnumerable<IScenario> Create(IEnumerable<ProtoItem> scenarios);
    }
}