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
        public void ctor_FolderCreationServiceRequired()
        {
            Assert.Throws<ArgumentNullException>(
                "folderCreationService",
                () => new CodeSavingService(
                    null,
                    Substitute.For<IIndentedTextWriterFactory>(),
                    Substitute.For<CodeDomProvider>()));
        }

        [Fact]
        public void ctor_IndentedTextWriterFactoryRequired()
        {
            Assert.Throws<ArgumentNullException>(
                "indentedTextWriterFactory",
                () => new CodeSavingService(
                    Substitute.For<IFolderCreationService>(),
                    null,
                    Substitute.For<CodeDomProvider>()));
        }

        [Fact]
        public void ctor_CodeDomProviderRequired()
        {
            Assert.Throws<ArgumentNullException>(
                "codeDomProvider",
                () => new CodeSavingService(
                    Substitute.For<IFolderCreationService>(),
                    Substitute.For<IIndentedTextWriterFactory>(),
                    null));
        }

        [Fact]
        public void Save_CodeCompileUnitRequired()
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

        [Theory, AutoData]
        public void Save_EnsuresPathExists(string path)
        {
            var folderCreationService = Substitute.For<IFolderCreationService>();
            var codeSavingService = CreateCodeSavingService(folderCreationService);

            codeSavingService.Save(Substitute.For<CodeCompileUnit>(), path);

            folderCreationService
                .Received()
                .EnsureExists(path);
        }

        [Theory, AutoData]
        public void Save_CreatesIndentedTextWriterWithExpectedPath(string path)
        {
            var indentedTextWriterFactory = Substitute.For<IIndentedTextWriterFactory>();
            var codeSavingService = CreateCodeSavingService(indentedTextWriterFactory: indentedTextWriterFactory);

            codeSavingService.Save(Substitute.For<CodeCompileUnit>(), path);

            indentedTextWriterFactory
                .Received()
                .Create(path);
        }

        [Fact]
        public void Save_GeneratesCodeFromCompiledUnit()
        {
            const string path = "some path";
            var indentedTextWriter = new IndentedTextWriter(Substitute.For<TextWriter>());
            var indentedTextWriterFactory = Substitute.For<IIndentedTextWriterFactory>();
            indentedTextWriterFactory
                .Create(path)
                .Returns(indentedTextWriter);
            var codeDomProvider = Substitute.For<CodeDomProvider>();
            var codeSavingService = CreateCodeSavingService(
                indentedTextWriterFactory: indentedTextWriterFactory,
                codeDomProvider: codeDomProvider);
            var codeCompileUnit = Substitute.For<CodeCompileUnit>();

            codeSavingService.Save(codeCompileUnit, path);

            codeDomProvider
                .Received()
                .GenerateCodeFromCompileUnit(
                    codeCompileUnit,
                    indentedTextWriter,
                    Arg.Any<CodeGeneratorOptions>());
        }

        private static ICodeSavingService CreateCodeSavingService(IFolderCreationService folderCreationService = null,
            IIndentedTextWriterFactory indentedTextWriterFactory = null, CodeDomProvider codeDomProvider = null)
        {
            return new CodeSavingService(
                folderCreationService     ?? Substitute.For<IFolderCreationService>(),
                indentedTextWriterFactory ?? Substitute.For<IIndentedTextWriterFactory>(),
                codeDomProvider           ?? Substitute.For<CodeDomProvider>());
        }
    }
}
