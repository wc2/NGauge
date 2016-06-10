namespace NGauge.Specs.Writer.Providers
{
    internal sealed class GeneratedCodeNamespaceProvider : IGeneratedCodeNamespaceProvider
    {
        internal const string Namespace = "NGauge";

        string IGeneratedCodeNamespaceProvider.GetNamespace()
        {
            return Namespace;
        }
    }
}