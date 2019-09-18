using Math.LinearAlgebra;

namespace VolumeCalculation
{
    public interface IVolumeCalculator
    {
        int BaseHorizonOffset { get; set; }
        int FluidContactDepth { get; set; }
        int PillarLength { get; set; }
        int PillarWidth { get; set; }

        double CalculateVolumeInFeet(IMatrix oilMatrix);
        double CalculateVolumeInMeters(IMatrix oilMatrix);
        double CalculateVolumeInBarrels(IMatrix oilMatrix);

        IMatrix GetMatrixTobeCalculated(IMatrix topHorizon);
    }
}