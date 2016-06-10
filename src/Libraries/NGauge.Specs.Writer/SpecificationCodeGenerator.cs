using System.CodeDom;
using NGauge.CodeContracts;

namespace NGauge.Specs.Writer
{
    internal sealed class SpecificationCodeGenerator : ISpecificationCodeGenerator
    {
        CodeCompileUnit ISpecificationCodeGenerator.GenerateCode(ISpecification specification)
        {
            Contract.RequiresNotNull(specification, nameof(specification));

            throw new System.NotImplementedException();
        }
    }
}