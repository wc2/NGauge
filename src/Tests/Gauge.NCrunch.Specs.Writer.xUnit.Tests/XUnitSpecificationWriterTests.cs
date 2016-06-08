using System;
using System.Threading.Tasks;
using Xunit;

namespace Gauge.NCrunch.Specs.Writer.xUnit.Tests
{
    public sealed class XUnitSpecificationWriterTests
    {
        [Fact]
        public async Task WriteSpecificationAsync_SpecificationsRequired()
        {
            var writer = CreateSpecificationWriter();

            await Assert.ThrowsAsync<ArgumentNullException>(
                "specifications",
                () => writer.WriteSpecificationsAsync(null));
        }

        private static ISpecificationWriter CreateSpecificationWriter()
        {
            return new XUnitSpecificationWriter();
        }
    }
}
