using Math.LinearAlgebra;

namespace VolumeCalculation
{
    public interface IVolumeCalculator
    {
        int BaseHorizonOffset { get; set; }
        int FluidContactDepth { get; set; }
        int PillarLength { get; set; }
        int PillarWidth { get; set; }

        double CalculateVolume(IMatrix oilMatrix);
        IMatrix GetMatrixTobeCaluclated(IMatrix topHorizon);
    }
}