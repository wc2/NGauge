using System.CodeDom;

namespace NGauge.Specs.Writer
{
    public interface ISpecificationCodeGenerator
    {
        CodeCompileUnit GenerateCode(ISpecification specification);
    }
}