using System.Collections.Generic;
using Gauge.NCrunch.CodeContracts;

namespace Gauge.NCrunch.Specs.Reader.Models
{
    internal sealed class Specification : ISpecification
    {
        internal Specification(string name, IEnumerable<IStep> contextSteps, IEnumerable<IScenario> scenarios,
            IEnumerable<string> tags)
        {
            Contract.RequiresNotNull(name, nameof(name));
            Contract.RequiresNotNull(contextSteps, nameof(contextSteps));
            Contract.RequiresNotNull(scenarios, nameof(scenarios));
            Contract.RequiresNotNull(tags, nameof(tags));

            Name = name;
            ContextSteps = contextSteps;
            Scenarios = scenarios;
            Tags = tags;
        }

        public string Name { get; }
        public IEnumerable<IStep> ContextSteps { get; }
        public IEnumerable<IScenario> Scenarios { get; }
        public IEnumerable<string> Tags { get; }
    }
}
