namespace Math.LinearAlgebra
{
    public interface IMatrixFactory
    {
        IMatrix CreateMatrix(int rows, int columns);
    }
}