using System.CodeDom;
using System.Threading.Tasks;

namespace NGauge.Specs.Writer
{
    public interface ICodeSavingService {
        Task SaveAsync(CodeCompileUnit generatedCode, string path);
    }
}