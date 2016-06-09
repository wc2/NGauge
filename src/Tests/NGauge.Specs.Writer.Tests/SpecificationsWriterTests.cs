using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NGauge.Specs.Writer.Tests.Helpers;
using NSubstitute;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace NGauge.Specs.Writer.Tests
{
    public sealed class SpecificationsWriterTests
    {
        [Fact]
        public void ctor_GeneratedCodeNamingServiceRequired()
        {
            Assert.Throws<ArgumentNullException>(
                "generatedCodeNamingService",
                () => new SpecificationsWriter(
                    null,
                    Substitute.For<ISpecificationCodeGenerator>(),
                    Substitute.For<IFolderDeletionService>(),
                    Substitute.For<ICodeSavingService>()));
        }

        [Fact]
        public void ctor_SpecificationCodeGeneratorServiceRequired()
        {
            Assert.Throws<ArgumentNullException>(
                "specificationCodeGenerator",
                () => new SpecificationsWriter(
                    Substitute.For<IGeneratedCodeNamingService>(),
                    null,
                    Substitute.For<IFolderDeletionService>(),
                    Substitute.For<ICodeSavingService>()));
        }

        [Fact]
        public void ctor_FolderDeletionServiceRequired()
        {
            Assert.Throws<ArgumentNullException>(
                "folderDeletionService",
                () => new SpecificationsWriter(
                    Substitute.For<IGeneratedCodeNamingService>(),
                    Substitute.For<ISpecificationCodeGenerator>(),
                    null,
                    Substitute.For<ICodeSavingService>()));
        }

        [Fact]
        public void ctor_CodeSavingServiceRequired()
        {
            Assert.Throws<ArgumentNullException>(
                "codeSavingService",
                () => new SpecificationsWriter(
                    Substitute.For<IGeneratedCodeNamingService>(),
                    Substitute.For<ISpecificationCodeGenerator>(),
                    Substitute.For<IFolderDeletionService>(),
                    null));
        }

        [Fact]
        public async Task WriteSpecificationsAsync_SpecificationsRequired()
        {
            var writer = CreateSpecificationWriter();

            await Assert.ThrowsAsync<ArgumentNullException>(
                "specifications",
                () => writer.WriteSpecificationsAsync(
                    null,
                    "some path"));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public async Task WriteSpecificationsAsync_ProjectPathRequired(string projectPath)
        {
            var writer = CreateSpecificationWriter();

            await Assert.ThrowsAsync<ArgumentNullException>(
                "projectPath",
                () => writer.WriteSpecificationsAsync(
                    Enumerable.Empty<ISpecification>(),
                    projectPath));
        }

        [Theory, AutoData]
        public async Task WriteSpecificationsAsync_DeletesPreviouslyGeneratedCodeFiles(string projectPath,
            string generatedCodePath)
        {
            var generatedCodeNamingService = Substitute.For<IGeneratedCodeNamingService>();
            generatedCodeNamingService
                .GetGeneratedCodePath(projectPath)
                .Returns(generatedCodePath);

            var folderDeletionService = Substitute.For<IFolderDeletionService>();
            var writer = CreateSpecificationWriter(
                generatedCodeNamingService: generatedCodeNamingService,
                folderDeletionService: folderDeletionService);

            await writer.WriteSpecificationsAsync(Enumerable.Empty<ISpecification>(), projectPath);

            folderDeletionService
                .Received()
                .Delete(generatedCodePath);
        }

        [Fact]
        public async Task WriteSpecificationsAsync_GeneratesSpecificationCode()
        {
            var specifications = GetSample<ISpecification>(sampleSize: 3);
            var specificationCodeGenerator = Substitute.For<ISpecificationCodeGenerator>();
            var writer = CreateSpecificationWriter(specificationCodeGenerator: specificationCodeGenerator);

            await writer.WriteSpecificationsAsync(specifications, "some path");

            specifications
                .ForEach(specification =>
                    specificationCodeGenerator
                        .Received()
                        .GenerateCode(specification));
        }

        [Fact]
        public async Task WriteSpecificationsAsync_SavesGeneratedSpecificationCode()
        {
            const int sampleSize = 3;

            var specifications = GetSample<ISpecification>(sampleSize);
            var generatedCode = GetSample<CodeCompileUnit>(sampleSize);
            var specificationCodeGenerator = CreateMockCodeGenerator(
                specifications: specifications,
                generatedCode: generatedCode);

            var savedCode = new List<CodeCompileUnit>();
            var codeSavingService = Substitute.For<ICodeSavingService>();
            await codeSavingService
                .SaveAsync(
                    Arg.Do<CodeCompileUnit>(codeCompileUnit => savedCode.Add(codeCompileUnit)),
                    Arg.Any<string>());

            var writer = CreateSpecificationWriter(
                specificationCodeGenerator: specificationCodeGenerator,
                codeSavingService: codeSavingService);

            await writer.WriteSpecificationsAsync(specifications, "some path");

            savedCode
                .ShouldBeEquivalentTo(generatedCode);
        }

        [Theory, AutoData]
        public async Task WriteSpecificationsAsync_SavesGeneratedSpecificationCodeToExpectedLocation(string projectPath,
            string expectedGeneratedCodePath)
        {
            const int sampleSize = 1;

            var generatedCodeNamingService = Substitute.For<IGeneratedCodeNamingService>();
            generatedCodeNamingService
                .GetGeneratedCodePath(projectPath)
                .Returns(expectedGeneratedCodePath);

            var specifications = GetSample<ISpecification>(sampleSize);
            var specificationCodeGenerator = CreateMockCodeGenerator(
                specifications: specifications,
                generatedCode: GetSample<CodeCompileUnit>(sampleSize));

            var codeSavingService = Substitute.For<ICodeSavingService>();
            var writer = CreateSpecificationWriter(
                generatedCodeNamingService: generatedCodeNamingService,
                specificationCodeGenerator: specificationCodeGenerator,
                codeSavingService: codeSavingService);

            await writer.WriteSpecificationsAsync(specifications, projectPath);

            codeSavingService
                .Received()
                .SaveAsync(Arg.Any<CodeCompileUnit>(), expectedGeneratedCodePath)
                .IgnoreAwaitForNSubstituteAssertion();
        }

        [Theory, AutoData]
        public async Task WriteSpecificationsAsync_ReturnsExpectedGeneratedCodeFileName(string expectedGeneratedCodePath)
        {
            var generatedCodeNamingService = Substitute.For<IGeneratedCodeNamingService>();
            generatedCodeNamingService
                .GetGeneratedCodePath(Arg.Any<string>())
                .Returns(expectedGeneratedCodePath);

            var writer = CreateSpecificationWriter(
                generatedCodeNamingService: generatedCodeNamingService);

            var actualGeneratedCodePath = await writer.WriteSpecificationsAsync(GetSample<ISpecification>(), "some path");

            actualGeneratedCodePath
                .Should()
                .Be(expectedGeneratedCodePath);
        }

        private static ISpecificationsWriter CreateSpecificationWriter(
            IGeneratedCodeNamingService generatedCodeNamingService = null,
            ISpecificationCodeGenerator specificationCodeGenerator = null,
            IFolderDeletionService folderDeletionService = null, ICodeSavingService codeSavingService = null)
        {
            return new SpecificationsWriter(
                generatedCodeNamingService ?? Substitute.For<IGeneratedCodeNamingService>(),
                specificationCodeGenerator ?? Substitute.For<ISpecificationCodeGenerator>(),
                folderDeletionService      ?? Substitute.For<IFolderDeletionService>(),
                codeSavingService          ?? Substitute.For<ICodeSavingService>());
        }

        private static List<T> GetSample<T>(uint sampleSize = 0)
            where T : class
        {
            return Enumerable
                .Range(0, (int)sampleSize)
                .Select(_ => Substitute.For<T>())
                .ToList();
        }

        private static ISpecificationCodeGenerator CreateMockCodeGenerator(IReadOnlyList<ISpecification> specifications,
            IReadOnlyList<CodeCompileUnit> generatedCode)
        {
            var specificationCodeGenerator = Substitute.For<ISpecificationCodeGenerator>();

            for (var i = 0; i < specifications.Count; i++)
            {
                specificationCodeGenerator
                    .GenerateCode(specifications[i])
                    .Returns(generatedCode[i]);
            }

            return specificationCodeGenerator;
        }
    }
}
