using Nova.LED.Infrastructure.Interfaces;
using Nova.LED.Modules.Splash.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
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
using System.Windows.Threading;

namespace Nova.LED.Modules.Splash.View
{
    /// <summary>
    /// SplashView.xaml 的交互逻辑
    /// </summary>
    [Export]
    public partial class SplashView : Window
    {
        [ImportingConstructor]
        public SplashView(SplashViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
            this.Closed += SplashView_Closed;
            this.UIDispatcher = Dispatcher.CurrentDispatcher;
        }

        void SplashView_Closed(object sender, EventArgs e)
        {
            
        }

        [Export]
        public Dispatcher UIDispatcher { get; private set; }
     
    }
}
