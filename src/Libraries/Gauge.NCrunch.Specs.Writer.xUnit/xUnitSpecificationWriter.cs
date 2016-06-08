using System.Collections.Generic;
using System.Threading.Tasks;
using Gauge.NCrunch.CodeContracts;

namespace Gauge.NCrunch.Specs.Writer.xUnit
{
    public sealed class XUnitSpecificationWriter : ISpecificationWriter
    {
        Task ISpecificationWriter.WriteSpecificationsAsync(IEnumerable<ISpecification> specifications)
        {
            Contract.RequiresNotNull(specifications, nameof(specifications));

            throw new System.NotImplementedException();
        }
    }
}
