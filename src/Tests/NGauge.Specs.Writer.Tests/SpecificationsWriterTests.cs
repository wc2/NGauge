using System.Threading.Tasks;

namespace NGauge.Specs.Writer.Tests
{
    public sealed class SpecificationsWriterTests
    {
        public void ctor_GeneratedCodeNamingServiceRequired() { }
        public void ctor_SpecificationCodeGeneratorServiceRequired() { }
        public void ctor_FileDeletionServiceRequired() { }
        public void ctor_CodeSavingServiceRequired() { }
        public async Task WriteSpecificationsAsync_SpecificationsRequired() { }

        public async Task WriteSpecificationsAsync_ProjectPathRequired() { }

        public async Task WriteSpecificationsAsync_DeletesPreviouslyGeneratedCodeFile() { }
        public async Task WriteSpecificationsAsync_GeneratesSpecificationCode() { }
        public async Task WriteSpecificationsAsync_SavesGeneratedSpecificationCode() { }

        public async Task WriteSpecificationsAsync_ReturnsExpectedGeneratedCodeFileName() { }
    }
}
