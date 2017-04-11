using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Graphics.MidpointDisplacement;
using Graphics.Common;
using Graphics.CellularAutomata;

namespace TerrainGenerationDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            int width   = 1024, 
                height  = 768;

            //var pixelmap = new MidpointDisplacement().Generate(new MidpointDisplacementOptions { Width = width, Height = height });
            var pixelmap = new CellularAutomata().Generate(new CellularAutomataOptions { Width = width, Height = height });

            this.image.Source = pixelmap.ToWriteableBitmap();
        }
    }
}
