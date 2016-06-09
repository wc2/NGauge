using System;
using System.CodeDom.Compiler;
using NSubstitute;
using Xunit;

namespace NGauge.Specs.Writer.Tests
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

        public void SaveAsync_CodeCompileUnitRequired() { }

        public void SaveAsync_PathRequired() { }

        public void SaveAsync_EnsuresPathExists() { }

        public void SaveAsync_CreatesIndentedTextWriterWithExpectedPath() { }

        public void SaveAsync_GeneratesCodeFromCompiledUnit() { }

        public void SaveAsync_ClosesIndentedTextWriter() { }
    }
}
