using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using IOC;
using Ninject;

namespace UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IKernel kernel;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            kernel = NinjectKernel.CreateKernel();
            Current.MainWindow = kernel.Get<MainWindow>();
            Current.MainWindow.Show();
        }
    }
}
