using Microsoft.Practices.ServiceLocation;
using Nova.LED.Infrastructure;
using Nova.LED.Infrastructure.Behaviors;
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
    /// LayoutTab.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainRegion)]
    public partial class LayoutTab : UserControl
    {
         
        public LayoutTab()
        {
            InitializeComponent();          
            
            //this.Loaded += LayoutTab_Loaded;
        }


        [Import]
        [SuppressMessage("Microsoft.Design", "CA1044:PropertiesShouldNotBeWriteOnly", Justification = "Needs to be a property to be composed by MEF")]
        protected LocationProfileViewModel ViewModel
        {
            set
            {
                this.DataContext = value;
            }
        }

        
        //void LayoutTab_Loaded(object sender, RoutedEventArgs e)
        //{
        //    var viewModel = ServiceLocator.Current.GetInstance<LocationProfileViewModel>();
        //    //this.Loaded += LayoutTab_Loaded;
        //    this.DataContext = viewModel;
        //}
    }
}
