using System.CodeDom;

namespace NGauge.Specs.Writer
{
    public interface ICodeSavingService
    {
        void Save(CodeCompileUnit generatedCode, string path);
    }
}