using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Math.LinearAlgebra.Tests
{
    [TestClass]
    public class MatrixTests
    {

        [TestMethod]
        [Description("Add scalar to a matrix should add it to all elements")]
        public void AddScalar()
        {
            var result = CreateAMatrix(5,10).Add(1);
            result[0, 0].Should().Be(6);
            result[1, 0].Should().Be(11);
        }


        [TestMethod]
        [Description("Add matrix to another matrix should add each element to its corresponding element")]
        public void AddMatrix()
        {
            var result = CreateAMatrix(5,10).Add(CreateAMatrix(5,10));
            result[0, 0].Should().Be(10);
            result[1, 0].Should().Be(20);
        }

        [TestMethod]
        [Description("Subtract scalar to a matrix should subtract it to all elements")]
        public void SubtractScalar()
        {
            var result = CreateAMatrix(5,10).Subtract(1);
            result[0, 0].Should().Be(4);
            result[1, 0].Should().Be(9);
        }


        [TestMethod]
        [Description("Subtract matrix to another matrix should subtract each element to its corresponding element")]
        public void SubtractMatrix()
        {
            var result = CreateAMatrix(5,10).Subtract(CreateAMatrix(5,10));
            result[0, 0].Should().Be(0);
            result[1, 0].Should().Be(0);
        }

        [TestMethod]
        [Description("PointwiseMaximum scalar to a matrix should compare the scalar to all elements")]
        public void PointwiseMaximumScalar()
        {
            var result1 = CreateAMatrix(5,10).PointwiseMaximum(1);
            result1[0, 0].Should().Be(5);
            result1[1, 0].Should().Be(10);

            var result2 = CreateAMatrix(5,10).PointwiseMaximum(10);
            result2[0, 0].Should().Be(10);
            result2[1, 0].Should().Be(10);
        }
        
        [TestMethod]
        [Description("PointwiseMaximum a matrix to another matrix should compare to all elements")]
        public void PointwiseMaximumMatrix()
        {
            var result1 = CreateAMatrix(5,10).PointwiseMaximum(CreateAMatrix(5,10));
            result1[0, 0].Should().Be(5);
            result1[1, 0].Should().Be(10);

            var result2 = CreateAMatrix(5, 10).PointwiseMaximum(CreateAMatrix(10, 20));
            result2[0, 0].Should().Be(10);
            result2[1, 0].Should().Be(20);
        }

        [TestMethod]
        public void PointwiseMinimumScalar()
        {
            var result1 = CreateAMatrix(5, 10).PointwiseMinimum(1);
            result1[0, 0].Should().Be(1);
            result1[1, 0].Should().Be(1);

            var result2 = CreateAMatrix(5, 10).PointwiseMinimum(10);
            result2[0, 0].Should().Be(5);
            result2[1, 0].Should().Be(10);
        }

        [TestMethod]
        public void PointwiseMinimumMatrix()
        {
            var result1 = CreateAMatrix(5, 10).PointwiseMinimum(CreateAMatrix(4, 9));
            result1[0, 0].Should().Be(4);
            result1[1, 0].Should().Be(9);

            var result2 = CreateAMatrix(5, 10).PointwiseMinimum(CreateAMatrix(10, 20));
            result2[0, 0].Should().Be(5);
            result2[1, 0].Should().Be(10);
        }

        [TestMethod]
        public void Clone()
        {
            var result = CreateAMatrix(5, 10).Clone();
            result[0, 0].Should().Be(5);
            result[1, 0].Should().Be(10);
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
