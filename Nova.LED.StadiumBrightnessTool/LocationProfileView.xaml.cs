using Nova.LED.StadiumBrightnessTool.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
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

namespace Nova.LED.StadiumBrightnessTool
{
    /// <summary>
    /// LocationProfileView.xaml 的交互逻辑
    /// </summary>
    [Export]
    public partial class LocationProfileView : UserControl
    {
        private ZoomableCanvas _zoomableCanvas;
        public LocationProfileView()
        {
            InitializeComponent();
        }

        private void ItemsControl_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var x = Math.Pow(2, e.Delta / 3.0 / Mouse.MouseWheelDeltaForOneLine);
            _zoomableCanvas.Scale *= x;

            e.Handled = true;
        }

        private void ZoomCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            _zoomableCanvas = sender as ZoomableCanvas;
        }


    }
}
