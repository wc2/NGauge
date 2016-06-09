using System.CodeDom;
using System.CodeDom.Compiler;
using System.Threading.Tasks;
using NGauge.CodeContracts;

namespace NGauge.Specs.Writer
{
    public sealed class CodeSavingService : ICodeSavingService
    {
        private readonly IFolderCreationService _folderCreationService;
        private readonly IIndentedTextWriterFactory _indentedTextWriterFactory;
        private readonly CodeDomProvider _codeDomProvider;

        public CodeSavingService(IFolderCreationService folderCreationService,
            IIndentedTextWriterFactory indentedTextWriterFactory, CodeDomProvider codeDomProvider)
        {
            Contract.RequiresNotNull(folderCreationService, nameof(folderCreationService));
            Contract.RequiresNotNull(indentedTextWriterFactory, nameof(indentedTextWriterFactory));
            Contract.RequiresNotNull(codeDomProvider, nameof(codeDomProvider));

            _folderCreationService = folderCreationService;
            _indentedTextWriterFactory = indentedTextWriterFactory;
            _codeDomProvider = codeDomProvider;
        }

        Task ICodeSavingService.SaveAsync(CodeCompileUnit generatedCode, string path)
        {
            throw new System.NotImplementedException();
        }
    }
}