using System.Collections.Generic;

namespace NGauge.Specs
{
    public interface IScenario
    {
        string Name { get; }
        IEnumerable<IStep> Steps { get; }
        IEnumerable<string> Tags { get; }
    }
}