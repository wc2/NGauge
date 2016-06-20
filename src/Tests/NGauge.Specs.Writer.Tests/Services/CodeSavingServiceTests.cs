using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using NGauge.Specs.Writer.Factories;
using NGauge.Specs.Writer.Services;
using NSubstitute;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace NGauge.Specs.Writer.Tests.Services
{
    public sealed class CodeSavingServiceTests
    {
        [Fact]
        public void ctor_IndentedTextWriterFactoryRequired()
        {
            Assert.Throws<ArgumentNullException>(
                "indentedTextWriterFactory",
                () => new CodeSavingService(
                    null,
                    Substitute.For<CodeDomProvider>()));
        }

        [Fact]
        public void ctor_CodeDomProviderRequired()
        {
            Assert.Throws<ArgumentNullException>(
                "codeDomProvider",
                () => new CodeSavingService(
                    Substitute.For<IIndentedTextWriterFactory>(),
                    null));
        }

        [Fact]
        public void Save_GeneratedCodeRequired()
        {
            var codeSavingService = CreateCodeSavingService();

            Assert.Throws<ArgumentNullException>(
                "generatedCode",
                () => codeSavingService.Save(
                    null,
                    "some path"));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Save_PathRequired(string path)
        {
            var codeSavingService = CreateCodeSavingService();

            Assert.Throws<ArgumentNullException>(
                "path",
                () => codeSavingService.Save(
                    Substitute.For<CodeCompileUnit>(),
                    path));
        }

        [Fact]
        public void Save_GeneratedCodeMustHaveAtLeastOneNamespace()
        {
            var codeSavingService = CreateCodeSavingService();

            Assert.Throws<ArgumentException>(
                "generatedCode",
                () => codeSavingService.Save(new CodeCompileUnit(), "some path"));
        }

        [Fact]
        public void Save_GeneratedCodeFirstNamespaceMustHaveAtLeastOneType()
        {
            var codeSavingService = CreateCodeSavingService();
            var generatedCode = new CodeCompileUnit();
            generatedCode.Namespaces.Add(new CodeNamespace());

            Assert.Throws<ArgumentException>(
                "generatedCode",
                () => codeSavingService.Save(generatedCode, "some path"));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Save_GeneratedCodeFirstNamespaceFirstTypeMustHaveName(string name)
        {
            var codeSavingService = CreateCodeSavingService();

            Assert.Throws<ArgumentException>(
                "generatedCode",
                () => codeSavingService.Save(GetMockGeneratedCode(name), "some path"));
        }

        [Theory, AutoData]
        public void Save_CreatesIndentedTextWriterWithExpectedPath(string path)
        {
            var indentedTextWriterFactory = Substitute.For<IIndentedTextWriterFactory>();
            var codeSavingService = CreateCodeSavingService(indentedTextWriterFactory: indentedTextWriterFactory);

            codeSavingService.Save(GetMockGeneratedCode("some name"), path);

            indentedTextWriterFactory
                .Received()
                .Create(path, Arg.Any<string>());
        }

        [Theory, AutoData]
        public void Save_CreatesIndentedTextWriterWithExpectedFileName(string expectedName)
        {
            var indentedTextWriterFactory = Substitute.For<IIndentedTextWriterFactory>();
            var codeSavingService = CreateCodeSavingService(indentedTextWriterFactory: indentedTextWriterFactory);
            var generatedCode = GetMockGeneratedCode(expectedName);

            codeSavingService.Save(generatedCode, "some path");

            indentedTextWriterFactory
                .Received()
                .Create(Arg.Any<string>(), Arg.Is<string>(name => name.StartsWith(expectedName) && name.EndsWith(".cs")));
        }

        [Fact]
        public void Save_GeneratesCodeFromCompiledUnit()
        {
            const string path = "some path";
            var indentedTextWriter = new IndentedTextWriter(Substitute.For<TextWriter>());
            var indentedTextWriterFactory = Substitute.For<IIndentedTextWriterFactory>();
            indentedTextWriterFactory
                .Create(path, Arg.Any<string>())
                .Returns(indentedTextWriter);
            var codeDomProvider = Substitute.For<CodeDomProvider>();
            var codeSavingService = CreateCodeSavingService(
                indentedTextWriterFactory: indentedTextWriterFactory,
                codeDomProvider: codeDomProvider);
            var generatedCode = GetMockGeneratedCode("some name");

            codeSavingService.Save(generatedCode, path);

            codeDomProvider
                .Received()
                .GenerateCodeFromCompileUnit(
                    generatedCode,
                    indentedTextWriter,
                    Arg.Any<CodeGeneratorOptions>());
        }

        private static ICodeSavingService CreateCodeSavingService(
            IIndentedTextWriterFactory indentedTextWriterFactory = null, CodeDomProvider codeDomProvider = null)
        {
            return new CodeSavingService(
                indentedTextWriterFactory ?? Substitute.For<IIndentedTextWriterFactory>(),
                codeDomProvider           ?? Substitute.For<CodeDomProvider>());
        }

        private static CodeCompileUnit GetMockGeneratedCode(string expectedName)
        {
            var generatedCode = new CodeCompileUnit();
            var type = new CodeTypeDeclaration(expectedName);
            var ns = new CodeNamespace();

            ns.Types.Add(type);
            generatedCode.Namespaces.Add(ns);

            return generatedCode;
        }
    }
}
