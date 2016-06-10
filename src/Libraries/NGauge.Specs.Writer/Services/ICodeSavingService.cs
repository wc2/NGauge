using System.CodeDom;

namespace NGauge.Specs.Writer.Services
{
    public interface ICodeSavingService
    {
        void Save(CodeCompileUnit generatedCode, string path);
    }
}