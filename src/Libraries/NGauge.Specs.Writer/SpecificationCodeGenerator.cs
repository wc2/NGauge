using System;
using System.CodeDom;
using System.Linq;
using NGauge.CodeContracts;
using NGauge.Specs.Writer.Providers;

namespace NGauge.Specs.Writer
{
    internal sealed class SpecificationCodeGenerator : ISpecificationCodeGenerator
    {
        private const string RunnerNamespace = "NGauge.Runner";
        private readonly IGeneratedCodeNamespaceProvider _generatedCodeNamespaceProvider;
        private readonly IGetInvariantTestAttributor _getInvariantTestAttributor;

        public SpecificationCodeGenerator(IGeneratedCodeNamespaceProvider generatedCodeNamespaceProvider,
            IGetInvariantTestAttributor getInvariantTestAttributor)
        {
            Contract.RequiresNotNull(generatedCodeNamespaceProvider, nameof(generatedCodeNamespaceProvider));
            Contract.RequiresNotNull(getInvariantTestAttributor, nameof(getInvariantTestAttributor));

            _generatedCodeNamespaceProvider = generatedCodeNamespaceProvider;
            _getInvariantTestAttributor = getInvariantTestAttributor;
        }

        CodeCompileUnit ISpecificationCodeGenerator.GenerateCode(ISpecification specification)
        {
            Contract.RequiresNotNull(specification, nameof(specification));

            var generatedCode = new CodeCompileUnit();

            generatedCode.Namespaces.Add(CreateTestNamespace(specification));

            return generatedCode;
        }

        private CodeNamespace CreateTestNamespace(ISpecification specification)
        {
            var namespaceName = _generatedCodeNamespaceProvider.GetNamespace();
            var ns = new CodeNamespace(namespaceName);

            ns.Imports.Add(new CodeNamespaceImport(RunnerNamespace));
            ns.Types.Add(CreateTestClass(specification));

            return ns;
        }

        private CodeTypeDeclaration CreateTestClass(ISpecification specification)
        {
            if (string.IsNullOrWhiteSpace(specification.Name))
            {
                throw new ArgumentException("Specification must have a name.", nameof(specification));
            }

            var testClass = new CodeTypeDeclaration(specification.Name) {IsClass = true};
            var testMethods = specification
                .Scenarios
                .Select(CreateTestMethod)
                .ToArray();

            testClass.Members.AddRange(new CodeTypeMemberCollection(testMethods));

            return testClass;
        }

        private CodeTypeMember CreateTestMethod(IScenario scenario)
        {
            var invariantTestAttribute = _getInvariantTestAttributor.GetAttribute();
            var testMethod = new CodeMemberMethod {Name = scenario.Name};

            testMethod.CustomAttributes.Add(new CodeAttributeDeclaration(invariantTestAttribute.FullName));

            return testMethod;
        }
    }
}