using Hardborn.UI.MetroUI.Controls;
using Microsoft.Practices.ServiceLocation;
using Nova.LED.Infrastructure.Interfaces;
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
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
     [Export(typeof(IShell))]
    public partial class Shell : MetroWindow, IShell
    {
         public Shell()
        {
            InitializeComponent();
        }

         [Import]
         [SuppressMessage("Microsoft.Design", "CA1044:PropertiesShouldNotBeWriteOnly", Justification = "Needs to be a property to be composed by MEF")]
         protected ShellViewModel ViewModel
         {
             set
             {
                 this.statusBar.DataContext = value;
             }
         }

         //[Import]
         //[SuppressMessage("Microsoft.Design", "CA1044:PropertiesShouldNotBeWriteOnly", Justification = "Needs to be a property to be composed by MEF")]
         //protected LocationProfileViewModel ProfileViewModel
         //{
         //    get;
         //    set;
         //} 
    }
}
