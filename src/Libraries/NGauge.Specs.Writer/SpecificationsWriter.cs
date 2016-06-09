using System.Collections.Generic;
using System.Threading.Tasks;

namespace NGauge.Specs.Writer
{
    public sealed class SpecificationsWriter : ISpecificationsWriter
    {
        Task<string> ISpecificationsWriter.WriteSpecificationsAsync(IEnumerable<ISpecification> specifications, string projectPath)
        {
            throw new System.NotImplementedException();
        }
    }
}
