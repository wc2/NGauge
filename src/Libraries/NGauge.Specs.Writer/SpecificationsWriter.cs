using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NGauge.CodeContracts;

namespace NGauge.Specs.Writer
{
    public sealed class SpecificationsWriter : ISpecificationsWriter
    {
        private readonly IGeneratedCodeNamingService _generatedCodeNamingService;
        private readonly ISpecificationCodeGenerator _specificationCodeGenerator;
        private readonly IFolderDeletionService _folderDeletionService;
        private readonly ICodeSavingService _codeSavingService;

        public SpecificationsWriter(IGeneratedCodeNamingService generatedCodeNamingService,
            ISpecificationCodeGenerator specificationCodeGenerator, IFolderDeletionService folderDeletionService,
            ICodeSavingService codeSavingService)
        {
            Contract.RequiresNotNull(generatedCodeNamingService, nameof(generatedCodeNamingService));
            Contract.RequiresNotNull(specificationCodeGenerator, nameof(specificationCodeGenerator));
            Contract.RequiresNotNull(folderDeletionService, nameof(folderDeletionService));
            Contract.RequiresNotNull(codeSavingService, nameof(codeSavingService));

            _generatedCodeNamingService = generatedCodeNamingService;
            _specificationCodeGenerator = specificationCodeGenerator;
            _folderDeletionService      = folderDeletionService;
            _codeSavingService          = codeSavingService;
        }

        async Task<string> ISpecificationsWriter.WriteSpecificationsAsync(IEnumerable<ISpecification> specifications, string projectPath)
        {
            Contract.RequiresNotNull(specifications, nameof(specifications));
            Contract.RequiresNotNull(projectPath, nameof(projectPath));

            var generatedCodePath = _generatedCodeNamingService.GetGeneratedCodeFolder(projectPath);

            _folderDeletionService.Delete(generatedCodePath);

            var generationTasks = specifications
                .AsParallel()
                .Select(_specificationCodeGenerator.GenerateCode)
                .Select(generatedCode => _codeSavingService.SaveAsync(generatedCode, generatedCodePath));

            await Task.WhenAll(generationTasks);

            return generatedCodePath;
        }
    }
}
