using System.Collections.Generic;
using Gauge.Messages;

namespace Gauge.NCrunch.Specs.Reader.Factories
{
    public interface IStepFactory
    {
        IEnumerable<IStep> Create(IEnumerable<ProtoItem> steps);
    }
}