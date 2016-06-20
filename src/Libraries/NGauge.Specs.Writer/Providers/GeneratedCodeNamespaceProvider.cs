using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace NGauge.Specs.Writer.Providers
{
    internal sealed class GeneratedCodeNamespaceProvider : IGeneratedCodeNamespaceProvider
    {
        private static readonly Assembly WriterAssembly = typeof(IGeneratedCodeNamespaceProvider).Assembly;
        private const string Namespace = "NGaugeBridge";

        string IGeneratedCodeNamespaceProvider.GetRootNamespace()
        {
            return Namespace;
        }

        string IGeneratedCodeNamespaceProvider.GetNamespace()
        {
            return $"{GetTestAssemblyName()}.{Namespace}";
        }

        private static string GetTestAssemblyName()
        {
            var stackTrace = new StackTrace();
            return stackTrace
                .GetFrames()
                .Select(frame => frame.GetMethod().DeclaringType.Assembly)
                .First(assembly => assembly != WriterAssembly)
                .GetName()
                .Name;
        }
    }
}