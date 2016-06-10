using NGauge.CodeContracts;
using NGauge.Specs.Writer.Providers;
using SystemInterface.IO;

namespace NGauge.Specs.Writer.Services
{
    internal sealed class GeneratedCodeNamingService : IGeneratedCodeNamingService
    {
        private readonly string _namespace;
        private readonly IPath _path;

        public GeneratedCodeNamingService(IGeneratedCodeNamespaceProvider generatedCodeNamespaceProvider, IPath path)
        {
            Contract.RequiresNotNull(generatedCodeNamespaceProvider, nameof(generatedCodeNamespaceProvider));
            Contract.RequiresNotNull(path, nameof(path));

            _namespace = generatedCodeNamespaceProvider.GetRootNamespace();
            _path = path;
        }

        string IGeneratedCodeNamingService.GetGeneratedCodePath(string projectPath)
        {
            Contract.RequiresNotNull(projectPath, nameof(projectPath));

            return _path.Combine(projectPath, _namespace);
        }
    }
}