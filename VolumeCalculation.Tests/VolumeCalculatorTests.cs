using System;
using System.Linq;
using Math.LinearAlgebra;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using FluentAssertions;

namespace VolumeCalculation.Tests
{
    [TestClass]
    public class VolumeCalculatorTests
    {
        private IVolumeCalculator Subject;
        private Mock<IMatrixBuilder> matrixBuilder;

        [TestInitialize]
        public void TestInitialize()
        {
            matrixBuilder = new Mock<IMatrixBuilder>();
            
            Subject = new VolumeCalculator(matrixBuilder.Object);
        }


        [TestMethod]
        public void GetMatrixTobeCalculated()
        {
            var fluidContact = new Mock<IMatrix>();
            var topHorizon = new Mock<IMatrix>();
            var thresholdSurface = new Mock<IMatrix>();

            fluidContact.Setup(s => s.Storage).Returns(CreateAMatrix(7, 11).Storage);

            topHorizon.Setup(s => s.Storage).Returns(CreateAMatrix(5, 5).Storage);

            thresholdSurface.Setup(s => s.Storage).Returns(CreateAMatrix(7, 10).Storage);

            topHorizon.Setup(a => a.Add(It.IsAny<double>())).Returns(CreateAMatrix(10, 10));

            fluidContact.Setup(a => a.PointwiseMinimum(It.IsAny<IMatrix>())).Returns(thresholdSurface.Object);

            thresholdSurface.Setup(a => a.Subtract(It.IsAny<IMatrix>())).Returns(CreateAMatrix(2, 5));

            matrixBuilder.Setup(d => d.Dense(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<double>())).Returns(fluidContact.Object);

            var result = Subject.GetMatrixTobeCalculated(topHorizon.Object);
            
            topHorizon.Verify(v => v.Add(328.08398950000003), Times.Once);
            fluidContact.Verify(v => v.PointwiseMinimum(It.IsAny<IMatrix>()), Times.Once);
            thresholdSurface.Verify(v => v.Subtract(It.IsAny<IMatrix>()), Times.Once);
            matrixBuilder.Verify(v => v.Dense(It.IsAny<int>(), It.IsAny<int>(), 9842.5196850000011), Times.Once);

            result.Storage.Should().BeEquivalentTo(CreateAMatrix(2, 5).Storage);
        }

        [TestMethod]
        public void CalculateVolumeInFeet()
        {
            var matrix = CreateAMatrix(5, 5);
            var result = Subject.CalculateVolumeInFeet(matrix);
            result.Should().Be(400000);
        }

        [TestMethod]
        public void CalculateVolumeInMeters()
        {
            var matrix = CreateAMatrix(5, 5);
            var result = Subject.CalculateVolumeInMeters(matrix);
            result.Should().Be(121920);
        }

        [TestMethod]
        public void CalculateVolumeInBarrels()
        {
            var matrix = CreateAMatrix(5, 5);
            var result = Subject.CalculateVolumeInBarrels(matrix);
            result.Should().Be(71244);
        }

        private Matrix CreateAMatrix(double elm1, double elm2)
        {
            var matrix = new Matrix(2, 1);
            matrix[0, 0] = elm1;
            matrix[1, 0] = elm2;
            return matrix;
        }
    }
}
