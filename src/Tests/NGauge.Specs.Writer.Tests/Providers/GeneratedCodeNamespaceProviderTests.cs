using FluentAssertions;
using Xunit;

namespace NGauge.Specs.Writer.Tests
{
    public sealed class GeneratedCodeNamespaceProviderTests
    {
        [Fact]
        public void GetNamespace_ReturnsExpectedValue()
        {
            IGeneratedCodeNamespaceProvider provider = new GeneratedCodeNamespaceProvider();

            provider
                .GetNamespace()
                .Should()
                .Be(GeneratedCodeNamespaceProvider.Namespace);
        }
    }
}
