using System.Collections.Generic;
using System.Threading.Tasks;

namespace NGauge.Specs.Writer
{
    public interface ISpecificationsWriter
    {
        Task<string> WriteSpecificationsAsync(IEnumerable<ISpecification> specifications, string projectPath);
    }
}