using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math.LinearAlgebra
{
    public class MatrixFactory : IMatrixFactory
    {
        public IMatrix CreateMatrix(int rows, int columns)
        {
            return new Matrix(rows, columns);
        }
    }
}
