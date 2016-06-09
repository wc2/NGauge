namespace NGauge.Specs.Writer
{
    public sealed class GeneratedCodeNamespaceProvider : IGeneratedCodeNamespaceProvider
    {
        string IGeneratedCodeNamespaceProvider.GetNamespace()
        {
            return Namespace;
        }

        internal const string Namespace = "NGauge";
    }
}