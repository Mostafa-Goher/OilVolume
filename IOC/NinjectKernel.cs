using Math.LinearAlgebra;
using Ninject;
using Ninject.Syntax;
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
            Bind<IMatrixFactory, MatrixFactory>();
            Bind<IMatrixBuilder, MatrixBuilder>();
            Bind<IFileSystem, FileSystem>();
            Bind<IVolumeCalculator, VolumeCalculator>();

            
            return Kernel;
        }

        private static IBindingWhenInNamedWithOrOnSyntax<TConcrete> Bind<TInterface, TConcrete>()
          where TConcrete : TInterface
        {
            return Kernel.Bind<TInterface>().To<TConcrete>();
        }
    }
}
