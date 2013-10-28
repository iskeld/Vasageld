using System;
using EldSharp.Vasageld.Core;
using EldSharp.Vasageld.Core.Interfaces;
using Moq;
using Xunit;

namespace EldSharp.Vasageld.UnitTests.Core
{
    public class ProjectIncrementorTests
    {
        [Fact]
        public void CtorThrowsWhenProjectFileNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ProjectIncrementor(null));
        }

        [Fact]
        public void CtorThrowsWhenFileDoesNotExist()
        {
            // Arrange
            var fsMock = new Mock<IFileSystem>();
            fsMock.Setup(fs => fs.FileExists(It.IsAny<string>())).Returns(false);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new ProjectIncrementor(fsMock.Object, "C:\\FakeFile"));
        }
    }
}
