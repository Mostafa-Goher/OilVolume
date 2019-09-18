using Math.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolumeCalculation
{
    public class VolumeCalculator : IVolumeCalculator
    {
        private readonly IMatrixBuilder matrixBuilder;

        public VolumeCalculator(IMatrixBuilder matrixBuilder)
        {
            this.matrixBuilder = matrixBuilder;
        }

        /// <summary>
        /// Base Horizon Offset compaired to the top horizon in meters
        /// </summary>
        public int BaseHorizonOffset { get; set; } = 100;

        /// <summary>
        /// Fluid Contact Depth in meters
        /// </summary>
        public int FluidContactDepth { get; set; } = 3000;

        public int PillarWidth { get; set; } = 200;
        public int PillarLength { get; set; } = 200;

        /// <summary>
        /// This will determine the matrix that accutally contains oil/gas and returen it in case
        /// the UI needs to render it
        /// </summary>
        public IMatrix GetMatrixTobeCaluclated(IMatrix topHorizon)
        {

            var baseHorizon = topHorizon.Add(BaseHorizonOffset * Constants.FeetsInMeter);

            var fluidContact = matrixBuilder.Dense(topHorizon.RowCount, topHorizon.ColumnCount, FluidContactDepth * Constants.FeetsInMeter);

            var thresholdSurface = fluidContact.PointwiseMinimum(baseHorizon);

            var distanceBetweenSurfaces = thresholdSurface.Subtract(topHorizon);
            return distanceBetweenSurfaces;
        }

        public double CalculateVolumeInFeet(IMatrix oilMatrix)
        {
            double volume = 0d;
            for (int row = 0; row < oilMatrix.RowCount; row++)
            {
                for (int column = 0; column < oilMatrix.ColumnCount; column++)
                {
                    var distance = oilMatrix[row, column];
                    if (distance <= 0)
                        continue;
                    volume += PillarWidth * PillarLength * distance;
                }
            }
            return volume;
        }

        public double CalculateVolumeInMeters(IMatrix oilMatrix)
        {
            var feet = CalculateVolumeInFeet(oilMatrix);
            return feet == 0 ? 0 : feet / Constants.FeetsInMeter;
        }

        public double CalculateVolumeInBarrels(IMatrix oilMatrix)
        {
            var meters = CalculateVolumeInMeters(oilMatrix);
            return meters * Constants.BarrelsInMeter;
        }
    }
}
