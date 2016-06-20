using FluentAssertions;
using NGauge.Specs.Writer.Providers;
using Xunit;

namespace NGauge.Specs.Writer.Tests.Providers
{
    public sealed class GeneratedCodeNamespaceProviderTests
    {
        [Fact]
        public void GetRootNamespace_ReturnsExpectedValue()
        {
            IGeneratedCodeNamespaceProvider provider = new GeneratedCodeNamespaceProvider();

            provider
                .GetRootNamespace()
                .Should()
                .Be(GeneratedCodeNamespaceProvider.Namespace);
        }

        [Fact]
        public void GetNamespace_ReturnsExpectedValue()
        {
            const string expectedNamespace = "NGauge.Specs.Writer.Tests." + GeneratedCodeNamespaceProvider.Namespace;
            IGeneratedCodeNamespaceProvider provider = new GeneratedCodeNamespaceProvider();

            provider
                .GetNamespace()
                .Should()
                .Be(expectedNamespace);
        }
    }
}
