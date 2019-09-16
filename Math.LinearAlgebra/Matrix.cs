namespace Math.LinearAlgebra
{
    public class Matrix : IMatrix
    {
        public double[,] Storage { get; private set; }
        public int ColumnCount { get; }
        
        public int RowCount { get; }

        public double this[int row, int column]
        {
            get { return Storage[row, column]; }
            set { Storage[row, column] = value; }
        }

        public Matrix(int rows, int columns)
        {
            RowCount = rows;
            ColumnCount = columns;
            Storage = new double[RowCount, ColumnCount];
        }

        public IMatrix Add(IMatrix matrixToAdd)
        {
            var matrix = this.Clone();
            for (int row = 0; row < RowCount; row++)
            {
                for (int column = 0; column < ColumnCount; column++)
                {
                    matrix[row, column] += matrixToAdd[row, column];
                }
            }
            return matrix;
        }

        public IMatrix Add(double scalar)
        {
            var matrix = this.Clone();
            for (int row = 0; row < RowCount; row++)
            {
                for (int column = 0; column < ColumnCount; column++)
                {
                    matrix[row, column] += scalar;
                }
            }
            return matrix;
        }

        public IMatrix Subtract(double scalar)
        {
            var matrix = this.Clone();
            for (int row = 0; row < RowCount; row++)
            {
                for (int column = 0; column < ColumnCount; column++)
                {
                    matrix[row, column] -= scalar;
                }
            }
            return matrix;
        }

        public IMatrix Subtract(IMatrix matrixToSubtract)
        {
            var matrix = this.Clone();
            for (int row = 0; row < RowCount; row++)
            {
                for (int column = 0; column < ColumnCount; column++)
                {
                    matrix[row, column] -= matrixToSubtract[row, column];
                }
            }
            return matrix;
        }

        /// <summary>
        /// Compare a scalar value to all elemnts in the matrix and set the value for the maximum of the 2
        /// </summary>
        public IMatrix PointwiseMaximum(double scalar)
        {
            var matrix = this.Clone();
            for (int row = 0; row < RowCount; row++)
            {
                for (int column = 0; column < ColumnCount; column++)
                {
                    if(scalar > matrix[row, column])
                        matrix[row, column] = scalar;
                }
            }
            return matrix;
        }

        /// <summary>
        /// Compare a matrix to all elemnts in the matrix and set the value for the maximum of the 2
        /// </summary>
        public IMatrix PointwiseMaximum(IMatrix matrixToCompair)
        {
            var matrix = this.Clone();
            for (int row = 0; row < RowCount; row++)
            {
                for (int column = 0; column < ColumnCount; column++)
                {
                    if (matrixToCompair[row, column] > matrix[row, column])
                        matrix[row, column] = matrixToCompair[row, column];
                }
            }
            return matrix;
        }

        /// <summary>
        /// Compare a scalar value to all elemnts in the matrix and set the value for the minimum of the 2
        /// </summary>
        public IMatrix PointwiseMinimum(double scalar)
        {
            var matrix = this.Clone();
            for (int row = 0; row < RowCount; row++)
            {
                for (int column = 0; column < ColumnCount; column++)
                {
                    if (scalar < matrix[row, column])
                        matrix[row, column] = scalar;
                }
            }
            return matrix;
        }

        /// <summary>
        /// Compare a matrix to all elemnts in the matrix and set the value for the minimum of the 2
        /// </summary>
        public IMatrix PointwiseMinimum(IMatrix matrixToCompair)
        {
            var matrix = this.Clone();
            for (int row = 0; row < RowCount; row++)
            {
                for (int column = 0; column < ColumnCount; column++)
                {
                    if (matrixToCompair[row, column] < matrix[row, column])
                        matrix[row, column] = matrixToCompair[row, column];
                }
            }
            return matrix;
        }

        public IMatrix Clone()
        {
            var matrix = new Matrix(RowCount, ColumnCount);
            for (int row = 0; row < RowCount; row++)
            {
                for (int column = 0; column < ColumnCount; column++)
                {
                    matrix[row, column] = this[row, column];
                }
            }
            return matrix;
        }

        public override string ToString()
        {
            return $"{RowCount}x{ColumnCount}";
        }
    }
}
