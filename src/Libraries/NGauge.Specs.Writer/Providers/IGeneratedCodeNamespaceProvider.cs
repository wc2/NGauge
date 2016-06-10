namespace NGauge.Specs.Writer.Providers
{
    public interface IGeneratedCodeNamespaceProvider
    {
        string GetNamespace();
        string GetRootNamespace();
    }
}