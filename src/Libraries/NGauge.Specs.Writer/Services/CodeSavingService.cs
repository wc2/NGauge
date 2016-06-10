using System.CodeDom;
using System.CodeDom.Compiler;
using NGauge.CodeContracts;
using NGauge.Specs.Writer.Factories;

namespace NGauge.Specs.Writer.Services
{
    internal sealed class CodeSavingService : ICodeSavingService
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

        void ICodeSavingService.Save(CodeCompileUnit generatedCode, string path)
        {
            Contract.RequiresNotNull(generatedCode, nameof(generatedCode));
            Contract.RequiresNotNull(path, nameof(path));

            _folderCreationService.EnsureExists(path);
            using (var indentedTextWriter = _indentedTextWriterFactory.Create(path))
            {
                _codeDomProvider.GenerateCodeFromCompileUnit(
                    generatedCode,
                    indentedTextWriter,
                    new CodeGeneratorOptions());
            }
        }
    }
}