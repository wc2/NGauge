using System.Collections.Generic;

namespace Gauge.NCrunch.Specs
{
    public interface IScenario
    {
        string Name { get; }
        IEnumerable<IStep> Steps { get; }
        IEnumerable<string> Tags { get; }
    }
}