using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gauge.NCrunch.Specs.Writer
{
    public interface ISpecificationWriter
    {
        Task WriteSpecificationsAsync(IEnumerable<ISpecification> specifications);
    }
}
