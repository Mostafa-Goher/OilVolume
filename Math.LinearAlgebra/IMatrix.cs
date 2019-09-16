namespace Math.LinearAlgebra
{
    public interface IMatrix
    {
        double this[int row, int column] { get; set; }

        int ColumnCount { get; }
        int RowCount { get; }
        double[,] Storage { get; }

        IMatrix Add(double scalar);
        IMatrix Add(IMatrix IMatrixToAdd);
        IMatrix Clone();
        IMatrix PointwiseMaximum(double scalar);
        IMatrix PointwiseMaximum(IMatrix IMatrixToCompair);
        IMatrix PointwiseMinimum(double scalar);
        IMatrix PointwiseMinimum(IMatrix IMatrixToCompair);
        IMatrix Subtract(double scalar);
        IMatrix Subtract(IMatrix IMatrixToSubtract);
    }
}