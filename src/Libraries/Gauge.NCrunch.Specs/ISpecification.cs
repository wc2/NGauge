using System.Collections.Generic;

namespace Gauge.NCrunch.Specs
{
    public interface ISpecification
    {
        string Name { get; }
        IEnumerable<IStep> ContextSteps { get; }
        IEnumerable<IScenario> Scenarios { get; }
        IEnumerable<string> Tags { get; }
    }
}
