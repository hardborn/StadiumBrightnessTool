using Nova.LED.Modules.Box.Services;
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

namespace M3LCTService.Test
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        private M3LCTServiceProxy _serviceProxy;
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _serviceProxy = new M3LCTServiceProxy(this.Dispatcher);
            _serviceProxy.InitalizeServerProxy();
            _serviceProxy.RegisterToServer();
            _serviceProxy.LoadAllComBaseInfoFromHW();
        }


    }
}
