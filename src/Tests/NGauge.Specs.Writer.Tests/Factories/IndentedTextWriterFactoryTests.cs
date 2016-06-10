using System;
using System.IO;
using FluentAssertions;
using NGauge.Specs.Writer.Factories;
using NGauge.Specs.Writer.Services;
using NSubstitute;
using Ploeh.AutoFixture.Xunit2;
using SystemInterface.IO;
using Xunit;

namespace NGauge.Specs.Writer.Tests.Factories
{
    public sealed class IndentedTextWriterFactoryTests
    {
        public static readonly object[] NullEmptyOrWhitespace =
        {
            new object[] {""},
            new object[] {" "},
            new object[] {null},
        };

        [Fact]
        public void ctor_FolderCreationServiceRequired()
        {
            Assert.Throws<ArgumentNullException>(
                "folderCreationService",
                () => new IndentedTextWriterFactory(
                    null,
                    Substitute.For<IPath>()));
        }

        [Fact]
        public void ctor_PathRequired()
        {
            Assert.Throws<ArgumentNullException>(
                "path",
                () => new IndentedTextWriterFactory(
                    Substitute.For<IFolderCreationService>(),
                    null));
        }

        [Theory]
        [MemberData(nameof(NullEmptyOrWhitespace))]
        public void Create_PathRequired(string path)
        {
            var factory = CreateIndentedTextWriterFactory();

            Assert.Throws<ArgumentNullException>(
                "path",
                () => factory.Create(
                    path,
                    "some name"));
        }

        [Theory]
        [MemberData(nameof(NullEmptyOrWhitespace))]
        public void Create_NameRequired(string name)
        {
            var factory = CreateIndentedTextWriterFactory();

            Assert.Throws<ArgumentNullException>(
                "name",
                () => factory.Create(
                    "some path",
                    name));
        }

        [Theory, AutoData]
        public void Save_EnsuresPathExists(string path)
        {
            var folderCreationService = Substitute.For<IFolderCreationService>();
            var factory = CreateIndentedTextWriterFactory(folderCreationService);

            factory.Create(path, "some name");

            folderCreationService
                .Received()
                .EnsureExists(path);
        }

        [Theory, AutoData]
        public void Save_GeneratesExpectedPathAndFileName(string path, string name)
        {
            var pathWrap = GetMockPath();
            var factory = CreateIndentedTextWriterFactory(path: pathWrap);

            factory.Create(path, name);

            pathWrap
                .Received()
                .Combine(path, name);
        }

        [Fact]
        public void Save_ReturnsInstanceOfIndentedTextWriter()
        {
            var pathWrap = Substitute.For<IPath>();
            pathWrap
                .Combine(Arg.Any<string>(), Arg.Any<string>())
                .Returns(Path.GetTempFileName());

            var factory = CreateIndentedTextWriterFactory(path: pathWrap);

            var indentedTextWriter = factory.Create("some path", "some file");

            indentedTextWriter
                .Should()
                .NotBeNull();
        }

        private static IIndentedTextWriterFactory CreateIndentedTextWriterFactory(
            IFolderCreationService folderCreationService = null, IPath path = null)
        {
            return new IndentedTextWriterFactory(
                folderCreationService ?? Substitute.For<IFolderCreationService>(),
                path                  ?? GetMockPath());
        }

        private static IPath GetMockPath()
        {
            var path = Substitute.For<IPath>();

            path
                .Combine(Arg.Any<string>(), Arg.Any<string>())
                .Returns(Path.GetTempFileName());

            return path;
        }
    }
}
