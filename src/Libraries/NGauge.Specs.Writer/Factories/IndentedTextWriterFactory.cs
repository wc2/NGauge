using System.CodeDom.Compiler;
using System.IO;
using NGauge.CodeContracts;
using NGauge.Specs.Writer.Services;
using SystemInterface.IO;

namespace NGauge.Specs.Writer.Factories
{
    internal sealed class IndentedTextWriterFactory : IIndentedTextWriterFactory
    {
        private readonly IFolderCreationService _folderCreationService;
        private readonly IPath _path;

        public IndentedTextWriterFactory(IFolderCreationService folderCreationService, IPath path)
        {
            Contract.RequiresNotNull(folderCreationService, nameof(folderCreationService));
            Contract.RequiresNotNull(path, nameof(path));

            _folderCreationService = folderCreationService;
            _path = path;
        }

        IndentedTextWriter IIndentedTextWriterFactory.Create(string path, string name)
        {
            Contract.RequiresNotNull(path, nameof(path));
            Contract.RequiresNotNull(name, nameof(name));

            _folderCreationService.EnsureExists(path);
            var fileName = _path.Combine(path, name);

            return new IndentedTextWriter(new StreamWriter(fileName, append: false));
        }
    }
}