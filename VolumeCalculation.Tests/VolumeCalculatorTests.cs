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
            result.Should().Be(121920.00000048768);
        }

        [TestMethod]
        public void CalculateVolumeInBarrels()
        {
            var matrix = CreateAMatrix(5, 5);
            var result = Subject.CalculateVolumeInBarrels(matrix);
            result.Should().Be(766853.70457554737);
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
