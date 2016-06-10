using System.Reflection;

namespace NGauge.Specs.Writer.Providers
{
    internal sealed class GeneratedCodeNamespaceProvider : IGeneratedCodeNamespaceProvider
    {
        internal const string Namespace = "NGauge";

        string IGeneratedCodeNamespaceProvider.GetRootNamespace()
        {
            return Namespace;
        }

        string IGeneratedCodeNamespaceProvider.GetNamespace()
        {
            return $"{Assembly.GetCallingAssembly().GetName().Name}.{Namespace}";
        }
    }
}