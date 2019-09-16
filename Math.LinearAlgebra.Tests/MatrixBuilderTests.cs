using System;
using System.IO.Abstractions.TestingHelpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Math.LinearAlgebra.Tests
{
    [TestClass]
    public class MatrixBuilderTests
    {
        private IMatrixBuilder matrixBuilder;
        private const string mockPathValid = nameof(mockPathValid);
        private const string mockPathInValid = nameof(mockPathInValid);
        private const string mockPathEmpty = nameof(mockPathEmpty);


        [TestInitialize]
        public void TestInitialize()
        {
            var mockFileSystem = new MockFileSystem();

            var mockValidFile = new MockFileData("55 60\n100 200");
            var mockInValidFile = new MockFileData("55 60\n100 A");
            var mockEmptyFile = new MockFileData(string.Empty);

            mockFileSystem.AddFile(mockPathValid, mockValidFile);
            mockFileSystem.AddFile(mockPathInValid, mockInValidFile);
            mockFileSystem.AddFile(mockPathEmpty, mockEmptyFile);

            matrixBuilder = new MatrixBuilder(mockFileSystem, new MatrixFactory());
        }

        [TestMethod]
        public void Dense()
        {
            var result = matrixBuilder.Dense(2, 1, 10);
            result.Storage.Should().HaveCount(2);
            result[0, 0].Should().Be(10);
            result[1, 0].Should().Be(10);
        }

        [TestMethod]
        public void FromValidFile()
        {
            var result = matrixBuilder.FromFile(mockPathValid);
            result.Storage.Should().HaveCount(4);
            result[0, 0].Should().Be(55);
            result[0, 1].Should().Be(60);
            result[1, 0].Should().Be(100);
            result[1, 1].Should().Be(200);
        }

        [TestMethod]
        [Description("Incase the file conatails none parsable items - 'A' for example - it will be considered a 0")]
        public void FromInValidFile()
        {
            var result = matrixBuilder.FromFile(mockPathInValid);
            result.Storage.Should().HaveCount(4);
            result[0, 0].Should().Be(55);
            result[0, 1].Should().Be(60);
            result[1, 0].Should().Be(100);
            result[1, 1].Should().Be(0);
        }

        [TestMethod]
        [Description("Incase the file is empty then an empty matrix is created")]
        public void FromInEmptyFile()
        {
            var result = matrixBuilder.FromFile(mockPathEmpty);
            result.Storage.Should().HaveCount(0);
        }
    }
}
