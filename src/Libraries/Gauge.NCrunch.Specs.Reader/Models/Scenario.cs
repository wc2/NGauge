using System.Collections.Generic;
using Gauge.NCrunch.CodeContracts;

namespace Gauge.NCrunch.Specs.Reader.Models
{
    internal sealed class Scenario : IScenario
    {
        internal Scenario(string name, IEnumerable<IStep> steps, IEnumerable<string> tags)
        {
            Contract.RequiresNotNull(name, nameof(name));
            Contract.RequiresNotNull(steps, nameof(steps));
            Contract.RequiresNotNull(tags, nameof(tags));

            Name = name;
            Steps = steps;
            Tags = tags;
        }

        public string Name { get; }
        public IEnumerable<IStep> Steps { get; }
        public IEnumerable<string> Tags { get; }
    }
}