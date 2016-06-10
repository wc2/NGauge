using FluentAssertions;
using NGauge.Specs.Writer.Providers;
using Xunit;

namespace NGauge.Specs.Writer.Tests.Providers
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
