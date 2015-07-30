using Microsoft.Practices.ServiceLocation;
using Nova.LED.StadiumBrightnessTool.ViewModel;
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

namespace Nova.LED.StadiumBrightnessTool
{
    /// <summary>
    /// LayoutTab.xaml 的交互逻辑
    /// </summary>
    public partial class LayoutTab : UserControl
    {
         
        public LayoutTab()
        {
            InitializeComponent();
            //var viewModel = ServiceLocator.Current.GetInstance<LocationProfileViewModel>();
            this.Loaded += LayoutTab_Loaded;
           //this.DataContext = viewModel;
        }

         void LayoutTab_Loaded(object sender, RoutedEventArgs e)
         {
             var viewModel = ServiceLocator.Current.GetInstance<LocationProfileViewModel>();
             //this.Loaded += LayoutTab_Loaded;
             this.DataContext = viewModel;
         }
    }
}
