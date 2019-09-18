using System;
using System.Linq;
using Math.LinearAlgebra;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

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
            matrixBuilder.Setup(m => m.Dense(It.IsAny<int>(), It.IsAny<int>(), 5)).Returns
                (new Matrix(2,2));
            Subject = new VolumeCalculator(matrixBuilder.Object);
        }

        [TestMethod]
        public void CalculateVolumeInFeet()
        {
        }
    }
}
