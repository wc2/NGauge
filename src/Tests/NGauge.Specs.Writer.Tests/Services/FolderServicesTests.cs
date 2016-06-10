using System;
using NGauge.Specs.Writer.Services;
using NSubstitute;
using Ploeh.AutoFixture.Xunit2;
using SystemInterface.IO;
using Xunit;

namespace NGauge.Specs.Writer.Tests.Services
{
    public sealed class FolderServicesTests
    {
        [Fact]
        public void ctor_DirectoryRequired()
        {
            Assert.Throws<ArgumentNullException>(
                "directory",
                () => new FolderServices(null));
        }

        [Theory, AutoData]
        public void Delete_DeletesDirectoryIfPathExists(string path)
        {
            var directory = Substitute.For<IDirectory>();
            directory
                .Exists(path)
                .Returns(true);

            IFolderDeletionService foldersService = new FolderServices(directory);

            foldersService.Delete(path);

            directory
                .Received()
                .Delete(path, true);
        }

        [Fact]
        public void Delete_DoesNotDeleteDirectoryIfPathDoesNotExist()
        {
            var directory = Substitute.For<IDirectory>();
            directory
                .Exists(Arg.Any<string>())
                .Returns(false);

            IFolderDeletionService foldersService = new FolderServices(directory);

            foldersService.Delete("some path");

            directory
                .DidNotReceive()
                .Delete(Arg.Any<string>(), Arg.Any<bool>());
        }

        [Theory, AutoData]
        public void EnsureExists_CreatesDirectoryIfPathDoesNotExist(string path)
        {
            var directory = Substitute.For<IDirectory>();
            directory
                .Exists(path)
                .Returns(false);

            IFolderCreationService foldersService = new FolderServices(directory);

            foldersService.EnsureExists(path);

            directory
                .Received()
                .CreateDirectory(path);
        }

        [Fact]
        public void EnsureExists_DoesNotCreateDirectoryIfPathExists()
        {
            var directory = Substitute.For<IDirectory>();
            directory
                .Exists(Arg.Any<string>())
                .Returns(true);

            IFolderCreationService foldersService = new FolderServices(directory);

            foldersService.EnsureExists("some path");

            directory
                .DidNotReceive()
                .CreateDirectory(Arg.Any<string>());
        }
    }
}
