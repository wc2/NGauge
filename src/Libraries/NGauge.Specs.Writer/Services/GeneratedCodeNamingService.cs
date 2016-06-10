using NGauge.CodeContracts;
using NGauge.Specs.Writer.Providers;
using SystemWrapper.IO;

namespace NGauge.Specs.Writer.Services
{
    public sealed class GeneratedCodeNamingService : IGeneratedCodeNamingService
    {
        private readonly string _namespace;
        private readonly IPathWrap _pathWrap;

        public GeneratedCodeNamingService(IGeneratedCodeNamespaceProvider generatedCodeNamespaceProvider, IPathWrap pathWrap)
        {
            Contract.RequiresNotNull(generatedCodeNamespaceProvider, nameof(generatedCodeNamespaceProvider));
            Contract.RequiresNotNull(pathWrap, nameof(pathWrap));

            _namespace = generatedCodeNamespaceProvider.GetNamespace();
            _pathWrap = pathWrap;
        }

        string IGeneratedCodeNamingService.GetGeneratedCodePath(string projectPath)
        {
            Contract.RequiresNotNull(projectPath, nameof(projectPath));

            return _pathWrap.Combine(projectPath, _namespace);
        }
    }
}