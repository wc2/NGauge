using System.Collections.Generic;

namespace NGauge.Specs
{
    public interface ISpecification
    {
        string Name { get; }
        IEnumerable<IScenario> Scenarios { get; }
        IEnumerable<string> Tags { get; }
    }
}
