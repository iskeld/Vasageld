using System;
using EldSharp.Vasageld.Common;
using EldSharp.Vasageld.Core;
using EldSharp.Vasageld.Core.Interfaces;
using Moq;
using Xunit;

namespace EldSharp.Vasageld.UnitTests.Core
{
    public class FileIncrementorTests
    {
        [Fact]
        public void CtorThrowsWhenProjectFileNull()
        {
            Assert.Throws<ArgumentNullException>(() => new FileIncrementor(null));
        }

        [Fact]
        public void CtorThrowsWhenFileDoesNotExist()
        {
            // Arrange
            var fsMock = new Mock<IFileSystem>();
            fsMock.Setup(fs => fs.FileExists(It.IsAny<string>())).Returns(false);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new FileIncrementor(fsMock.Object, "C:\\FakeFile"));
        }

        [Fact]
        public void IncrementThrowsNotSupportedWhenProviderDoesNotSupportRequiredVersions()
        {
            // Arrange
            var vpMock = new Mock<IVersionsProvider>();
            vpMock.Setup(vp => vp.SupportedVersions).Returns(Versions.AssemblyVersion);
            IVersionsProvider singleVersionProvider = vpMock.Object;
            var incrementor = new FileIncrementor(SafeFileSystem, "C:\\dummy");

            // Act & Assert
            Assert.Throws<NotSupportedException>(() =>
                incrementor.Increment(singleVersionProvider, 
                    Versions.AssemblyVersion | Versions.AssemblyFileVersion));
        }

        private IFileSystem SafeFileSystem
        {
            get 
            { 
                var fsMock = new Mock<IFileSystem>();
                fsMock.Setup(fs => fs.FileExists(It.IsAny<string>())).Returns(true);
                return fsMock.Object;
            }
        }
    }
}
