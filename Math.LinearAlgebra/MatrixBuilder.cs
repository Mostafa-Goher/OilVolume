using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text.RegularExpressions;

namespace Math.LinearAlgebra
{
    public class MatrixBuilder : IMatrixBuilder
    {
        private const string WhiteSpaceRegexTemplate = "\\([^\\)]*\\)|'[^']*'|\"[^\"]*\"|[^\\s]*";
        private readonly IFileSystem fileSystem;
        private readonly IMatrixFactory matrixFactory;

        public MatrixBuilder(IFileSystem fileSystem, IMatrixFactory matrixFactory)
        {
            this.fileSystem = fileSystem;
            this.matrixFactory = matrixFactory;
        }

        /// <summary>
        /// Create a Matrix object with all elemnts equal to a scalar value
        /// </summary>
        public IMatrix Dense(int rows, int columns, double value)
        {
            var matrix = matrixFactory.CreateMatrix(rows, columns);
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    matrix[row, column] = value;
                }
            }
            return matrix;
        }

        /// <summary>
        /// Create a Matrix object from file where values are seperated by a white space
        /// </summary>
        public IMatrix FromFile(string filePath)
        {
            var data = new List<double[]>();
            using (var reader = fileSystem.File.OpenText(filePath))
            {
                var line = reader.ReadLine();
                var regex = new Regex(WhiteSpaceRegexTemplate, RegexOptions.Compiled);

                while (line != null)
                {
                    line = line.Trim();

                    if (line.Length > 0)
                    {
                        var matches = regex.Matches(line);

                        var row = (from Match match in matches where match.Length > 0 select Parse(match.Value)).ToArray();
                        data.Add(row);
                    }
                    line = reader.ReadLine();
                }
            }

            var matrix = matrixFactory.CreateMatrix(data.Count, data.FirstOrDefault()?.Length ?? 0);
            for (int row = 0; row < matrix.RowCount; row++)
            {
                for (int column = 0; column < matrix.ColumnCount; column++)
                {
                    matrix[row, column] = data[row].ElementAt(column);
                }
            }
            return matrix;
        }

        private double Parse(string str)
        {
            if (double.TryParse(str, out var result))
                return result;
            return 0;
        }
    }
}
