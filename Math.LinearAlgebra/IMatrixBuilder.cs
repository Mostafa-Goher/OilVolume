namespace Math.LinearAlgebra
{
    public interface IMatrixBuilder
    {
        IMatrix Dense(int rows, int columns, double value);
        IMatrix FromFile(string filePath);
    }
}