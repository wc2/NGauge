using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Linq;
using NGauge.CodeContracts;
using NGauge.Specs.Writer.Factories;

namespace NGauge.Specs.Writer.Services
{
    internal sealed class CodeSavingService : ICodeSavingService
    {
        private readonly IIndentedTextWriterFactory _indentedTextWriterFactory;
        private readonly CodeDomProvider _codeDomProvider;

        public CodeSavingService(IIndentedTextWriterFactory indentedTextWriterFactory, CodeDomProvider codeDomProvider)
        {
            Contract.RequiresNotNull(indentedTextWriterFactory, nameof(indentedTextWriterFactory));
            Contract.RequiresNotNull(codeDomProvider, nameof(codeDomProvider));

            _indentedTextWriterFactory = indentedTextWriterFactory;
            _codeDomProvider = codeDomProvider;
        }

        void ICodeSavingService.Save(CodeCompileUnit generatedCode, string path)
        {
            Contract.RequiresNotNull(generatedCode, nameof(generatedCode));
            Contract.RequiresNotNull(path, nameof(path));

            var name = GetNameOfFirstTypeInFirstNamespace(generatedCode);
            using (var indentedTextWriter = _indentedTextWriterFactory.Create(path, name))
            {
                _codeDomProvider.GenerateCodeFromCompileUnit(
                    generatedCode,
                    indentedTextWriter,
                    new CodeGeneratorOptions());
            }
        }

        private static string GetNameOfFirstTypeInFirstNamespace(CodeCompileUnit generatedCode)
        {
            var name = generatedCode
                .Namespaces.Cast<CodeNamespace>().FirstOrDefault()
                ?.Types.Cast<CodeTypeDeclaration>().FirstOrDefault()
                ?.Name;

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(
                    "Generated code should contain at least one namespace with at least one named type.",
                    nameof(generatedCode));
            }

            return name;
        }
    }
}