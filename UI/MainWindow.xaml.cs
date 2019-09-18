using System;
using System.Windows;
using Math.LinearAlgebra;
using Microsoft.Win32;
using VolumeCalculation;

namespace UI
{
    public partial class MainWindow : Window
    {
        private readonly IVolumeCalculator volumeCalculator;
        private readonly IMatrixBuilder matrixBuilder;
        private IMatrix topHorizonMatrix;

        public MainWindow(IVolumeCalculator volumeCalculator,IMatrixBuilder matrixBuilder)
        {
            InitializeComponent();
            this.volumeCalculator = volumeCalculator;
            this.matrixBuilder = matrixBuilder;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                txtFilePath.Text = openFileDialog.FileName;
                var matrix = matrixBuilder.FromFile(openFileDialog.FileName);
                if (matrix != null)
                {
                    topHorizonMatrix = matrix;
                    lblDimensions.Content = topHorizonMatrix.ToString();
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (topHorizonMatrix != null)
            {
                volumeCalculator.PillarLength = upDownCellSize.Value.GetValueOrDefault();
                volumeCalculator.PillarWidth = upDownCellSize.Value.GetValueOrDefault();
                volumeCalculator.BaseHorizonOffset = upDownbhOffset.Value.GetValueOrDefault();
                volumeCalculator.FluidContactDepth=upDownfContact.Value.GetValueOrDefault();
                
                switch (((System.Windows.Controls.ComboBoxItem)cmbUnit.SelectedItem).Content)
                {
                    case "Feet":
                        MessageBox.Show($"Volume in Feet: {volumeCalculator.CalculateVolumeInFeet(topHorizonMatrix)}");
                        break;
                    case "Meters":
                        MessageBox.Show($"Volume in Meters: {volumeCalculator.CalculateVolumeInMeters(topHorizonMatrix)}");
                        break;
                    case "Barrels":
                        MessageBox.Show($"Volume in Barrels: {volumeCalculator.CalculateVolumeInBarrels(topHorizonMatrix)}");
                        break;
                }
            }
        }
    }
}
