using System;
using System.CodeDom;
using System.Linq;
using System.Text.RegularExpressions;
using NGauge.CodeContracts;
using NGauge.Runner;
using NGauge.Specs.Writer.Providers;

namespace NGauge.Specs.Writer
{
    internal sealed class SpecificationCodeGenerator : ISpecificationCodeGenerator
    {
        private static readonly Regex InvalidCodeSymbolPattern = new Regex(@"\W");
        private const string RunnerNamespace = "NGauge.Runner";
        private const string RunnerVariableName = "runner";
        private readonly IGeneratedCodeNamespaceProvider _generatedCodeNamespaceProvider;
        private readonly IGetInvariantTestAttributor _getInvariantTestAttributor;
        private static readonly CodeMethodInvokeExpression CreateRunnerExpression =
            new CodeMethodInvokeExpression(
                new CodeTypeReferenceExpression(
                    typeof(Runner.Scenario).FullName),
                    nameof(Runner.Scenario.CreateRunner));

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

            var testClass = new CodeTypeDeclaration(GetCodeSafeName(specification.Name))
            {
                IsClass = true,
                IsPartial = true
            };
            var testMethods = specification
                .Scenarios
                .Select(CreateTestMethod)
                .ToArray();

            testClass.Members.AddRange(testMethods);

            return testClass;
        }

        private CodeTypeMember CreateTestMethod(IScenario scenario)
        {
            var invariantTestAttribute = _getInvariantTestAttributor.GetAttribute();
            var testMethod = new CodeMemberMethod
            {
                Name = GetCodeSafeName(scenario.Name),
                Attributes = MemberAttributes.Public
            };
            var runnerDeclaration = new CodeVariableDeclarationStatement(
                type: typeof(IScenarioRunner),
                name: RunnerVariableName,
                initExpression: CreateRunnerExpression);

            testMethod.CustomAttributes.Add(new CodeAttributeDeclaration(invariantTestAttribute.FullName));
            testMethod.Statements.Add(runnerDeclaration);

            scenario
                .Steps
                .Select(CreateTestStatement)
                .ToList()
                .ForEach(statement => testMethod.Statements.Add(statement));

            return testMethod;
        }

        private static string GetCodeSafeName(string name)
        {
            return InvalidCodeSymbolPattern.Replace(name, "");
        }

        private static CodeMethodInvokeExpression CreateTestStatement(IStep step)
        {
            return new CodeMethodInvokeExpression(
                new CodeVariableReferenceExpression("runner"),
                nameof(IScenarioRunner.ExecuteStep),
                new[] { step.StepText }.Concat(step.Parameters ?? new object[] { })
                    .Select(CreateParameter)
                    .ToArray());
        }

        private static CodePrimitiveExpression CreateParameter(object parameter)
        {
            return new CodePrimitiveExpression(parameter);
        }
    }
}