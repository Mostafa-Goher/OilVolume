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
        /// This will determine the matrix that actually contains oil/gas and return it in case
        /// the UI needs to render it
        /// </summary>
        public IMatrix GetMatrixTobeCalculated(IMatrix topHorizon)
        {
            //Constructs the base horizon by adding a scalar to all values from top horizon
            var baseHorizon = topHorizon.Add(BaseHorizonOffset * Constants.FeetsInMeter);

            //create horizon for fluid contact
            var fluidContact = matrixBuilder.Dense(topHorizon.RowCount, topHorizon.ColumnCount, FluidContactDepth * Constants.FeetsInMeter);

            //create another horizon by comparing the fluid with base and choosing the minimum.
            var thresholdSurface = fluidContact.PointwiseMinimum(baseHorizon);

            //create a matrix to hold the distances between top and the threshold, negative values will be ignored during calculating the volume 
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
                    //every data point represents the hight of a pillar filled with oil, the volume of which is width * length  * hight
                    volume += PillarWidth * PillarLength * distance;
                }
            }
            return volume;
        }

        public double CalculateVolumeInMeters(IMatrix oilMatrix)
        {
            var feet = CalculateVolumeInFeet(oilMatrix);
            return feet == 0 ? 0 : feet * Constants.FeetsToMeter;
        }

        public double CalculateVolumeInBarrels(IMatrix oilMatrix)
        {
            var feet = CalculateVolumeInFeet(oilMatrix);
            return feet == 0 ? 0 : feet * Constants.FeetsToBarrels;
        }
    }
}
