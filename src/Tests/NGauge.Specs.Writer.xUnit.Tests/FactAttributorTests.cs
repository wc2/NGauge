using FluentAssertions;
using Xunit;

namespace NGauge.Specs.Writer.xUnit.Tests
{
    public sealed class FactAttributorTests
    {
        [Fact]
        public void GetAttribute_ReturnsFactAttribute()
        {
            IGetInvariantTestAttributor attributor = new FactAttributor();

            var attribute = attributor.GetAttribute();

            attribute
                .Should()
                .Be(typeof(FactAttribute));
        }
    }
}
