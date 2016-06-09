namespace NGauge.Specs.Writer.Tests
{
    public sealed class CodeSavingServiceTests
    {
        public void ctor_FolderCreationServiceRequired() { }

        public void ctor_IndentedTextWriterFactoryRequired() { }

        public void ctor_CodeDomProviderRequired() { }

        public void SaveAsync_CodeCompileUnitRequired() { }

        public void SaveAsync_PathRequired() { }

        public void SaveAsync_EnsuresPathExists() { }

        public void SaveAsync_CreatesIndentedTextWriterWithExpectedPath() { }

        public void SaveAsync_GeneratesCodeFromCompiledUnit() { }

        public void SaveAsync_ClosesIndentedTextWriter() { }
    }
}
