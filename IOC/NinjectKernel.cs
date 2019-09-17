using Math.LinearAlgebra;
using Ninject;
using System.IO.Abstractions;
using VolumeCalculation;

namespace IOC
{
    public static class NinjectKernel
    {
        public static IKernel Kernel { get; private set; }

        public static IKernel CreateKernel()
        {
            Kernel = new StandardKernel();

            Kernel.Bind<IMatrixFactory>().To<MatrixFactory>();
            Kernel.Bind<IMatrixBuilder>().To<MatrixBuilder>();
            Kernel.Bind<IFileSystem>().To<FileSystem>();
            Kernel.Bind<IVolumeCalculator>().To<VolumeCalculator>();
            
            return Kernel;
        }
    }
}
