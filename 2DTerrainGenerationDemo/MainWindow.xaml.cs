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
        CellularAutomata automata;

        public MainWindow()
        {
            InitializeComponent();
            automata = new CellularAutomata();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            int width = 150,
                height = 150;

            //var pixelmap = new MidpointDisplacement().Generate(new MidpointDisplacementOptions { Width = width, Height = height });
            var pixelmap = automata.Generate(new CellularAutomataOptions {
                Width = width,
                Height = height,
                Born = new int[] { 6, 7, 8 },
                Survive = new int[] { 4, 5, 6, 7, 8 }
            });

            this.image.Source = pixelmap.ToWriteableBitmap();
        }
    }
}
