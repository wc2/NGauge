using System.CodeDom;
using System.Threading.Tasks;

namespace NGauge.Specs.Writer
{
    public sealed class CodeSavingService : ICodeSavingService
    {
        Task ICodeSavingService.SaveAsync(CodeCompileUnit generatedCode, string path)
        {
            throw new System.NotImplementedException();
        }
    }
}